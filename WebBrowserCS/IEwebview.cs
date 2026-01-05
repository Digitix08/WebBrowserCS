using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using mshtml;

namespace WebBrowserCS
{
    public class WebBrowserEx : WebBrowser
    {
        class WebBrowserSiteEx : WebBrowserSite, NativeMethods.IOleCommandTarget
        {
            public WebBrowserSiteEx(WebBrowser browser) : base(browser)
            {
            }

            public int QueryStatus(IntPtr pguidCmdGroup, uint cCmds, NativeMethods.OLECMD[] prgCmds, ref NativeMethods.OLECMDTEXT CmdText)
            {
                return NativeMethods.OLECMDERR_E_UNKNOWNGROUP;
            }

            public int Exec(IntPtr pguidCmdGroup, uint nCmdId, uint nCmdExecOpt, IntPtr pvaIn, IntPtr pvaOut)
            {
                if (pguidCmdGroup != IntPtr.Zero)
                {
                    Guid guid = (Guid)Marshal.PtrToStructure(pguidCmdGroup, typeof(Guid));
                    if (guid == NativeMethods.CGID_DocHostCommandHandler)
                    {
                        if (nCmdId == NativeMethods.OLECMDID_SHOWSCRIPTERROR)
                        {
                            // for dom: dynamic document = Marshal.GetObjectForNativeVariant(nCmdId);

                            // continue running scripts
                            if (pvaOut != IntPtr.Zero)
                                Marshal.GetNativeVariantForObject(true, pvaOut);

                            //return NativeMethods.S_OK;
                        }
                    }
                }
                return NativeMethods.OLECMDERR_E_UNKNOWNGROUP;
            }
        }

        protected override WebBrowserSiteBase CreateWebBrowserSiteBase()
        {
            return new WebBrowserSiteEx(this);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }

    static class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct OLECMD
        {
            public uint cmdID;
            public uint cmdf;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OLECMDTEXT
        {
            public UInt32 cmdtextf;
            public UInt32 cwActual;
            public UInt32 cwBuf;
            public char rgwz;
        }

        public const int OLECMDERR_E_UNKNOWNGROUP = unchecked((int)0x80040102);
        public const int OLECMDID_SHOWSCRIPTERROR = 40;
        public static readonly Guid CGID_DocHostCommandHandler = new Guid("f38bc242-b950-11d1-8918-00c04fc2c836");
        public const int S_OK = 0;

