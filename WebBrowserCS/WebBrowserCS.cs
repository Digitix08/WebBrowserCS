using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    public partial class BrowserCS : Form
    {
        string home = Properties.Settings.Default.HomePage;
        string defaultsearch;
        string[] SupportedHTMLTypes = { ".htm", ".html", ".xhtm", ".xhtml", ".mhtm", ".mhtml" };
        int OpenTabs = 0;
        IGNetworkHandler igNet = new IGNetworkHandler();
        ExtensionLoader ExtLoad;
        MainHistory History;
        public List<string[]> AvailTabs = new List<string[]>();

        public BrowserCS()
        {
            InitializeComponent();
            Search1.Text = Properties.Settings.Default.Search1;
            Search2.Text = Properties.Settings.Default.Search2;
            Search3.Text = Properties.Settings.Default.Search3;
            Search4.Text = Properties.Settings.Default.Search4;
            Search5.Text = Properties.Settings.Default.Search5;
            defaultsearch = System.Convert.ToString(Properties.Settings.Default.DefaultSearch);
            History = new MainHistory(this);
            ExtLoad = new ExtensionLoader(this);
        }

        private void Setcolor()
        {
            if (Properties.Settings.Default.Textcolor == Properties.Settings.Default.Elembgcolor)
            {
                Color inv = Properties.Settings.Default.Textcolor;
                inv = Color.FromArgb(inv.A, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                MessageBox.Show("The Text color is the same as the background color (" + Properties.Settings.Default.Elembgcolor.ToString() + ")." + Environment.NewLine + "To make sure you can use the app, we will change it to the opposite color (" + inv.ToString() + ")");
                Properties.Settings.Default.Textcolor = inv;
                Properties.Settings.Default.Save();
            }
            this.BackColor = Properties.Settings.Default.Windowbgcolor;
            this.ForeColor = Properties.Settings.Default.Textcolor;
            ColorSet.SetColorIncludingChildren(this, typeof(Button), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(TextBox), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(TabPage), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(MenuStrip), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(ToolStrip), default, default);
        }

        private void WebBrowserCS_Load(object sender, EventArgs e)
        {
            //BrowserCS.BrowserCS_show();
            if (Program.StartArgs.Length != 0)
            {
                if (Program.StartArgs[0] == "/help" || Program.StartArgs[0] == "/h") Console.WriteLine("please use command line to only specify filepaths");
                else StartArgsHandler(Program.StartArgs[0]);
            }
            Setcolor();
            OLCheck();
            switch (defaultsearch) {
                case "4": defaultsearch = Properties.Settings.Default.Search1; break;
                case "3": defaultsearch = Properties.Settings.Default.Search2; break;
                case "2": defaultsearch = Properties.Settings.Default.Search3; break;
                case "1": defaultsearch = Properties.Settings.Default.Search4; break;
                case "0": defaultsearch = Properties.Settings.Default.Search5; break;
                default: break;
            }

            AvailTabs.Add(new string[] { newTabToolStripMenuItem.DropDownItems[0].Text, "NewIETab" });
            AvailTabs.Add(new string[] { newTabToolStripMenuItem.DropDownItems[1].Text, "NewChromiumTab" });
            ExtLoad.LoadExtensions();
        }

        private async void OLCheck()
        {/*
            string result = await igNet.Check_mode(home);
            if (igNet.Check_mode(home) != "false")
                home = igNet.Check_mode(home);*/
        }       

        private void StartArgsHandler(string Args)
        {
            if (Args.Contains("StartIE")) IERedirect();
            if ((Args.IndexOf("\\") != -1 && Path.GetExtension(Args) != null) || Args.Contains("http"))
            {
                string path = Path.GetExtension(Args);
                if (SupportedHTMLTypes.Contains(path) || Args.Contains("http"))
                {
                    NewTab(Args);
                }
                else if (path == ".xml") NewTab(Args);
                else if (path == ".txt") NewTab(Args, "FileTab");
            }
        }

        public void IERedirect()
        {
            IEWindow NewIE = new IEWindow();
            NewIE.OpenIE();
            Application.Exit();
        }

        public CancellationTokenSource SetTimeout(Action action, int millis)
        {
            var cts = new CancellationTokenSource();
            var ct = cts.Token;
            _ = Task.Run(() => {
                Thread.Sleep(millis);
                if (!ct.IsCancellationRequested)
                    action();
            }, ct);

            return cts;
        }

        //ecternal launch
        public void ProcessTab(string Name, string Tag, string[] args)
        {
            ToolStripMenuItem ext = new ToolStripMenuItem { Text = Name, Tag = Tag };
            ToolStripMenuItem ext2 = new ToolStripMenuItem { Text = Name, Tag = Tag };
            ToolStripMenuItem ext3 = new ToolStripMenuItem { Text = Name, Tag = Tag };
            ext.Click += Ext_Tab_Click; ext2.Click += Ext_Tab_Click; ext3.Click += Ext_Tab_Click;
            newTabToolStripMenuItem.DropDownItems.Add(ext);
            tabContextMenu.Items.Add(ext2);
            MoreContextMenuStrip.Items.Add(ext3);
            AvailTabs.Add(new string[] { ext.Text, "NewUserTab", args[1] });
        }

        public void ProcessWindow(string Name, string Tag, string[] args)
        {
            ToolStripMenuItem ext = new ToolStripMenuItem { Text = Name, Tag = Tag };
            newWindowToolStripMenuItem.DropDownItems.Add(ext);
            ext.Click += Ext_Click;
        }

        private void Ext_Click(object sender, EventArgs e)
        {
            string tag = ((ToolStripMenuItem)sender).Tag.ToString();
            ExtLoad.LaunchExtension(tag, ((ToolStripMenuItem)sender).Text, false, home);
        }

        private void Ext_Tab_Click(object sender, EventArgs e)
        {
            string tag = ((ToolStripMenuItem)sender).Tag.ToString();
            ExtLoad.LaunchExtension(tag, ((ToolStripMenuItem)sender).Text, true, home);
        }

        public void LoadTab(UserControl tab, string name)
        {
            TabPage myTabPage = new TabPage();
            if (Tabs.SelectedIndex == 0 && Tabs.TabCount > 1)
            {
                Tabs.TabPages.Insert(Tabs.SelectedIndex + 1, myTabPage);
                Tabs.SelectedIndex += 1;
            }
            else
            {
                Tabs.TabPages.Insert(Tabs.SelectedIndex, myTabPage);
                Tabs.SelectedIndex -= 1;
            }
            string title = name + " " + (Tabs.TabCount + 1).ToString();
            myTabPage.Text = title;
            myTabPage.Controls.Add(tab);
            tab.Dock = DockStyle.Fill;
        }

        private void Tabs_MouseClick(object sender, MouseEventArgs e)
        {
            Point pointerXY = new Point((Size)e.Location);
            pointerXY.Offset(Location);
            if (e.Button == MouseButtons.Left)
            {
                if (Tabs.SelectedTab == Tabs.TabPages["creatTab"])
                {
                    NewTab(home, "IETab");
                }
            }
        }

        internal void NewIETab(string url, TabPage tab)
        {
            string title = "IETab " + (Tabs.TabCount + 1).ToString();
            tab.Text = title;
            IEwebview newTab = new IEwebview(url, this);
            tab.Controls.Add(newTab);
            newTab.Dock = DockStyle.Fill;
            newTab.TitleChanged += ChangeTitle;
            newTab.HistoryNewEntry += AppendHistory;
            newTab.FaviconChanged += ChangeFavicon;
        }

        internal void NewChromiumTab(string url, TabPage tab)
        {
            string title = "ChrTab " + (OpenTabs + 1).ToString();
            tab.Text = title;
            ChromeWebview ChromeTab = new ChromeWebview(url);
            tab.Controls.Add(ChromeTab);
            ChromeTab.Dock = DockStyle.Fill;
            ChromeTab.TitleChanged += ChangeTitle;
            ChromeTab.HistoryNewEntry += AppendHistory;
            ChromeTab.FaviconChanged += ChangeFavicon;
        }

        internal void NewFileTab(string path, TabPage tab)
        {
            string title = "FileTab " + (Tabs.TabCount + 1).ToString();
            tab.Text = title;
            FileTab newTab;
            if (path != "NewFile") { newTab = new FileTab(path); }
            else newTab = new FileTab();
            tab.Controls.Add(newTab);
            newTab.Dock = DockStyle.Fill;
            newTab.TitleChanged += ChangeTitle;
            newTab.NewTab += OpenNewTab;
        }

        public void ChangeTitle(string title, Control caller)
        {
            if (Tabs.InvokeRequired)
            {
                Action safeWrite = delegate { ChangeTitle(title, caller); };
                Tabs.Invoke(safeWrite);
            }
            else caller.Parent.Text = title;
        }

        public void OpenNewTab(string url, string type, Control caller)
        {
            NewTab(url, type);
        }

        public void AppendHistory(string url, DateTime time, Control caller)
        {
            History.Add(url, caller, time);
        }

        public void ChangeFavicon(string url, Control caller, bool update)
        {
            /*if (caller.Parent.InvokeRequired)
            {
                Action safeWrite = delegate { ChangeFavicon(url, caller, update); };
                caller.Parent.Invoke(safeWrite);
            }
            else
            {
                if(url.Length > 0)
                    TabSelectors[SelectedTab].setFavicon(url);
                else
                    TabSelectors[SelectedTab].clearFavicon();
            }*/
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tabs.SelectedIndex < Tabs.TabCount - 1)
                Tabs.TabPages.Remove(Tabs.SelectedTab);
            else
            {
                MessageBox.Show("you can't do that");
            }
            Tabs.SelectedIndex = Tabs.TabCount - 2;
            OpenTabsLabel.Text = (Tabs.TabCount - 1).ToString() + " tabs open";
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options settings = new Options();
            settings.Show();
        }

        private void TabContextMenu_Opening(object sender, CancelEventArgs e)
        {
            Point p = this.Tabs.PointToClient(Cursor.Position);
            for (int i = 0; i < this.Tabs.TabCount; i++)
            {
                Rectangle r = this.Tabs.GetTabRect(i);
                if (r.Contains(p))
                {
                    this.Tabs.SelectedIndex = i; // i is the index of tab under cursor
                    return;
                }
            }
            e.Cancel = true;
        }

        private void CreateTab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => NewTab(home);
        private void IETabToolStripMenuItem_Click(object sender, EventArgs e) => NewTab(home);
        private void FileTabToolStripMenuItem1_Click(object sender, EventArgs e) => NewTab("NewFile", "FileTab");
        private void NewChromeTabToolStripMenuItem_Click(object sender, EventArgs e) => NewTab(home, "ChromiumTab");

        private void ChromiumWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChromeBrowser chrome = new ChromeBrowser();
            chrome.Show();
        }

        private void CustomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options settings = new Options();
            settings.tabControl1.SelectTab("Customization");
            settings.Show();
        }

        private void IEWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEWindow browser = new IEWindow();
            browser.Show();
        }

        private void ExitToolStripMenuItem_Click_1(object sender, EventArgs e) => Application.Exit();
        private void NewTab_Click(object sender, EventArgs e) => NewTab("");

        public void NewTab(string URL, string TabType = null) 
        {
            if(String.IsNullOrEmpty(URL))URL = home;
            string NewTabType = Properties.Settings.Default.DefaultNewTab;
            if (TabType != null) NewTabType = TabType;
            if (NewTabType != null)
            {
                if (OpenTabs <= 5)
                {
                    TabPage myTabPage = new TabPage();
                    if (Tabs.SelectedIndex == 0 && Tabs.TabCount > 1)
                    {
                        Tabs.TabPages.Insert(Tabs.SelectedIndex + 1, myTabPage);
                        Tabs.SelectedIndex += 1;
                    }
                    else
                    {
                        Tabs.TabPages.Insert(Tabs.SelectedIndex, myTabPage);
                        Tabs.SelectedIndex -= 1;
                    }
                    switch (NewTabType)
                    {
                        case ("IETab"): NewIETab(URL, myTabPage); break;
                        case ("ChromiumTab"): NewChromiumTab(URL, myTabPage); break;
                        case ("FileTab"): NewFileTab(URL, myTabPage); break;
                        default:
                            Options options = new Options();
                            if (MessageBox.Show("The current setting for default new tab is invalid! Do you want to go to options and set it to accepted value?", "Invalid setting!", MessageBoxButtons.YesNo) == DialogResult.Yes) options.Show();
                            break;
                    }
                    OpenTabsLabel.Text = (Tabs.TabCount - 1).ToString() + " tabs open";
                    OpenTabs++;
                    var timeout = SetTimeout(() =>
                    {
                        if (OpenTabs > 1) OpenTabs--;
                    }, 1000);
                }
                else MessageBox.Show("Too many tabs opened in a second. Press OK and try again", "Too many tabs opened in a second");
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                Title = "Open file...",
                InitialDirectory = "%homedir%",
                Filter = "All files (*.*)|*.*|Html files (*.htm, *.html)|*.htm; *.html|Text files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                StartArgsHandler(fd.FileName);
            }
        }

        private void mdiparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIParent1 mdiParent = new MDIParent1();
            mdiParent.Show();
        }

        private void Go_Click(object sender, EventArgs e) => NewTab(textBox1.Text);
        private void Search_Click(object sender, EventArgs e) => NewTab(defaultsearch + textBox2.Text);

        private void More1_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            MoreContextMenuStrip.Show(ptLowerLeft);
        }

        private void More2_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            SearchContextMenuStrip.Show(ptLowerLeft);
        }

        private void iETabToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String Url;
            if (String.IsNullOrEmpty(textBox1.Text)) Url = home;
            else Url = textBox1.Text;
            NewTab(Url, "IETab");
        }

        private void chromiumTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Url;
            if (String.IsNullOrEmpty(textBox1.Text)) Url = home;
            else Url = textBox1.Text;
            TabPage myTabPage = new TabPage();
            if (Tabs.SelectedIndex == 0 && Tabs.TabCount > 1)
            {
                Tabs.TabPages.Insert(Tabs.SelectedIndex + 1, myTabPage);
                Tabs.SelectedIndex += 1;
            }
            else
            {
                Tabs.TabPages.Insert(Tabs.SelectedIndex, myTabPage);
                Tabs.SelectedIndex -= 1;
            }
            NewChromiumTab(Url, myTabPage);
        }

        private void fileTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Path = null;
            if (!String.IsNullOrEmpty(textBox1.Text))Path = textBox1.Text;
            NewTab(Path, "FileTab");
        }

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                Title = "Open file...",
                InitialDirectory = "%homedir%",
                Filter = "All files (*.*)|*.*|Html files (*.htm, *.html)|*.htm; *.html|Text files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fd.FileName;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        { 
            if (e.KeyData == Keys.Enter) NewTab(textBox1.Text);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) NewTab(defaultsearch + textBox2.Text);
        }

        private void textEditWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileEdit fedit = new FileEdit();
            fedit.Show();
        }

        private void tabbedWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrowserCS wbcs = new BrowserCS();
            wbcs.Show();
        }

        private void NewIEInstanceactualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEWindow NewIE = new IEWindow();
            NewIE.OpenIE();
        }

        private void HistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            History.Show();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //IEwebview.Save();
        }
    }
}
