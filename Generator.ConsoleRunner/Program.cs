﻿using KnockoutTypeScriptGenerator;
using System;

namespace Generator.ConsoleRunner
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            const string solutionName = "KnockoutTypeScriptGenerator";
            //const string solutionName = "OldKnockoutTypeScriptGenerator";

            var dte = DteLoader.GetDteBySolutionName(solutionName);

            var processor = new CodeReader(dte);
            //processor.ProcessClassFile($@"{solutionName}\Generator.SampleReferencedLibrary\ExampleClass.cs");
            //processor.ProcessClassFile($@"{solutionName}\Generator.SampleLibrary\SimpleClasses\Sample_Int32.cs");

            //processor.ProcessClassFile($@"{solutionName}\Generator.SampleLibrary\SimpleClasses\enum\Sample_Enum.cs");
            processor.ProcessClassFile($@"{solutionName}\Generator.SampleLibrary\ComplexClasses\Sample_MultipleEnums_MultipleInts.cs");
            //processor.ProcessClassFile($@"{solutionName}\MyGenerator\Simple\Sample_NullableEnum.cs");

            //var rez = processor.GenerateInterface("Generator.SampleLibrary.ComplexClasses.Sample_MultipleEnums_MultipleInts");
            var rez = processor.GenerateInterface();
            Console.WriteLine(rez);


            Console.WriteLine("=============");
            Console.ReadKey();
        }
    }
}
