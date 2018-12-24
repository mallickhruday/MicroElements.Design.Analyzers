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
    public class ME101ModelShouldBeImmutable : DiagnosticAnalyzer
    {
        private const string Category = Analyzers.RuleGroup.ModelRules;
        private const string Id = Rules.ModelShouldBeImmutable;

        private static readonly string Title = "Model should be immutable";
        private static readonly string MessageFormat = "Model {0} should be immutable";
        private static readonly string Description = "Model should be immutable by conventions";
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
            // TODO: Replace the following code with your own analysis, generating Diagnostic objects for any issues you find
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;

            // Find just those named type symbols with names containing lowercase letters.
            if (namedTypeSymbol.Name.ToCharArray().Any(char.IsLower))
            {
                // For all such symbols, produce a diagnostic.
                var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);

                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
