using EnvDTE;
using KnockoutTypeScriptGenerator.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnockoutTypeScriptGenerator
{
    public class ControllerMethodsReader
    {
        private readonly DTE dte;

        public ControllerMethodsReader(DTE dte)
        {
            this.dte = dte;
        }

        public void Process(string fileName)
        {
            var projectItem = this.dte.Solution.FindProjectItem(fileName);

            var count = this.dte.Solution.Projects.Count;
            var name = this.dte.Solution.FullName;

            foreach (CodeElement element in projectItem.FileCodeModel.CodeElements)
            {
                if (element.Kind == vsCMElement.vsCMElementNamespace)
                {
                    var cn = (CodeNamespace)element;
                    foreach (CodeElement member in cn.Members)
                    {
                        if (member.Kind == vsCMElement.vsCMElementClass)
                            this.ProcessClass((CodeClass)member);
                    }
                }
            }
        }

        private void ProcessClass(CodeClass codeClass)
        {
            var funcs = codeClass.Members.OfType<CodeFunction>().Where(m => m.Access == vsCMAccess.vsCMAccessPublic);
            foreach (var item in funcs)
            {
                if (item.FunctionKind != vsCMFunction.vsCMFunctionFunction)
                    continue;
                var stuff = "public " + item.Name + "(";
                var lstParameters = new List<string>();
                foreach (CodeParameter parameter in item.Parameters)
                    lstParameters.Add($"{parameter.Type.AsFullName} {parameter.Name}");

                stuff += string.Join(", ", lstParameters);
                stuff += $") : {item.Type.AsFullName};";

                Console.WriteLine(stuff);
            }
        }
    }

    class GeneratorCodeController : IGeneratorCodeItem
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

        public string FullName { get; }
    }

    class GeneratorCodeControllerMethod
    {
        public string Name { get; set; }

        public GeneratorCodeProperty ReturnData { get; set; }

        public List<GeneratorCodeProperty> Parameters { get; set; }
    }


}
