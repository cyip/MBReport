using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MBReport
{
    public partial class SettingsScreen : Form
    {
        public SettingsScreen()
        {
            InitializeComponent();
            PopulateSettings();
        }

        private void PopulateSettings()
        {
            this.comboBoxServer.Text = Properties.Settings.Default.dbConnection;
            this.comboBoxServer.Items.Add(@"DATAENCODER\SQL2008R2");
            this.databaseList.Items.Add("SovannPhoum_USD");
            this.databaseList.Items.Add("SovannPhoum_KHR");
            this.databaseList.Text = Properties.Settings.Default.dbName;
            if (Properties.Settings.Default.dbUserIntLogin)
            {
                this.checkBoxSecurity.Checked = true;
                this.labelUsername.Enabled = false;
                this.labelPassword.Enabled = false;
                this.textBoxUsername.Enabled = false;
                this.textBoxPassword.Enabled = false;
            }
            else
            {
                this.textBoxUsername.Text = Properties.Settings.Default.dbUsername;
                this.textBoxPassword.Text = Properties.Settings.Default.dbPassword;
            }

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateSettings();
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void checkBoxSecurity_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.dbUserIntLogin = checkBoxSecurity.Checked;

            if (checkBoxSecurity.Checked)
            {
                this.labelUsername.Enabled = false;
                this.labelPassword.Enabled = false;
                this.textBoxUsername.Enabled = false;
                this.textBoxPassword.Enabled = false;
            }
            else
            {
                this.labelUsername.Enabled = true;
                this.labelPassword.Enabled = true;
                this.textBoxUsername.Enabled = true;
                this.textBoxPassword.Enabled = true;
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            updateSettings();

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

            using(SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select top 10 cid, line1, line2 From Address", connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Connected to database successfully.", "Succeed",
                                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Failed",
                                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }  
            }
        }

        private void updateSettings()
        {
            Properties.Settings.Default.dbConnection = comboBoxServer.Text;
            Properties.Settings.Default.dbUsername   = textBoxUsername.Text;
            Properties.Settings.Default.dbPassword   = textBoxPassword.Text;
            Properties.Settings.Default.dbName       = databaseList.Text;
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void databaseList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboServer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
