using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    public partial class IEWindow : Form
    {
        string home = Properties.Settings.Default.HomePage;
        bool newtab = true;
        bool useExtJS = false;
        string defaultsearch;
        IEExternalScript err;
        IGNetworkHandler igNet = new IGNetworkHandler();
        Form errForm = null;
        public IEWindow()
        {
            err = new IEExternalScript();
            errForm = err.createStandaloneElement(this); 
            InitializeComponent();
            /*Search1.Text = Properties.Settings.Default.Search1;
            Search2.Text = Properties.Settings.Default.Search2;
            Search3.Text = Properties.Settings.Default.Search3;
            Search4.Text = Properties.Settings.Default.Search4;
            Search5.Text = Properties.Settings.Default.Search5;*/
            defaultsearch = System.Convert.ToString(Properties.Settings.Default.DefaultSearch);
        }

        public void SetCLogState(bool state)
        {
            useExtJS = state;
        }

        public void SetJSErrState(bool state)
        {
            webBrowser1.ScriptErrorsSuppressed = state;
        }

        public void OpenIE()
        {
            newtab = false;
            webBrowser1.Navigate(home, "_blank");
            newtab = true;
            this.Close();
        }

        private void Setcolor()
        {
            this.BackColor = Properties.Settings.Default.Windowbgcolor;
            this.ForeColor = Properties.Settings.Default.Textcolor;
            fileToolStripMenuItem.BackColor = Properties.Settings.Default.Elembgcolor;
            ColorSet.SetColorIncludingChildren(this, typeof(Button), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(TextBox), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(StatusStrip), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(MenuStrip), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(ToolStrip), default, default);
        }
        private void Browser_Load(object sender, EventArgs e)
        {
            Setcolor();
            if (igNet.Check_mode(home) != "false")
                home = igNet.Check_mode(home);
            webBrowser1.Navigate(home);
            webBrowser1.ObjectForScripting = err;
            if (defaultsearch == "4") defaultsearch = Properties.Settings.Default.Search1;
            else if (defaultsearch == "3") defaultsearch = Properties.Settings.Default.Search2;
            else if (defaultsearch == "2") defaultsearch = Properties.Settings.Default.Search3;
            else if (defaultsearch == "1") defaultsearch = Properties.Settings.Default.Search4;
            else if (defaultsearch == "0") defaultsearch = Properties.Settings.Default.Search5;
            SHDocVw.WebBrowser_V1 axBrowser = (SHDocVw.WebBrowser_V1)webBrowser1.ActiveXInstance;
            axBrowser.NewWindow += AxBrowser_NewWindow;
        }

        void AxBrowser_NewWindow(string URL, int Flags, string TargetFrameName, ref object PostData, string Headers, ref bool Processed)
        {
            if (newtab)
            {
                Processed = true;
                IEWindow browser = new IEWindow();
                browser.Show();
                browser.webBrowser1.Navigate(URL);
            }
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
            TabPage MAIN = (TabPage)this.Parent;
            if (MAIN is TabPage)
            {
                MAIN.Text = title;
            }
        }

        private void GoTo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(GoToUrl.Text))
            {
                webBrowser1.Navigate(GoToUrl.Text);
            }
        }

        private void Forward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void PageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                Title = "Open file...",
                InitialDirectory = "%homedir%",
                Filter = "All files (*.*)|*.*|Html files (*.htm, *.html)|*.htm; *.html",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                webBrowser1.Navigate(fd.FileName);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void NewIEInstanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newtab = false;
            webBrowser1.Navigate(home, "_blank");
            newtab = true;
        }

        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEWindow browser = new IEWindow();
            browser.Show();
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options setting = new Options();
            setting.Show();
        }

        private void GoHome_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(home);
        }

        private void testChromiumVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChromeBrowser chrome = new ChromeBrowser();
            chrome.Show();
        }

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
            if (useExtJS)
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

        private void TabbedVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowserCS tabbed = new WebBrowserCS();
            tabbed.Show();
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileEdit fileEdit;
            OpenFileDialog fd = new OpenFileDialog
            {
                Title = "Open file...",
                InitialDirectory = "%homedir%",
                Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt|Html files (*.htm, *.html)|*.htm; *.html",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                fileEdit = new FileEdit() { fileDir = fd.FileName };
                fileEdit.Show();
            }
        }

        private void FileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FileEdit newfile = new FileEdit();
            newfile.Show();
        }

        private void MdiparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIParent1 mdi = new MDIParent1();
            mdi.Show();
        }

        private void CustomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options settings = new Options();
            settings.tabControl1.SelectTab("Customization");
            settings.Show();
        }

        private void GoToUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter)) webBrowser1.Navigate(GoToUrl.Text);
            if (e.KeyData == Keys.X && e.Modifiers == Keys.Control) (sender as TextBox).Cut();
            if (e.KeyData == Keys.C && e.Modifiers == Keys.Control) (sender as TextBox).Copy();
            if (e.KeyData == Keys.V && e.Modifiers == Keys.Control) (sender as TextBox).Paste();
        }

        /*private void Search1_Click(object sender, EventArgs e) => webBrowser1.Navigate(Search1.Text + GoToUrl.Text);
        private void Search2_Click(object sender, EventArgs e) => webBrowser1.Navigate(Search2.Text + GoToUrl.Text);
        private void Search3_Click(object sender, EventArgs e) => webBrowser1.Navigate(Search3.Text + GoToUrl.Text);
        private void Search4_Click(object sender, EventArgs e) => webBrowser1.Navigate(Search4.Text + GoToUrl.Text);
        private void Search5_Click(object sender, EventArgs e) => webBrowser1.Navigate(Search5.Text + GoToUrl.Text);*/

        private void scriptErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            errForm.Show();
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
