using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalyzerAndTestGeneratorLibrary.FilesForTests
{
    public class Example3
    {
        public IEnumerable<int> Interface { get; private set; }

        public Example3(IEnumerable<int> d){}
        public void Function1(int a, string b){}
        public void Function2(){}
    }

}
