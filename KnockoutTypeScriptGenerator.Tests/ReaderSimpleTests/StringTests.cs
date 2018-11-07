using Generator.SampleLibrary.SimpleClasses;
using KnockoutTypeScriptGenerator.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq;

namespace KnockoutTypeScriptGenerator.Tests.ReaderSimpleTests
{
    [TestClass]
    public class StringTests : SimpleTestBase
    {
        [TestMethod]
        public void Sample_SingleString()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);
            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\string\Sample_String.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var targetClass = classes.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_String));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_String.MyStringProperty),
                Type = typeof(string).ToString(),
                IsArray = false,
                IsNullable = false
            });

            targetClass.ShouldBe(expectedClass);
        }

        [TestMethod]
        public void Sample_SingleString_List()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);
            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\string\Sample_String_List.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var targetClass = classes.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_String_List));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_String_List.MyStringListProperty),
                Type = typeof(string).ToString(),
                IsArray = true,
                IsNullable = false
            });

            targetClass.ShouldBe(expectedClass);
        }
    }
}
