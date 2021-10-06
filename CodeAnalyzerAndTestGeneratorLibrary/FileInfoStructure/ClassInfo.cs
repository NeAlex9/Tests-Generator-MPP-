using System.Collections.Generic;

namespace CodeAnalyzerAndTestGeneratorLibrary.FileInfoStructure
{
    public class ClassInfo
    {
        public List<MethodInfo> Methods { get; private set; }
        public string ClassName{ get; private set; }

        public ClassInfo(List<MethodInfo> methods, string className)
        {
            this.Methods = methods;
            this.ClassName = className;
        }
    }
}
