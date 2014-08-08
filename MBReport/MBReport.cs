using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MBReport
{
    public partial class MBReport : Form
    {
        public MBReport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form reportViewer = new ReportViewer();
            //reportViewer.ShowDialog(this);
            Form creditOfficersViewer = new CreditOfficer();
            creditOfficersViewer.ShowDialog(this);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form settingsScreen = new SettingsScreen();
            settingsScreen.ShowDialog(this);
        }
    }
}
