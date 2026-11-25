using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    public partial class SCRErrorIEControl : UserControl
    {
        IEwebview webview = null;
        IEWindow standalone = null;
        TextBox textBox1 = new TextBox() { Multiline = true };
        DateTime time = new DateTime();
        public void sendData(bool JSDisabled)
        {
            disableJS.Checked = JSDisabled;
            string JStatus = "Browser has JS ";
            if (JSDisabled) JStatus += "enabled";
            else JStatus += "enabled";
            info(JStatus);
        }

        public void log(string s)
        {
            time = DateTime.Now;
            GetText(time.ToString(), s);
        }
        public void info(string s)
        {
            string[] row = { s };
            var listViewItem = listView1.Items.Add("Info:");
            listViewItem.SubItems.AddRange(row);
            listViewItem.BackColor = Color.Aqua;
        }
        public void warn(string s)
        {
            string[] row = { s };
            var listViewItem = listView1.Items.Add("Warn:");
            listViewItem.SubItems.AddRange(row);
            listViewItem.BackColor = Color.Orange;
        }
        public void error(string s)
        {
            string[] row = { s };
            var listViewItem = listView1.Items.Add("ERROR:");
            listViewItem.SubItems.AddRange(row);
            listViewItem.BackColor = Color.Red;
            listViewItem.ForeColor = Color.White;
        }
        public void LogError(string page, string row, string column, string error, string text)
        {
            string errorText = "Page:" + Environment.NewLine
                + page + Environment.NewLine
                + "Row: " + row + Environment.NewLine
                + "Character: " + column + Environment.NewLine
                + "Error Code: " + error + Environment.NewLine
                + "Description: " + text;
            string[] line = { text, errorText };
            var listViewItem = listView1.Items.Add("Script Error");
            listViewItem.SubItems.AddRange(line);
            listViewItem.BackColor = Color.Red;
            listViewItem.ForeColor = Color.White;
        }
        public void LogNaviChange(string page)
        {
            string[] line = { page };
            var listViewItem = listView1.Items.Add("Navigated");
            listViewItem.SubItems.AddRange(line);
        }
        public void GetText(string desc, string text)
        {
            string[] row = { text };
            var listViewItem = listView1.Items.Add(desc);
            listViewItem.SubItems.AddRange(row);
        }

        public SCRErrorIEControl(IEwebview ieW = null, IEWindow ieS = null)
        {
            InitializeComponent();
            if (ieW != null) webview = ieW;
            if (ieS != null) standalone = ieS;
        }

        private void SCRErrorIE_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
        private void listView1_KeyUp(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) GetItem(); }
        private void listView1_DoubleClick(object sender, EventArgs e) => GetItem();
        private void GetItem()
        {
            if (!ScrErrorPanel.Controls.Contains(textBox1))
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ScrErrorPanel.SetColumnSpan(listView1, 2);
                    ScrErrorPanel.Controls.Add(textBox1, 2, 0);
                    textBox1.Multiline = true;
                    textBox1.Dock = DockStyle.Fill;
                    textBox1.Text = listView1.SelectedItems[0].SubItems[listView1.SelectedItems[0].SubItems.Count - 1].Text;
                }
            }
            else
            {
                ScrErrorPanel.Controls.Remove(textBox1);
                ScrErrorPanel.SetColumnSpan(listView1, 3);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (webview != null) webview.SetJSErrState(disableJS.Checked);
            if (standalone != null) standalone.SetJSErrState(disableJS.Checked);
        }

        private void disableErrorLog_CheckedChanged(object sender, EventArgs e)
        {
            if (webview != null) webview.SetCLogState(disableJS.Checked);
            if (standalone != null) standalone.SetCLogState(disableJS.Checked);
        }
    }
}
