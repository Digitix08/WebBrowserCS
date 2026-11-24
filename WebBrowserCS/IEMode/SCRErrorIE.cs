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
        TextBox LogTextBox = new TextBox();
        TextBox ErrTextBox = new TextBox();
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
        public void GetText(string desc, string text)
        {
            string[] row = { text };
            var listViewItem = logList.Items.Add(desc);
            listViewItem.SubItems.AddRange(row);
        }
        public void LogError(string page, string row, string column, string error, string text)
        {
            string[] line = { row, error, text };
            var listViewItem = errorList.Items.Add(page);
            listViewItem.SubItems.AddRange(line);
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
        private void listView1_KeyUp(object sender, KeyEventArgs e) {if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) GetLog(); }
        private void listView1_DoubleClick(object sender, EventArgs e) => GetLog();
        private void GetLog()
        {
            if (!LogLayoutPanel.Controls.Contains(LogTextBox))
            {
                if (logList.SelectedItems.Count > 0)
                {
                    LogLayoutPanel.SetColumnSpan(logList, 1);
                    LogLayoutPanel.Controls.Add(LogTextBox, 1, 1);
                    LogTextBox.Multiline = true;
                    LogTextBox.Dock = DockStyle.Fill;
                    LogTextBox.Text = logList.SelectedItems[0].SubItems[1].Text;
                }
            }
            else
            {
                LogLayoutPanel.Controls.Remove(LogTextBox);
                LogLayoutPanel.SetColumnSpan(logList, 2);
            }
        }
        private void GetError()
        {
            if (!ErrLayoutPanel.Controls.Contains(ErrTextBox))
            {
                if (errorList.SelectedItems.Count > 0)
                {
                    ErrLayoutPanel.SetColumnSpan(logList, 1);
                    ErrLayoutPanel.Controls.Add(ErrTextBox, 1, 1);
                    ErrTextBox.Multiline = true;
                    ErrTextBox.Dock = DockStyle.Fill;
                    ErrTextBox.Text = errorList.SelectedItems[0].SubItems[0].Text;
                }
            }
            else
            {
                ErrLayoutPanel.Controls.Remove(ErrTextBox);
                ErrLayoutPanel.SetColumnSpan(errorList, 2);
            }
        }

        private void errorList_DoubleClick(object sender, EventArgs e) => GetError();
        private void errorList_KeyUp(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) GetLog(); }
    }
}
