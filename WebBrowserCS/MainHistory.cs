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
        DateTime time = new DateTime();
        DataClass dbtools = new DataClass();
        WebBrowserCS MainWindow;
        string histQueryAdd = "INSERT INTO history (date_time, website, browser_eng) VALUES ('{0}', '{1}', '{2}')";
        string histQueryRead = "SELECT date_time, website, browser_eng FROM history ORDER BY date_time DESC";
        public MainHistory(WebBrowserCS sender)
        {
            InitializeComponent();
            MainWindow = sender;
            LoadHistoryElem();
        }

        private void LoadHistoryElem() {
            DataTable dt = dbtools.selectQuery(histQueryRead);
            foreach (DataRow row in dt.Rows)
            {
                string date = row["date_time"].ToString();
                string url = row["website"].ToString();
                string browser = row["browser_eng"].ToString();
                string dateACT = DateTime.Parse(date).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                GetText(dateACT, url, browser);
            }
        }

        public void Add(string url, Control sender, DateTime time)
        {
            GetText(time.ToString(), url, sender.Name);
            string date = time.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            string[] histData = new string[] { date, url, sender.Name };
            int result = dbtools.writeQuery(histData, histQueryAdd);
            if (result != 0)
                MessageBox.Show("write failed (" + result + ")");
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
