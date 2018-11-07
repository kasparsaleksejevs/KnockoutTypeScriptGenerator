using System;
using System.Collections.Generic;

namespace KnockoutTypeScriptGenerator.Metadata
{
    public class CsProperty
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

        public string GetJavascriptType()
        {
            var numericTypes = new List<string> { typeof(int).ToString(), typeof(long).ToString(), typeof(decimal).ToString(), typeof(float).ToString(), typeof(double).ToString(), typeof(byte).ToString() };
            var stringTypes = new List<string> { typeof(string).ToString(), typeof(char).ToString() };

            string jsPropertyType = null;

            if (numericTypes.Contains(this.Type))
                jsPropertyType = "number";
            else if (stringTypes.Contains(this.Type))
                jsPropertyType = "string";
            else if (this.Type == typeof(bool).ToString())
                jsPropertyType = "boolean";
            else if (this.Type == typeof(DateTime).ToString())
                jsPropertyType = "Date";
            else
                jsPropertyType = this.Type;

            return jsPropertyType;
        }
    }
}
