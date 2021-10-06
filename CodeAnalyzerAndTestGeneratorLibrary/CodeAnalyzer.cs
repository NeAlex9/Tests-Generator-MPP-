using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CodeAnalyzerAndTestGeneratorLibrary.FileHolder;
using CodeAnalyzerAndTestGeneratorLibrary.FileInfoStructure;

namespace CodeAnalyzerAndTestGeneratorLibrary
{
    public static class CodeAnalyzer
    {
        public static FileInfo GetFileInfo(string code)
        {
            CompilationUnitSyntax root = CSharpSyntaxTree.ParseText(code).GetCompilationUnitRoot();
            var classes = new List<ClassInfo>();
            foreach (ClassDeclarationSyntax classDeclaration in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                classes.Add(GetClassInfo(classDeclaration));
            }

            return new FileInfo(classes);
        }

        public static ClassInfo GetClassInfo(ClassDeclarationSyntax classDeclaration)
        {
            var methods = new List<MethodInfo>();
            foreach (var method in classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>().Where((methodDeclaration) => methodDeclaration.Modifiers.Any((modifier) => modifier.IsKind(SyntaxKind.PublicKeyword))))
            {
                methods.Add(GetMethodInfo(method));
            }

            return new ClassInfo(methods, classDeclaration.Identifier.ValueText);
        }

        public static MethodInfo GetMethodInfo(MethodDeclarationSyntax method)
        {
            return new MethodInfo(method.Identifier.ValueText);
        }
    }
}
