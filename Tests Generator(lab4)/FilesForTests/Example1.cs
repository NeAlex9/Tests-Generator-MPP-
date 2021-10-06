using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalyzerAndTestGeneratorLibrary.FilesForTests
{
    public class Example1
    {
        public IEnumerable<int> Interface { get; private set; }

        public void Function1(){}
        public void Function2(){}
    }

    public class Example2
    {
        public IEnumerable<int> Interface { get; private set; }

        public void Function1() { }
        public void Function2() { }
    }
}
