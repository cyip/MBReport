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
using System.Globalization;

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
                    string sqlCommandOverdueStr =
                                       "Select r.cid, RTRIM(ISNULL(c.Name1,'')) + ' ' + RTRIM(ISNULL(c.Name2,'')) + ' ' + RTRIM(ISNULL(c.Name3,'')) + ' ' + RTRIM(ISNULL(c.Name4,'')) as Name, l.acc + '-' + l.chd as Account, " +
                                      "parsename(convert(varchar(100), cast(l.BalAmt as money), 1), 2) as 'Principle Balance', " +
                                      "parsename(convert(varchar(100), cast((l.ODuePriAmt + SUM(coalesce(i.PriAmt, 0))) as money), 1), 2) as 'Principle Due', " +
                                      "parsename(convert(varchar(100), cast(l.AcrIntAmt as money), 1), 2) as 'Interest Due', l.ODuePriAmt as 'Prepaid', " +
                                      "0 as 'Total Due', " +
                                      "0 as 'Saving Amount', " +
                                      "lookup.FullDesc as 'Status', " +
                                      "l.Tracc + '-' + TrChd as 'Saving Account', " +
                                      "case when l.OduePriAmt > 0 then 'overdue' else Convert(varchar(10),CONVERT(date,min(i.DueDate),106),103) end as 'Due Date' " +
                                      "from relacc as r " +
                                      "inner join CIF as c on r.CID=c.CID " +
                                      "inner join lnacc as l on l.acc=r.ACC " +
                                      "inner join address as a on a.cid=r.CID " +
                                      "inner join RELCID as rc on r.cid = rc.RelatedCID " +
                                      "left outer join lninst as i on i.acc = l.acc and i.DueDate between (select DATEADD(dd, 1, CurrRunDate) from brparms) and @collectionDate " +
                                      "inner join lookup lookup on lookup.LookUpCode = '4' + l.AccStatus " +
                                      "where rc.cid = @cid and (l.AccStatus > '01' and l.AccStatus < '99') and " +
                                      "lookup.lookupid = 'AS' and lookup.lookupcode like '4%' and lookup.langtype = '001' ";

                                      
                    sqlCommandOverdueStr += "group by r.CID, c.Name1, c.name2, c.Name3, c.Name4, l.Acc, l.chd, l.BalAmt, l.OduePriAmt, l.AcrIntAmt, l.AccStatus, lookup.FullDesc, l.Tracc, l.TrChd";

                    MBReport parent = (MBReport)(this.Owner);
                    SqlCommand sqlCommandOverdue = new SqlCommand(sqlCommandOverdueStr, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommandOverdue);
                    

                    sqlCommandOverdue.Parameters.Add("@cid", parent.Cid);
                    sqlCommandOverdue.Parameters.Add("@collectionDate", DateTime.Parse(parent.CollectionDate.ToString()));

                    InstallmentTable dataset = new InstallmentTable();
                    adapter.Fill(dataset, "Installments");

                    int principleBalanceSum = 0;
                    int principleDueSum = 0;
                    int interestDueSum = 0;
                    int savingAmountSum = 0;
                    int totalDueSum = 0;
                    foreach(DataRow installment in dataset.Tables["Installments"].Rows)
                    {
                        if (parent.Status == "Due" && installment["Due Date"] == DBNull.Value)
                        {
                            installment.Delete();
                            continue;
                        }

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
                        //installment["Interest Due"] = Convert.ToInt32(installment["Interest Due"].ToString()) + interest;
                        installment["Interest Due"] = int.Parse(installment["Interest Due"].ToString(), NumberStyles.Number) + interest;

                        // Update Total due = principle due + interest due
                        // If principle due  < 0 (prepaid) do not include that in Total Due.
                        // Prepaid amount only covers priciple due, not interest due.
                        int principleDue = int.Parse(installment["Principle Due"].ToString(), NumberStyles.Number);
                        installment["Total Due"] = int.Parse(installment["Interest Due"].ToString(), NumberStyles.Number);
                        if (principleDue > 0)
                        {
                            installment["Total Due"] = int.Parse(installment["Interest Due"].ToString(), NumberStyles.Number) + principleDue;
                        }

                        //Change account number to proper format
                        // - “xxx-xxxxxx-xx-x” Ex. “771-001408-08-4”
                        accountId = accountId.Insert(3, "-");
                        accountId = accountId.Insert(10, "-");
                        installment["Account"] = accountId;

                        //Get saving account if there is no associated recovery account 
                        // If it has Recovery Account, shows account id. should starts with 111.
                        // If Recovery account is empty, shows account id starts with 112.
                        // Show 111-xxxxxx-xx-x if loan has link with recovery saving (For KHR database)
                        // Show 112-xxxxxx-xx-x if loan has no link with recovery saving(For KHR database)
                        // Show 221-xxxxxx-xx-x if loan has link with recovery saving (For USD database)
                        // Show 222-xxxxxx-xx-x if loan has no link with recovery saving(For USD database)
                        //Also sums up balance across all saving accounts
                        string savingAccountId, savingAmount;
                        Database.SavingAccount(installment["cid"].ToString(),
                                               out savingAccountId,
                                               out savingAmount);
                        installment["Saving Amount"] = savingAmount;
                        if (String.IsNullOrEmpty(installment["Saving Account"].ToString()))
                        {
                            installment["Saving Account"] = savingAccountId;
                        }

                        //Change saving account to proper format
                        savingAccountId = installment["Saving Account"].ToString();
                        if(!String.IsNullOrEmpty(savingAccountId))
                        {
                            savingAccountId = savingAccountId.Insert(3, "-");
                            savingAccountId = savingAccountId.Insert(10, "-");
                            
                            installment["Saving Account"] = savingAccountId;
                        }

                        //Divide all dollar amounts if it's USD database
                        if (Database.Currency() == "USD")
                        {
                            installment["Principle Balance"] = int.Parse(installment["Principle Balance"].ToString(), NumberStyles.Number) / 10;
                            installment["Principle Due"] = int.Parse(installment["Principle Due"].ToString(), NumberStyles.Number) / 10;
                            installment["Interest Due"] = int.Parse(installment["Interest Due"].ToString(), NumberStyles.Number) / 10;
                            installment["Prepaid"] = int.Parse(installment["Prepaid"].ToString(), NumberStyles.Number) / 10;
                            installment["Total Due"] = int.Parse(installment["Total Due"].ToString(), NumberStyles.Number) / 10;
                            installment["Saving Amount"] = int.Parse(installment["Saving Amount"].ToString(), NumberStyles.Number) / 10;
                        }

                        // Calculate sum of amounts
                        principleBalanceSum += int.Parse(installment["Principle Balance"].ToString(), NumberStyles.Number);
                        principleDueSum += int.Parse(installment["Principle Due"].ToString(), NumberStyles.Number);
                        interestDueSum += int.Parse(installment["Interest Due"].ToString(), NumberStyles.Number);
                        savingAmountSum += int.Parse(installment["Saving Amount"].ToString(), NumberStyles.Number);
                        totalDueSum += int.Parse(installment["Total Due"].ToString(), NumberStyles.Number);

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

                    // Update report footer
                    TextObject principleBalanceSumText = (TextObject)CustomerReport.ReportDefinition.Sections["Section4"].ReportObjects["PrincipleBalanceSum"];
                    principleBalanceSumText.Text = Convert.ToString(principleBalanceSum);
                    TextObject principleDueSumText = (TextObject)CustomerReport.ReportDefinition.Sections["Section4"].ReportObjects["PrincipleDueSum"];
                    principleDueSumText.Text = Convert.ToString(principleDueSum);
                    TextObject interestDueSumText = (TextObject)CustomerReport.ReportDefinition.Sections["Section4"].ReportObjects["InterestDueSum"];
                    interestDueSumText.Text = Convert.ToString(interestDueSum);
                    TextObject savingAmountSumText = (TextObject)CustomerReport.ReportDefinition.Sections["Section4"].ReportObjects["SavingAmountSum"];
                    savingAmountSumText.Text = Convert.ToString(savingAmountSum);
                    TextObject totalDueSumText = (TextObject)CustomerReport.ReportDefinition.Sections["Section4"].ReportObjects["TotalDueSum"];
                    totalDueSumText.Text = Convert.ToString(totalDueSum);

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
                    TextObject title = (TextObject)CustomerReport.ReportDefinition.Sections["Section1"].ReportObjects["Title"];
                    title.Text = parent.Status + " Collection Report";

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
