
namespace VisionPlatform
{
    partial class FormPLCCommQ
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
            this.label3 = new System.Windows.Forms.Label();
            this.txt_IpAddress = new System.Windows.Forms.TextBox();
            this.txt_ReadPort = new System.Windows.Forms.TextBox();
            this.txt_WritePort = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_M315 = new System.Windows.Forms.Label();
            this.lbl_M310 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_M300 = new System.Windows.Forms.Label();
            this.lbl_M305 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Write = new System.Windows.Forms.Button();
            this.btn_Read = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txt_IpAddress, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_ReadPort, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txt_WritePort, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(515, 83);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(116, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(116, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "读端口";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(116, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 29);
            this.label3.TabIndex = 1;
            this.label3.Text = "写端口";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_IpAddress
            // 
            this.txt_IpAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_IpAddress.Location = new System.Drawing.Point(224, 3);
            this.txt_IpAddress.Name = "txt_IpAddress";
            this.txt_IpAddress.Size = new System.Drawing.Size(174, 21);
            this.txt_IpAddress.TabIndex = 2;
            this.txt_IpAddress.Text = "192.168.3.39";
            // 
            // txt_ReadPort
            // 
            this.txt_ReadPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_ReadPort.Location = new System.Drawing.Point(224, 30);
            this.txt_ReadPort.Name = "txt_ReadPort";
            this.txt_ReadPort.Size = new System.Drawing.Size(174, 21);
            this.txt_ReadPort.TabIndex = 3;
            this.txt_ReadPort.Text = "6000";
            // 
            // txt_WritePort
            // 
            this.txt_WritePort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_WritePort.Location = new System.Drawing.Point(224, 57);
            this.txt_WritePort.Name = "txt_WritePort";
            this.txt_WritePort.Size = new System.Drawing.Size(174, 21);
            this.txt_WritePort.TabIndex = 4;
            this.txt_WritePort.Text = "1107";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(515, 142);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PLC测试";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel3.Controls.Add(this.lbl_M315, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.lbl_M310, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label7, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label11, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label12, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.lbl_M300, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_M305, 2, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(509, 122);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // lbl_M315
            // 
            this.lbl_M315.AutoSize = true;
            this.lbl_M315.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_M315.Location = new System.Drawing.Point(232, 91);
            this.lbl_M315.Name = "lbl_M315";
            this.lbl_M315.Size = new System.Drawing.Size(119, 30);
            this.lbl_M315.TabIndex = 12;
            this.lbl_M315.Text = "未连接";
            this.lbl_M315.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_M310
            // 
            this.lbl_M310.AutoSize = true;
            this.lbl_M310.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_M310.Location = new System.Drawing.Point(232, 61);
            this.lbl_M310.Name = "lbl_M310";
            this.lbl_M310.Size = new System.Drawing.Size(119, 29);
            this.lbl_M310.TabIndex = 11;
            this.lbl_M310.Text = "未连接";
            this.lbl_M310.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("宋体", 9F);
            this.label5.Location = new System.Drawing.Point(30, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(195, 29);
            this.label5.TabIndex = 7;
            this.label5.Text = "前芯触发信号：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("宋体", 9F);
            this.label7.Location = new System.Drawing.Point(30, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(195, 29);
            this.label7.TabIndex = 7;
            this.label7.Text = "后芯触发信号：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("宋体", 9F);
            this.label11.Location = new System.Drawing.Point(30, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(195, 29);
            this.label11.TabIndex = 7;
            this.label11.Text = "前端触发信号：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("宋体", 9F);
            this.label12.Location = new System.Drawing.Point(30, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(195, 30);
            this.label12.TabIndex = 7;
            this.label12.Text = "后端触发信号：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_M300
            // 
            this.lbl_M300.AutoSize = true;
            this.lbl_M300.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_M300.Font = new System.Drawing.Font("宋体", 9F);
            this.lbl_M300.Location = new System.Drawing.Point(232, 1);
            this.lbl_M300.Name = "lbl_M300";
            this.lbl_M300.Size = new System.Drawing.Size(119, 29);
            this.lbl_M300.TabIndex = 8;
            this.lbl_M300.Text = "未连接";
            this.lbl_M300.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_M305
            // 
            this.lbl_M305.AutoSize = true;
            this.lbl_M305.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_M305.Font = new System.Drawing.Font("宋体", 9F);
            this.lbl_M305.Location = new System.Drawing.Point(232, 31);
            this.lbl_M305.Name = "lbl_M305";
            this.lbl_M305.Size = new System.Drawing.Size(119, 29);
            this.lbl_M305.TabIndex = 9;
            this.lbl_M305.Text = "未连接";
            this.lbl_M305.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.85714F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.85714F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.btn_Save, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_Write, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_Read, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 225);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(515, 31);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // btn_Save
            // 
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.Location = new System.Drawing.Point(388, 3);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(124, 25);
            this.btn_Save.TabIndex = 0;
            this.btn_Save.Text = "参数保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Write
            // 
            this.btn_Write.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Write.Location = new System.Drawing.Point(132, 3);
            this.btn_Write.Name = "btn_Write";
            this.btn_Write.Size = new System.Drawing.Size(122, 25);
            this.btn_Write.TabIndex = 1;
            this.btn_Write.Text = "写打开";
            this.btn_Write.UseVisualStyleBackColor = true;
            // 
            // btn_Read
            // 
            this.btn_Read.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Read.Location = new System.Drawing.Point(3, 3);
            this.btn_Read.Name = "btn_Read";
            this.btn_Read.Size = new System.Drawing.Size(123, 25);
            this.btn_Read.TabIndex = 2;
            this.btn_Read.Text = "读打开";
            this.btn_Read.UseVisualStyleBackColor = true;
            this.btn_Read.Click += new System.EventHandler(this.btn_Read_Click);
            // 
            // FormPLCCommQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 450);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormPLCCommQ";
            this.Text = "PLC通讯测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPLCCommQ_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_IpAddress;
        private System.Windows.Forms.TextBox txt_ReadPort;
        private System.Windows.Forms.TextBox txt_WritePort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_M300;
        private System.Windows.Forms.Label lbl_M305;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Write;
        private System.Windows.Forms.Button btn_Read;
        private System.Windows.Forms.Label lbl_M310;
        private System.Windows.Forms.Label lbl_M315;
    }
}