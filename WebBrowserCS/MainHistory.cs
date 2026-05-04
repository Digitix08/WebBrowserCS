using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    public partial class MainHistory : Form
    {
        DataClass dbtools = new DataClass();
        BrowserCS MainWindow;
        string histQueryAdd = "INSERT INTO history (date_time, website, browser_eng) VALUES ('{0}', '{1}', '{2}') Returning id";
        string histQueryRead = "SELECT id, date_time, website, browser_eng FROM history ORDER BY date_time DESC";
        string histQueryDelete = "DELETE FROM history WHERE id {0}";
        public MainHistory(BrowserCS sender)
        {
            InitializeComponent();
            MainWindow = sender;
            LoadHistoryElem();
        }

        private void LoadHistoryElem() {
            DataTable dt = dbtools.selectQuery(histQueryRead);
            foreach (DataRow row in dt.Rows)
            {
                string id = row["id"].ToString();
                string date = row["date_time"].ToString();
                string url = row["website"].ToString();
                string browser = row["browser_eng"].ToString();
                string dateACT = DateTime.Parse(date).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                AppendText(id, dateACT, url, browser);
            }
        }

        public void Add(string url, Control sender, DateTime time)
        {
            
            string date = time.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            string[] histData = new string[] { date, url, sender.Name };
            DataTable result = dbtools.selectQuery(histQueryAdd, histData);
            if (result == null)
                MessageBox.Show("write failed ()");
            else
            {
                string id = result.Rows[0]["id"].ToString();
                InsText(id, time.ToString(), url, sender.Name);
            }
        }
        public void InsText(string id, string desc, string text, string senderName)
        {
            string[] row = { text, senderName, id };
            var listViewItem = listView1.Items.Insert(0, desc);
            listViewItem.SubItems.AddRange(row);
        }

        public void AppendText(string id, string desc, string text, string senderName)
        {
            string[] row = { text, senderName, id };
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

        private void histEraseElem(object sender, EventArgs e)
        {
            button1.Text = "Erasing history...";
            button1.Enabled = false;
            ListView.SelectedListViewItemCollection selItems = listView1.SelectedItems;
            int count = selItems.Count;
            long[] toErase = new long[selItems.Count];
            string elem = "in (";

            if (selItems.Count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    elem += selItems[i].SubItems[3].Text;
                    if (i < count - 1) elem += ", ";
                    listView1.Items.Remove(selItems[i]);
                }
                elem += ")";
                string[] thingArray = new string[] { elem };
                dbtools.selectQuery(histQueryDelete, thingArray);
            }
            MessageBox.Show(count.ToString() + " elements erased");

            button1.Enabled = true;
            listView1_SelectedIndexChanged(sender, e);
        }

        private void MainHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                button1.Text = "Erase selected";
                button1.Click += histEraseElem;
            }
            else
            {
                button1.Text = "Delete history...";
                button1.Click -= histEraseElem;
            }
        }
    }
}
