
namespace VisionPlatform
{
    partial class FormLedSet
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
            this.label_CH = new System.Windows.Forms.Label();
            this.trackBar_Brightness = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.numUpD_Brightness = new System.Windows.Forms.NumericUpDown();
            this.label_CheckItem = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Brightness)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpD_Brightness)).BeginInit();
            this.SuspendLayout();
            // 
            // label_CH
            // 
            this.label_CH.AutoSize = true;
            this.label_CH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_CH.Location = new System.Drawing.Point(55, 1);
            this.label_CH.Name = "label_CH";
            this.label_CH.Size = new System.Drawing.Size(44, 24);
            this.label_CH.TabIndex = 5;
            this.label_CH.Text = "CH1";
            this.label_CH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_Brightness
            // 
            this.trackBar_Brightness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Brightness.LargeChange = 1;
            this.trackBar_Brightness.Location = new System.Drawing.Point(157, 4);
            this.trackBar_Brightness.Maximum = 255;
            this.trackBar_Brightness.Name = "trackBar_Brightness";
            this.trackBar_Brightness.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar_Brightness.Size = new System.Drawing.Size(159, 18);
            this.trackBar_Brightness.TabIndex = 0;
            this.trackBar_Brightness.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Brightness.Scroll += new System.EventHandler(this.trackBar_Brightness_Scroll);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tableLayoutPanel1.Controls.Add(this.label_CH, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBar_Brightness, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.numUpD_Brightness, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_CheckItem, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(320, 26);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // numUpD_Brightness
            // 
            this.numUpD_Brightness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUpD_Brightness.Location = new System.Drawing.Point(104, 2);
            this.numUpD_Brightness.Margin = new System.Windows.Forms.Padding(1);
            this.numUpD_Brightness.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUpD_Brightness.Name = "numUpD_Brightness";
            this.numUpD_Brightness.Size = new System.Drawing.Size(48, 21);
            this.numUpD_Brightness.TabIndex = 6;
            this.numUpD_Brightness.ValueChanged += new System.EventHandler(this.numUpD_Brightness_ValueChanged);
            // 
            // label_CheckItem
            // 
            this.label_CheckItem.AutoSize = true;
            this.label_CheckItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_CheckItem.Location = new System.Drawing.Point(4, 1);
            this.label_CheckItem.Name = "label_CheckItem";
            this.label_CheckItem.Size = new System.Drawing.Size(44, 24);
            this.label_CheckItem.TabIndex = 7;
            this.label_CheckItem.Text = "--";
            this.label_CheckItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormLedSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(320, 26);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLedSet";
            this.Text = "FormLedSet";
            this.Load += new System.EventHandler(this.FormLedSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Brightness)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpD_Brightness)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label_CH;
        private System.Windows.Forms.TrackBar trackBar_Brightness;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown numUpD_Brightness;
        private System.Windows.Forms.Label label_CheckItem;
    }
}