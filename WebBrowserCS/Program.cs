using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    static class Program
    {
        public static string[] StartArgs;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] CMDArgs)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string folder = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "CefSharp"
                );

                string name = new AssemblyName(args.Name).Name + ".dll";
                string path = Path.Combine(folder, name);

                if (File.Exists(path))
                    return Assembly.LoadFrom(path);

                return null;
            };
            StartArgs = CMDArgs;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BrowserCS());
        }
    }
}
