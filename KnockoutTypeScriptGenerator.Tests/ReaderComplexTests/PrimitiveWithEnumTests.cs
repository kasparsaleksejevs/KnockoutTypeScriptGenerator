using Generator.SampleLibrary.ComplexClasses;
using KnockoutTypeScriptGenerator.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq;

namespace KnockoutTypeScriptGenerator.Tests.ReaderComplexTests
{
    [TestClass]
    public class PrimitiveWithEnumTests : ComplexTestBase
    {
        [TestMethod]
        public void NullableEnum_WithInt()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{complexClassesPath}\Sample_EnumWithInt.cs");

            var classes = processor.GetGeneratorCodeItems().OfType<GeneratorCodeClass>().ToList();
            var enums = processor.GetGeneratorCodeItems().OfType<GeneratorCodeEnum>().ToList();

            classes.Count.ShouldBe(1);
            enums.Count.ShouldBe(1);

            var targetClass = classes.First();
            var targetEnum = enums.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.ComplexClasses)}";
            var expectedClass = new GeneratorCodeClass(expectedNamespace, nameof(Sample_EnumWithInt));
            // order is important...
            expectedClass.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_EnumWithInt.MyEnumProperty),
                Type = typeof(MyComplexEnum).ToString(),
                IsArray = false,
                IsNullable = true
            });
            expectedClass.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_EnumWithInt.MyIntProperty),
                Type = typeof(int).ToString(),
                IsArray = false,
                IsNullable = false
            });

            targetClass.ShouldBe(expectedClass);

            var expectedEnum = this.GetExpectedMyEnum();
            targetEnum.ShouldBe(expectedEnum);
        }

        [TestMethod]
        public void EnumList_WithStringList()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{complexClassesPath}\Sample_EnumListWithStringList.cs");

            var classes = processor.GetGeneratorCodeItems().OfType<GeneratorCodeClass>().ToList();
            var enums = processor.GetGeneratorCodeItems().OfType<GeneratorCodeEnum>().ToList();

            classes.Count.ShouldBe(1);
            enums.Count.ShouldBe(1);

            var targetClass = classes.First();
            var targetEnum = enums.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.ComplexClasses)}";
            var expectedClass = new GeneratorCodeClass(expectedNamespace, nameof(Sample_EnumListWithStringList));
            expectedClass.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_EnumListWithStringList.MyEnumList),
                Type = typeof(MyComplexEnum).ToString(),
                IsArray = true,
                IsNullable = false
            });
            expectedClass.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_EnumListWithStringList.MyStringList),
                Type = typeof(string).ToString(),
                IsArray = true,
                IsNullable = false
            });

            targetClass.ShouldBe(expectedClass);

            var expectedEnum = this.GetExpectedMyEnum();
            targetEnum.ShouldBe(expectedEnum);
        }

        [TestMethod]
        public void MultipleEnums_WithMultipleInts()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{complexClassesPath}\Sample_MultipleEnums_MultipleInts.cs");

            var classes = processor.GetGeneratorCodeItems().OfType<GeneratorCodeClass>().ToList();
            var enums = processor.GetGeneratorCodeItems().OfType<GeneratorCodeEnum>().ToList();

            classes.Count.ShouldBe(1);
            enums.Count.ShouldBe(2);

            var targetClass = classes.First();
            var targetMyEnum = enums.First(m => m.Name == nameof(MyComplexEnum));
            var targetOtherEnum = enums.First(m => m.Name == nameof(SomeOtherEnum));

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.ComplexClasses)}";
            var expectedClass = new GeneratorCodeClass(expectedNamespace, nameof(Sample_MultipleEnums_MultipleInts));
            expectedClass.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_MultipleEnums_MultipleInts.MyEnumProperty),
                Type = typeof(MyComplexEnum).ToString(),
                IsArray = false,
                IsNullable = false
            });
            expectedClass.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_MultipleEnums_MultipleInts.MyIntProperty),
                Type = typeof(int).ToString(),
                IsArray = false,
                IsNullable = false
            });
            expectedClass.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_MultipleEnums_MultipleInts.OtherEnumProperty),
                Type = typeof(SomeOtherEnum).ToString(),
                IsArray = false,
                IsNullable = true
            });
            expectedClass.Properties.Add(new GeneratorCodeProperty
            {
                Name = nameof(Sample_MultipleEnums_MultipleInts.MyIntListProperty),
                Type = typeof(int).ToString(),
                IsArray = true,
                IsNullable = true
            });

            targetClass.ShouldBe(expectedClass);

            var expectedMyEnum = this.GetExpectedMyEnum();
            targetMyEnum.ShouldBe(expectedMyEnum);

            var expectedOtherEnum = this.GetExpectedOtherEnum();
            targetOtherEnum.ShouldBe(expectedOtherEnum);
        }

        private GeneratorCodeEnum GetExpectedMyEnum()
        {
            var expectedEnum = new GeneratorCodeEnum($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.ComplexClasses)}", nameof(MyComplexEnum));
            expectedEnum.EnumFields.Add(new GeneratorCodeEnumField { Name = nameof(MyComplexEnum.SomeEnumValue1), NumericValue = "0" });
            expectedEnum.EnumFields.Add(new GeneratorCodeEnumField { Name = nameof(MyComplexEnum.SomeEnumValue2), NumericValue = "2" });
            expectedEnum.EnumFields.Add(new GeneratorCodeEnumField { Name = nameof(MyComplexEnum.SomeEnumValue3), NumericValue = "3" });

            return expectedEnum;
        }

        private GeneratorCodeEnum GetExpectedOtherEnum()
        {
            var expectedEnum = new GeneratorCodeEnum($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.ComplexClasses)}", nameof(SomeOtherEnum));
            expectedEnum.EnumFields.Add(new GeneratorCodeEnumField { Name = nameof(SomeOtherEnum.FirstValue), NumericValue = "1" });
            expectedEnum.EnumFields.Add(new GeneratorCodeEnumField { Name = nameof(SomeOtherEnum.SecondValue), NumericValue = "2" });
            expectedEnum.EnumFields.Add(new GeneratorCodeEnumField { Name = nameof(SomeOtherEnum.ThirdValue), NumericValue = "3" });

            return expectedEnum;
        }
    }
}
