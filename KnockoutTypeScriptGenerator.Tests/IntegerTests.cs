using Generator.SampleLibrary.SimpleClasses;
using KnockoutTypeScriptGenerator.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq;

namespace KnockoutTypeScriptGenerator.Tests
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
            var target = classes.First();

            target.Name.ShouldBe(nameof(Sample_Int32));
            target.Namespace.ShouldBe($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}");
            target.Properties.Count.ShouldBe(1);

            var targetProperty = new CsProperty
            {
                Name = nameof(Sample_Int32.MyInt32Property),
                Type = typeof(int).ToString(),
                IsArray = false,
                IsNullable = false
            };

            target.Properties.First().ShouldBe(targetProperty);
        }

        [TestMethod]
        public void Sample_SingleNullableInt32()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32_Nullable.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var target = classes.First();

            target.Name.ShouldBe(nameof(Sample_Int32_Nullable));
            target.Namespace.ShouldBe($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}");
            target.Properties.Count.ShouldBe(1);

            var targetProperty = new CsProperty
            {
                Name = nameof(Sample_Int32_Nullable.MyInt32Property),
                Type = typeof(int).ToString(),
                IsArray = false,
                IsNullable = true
            };

            target.Properties.First().ShouldBe(targetProperty);
        }

        [TestMethod]
        public void Sample_SingleArrayInt32()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32_Array.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var target = classes.First();

            target.Name.ShouldBe(nameof(Sample_Int32_Array));
            target.Namespace.ShouldBe($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}");
            target.Properties.Count.ShouldBe(1);

            var targetProperty = new CsProperty
            {
                Name = nameof(Sample_Int32_Array.MyInt32ArrayProperty),
                Type = typeof(int).ToString(),
                IsArray = true,
                IsNullable = false
            };

            target.Properties.First().ShouldBe(targetProperty);
        }

        [TestMethod]
        public void Sample_SingleInt32IEnumerable()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32_IEnumerable.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var target = classes.First();

            target.Name.ShouldBe(nameof(Sample_Int32_IEnumerable));
            target.Namespace.ShouldBe($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}");
            target.Properties.Count.ShouldBe(1);

            var targetProperty = new CsProperty
            {
                Name = nameof(Sample_Int32_IEnumerable.MyInt32EnumerableProperty),
                Type = typeof(int).ToString(),
                IsArray = true,
                IsNullable = false
            };

            target.Properties.First().ShouldBe(targetProperty);
        }

        [TestMethod]
        public void Sample_SingleInt32IList()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32_IList.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var target = classes.First();

            target.Name.ShouldBe(nameof(Sample_Int32_IList));
            target.Namespace.ShouldBe($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}");
            target.Properties.Count.ShouldBe(1);

            var targetProperty = new CsProperty
            {
                Name = nameof(Sample_Int32_IList.MyInt32IListProperty),
                Type = typeof(int).ToString(),
                IsArray = true,
                IsNullable = false
            };

            target.Properties.First().ShouldBe(targetProperty);
        }

        [TestMethod]
        public void Sample_Int32List()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32_List.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var target = classes.First();

            target.Name.ShouldBe(nameof(Sample_Int32_List));
            target.Namespace.ShouldBe($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}");
            target.Properties.Count.ShouldBe(1);

            var targetProperty = new CsProperty
            {
                Name = nameof(Sample_Int32_List.MyInt32ListProperty),
                Type = typeof(int).ToString(),
                IsArray = true,
                IsNullable = false
            };

            target.Properties.First().ShouldBe(targetProperty);
        }

        [TestMethod, Ignore]
        public void Sample_Int32ListOfLists()
        {
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}{simpleClassesPath}\int\Sample_Int32_ListOfLists.cs");

            var classes = processor.GetClassList().OfType<CsClass>().ToList();

            classes.Count.ShouldBe(1);
            var target = classes.First();

            target.Name.ShouldBe(nameof(Sample_Int32_ListOfLists));
            target.Namespace.ShouldBe($"{nameof(Generator)}.{nameof(Generator.SampleLibrary)}.{nameof(Generator.SampleLibrary.SimpleClasses)}");
            target.Properties.Count.ShouldBe(1);

            var targetProperty = new CsProperty
            {
                Name = nameof(Sample_Int32_ListOfLists.MyInt32ListOfListProperty),
                Type = typeof(int).ToString(), // well, this is not right
                IsArray = true,
                IsNullable = false
            };

            target.Properties.First().ShouldBe(targetProperty);
        }
    }
}
