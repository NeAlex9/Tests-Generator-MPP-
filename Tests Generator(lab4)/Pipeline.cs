using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using CodeAnalyzerAndTestGeneratorLibrary;

namespace Tests_Generator_lab4_
{
    public class Pipeline
    {
        public Task Generate(string sourcePath, string[] fileNames, string destinationPath, int maxPipelineTasks)
        {
            var execOptions = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = maxPipelineTasks };
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            Directory.CreateDirectory(destinationPath);
            var downloadStringBlock = new TransformBlock<string, string>
            (
                async path =>
                {
                    using (var reader = new StreamReader(path))
                    {
                        return await reader.ReadToEndAsync();
                    }
                },
                execOptions
            );

            var generateTestsBlock = new TransformManyBlock<string, KeyValuePair<string, string>>
            (
                async sourceCode =>
                {
                    var fileInfo = await Task.Run(()=> CodeAnalyzer.GetFileInfo(sourceCode));
                    return await Task.Run(()=> TestsGenerator.GenerateTests(fileInfo));
                },
                execOptions
            );

            var writeFileBlock = new ActionBlock<KeyValuePair<string, string>>
            (
                async fileNameCodePair =>
                {
                    using (var writer = new StreamWriter(destinationPath + '\\' + fileNameCodePair.Key + ".cs"))
                    {
                        await writer.WriteAsync(fileNameCodePair.Value);
                    }
                },
                execOptions
            );

            downloadStringBlock.LinkTo(generateTestsBlock);
            generateTestsBlock.LinkTo(writeFileBlock);
            foreach (var fileName in fileNames)
            {
                downloadStringBlock.Post(sourcePath + @"\" + fileName);
            }

            downloadStringBlock.Complete();
            return writeFileBlock.Completion;
        }
    }
}
