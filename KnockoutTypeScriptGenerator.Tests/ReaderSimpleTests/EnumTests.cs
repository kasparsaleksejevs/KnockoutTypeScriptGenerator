using Generator.SampleLibrary.SimpleClasses;
using KnockoutTypeScriptGenerator.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq;

namespace KnockoutTypeScriptGenerator.Tests.ReaderSimpleTests
{
    [TestClass]
    public class EnumTests : SimpleTestBase
    {
        [TestMethod]
        public void Sample_SingleEnum()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);
            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\enum\Sample_Enum.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();
            var enums = processor.GetClassList().OfType<CsEnum>().ToList();

            classes.Count.ShouldBe(1);
            enums.Count.ShouldBe(1);
            var targetClass = classes.First();
            var targetEnum = enums.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_Enum));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_Enum.MyEnumProperty),
                Type = typeof(MyEnum).ToString(),
                IsArray = false,
                IsNullable = false
            });

            targetClass.ShouldBe(expectedClass);

            var expectedEnum = this.GetExpectedMyEnum();
            targetEnum.ShouldBe(expectedEnum);
        }

        [TestMethod]
        public void Sample_SingleNullableEnum()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);
            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\enum\Sample_NullableEnum.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();
            var enums = processor.GetClassList().OfType<CsEnum>().ToList();

            classes.Count.ShouldBe(1);
            enums.Count.ShouldBe(1);

            var targetClass = classes.First();
            var targetEnum = enums.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_NullableEnum));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_NullableEnum.MyEnumProperty),
                Type = typeof(MyEnum).ToString(),
                IsArray = false,
                IsNullable = true
            });

            targetClass.ShouldBe(expectedClass);

            var expectedEnum = this.GetExpectedMyEnum();
            targetEnum.ShouldBe(expectedEnum);
        }

        [TestMethod]
        public void Sample_SingleEnumList()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);
            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\enum\Sample_Enum_List.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();
            var enums = processor.GetClassList().OfType<CsEnum>().ToList();

            classes.Count.ShouldBe(1);
            enums.Count.ShouldBe(1);

            var targetClass = classes.First();
            var targetEnum = enums.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_Enum_List));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_Enum_List.MyEnumListProperty),
                Type = typeof(MyEnum).ToString(),
                IsArray = true,
                IsNullable = false
            });

            targetClass.ShouldBe(expectedClass);

            var expectedEnum = this.GetExpectedMyEnum();
            targetEnum.ShouldBe(expectedEnum);
        }

        [TestMethod]
        public void Sample_SingleNullableEnumList()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);
            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\enum\Sample_NullableEnum_List.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();
            var enums = processor.GetClassList().OfType<CsEnum>().ToList();

            classes.Count.ShouldBe(1);
            enums.Count.ShouldBe(1);

            var targetClass = classes.First();
            var targetEnum = enums.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_NullableEnum_List));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_NullableEnum_List.MyNullableEnumListProperty),
                Type = typeof(MyEnum).ToString(),
                IsArray = true,
                IsNullable = true
            });

            targetClass.ShouldBe(expectedClass);

            var expectedEnum = this.GetExpectedMyEnum();
            targetEnum.ShouldBe(expectedEnum);
        }


        private CsEnum GetExpectedMyEnum()
        {
            var expectedEnum = new CsEnum($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}", nameof(MyEnum));
            expectedEnum.EnumFields.Add(new CsEnumField { Name = nameof(MyEnum.SomeValue1), NumericValue = "0" });
            expectedEnum.EnumFields.Add(new CsEnumField { Name = nameof(MyEnum.SomeValue2), NumericValue = "2" });
            expectedEnum.EnumFields.Add(new CsEnumField { Name = nameof(MyEnum.SomeValue3), NumericValue = "3" });

            return expectedEnum;
        }
    }
}
