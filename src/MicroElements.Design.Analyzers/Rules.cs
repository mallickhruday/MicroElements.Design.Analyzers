namespace MicroElements.Design.Analyzers
{
    public static class Rules
    {
        /////////////////
        // Model rules //
        /////////////////

        public const string ModelShouldBeImmutable        = "ME101";
        public const string ModelShouldBeJsonSerializable = "ME102";
        public const string ModelShouldBeXmlSerializable  = "ME103";
        // 

        //////////////////
        // Naming rules //
        //////////////////

        // Ex: Type name should end with StorageModel
        public const string NameConventions = "ME201";

        //////////////////////
        // Dependency rules //
        //////////////////////

        // Contract can not use DomainModel
        // StorageModel can not use DomainModel 


        //////////////////////
        // Arch conventions //
        //////////////////////

        // Repository should return DomainModel
    }

    public static class RuleGroup
    {
        public const string ModelRules = "ModelRules";
        public const string NamingRules = "NamingRules";
        public const string DependencyRules = "DependencyRules";
    }


}
