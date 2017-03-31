namespace FunctionTestBoxTool
{
    partial class frmSetting
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSaveSequence = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstOldSequence = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstNewSequnce = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checklistTestItem = new System.Windows.Forms.CheckedListBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtUSB30ResultFile = new System.Windows.Forms.TextBox();
            this.txtUSB30ToolPath = new System.Windows.Forms.TextBox();
            this.txtHDMIResultFile = new System.Windows.Forms.TextBox();
            this.txtHDMIToolPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBarcodeLength = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBarcodeStart = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSaveSequence);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(22, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 188);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EditTestSequence";
            // 
            // btnSaveSequence
            // 
            this.btnSaveSequence.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveSequence.Location = new System.Drawing.Point(136, 138);
            this.btnSaveSequence.Name = "btnSaveSequence";
            this.btnSaveSequence.Size = new System.Drawing.Size(75, 31);
            this.btnSaveSequence.TabIndex = 6;
            this.btnSaveSequence.Text = "Save";
            this.btnSaveSequence.UseVisualStyleBackColor = true;
            this.btnSaveSequence.Click += new System.EventHandler(this.btnSaveSequence_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.Location = new System.Drawing.Point(136, 104);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 31);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstOldSequence);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(6, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(124, 162);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "OldSequence";
            // 
            // lstOldSequence
            // 
            this.lstOldSequence.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstOldSequence.FormattingEnabled = true;
            this.lstOldSequence.ItemHeight = 17;
            this.lstOldSequence.Location = new System.Drawing.Point(6, 20);
            this.lstOldSequence.Name = "lstOldSequence";
            this.lstOldSequence.Size = new System.Drawing.Size(109, 123);
            this.lstOldSequence.TabIndex = 0;
            this.lstOldSequence.DoubleClick += new System.EventHandler(this.lstOldSequence_DoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstNewSequnce);
            this.groupBox2.Location = new System.Drawing.Point(217, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(115, 162);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "NewSequnce";
            // 
            // lstNewSequnce
            // 
            this.lstNewSequnce.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstNewSequnce.FormattingEnabled = true;
            this.lstNewSequnce.ItemHeight = 17;
            this.lstNewSequnce.Location = new System.Drawing.Point(6, 20);
            this.lstNewSequnce.Name = "lstNewSequnce";
            this.lstNewSequnce.Size = new System.Drawing.Size(102, 123);
            this.lstNewSequnce.TabIndex = 1;
            this.lstNewSequnce.DoubleClick += new System.EventHandler(this.lstNewSequnce_DoubleClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.Location = new System.Drawing.Point(136, 70);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 31);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "<-";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.Location = new System.Drawing.Point(136, 39);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 31);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "->";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(371, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(211, 188);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "EditTestItems";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checklistTestItem);
            this.groupBox5.Location = new System.Drawing.Point(6, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(196, 162);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            // 
            // checklistTestItem
            // 
            this.checklistTestItem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checklistTestItem.FormattingEnabled = true;
            this.checklistTestItem.Location = new System.Drawing.Point(6, 20);
            this.checklistTestItem.Name = "checklistTestItem";
            this.checklistTestItem.Size = new System.Drawing.Size(184, 130);
            this.checklistTestItem.TabIndex = 0;
            this.checklistTestItem.Click += new System.EventHandler(this.checklistTestItem_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtUSB30ResultFile);
            this.groupBox6.Controls.Add(this.txtUSB30ToolPath);
            this.groupBox6.Controls.Add(this.txtHDMIResultFile);
            this.groupBox6.Controls.Add(this.txtHDMIToolPath);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.Location = new System.Drawing.Point(21, 204);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(679, 146);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Tool_ResutFile Path";
            // 
            // txtUSB30ResultFile
            // 
            this.txtUSB30ResultFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUSB30ResultFile.Location = new System.Drawing.Point(120, 116);
            this.txtUSB30ResultFile.Name = "txtUSB30ResultFile";
            this.txtUSB30ResultFile.Size = new System.Drawing.Size(538, 23);
            this.txtUSB30ResultFile.TabIndex = 7;
            this.txtUSB30ResultFile.TextChanged += new System.EventHandler(this.txtUSB30ResultFile_TextChanged);
            this.txtUSB30ResultFile.DoubleClick += new System.EventHandler(this.txtUSB30ResultFile_DoubleClick);
            // 
            // txtUSB30ToolPath
            // 
            this.txtUSB30ToolPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUSB30ToolPath.Location = new System.Drawing.Point(120, 86);
            this.txtUSB30ToolPath.Name = "txtUSB30ToolPath";
            this.txtUSB30ToolPath.Size = new System.Drawing.Size(538, 23);
            this.txtUSB30ToolPath.TabIndex = 6;
            this.txtUSB30ToolPath.TextChanged += new System.EventHandler(this.txtUSB30ToolPath_TextChanged);
            this.txtUSB30ToolPath.DoubleClick += new System.EventHandler(this.txtUSB30ToolPath_DoubleClick);
            // 
            // txtHDMIResultFile
            // 
            this.txtHDMIResultFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHDMIResultFile.Location = new System.Drawing.Point(121, 57);
            this.txtHDMIResultFile.Name = "txtHDMIResultFile";
            this.txtHDMIResultFile.Size = new System.Drawing.Size(539, 23);
            this.txtHDMIResultFile.TabIndex = 5;
            this.txtHDMIResultFile.TextChanged += new System.EventHandler(this.txtHDMIResultFile_TextChanged);
            this.txtHDMIResultFile.DoubleClick += new System.EventHandler(this.txtHDMIResultFile_DoubleClick);
            // 
            // txtHDMIToolPath
            // 
            this.txtHDMIToolPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHDMIToolPath.Location = new System.Drawing.Point(120, 26);
            this.txtHDMIToolPath.Name = "txtHDMIToolPath";
            this.txtHDMIToolPath.Size = new System.Drawing.Size(540, 23);
            this.txtHDMIToolPath.TabIndex = 4;
            this.txtHDMIToolPath.TextChanged += new System.EventHandler(this.txtHDMIToolPath_TextChanged);
            this.txtHDMIToolPath.DoubleClick += new System.EventHandler(this.txtHDMIToolPath_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(5, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "USB30 Result File:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(10, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "USB30 Tool Path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(9, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "HDMI Result File:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "HDMI Tool Path:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtBarcodeStart);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.txtBarcodeLength);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox7.Location = new System.Drawing.Point(588, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(112, 186);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "BarcodeFilter";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "BarcodeLength:";
            // 
            // txtBarcodeLength
            // 
            this.txtBarcodeLength.AcceptsReturn = true;
            this.txtBarcodeLength.Location = new System.Drawing.Point(9, 57);
            this.txtBarcodeLength.Name = "txtBarcodeLength";
            this.txtBarcodeLength.Size = new System.Drawing.Size(82, 23);
            this.txtBarcodeLength.TabIndex = 1;
            this.txtBarcodeLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBarcodeLength.TextChanged += new System.EventHandler(this.txtBarcodeLength_TextChanged);
            this.txtBarcodeLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcodeLength_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "BarcodeStart:";
            // 
            // txtBarcodeStart
            // 
            this.txtBarcodeStart.Location = new System.Drawing.Point(10, 115);
            this.txtBarcodeStart.Name = "txtBarcodeStart";
            this.txtBarcodeStart.Size = new System.Drawing.Size(82, 23);
            this.txtBarcodeStart.TabIndex = 3;
            this.txtBarcodeStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBarcodeStart.TextChanged += new System.EventHandler(this.txtBarcodeStart_TextChanged);
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 364);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSetting";
            this.Text = "ftmSetting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetting_FormClosing);
            this.Load += new System.EventHandler(this.ftmSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSaveSequence;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox checklistTestItem;
        private System.Windows.Forms.ListBox lstOldSequence;
        private System.Windows.Forms.ListBox lstNewSequnce;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtUSB30ResultFile;
        private System.Windows.Forms.TextBox txtUSB30ToolPath;
        private System.Windows.Forms.TextBox txtHDMIResultFile;
        private System.Windows.Forms.TextBox txtHDMIToolPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtBarcodeStart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBarcodeLength;
        private System.Windows.Forms.Label label5;
    }
}