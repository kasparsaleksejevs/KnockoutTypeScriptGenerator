using System.Collections.Generic;

namespace KnockoutTypeScriptGenerator.Metadata
{
    public class GeneratorCodeEnum : IGeneratorCodeItem
    {
        public GeneratorCodeEnum(string classNamespace, string className, List<GeneratorCodeEnumField> enumMembers = null)
        {
            this.Namespace = classNamespace;
            this.Name = className;
            this.FullName = this.Namespace + "." + this.Name;
            if (enumMembers != null)
                this.EnumFields.AddRange(enumMembers);
        }

        public string Namespace { get; }

        public string Name { get; }

        public string FullName { get; }

        public List<GeneratorCodeEnumField> EnumFields { get; set; } = new List<GeneratorCodeEnumField>();
    }
}
