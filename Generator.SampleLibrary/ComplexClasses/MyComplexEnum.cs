using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Generator.SampleLibrary.ComplexClasses
{
    public enum MyComplexEnum
    {
        [Display(Name = "Some Enum Value 1")]
        SomeEnumValue1,

        [Description("Some Enum Value 2")]
        SomeEnumValue2 = 2,

        [Display(Name = nameof(SomeEnumValue3), ResourceType = typeof(TextResources))]
        SomeEnumValue3
    }
}
