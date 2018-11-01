using KnockoutTypeScriptGenerator.Metadata;
using Shouldly;

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
    }
}
