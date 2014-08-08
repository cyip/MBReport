namespace MBReport
{
    partial class SettingsScreen
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.checkBoxSecurity = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.databaseList = new System.Windows.Forms.ComboBox();
            this.comboBoxServer = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(198, 219);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(71, 219);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 1;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // checkBoxSecurity
            // 
            this.checkBoxSecurity.AutoSize = true;
            this.checkBoxSecurity.Location = new System.Drawing.Point(33, 81);
            this.checkBoxSecurity.Name = "checkBoxSecurity";
            this.checkBoxSecurity.Size = new System.Drawing.Size(135, 17);
            this.checkBoxSecurity.TabIndex = 2;
            this.checkBoxSecurity.Text = "Use Integrated security";
            this.checkBoxSecurity.UseVisualStyleBackColor = true;
            this.checkBoxSecurity.CheckedChanged += new System.EventHandler(this.checkBoxSecurity_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter server name";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(45, 104);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(55, 13);
            this.labelUsername.TabIndex = 5;
            this.labelUsername.Text = "Username";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(47, 130);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 6;
            this.labelPassword.Text = "Password";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(106, 101);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(204, 20);
            this.textBoxUsername.TabIndex = 7;
            this.textBoxUsername.TextChanged += new System.EventHandler(this.textBoxUsername_TextChanged);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(106, 127);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(204, 20);
            this.textBoxPassword.TabIndex = 8;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Name of database";
            // 
            // databaseList
            // 
            this.databaseList.FormattingEnabled = true;
            this.databaseList.Location = new System.Drawing.Point(130, 161);
            this.databaseList.Name = "databaseList";
            this.databaseList.Size = new System.Drawing.Size(180, 21);
            this.databaseList.TabIndex = 10;
            this.databaseList.SelectedIndexChanged += new System.EventHandler(this.databaseList_SelectedIndexChanged);
            // 
            // comboBoxServer
            // 
            this.comboBoxServer.FormattingEnabled = true;
            this.comboBoxServer.Location = new System.Drawing.Point(129, 44);
            this.comboBoxServer.Name = "comboBoxServer";
            this.comboBoxServer.Size = new System.Drawing.Size(180, 21);
            this.comboBoxServer.TabIndex = 11;
            this.comboBoxServer.SelectedIndexChanged += new System.EventHandler(this.comboServer_SelectedIndexChanged);
            // 
            // SettingsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 266);
            this.Controls.Add(this.comboBoxServer);
            this.Controls.Add(this.databaseList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxSecurity);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.buttonSave);
            this.Name = "SettingsScreen";
            this.Text = "Database Connection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.CheckBox checkBoxSecurity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox databaseList;
        private System.Windows.Forms.ComboBox comboBoxServer;
    }
}