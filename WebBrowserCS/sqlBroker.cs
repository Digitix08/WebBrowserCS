using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowserCS
{
    class DataClass
    {
        private SQLiteConnection sqlite;

        public DataClass()
        {
            //This part killed me in the beginning.  I was specifying "DataSource"
            //instead of "Data Source"
            string location = Directory.GetCurrentDirectory() + "\\" + "history.db";
            sqlite = new SQLiteConnection("Data Source=" + location + ";New=False");

        }

        public DataTable selectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }
            sqlite.Close();
            return dt;
        }

        public int writeQuery(string[] data, string query)
        {
            SQLiteDataAdapter ad;
            int fail = 0;

            string NewQuery = string.Format(query, data);

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = NewQuery;  //set the passed query
                cmd.ExecuteNonQuery();
                fail = 0;
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
                fail = ex.ErrorCode;
            }
            sqlite.Close();
            return fail;
        }
    }
}
