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
        /// Note: description is written to the JS as is (without quotes to support variables); if it is a string value - single quotes must be added manually. 
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}
