using KnockoutTypeScriptGenerator.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnockoutTypeScriptGenerator
{
    public class InterfaceGenerator
    {
        private List<IStuff> codeItems = new List<IStuff>();

        public string GenerateInterface(IStuff stuff)
        {
            if (!codeItems.Any(m => m.FullName == stuff.FullName))
                codeItems.Add(stuff);

            var result = string.Empty;
            switch (stuff)
            {
                case CsClass csClass:
                    result = this.GenerateInterfaceForClass(csClass);
                    break;
                case CsEnum csEnum:
                    result = this.GenerateInterfaceForEnum(csEnum);
                    break;
                default:
                    break;
            }

            return result;
        }

        public string GenerateInterface(List<IStuff> stuff)
        {
            foreach (var item in stuff)
            {
                if (!codeItems.Any(m => m.FullName == item.FullName))
                    codeItems.Add(item);
            }

            var result = string.Empty;
            foreach (var item in stuff)
            {
                switch (item)
                {
                    case CsClass csClass:
                        result += this.GenerateInterfaceForClass(csClass);
                        break;
                    case CsEnum csEnum:
                        result += this.GenerateInterfaceForEnum(csEnum);
                        break;
                    default:
                        break;
                }

                result += "\r\n\r\n";
            }

            return result;
        }

        private string GenerateInterfaceForClass(CsClass csClass)
        {
            var result = string.Empty;

            result += $"interface I{csClass.Name} {{\r\n";
            foreach (var item in csClass.Properties)
                result += $"\t{this.GetInterfacePropertyDescriptor(item)};\r\n";
            result += "}";

            return result;
        }

        private string GenerateInterfaceForEnum(CsEnum csEnum)
        {
            var result = string.Empty;
            result += $"enum {csEnum.Name} {{\r\n";
            foreach (var enumField in csEnum.EnumFields)
                result += $"\t{enumField.Name} = {enumField.NumericValue},\r\n";
            result += "}\r\n\r\n";

            // Note: to get the text value of the enum use: `var text = SomeOtherEnumText[SomeOtherEnum.FirstValue];`
            result += $"const {csEnum.Name}Text = new Map<number, string>([\r\n";
            foreach (var enumField in csEnum.EnumFields)
                result += $"\t[{csEnum.Name}.{enumField.Name}, '{enumField.Description}'],\r\n";
            result += "]);";

            return result;
        }

        private string GetInterfacePropertyDescriptor(CsProperty csProperty)
        {
            var numericTypes = new List<string> { typeof(int).ToString(), typeof(long).ToString(), typeof(decimal).ToString(), typeof(float).ToString(), typeof(double).ToString(), typeof(byte).ToString() };
            var stringTypes = new List<string> { typeof(string).ToString(), typeof(char).ToString() };

            string jsPropertyType = null;

            if (numericTypes.Contains(csProperty.Type))
                jsPropertyType = "number";
            else if (stringTypes.Contains(csProperty.Type))
                jsPropertyType = "string";
            else if (csProperty.Type == typeof(bool).ToString())
                jsPropertyType = "boolean";
            else if (csProperty.Type == typeof(DateTime).ToString())
                jsPropertyType = "Date";
            else
            {
                jsPropertyType = csProperty.Type;

                var lastDotIndex = csProperty.Type.LastIndexOf(".");
                var propertyTypeNamespace = csProperty.Type.Substring(0, lastDotIndex);

                var targetType = this.codeItems.FirstOrDefault(m => m.FullName == csProperty.Type);
                if (targetType != null && targetType.Namespace == propertyTypeNamespace)
                    jsPropertyType = jsPropertyType.Substring(lastDotIndex + 1);
            }

            var arraySymbol = csProperty.IsArray ? "[]" : string.Empty;
            var nullableSymbol = csProperty.IsNullable && !csProperty.IsArray ? "?" : string.Empty;
            var property = $"{csProperty.Name}{nullableSymbol}: {jsPropertyType}{arraySymbol}";

            return property;
        }
    }
}
