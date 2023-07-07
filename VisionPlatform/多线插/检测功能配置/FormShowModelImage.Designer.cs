
namespace VisionPlatform
{
    partial class FormShowModelImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShowModelImage));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hWndCtrl = new HalconDotNet.HWindowControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.but_ProTestData = new System.Windows.Forms.Button();
            this.but_ModelData = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.hWndCtrl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(616, 558);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // hWndCtrl
            // 
            this.hWndCtrl.BackColor = System.Drawing.Color.Black;
            this.hWndCtrl.BorderColor = System.Drawing.Color.Black;
            this.hWndCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWndCtrl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWndCtrl.Location = new System.Drawing.Point(3, 3);
            this.hWndCtrl.Name = "hWndCtrl";
            this.hWndCtrl.Size = new System.Drawing.Size(610, 468);
            this.hWndCtrl.TabIndex = 4;
            this.hWndCtrl.WindowSize = new System.Drawing.Size(610, 468);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel2.Controls.Add(this.but_ProTestData, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.but_ModelData, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 477);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(610, 78);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // but_ProTestData
            // 
            this.but_ProTestData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.but_ProTestData.Location = new System.Drawing.Point(155, 3);
            this.but_ProTestData.Name = "but_ProTestData";
            this.but_ProTestData.Size = new System.Drawing.Size(116, 25);
            this.but_ProTestData.TabIndex = 2;
            this.but_ProTestData.Text = "当前数据";
            this.but_ProTestData.UseVisualStyleBackColor = true;
            this.but_ProTestData.Click += new System.EventHandler(this.but_ProTestData_Click);
            // 
            // but_ModelData
            // 
            this.but_ModelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.but_ModelData.Location = new System.Drawing.Point(3, 3);
            this.but_ModelData.Name = "but_ModelData";
            this.but_ModelData.Size = new System.Drawing.Size(116, 25);
            this.but_ModelData.TabIndex = 3;
            this.but_ModelData.Text = "模板数据";
            this.but_ModelData.UseVisualStyleBackColor = true;
            this.but_ModelData.Click += new System.EventHandler(this.but_ModelData_Click);
            // 
            // FormShowModelImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 558);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormShowModelImage";
            this.Text = "模板图像";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormShowModelImage_FormClosed);
            this.Load += new System.EventHandler(this.FormShowModelImage_Load);
            this.MouseEnter += new System.EventHandler(this.FormShowModelImage_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.FormShowModelImage_MouseLeave);
            this.Resize += new System.EventHandler(this.FormShowModelImage_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private HalconDotNet.HWindowControl hWndCtrl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button but_ProTestData;
        private System.Windows.Forms.Button but_ModelData;
    }
}