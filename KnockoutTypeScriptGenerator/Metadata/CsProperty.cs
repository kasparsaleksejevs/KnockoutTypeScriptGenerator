using System;
using System.Collections.Generic;

namespace KnockoutTypeScriptGenerator.Metadata
{
    public class CsProperty
    {
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// String, number, date, boolean, etc, or a class with full namespace.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        public bool IsArray { get; set; }

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
