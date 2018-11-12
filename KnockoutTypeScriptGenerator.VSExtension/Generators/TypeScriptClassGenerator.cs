using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using System;
using System.IO;
using System.Text;

namespace KnockoutTypeScriptGenerator.VSExtension.Generators
{
    public sealed class TypeScriptClassGenerator : BaseCodeGeneratorWithSite
    {
        public const string Name = nameof(TypeScriptClassGenerator);
        public const string Description = "Automatically generates the .d.ts file based on the C#/VB model class.";

        public override string GetDefaultExtension()
        {
            return ".min.kek";
        }

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            ProjectItem item = Dte.Solution.FindProjectItem(inputFileName);
            var extension = Path.GetExtension(inputFileName);
            if (item != null)
            {
                try
                {
                    var processor = new CodeReader(dte);
                    processor.ProcessClassFile(inputFileName);
                    var result = processor.GenerateInterface();
                    return Encoding.UTF8.GetBytes(result);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return new byte[0];
        }

        public static string GetGeneratedFileName(string inputFileName)
        {
            return Path.ChangeExtension(inputFileName, ".d.ts");
        }
    }
}
