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
    public partial class MainHistory : Form
    {
        DateTime time = new DateTime();
        WebBrowserCS MainWindow;

        public MainHistory(WebBrowserCS sender)
        {
            InitializeComponent();
            MainWindow = sender;
        }

        public void Add(string url, Control sender, DateTime time)
        {
            GetText(time.ToString(), url, sender.Name);
        }
        public void GetText(string desc, string text, string senderName)
        {
            string[] row = { text, senderName };
            var listViewItem = listView1.Items.Add(desc);
            listViewItem.SubItems.AddRange(row);
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
                if (listView1.Sorting == (SortOrder)1) listView1.Sorting = (SortOrder)2;
                else listView1.Sorting = (SortOrder)1;
        }

        private void listView1_DoubleClick(object sender, EventArgs e) => LoadHistory();
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter)) LoadHistory();
        }

        private void LoadHistory()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                MainWindow.NewTab(listView1.SelectedItems[0].SubItems[1].Text); //, listView1.SelectedItems[0].SubItems[2].Text);
                this.Hide();
            }
        }

        private void MainHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }
    }
}
