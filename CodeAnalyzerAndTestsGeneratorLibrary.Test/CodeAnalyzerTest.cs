using System;
using System.Collections.Generic;
using System.IO;
using CodeAnalyzerAndTestGeneratorLibrary;
using CodeAnalyzerAndTestGeneratorLibrary.FileInfoStructure;
using NUnit.Framework;
using FileInfo = CodeAnalyzerAndTestGeneratorLibrary.FileHolder.FileInfo;

namespace CodeAnalyzerAndTestsGeneratorLibrary.Test
{
    [TestFixture]
    public class CodeAnalyzerTest
    {
        private bool IsEqualParameters(Dictionary<string, string> params1, Dictionary<string, string> params2)
        {
            try
            {
                CollectionAssert.AreEquivalent(params1, params2);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool IsEqualMethodInfo(MethodInfo method1, MethodInfo method2)
        {
            if (method1.Name != method2.Name || method1.ReturnType != method2.ReturnType || !IsEqualParameters(method1.Parameters, method2.Parameters))
            {
                return false;
            }

            return true;
        }

        private bool IsEqualConstructorInfo(ConstructorInfo constructorInfo1, ConstructorInfo constructorInfo2)
        {
            if (constructorInfo1.Name != constructorInfo1.Name || !IsEqualParameters(constructorInfo1.Parameters, constructorInfo1.Parameters))
            {
                return false;
            }

            return true;
        }

        private bool IsEqualClassInfo(ClassInfo classInfo1, ClassInfo classInfo2)
        {
            if (classInfo1.ClassName != classInfo2.ClassName || classInfo1.Methods.Count != classInfo2.Methods.Count || classInfo1.Constructors.Count != classInfo2.Constructors.Count)
            {
                return false;
            }

            for (int i = 0; i < classInfo1.Methods.Count; i++)
            {
                if (!IsEqualMethodInfo(classInfo1.Methods[i], classInfo2.Methods[i]))
                {
                    return false;
                }
            }

            for (int i = 0; i < classInfo1.Constructors.Count; i++)
            {
                if (!IsEqualConstructorInfo(classInfo1.Constructors[i], classInfo2.Constructors[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsEqualFileInfo(FileInfo file1, FileInfo file2)
        {
            if (file1.Classes.Count != file2.Classes.Count)
            {
                return false;
            }

            for (int i = 0; i < file1.Classes.Count; i++)
            {
                if (!IsEqualClassInfo(file1.Classes[i], file1.Classes[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static IEnumerable<TestCaseData> FileInfoProvider()
        {
            var fileInfo = new FileInfo(new List<ClassInfo>()
                {
                    new ClassInfo(
                        new List<MethodInfo>()
                        {
                            new MethodInfo(new Dictionary<string, string>() {{"a", "int"}, {"b", "string"}}, "Function1", "void"),
                            new MethodInfo(new Dictionary<string, string>(), "Function2", "void")
                        },
                        new List<ConstructorInfo>()
                        {
                            new ConstructorInfo(new Dictionary<string, string>(){{"d", "IEnumerable<int>" }}, "Example3" )
                        },
                        "Example3")
                }
            );
            yield return new TestCaseData("Example3.cs", fileInfo);

            fileInfo = new FileInfo(new List<ClassInfo>()
                {
                    new ClassInfo(
                        new List<MethodInfo>()
                        {
                            new MethodInfo(new Dictionary<string, string>(){{"d", "int"}, {"e", "int"}}, "Function1", "void"),
                            new MethodInfo(new Dictionary<string, string>(), "Function2", "void")
                        },
                        new List<ConstructorInfo>()
                        {
                            new ConstructorInfo(new Dictionary<string, string>(){{"s", "IDisposable" }, {"c", "ICloneable"}, {"a", "int"}, {"str", "string"}}, "Example1")
                        },
                        "Example1"),
                    new ClassInfo(
                        new List<MethodInfo>()
                        {
                            new MethodInfo(new Dictionary<string, string>(), "Function1", "void"),
                            new MethodInfo(new Dictionary<string, string>(), "Function2", "void")
                        },
                        new List<ConstructorInfo>(),
                        "Example2")
                }
            );
            yield return new TestCaseData("Example1.cs", fileInfo);
        }

        [Test, TestCaseSource(nameof(FileInfoProvider))]
        public void GetFileInfo_OneClassInFile_CorrectResult(string fileName, FileInfo fileInfo)
        {
            var path = @"B:\BSUIR\3 course\5 sem\СПП\lab\Tests Generator(lab4)\Tests Generator(lab4)\FilesForTests\" + fileName;
            using (var reader = new System.IO.StreamReader(path))
            {
                var content = reader.ReadToEnd();
                var actual = CodeAnalyzer.GetFileInfo(content);

                if (!IsEqualFileInfo(fileInfo, actual))
                {
                    throw new Exception("Not equal results");
                }
            }
        }
    }
}
