using KnockoutTypeScriptGenerator.Metadata;
using Shouldly;
using System.Collections.Generic;

namespace KnockoutTypeScriptGenerator.Tests
{
    public static class ShouldlyExtensions
    {
        public static void ShouldBe(this GeneratorCodeProperty codeProperty, GeneratorCodeProperty expected)
        {
            codeProperty.ShouldSatisfyAllConditions(
                () => codeProperty.Name.ShouldBe(expected.Name),
                () => codeProperty.Type.ShouldBe(expected.Type),
                () => codeProperty.IsArray.ShouldBe(expected.IsArray),
                () => codeProperty.IsNullable.ShouldBe(expected.IsNullable));
        }

        public static void ShouldBe(this List<GeneratorCodeProperty> codeProperties, List<GeneratorCodeProperty> expected)
        {
            codeProperties.Count.ShouldBe(expected.Count);
            for (int i = 0; i < codeProperties.Count; i++)
                codeProperties[i].ShouldBe(expected[i]);
        }

        public static void ShouldBe(this GeneratorCodeClass codeClass, GeneratorCodeClass expected)
        {
            codeClass.ShouldSatisfyAllConditions(
                () => codeClass.Namespace.ShouldBe(expected.Namespace),
                () => codeClass.Name.ShouldBe(expected.Name),
                () => codeClass.Properties.ShouldBe(expected.Properties)
            );
        }

        public static void ShouldBe(this GeneratorCodeEnumField codeEnumField, GeneratorCodeEnumField expected)
        {
            codeEnumField.ShouldSatisfyAllConditions(
                () => codeEnumField.Name.ShouldBe(expected.Name),
                () => codeEnumField.NumericValue.ShouldBe(expected.NumericValue),
                () => codeEnumField.Description.ShouldBe(expected.Description)
            );
        }

        public static void ShouldBe(this List<GeneratorCodeEnumField> codeEnumFields, List<GeneratorCodeEnumField> expected)
        {
            codeEnumFields.Count.ShouldBe(expected.Count);
            for (int i = 0; i < codeEnumFields.Count; i++)
                codeEnumFields[i].ShouldBe(expected[i]);
        }

        public static void ShouldBe(this GeneratorCodeEnum codeEnum, GeneratorCodeEnum expected)
        {
            codeEnum.ShouldSatisfyAllConditions(
                () => codeEnum.Namespace.ShouldBe(expected.Namespace),
                () => codeEnum.Name.ShouldBe(expected.Name),
                () => codeEnum.EnumFields.ShouldBe(expected.EnumFields)
            );
        }
    }
}
