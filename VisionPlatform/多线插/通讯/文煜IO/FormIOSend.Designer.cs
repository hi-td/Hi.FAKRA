namespace VisionPlatform
{
    partial class FormIOSend
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_PreSendOK = new System.Windows.Forms.Label();
            this.label_PreSendNG = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox_OK = new System.Windows.Forms.CheckBox();
            this.checkBox_NG = new System.Windows.Forms.CheckBox();
            this.comboBox_OK = new System.Windows.Forms.ComboBox();
            this.comboBox_NG = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numUpD_Sleep = new System.Windows.Forms.NumericUpDown();
            this.checkBox_Invert = new System.Windows.Forms.CheckBox();
            this.tLPanel_Sleep = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.but_SaveSet = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpD_Sleep)).BeginInit();
            this.tLPanel_Sleep.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.76119F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.23881F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_PreSendOK, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_PreSendNG, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(202, 105);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前OK输出点位";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "当前NG输出点位";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_PreSendOK
            // 
            this.label_PreSendOK.AutoSize = true;
            this.label_PreSendOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_PreSendOK.Location = new System.Drawing.Point(100, 1);
            this.label_PreSendOK.Name = "label_PreSendOK";
            this.label_PreSendOK.Size = new System.Drawing.Size(98, 25);
            this.label_PreSendOK.TabIndex = 2;
            this.label_PreSendOK.Text = "-";
            this.label_PreSendOK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_PreSendNG
            // 
            this.label_PreSendNG.AutoSize = true;
            this.label_PreSendNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_PreSendNG.Location = new System.Drawing.Point(100, 27);
            this.label_PreSendNG.Name = "label_PreSendNG";
            this.label_PreSendNG.Size = new System.Drawing.Size(98, 25);
            this.label_PreSendNG.TabIndex = 3;
            this.label_PreSendNG.Text = "-";
            this.label_PreSendNG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 51);
            this.label3.TabIndex = 4;
            this.label3.Text = "输出信号";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.93877F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.06123F));
            this.tableLayoutPanel2.Controls.Add(this.checkBox_OK, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkBox_NG, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.comboBox_OK, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox_NG, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(100, 56);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(98, 45);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // checkBox_OK
            // 
            this.checkBox_OK.AutoSize = true;
            this.checkBox_OK.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_OK.Location = new System.Drawing.Point(3, 3);
            this.checkBox_OK.Name = "checkBox_OK";
            this.checkBox_OK.Size = new System.Drawing.Size(36, 16);
            this.checkBox_OK.TabIndex = 0;
            this.checkBox_OK.Text = "OK";
            this.checkBox_OK.UseVisualStyleBackColor = true;
            this.checkBox_OK.CheckedChanged += new System.EventHandler(this.checkBox_OK_CheckedChanged);
            // 
            // checkBox_NG
            // 
            this.checkBox_NG.AutoSize = true;
            this.checkBox_NG.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_NG.Location = new System.Drawing.Point(3, 25);
            this.checkBox_NG.Name = "checkBox_NG";
            this.checkBox_NG.Size = new System.Drawing.Size(36, 17);
            this.checkBox_NG.TabIndex = 1;
            this.checkBox_NG.Text = "NG";
            this.checkBox_NG.UseVisualStyleBackColor = true;
            this.checkBox_NG.CheckedChanged += new System.EventHandler(this.checkBox_NG_CheckedChanged);
            // 
            // comboBox_OK
            // 
            this.comboBox_OK.FormattingEnabled = true;
            this.comboBox_OK.Location = new System.Drawing.Point(48, 3);
            this.comboBox_OK.Name = "comboBox_OK";
            this.comboBox_OK.Size = new System.Drawing.Size(46, 20);
            this.comboBox_OK.TabIndex = 2;
            this.comboBox_OK.Visible = false;
            this.comboBox_OK.SelectedIndexChanged += new System.EventHandler(this.comboBox_OK_SelectedIndexChanged);
            // 
            // comboBox_NG
            // 
            this.comboBox_NG.FormattingEnabled = true;
            this.comboBox_NG.Location = new System.Drawing.Point(48, 25);
            this.comboBox_NG.Name = "comboBox_NG";
            this.comboBox_NG.Size = new System.Drawing.Size(46, 20);
            this.comboBox_NG.TabIndex = 3;
            this.comboBox_NG.Visible = false;
            this.comboBox_NG.SelectedIndexChanged += new System.EventHandler(this.comboBox_NG_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(4, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 33);
            this.label4.TabIndex = 6;
            this.label4.Text = "信号持续时长";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(67, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 27);
            this.label6.TabIndex = 0;
            this.label6.Text = "ms";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numUpD_Sleep
            // 
            this.numUpD_Sleep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUpD_Sleep.Location = new System.Drawing.Point(3, 3);
            this.numUpD_Sleep.Name = "numUpD_Sleep";
            this.numUpD_Sleep.Size = new System.Drawing.Size(58, 21);
            this.numUpD_Sleep.TabIndex = 1;
            this.numUpD_Sleep.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // checkBox_Invert
            // 
            this.checkBox_Invert.AutoSize = true;
            this.checkBox_Invert.Checked = true;
            this.checkBox_Invert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Invert.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBox_Invert.Location = new System.Drawing.Point(0, 105);
            this.checkBox_Invert.Name = "checkBox_Invert";
            this.checkBox_Invert.Size = new System.Drawing.Size(202, 16);
            this.checkBox_Invert.TabIndex = 9;
            this.checkBox_Invert.Text = "立即反转信号";
            this.checkBox_Invert.UseVisualStyleBackColor = true;
            this.checkBox_Invert.CheckedChanged += new System.EventHandler(this.checkBox_Invert_CheckedChanged);
            // 
            // tLPanel_Sleep
            // 
            this.tLPanel_Sleep.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tLPanel_Sleep.ColumnCount = 2;
            this.tLPanel_Sleep.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.76119F));
            this.tLPanel_Sleep.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.23881F));
            this.tLPanel_Sleep.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tLPanel_Sleep.Controls.Add(this.label4, 0, 0);
            this.tLPanel_Sleep.Dock = System.Windows.Forms.DockStyle.Top;
            this.tLPanel_Sleep.Location = new System.Drawing.Point(0, 121);
            this.tLPanel_Sleep.Name = "tLPanel_Sleep";
            this.tLPanel_Sleep.RowCount = 1;
            this.tLPanel_Sleep.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tLPanel_Sleep.Size = new System.Drawing.Size(202, 35);
            this.tLPanel_Sleep.TabIndex = 10;
            this.tLPanel_Sleep.Visible = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.01942F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.98058F));
            this.tableLayoutPanel3.Controls.Add(this.label6, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.numUpD_Sleep, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(100, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(98, 27);
            this.tableLayoutPanel3.TabIndex = 11;
            // 
            // but_SaveSet
            // 
            this.but_SaveSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.but_SaveSet.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.but_SaveSet.Location = new System.Drawing.Point(0, 156);
            this.but_SaveSet.Name = "but_SaveSet";
            this.but_SaveSet.Size = new System.Drawing.Size(202, 31);
            this.but_SaveSet.TabIndex = 11;
            this.but_SaveSet.Text = "保存设置";
            this.but_SaveSet.UseVisualStyleBackColor = true;
            this.but_SaveSet.Click += new System.EventHandler(this.but_SaveSet_Click);
            // 
            // FormIOSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 187);
            this.Controls.Add(this.but_SaveSet);
            this.Controls.Add(this.tLPanel_Sleep);
            this.Controls.Add(this.checkBox_Invert);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormIOSend";
            this.Text = "I/O输出点位配置";
            this.Load += new System.EventHandler(this.FormIOSend_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpD_Sleep)).EndInit();
            this.tLPanel_Sleep.ResumeLayout(false);
            this.tLPanel_Sleep.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_PreSendOK;
        private System.Windows.Forms.Label label_PreSendNG;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBox_OK;
        private System.Windows.Forms.CheckBox checkBox_NG;
        private System.Windows.Forms.ComboBox comboBox_OK;
        private System.Windows.Forms.ComboBox comboBox_NG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_Invert;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numUpD_Sleep;
        private System.Windows.Forms.TableLayoutPanel tLPanel_Sleep;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button but_SaveSet;
    }
}