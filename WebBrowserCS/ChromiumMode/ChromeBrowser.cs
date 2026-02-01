using CefSharp;
using CefSharp.Handler;
using CefSharp.WinForms;
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
        TableLayoutPanel table = new TableLayoutPanel();
        Button ScrErrorClose = new Button { Text = "Close DevTools" };
        SplitContainer split = new SplitContainer();
        Label lb = new Label { Text = "Html dev tools" };
        Panel DebPanel = new Panel();
        readonly string home = Properties.Settings.Default.HomePage;
        string defaultsearch;
        IGNetworkHandler igNet = new IGNetworkHandler();
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

        internal void GoToOuter(string url)
        {
            chromiumWebBrowser1.Load(url);
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
            this.Invoke(new Action(() => this.Text = title));
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

        /*private void Search1_Click(object sender, EventArgs e) => chromiumWebBrowser1.Load(Search1.Text + GoToUrl.Text);
        private void Search2_Click(object sender, EventArgs e) => chromiumWebBrowser1.Load(Search2.Text + GoToUrl.Text);
        private void Search3_Click(object sender, EventArgs e) => chromiumWebBrowser1.Load(Search3.Text + GoToUrl.Text);
        private void Search4_Click(object sender, EventArgs e) => chromiumWebBrowser1.Load(Search4.Text + GoToUrl.Text);
        private void Search5_Click(object sender, EventArgs e) => chromiumWebBrowser1.Load(Search5.Text + GoToUrl.Text);*/

        private void ChromiumWebBrowser1_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            UriChanged();
        }

        private void ChromiumWebBrowser1_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            UriChanged(e.Title);
        }

        private void ChromiumWebBrowser1_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading) StartBrowse();
            if (!e.IsLoading) CompleteBrowse();
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

        private void iEWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEWindow browser = new IEWindow();
            browser.Show();
        }

        private void chromiumWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            ChromeBrowser chrome = new ChromeBrowser();
            chrome.Show();
        }

        private void textEditWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileEdit file = new FileEdit();
            file.Show();
        }

        private void tabbedWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowserCS TabbedWindow = new WebBrowserCS();
            TabbedWindow.Show();
        }

        private void SearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter)) chromiumWebBrowser1.Load(defaultsearch + GoToUrl.Text);
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
                fileEdit = new FileEdit(fd.FileName);
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

        private void dockedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(DebPanel.Width.ToString() + " " + table.Width.ToString() + " " + table.GetColumnSpan(DebPanel).ToString());
            table.Controls.Add(DebPanel, 0, 1);
            table.SetColumnSpan(DebPanel, 2);
            tableLayoutPanel1.Controls.Remove(chromiumWebBrowser1);
            tableLayoutPanel1.Controls.Add(split, 0, 1);
            tableLayoutPanel1.SetColumnSpan(split, 10);
            DebPanel.Dock = DockStyle.Fill;
            if (chromiumWebBrowser1.ShowDevToolsDocked(DebPanel, "DevTools") == null)
                MessageBox.Show("Please reopen the window", "Error opening DevTools");
            chromiumWebBrowser1.ShowDevToolsDocked(DebPanel, "DevTools");
            split.Panel1.Controls.Add(chromiumWebBrowser1);
        }

        private void standaloneWindowToolStripMenuItem_Click(object sender, EventArgs e) => chromiumWebBrowser1.ShowDevTools();
    }
}
