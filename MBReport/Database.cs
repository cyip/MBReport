using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace MBReport
{
    class Entry
    {
        public Entry(string name, string cid)
        {
            this.name = cid + " " + name;
            this.cid  = cid;
        }

        public string Name { get { return this.name; } }
        public string Cid { get { return this.cid; } }

        private string name;
        private string cid;
    }

    class Database
    {
        static public string ConstructSqlConnectionString()
        {
            string sqlConnectionString = "";

            sqlConnectionString += "Server=";
            sqlConnectionString += Properties.Settings.Default.dbConnection;
            sqlConnectionString += ";";
            sqlConnectionString += "Database=";
            sqlConnectionString += Properties.Settings.Default.dbName;
            sqlConnectionString += ";";
            if (Properties.Settings.Default.dbUserIntLogin)
            {
                sqlConnectionString += "Integrated Security=true;";
            }
            else
            {
                sqlConnectionString += "user id=";
                sqlConnectionString += Properties.Settings.Default.dbUsername;
                sqlConnectionString += ";";
                sqlConnectionString += "password=";
                sqlConnectionString += Properties.Settings.Default.dbPassword;
                sqlConnectionString += ";";
            }
            sqlConnectionString += "Connection Timeout=5;";
            return sqlConnectionString;
        }

        static public List<Entry> GetCreditOfficers()
        {
            using (SqlConnection connection = new SqlConnection(ConstructSqlConnectionString()))
            {
                connection.Open();
                SqlDataReader sqlReader = null;
                SqlCommand sqlCreditOfficerCommand =
                    new SqlCommand("select distinct cif.cid, cif.Name1, cif.Name2, cif.Name3, cif.Name4 " +
                                    "from relcid inner join cif on relcid.CID = cif.CID " +
                                    "where relcid.Type = 499 order by cif.CID asc",
                                    connection);
                sqlReader = sqlCreditOfficerCommand.ExecuteReader();

                SqlCommand sqlSubgroupCommand =
                    new SqlCommand("select relcid.RelatedCID, cif.Name1, cif.Name2, cif.Name3, cif.Name4 " +
                                    "from relcid inner join cif on relcid.RelatedCID = cif.CID " +
                                    "where relcid.CID = @cid",
                                    connection);

                List<Entry> officersTemp = new List<Entry>();
                while (sqlReader.Read())
                {
                    officersTemp.Add(new Entry(sqlReader["Name1"].ToString().Trim() + " " +
                                                sqlReader["Name2"].ToString().Trim() + " " +
                                                sqlReader["Name3"].ToString().Trim() + " " +
                                                sqlReader["Name4"].ToString().Trim(),
                                                sqlReader["cid"].ToString()));
                }

                List<Entry> officers = new List<Entry>();
                foreach (Entry officer in officersTemp)
                {
                    sqlSubgroupCommand.Parameters.Clear();
                    sqlSubgroupCommand.Parameters.Add("@cid", officer.Cid);
                    sqlReader.Close();
                    sqlReader = sqlSubgroupCommand.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        officers.Add(
                            new Entry(sqlReader["Name1"].ToString().Trim() + " " +
                                        sqlReader["Name2"].ToString().Trim() + " " +
                                        sqlReader["Name3"].ToString().Trim() + " " +
                                        sqlReader["Name4"].ToString().Trim(),
                                        sqlReader["RelatedCID"].ToString()));
                    }
                    sqlReader.Close();
                }

                return officers;
       
            }
        }

        static public List<Entry> GetVillages(string cid)
        {
            using (SqlConnection connection = new SqlConnection(ConstructSqlConnectionString()))
            {
                connection.Open();

                SqlCommand sqlSubgroupCommand =
                    new SqlCommand("select relcid.RelatedCID, cif.Name1, cif.Name2, cif.Name3, cif.Name4 " +
                                    "from relcid inner join cif on relcid.RelatedCID = cif.CID " +
                                    "where relcid.CID = @cid",
                                    connection);

                sqlSubgroupCommand.Parameters.Add("@cid", cid);

                SqlDataReader sqlReader = null;
                sqlReader = sqlSubgroupCommand.ExecuteReader();

                List<Entry> villages = new List<Entry>();
                while (sqlReader.Read())
                {
                    villages.Add(
                        new Entry(sqlReader["Name1"].ToString().Trim() + " " +
                                    sqlReader["Name2"].ToString().Trim() + " " +
                                    sqlReader["Name3"].ToString().Trim() + " " +
                                    sqlReader["Name4"].ToString().Trim(),
                                    sqlReader["RelatedCID"].ToString()));
                }
                sqlReader.Close();

                return villages;
            }
        }

        static public string Currency()
        {
            using (SqlConnection connection = new SqlConnection(ConstructSqlConnectionString()))
            {
                connection.Open();

                SqlCommand sqlCurrencyCommand =
                    new SqlCommand("select CcyType from brparms",
                                    connection);

                SqlDataReader sqlReader = sqlCurrencyCommand.ExecuteReader();

                string currency = "";
                while (sqlReader.Read())
                {
                    currency = sqlReader["CcyType"].ToString();
                }
                sqlReader.Close();

                return currency;
            }
        }

        static public DateTime CurrentRunDate()
        {
            using (SqlConnection connection = new SqlConnection(ConstructSqlConnectionString()))
            {
                connection.Open();

                SqlCommand sqlCommand =
                    new SqlCommand("select CurrRunDate from brparms",
                                    connection);

                SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                DateTime currentRunDate = DateTime.Now;
                while (sqlReader.Read())
                {
                    currentRunDate = Convert.ToDateTime(sqlReader["CurrRunDate"].ToString());
                }
                sqlReader.Close();

                return currentRunDate;
            }
        }

        static public void SavingAccount(string cid, out string savingAccountId, out string savingAmount)
        {
            string currency = Currency();

            using (SqlConnection connection = new SqlConnection(ConstructSqlConnectionString()))
            {
                connection.Open();

                string accPrefix = @"11%";
                if (currency.ToUpper() == "USD")
                    accPrefix = @"22%";

                SqlCommand sqlCommand =
                    new SqlCommand("select r.ACC + '-' + r.chd as savacc, s.BalAmt from RELACC r " +
                                   "inner join SVACC s on s.Acc = r.Acc " +
                                   "where CID=@cid",
                                    connection);

                sqlCommand.Parameters.Add("@cid", cid);

                SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                savingAccountId = "";
                int savingAmountInt = 0;
                while (sqlReader.Read())
                {
                    if(sqlReader["savacc"].ToString().Substring(2,1) == "2")
                        savingAccountId = sqlReader["savacc"].ToString();

                    savingAmountInt += Convert.ToInt32(sqlReader["BalAmt"].ToString());
                }

                savingAmount = Convert.ToString(savingAmountInt);

                sqlReader.Close();
            }
        }
 
    }
}
