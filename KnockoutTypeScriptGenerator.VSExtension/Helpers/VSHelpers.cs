using EnvDTE;
using System;

namespace KnockoutTypeScriptGenerator.VSExtension.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>https://github.com/madskristensen/TypeScriptDefinitionGenerator/</remarks>
    public static class VSHelpers
    {
        public static bool IsKind(this Project project, params string[] kindGuids)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            foreach (var guid in kindGuids)
            {
                if (project.Kind.Equals(guid, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>https://github.com/madskristensen/TypeScriptDefinitionGenerator/</remarks>
    public static class ProjectTypes
    {
        public const string ASPNET_5 = "{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}";
        public const string DOTNET_Core = "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}";
        public const string WEBSITE_PROJECT = "{E24C65DC-7377-472B-9ABA-BC803B73C61A}";
    }
}
