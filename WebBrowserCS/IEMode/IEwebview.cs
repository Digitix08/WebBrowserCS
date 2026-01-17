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
    public partial class IEwebview : UserControl
    {
        WebBrowserEx webBrowser1;
        WebBrowserCS TabbedWindow;
        IEExternalScript err;
        IGNetworkHandler igNet = new IGNetworkHandler();
        UserControl errCtrl = null;
        TableLayoutPanel table = new TableLayoutPanel();
        Button ScrErrorClose = new Button {Text = "Close Log" };
        SplitContainer split = new SplitContainer();
        Label lb = new Label {Text = "Log viewer" };
        string home = Properties.Settings.Default.HomePage;
        string defaultsearch;
        bool newtab = true;
        bool useExtJS = false;
        int charlimit = 30;
        public IEwebview(string url, WebBrowserCS parent)
        {
            err = new IEExternalScript();
            TabbedWindow = parent;
            errCtrl = err.createElement(this, TabbedWindow);
            InitializeComponent();
            InitIEWebview();
            if (igNet.Check_mode(home) != "false")
                home = igNet.Check_mode(home);
            tableLayoutPanel1.Controls.Add(webBrowser1, 0, 1);
            tableLayoutPanel1.SetColumnSpan(webBrowser1, 10);
            if (igNet.Check_mode(url) != "false")
                webBrowser1.Navigate(igNet.Check_mode(url));
            else webBrowser1.Navigate(url);
            defaultsearch = System.Convert.ToString(Properties.Settings.Default.DefaultSearch);
            err.sendData(this, webBrowser1.ScriptErrorsSuppressed);
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

        public void SetCLogState(bool state)
        {
            useExtJS = state;
        }

        public void SetJSErrState(bool state)
        {
            webBrowser1.ScriptErrorsSuppressed = state;
        }

        void AxBrowser_NewWindow(string URL, int Flags, string TargetFrameName, ref object PostData, string Headers, ref bool Processed)
        {
            Processed = true;
            IEWindow browser = new IEWindow();
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
            table.Controls.Add(errCtrl, 0, 1);
            table.Controls.Add(lb, 1, 0);
            table.SetColumnSpan(errCtrl, 2);
            split.Dock = table.Dock = errCtrl.Dock = DockStyle.Fill;
            ScrErrorClose.Click += ScrErrClose_Click;
            split.Panel2.Controls.Add(table);
            Setcolor();
        }

        private void UriChanged(string text = null)
        {
            string url = "New Page";
            string title = url;
            if (webBrowser1.Url != null)
            {
                url = String.Concat(webBrowser1.Url.ToString());
                title = url;
                if (Convert.ToString(webBrowser1.Url).IndexOf("\\") == -1)
                    if (webBrowser1.Document.Title.Length > 0) title = webBrowser1.Document.Title;
            }
            if (text != null) title = text;
            CurrentUrl.Text = url;
            if (!GoToUrl.Focused)
            {
                GoToUrl.Text = url;
            }
            if (title.Length > charlimit) title = title.Substring(0, charlimit) + "...";
            TabPage MAIN = (TabPage)this.Parent;
            if (MAIN is TabPage)
            {
                MAIN.Text = title;
            }
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

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (toolStripProgressBar1.Style != ProgressBarStyle.Blocks)
            {
                toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                toolStripProgressBar1.Value = 100;
            }
            string title = String.Concat(webBrowser1.Url.ToString());
            int oldstep = toolStripProgressBar1.Step;
            int step = oldstep;
            step = toolStripProgressBar1.Maximum - toolStripProgressBar1.Value;
            toolStripProgressBar1.Step = step;
            toolStripProgressBar1.PerformStep();
            toolStripProgressBar1.Step = oldstep;
            status.Text = "Done";
            UriChanged();
            Reload.Image = Properties.Resources.arrow_reload;
            Reload.Click += Reload_Click;
        }

        private void WebBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            UriChanged();
            if (!webBrowser1.CanGoBack) { Back.Image = Properties.Resources.arrow_back_disabled; Back.Enabled = false; }
            else { Back.Image = Properties.Resources.arrow_back; Back.Enabled = true; }
            if (!webBrowser1.CanGoForward) { Forward.Image = Properties.Resources.arrow_forward_disabled; Forward.Enabled = false; }
            else { Forward.Image = Properties.Resources.arrow_forward; Forward.Enabled = true; }
            status.Text = "Loading Scripts";
            if(useExtJS)
            try
            {
                HtmlElement head = webBrowser1.Document.GetElementsByTagName("head")[0];
                HtmlElement scriptEl = webBrowser1.Document.CreateElement("script");
                ((IHTMLScriptElement)scriptEl.DomElement).src = AppDomain.CurrentDomain.BaseDirectory + "func190520251642343Mon.js";
                head.InsertAdjacentElement(HtmlElementInsertionOrientation.AfterBegin, scriptEl);
                err.log("FOR DEBUG:" + head.InnerHtml);
            }
            catch (System.Runtime.InteropServices.COMException) { err.error("Cannot debug the page " + CurrentUrl.Text); }
            catch (System.NullReferenceException) { }
        }

        private void WebBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar1.Value = 50;
            if (igNet.Check_mode(e.Url.ToString()) != "false")
                webBrowser1.Navigate(igNet.Check_mode(e.Url.ToString()));
            status.Text = "Searching for host...";
            UriChanged();
            Reload.Image = Properties.Resources.cancel;
            Reload.Click += Cancel_Click;
        }

        private void WebBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            var url = webBrowser1.Document.ActiveElement.GetAttribute("href");
            if (newtab)
            {
                e.Cancel = true;
                IEWindow browser = new IEWindow();
                browser.Show();
                browser.webBrowser1.Navigate(url);
            }
        }

        private void WebBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (e.CurrentProgress > 0 && e.MaximumProgress > 0 && e.CurrentProgress <= e.MaximumProgress)
            {
                long progress = 100 / (e.MaximumProgress / e.CurrentProgress);
                if (progress >= 1) toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                toolStripProgressBar1.Value = System.Convert.ToInt32(progress);
                if (status.Text != "Loading Scripts")
                    status.Text = "Downloading...";
            }
        }

        private async void CurrentUrl_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(CurrentUrl.Text);
            CurrentUrl.Text = "Copied to clipboard";
            await Task.Delay(500);
            CurrentUrl.Text = System.Convert.ToString(webBrowser1.Url);
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
