namespace KnockoutTypeScriptGenerator.Metadata
{
    public class GeneratorCodeEnumField
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
