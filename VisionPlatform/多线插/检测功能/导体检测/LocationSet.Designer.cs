namespace VisionPlatform
{
    partial class LocationSet
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numUp_Space = new System.Windows.Forms.NumericUpDown();
            this.numUp_Width = new System.Windows.Forms.NumericUpDown();
            this.numUp_Height = new System.Windows.Forms.NumericUpDown();
            this.trackBar_Space = new System.Windows.Forms.TrackBar();
            this.trackBar_Width = new System.Windows.Forms.TrackBar();
            this.trackBar_Height = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUp_Space)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUp_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUp_Height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Space)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Height)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.numUp_Space, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numUp_Width, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.numUp_Height, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.trackBar_Space, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBar_Width, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.trackBar_Height, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(403, 74);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "间距";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "宽度";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "高度";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numUp_Space
            // 
            this.numUp_Space.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUp_Space.Location = new System.Drawing.Point(63, 2);
            this.numUp_Space.Margin = new System.Windows.Forms.Padding(1);
            this.numUp_Space.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUp_Space.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numUp_Space.Name = "numUp_Space";
            this.numUp_Space.Size = new System.Drawing.Size(73, 21);
            this.numUp_Space.TabIndex = 3;
            this.numUp_Space.Value = new decimal(new int[] {
            830,
            0,
            0,
            -2147483648});
            this.numUp_Space.ValueChanged += new System.EventHandler(this.Inspect);
            // 
            // numUp_Width
            // 
            this.numUp_Width.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUp_Width.Location = new System.Drawing.Point(63, 26);
            this.numUp_Width.Margin = new System.Windows.Forms.Padding(1);
            this.numUp_Width.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUp_Width.Name = "numUp_Width";
            this.numUp_Width.Size = new System.Drawing.Size(73, 21);
            this.numUp_Width.TabIndex = 4;
            this.numUp_Width.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numUp_Width.ValueChanged += new System.EventHandler(this.Inspect);
            // 
            // numUp_Height
            // 
            this.numUp_Height.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUp_Height.Location = new System.Drawing.Point(63, 50);
            this.numUp_Height.Margin = new System.Windows.Forms.Padding(1);
            this.numUp_Height.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUp_Height.Name = "numUp_Height";
            this.numUp_Height.Size = new System.Drawing.Size(73, 21);
            this.numUp_Height.TabIndex = 5;
            this.numUp_Height.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUp_Height.ValueChanged += new System.EventHandler(this.Inspect);
            // 
            // trackBar_Space
            // 
            this.trackBar_Space.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Space.Location = new System.Drawing.Point(141, 4);
            this.trackBar_Space.Maximum = 1000;
            this.trackBar_Space.Minimum = -1000;
            this.trackBar_Space.Name = "trackBar_Space";
            this.trackBar_Space.Size = new System.Drawing.Size(258, 17);
            this.trackBar_Space.TabIndex = 6;
            this.trackBar_Space.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Space.Value = -830;
            this.trackBar_Space.Scroll += new System.EventHandler(this.trackBar__Space_Scroll);
            // 
            // trackBar_Width
            // 
            this.trackBar_Width.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Width.Location = new System.Drawing.Point(141, 28);
            this.trackBar_Width.Maximum = 1000;
            this.trackBar_Width.Name = "trackBar_Width";
            this.trackBar_Width.Size = new System.Drawing.Size(258, 17);
            this.trackBar_Width.TabIndex = 7;
            this.trackBar_Width.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Width.Value = 100;
            this.trackBar_Width.Scroll += new System.EventHandler(this.trackBar_Width_Scroll);
            // 
            // trackBar_Height
            // 
            this.trackBar_Height.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Height.Location = new System.Drawing.Point(141, 52);
            this.trackBar_Height.Maximum = 1000;
            this.trackBar_Height.Name = "trackBar_Height";
            this.trackBar_Height.Size = new System.Drawing.Size(258, 18);
            this.trackBar_Height.TabIndex = 8;
            this.trackBar_Height.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Height.Value = 200;
            this.trackBar_Height.Scroll += new System.EventHandler(this.trackBar_Height_Scroll);
            // 
            // LocationSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LocationSet";
            this.Size = new System.Drawing.Size(403, 74);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUp_Space)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUp_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUp_Height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Space)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Height)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numUp_Space;
        private System.Windows.Forms.NumericUpDown numUp_Width;
        private System.Windows.Forms.NumericUpDown numUp_Height;
        private System.Windows.Forms.TrackBar trackBar_Space;
        private System.Windows.Forms.TrackBar trackBar_Width;
        private System.Windows.Forms.TrackBar trackBar_Height;
    }
}
