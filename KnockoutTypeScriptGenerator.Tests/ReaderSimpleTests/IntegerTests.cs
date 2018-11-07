using Generator.SampleLibrary.SimpleClasses;
using KnockoutTypeScriptGenerator.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq;

namespace KnockoutTypeScriptGenerator.Tests.ReaderSimpleTests
{
    [TestClass]
    public class IntegerTests : SimpleTestBase
    {
        [TestMethod]
        public void Sample_SingleInt32()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var targetClass = classes.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_Int32));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_Int32.MyInt32Property),
                Type = typeof(int).ToString(),
                IsArray = false,
                IsNullable = false
            });

            targetClass.ShouldBe(expectedClass);
        }

        [TestMethod]
        public void Sample_SingleNullableInt32()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32_Nullable.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var targetClass = classes.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_Int32_Nullable));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_Int32_Nullable.MyInt32Property),
                Type = typeof(int).ToString(),
                IsArray = false,
                IsNullable = true
            });

            targetClass.ShouldBe(expectedClass);
        }

        [TestMethod]
        public void Sample_SingleArrayInt32()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32_Array.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var targetClass = classes.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_Int32_Array));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_Int32_Array.MyInt32ArrayProperty),
                Type = typeof(int).ToString(),
                IsArray = true,
                IsNullable = false
            });

            targetClass.ShouldBe(expectedClass);
        }

        [TestMethod]
        public void Sample_SingleInt32IEnumerable()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32_IEnumerable.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var targetClass = classes.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_Int32_IEnumerable));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_Int32_IEnumerable.MyInt32EnumerableProperty),
                Type = typeof(int).ToString(),
                IsArray = true,
                IsNullable = false
            });

            targetClass.ShouldBe(expectedClass);
        }

        [TestMethod]
        public void Sample_SingleInt32IList()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32_IList.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var targetClass = classes.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_Int32_IList));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_Int32_IList.MyInt32IListProperty),
                Type = typeof(int).ToString(),
                IsArray = true,
                IsNullable = false
            });

            targetClass.ShouldBe(expectedClass);
        }

        [TestMethod]
        public void Sample_Int32List()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32_List.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var targetClass = classes.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_Int32_List));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_Int32_List.MyInt32ListProperty),
                Type = typeof(int).ToString(),
                IsArray = true,
                IsNullable = false
            });

            targetClass.ShouldBe(expectedClass);
        }

        [TestMethod, Ignore]
        public void Sample_Int32ListOfLists()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32_ListOfLists.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var targetClass = classes.First();

            var expectedNamespace = $"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}";
            var expectedClass = new CsClass(expectedNamespace, nameof(Sample_Int32_ListOfLists));
            expectedClass.Properties.Add(new CsProperty
            {
                Name = nameof(Sample_Int32_ListOfLists.MyInt32ListOfListProperty),
                Type = typeof(int).ToString(), // well, this is not right
                IsArray = true,
                IsNullable = false
            });

            targetClass.ShouldBe(expectedClass);
        }
    }
}
