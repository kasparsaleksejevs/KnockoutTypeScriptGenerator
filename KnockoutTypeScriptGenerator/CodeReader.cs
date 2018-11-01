using EnvDTE;
using KnockoutTypeScriptGenerator.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KnockoutTypeScriptGenerator
{
    public class CodeReader
    {
        private static readonly Regex nullableTypeRegex = new Regex(@"^System\.Nullable<(.+)>$");

        private static readonly Regex genericTypeRegex = new Regex(@"^System\.Collections\.Generic\..*?<(.+)>$");

        private readonly DTE dte;

        private List<(string fileName, string fullClassName)> solutionItems = new List<(string fileName, string fullClassName)>();

        private List<IStuff> generatedClasses = new List<IStuff>();

        private List<string> classesFullNamesProcessed = new List<string>();
        private List<string> classesFullNamesToProcess = new List<string>();

        public CodeReader(DTE dte)
        {
            this.dte = dte;
        }

        public void ProcessClassFile(string fileName)
        {
            var projectItem = this.dte.Solution.FindProjectItem(fileName);

            foreach (CodeElement element in projectItem.FileCodeModel.CodeElements)
            {
                if (element.Kind == vsCMElement.vsCMElementNamespace)
                {
                    var cn = (CodeNamespace)element;
                    foreach (CodeElement member in cn.Members)
                    {
                        //Console.WriteLine($"Member: {member.FullName}");

                        if (member.Kind == vsCMElement.vsCMElementClass)
                            ProcessClass((CodeClass)member);
                        else if (member.Kind == vsCMElement.vsCMElementEnum)
                            ProcessEnum((CodeEnum)member);

                        // what else we could process?
                    }
                }
            }
        }

        public List<IStuff> GetClassList()
        {
            return this.generatedClasses.OrderBy(o => o.FullName).ToList();
        }

        private void ProcessClass(CodeClass codeClass)
        {
            var csClass = new CsClass(codeClass.Namespace.FullName, codeClass.Name);

            var properties = codeClass.Members.OfType<CodeProperty>().Where(m => m.Access == vsCMAccess.vsCMAccessPublic);
            foreach (var item in properties)
            {
                csClass.Properties.Add(this.ProcessProperty(item));
            }

            generatedClasses.Add(csClass);
        }

        private CsProperty ProcessProperty(CodeProperty property)
        {
            // do we even have the need to know if it is an enum?
            //var isEnum = false;
            //if (property.Type.TypeKind == vsCMTypeRef.vsCMTypeRefCodeType && property.Type.CodeType is CodeEnum)
            //    isEnum = true;

            var csMember = new CsProperty
            {
                Name = property.Name,
                IsNullable = property.Type.AsFullName.StartsWith("System.Nullable<"),
                IsArray = property.Type.TypeKind == vsCMTypeRef.vsCMTypeRefArray || property.Type.AsFullName.StartsWith("System.Collections"),
                Type = property.Type.AsFullName
            };

            if (csMember.IsNullable)
            {
                var nullableMatch = nullableTypeRegex.Match(csMember.Type);
                if (nullableMatch.Success)
                    csMember.Type = nullableMatch.Groups[1].Value;
                if (!this.IsSystemType(csMember.Type))
                {
                    // check if we have the type already generated
                    // TODO:

                    // find related class/enum projectItem

                    var codeElement = this.GetCodeElementByFullName(csMember.Type);
                    if (codeElement.Kind == vsCMElement.vsCMElementEnum)
                    {
                        //csMember.IsEnum = true;
                        ProcessEnum(codeElement as CodeEnum);
                    }
                }
            }

            if (csMember.IsArray)
            {
                var genericCollectionMatch = genericTypeRegex.Match(csMember.Type);
                if (genericCollectionMatch.Success)
                    csMember.Type = genericCollectionMatch.Groups[1].Value;
                else if (csMember.Type.EndsWith("[]"))
                    csMember.Type = csMember.Type.Substring(0, csMember.Type.Length - 2);
            }

            return csMember;
        }

        private bool IsSystemType(string typeName)
        {
            return typeName.StartsWith("System.");
        }

        /// <summary>
        /// Gets the C# project items, including those from subfolders.
        /// </summary>
        /// <param name="projectItems">The root project items.</param>
        /// <returns>Collection of C# project items.</returns>
        private List<ProjectItem> GetCsharpProjectItems(IEnumerable<ProjectItem> projectItems)
        {
            const string projectItemKindFolder = "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}";

            var lstCsProjItems = new List<ProjectItem>();
            foreach (var projItem in projectItems)
            {
                if (projItem.Kind == projectItemKindFolder)
                {
                    lstCsProjItems.AddRange(this.GetCsharpProjectItems(projItem.ProjectItems.OfType<ProjectItem>()));
                    continue;
                }

                if (projItem.Name.EndsWith(".cs"))
                    lstCsProjItems.Add(projItem);
            }

            return lstCsProjItems;
        }

        private CodeElement GetCodeElementByFullName(string fullName)
        {
            var lstCsProjItems = new List<ProjectItem>();
            foreach (Project project in this.dte.Solution.Projects)
            {
                if (project.ProjectItems == null)
                    continue;

                lstCsProjItems.AddRange(this.GetCsharpProjectItems(project.ProjectItems.OfType<ProjectItem>()));
            }

            foreach (var projectItem in lstCsProjItems)
            {
                if (projectItem?.FileCodeModel?.CodeElements == null)
                    continue;

                var namespaces = projectItem.FileCodeModel.CodeElements.OfType<CodeElement>().Where(m => m.Kind == vsCMElement.vsCMElementNamespace).OfType<CodeNamespace>();
                foreach (var cn in namespaces)
                {
                    foreach (CodeElement codeElement in cn.Members)
                    {
                        if (codeElement.FullName == fullName && (codeElement.Kind == vsCMElement.vsCMElementClass || codeElement.Kind == vsCMElement.vsCMElementEnum))
                        {
                            return codeElement;
                        }
                    }
                }
            }

            return null;
        }

        private void ProcessEnum(CodeEnum member)
        {
            //throw new NotImplementedException();
        }

        private void WriteClass(CsClass csClass)
        {
            var rez = $"{csClass.Namespace}\r\npublic class {csClass.Name} \r\n{{\r\n";
            foreach (var item in csClass.Properties)
            {
                //var mappedType = MapToTypeScriptType(item.Type);
                rez += $"\tpublic {item.Type}{(item.IsNullable ? "?" : string.Empty)} {item.Name} {{ get;set; }}\r\n";
            }

            rez += "}\r\n";

            Console.WriteLine(rez);
        }
    }
}
