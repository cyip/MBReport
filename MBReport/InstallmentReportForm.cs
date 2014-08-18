using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

namespace MBReport
{
    public partial class InstallmentReportForm : Form
    {
        public InstallmentReportForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.installmentReportViewer.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void populateReportHeader()
        {
            MBReport parent = (MBReport)this.Owner;
            TextObject collectionDate = (TextObject)this.InstallmentReport1.ReportDefinition.Sections["Section1"].ReportObjects["CollectionDate"];
            //collectionDate.Text = parent.CollectionDate.ToString();
            collectionDate.Text = "ABC";
        }


        private void installmentReportViewer_Load(object sender, EventArgs e)
        {
            

            using (SqlConnection connection = new SqlConnection(CreditOfficer.ConstructSqlConnectionString()))
            {
                try
                {
                    connection.Open();
                    /*
                    SqlCommand sqlCommand =
                        new SqlCommand("Select r.cid, RTRIM(c.Name1) + ' ' + RTRIM(c.Name2) as Name, l.acc as Account, " +
                                       "i.BFBalAmt as 'Principle Balance', i.OrigPriAmt as 'Principle Due', " +
                                       "i.IntAmt as 'Interest Due', i.ODuePriAmt as 'Prepaid', " +
                                       "Convert(varchar(10),CONVERT(date,i.DueDate,106),103) as 'Due Date'" +
                                       "from relacc as r " +
                                       "inner join CIF as c on r.CID=c.CID " +
                                       "inner join lnacc as l on l.acc=r.ACC " +
                                       "inner join lninst as i on l.Acc = i.Acc " +
                                       "inner join address as a on a.cid=r.CID " +
                                       "inner join RELCID as rc on r.cid = rc.RelatedCID " +
                                       "where rc.cid = @cid and (l.AccStatus > '01' and l.AccStatus < '99') " +
                                       "and i.PaidDate is NULL and i.DueDate <= @collectonDate and l.OduePriAmt <= 0 ",
                                       connection);
                     */
                     
                     SqlCommand sqlCommandOverdue =
                        new SqlCommand("Select r.cid, RTRIM(c.Name1) + ' ' + RTRIM(c.Name2) as Name, l.acc + '-' + l.chd as Account, " +
                                       "l.BalAmt as 'Principle Balance', l.ODuePriAmt as 'Principle Due', " +
                                       "l.AcrIntAmt as 'Interest Due', l.ODuePriAmt as 'Prepaid', " +
                                       "0 as 'Total Due', " +
                                       "lookup.FullDesc as 'Status', " +
                                       "Convert(varchar(10),CONVERT(date,min(i.DueDate),106),103) as 'Due Date' " +
                                       "from relacc as r " +
                                       "inner join CIF as c on r.CID=c.CID " +
                                       "inner join lnacc as l on l.acc=r.ACC " +
                                       "inner join address as a on a.cid=r.CID " +
                                       "inner join RELCID as rc on r.cid = rc.RelatedCID " +
                                       "inner join lninst as i on i.acc = l.acc " +
                                       "inner join lookup lookup on lookup.LookUpCode = '4' + l.AccStatus " +
                                       "where rc.cid = @cid and (l.AccStatus > '01' and l.AccStatus < '99') and " +
                                       "lookup.lookupid = 'AS' and lookup.lookupcode like '4%' and lookup.langtype = '001' " +
                                       "and l.OduePriAmt > 0 " +
                                       "group by r.CID, c.Name1, name2, l.Acc, l.chd, l.BalAmt, l.OduePriAmt, l.AcrIntAmt, l.AccStatus, lookup.FullDesc",
                                       connection);
                     SqlDataAdapter adapter = new SqlDataAdapter(sqlCommandOverdue);
                    MBReport parent = (MBReport)(this.Owner);

                    sqlCommandOverdue.Parameters.Add("@cid", parent.Cid);
                    //sqlCommand.Parameters.Add("@collectionDate", DateTime.Parse(parent.CollectionDate.ToString());

                    InstallmentTable dataset = new InstallmentTable();
                    adapter.Fill(dataset, "Installments");
       
                    
                    foreach(DataRow installment in dataset.Tables["Installments"].Rows)
                    {
                        //Calculate interest due up to date
                        // - Iterate each installment row and calculate up to date interest with stored procedure
                        string accountId = installment["Account"].ToString();
                        SqlCommand calcInterestCmd =
                            new SqlCommand("sp_LNCalcInterest", connection);
                        calcInterestCmd.CommandType = CommandType.StoredProcedure;

                        calcInterestCmd.Parameters.AddWithValue("@AAcc", accountId);
                        int numDays = (parent.CollectionDate - Database.CurrentRunDate()).Days;
                        calcInterestCmd.Parameters.AddWithValue("@FutureDays", numDays);
                        calcInterestCmd.Parameters.AddWithValue("@IntAmt", 0);
                        calcInterestCmd.Parameters["@IntAmt"].Direction = ParameterDirection.Output;
                        calcInterestCmd.Parameters.AddWithValue("@PenAmt", 0);
                        calcInterestCmd.Parameters.AddWithValue("@IntODueAmt", 0);
                        calcInterestCmd.Parameters["@IntODueAmt"].Direction = ParameterDirection.Output;
                        calcInterestCmd.Parameters.AddWithValue("@TaxAmt", 0);
                        calcInterestCmd.Parameters.AddWithValue("@TrnChgAmt", 0);
                        calcInterestCmd.ExecuteNonQuery();

                        Int32 interest = (Int32)calcInterestCmd.Parameters["@IntAmt"].Value;
                        installment["Interest Due"] = Convert.ToInt32(installment["Interest Due"].ToString()) + interest;

                        // Update Total due = principle due + interest due
                        installment["Total Due"] = Convert.ToInt32(installment["Principle Due"].ToString()) + 
                                                   Convert.ToInt32(installment["Interest Due"].ToString());

                        //Change account number to proper format
                        // - “xxx-xxxxxx-xx-x” Ex. “771-001408-08-4”
                        accountId = accountId.Insert(3, "-");
                        accountId = accountId.Insert(10, "-");
                        installment["Account"] = accountId;
                    }
                    dataset.Tables["installments"].AcceptChanges();

                    ReportDocument CustomerReport = new ReportDocument();
                    //CustomerReport.Load(@"C:\Documents and Settings\User\My Documents\Development\MBReport\MBReport\InstallmentReport.rpt");
                    //CustomerReport.Load(Directory.GetCurrentDirectory() + @"\InstallmentReport.rpt");
                    CustomerReport.Load(Application.StartupPath + @"\InstallmentReport.rpt");
                    CustomerReport.SetDataSource(dataset.Tables["Installments"]);

                    connection.Close();

                    //Form installmentReportForm = new InstallmentReportForm();
                    this.installmentReportViewer.ReportSource = CustomerReport;
                    this.installmentReportViewer.Refresh();


                    // Populate Header Text Fields
                    // i.e. loan officer, village, collection date
                    TextObject collectionDate = (TextObject)CustomerReport.ReportDefinition.Sections["Section1"].ReportObjects["CollectionDate"];
                    collectionDate.Text = parent.CollectionDate.ToString("yyyy-MM-dd");
                    TextObject officerName = (TextObject)CustomerReport.ReportDefinition.Sections["Section1"].ReportObjects["Officer"];
                    officerName.Text = parent.OfficerName;
                    TextObject villageName = (TextObject)CustomerReport.ReportDefinition.Sections["Section1"].ReportObjects["Village"];
                    villageName.Text = parent.VillageName;
                    TextObject currency = (TextObject)CustomerReport.ReportDefinition.Sections["Section1"].ReportObjects["Currency"];
                    currency.Text = Database.Currency();

                }
                finally
                {
                    connection.Close();
                }
            }

            this.populateReportHeader();
        }
    }
}