        [ComImport, Guid("b722bccb-4e68-101b-a2bc-00aa00404770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleCommandTarget
        {
            [PreserveSig]
            int QueryStatus(
                IntPtr pguidCmdGroup,
                UInt32 cCmds,
                [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] OLECMD[] prgCmds,
                ref OLECMDTEXT CmdText);

            [PreserveSig]
            int Exec(
                IntPtr pguidCmdGroup,
                uint nCmdId,
                uint nCmdExecOpt,
                IntPtr pvaIn,
                IntPtr pvaOut);
        }
    }

    public partial class IEwebview : UserControl
    {
        WebBrowserEx webBrowser1;
        SCRErrorIEControl err;
        TableLayoutPanel table = new TableLayoutPanel();
        Button ScrErrorClose = new Button {Text = "Close Log" };
        SplitContainer split = new SplitContainer();
        Label lb = new Label {Text = "Log viewer" };
        readonly string home = Properties.Settings.Default.HomePage;
        string defaultsearch;
        bool newtab = true;
        int charlimit = 30;
        public IEwebview(string url)
        {
            err = new SCRErrorIEControl(this);
            InitializeComponent();
            InitIEWebview();
            tableLayoutPanel1.Controls.Add(webBrowser1, 0, 1);
            tableLayoutPanel1.SetColumnSpan(webBrowser1, 10);
            webBrowser1.Navigate(url);
            defaultsearch = System.Convert.ToString(Properties.Settings.Default.DefaultSearch);
            err.sendData(webBrowser1.ScriptErrorsSuppressed);
        }
        private void InitIEWebview()
        {
            webBrowser1 = new WebBrowserEx();
            webBrowser1.Dock = DockStyle.Fill;
            webBrowser1.ProgressChanged += WebBrowser1_ProgressChanged;
            webBrowser1.NewWindow += WebBrowser1_NewWindow;
            webBrowser1.Navigated += WebBrowser1_Navigated;
            webBrowser1.Navigating += WebBrowser1_Navigating;
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            webBrowser1.ObjectForScripting = err;
        }

        public void SetJSErrState(bool state)
        {
            webBrowser1.ScriptErrorsSuppressed = state;
        }

        void AxBrowser_NewWindow(string URL, int Flags, string TargetFrameName, ref object PostData, string Headers, ref bool Processed)
        {
            Processed = true;
            Browser browser = new Browser();
            browser.Show();
            browser.webBrowser1.Navigate(URL);
        }

        private void Setcolor()
        {
            this.BackColor = Properties.Settings.Default.Windowbgcolor;
            this.ForeColor = Properties.Settings.Default.Textcolor;
            ColorSet.SetColorIncludingChildren(this, typeof(Panel), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(StatusStrip), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(TableLayoutPanel), default, default);
        }

        private void IEwebview_Load(object sender, EventArgs e)
        {
            webBrowser1.ObjectForScripting = err;
            if (defaultsearch == "4") defaultsearch = Properties.Settings.Default.Search1;
            else if (defaultsearch == "3") defaultsearch = Properties.Settings.Default.Search2;
            else if (defaultsearch == "2") defaultsearch = Properties.Settings.Default.Search3;
            else if (defaultsearch == "1") defaultsearch = Properties.Settings.Default.Search4;
            else if (defaultsearch == "0") defaultsearch = Properties.Settings.Default.Search5;
            SHDocVw.WebBrowser_V1 axBrowser = (SHDocVw.WebBrowser_V1)webBrowser1.ActiveXInstance;
            axBrowser.NewWindow += AxBrowser_NewWindow;
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            lb.Font = new Font("Arial", 15, FontStyle.Bold, GraphicsUnit.Point);
            lb.Width = 500;
            table.Controls.Add(ScrErrorClose, 0, 0);
            table.Controls.Add(err, 0, 1);
            table.Controls.Add(lb, 1, 0);
            table.SetColumnSpan(err, 2);
            split.Dock = table.Dock = err.Dock = DockStyle.Fill;
            ScrErrorClose.Click += ScrErrClose_Click;
            split.Panel2.Controls.Add(table);
            Setcolor();
        }

        private void UriChanged(string text)
        {
            if (text.Length > 0)
            {
                CurrentUrl.Text = text;
                if (!GoToUrl.Focused)
                {
                    GoToUrl.Text = text;
                }
            }
        }

        private void WebBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (e.CurrentProgress > 0 && e.MaximumProgress > 0 && e.CurrentProgress <= e.MaximumProgress)
            {
                long progress = 100 / (e.MaximumProgress / e.CurrentProgress);
                if (progress >= 1) toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                toolStripProgressBar1.Value = System.Convert.ToInt32(progress);
                UriChanged(Convert.ToString(webBrowser1.Url));
                if(status.Text != "Loading Scripts")
                status.Text = "Downloading...";
            }
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (toolStripProgressBar1.Style != ProgressBarStyle.Blocks)
            {
                toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                toolStripProgressBar1.Value = 100;
            }
            int oldstep = toolStripProgressBar1.Step;
            int step = oldstep;
            step = toolStripProgressBar1.Maximum - toolStripProgressBar1.Value;
            toolStripProgressBar1.Step = step;
            toolStripProgressBar1.PerformStep();
            toolStripProgressBar1.Step = oldstep;
            status.Text = "Done";
        }
        private void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {     
            e.Handled = true;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(home);
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void Forward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void GoTo_Click(object sender, EventArgs e) => webNavigate(GoToUrl.Text);

        private void Search_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(defaultsearch + GoToUrl.Text);
        }

        private void WebBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            var url = webBrowser1.Document.ActiveElement.GetAttribute("href");
            if (newtab)
            {
                e.Cancel = true;
                Browser browser = new Browser();
                browser.Show();
                browser.webBrowser1.Navigate(url);
            }
        }

