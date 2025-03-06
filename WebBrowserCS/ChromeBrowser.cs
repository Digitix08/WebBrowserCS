using CefSharp;
using CefSharp.Handler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    public partial class ChromeBrowser : Form
    {
        readonly string home = Properties.Settings.Default.HomePage;
        string defaultsearch;
        public ChromeBrowser()
        {
            InitializeComponent();
            /*Search1.Text = Properties.Settings.Default.Search1;
            Search2.Text = Properties.Settings.Default.Search2;
            Search3.Text = Properties.Settings.Default.Search3;
            Search4.Text = Properties.Settings.Default.Search4;
            Search5.Text = Properties.Settings.Default.Search5;*/
            defaultsearch = System.Convert.ToString(Properties.Settings.Default.DefaultSearch);
            DisplayHandler displayer = new DisplayHandler();
            chromiumWebBrowser1.DisplayHandler = displayer;
        }

        private void ChromeBrowser_Load(object sender, EventArgs e)
        {
            Setcolor();
            chromiumWebBrowser1.Load(home);
            if (defaultsearch == "4") defaultsearch = Properties.Settings.Default.Search1;
            else if (defaultsearch == "3") defaultsearch = Properties.Settings.Default.Search2;
            else if (defaultsearch == "2") defaultsearch = Properties.Settings.Default.Search3;
            else if (defaultsearch == "1") defaultsearch = Properties.Settings.Default.Search4;
            else if (defaultsearch == "0") defaultsearch = Properties.Settings.Default.Search5;
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

        private void Back_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Back();
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Reload();
        }

        private void GoHome_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load(home);
        }
        private void Forward_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Forward();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load(defaultsearch + GoToUrl.Text);
        }

        /*private void Search1_Click(object sender, EventArgs e) => chromiumWebBrowser1.Load(Search1.Text + GoToUrl.Text);
        private void Search2_Click(object sender, EventArgs e) => chromiumWebBrowser1.Load(Search2.Text + GoToUrl.Text);
        private void Search3_Click(object sender, EventArgs e) => chromiumWebBrowser1.Load(Search3.Text + GoToUrl.Text);
        private void Search4_Click(object sender, EventArgs e) => chromiumWebBrowser1.Load(Search4.Text + GoToUrl.Text);
        private void Search5_Click(object sender, EventArgs e) => chromiumWebBrowser1.Load(Search5.Text + GoToUrl.Text);*/

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

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options setting = new Options();
            setting.Show();
        }

        private void CustomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options settings = new Options();
            settings.tabControl1.SelectTab("Customization");
            settings.Show();
        }

        private void GoToUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter)) chromiumWebBrowser1.Load(GoToUrl.Text);
        }

        private void NewIEInstanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Browser browser = new Browser();
            browser.Show();
        }

        private void TestChromiumVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChromeBrowser chrome = new ChromeBrowser();
            chrome.Show();
        }

        private void SearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter)) chromiumWebBrowser1.Load(defaultsearch + GoToUrl.Text);
        }

        private void ChromiumWebBrowser1_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            ProgressBarChangeLoad(ProgressBarStyle.Marquee, 50);
        }

        private void GoTo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(GoToUrl.Text))
            {
                chromiumWebBrowser1.Load(GoToUrl.Text);
            }
        }

        private void WindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChromeBrowser browser = new ChromeBrowser();
            browser.Show();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

        private async void CurrentUrl_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(CurrentUrl.Text);
            CurrentUrl.Text = "Copied to clipboard";
            await Task.Delay(500);
            CurrentUrl.Text = System.Convert.ToString(chromiumWebBrowser1.Address);
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
                chromiumWebBrowser1.Load(fd.FileName);
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

        public void ChangeText(Control place, string text)
        {
            if (this.InvokeRequired)
            {
                Action safeWrite = delegate { ChangeText(place,text); };
                this.Invoke(safeWrite);
            }
            else
            {
                place.Text = text;
            }
        }

        private void chromiumWebBrowser1_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            ChangeText(this, e.Title + " (Chrome browser)");
        }
    }
}
