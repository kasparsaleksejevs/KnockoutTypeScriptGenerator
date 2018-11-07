using KnockoutTypeScriptGenerator.Metadata;
using Shouldly;
using System.Collections.Generic;

namespace KnockoutTypeScriptGenerator.Tests
{
    public static class ShouldlyExtensions
    {
        public static void ShouldBe(this CsProperty csMember, CsProperty expected)
        {
            csMember.ShouldSatisfyAllConditions(
                () => csMember.Name.ShouldBe(expected.Name),
                () => csMember.Type.ShouldBe(expected.Type),
                () => csMember.IsArray.ShouldBe(expected.IsArray),
                () => csMember.IsNullable.ShouldBe(expected.IsNullable));
        }

        public static void ShouldBe(this List<CsProperty> csProperties, List<CsProperty> expected)
        {
            csProperties.Count.ShouldBe(expected.Count);
            for (int i = 0; i < csProperties.Count; i++)
                csProperties[i].ShouldBe(expected[i]);
        }

        public static void ShouldBe(this CsClass csClass, CsClass expected)
        {
            csClass.ShouldSatisfyAllConditions(
                () => csClass.Namespace.ShouldBe(expected.Namespace),
                () => csClass.Name.ShouldBe(expected.Name),
                () => csClass.Properties.ShouldBe(expected.Properties)
            );
        }

        public static void ShouldBe(this CsEnumField csEnumField, CsEnumField expected)
        {
            csEnumField.ShouldSatisfyAllConditions(
                () => csEnumField.Name.ShouldBe(expected.Name),
                () => csEnumField.NumericValue.ShouldBe(expected.NumericValue),
                () => csEnumField.Description.ShouldBe(expected.Description)
            );
        }

        public static void ShouldBe(this List<CsEnumField> csEnumFields, List<CsEnumField> expected)
        {
            csEnumFields.Count.ShouldBe(expected.Count);
            for (int i = 0; i < csEnumFields.Count; i++)
                csEnumFields[i].ShouldBe(expected[i]);
        }

        public static void ShouldBe(this CsEnum csEnum, CsEnum expected)
        {
            csEnum.ShouldSatisfyAllConditions(
                () => csEnum.Namespace.ShouldBe(expected.Namespace),
                () => csEnum.Name.ShouldBe(expected.Name),
                () => csEnum.EnumFields.ShouldBe(expected.EnumFields)
            );
        }
    }
}
