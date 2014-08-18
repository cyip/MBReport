namespace MBReport
{
    partial class MBReport
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creditOfficerComboBox = new System.Windows.Forms.ComboBox();
            this.villageComboBox = new System.Windows.Forms.ComboBox();
            this.refresh = new System.Windows.Forms.Button();
            this.collectionDatePicker = new System.Windows.Forms.DateTimePicker();
            this.creditOfficerLabel = new System.Windows.Forms.Label();
            this.village = new System.Windows.Forms.Label();
            this.collectionDate = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(96, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(450, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // creditOfficerComboBox
            // 
            this.creditOfficerComboBox.FormattingEnabled = true;
            this.creditOfficerComboBox.Location = new System.Drawing.Point(15, 57);
            this.creditOfficerComboBox.Name = "creditOfficerComboBox";
            this.creditOfficerComboBox.Size = new System.Drawing.Size(201, 21);
            this.creditOfficerComboBox.TabIndex = 3;
            this.creditOfficerComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // villageComboBox
            // 
            this.villageComboBox.FormattingEnabled = true;
            this.villageComboBox.Location = new System.Drawing.Point(232, 57);
            this.villageComboBox.Name = "villageComboBox";
            this.villageComboBox.Size = new System.Drawing.Size(201, 21);
            this.villageComboBox.TabIndex = 4;
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(233, 165);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(109, 23);
            this.refresh.TabIndex = 5;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // collectionDatePicker
            // 
            this.collectionDatePicker.Location = new System.Drawing.Point(233, 111);
            this.collectionDatePicker.Name = "collectionDatePicker";
            this.collectionDatePicker.Size = new System.Drawing.Size(200, 20);
            this.collectionDatePicker.TabIndex = 6;
            this.collectionDatePicker.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // creditOfficerLabel
            // 
            this.creditOfficerLabel.AutoSize = true;
            this.creditOfficerLabel.Location = new System.Drawing.Point(12, 38);
            this.creditOfficerLabel.Name = "creditOfficerLabel";
            this.creditOfficerLabel.Size = new System.Drawing.Size(68, 13);
            this.creditOfficerLabel.TabIndex = 7;
            this.creditOfficerLabel.Text = "Credit Officer";
            this.creditOfficerLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // village
            // 
            this.village.AutoSize = true;
            this.village.Location = new System.Drawing.Point(229, 38);
            this.village.Name = "village";
            this.village.Size = new System.Drawing.Size(38, 13);
            this.village.TabIndex = 8;
            this.village.Text = "Village";
            // 
            // collectionDate
            // 
            this.collectionDate.AutoSize = true;
            this.collectionDate.Location = new System.Drawing.Point(230, 95);
            this.collectionDate.Name = "collectionDate";
            this.collectionDate.Size = new System.Drawing.Size(79, 13);
            this.collectionDate.TabIndex = 9;
            this.collectionDate.Text = "Collection Date";
            // 
            // MBReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 209);
            this.Controls.Add(this.collectionDate);
            this.Controls.Add(this.village);
            this.Controls.Add(this.creditOfficerLabel);
            this.Controls.Add(this.collectionDatePicker);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.villageComboBox);
            this.Controls.Add(this.creditOfficerComboBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MBReport";
            this.Text = "MBReport";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ComboBox creditOfficerComboBox;
        private System.Windows.Forms.ComboBox villageComboBox;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.DateTimePicker collectionDatePicker;
        private System.Windows.Forms.Label creditOfficerLabel;
        private System.Windows.Forms.Label village;
        private System.Windows.Forms.Label collectionDate;
    }
}

