namespace MBReport
{
    partial class CreditOfficer
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
            this.treeViewOfficers = new System.Windows.Forms.TreeView();
            this.Report = new System.Windows.Forms.Button();
            this.collectionDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeViewOfficers
            // 
            this.treeViewOfficers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewOfficers.Location = new System.Drawing.Point(-2, -3);
            this.treeViewOfficers.Name = "treeViewOfficers";
            this.treeViewOfficers.Size = new System.Drawing.Size(394, 327);
            this.treeViewOfficers.TabIndex = 0;
            this.treeViewOfficers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewOfficers_AfterSelect);
            // 
            // Report
            // 
            this.Report.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Report.Location = new System.Drawing.Point(306, 330);
            this.Report.Name = "Report";
            this.Report.Size = new System.Drawing.Size(75, 23);
            this.Report.TabIndex = 1;
            this.Report.Text = "Report";
            this.Report.UseVisualStyleBackColor = true;
            this.Report.Click += new System.EventHandler(this.button1_Click);
            // 
            // collectionDate
            // 
            this.collectionDate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.collectionDate.Location = new System.Drawing.Point(100, 331);
            this.collectionDate.Name = "collectionDate";
            this.collectionDate.Size = new System.Drawing.Size(200, 20);
            this.collectionDate.TabIndex = 3;
            this.collectionDate.ValueChanged += new System.EventHandler(this.dateTimePickerTo_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 335);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Collection Date:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // CreditOfficer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 362);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.collectionDate);
            this.Controls.Add(this.Report);
            this.Controls.Add(this.treeViewOfficers);
            this.Name = "CreditOfficer";
            this.Text = "CreditOfficer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewOfficers;
        private System.Windows.Forms.Button Report;
        private System.Windows.Forms.DateTimePicker collectionDate;
        private System.Windows.Forms.Label label2;
    }
}