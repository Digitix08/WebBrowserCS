using Microsoft.Win32;
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
    public partial class Options : Form
    {
        string search1 = Properties.Settings.Default.Search1;
        string search2 = Properties.Settings.Default.Search2;
        string search3 = Properties.Settings.Default.Search3;
        string search4 = Properties.Settings.Default.Search4;
        string search5 = Properties.Settings.Default.Search5;
        int defaultSearch = Properties.Settings.Default.DefaultSearch;
        int RegVal;
        bool RequiresRestart = false;
        public Options()
        {
            InitializeComponent();
        }
        private void Apply()
        {
            Properties.Settings.Default.HomePage = HomePage.Text;
            Properties.Settings.Default.Search1 = search1;
            Properties.Settings.Default.Search2 = search2;
            Properties.Settings.Default.Search3 = search3;
            Properties.Settings.Default.Search4 = search4;
            Properties.Settings.Default.Search5 = search5;
            Properties.Settings.Default.DefaultSearch = defaultSearch;
            if (textBox3.Text != "Transparent")
                Properties.Settings.Default.Elembgcolor = textBox3.BackColor;
            else Properties.Settings.Default.Elembgcolor = Color.Transparent;
            if (textBox5.Text != "Transparent")
                Properties.Settings.Default.Windowbgcolor = textBox5.BackColor;
            else Properties.Settings.Default.Windowbgcolor = Color.Transparent;
            if (textBox4.Text != "Transparent")
                Properties.Settings.Default.Textcolor = textBox4.BackColor;
            else Properties.Settings.Default.Textcolor = Color.Transparent;
            Properties.Settings.Default.DefaultNewTab = (string)comboBox2.SelectedItem;
            Properties.Settings.Default.Save();
            RegVal = (int)numericUpDown1.Value;
            if (RegVal != 0)
            {
                if (RegVal == 7) RegEdit(7000);
                if (RegVal == 8) RegEdit(8000);
                if (RegVal == 9) RegEdit(9000);
                if (RegVal == 10) RegEdit(10000);
                if (RegVal == 11) RegEdit(11000);
            }
            Setcolor();
            if (RequiresRestart)
                if (MessageBox.Show("To apply options, you need to restart the program" + Environment.NewLine + "Do you want to restart it now?", "Restart required", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Application.Restart();
        }
        private void RegEdit(int reg)
        {
            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", RegistryKeyPermissionCheck.ReadWriteSubTree))
                Key.SetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", reg, RegistryValueKind.DWord);
        }
        private void Setcolor()
        {
            this.BackColor = Properties.Settings.Default.Windowbgcolor;
            this.ForeColor = Properties.Settings.Default.Textcolor;
            ColorSet.SetColorIncludingChildren(this, typeof(Button), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(TextBox), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(DomainUpDown), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(ComboBox), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(TabPage), default, default);
            ColorSet.SetColorIncludingChildren(this, typeof(GroupBox), default, default);
        }
            private void Options_Load(object sender, EventArgs e)
        {
            Setcolor();
            string home = Properties.Settings.Default.HomePage;
            HomePage.Text = home;
            domainUpDown2.SelectedIndex = Properties.Settings.Default.DefaultSearch;
            int BrowserVer;
            using (WebBrowser Wb = new WebBrowser())
                BrowserVer = Wb.Version.Major;
            object ieWebVersion = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION").GetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe");
            label3.Text = System.Convert.ToString(BrowserVer);
            label4.Text = System.Convert.ToString(ieWebVersion);
            numericUpDown1.Value = (int)ieWebVersion / 1000;
            if (Properties.Settings.Default.Elembgcolor != Color.Transparent)
                textBox3.BackColor = Properties.Settings.Default.Elembgcolor;
            else textBox3.Text = "Transparent";
            if (Properties.Settings.Default.Windowbgcolor != Color.Transparent)
                textBox5.BackColor = Properties.Settings.Default.Windowbgcolor;
            else textBox5.Text = "Transparent";
            if (Properties.Settings.Default.Textcolor != Color.Transparent)
                textBox4.BackColor = Properties.Settings.Default.Textcolor;
            else textBox4.Text = "Transparent";
            comboBox2.SelectedItem = Properties.Settings.Default.DefaultNewTab;
        }

        private void Okay_Click(object sender, EventArgs e)
        {
            Apply();
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e) => this.Close();
        private void ApplyButton_Click(object sender, EventArgs e) => Apply();

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            if (selectedIndex == 0) textBox2.Text = Properties.Settings.Default.Search1;
            else if (selectedIndex == 1) textBox2.Text = Properties.Settings.Default.Search2;
            else if (selectedIndex == 2) textBox2.Text = Properties.Settings.Default.Search3;
            else if (selectedIndex == 3) textBox2.Text = Properties.Settings.Default.Search4;
            else if (selectedIndex == 4) textBox2.Text = Properties.Settings.Default.Search5;
        }

        private void DomainUpDown2_SelectedItemChanged(object sender, EventArgs e) => defaultSearch = domainUpDown2.SelectedIndex;

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            if (selectedIndex == 0) search1 = textBox2.Text;
            else if (selectedIndex == 1) search2 = textBox2.Text;
            else if (selectedIndex == 2) search3 = textBox2.Text;
            else if (selectedIndex == 3) search4 = textBox2.Text;
            else if (selectedIndex == 4) search5 = textBox2.Text;
        }

        private void TextBox3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (colorDialog.Color != Color.Transparent)
                { textBox3.BackColor = colorDialog.Color; textBox3.Clear(); }
                else textBox3.Text = "Transparent";
                RequiresRestart = true;
            }
        }

        private void TextBox4_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (colorDialog.Color != Color.Transparent)
                { textBox4.BackColor = colorDialog.Color; textBox4.Clear(); }
                else textBox4.Text = "Transparent";
                RequiresRestart = true;
            }
        }
        private void TextBox5_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (colorDialog.Color != Color.Transparent)
                { textBox5.BackColor = colorDialog.Color; textBox5.Clear(); }
                else textBox5.Text = "Transparent";
                RequiresRestart = true;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "Transparent";
            textBox4.BackColor = SystemColors.ControlText;
            textBox5.BackColor = SystemColors.Control;
        }

        private void HomePage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.X && e.Modifiers == Keys.Control) (sender as TextBox).Cut();
            if (e.KeyData == Keys.C && e.Modifiers == Keys.Control) (sender as TextBox).Copy();
            if (e.KeyData == Keys.V && e.Modifiers == Keys.Control) (sender as TextBox).Paste();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            RequiresRestart = true;
        }
    }
}
