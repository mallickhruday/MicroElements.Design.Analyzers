namespace MicroElements.Design.Analyzers
{
    using System.Collections.Immutable;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    /// <summary>
    /// Model should be immutable.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ME201ModelNameConvention : DiagnosticAnalyzer
    {
        private const string Category = RuleGroup.NamingRules;
        private const string Id = Rules.NameConventions;

        private static readonly string Title = "Model name does not match conventions";
        private static readonly string MessageFormat = "Model type {0} should end with {1}";
        private static readonly string Description = "Model name does not match conventions";
        private static readonly string HelpLink = $"https://github.com/micro-elements/MicroElements.Design.Analyzers/blob/master/docs/{Id}.md";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            Id,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Description,
            helpLinkUri: HelpLink);

        /// <inheritdoc />
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        /// <inheritdoc />
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();

            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;

            // Find just those named type symbols with names containing lowercase letters.
            string modelSuffix = "StorageModel";
            if (!namedTypeSymbol.Name.EndsWith(modelSuffix))
            {
                // For all such symbols, produce a diagnostic.
                var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name, modelSuffix);

                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
