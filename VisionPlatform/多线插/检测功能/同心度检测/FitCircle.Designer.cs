﻿namespace VisionPlatform
{
    partial class FitCircle
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar_Len2 = new System.Windows.Forms.TrackBar();
            this.numUpD_Thd = new System.Windows.Forms.NumericUpDown();
            this.trackBar_Thd = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.comBox_EdgePoint = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numUpD_Len2 = new System.Windows.Forms.NumericUpDown();
            this.comBox_Trans = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Len2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpD_Thd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Thd)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpD_Len2)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.trackBar_Len2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.numUpD_Thd, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.trackBar_Thd, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.numUpD_Len2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comBox_Trans, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(388, 71);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "阈值";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_Len2
            // 
            this.trackBar_Len2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Len2.Location = new System.Drawing.Point(158, 27);
            this.trackBar_Len2.Maximum = 100;
            this.trackBar_Len2.Name = "trackBar_Len2";
            this.trackBar_Len2.Size = new System.Drawing.Size(226, 16);
            this.trackBar_Len2.TabIndex = 7;
            this.trackBar_Len2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Len2.Value = 25;
            this.trackBar_Len2.Scroll += new System.EventHandler(this.trackBar_Len2_Scroll);
            // 
            // numUpD_Thd
            // 
            this.numUpD_Thd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUpD_Thd.Location = new System.Drawing.Point(79, 48);
            this.numUpD_Thd.Margin = new System.Windows.Forms.Padding(1);
            this.numUpD_Thd.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUpD_Thd.Name = "numUpD_Thd";
            this.numUpD_Thd.Size = new System.Drawing.Size(74, 21);
            this.numUpD_Thd.TabIndex = 6;
            this.numUpD_Thd.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUpD_Thd.ValueChanged += new System.EventHandler(this.Inspect);
            // 
            // trackBar_Thd
            // 
            this.trackBar_Thd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Thd.Location = new System.Drawing.Point(158, 50);
            this.trackBar_Thd.Maximum = 255;
            this.trackBar_Thd.Name = "trackBar_Thd";
            this.trackBar_Thd.Size = new System.Drawing.Size(226, 17);
            this.trackBar_Thd.TabIndex = 8;
            this.trackBar_Thd.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Thd.Value = 10;
            this.trackBar_Thd.Scroll += new System.EventHandler(this.trackBar_Thd_Scroll);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.29578F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.70422F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.comBox_EdgePoint, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(155, 1);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(182, 22);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "边缘点";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comBox_EdgePoint
            // 
            this.comBox_EdgePoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comBox_EdgePoint.FormattingEnabled = true;
            this.comBox_EdgePoint.Items.AddRange(new object[] {
            "第一个点",
            "最后一个点"});
            this.comBox_EdgePoint.Location = new System.Drawing.Point(90, 1);
            this.comBox_EdgePoint.Margin = new System.Windows.Forms.Padding(1);
            this.comBox_EdgePoint.Name = "comBox_EdgePoint";
            this.comBox_EdgePoint.Size = new System.Drawing.Size(91, 20);
            this.comBox_EdgePoint.TabIndex = 5;
            this.comBox_EdgePoint.SelectedIndexChanged += new System.EventHandler(this.Inspect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "搜索框宽";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "灰度变化";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numUpD_Len2
            // 
            this.numUpD_Len2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUpD_Len2.Location = new System.Drawing.Point(79, 25);
            this.numUpD_Len2.Margin = new System.Windows.Forms.Padding(1);
            this.numUpD_Len2.Name = "numUpD_Len2";
            this.numUpD_Len2.Size = new System.Drawing.Size(74, 21);
            this.numUpD_Len2.TabIndex = 5;
            this.numUpD_Len2.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numUpD_Len2.ValueChanged += new System.EventHandler(this.Inspect);
            // 
            // comBox_Trans
            // 
            this.comBox_Trans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comBox_Trans.FormattingEnabled = true;
            this.comBox_Trans.Items.AddRange(new object[] {
            "从亮到暗",
            "从暗到亮"});
            this.comBox_Trans.Location = new System.Drawing.Point(79, 2);
            this.comBox_Trans.Margin = new System.Windows.Forms.Padding(1);
            this.comBox_Trans.Name = "comBox_Trans";
            this.comBox_Trans.Size = new System.Drawing.Size(74, 20);
            this.comBox_Trans.TabIndex = 4;
            this.comBox_Trans.SelectedIndexChanged += new System.EventHandler(this.Inspect);
            // 
            // FitCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FitCircle";
            this.Size = new System.Drawing.Size(388, 71);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Len2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpD_Thd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Thd)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpD_Len2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox comBox_EdgePoint;
        private System.Windows.Forms.ComboBox comBox_Trans;
        private System.Windows.Forms.TrackBar trackBar_Len2;
        private System.Windows.Forms.NumericUpDown numUpD_Thd;
        private System.Windows.Forms.TrackBar trackBar_Thd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numUpD_Len2;
    }
}
