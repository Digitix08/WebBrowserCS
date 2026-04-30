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
        TextBox textBox1 = new TextBox();
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
            if (!tableLayoutPanel1.Controls.Contains(textBox1))
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    tableLayoutPanel1.SetColumnSpan(listView1, 1);
                    tableLayoutPanel1.Controls.Add(textBox1, 1, 0);
                    textBox1.Multiline = true;
                    textBox1.Dock = DockStyle.Fill;
                    textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                }
            }
            else
            {
                tableLayoutPanel1.Controls.Remove(textBox1);
                tableLayoutPanel1.SetColumnSpan(listView1, 2);
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
