using CefSharp;
using CefSharp.Enums;
using CefSharp.Structs;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
//using CefSharp.MinimalExample.WinForms.Controls;
namespace WebBrowserCS
{
    public class DisplayHandler : IDisplayHandler, INotifyPropertyChanged
    {
        private Control parent;
        private Form fullScreenForm;

        bool isfullscreen = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void OnProgressChange(int value);
        public event OnProgressChange ProgressChanged;
        public delegate void OnFaviconChange(string url);
        public event OnFaviconChange FaviconChanged;

        protected void OnPropertyChanged(string propertyName)
        {
        }



        public void OnAddressChanged(IWebBrowser chromiumWebBrowser, AddressChangedEventArgs addressChangedArgs)
        {
        }

        public bool OnAutoResize(IWebBrowser chromiumWebBrowser, IBrowser browser, CefSharp.Structs.Size newSize)
        {
            return false;
        }

        public bool OnConsoleMessage(IWebBrowser chromiumWebBrowser, ConsoleMessageEventArgs consoleMessageArgs)
        {
            return false;
        }

        public bool OnCursorChange(IWebBrowser chromiumWebBrowser, IBrowser browser, IntPtr cursor, CursorType type, CursorInfo customCursorInfo)
        {
            return false;
        }

        public void OnFaviconUrlChange(IWebBrowser chromiumWebBrowser, IBrowser browser, IList<string> urls)
        {
            string FavUrl = urls[0];
            foreach(string url in urls)
            {
                if (url.Contains("favicon.ico")) FavUrl = url;
            }
            FaviconChanged?.Invoke(FavUrl);
        }

        public void OnFullscreenModeChange(IWebBrowser chromiumWebBrowser, IBrowser browser, bool fullscreen)
        {
            var chrWebBrowser = (ChromiumWebBrowser)chromiumWebBrowser;
            chrWebBrowser.InvokeOnUiThreadIfRequired(() =>
            {
                if (fullscreen)
                {
                    parent = chrWebBrowser.Parent;
                    parent.Controls.Remove(chrWebBrowser);
                    fullScreenForm = new Form
                    {
                        FormBorderStyle = FormBorderStyle.None,
                        WindowState = FormWindowState.Maximized
                    };
                    fullScreenForm.Controls.Add(chrWebBrowser);
                    fullScreenForm.ShowDialog(parent.FindForm());
                    isfullscreen = true;
                }
                else
                {
                    fullScreenForm.Controls.Remove(chrWebBrowser);
                    parent.Controls.Add(chrWebBrowser);
                    fullScreenForm.Close();
                    fullScreenForm.Dispose();
                    fullScreenForm = null;
                    isfullscreen = false;
                }
            });
        }

        public void OnLoadingProgressChange(IWebBrowser chromiumWebBrowser, IBrowser browser, double progress)
        {
            ProgressChanged?.Invoke((int)(progress * 100));
        }

        public void OnStatusMessage(IWebBrowser chromiumWebBrowser, StatusMessageEventArgs statusMessageArgs)
        {
        }

        public void OnTitleChanged(IWebBrowser chromiumWebBrowser, TitleChangedEventArgs titleChangedArgs)
        {
        }

        public bool OnTooltipChanged(IWebBrowser chromiumWebBrowser, ref string text)
        {
            return false;
        }
    }
}