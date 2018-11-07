using System.Collections.Generic;

namespace KnockoutTypeScriptGenerator.Metadata
{
    public class CsEnum : IStuff
    {
        public CsEnum(string classNamespace, string className, List<CsEnumField> enumMembers = null)
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

        public List<CsEnumField> EnumFields { get; set; } = new List<CsEnumField>();
    }

    public class CsEnumField
    {
        public string Name { get; set; }

        public string NumericValue { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// This is usually set by [Display] or [Description] attributes.
        /// Useful for enumerations.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}
