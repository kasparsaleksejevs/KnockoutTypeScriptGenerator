using KnockoutTypeScriptGenerator.Metadata;
using System;
using System.Collections.Generic;

namespace KnockoutTypeScriptGenerator
{
    public class InterfaceGenerator
    {
        private Dictionary<string, IGeneratorCodeItem> codeItems = new Dictionary<string, IGeneratorCodeItem>();

        public string GenerateInterface(IGeneratorCodeItem generatorCodeItem)
        {
            if (!codeItems.ContainsKey(generatorCodeItem.FullName))
                codeItems.Add(generatorCodeItem.FullName, generatorCodeItem);

            var result = string.Empty;
            switch (generatorCodeItem)
            {
                case GeneratorCodeClass codeClass:
                    result = this.GenerateInterfaceForClass(codeClass);
                    break;
                case GeneratorCodeEnum codeEnum:
                    result = this.GenerateInterfaceForEnum(codeEnum);
                    break;
                default:
                    break;
            }

            return result;
        }

        public string GenerateInterface(List<IGeneratorCodeItem> generatorCodeItems)
        {
            foreach (var generatorCodeItem in generatorCodeItems)
            {
                if (!codeItems.ContainsKey(generatorCodeItem.FullName))
                    codeItems.Add(generatorCodeItem.FullName, generatorCodeItem);
            }

            var result = string.Empty;
            foreach (var item in generatorCodeItems)
            {
                switch (item)
                {
                    case GeneratorCodeClass codeClass:
                        result += this.GenerateInterfaceForClass(codeClass);
                        break;
                    case GeneratorCodeEnum codeEnum:
                        result += this.GenerateInterfaceForEnum(codeEnum);
                        break;
                    default:
                        break;
                }

                result += "\r\n\r\n";
            }

            return result;
        }

        private string GenerateInterfaceForClass(GeneratorCodeClass generatorCodeClass)
        {
            var result = string.Empty;

            result += $"interface I{generatorCodeClass.Name} {{\r\n";
            foreach (var item in generatorCodeClass.Properties)
                result += $"\t{this.GetInterfacePropertyDescriptor(item)};\r\n";
            result += "}";

            return result;
        }

        private string GenerateInterfaceForEnum(GeneratorCodeEnum generatorCodeEnum)
        {
            var result = string.Empty;
            result += $"enum {generatorCodeEnum.Name} {{\r\n";
            foreach (var enumField in generatorCodeEnum.EnumFields)
                result += $"\t{enumField.Name} = {enumField.NumericValue},\r\n";
            result += "}\r\n\r\n";

            // Note: to get the text value of the enum use: `var text = SomeOtherEnumText[SomeOtherEnum.FirstValue];`
            result += $"const {generatorCodeEnum.Name}Text = new Map<number, string>([\r\n";
            foreach (var enumField in generatorCodeEnum.EnumFields)
                result += $"\t[{generatorCodeEnum.Name}.{enumField.Name}, {enumField.Description}],\r\n";
            result += "]);";

            return result;
        }

        private string GetInterfacePropertyDescriptor(GeneratorCodeProperty generatorCodeProperty)
        {
            var numericTypes = new HashSet<string> {
                typeof(int).ToString(),
                typeof(uint).ToString(),
                typeof(long).ToString(),
                typeof(ulong).ToString(),
                typeof(short).ToString(),
                typeof(ushort).ToString(),
                typeof(decimal).ToString(),
                typeof(float).ToString(),
                typeof(double).ToString(),
                typeof(byte).ToString(),
                typeof(sbyte).ToString()
            };
            var stringTypes = new HashSet<string> { typeof(string).ToString(), typeof(char).ToString() };

            string jsPropertyType = null;

            if (numericTypes.Contains(generatorCodeProperty.Type))
                jsPropertyType = "number";
            else if (stringTypes.Contains(generatorCodeProperty.Type))
                jsPropertyType = "string";
            else if (generatorCodeProperty.Type == typeof(bool).ToString())
                jsPropertyType = "boolean";
            else if (generatorCodeProperty.Type == typeof(DateTime).ToString())
                jsPropertyType = "Date";
            else
            {
                jsPropertyType = generatorCodeProperty.Type;
                if (this.codeItems.TryGetValue(generatorCodeProperty.Type, out IGeneratorCodeItem targetType))
                {
                    // determine if we need to show full namespace
                    var lastDotIndex = generatorCodeProperty.Type.LastIndexOf(".");
                    var propertyTypeNamespace = generatorCodeProperty.Type.Substring(0, lastDotIndex);
                    if (targetType.Namespace == propertyTypeNamespace)
                        jsPropertyType = jsPropertyType.Substring(lastDotIndex + 1);

                    jsPropertyType = "I" + jsPropertyType;
                }
                else
                    jsPropertyType = "any";
            }

            var arraySymbol = generatorCodeProperty.IsArray ? "[]" : string.Empty;
            var nullableSymbol = generatorCodeProperty.IsNullable && !generatorCodeProperty.IsArray ? "?" : string.Empty;
            var property = $"{generatorCodeProperty.Name}{nullableSymbol}: {jsPropertyType}{arraySymbol}";

            return property;
        }
    }
}
