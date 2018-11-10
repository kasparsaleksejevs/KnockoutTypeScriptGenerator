using System.Collections.Generic;

namespace KnockoutTypeScriptGenerator.Metadata
{
    public class GeneratorCodeClass : IGeneratorCodeItem
    {
        public GeneratorCodeClass(string classNamespace, string className, List<GeneratorCodeProperty> properties = null)
        {
            this.Namespace = classNamespace;
            this.Name = className;
            this.FullName = this.Namespace + "." + this.Name;
            if (properties != null)
                this.Properties.AddRange(properties);
        }

        public string Namespace { get; }

        public string Name { get; }

        public string FullName { get; }

        public List<GeneratorCodeProperty> Properties { get; } = new List<GeneratorCodeProperty>();
    }
}
