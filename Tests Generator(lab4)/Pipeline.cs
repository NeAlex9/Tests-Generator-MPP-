using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tests_Generator_lab4_
{
    public class Pipeline
    {
        public Task Generate(string sourcePath, string destinationPath, int maxPipelineTasks)
        {
            var execOptions = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = maxPipelineTasks };
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            var downloadString = new TransformBlock<string, string>
            (
                async path => await File.ReadAllTextAsync(path),
                execOptions
            );

            
        }
    }
}
