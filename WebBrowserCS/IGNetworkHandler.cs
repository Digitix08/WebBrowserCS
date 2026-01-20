using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowserCS
{
    public class IGNetworkHandler
    {
        string defaultPath = "file:///" + AppDomain.CurrentDomain.BaseDirectory + "html/";
        string defaultBrowserpath = "igbrowser://";
        string defaultOLPage = "http://digitix08.github.io/webbrowser/home.html";

        public string Check_mode(string url)
        {
            if (url.StartsWith("http://")) url = url.Substring(7);
            else if (url.StartsWith("https://")) url = url.Substring(8);

            if (url.ToLower().StartsWith(defaultBrowserpath))
            {
                url = url.Substring(defaultBrowserpath.Length);
                url = url.Substring(0, url.Length - 1);
                switch (url){
                    case "offlineHome": return defaultPath + "home.html";
                    default: return checkOnlinePage();
                }
            }
            return "false";
        }

        private string checkOnlinePage()
        {
            if(RemoteFileExists(defaultOLPage))return defaultOLPage;
            else return defaultPath + "home.html";
        }

///
/// Checks the file exists or not.
///
/// The URL of the remote file.
/// True : If the file exits, False if file not exists
        private bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.Redirect);
            }
            catch
            {
                //Any exception will return false.
                return false;
            }
    }
}
}
