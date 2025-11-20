using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WebBrowserCS
{
    public partial class FileTab : UserControl
    {
        readonly WebBrowser EditViewer = new WebBrowser();
        readonly TabPage WebEdit = new TabPage();
        public string filePath;
        string fileDir; int row = 0, inUse = 0;
        private bool IsHTML;
        public FileTab()
        {
            InitializeComponent();
            WebEdit.Text = "Visual HTML";
            EditViewer.Dock = DockStyle.Fill;
            EditViewer.ScriptErrorsSuppressed = true;
        }

        private void Setcolor()
        {
            this.BackColor = Properties.Settings.Default.Windowbgcolor;
            this.ForeColor = Properties.Settings.Default.Textcolor;
            ColorSet.SetColorIncludingChildren(this, typeof(TextBox), Properties.Settings.Default.Elembgcolor, Properties.Settings.Default.Textcolor);
            ColorSet.SetColorIncludingChildren(this, typeof(StatusStrip), Properties.Settings.Default.Elembgcolor, Properties.Settings.Default.Textcolor);
            ColorSet.SetColorIncludingChildren(this, typeof(MenuStrip), Properties.Settings.Default.Elembgcolor, Properties.Settings.Default.Textcolor);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                row = 0;
                MainText.Clear();
                string line;
                FPath.Text = fd.FileName;
                fileDir = fd.FileName;
                if (Path.GetExtension(fd.FileName) == ".htm" || Path.GetExtension(fd.FileName) == ".html" || Path.GetExtension(fd.FileName) == ".xhtm" || Path.GetExtension(fd.FileName) == ".xhtml")
                {
                    HtmlPreparations(fd.FileName);
                }
                else
                {
                    if (EditModes.TabPages.Contains(WebEdit)) EditModes.TabPages.Remove(WebEdit);
                    previewToolStripMenuItem.Visible = false;
                    StreamReader filecontent = new StreamReader(fd.FileName);
                    line = filecontent.ReadLine();
                    while (line != null)
                    {
                        MainText.AppendText(line);
                        MainText.AppendText(Environment.NewLine);
                        line = filecontent.ReadLine();
                        toolStripStatusLabel3.Text = System.Convert.ToString(row);
                        row++;
                    }
                    filecontent.Close();
                    MainText.SelectionStart = 0; MainText.ScrollToCaret();
                }
                FPath.Text = fileDir;
                string title = Path.GetFileName(fileDir);
                TabPage MAIN = (TabPage)this.Parent;
                if (MAIN is TabPage)
                {
                    MAIN.Text = title;
                }
            }
        }

        private void HtmlPreparations(string FileName)
        {
            previewToolStripMenuItem.Visible = true; IsHTML = true;
            if (!EditModes.TabPages.Contains(WebEdit))
            {
                EditModes.TabPages.Insert(1, WebEdit);
                WebEdit.Controls.Add(EditViewer);
                WebEdit.Leave += WebEdit_Leave;
            }
            MainText.Clear(); row = 0;
            string TempFile = Path.GetDirectoryName(FileName) + "\\___unsaved.html";
            inUse = 1;
            StreamWriter HTMLTemp = new StreamWriter(TempFile);
            StreamReader filecontent = new StreamReader(FileName);
            string line = filecontent.ReadLine();
            while (line != null)
            {
                string Lowerline = line.ToLower();
                int temp0 = Lowerline.IndexOf("<body");
                if (temp0 != -1)
                {
                    int temp1 = Lowerline.IndexOf(">", temp0) + 1;
                    line = line.Substring(0, temp0 + temp1) + "<div id='DffD_199870960_Edit0' contentEditable='true'>";
                    if (Lowerline.Length != temp1) line += line.Substring(temp0 + temp1, Lowerline.Length);
                }
                MainText.AppendText(line); MainText.AppendText(Environment.NewLine);
                toolStripStatusLabel3.Text = System.Convert.ToString(row);
                row++;
                HTMLTemp.WriteLine(line);
                line = filecontent.ReadLine();
            }
            filecontent.Close();
            HTMLTemp.Close();
            inUse = 0;
            MainText.SelectionStart = 0; MainText.ScrollToCaret();
            EditViewer.Navigate(TempFile);
        }

        private void WebEdit_Leave(object sender, EventArgs e)
        {
            
            //MessageBox.Show("test");
            //MainText.Text = EditViewer.DocumentText.ToString();
            //MessageBox.Show(EditViewer.DocumentText.ToString());
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileDir == null) SaveAs();
            else
            {
                File.WriteAllText(fileDir, MainText.Text);
            }
        }

        private void SaveAs()
        {
            SaveFileDialog sd = new SaveFileDialog
            {
                Title = "Save file...",
                InitialDirectory = "%homedir%",
                Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt|Html files (*.htm, *.html)|*.htm; *.html",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (sd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sd.FileName, MainText.Text);
                fileDir = sd.FileName;
                FPath.Text = fileDir;
                string title = Path.GetFileName(fileDir);
                WebBrowserCS MAIN = (WebBrowserCS)this.ParentForm;
                if (this.ParentForm is WebBrowserCS)
                {
                    MAIN.Text = title;
                }
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e) => SaveAs();

        private void PreviewInIETabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowserCS MAIN = (WebBrowserCS)this.ParentForm;
            if (this.ParentForm is WebBrowserCS)
            {
                MAIN.NewTab(fileDir, "IETab");
            }
        }

        private void PreviewInChromiumTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowserCS MAIN = (WebBrowserCS)this.ParentForm;
            if (this.ParentForm is WebBrowserCS)
            {
                MAIN.NewTab(fileDir, "ChromiumTab");
            }
        }

        private void MainText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                row++;
                toolStripStatusLabel3.Text = System.Convert.ToString(row);
            }
        }

        private void FileTab_Load(object sender, EventArgs e)
        {
            Setcolor();
            if (filePath != null)
            {
                row = 0;
                MainText.Clear();
                try
                {
                    string line;
                    FPath.Text = filePath;
                    fileDir = filePath;
                    StreamReader filecontent = new StreamReader(filePath);
                    line = filecontent.ReadLine();
                    while (line != null)
                    {
                        MainText.AppendText(line);
                        MainText.AppendText(Environment.NewLine);
                        line = filecontent.ReadLine();
                        toolStripStatusLabel3.Text = System.Convert.ToString(row);
                        row++;
                    }
                    filecontent.Close();
                }
                catch (System.IO.FileNotFoundException) { MessageBox.Show("The file \"" + filePath + "\" does not exist. Creating new file", "File not found"); filePath = null; fileDir = null; }
                catch (System.IO.DirectoryNotFoundException) { MessageBox.Show("The path \"" + filePath + "\" does not exist. Creating new file", "Path not found"); filePath = null; fileDir = null; }
                finally
                {
                    MainText.SelectionStart = 0; MainText.ScrollToCaret();
                    string title = Path.GetFileName(filePath);
                    TabPage MAIN = (TabPage)this.Parent;
                    if (MAIN is TabPage)
                    {
                        MAIN.Text = title;
                    }
                    MainText.SelectionStart = 0; MainText.ScrollToCaret();
                    if (Path.GetExtension(filePath) == ".htm" || Path.GetExtension(filePath) == ".html" || Path.GetExtension(filePath) == ".xhtm" || Path.GetExtension(filePath) == ".xhtml")
                    {
                        HtmlPreparations(filePath);
                    }
                    else if (EditModes.TabPages.Contains(WebEdit)) EditModes.TabPages.Remove(WebEdit);
                    previewToolStripMenuItem.Visible = false;
                }
            }
            MainText.Font = new Font(MainText.Font.FontFamily, Properties.Settings.Default.FontSize);
        }

        private void MainText_TextChanged(object sender, EventArgs e)
        {
            string TempFile;
            if (fileDir == null) TempFile = "___unsaved.html";
            else TempFile = Path.GetDirectoryName(fileDir) + "\\___unsaved.html";
            if (inUse == 0)
            {
                StreamWriter HTMLTemp = new StreamWriter(TempFile);
                HTMLTemp.Write(MainText.Text);
                HTMLTemp.Close();
                EditViewer.Navigate(TempFile);
            }
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainText.CanUndo) MainText.Undo();
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainText.CanSelect) MainText.SelectAll();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainText.SelectedText != null) MainText.Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainText.SelectedText != null) MainText.Copy();
        }

        private void MainText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.X && e.Modifiers == Keys.Control) { MainText.Cut(); e.Handled = true; }
            if (e.KeyData == Keys.C && e.Modifiers == Keys.Control) { MainText.Copy(); e.Handled = true; }
            if (e.KeyData == Keys.V && e.Modifiers == Keys.Control) { (sender as TextBox).Paste(); e.Handled = true; }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText()) MainText.Paste();
        }
    }
}
