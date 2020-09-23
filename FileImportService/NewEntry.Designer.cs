namespace FileImportService
{
    partial class NewEntry
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
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtDBUName = new System.Windows.Forms.TextBox();
            this.txtDBPass = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.txtFolderBU = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSproc = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtFolderFail = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblNewEntryTtitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveNewEntry = new System.Windows.Forms.Button();
            this.btnCancelNewEntry = new System.Windows.Forms.Button();
            this.chkBxEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvFileImpsvc = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnAssocFile = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileImpsvc)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(58, 30);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(100, 20);
            this.txtDatabase.TabIndex = 0;
            // 
            // txtDBUName
            // 
            this.txtDBUName.Location = new System.Drawing.Point(58, 83);
            this.txtDBUName.Name = "txtDBUName";
            this.txtDBUName.Size = new System.Drawing.Size(100, 20);
            this.txtDBUName.TabIndex = 1;
            // 
            // txtDBPass
            // 
            this.txtDBPass.Location = new System.Drawing.Point(58, 132);
            this.txtDBPass.Name = "txtDBPass";
            this.txtDBPass.Size = new System.Drawing.Size(100, 20);
            this.txtDBPass.TabIndex = 2;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(44, 33);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 0;
            // 
            // txtSQL
            // 
            this.txtSQL.Location = new System.Drawing.Point(18, 30);
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new System.Drawing.Size(122, 20);
            this.txtSQL.TabIndex = 0;
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(18, 33);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(122, 20);
            this.txtFolder.TabIndex = 9;
            //this.txtFolder.TextChanged += new System.EventHandler(this.TxtFolder_TextChanged);
            // 
            // txtFolderBU
            // 
            this.txtFolderBU.Location = new System.Drawing.Point(18, 83);
            this.txtFolderBU.Name = "txtFolderBU";
            this.txtFolderBU.Size = new System.Drawing.Size(122, 20);
            this.txtFolderBU.TabIndex = 10;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(58, 33);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 20);
            this.textBox9.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDBPass);
            this.groupBox1.Controls.Add(this.txtDBUName);
            this.groupBox1.Controls.Add(this.txtDatabase);
            this.groupBox1.Location = new System.Drawing.Point(33, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 181);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox9);
            this.groupBox2.Location = new System.Drawing.Point(33, 254);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 164);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSproc);
            this.groupBox3.Controls.Add(this.txtSQL);
            this.groupBox3.Location = new System.Drawing.Point(570, 60);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(186, 152);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Command";
            // 
            // txtSproc
            // 
            this.txtSproc.Location = new System.Drawing.Point(18, 83);
            this.txtSproc.Name = "txtSproc";
            this.txtSproc.Size = new System.Drawing.Size(122, 20);
            this.txtSproc.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.txtFolderFail);
            this.groupBox4.Controls.Add(this.txtFolderBU);
            this.groupBox4.Controls.Add(this.txtFolder);
            this.groupBox4.Location = new System.Drawing.Point(570, 254);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(186, 164);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Folders";
            // 
            // txtFolderFail
            // 
            this.txtFolderFail.Location = new System.Drawing.Point(18, 126);
            this.txtFolderFail.Name = "txtFolderFail";
            this.txtFolderFail.Size = new System.Drawing.Size(122, 20);
            this.txtFolderFail.TabIndex = 11;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox4);
            this.groupBox5.Location = new System.Drawing.Point(346, 254);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(191, 94);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "File Types";
            // 
            // lblNewEntryTtitle
            // 
            this.lblNewEntryTtitle.Location = new System.Drawing.Point(33, 23);
            this.lblNewEntryTtitle.Name = "lblNewEntryTtitle";
            this.lblNewEntryTtitle.Size = new System.Drawing.Size(504, 20);
            this.lblNewEntryTtitle.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Name";
            // 
            // btnSaveNewEntry
            // 
            this.btnSaveNewEntry.Location = new System.Drawing.Point(668, 440);
            this.btnSaveNewEntry.Name = "btnSaveNewEntry";
            this.btnSaveNewEntry.Size = new System.Drawing.Size(75, 23);
            this.btnSaveNewEntry.TabIndex = 9;
            this.btnSaveNewEntry.Text = "Save";
            this.btnSaveNewEntry.UseVisualStyleBackColor = true;
            this.btnSaveNewEntry.Click += new System.EventHandler(this.BtnSaveNewEntry_Click);
            // 
            // btnCancelNewEntry
            // 
            this.btnCancelNewEntry.Location = new System.Drawing.Point(570, 440);
            this.btnCancelNewEntry.Name = "btnCancelNewEntry";
            this.btnCancelNewEntry.Size = new System.Drawing.Size(75, 23);
            this.btnCancelNewEntry.TabIndex = 8;
            this.btnCancelNewEntry.Text = "Cancel";
            this.btnCancelNewEntry.UseVisualStyleBackColor = true;
            this.btnCancelNewEntry.Click += new System.EventHandler(this.BtnCancelNewEntry_Click);
            // 
            // chkBxEnabled
            // 
            this.chkBxEnabled.AutoSize = true;
            this.chkBxEnabled.Location = new System.Drawing.Point(570, 23);
            this.chkBxEnabled.Name = "chkBxEnabled";
            this.chkBxEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkBxEnabled.TabIndex = 16;
            this.chkBxEnabled.Text = "Enabled";
            this.chkBxEnabled.UseVisualStyleBackColor = true;
            //this.chkBxEnabled.CheckedChanged += new System.EventHandler(this.ChkBxEnabled_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgvFileImpsvc);
            this.groupBox6.Location = new System.Drawing.Point(12, 480);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(525, 249);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "FileMappings";
            // 
            // dgvFileImpsvc
            // 
            this.dgvFileImpsvc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFileImpsvc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvFileImpsvc.Location = new System.Drawing.Point(21, 19);
            this.dgvFileImpsvc.Name = "dgvFileImpsvc";
            this.dgvFileImpsvc.Size = new System.Drawing.Size(480, 213);
            this.dgvFileImpsvc.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Col Num";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Col Name";
            this.Column2.Name = "Column2";
            this.Column2.Width = 215;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Col DataType";
            this.Column3.Items.AddRange(new object[] {
            "Varchar",
            "Int",
            "Date",
            "DateTime",
            "Bool"});
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column3.Width = 120;
            // 
            // btnAssocFile
            // 
            this.btnAssocFile.Location = new System.Drawing.Point(570, 499);
            this.btnAssocFile.Name = "btnAssocFile";
            this.btnAssocFile.Size = new System.Drawing.Size(75, 36);
            this.btnAssocFile.TabIndex = 10;
            this.btnAssocFile.Text = "Associate File";
            this.btnAssocFile.UseVisualStyleBackColor = true;
            this.btnAssocFile.Click += new System.EventHandler(this.BtnAssocFile_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(146, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // NewEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 741);
            this.ControlBox = false;
            this.Controls.Add(this.btnAssocFile);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.chkBxEnabled);
            this.Controls.Add(this.btnCancelNewEntry);
            this.Controls.Add(this.btnSaveNewEntry);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNewEntryTtitle);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "NewEntry";
            this.Text = "File Import Service";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewEntry_FormClosing);
            //this.Load += new System.EventHandler(this.NewEntry_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileImpsvc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtDBUName;
        private System.Windows.Forms.TextBox txtDBPass;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.TextBox txtFolderBU;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox lblNewEntryTtitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolderFail;
        private System.Windows.Forms.Button btnSaveNewEntry;
        private System.Windows.Forms.Button btnCancelNewEntry;
        private System.Windows.Forms.TextBox txtSproc;
        private System.Windows.Forms.CheckBox chkBxEnabled;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgvFileImpsvc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column3;
        private System.Windows.Forms.Button btnAssocFile;
        private System.Windows.Forms.Button button1;
    }
}

