using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    public partial class WebBrowserCS : Form
    {
        string home = Properties.Settings.Default.HomePage;
        string defaultsearch, ExtFile = "AvailableExtensions.txt";
        string[] ExtDelimit = new string[] { "_;_" };
        int OpenTabs = 0;
        IGNetworkHandler igNet = new IGNetworkHandler();
        public List<string[]> AvailTabs = new List<string[]>();

        public WebBrowserCS()
        {
            InitializeComponent();
            Search1.Text = Properties.Settings.Default.Search1;
            Search2.Text = Properties.Settings.Default.Search2;
            Search3.Text = Properties.Settings.Default.Search3;
            Search4.Text = Properties.Settings.Default.Search4;
            Search5.Text = Properties.Settings.Default.Search5;
            defaultsearch = System.Convert.ToString(Properties.Settings.Default.DefaultSearch);
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

        private void StartArgsHandler(string Args)
        {
            if (Args.Contains("StartIE")) IERedirect();
            if (Args.IndexOf("\\") != -1 && Path.GetExtension(Args) != null || Args.Contains("http"))
            {
                string path = Path.GetExtension(Args);
                if (path == ".html" || path == ".htm" || Args.Contains("http"))
                {
                    NewTab(Args);
                }
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

        private void WebBrowserCS_Load(object sender, EventArgs e)
        {
            //BrowserCS.BrowserCS_show();
            if (Program.StartArgs.Length != 0)
            {
                if (Program.StartArgs[0] == "/help" || Program.StartArgs[0] == "/h") Console.WriteLine("please use command line to only specify filepaths");
                else StartArgsHandler(Program.StartArgs[0]);
            }
            Setcolor();
            if(igNet.Check_mode(home) != "false")
                home = igNet.Check_mode(home);
            if (defaultsearch == "4") defaultsearch = Properties.Settings.Default.Search1;
            else if (defaultsearch == "3") defaultsearch = Properties.Settings.Default.Search2;
            else if (defaultsearch == "2") defaultsearch = Properties.Settings.Default.Search3;
            else if (defaultsearch == "1") defaultsearch = Properties.Settings.Default.Search4;
            else if (defaultsearch == "0") defaultsearch = Properties.Settings.Default.Search5;
            AvailTabs.Add(new string[] { newTabToolStripMenuItem.DropDownItems[0].Text, "NewIETab" });
            AvailTabs.Add(new string[] { newTabToolStripMenuItem.DropDownItems[1].Text, "NewChromiumTab" });
            if (File.Exists(ExtFile))
            {
                StreamReader extens = new StreamReader(ExtFile);
                string line = extens.ReadLine();
                while (line != null)
                {
                    if (line[0] != '#')
                    {
                        ToolStripMenuItem ext = new ToolStripMenuItem { Text = line.Substring(0, line.IndexOf("_;_")) };
                        ToolStripMenuItem ext2 = new ToolStripMenuItem { Text = line.Substring(0, line.IndexOf("_;_")) };
                        ToolStripMenuItem ext3 = new ToolStripMenuItem { Text = line.Substring(0, line.IndexOf("_;_")) };
                        bool isTab = false;

                        line = line.Substring(line.IndexOf("_;_") + 3, line.Length - line.IndexOf("_;_") - 3);
                        string[] args = line.Split(ExtDelimit, StringSplitOptions.None);
                        if (args.Length >= 3) { if (args[2] == "isTab") { isTab = true; line = line.Substring(0, line.IndexOf("_;_isTab")); } }
                        ext.Tag = line; ext2.Tag = line; ext3.Tag = line;
                        
                        string path = Directory.GetCurrentDirectory() + "\\" + args[0];

                        if (File.Exists(path))
                        {
                            if (isTab)
                            {
                                ext.Click += Ext_Tab_Click; ext2.Click += Ext_Tab_Click; ext3.Click += Ext_Tab_Click;
                                newTabToolStripMenuItem.DropDownItems.Add(ext);
                                tabContextMenu.Items.Add(ext2);
                                MoreContextMenuStrip.Items.Add(ext3);
                                AvailTabs.Add(new string[] { ext.Text, "NewUserTab", args[1] });
                            }
                            else
                            {
                                newWindowToolStripMenuItem.DropDownItems.Add(ext);
                                ext.Click += Ext_Click;
                            }
                        }
                        else MessageBox.Show("The file " + path + " does not exist");
                    }
                    line = extens.ReadLine();
                }
                extens.Close();
            }
        }

        private void Ext_Click(object sender, EventArgs e)
        {
            string tag = ((ToolStripMenuItem)sender).Tag.ToString();
            ExternalLaunch(tag, ((ToolStripMenuItem)sender).Text, false, home);
        }

        private void Ext_Tab_Click(object sender, EventArgs e)
        {
            string tag = ((ToolStripMenuItem)sender).Tag.ToString();
            ExternalLaunch(tag, ((ToolStripMenuItem)sender).Text, true, home);
        }

        public void ExternalLaunch(string tag, string name, bool isTab, string vars = "")
        {
            string[] args = tag.Split(ExtDelimit, StringSplitOptions.None);
            string loc = Directory.GetCurrentDirectory() + "\\" + args[0];
            string exe = args[1];

            Assembly DLL; Type theType; var c = new object(); MethodInfo method;
            if (File.Exists(loc))
            {
                if (isTab) try
                    {
                        DLL = Assembly.LoadFile(loc);
                        theType = DLL.GetType(exe + ".IGTab");
                        c = Activator.CreateInstance(theType);
                        method = theType.GetMethod("init");
                        var tabRaw = method.Invoke(c, new object[] { @vars });
                        if (tabRaw is UserControl)
                        {
                            UserControl tab = (UserControl)tabRaw;
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
                        else MessageBox.Show("The call did not return the adequate contents for a tab", "Invalid value returned");
                    }
                    catch (Exception ex)
                    {
                        if (ex is System.BadImageFormatException || ex is System.Reflection.TargetInvocationException) { MessageBox.Show("The build you are using is not compatible with this extension." + Environment.NewLine + "Details:" + Environment.NewLine + loc + Environment.NewLine + "Name: " + name, "Invalid extension runtime version"); }
                        if (ex is System.ArgumentNullException) { MessageBox.Show("The extension you are using does not support tabs." + Environment.NewLine + "Details:" + Environment.NewLine + loc + Environment.NewLine + "Name: " + name, "Invalid extension call mode"); }
                    }
                else try
                    {
                        DLL = Assembly.LoadFile(loc);
                        theType = DLL.GetType(exe + ".IGExtension");
                        c = Activator.CreateInstance(theType);
                        method = theType.GetMethod("init");
                        method.Invoke(c, new object[] { @vars });
                    }
                    catch (Exception ex)
                    {
                        if (ex is System.BadImageFormatException || ex is System.Reflection.TargetInvocationException) { MessageBox.Show("The build you are using is not compatible with this extension." + Environment.NewLine + "Details:" + Environment.NewLine + loc + Environment.NewLine + "Name: " + name, "Invalid extension runtime version"); }
                    }
            }
            else MessageBox.Show("The file " + loc + "does not exist");
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

        internal void NewChromiumTab(string url, TabPage tab)
        {
            string title = "Tab " + (Tabs.TabCount + 1).ToString();
            tab.Text = title;
            ChromeWebview ChromeTab = new ChromeWebview(url);
            tab.Controls.Add(ChromeTab);
            ChromeTab.Dock = DockStyle.Fill;
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

        internal void NewFileTab(string path, TabPage tab)
        {
            string title = "FileTab " + (Tabs.TabCount + 1).ToString();
            tab.Text = title;
            FileTab newTab;
            if (path != "NewFile") { newTab = new FileTab(path); }
            else newTab = new FileTab();
            tab.Controls.Add(newTab);
            newTab.Dock = DockStyle.Fill;
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
            WebBrowserCS wbcs = new WebBrowserCS();
            wbcs.Show();
        }

        private void newIEInstanceactualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEWindow NewIE = new IEWindow();
            NewIE.OpenIE();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //IEwebview.Save();
        }
    }
}
