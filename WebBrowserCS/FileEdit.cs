using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    public partial class FileEdit : Form
    {
        public string fileDir; int row = 0;
        public FileEdit()
        {
            InitializeComponent();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

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
                MainText.Clear();
                string line;
                this.Text = fd.FileName;
                fileDir = fd.FileName;
                StreamReader filecontent = new StreamReader(fd.FileName);
                line = filecontent.ReadLine();
                while (line != null)
                {
                    MainText.AppendText(line);
                    MainText.AppendText(Environment.NewLine);
                    line = filecontent.ReadLine();
                }
                filecontent.Close();
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileDir == null) SaveAs();
            else
            {
                File.WriteAllText(fileDir, MainText.Text);
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e) => SaveAs();

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
                this.Text = fileDir;
            }
        }

        private void FileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FileEdit fileEdit = new FileEdit();
            fileEdit.Show();
        }

        private void FileEdit_Load(object sender, EventArgs e)
        {
            Setcolor();
            if (fileDir != null)
            {
                row = 0;
                MainText.Clear();
                string line;
                this.Text = fileDir;
                StreamReader filecontent = new StreamReader(fileDir);
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
            MainText.Font = new Font(MainText.Font.FontFamily, Properties.Settings.Default.FontSize);
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

        private void NewIEInstanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Browser browser = new Browser();
            browser.Show();
        }

        private void TestChromiumVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChromeBrowser chromeBrowser = new ChromeBrowser();
            chromeBrowser.Show();
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Show();
        }

        private void CustomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options settings = new Options();
            settings.tabControl1.SelectTab("Customization");
            settings.Show();
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainText.CanUndo) MainText.Undo();
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MainText.CanSelect)MainText.SelectAll();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainText.SelectedText != null)MainText.Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainText.SelectedText != null) MainText.Copy();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText()) MainText.Paste();
        }
    }
}
