using KnockoutTypeScriptGenerator;
using System;

namespace Generator.ConsoleRunner
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            Interfaces();


            Console.WriteLine("=============");
            Console.ReadKey();
        }

        static void Controllers()
        {
            const string solutionName = "Test";
            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new ControllerMethodsReader(dte);
            processor.Process($@"{solutionName}\TestWeb\Controllers\ReportController.cs");

            Console.WriteLine("====");
            processor.Process($@"{solutionName}\TestWeb\Controllers\api\ReportController.cs");

        }

        static void Interfaces()
        {
            const string solutionName = "KnockoutTypeScriptGenerator";
            //const string solutionName = "OldKnockoutTypeScriptGenerator";

            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            processor.ProcessClassFile($@"{solutionName}\Generator.SampleLibrary\ComplexClasses\Sample_PropertiesWithRefToClass.cs");
            var rez = processor.GenerateInterface();
            //var rez = processor.GenerateInterface("Generator.SampleLibrary.ComplexClasses.Sample_MultipleEnums_MultipleInts");
            Console.WriteLine(rez);

            rez = processor.GenerateClass();
            Console.WriteLine(rez);
        }
    }
}
