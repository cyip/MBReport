namespace MBReport
{
    partial class InstallmentReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.installmentReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            //this.InstallmentReport1 = new MBReport.InstallmentReport();
            this.InstallmentReport1 = new InstallmentReport();
            this.SuspendLayout();
            // 
            // installmentReportViewer
            // 
            this.installmentReportViewer.ActiveViewIndex = 0;
            this.installmentReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.installmentReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.installmentReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.installmentReportViewer.Location = new System.Drawing.Point(0, 0);
            this.installmentReportViewer.Name = "installmentReportViewer";
            this.installmentReportViewer.ReportSource = this.InstallmentReport1;
            this.installmentReportViewer.Size = new System.Drawing.Size(1338, 493);
            this.installmentReportViewer.TabIndex = 0;
            this.installmentReportViewer.Load += new System.EventHandler(this.installmentReportViewer_Load);
            // 
            // InstallmentReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 493);
            this.Controls.Add(this.installmentReportViewer);
            this.Name = "InstallmentReportForm";
            this.Text = "Installment Report";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer installmentReportViewer;
        private InstallmentReport InstallmentReport1;

    }
}