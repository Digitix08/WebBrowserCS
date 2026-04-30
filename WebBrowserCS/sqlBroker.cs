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
            string userdata = Directory.GetCurrentDirectory() + "\\userdata\\";
            if (!Directory.Exists(userdata)) Directory.CreateDirectory(userdata);
            string location = userdata + "history.db";
            if (!File.Exists(location)) SQLiteConnection.CreateFile(location);
            sqlite = new SQLiteConnection("Data Source=" + location + ";New=False");

            string checkQuery = "CREATE TABLE IF NOT EXISTS \"history\" ("
                + "\"id\"    INTEGER NOT NULL UNIQUE, \"date_time\" TEXT NOT NULL, \"website\"   TEXT NOT NULL, \"browser_eng\"   TEXT NOT NULL, \"more_info\" TEXT,"
                + " PRIMARY KEY(\"id\" AUTOINCREMENT))";
            SQLiteCommand cmd;
            sqlite.Open();  //Initiate connection to the db
            cmd = sqlite.CreateCommand();
            cmd.CommandText = checkQuery;  //set the passed query
            cmd.ExecuteNonQuery();
            sqlite.Close();
        }

        public DataTable selectQuery(string query)
        {
            int fail = 0;
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
                fail = ex.ErrorCode;
            }
            sqlite.Close();
            return dt;
        }

        public int writeQuery(string[] data, string query)
        {
            int fail = 0;

            string NewQuery = string.Format(query, data);

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = NewQuery;  //set the passed query
                cmd.ExecuteNonQuery();
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
