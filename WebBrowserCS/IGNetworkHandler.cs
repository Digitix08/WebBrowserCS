using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowserCS
{
    public class IGNetworkHandler
    {
        string defaultPath = "file:///" + AppDomain.CurrentDomain.BaseDirectory + "html/";
        string defaultBrowserpath = "igbrowser://";

        public string Check_mode(string url)
        {
            if (url.StartsWith("http://")) url = url.Substring(7);
            else if (url.StartsWith("https://")) url = url.Substring(8);

            if (url.ToLower().StartsWith(defaultBrowserpath))
            {
                url = url.Substring(defaultBrowserpath.Length);
                url = url.Substring(0, url.Length - 1);
                switch (url){
                    default: return defaultPath + "home.html";
                }
            }
            return "false";
        }
    }
}
