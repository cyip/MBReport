using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace MBReport
{
    
    public partial class CreditOfficer : Form
    {
        public CreditOfficer()
        {
            InitializeComponent();
            PopulateOfficers();
        }

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
            return sqlConnectionString;
        }

        private void PopulateOfficers()
        {
            using (SqlConnection connection = new SqlConnection(ConstructSqlConnectionString()))
            {
                try
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

                    ArrayList officers = new ArrayList();
                    while (sqlReader.Read())
                    {
                        officers.Add(new Officer(sqlReader["Name1"].ToString().Trim() + " " +
                                                 sqlReader["Name2"].ToString().Trim() + " " +
                                                 sqlReader["Name3"].ToString().Trim() + " " +
                                                 sqlReader["Name4"].ToString().Trim(),
                                                 sqlReader["cid"].ToString()));
                    }

                    foreach (Officer officer in officers)
                    {
                        sqlSubgroupCommand.Parameters.Clear();
                        sqlSubgroupCommand.Parameters.Add("@cid", officer.Cid);
                        sqlReader.Close();
                        sqlReader = sqlSubgroupCommand.ExecuteReader();
                        while (sqlReader.Read())
                        {
                            officer.Subgroups.Add(
                                new Officer(sqlReader["Name1"].ToString().Trim() + " " +
                                            sqlReader["Name2"].ToString().Trim() + " " +
                                            sqlReader["Name3"].ToString().Trim() + " " +
                                            sqlReader["Name4"].ToString().Trim(),
                                            sqlReader["RelatedCID"].ToString()));
                        }
                        sqlReader.Close();
                    }

                    foreach (Officer officer in officers)
                    {
                        foreach (Officer subgroup in officer.Subgroups)
                        {
                            sqlSubgroupCommand.Parameters.Clear();
                            sqlSubgroupCommand.Parameters.Add("@cid", subgroup.Cid);
                          
                            sqlReader = sqlSubgroupCommand.ExecuteReader();
                            while (sqlReader.Read())
                            {
                                subgroup.Subgroups.Add(
                                    new Officer(sqlReader["Name1"].ToString().Trim() + " " +
                                                sqlReader["Name2"].ToString().Trim() + " " +
                                                sqlReader["Name3"].ToString().Trim() + " " +
                                                sqlReader["Name4"].ToString().Trim(),
                                                sqlReader["RelatedCID"].ToString()));
                            }
                            sqlReader.Close();
                        }
                    }           

                    treeViewOfficers.BeginUpdate();

                    foreach (Officer officer in officers)
                    {
                        treeViewOfficers.Nodes.Add(officer.ToString());

                        foreach (Officer subgroup in officer.Subgroups)
                        {
                            treeViewOfficers.Nodes[officers.IndexOf(officer)].Nodes.Add(subgroup.ToString());
                            
                            foreach (Officer village in subgroup.Subgroups)
                            {
                                treeViewOfficers.Nodes[officers.IndexOf(officer)]
                                    .Nodes[officer.Subgroups.IndexOf(subgroup)]
                                    .Nodes.Add(village.ToString());
                            }
                        }
                    }
                }
                finally
                {
                    treeViewOfficers.EndUpdate();
                }
            }
        }

        private void treeViewOfficers_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InstallmentReportForm installmentReportForm = new InstallmentReportForm();

            installmentReportForm.ShowDialog(this);
        }

        public string Cid
        {
            get { return this.treeViewOfficers.SelectedNode.Text.Substring(0, 6); }
        }

        public string VillageName
        {
            get { return this.treeViewOfficers.SelectedNode.Text.Substring(6); }
        }

        public string OfficerName
        {
            get { return "Dummy officer name"; }
        }

        public DateTime CollectionDate
        {
            //get{return this.collectionDate.Value.ToString("yyyy-MM-dd");}

            get { return this.collectionDate.Value; }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {

        }
    }

    public class Officer : System.Object
    {
        private string name = "";
        private string cid = "";
        private ArrayList subgroups = new ArrayList();

        public Officer(string name, string cid)
        {
            this.name = name;
            this.cid = cid;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Cid
        {
            get { return this.cid; }
            set { this.cid = value; }
        }

        public string ToString()
        {
            return (this.cid + " " + this.name);
        }

        public ArrayList Subgroups
        {
            get { return this.subgroups; }
        }
    }

}
