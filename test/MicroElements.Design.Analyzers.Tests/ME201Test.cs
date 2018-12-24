using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using TestHelper;
using Xunit;

namespace MicroElements.Design.Analyzers.Tests
{
    public class ME201Test : CodeFixVerifier
    {
        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer() => new ME201ModelNameConvention();
        protected override CodeFixProvider GetCSharpCodeFixProvider() => new MicroElementsDesignCodeFixProvider();

        [Fact]
        public void EmptyCodeTest()
        {
            var test = @"";

            // No diagnostics expected to show up
            VerifyCSharpDiagnostic(test);
        }

        //Diagnostic and CodeFix both triggered and checked for
        [Fact]
        public void TestMethod2()
        {
            var test = @"
    using System;

    namespace ConsoleApplication1
    {
        public class SomeModel
        {   
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "ME201",
                Message = String.Format("Model type {0} should end with {1}", "SomeModel", "StorageModel"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 6, 22)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);

            var fixtest = @"
    using System;

    namespace ConsoleApplication1
    {
        class SomeStorageModel
        {   
        }
    }";
            //VerifyCSharpFix(test, fixtest);
        }
    }
}
