using System.Collections.Generic;

namespace KnockoutTypeScriptGenerator.Metadata
{
    public class CsClass : IStuff
    {
        public CsClass(string classNamespace, string className, List<CsProperty> properties = null)
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

        public List<CsProperty> Properties { get; } = new List<CsProperty>();
    }

    public interface IStuff
    {
        string Namespace { get; }
        string Name { get; }
        string FullName { get; }
    }
}
