namespace KnockoutTypeScriptGenerator.Metadata
{
    public class GeneratorCodeProperty
    {
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the full type name (namespace and class) of the property.
        /// </summary>
        /// <value>
        /// The full type name of the property.
        /// </value>
        /// <example>System.String; System.Int32; MyNamespace.MySubNamespace.MyClass.</example>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this property is an array (array, enumerable, list, collection, etc).
        /// </summary>
        /// <value>
        ///   <c>True</c> if this property is an array; otherwise, <c>false</c>.
        /// </value>
        public bool IsArray { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this property is nullable.
        /// If the <see cref="IsArray"/> is <c>true</c>, it shows whether the array element is nullable (as arrays in C# are nullable anyway).
        /// </summary>
        /// <value>
        ///   <c>True</c> if this property is nullable; otherwise, <c>false</c>.
        /// </value>
        public bool IsNullable { get; set; }
    }
}
