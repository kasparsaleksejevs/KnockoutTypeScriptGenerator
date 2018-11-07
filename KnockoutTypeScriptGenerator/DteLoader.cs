using EnvDTE;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace KnockoutTypeScriptGenerator
{
    /// <summary>
    /// DTE loading helper class for the cases when it is used without VS Extension.
    /// </summary>
    public class DteLoader
    {
        [DllImport("ole32.dll")]
        private static extern void CreateBindCtx(int reserved, out IBindCtx ppbc);

        [DllImport("ole32.dll")]
        private static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

        /// <summary>
        /// Gets the DTE by the solution name.
        /// </summary>
        /// <param name="solutionName">Name of the solution.</param>
        /// <returns></returns>
        public static DTE GetDteBySolutionName(string solutionName)
        {
            DTE dte = null;

            var vsInstances = GetVisualStudioInstances();
            foreach (var vsInstanceDte in vsInstances)
            {
                if (vsInstanceDte.Solution.FullName.Contains(solutionName))
                {
                    dte = vsInstanceDte;
                    break;
                }
            }

            return dte;
        }

        /// <summary>
        /// Gets all running DTE instances.
        /// https://stackoverflow.com/questions/14205933/how-do-i-get-the-dte-for-running-visual-studio-instance/14205934#14205934
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<DTE> GetVisualStudioInstances()
        {
            int retVal = GetRunningObjectTable(0, out IRunningObjectTable rot);
            if (retVal == 0)
            {
                rot.EnumRunning(out IEnumMoniker enumMoniker);

                IntPtr fetched = IntPtr.Zero;
                IMoniker[] moniker = new IMoniker[1];
                while (enumMoniker.Next(1, moniker, fetched) == 0)
                {
                    CreateBindCtx(0, out IBindCtx bindCtx);
                    moniker[0].GetDisplayName(bindCtx, null, out string displayName);

                    bool isVisualStudio = displayName.StartsWith("!VisualStudio");
                    if (isVisualStudio)
                    {
                        rot.GetObject(moniker[0], out object obj);
                        var dte = obj as DTE;
                        yield return dte;
                    }
                }
            }
        }
    }
}
