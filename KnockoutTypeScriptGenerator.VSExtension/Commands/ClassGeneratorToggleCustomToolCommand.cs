using EnvDTE;
using EnvDTE80;
using KnockoutTypeScriptGenerator.VSExtension.Generators;
using KnockoutTypeScriptGenerator.VSExtension.Helpers;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.IO;

namespace KnockoutTypeScriptGenerator.VSExtension.Commands
{
    internal sealed class ClassGeneratorToggleCustomToolCommand
    {
        private readonly Microsoft.VisualStudio.Shell.Package _package;
        private ProjectItem _item;
        private DTE2 _dte;

        private ClassGeneratorToggleCustomToolCommand(Microsoft.VisualStudio.Shell.Package package, OleMenuCommandService commandService, DTE2 dte)
        {
            _package = package;
            _dte = dte;

            var cmdId = new CommandID(PackageGuids.guidDtsPackageCmdSet, PackageIds.ToggleCustomToolId);
            var cmd = new OleMenuCommand(Execute, cmdId);
            cmd.BeforeQueryStatus += BeforeQueryStatus;
            commandService.AddCommand(cmd);
        }

        public static ClassGeneratorToggleCustomToolCommand Instance
        {
            get;
            private set;
        }

        private IServiceProvider ServiceProvider
        {
            get { return _package; }
        }

        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage package)
        {
            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            var dte = await package.GetServiceAsync(typeof(DTE)) as DTE2;

            Instance = new ClassGeneratorToggleCustomToolCommand(package, commandService, dte);
        }

        private void BeforeQueryStatus(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var button = (OleMenuCommand)sender;
            //button.Visible = button.Enabled = false;
            button.Visible = true;
            button.Enabled = false;

            if (_dte.SelectedItems.Count != 1)
                return;

            _item = _dte.SelectedItems?.Item(1)?.ProjectItem;
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            // .NET Core and Website projects
            if (_item.ContainingProject.IsKind(ProjectTypes.DOTNET_Core, ProjectTypes.ASPNET_5, ProjectTypes.WEBSITE_PROJECT))
            {
                var generatedFileName = TypeScriptClassGenerator.GetGeneratedFileName(_item.FileNames[1]);
                var fileExists = File.Exists(generatedFileName);
                if (fileExists)
                {
                    var dtsItem = VSHelpers.GetProjectItem(generatedFileName);
                    dtsItem?.Delete();
                    File.Delete(generatedFileName);
                }
                else
                {
                    GenerationService.CreateDtsFile(_item);
                }
            }
            else
            {
                // Legacy .NET projects
                bool synOn = _item.Properties.Item("CustomTool").Value.ToString() == TypeScriptClassGenerator.Name;
                if (synOn)
                    _item.Properties.Item("CustomTool").Value = "";
                else
                    _item.Properties.Item("CustomTool").Value = TypeScriptClassGenerator.Name;
            }
        }
    }
}
