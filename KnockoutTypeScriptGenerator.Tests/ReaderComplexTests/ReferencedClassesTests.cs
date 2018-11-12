using Generator.SampleLibrary.ComplexClasses;
using KnockoutTypeScriptGenerator.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq;

namespace KnockoutTypeScriptGenerator.Tests.ReaderComplexTests
{
    [TestClass]
    public class ReferencedClassesTests : ComplexTestBase
    {
        [TestMethod]
        public void SingleReferencedClass()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{complexClassesPath}\Sample_PropertiesWithRefToClass.cs");

            var classes = processor.GetGeneratorCodeItems().OfType<GeneratorCodeClass>().ToList();
            var enums = processor.GetGeneratorCodeItems().OfType<GeneratorCodeEnum>().ToList();

            classes.Count.ShouldBe(2);
            enums.Count.ShouldBe(1);

            var targetClass1 = classes.First(m => m.FullName == "Generator.SampleLibrary.ComplexClasses.Sample_PropertiesWithRefToClass");
            var targetClass2 = classes.First(m => m.FullName == "Generator.SampleLibrary.ComplexClasses.Sample_ReferencedClass");
            var targetEnum = enums.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.ComplexClasses)}";
            var expectedClass1 = new GeneratorCodeClass(expectedNamespace, nameof(Sample_PropertiesWithRefToClass));
            expectedClass1.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_PropertiesWithRefToClass.IntProperty),
                Type = typeof(int).ToString(),
                IsArray = false,
                IsNullable = false
            });
            expectedClass1.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_PropertiesWithRefToClass.ReferencedValue),
                Type = typeof(Sample_ReferencedClass).ToString(),
                IsArray = false,
                IsNullable = false
            });

            targetClass1.ShouldBe(expectedClass1);

            var expectedClass2 = new GeneratorCodeClass(expectedNamespace, nameof(Sample_ReferencedClass));
            expectedClass2.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_ReferencedClass.AnotherIntProperty),
                Type = typeof(int).ToString(),
                IsArray = false,
                IsNullable = false
            });
            expectedClass2.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_ReferencedClass.SomeStringValue),
                Type = typeof(string).ToString(),
                IsArray = false,
                IsNullable = false
            });
            expectedClass2.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_ReferencedClass.EnumValue),
                Type = typeof(MyComplexEnum).ToString(),
                IsArray = false,
                IsNullable = false
            });

            targetClass2.ShouldBe(expectedClass2);

            var expectedEnum = this.GetExpectedMyEnum();
            targetEnum.ShouldBe(expectedEnum);
        }

        private GeneratorCodeEnum GetExpectedMyEnum()
        {
            var expectedEnum = new GeneratorCodeEnum($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.ComplexClasses)}", nameof(MyComplexEnum));
            expectedEnum.EnumFields.Add(new GeneratorCodeEnumField { Name = nameof(MyComplexEnum.SomeEnumValue1), NumericValue = "0", Description = "\"Some Enum Value 1\"" });
            expectedEnum.EnumFields.Add(new GeneratorCodeEnumField { Name = nameof(MyComplexEnum.SomeEnumValue2), NumericValue = "2", Description = "\"Some Enum Value 2\"" });
            expectedEnum.EnumFields.Add(new GeneratorCodeEnumField { Name = nameof(MyComplexEnum.SomeEnumValue3), NumericValue = "3", Description = "TextResources.SomeEnumValue3" });

            return expectedEnum;
        }
    }
}
