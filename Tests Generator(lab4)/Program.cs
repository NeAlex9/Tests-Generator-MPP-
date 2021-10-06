using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_Generator_lab4_
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathToFolder = @"..\..\FilesForTests";
            var filesName = new string[] { "Example1.cs", "Example3.cs"};
            var destPath = @"..\..\GeneratedFiles";
            var t = Task.Run(async ()=> await new Pipeline().Generate(pathToFolder, filesName, destPath, 1));
            Console.ReadLine();
        }
    }
}
