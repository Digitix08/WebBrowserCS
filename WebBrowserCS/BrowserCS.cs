using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    class BrowserCS
    {
        static readonly WebBrowserCS webBrowserCS = new WebBrowserCS();
        internal static void BrowserCS_show()
        {
            webBrowserCS.Show();
        }
        internal static void NewWindow()
        {
            
        }
        internal static void NewIETab(string url)
        {
            webBrowserCS.NewIETab(url);
        }
        internal static void NewChromiumTab(string url)
        {
            webBrowserCS.NewChromiumTab(url);
        }
    }
    
    public class Logger
    {
        
    }
}
