using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    [ComVisible(true)]
    public class IEExternalScript
    {
        SCRErrorIE ScrError = new SCRErrorIE();
        SCRErrorIEControl ScrErrorCtrl = new SCRErrorIEControl();
        bool IsInTab = false;
        IEwebview IETab;
        IEWindow IEWindow;
        WebBrowserCS TabbedWindow;

        public UserControl createElement(IEwebview sender, WebBrowserCS parent)
        {
            if (sender is IEwebview)
            {
                IsInTab = true;
                ScrError.Dispose();
                IETab = sender;
                TabbedWindow = parent;
                return ScrErrorCtrl;
            }
            return null;
        }

        public Form createStandaloneElement(IEWindow sender)
        {
            if (sender is IEWindow)
            {
                ScrErrorCtrl.Dispose();
                IEWindow = sender;
                return ScrError;
            }
            return null;
        }

        public void log(string s)
        {
            if (IsInTab) ScrErrorCtrl.log(s);
            else ScrError.log(s);
        }

        public void info(string s)
        {
            if (IsInTab) ScrErrorCtrl.info(s);
            else ScrError.info(s);
        }
        public void warn(string s)
        {
            if (IsInTab) ScrErrorCtrl.warn(s);
            else ScrError.warn(s);
        }
        public void error(string s)
        {
            if (IsInTab) ScrErrorCtrl.error(s);
            else ScrError.error(s);
        }

        public string GetBrowserVer()
        {
            return "WebBrowserCS v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public string GetSearchEngines(string callerURL)
        {
            if (callerURL != null)
            {
                string searchEngines = "{\"SearchEngines\": [";
                int nr = 0;
                if (Properties.Settings.Default.Search1.Length > 0 && Properties.Settings.Default.Search1 != "undefined") { searchEngines += "\"" + Properties.Settings.Default.Search1 + "\""; nr++; }
                if (Properties.Settings.Default.Search2.Length > 0 && Properties.Settings.Default.Search2 != "undefined")
                {
                    if (nr > 0) searchEngines += ", ";
                    searchEngines += "\"" + Properties.Settings.Default.Search2 + "\""; nr++;
                }
                if (Properties.Settings.Default.Search3.Length > 0 && Properties.Settings.Default.Search3 != "undefined")
                {
                    if (nr > 0) searchEngines += ", ";
                    searchEngines += "\"" + Properties.Settings.Default.Search3 + "\""; nr++;
                }
                if (Properties.Settings.Default.Search4.Length > 0 && Properties.Settings.Default.Search4 != "undefined")
                {
                    if (nr > 0) searchEngines += ", ";
                    searchEngines += "\"" + Properties.Settings.Default.Search4 + "\""; nr++;
                }
                if (Properties.Settings.Default.Search5.Length > 0 && Properties.Settings.Default.Search5 != "undefined")
                {
                    if (nr > 0) searchEngines += ", ";
                    searchEngines += "\"" + Properties.Settings.Default.Search5 + "\""; nr++;
                }
                searchEngines += "] }";
                return searchEngines;
            }
            else return null;
        }

        public string GetAvailableTabs(string callerURL)
        {
            if (callerURL != null)
            {
                string AvailableTabs = "{\"AvailableTabs\": [";
                string[][] NewTabs = TabbedWindow.AvailTabs.ToArray();
                for (int i = 0; i < NewTabs.Length; i++) {
                    string name = NewTabs[i][0];
                    string command = NewTabs[i][1];
                    string CurrTab = "{\"name\":\"" + name + "\", \"command\":\"" + command + "\"";
                    if (NewTabs[i].Length > 2) {
                        string callsign = NewTabs[i][2];
                        CurrTab += ", \"tab_name\":\"" + callsign + "\"";
                    }
                    CurrTab += "}";
                    if (NewTabs.Length>1 && i < NewTabs.Length -1) CurrTab += ", ";
                    AvailableTabs += CurrTab;
                }
                AvailableTabs += "] }";
                return AvailableTabs;
            }
            else return null;
        }

        public void NewIETab(string callerURL, string URLToGo = null)
        {
            if (callerURL != null) TabbedWindow.NewTab(URLToGo, "IETab");
        }

        public void NewChromiumTab(string callerURL, string URLToGo = null)
        {
            if (callerURL != null) TabbedWindow.NewTab(URLToGo, "ChromiumTab");
        }

        public void NewUserTab(string callerURL, string tabID, string URLToGo = null)
        {
            var Tabs = TabbedWindow.newTabToolStripMenuItem;
            for(int i=0; i< Tabs.DropDownItems.Count; i++){
                ToolStripItem Tab = Tabs.DropDownItems[i];
                if (Tab.Tag!=null && !string.IsNullOrEmpty(Tab.Tag.ToString())){
                    string Tag = Tab.Tag.ToString();
                    if (Tag.EndsWith(tabID)){
                        TabbedWindow.ExternalLaunch(Tag, tabID, true, URLToGo);
                    }
                }
            } 
        }

        public void sendData(UserControl sender, bool JsDisable)
        {
            if (sender is UserControl)
            {
                if (IsInTab) ScrErrorCtrl.sendData(JsDisable);
            }
        }
    }
}
