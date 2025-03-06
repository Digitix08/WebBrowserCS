using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace WebBrowserCS
{
    public partial class WebBrowserCS : Form
    {
        readonly string home = Properties.Settings.Default.HomePage;
        string defaultsearch, ExtFile = "AvailableExtensions.txt";
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
            MessageBox.Show(Args);
            if (Args.IndexOf("\\") != -1 && Path.GetExtension(Args) != null || Args.Contains("http"))
            {
                string path = Path.GetExtension(Args);
                if (path == ".html" || path == ".htm" || Args.Contains("http"))
                {
                    DialogResult chrome = MessageBox.Show("Do you want to open in Chrome View?", "", MessageBoxButtons.YesNo);
                    if (chrome == DialogResult.Yes) NewChromiumTab(Args);
                    else NewIETab(Args);
                }
                else if (path == ".txt") NewFileTab(Args);
            }
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
            if (defaultsearch == "4") defaultsearch = Properties.Settings.Default.Search1;
            else if (defaultsearch == "3") defaultsearch = Properties.Settings.Default.Search2;
            else if (defaultsearch == "2") defaultsearch = Properties.Settings.Default.Search3;
            else if (defaultsearch == "1") defaultsearch = Properties.Settings.Default.Search4;
            else if (defaultsearch == "0") defaultsearch = Properties.Settings.Default.Search5;
            if (File.Exists(ExtFile))
            {
                StreamReader extens = new StreamReader(ExtFile);
                string line = extens.ReadLine();
                while (line != null)
                {
                    if (line[0] != '#')
                    {
                        ToolStripMenuItem ext = new ToolStripMenuItem { Text = line.Substring(0, line.IndexOf("_;_")) };
                        line = line.Substring(line.IndexOf("_;_") + 3, line.Length - line.IndexOf("_;_") - 3);
                        ext.Tag = line;
                        string path = Directory.GetCurrentDirectory() + "\\" + line.Substring(0, line.Length - line.IndexOf("_;_") + 1);
                        if (File.Exists(path))
                        {
                            newToolStripMenuItem.DropDownItems.Add(ext);
                            ext.Click += ext_Click;
                        }
                        else MessageBox.Show("The file " + path + "does not exist");
                    }
                    line = extens.ReadLine();
                }
                extens.Close();
            }
        }

        private void ext_Click(object sender, EventArgs e)
        {
            string tag = ((ToolStripMenuItem)sender).Tag.ToString();
            externalLaunch(tag, "window");
        }

        private void externalLaunch(string tag, string mode)
        {
            string loc = Directory.GetCurrentDirectory() + "\\" + tag.Substring(0, tag.Length - tag.IndexOf("_;_") + 1);
            string exe = tag.Substring(tag.IndexOf("_;_") + 3, tag.Length - tag.IndexOf("_;_") - 3);
            Assembly DLL; Type theType; var c = new object(); MethodInfo method;
            if (File.Exists(loc))
            {
                DLL = Assembly.LoadFile(loc);
                theType = DLL.GetType(exe + ".IGExtension");
                c = Activator.CreateInstance(theType);
                method = theType.GetMethod("init");
                method.Invoke(c, new object[] { @mode });
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
                    NewIETab(home);
                }
            }
        }
        internal void NewIETab(string url)
        {
            string title = "IETab " + (Tabs.TabCount + 1).ToString();
            TabPage myTabPage = new TabPage(title);
            if (Tabs.SelectedIndex == 0)
            {
                Tabs.TabPages.Insert(Tabs.SelectedIndex + 1, myTabPage);
                Tabs.SelectedIndex += 1;
            }
            else
            {
                Tabs.TabPages.Insert(Tabs.SelectedIndex, myTabPage);
                Tabs.SelectedIndex -= 1;
            }
            IEwebview newTab = new IEwebview(url);
            myTabPage.Controls.Add(newTab);
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
        }

        internal void NewChromiumTab(string url)
        {
            string title = "Tab " + (Tabs.TabCount + 1).ToString();
            TabPage myTabPage = new TabPage(title);
            Tabs.TabPages.Insert(Tabs.TabPages.Count - 1, myTabPage);
            Tabs.SelectedIndex = Tabs.TabPages.Count - 2;
            Chromewebview ChromeTab = new Chromewebview(url);
            myTabPage.Controls.Add(ChromeTab);
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

        internal void NewFileTab(string path)
        {
            string title = "FileTab " + (Tabs.TabCount + 1).ToString();
            TabPage myTabPage = new TabPage(title);
            if (Tabs.SelectedIndex == 0)
            {
                Tabs.TabPages.Insert(Tabs.SelectedIndex + 1, myTabPage);
                Tabs.SelectedIndex += 1;
            }
            else
            {
                Tabs.TabPages.Insert(Tabs.SelectedIndex, myTabPage);
                Tabs.SelectedIndex -= 1;
            }
            FileTab newTab;
            if (path != "NewFile") { newTab = new FileTab() { filePath = path }; }
            else newTab = new FileTab();
            myTabPage.Controls.Add(newTab);
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

        private void CreateTab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => NewIETab(home);
        private void IETabToolStripMenuItem_Click(object sender, EventArgs e) => NewIETab(home);
        private void FileToolStripMenuItem1_Click(object sender, EventArgs e) => NewFileTab("NewFile");
        private void NewChromeTabToolStripMenuItem_Click(object sender, EventArgs e) => NewChromiumTab(home);

        private void TestChromiumVersionToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void OldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Browser browser = new Browser();
            browser.Show();
        }

        private void ExitToolStripMenuItem_Click_1(object sender, EventArgs e) => Application.Exit();

        private void NewTab_Click(object sender, EventArgs e)
        {
            NewTab("");
        }

        private void NewTab(string URL) 
        {
            if(String.IsNullOrEmpty(URL))URL = home;
            string NewTabType = Properties.Settings.Default.DefaultNewTab;
            if (NewTabType != null)
                switch (NewTabType)
                {
                    case ("IETab"): NewIETab(URL); return;
                    case ("ChromiumTab"): NewChromiumTab(URL); return;
                    case ("FileTab"): NewIETab(URL); return;
                    default:
                        Options options = new Options();
                        if (MessageBox.Show("The current setting for default new tab is invalid! Do you want to go to options and set it to accepted value?", "Invalid setting!", MessageBoxButtons.YesNo) == DialogResult.Yes)options.Show();
                        return;
                }
        }

        private void Forward_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void Reload_Click(object sender, EventArgs e)
        {

        }

        private void Back_Click(object sender, EventArgs e)
        {

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
            NewIETab(Url);
        }

        private void chromiumTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Url;
            if (String.IsNullOrEmpty(textBox1.Text)) Url = home;
            else Url = textBox1.Text;
            NewChromiumTab(Url);
        }

        private void fileTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Path = null;
            if (!String.IsNullOrEmpty(textBox1.Text))Path = textBox1.Text;
            NewFileTab(Path);
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

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyData == Keys.X && e.Modifiers == Keys.Control) { (sender as TextBox).Cut(); e.Handled = true; }
            if (e.KeyData == Keys.C && e.Modifiers == Keys.Control) { (sender as TextBox).Copy(); e.Handled = true; }
            if (e.KeyData == Keys.V && e.Modifiers == Keys.Control) { (sender as TextBox).Paste(); e.Handled = true; }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        { 
            if (e.KeyData == Keys.Enter) NewTab(textBox1.Text);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) NewTab(defaultsearch + textBox2.Text);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //IEwebview.Save();
        }
    }
}
