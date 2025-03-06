using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    public partial class IEwebview : UserControl
    { 
        readonly string home = Properties.Settings.Default.HomePage;
        string defaultsearch;
        bool newtab = true;
        int charlimit = 30;
        public IEwebview(string url)
        {
            InitializeComponent();
            webBrowser1.Navigate(url);
            defaultsearch = System.Convert.ToString(Properties.Settings.Default.DefaultSearch);
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
        }

        private void IEwebview_Load(object sender, EventArgs e)
        {
            Setcolor();
            if (defaultsearch == "4") defaultsearch = Properties.Settings.Default.Search1;
            else if (defaultsearch == "3") defaultsearch = Properties.Settings.Default.Search2;
            else if (defaultsearch == "2") defaultsearch = Properties.Settings.Default.Search3;
            else if (defaultsearch == "1") defaultsearch = Properties.Settings.Default.Search4;
            else if (defaultsearch == "0") defaultsearch = Properties.Settings.Default.Search5;
            SHDocVw.WebBrowser_V1 axBrowser = (SHDocVw.WebBrowser_V1)webBrowser1.ActiveXInstance;
            axBrowser.NewWindow += AxBrowser_NewWindow;
        }

        private void WebBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (e.CurrentProgress > 0 && e.MaximumProgress > 0 && e.CurrentProgress <= e.MaximumProgress)
            {
                long progress = 100 / (e.MaximumProgress / e.CurrentProgress);
                toolStripProgressBar1.Value = System.Convert.ToInt32(progress);
                CurrentUrl.Text = System.Convert.ToString(webBrowser1.Url);
            }
        }

        private void WebBrowser_Inner_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
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

        private void GoTo_Click(object sender, EventArgs e)
        {
            if(GoToUrl.Text.Length>0)
                webBrowser1.Navigate(GoToUrl.Text);
        }

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
            CurrentUrl.Text = title;
            if (title.Length > charlimit) title = title.Substring(0, charlimit) + "...";
            TabPage MAIN = (TabPage)this.Parent;
            if (MAIN is TabPage)
            {
                MAIN.Text = title;
            }
            if (!webBrowser1.CanGoBack) { Back.Image = null; Back.Enabled = false; }
            else { Back.Image = Properties.Resources.arrow_back; Back.Enabled = true; }
            if (!webBrowser1.CanGoForward) { Forward.Image = null; Forward.Enabled = false; }
            else { Forward.Image = Properties.Resources.arrow_forward; Forward.Enabled = true; }
        }

        private void GoToUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter)) webBrowser1.Navigate(GoToUrl.Text);
            if (e.KeyData == Keys.X && e.Modifiers == Keys.Control) (sender as TextBox).Cut();
            if (e.KeyData == Keys.C && e.Modifiers == Keys.Control) (sender as TextBox).Copy();
            if (e.KeyData == Keys.V && e.Modifiers == Keys.Control) (sender as TextBox).Paste();
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
    }
}
