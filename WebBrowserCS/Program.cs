using System;
using System.Collections.Generic;
using System.Linq;
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
            StartArgs = CMDArgs;
            if(StartArgs.Length == 0)
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WebBrowserCS());
        }
    }
}
