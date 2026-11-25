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
    public partial class SCRErrorIE : Form
    {
        TextBox textBox1 = new TextBox() { Multiline = true };
        DateTime time = new DateTime();

        public void log(string s)
        {
            time = DateTime.Now;
            GetText(time.ToString(), s);
        }
        public void info(string s)
        {
            string[] row = { s };
            var listViewItem = logList.Items.Add("Info:");
            listViewItem.SubItems.AddRange(row);
            listViewItem.BackColor = Color.Aqua;
        }
        public void warn(string s)
        {
            string[] row = { s };
            var listViewItem = logList.Items.Add("Warn:");
            listViewItem.SubItems.AddRange(row);
            listViewItem.BackColor = Color.Orange;
        }
        public void error(string s)
        {
            string[] row = { s };
            var listViewItem = logList.Items.Add("ERROR:");
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
            var listViewItem = logList.Items.Add("Script Error");
            listViewItem.SubItems.AddRange(line);
            listViewItem.BackColor = Color.Red;
            listViewItem.ForeColor = Color.White;
        }
        public void LogNaviChange(string page)
        {
            string[] line = { page };
            var listViewItem = logList.Items.Add("Navigated");
            listViewItem.SubItems.AddRange(line);
        }
        public void GetText(string desc, string text)
        {
            string[] row = { text };
            var listViewItem = logList.Items.Add(desc);
            listViewItem.SubItems.AddRange(row);
        }

        public SCRErrorIE()
        {
            InitializeComponent();
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
            logList.Items.Clear();
        }
        private void logList_KeyUp(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) GetItem(); }
        private void logList_DoubleClick(object sender, EventArgs e) => GetItem();
        private void GetItem()
        {
            if (!LogLayoutPanel.Controls.Contains(textBox1))
            {
                if (logList.SelectedItems.Count > 0)
                {
                    LogLayoutPanel.SetColumnSpan(logList, 1);
                    LogLayoutPanel.Controls.Add(textBox1, 1, 1);
                    textBox1.Multiline = true;
                    textBox1.Dock = DockStyle.Fill;
                    textBox1.Text = logList.SelectedItems[0].SubItems[logList.SelectedItems[0].SubItems.Count - 1].Text;
                }
            }
            else
            {
                LogLayoutPanel.Controls.Remove(textBox1);
                LogLayoutPanel.SetColumnSpan(logList, 2);
            }
        }
    }
}
