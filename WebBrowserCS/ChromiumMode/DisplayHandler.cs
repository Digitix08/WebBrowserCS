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

        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void OnProgressChange(int value);
        public event OnProgressChange ProgressChanged;

        private object _favIcon;

        /// <summary>
        /// For binding to System.Windows.Window.Icon.
        /// </summary>
        public object FavIcon
        {
            get { return _favIcon; }
            set { _favIcon = value; OnPropertyChanged("FavIcon"); }
        }

        /*private BitmapDecoder _decoder;
        private BitmapDecoder Decoder
        {
            get => _decoder;
            set
            {
                if (_decoder != null) _decoder.DownloadCompleted -= decoderDownloadCompleted;
                _decoder = value;
                if (_decoder != null) _decoder.DownloadCompleted += decoderDownloadCompleted;
            }
        }

        private void decoderDownloadCompleted(object sender, EventArgs e)
        {
            FavIcon = Decoder.Frames.OrderBy(f => f.Width).FirstOrDefault();
            Decoder = null;
        }*/

        protected void OnPropertyChanged(string propertyName)
        {
            /*   if (!Application.Current.Dispatcher.CheckAccess())
                   Application.Current.Dispatcher.Invoke(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
               else PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));*/
            MessageBox.Show(propertyName);
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
            var baseUrl = new Uri(browser.MainFrame.Url).GetLeftPart(UriPartial.Authority);
            MessageBox.Show(string.Join("\n", urls));
            /*Application.Current.Dispatcher.Invoke(() =>
            {
                Decoder = BitmapDecoder.Create(new Uri(baseUrl + "/favicon.ico"), BitmapCreateOptions.None, BitmapCacheOption.OnDemand);
            });*/
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
                }
                else
                {
                    fullScreenForm.Controls.Remove(chrWebBrowser);
                    parent.Controls.Add(chrWebBrowser);
                    fullScreenForm.Close();
                    fullScreenForm.Dispose();
                    fullScreenForm = null;
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