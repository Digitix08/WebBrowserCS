using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Handler;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    public partial class Chromewebview : UserControl
    {
        readonly string home = Properties.Settings.Default.HomePage;
        string defaultsearch;
        ChromiumWebBrowser chromiumWebBrowser1;
        public Chromewebview(string url)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                InitializeComponent();
                InitializeBrowser();
                DisplayHandler displayer = new DisplayHandler();
                chromiumWebBrowser1.DisplayHandler = displayer;
                chromiumWebBrowser1.Load(url);
                defaultsearch = System.Convert.ToString(Properties.Settings.Default.DefaultSearch);
                
            }
        }

        private void InitializeBrowser()
        {
            chromiumWebBrowser1 = new ChromiumWebBrowser();
            tableLayoutPanel1.Controls.Add(chromiumWebBrowser1, 1, 0);
            chromiumWebBrowser1.Dock = DockStyle.Fill;
            chromiumWebBrowser1.FrameLoadEnd += ChromiumWebBrowser1_FrameLoadEnd;
            chromiumWebBrowser1.FrameLoadStart += ChromiumWebBrowser1_FrameLoadStart;
            chromiumWebBrowser1.TitleChanged += ChromiumWebBrowser1_TitleChanged;
            chromiumWebBrowser1.AddressChanged += ChromiumWebBrowser1_AddressChanged;
        }

        private void Setcolor()
        {
            this.BackColor = Properties.Settings.Default.Windowbgcolor;
            this.ForeColor = Properties.Settings.Default.Textcolor;
            ColorSet.SetColorIncludingChildren(this, typeof(Panel), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(StatusStrip), default, default);
        }

        private void Chromewebview_Load(object sender, EventArgs e)
        {
            Setcolor();
            chromiumWebBrowser1.Load(home);
            if (defaultsearch == "4") defaultsearch = Properties.Settings.Default.Search1;
            else if (defaultsearch == "3") defaultsearch = Properties.Settings.Default.Search2;
            else if (defaultsearch == "2") defaultsearch = Properties.Settings.Default.Search3;
            else if (defaultsearch == "1") defaultsearch = Properties.Settings.Default.Search4;
            else if (defaultsearch == "0") defaultsearch = Properties.Settings.Default.Search5;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Back();
        }

        private void ChromiumWebBrowser1_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            ProgressBarChangeLoad(ProgressBarStyle.Continuous, 0);
        }

        public void ProgressBarChangeLoad(ProgressBarStyle style, int value)
        {
            if (this.InvokeRequired)
            {
                Action safeWrite = delegate { ProgressBarChangeLoad(style, value); };
                this.Invoke(safeWrite);
            }
            else
            {
                toolStripProgressBar1.Style = style;
                toolStripProgressBar1.Value = value;
            }
        }
        private void Reload_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Refresh();
        }

        private void GoHome_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load(home);
        }

        private void Forward_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Forward();
        }

        private void GoTo_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load(GoToUrl.Text);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load(defaultsearch + GoToUrl.Text);
        }

        private async void ToolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(CurrentUrl.Text);
            CurrentUrl.Text = "Copied to clipboard";
            await Task.Delay(500);
            CurrentUrl.Text = System.Convert.ToString(chromiumWebBrowser1.Address);
        }

        private void ChromiumWebBrowser1_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            string title = e.Title;
            ComplicationRequired(title);
        }
        public void ComplicationRequired(string text)
        {
            TabPage MAIN = (TabPage)this.Parent;
            if (MAIN is TabPage)
            {
                if (MAIN.InvokeRequired)
                {
                    Action safeWrite = delegate { ComplicationRequired($"{text} (THREAD2)"); };
                    MAIN.Invoke(safeWrite);
                }
                else MAIN.Text = text.Substring(0, text.Length-10);
            }
        }

        private void ChromiumWebBrowser1_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            ProgressBarChangeLoad(ProgressBarStyle.Marquee, 50);
        }

        private void ChromiumWebBrowser1_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            CurrentUrl.Text = System.Convert.ToString(chromiumWebBrowser1.Address);
            if (!chromiumWebBrowser1.CanGoBack) Change(Back, false, null);
            else Change(Back, true, Properties.Resources.arrow_back);
            if (!chromiumWebBrowser1.CanGoForward) Change(Forward, false, null);
            else Change(Forward, true, Properties.Resources.arrow_forward);
        }

        public void Change(Control place, bool ToChange, Bitmap image)
        {
            if (this.InvokeRequired)
            {
                Action safeWrite = delegate { Change(place, ToChange, image); };
                this.Invoke(safeWrite);
            }
            else
            {
                place.Enabled = ToChange;
                ((PictureBox)place).Image = image;
            }
        }

        private void GoToUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter)) chromiumWebBrowser1.Load(GoToUrl.Text);
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