        private async void CurrentUrl_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(CurrentUrl.Text);
            CurrentUrl.Text = "Copied to clipboard";
            await Task.Delay(500);
            CurrentUrl.Text = System.Convert.ToString(webBrowser1.Url);
        }

        private void WebBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            string title = String.Concat(webBrowser1.Url.ToString());
            if (Convert.ToString(webBrowser1.Url).IndexOf("\\") == -1)
                if (webBrowser1.Document.Title.Length > 0) title = webBrowser1.Document.Title;
            UriChanged(Convert.ToString(webBrowser1.Url));
            if (title.Length > charlimit) title = title.Substring(0, charlimit) + "...";
            TabPage MAIN = (TabPage)this.Parent;
            if (MAIN is TabPage)
            {
                MAIN.Text = title;
            }
            if (!webBrowser1.CanGoBack) { Back.Image = Properties.Resources.arrow_back_disabled; Back.Enabled = false; }
            else { Back.Image = Properties.Resources.arrow_back; Back.Enabled = true; }
            if (!webBrowser1.CanGoForward) { Forward.Image = Properties.Resources.arrow_forward_disabled; Forward.Enabled = false; }
            else { Forward.Image = Properties.Resources.arrow_forward; Forward.Enabled = true; }
            status.Text = "Loading Scripts";
            try
            {
                HtmlElement head = webBrowser1.Document.GetElementsByTagName("head")[0];
                HtmlElement scriptEl = webBrowser1.Document.CreateElement("script");
                ((IHTMLScriptElement)scriptEl.DomElement).src = AppDomain.CurrentDomain.BaseDirectory + "func190520251642343Mon.js";
                head.InsertAdjacentElement(HtmlElementInsertionOrientation.AfterBegin, scriptEl);
                err.GetText("FOR DEBUG:", head.InnerHtml);
            }
            catch (System.Runtime.InteropServices.COMException) { err.error("Cannot debug the page " + CurrentUrl.Text); }
            catch (System.NullReferenceException) { }
        }

        private void GoToUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                webNavigate(GoToUrl.Text);
            }
        }

        private void webNavigate(string text)
        {
            if (text.Length > 0)
            {
                webBrowser1.Navigate(GoToUrl.Text);
            }
        }

        private void Picture_invert(object sender, MouseEventArgs e)
        {
            Bitmap pic = new Bitmap(((PictureBox)sender).Image);
            for (int y = 0; (y <= (pic.Height - 1)); y++)
            {
                for (int x = 0; (x <= (pic.Width - 1)); x++)
                {
                    Color inv = pic.GetPixel(x, y);
                    inv = Color.FromArgb(inv.A, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    pic.SetPixel(x, y, inv);
                }
            }
            ((PictureBox)sender).Image = pic;
        }

        private void ScrErr_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Remove(webBrowser1);
            tableLayoutPanel1.Controls.Add(split, 0, 1);
            split.Panel1.Controls.Add(webBrowser1);
            tableLayoutPanel1.SetColumnSpan(split, 10);
        }

        private void ScrErrClose_Click(object sender, EventArgs e)
        {
            split.Panel1.Controls.Remove(webBrowser1);
            tableLayoutPanel1.Controls.Remove(split);
            tableLayoutPanel1.Controls.Add(webBrowser1, 0, 1);
        }

        private void WebBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar1.Value = 50;
            status.Text = "Searching for host...";
            Reload.Image = Properties.Resources.cancel;
            Reload.Click += Cancel_Click;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
            Reload.Image = Properties.Resources.arrow_reload;
            Reload.Click += Reload_Click;

            Bitmap pic = new Bitmap(((PictureBox)sender).Image);
            for (int y = 0; (y <= (pic.Height - 1)); y++)
            {
                for (int x = 0; (x <= (pic.Width - 1)); x++)
                {
                    Color inv = pic.GetPixel(x, y);
                    inv = Color.FromArgb(inv.A, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    pic.SetPixel(x, y, inv);
                }
            }
            ((PictureBox)sender).Image = pic;
        }
    }
}
