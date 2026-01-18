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
    public partial class ChromeWebview : UserControl
    {
        TableLayoutPanel table = new TableLayoutPanel();
        Button ScrErrorClose = new Button { Text = "Close DevTools" };
        SplitContainer split = new SplitContainer();
        Label lb = new Label { Text = "Html dev tools" };
        Panel DebPanel = new Panel();
        readonly string home = Properties.Settings.Default.HomePage;
        string defaultsearch;
        ChromiumWebBrowser chromiumWebBrowser1;
        int charlimit = 30;
        IGNetworkHandler igNet = new IGNetworkHandler();
        public ChromeWebview(string url)
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
            tableLayoutPanel1.Controls.Add(chromiumWebBrowser1, 0, 1);
            tableLayoutPanel1.SetColumnSpan(chromiumWebBrowser1, 10);
            chromiumWebBrowser1.Dock = DockStyle.Fill;
            chromiumWebBrowser1.TitleChanged += ChromiumWebBrowser1_TitleChanged;
            chromiumWebBrowser1.AddressChanged += ChromiumWebBrowser1_AddressChanged;
            chromiumWebBrowser1.LoadingStateChanged += ChromiumWebBrowser1_LoadingStateChanged;
        }

        private void Setcolor()
        {
            this.BackColor = Properties.Settings.Default.Windowbgcolor;
            this.ForeColor = Properties.Settings.Default.Textcolor;
            ColorSet.SetColorIncludingChildren(this, typeof(Panel), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(StatusStrip), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(TableLayoutPanel), default, default);
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
            split.Dock = DockStyle.Fill;
            table.Dock = DockStyle.Fill;
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            lb.Font = new Font("Arial", 15, FontStyle.Bold, GraphicsUnit.Point);
            lb.Width = 500;
            table.Controls.Add(ScrErrorClose, 0, 0);
            table.Controls.Add(lb, 1, 0);
            table.SetColumnSpan(DebPanel, 2);
            ScrErrorClose.Click += ScrDebClose_Click;
            split.Panel2.Controls.Add(table);
            Setcolor();
        }

        private void UriChanged(string text = null)
        {
            string url = "New Page";
            string title = url;
            if (chromiumWebBrowser1.Address != null)
            {
                url = chromiumWebBrowser1.Address;
                title = url;
            }
            if (text != null) title = text;
            CurrentUrl.GetCurrentParent().Invoke(new Action(() => CurrentUrl.Text = url));
            bool isFocused = false;
            GoToUrl.Invoke(new Action(() => isFocused = GoToUrl.Focused));
            if (!isFocused)
            {
                GoToUrl.Invoke(new Action(() => GoToUrl.Text = url));
            }
            if (title.Length > charlimit) title = title.Substring(0, charlimit) + "...";
            ComplicationRequired(title);
        }

        private void Back_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Back();
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Reload();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Stop();
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
            if (!string.IsNullOrEmpty(GoToUrl.Text))
            {
                chromiumWebBrowser1.Load(GoToUrl.Text);
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load(defaultsearch + GoToUrl.Text);
        }

        private async void CurrentUrl_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(status.Text);
            status.Text = "Copied to clipboard";
            await Task.Delay(500);
            status.Text = System.Convert.ToString(chromiumWebBrowser1.Address);
        }

        private void ChromiumWebBrowser1_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            UriChanged();
        }
        
        private void ChromiumWebBrowser1_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            UriChanged(e.Title);
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

        private void ChromiumWebBrowser1_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading)StartBrowse();
            if (!e.IsLoading)CompleteBrowse();
        }

        private void StartBrowse()
        {
            if (this.InvokeRequired)
            {
                Action safeWrite = delegate { StartBrowse(); };
                this.Invoke(safeWrite);
            }
            else
            {
                toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                toolStripProgressBar1.Value = 50;
                if (igNet.Check_mode(chromiumWebBrowser1.Address) != "false")
                    chromiumWebBrowser1.Load(igNet.Check_mode(chromiumWebBrowser1.Address));
                status.Text = "Loading...";
                Reload.Image = Properties.Resources.cancel;
                Reload.Click += Cancel_Click;
            }
        }

        private void CompleteBrowse()
        {
            if (this.InvokeRequired)
            {
                Action safeWrite = delegate { CompleteBrowse(); };
                this.Invoke(safeWrite);
            }
            else
            {
                if (toolStripProgressBar1.Style != ProgressBarStyle.Blocks)
                {
                    toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                    toolStripProgressBar1.Value = 100;
                }
                string title = chromiumWebBrowser1.Address;
                int oldstep = toolStripProgressBar1.Step;
                int step = oldstep;
                step = toolStripProgressBar1.Maximum - toolStripProgressBar1.Value;
                toolStripProgressBar1.Step = step;
                toolStripProgressBar1.PerformStep();
                toolStripProgressBar1.Step = oldstep;
                status.Text = "Done";
                Reload.Image = Properties.Resources.arrow_reload;
                Reload.Click += Reload_Click;
                if (!chromiumWebBrowser1.CanGoBack) Change(Back, false, Properties.Resources.arrow_back_disabled);
                else Change(Back, true, Properties.Resources.arrow_back);
                if (!chromiumWebBrowser1.CanGoForward) Change(Forward, false, Properties.Resources.arrow_forward_disabled);
                else Change(Forward, true, Properties.Resources.arrow_forward);
            }
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

        private void DebMode_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem itm in DebMode.DropDownItems)
            {
                if (itm.Checked)
                {
                    if (itm.Text == "Docked") {
                        //MessageBox.Show(DebPanel.Width.ToString() + " " + table.Width.ToString() + " " + table.GetColumnSpan(DebPanel).ToString());
                        table.Controls.Add(DebPanel, 0, 1);
                        table.SetColumnSpan(DebPanel, 2);
                        tableLayoutPanel1.Controls.Remove(chromiumWebBrowser1);
                        tableLayoutPanel1.Controls.Add(split, 0, 1);
                        tableLayoutPanel1.SetColumnSpan(split, 10);
                        DebPanel.Dock = DockStyle.Fill;
                        if (chromiumWebBrowser1.ShowDevToolsDocked(DebPanel, "DevTools") == null)
                            MessageBox.Show("Please reopen the tab", "Error opening DevTools");
                        chromiumWebBrowser1.ShowDevToolsDocked(DebPanel, "DevTools");
                        split.Panel1.Controls.Add(chromiumWebBrowser1);
                    }
                    else chromiumWebBrowser1.ShowDevTools();
                }
            }     
        }

        private void ScrDebClose_Click(object sender, EventArgs e)
        {
            split.Panel1.Controls.Remove(chromiumWebBrowser1);
            tableLayoutPanel1.Controls.Remove(split);
            tableLayoutPanel1.Controls.Add(chromiumWebBrowser1, 0, 1);
        }

        private void DevToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem itm in ((ToolStripMenuItem)sender).GetCurrentParent().Items) itm.Checked = false;
            ((ToolStripMenuItem)sender).Checked = true;
        }
    }
}
