using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MBReport
{
    public partial class MBReport : Form
    {
        public MBReport()
        {
            InitializeComponent();
            populateCreditOfficers();
            if (this.creditOfficerComboBox.SelectedIndex != -1)
            {
                populateVillages(this.creditOfficerComboBox.SelectedValue.ToString());
            }
            populateStatus();
        }

        private void populateStatus()
        {
            this.statusComboBox.Items.AddRange(new object[] { "Due", "All" });
            this.statusComboBox.SelectedIndex = 0;
        }

        private void populateCreditOfficers()
        {
            try
            {
                List<Entry> officers = Database.GetCreditOfficers();

                this.creditOfficerComboBox.DataSource = officers;
                this.creditOfficerComboBox.DisplayMember = "Name";
                this.creditOfficerComboBox.ValueMember = "Cid";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            } 
        }

        private void populateVillages(string cid)
        {
            List<Entry> villages = Database.GetVillages(cid);

            this.villageComboBox.DataSource = villages;
            this.villageComboBox.DisplayMember = "Name";
            this.villageComboBox.ValueMember = "Cid";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Show tree window of officers and villages
            //Form creditOfficersViewer = new CreditOfficer();
            //creditOfficersViewer.ShowDialog(this);

            InstallmentReportForm installmentReportForm = new InstallmentReportForm();

            installmentReportForm.ShowDialog(this);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form settingsScreen = new SettingsScreen();
            settingsScreen.ShowDialog(this);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.creditOfficerComboBox.SelectedIndex != -1)
            {
                populateVillages(this.creditOfficerComboBox.SelectedValue.ToString());
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        public string Status
        {
            get
            {
                if (this.statusComboBox.SelectedIndex != -1)
                {
                    return this.statusComboBox.SelectedItem.ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        public string Cid
        {
            get
            {
                if (this.villageComboBox.SelectedIndex != -1)
                {
                    return this.villageComboBox.SelectedValue.ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        public string VillageName
        {
            get 
            {
                if (this.creditOfficerComboBox.SelectedIndex != -1)
                {
                    return this.creditOfficerComboBox.Text.Substring(6);
                }
                else
                {
                    return "";
                }
            }
        }

        public string OfficerName
        {
            get
            {
                if (this.villageComboBox.SelectedIndex != -1)
                {
                    return this.villageComboBox.Text.Substring(6);
                }
                else
                {
                    return "";
                }
            }
        }

        public DateTime CollectionDate
        {
            //get{return this.collectionDate.Value.ToString("yyyy-MM-dd");}

            get { return this.collectionDatePicker.Value; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void refresh_Click(object sender, EventArgs e)
        {
            populateCreditOfficers();
            if (this.creditOfficerComboBox.SelectedIndex != -1)
            {
                populateVillages(this.creditOfficerComboBox.SelectedValue.ToString());
            }
        }
    }
}
