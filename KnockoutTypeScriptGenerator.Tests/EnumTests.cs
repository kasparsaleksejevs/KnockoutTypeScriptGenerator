using Generator.SampleLibrary.SimpleClasses;
using KnockoutTypeScriptGenerator.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq;

namespace KnockoutTypeScriptGenerator.Tests
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
            //var enums = processor.GetClassList().OfType<CsEnum>().ToList();

            classes.Count.ShouldBe(1);
            //enums.Count.ShouldBe(1);
            var targetClass = classes.First();

            targetClass.Name.ShouldBe(nameof(Sample_Enum));
            targetClass.Namespace.ShouldBe($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}");
            targetClass.Properties.Count.ShouldBe(1);

            var targetProperty = new CsProperty
            {
                Name = nameof(Sample_Enum.MyEnumProperty),
                Type = typeof(MyEnum).ToString(),
                IsArray = false,
                IsNullable = false
            };

            targetClass.Properties.First().ShouldBe(targetProperty);


        }

        [TestMethod]
        public void Sample_SingleNullableEnum()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);
            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\enum\Sample_NullableEnum.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var target = classes.First();

            target.Name.ShouldBe(nameof(Sample_NullableEnum));
            target.Namespace.ShouldBe($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}");
            target.Properties.Count.ShouldBe(1);

            var targetProperty = new CsProperty
            {
                Name = nameof(Sample_NullableEnum.MyEnumProperty),
                Type = typeof(MyEnum).ToString(),
                IsArray = false,
                IsNullable = true
            };

            target.Properties.First().ShouldBe(targetProperty);
        }
    }
}
