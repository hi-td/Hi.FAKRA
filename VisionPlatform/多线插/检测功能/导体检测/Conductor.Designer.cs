namespace VisionPlatform
{
    partial class Conductor
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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Test = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.tabControl_InspectItem = new System.Windows.Forms.TabControl();
            this.tabPage_Head = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox_Head = new System.Windows.Forms.GroupBox();
            this.tabPage_Central = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox_Central = new System.Windows.Forms.GroupBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox_Tail = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.trackBar_Location_Erosion = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numUp_Location_Thr = new System.Windows.Forms.NumericUpDown();
            this.numUp_Location_Erosion = new System.Windows.Forms.NumericUpDown();
            this.trackBar_Location_Thr = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label_name = new System.Windows.Forms.Label();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabControl_InspectItem.SuspendLayout();
            this.tabPage_Head.SuspendLayout();
            this.tabPage_Central.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Location_Erosion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUp_Location_Thr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUp_Location_Erosion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Location_Thr)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.btn_Test, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_Save, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1, 545);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(354, 35);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // btn_Test
            // 
            this.btn_Test.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Test.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Test.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Test.Location = new System.Drawing.Point(179, 3);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(82, 29);
            this.btn_Test.TabIndex = 0;
            this.btn_Test.Text = "测  试";
            this.btn_Test.UseVisualStyleBackColor = false;
            this.btn_Test.Click += new System.EventHandler(this.Inspect);
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Save.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Save.Location = new System.Drawing.Point(267, 3);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(84, 29);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.Text = "保存数据";
            this.btn_Save.UseVisualStyleBackColor = false;
            // 
            // tabControl_InspectItem
            // 
            this.tabControl_InspectItem.Controls.Add(this.tabPage_Head);
            this.tabControl_InspectItem.Controls.Add(this.tabPage_Central);
            this.tabControl_InspectItem.Controls.Add(this.tabPage1);
            this.tabControl_InspectItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_InspectItem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl_InspectItem.Location = new System.Drawing.Point(1, 101);
            this.tabControl_InspectItem.Margin = new System.Windows.Forms.Padding(1);
            this.tabControl_InspectItem.Name = "tabControl_InspectItem";
            this.tabControl_InspectItem.SelectedIndex = 0;
            this.tabControl_InspectItem.Size = new System.Drawing.Size(354, 444);
            this.tabControl_InspectItem.TabIndex = 3;
            // 
            // tabPage_Head
            // 
            this.tabPage_Head.AutoScroll = true;
            this.tabPage_Head.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabPage_Head.Controls.Add(this.groupBox1);
            this.tabPage_Head.Controls.Add(this.groupBox_Head);
            this.tabPage_Head.Location = new System.Drawing.Point(4, 26);
            this.tabPage_Head.Name = "tabPage_Head";
            this.tabPage_Head.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Head.Size = new System.Drawing.Size(346, 414);
            this.tabPage_Head.TabIndex = 0;
            this.tabPage_Head.Text = "头部";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 235);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数设置";
            // 
            // groupBox_Head
            // 
            this.groupBox_Head.BackColor = System.Drawing.Color.Transparent;
            this.groupBox_Head.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_Head.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox_Head.Location = new System.Drawing.Point(3, 3);
            this.groupBox_Head.Name = "groupBox_Head";
            this.groupBox_Head.Size = new System.Drawing.Size(340, 99);
            this.groupBox_Head.TabIndex = 0;
            this.groupBox_Head.TabStop = false;
            this.groupBox_Head.Text = "检测位置";
            // 
            // tabPage_Central
            // 
            this.tabPage_Central.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabPage_Central.Controls.Add(this.groupBox2);
            this.tabPage_Central.Controls.Add(this.groupBox_Central);
            this.tabPage_Central.Location = new System.Drawing.Point(4, 26);
            this.tabPage_Central.Name = "tabPage_Central";
            this.tabPage_Central.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Central.Size = new System.Drawing.Size(346, 324);
            this.tabPage_Central.TabIndex = 1;
            this.tabPage_Central.Text = "中部";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 243);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数设置";
            // 
            // groupBox_Central
            // 
            this.groupBox_Central.BackColor = System.Drawing.Color.Transparent;
            this.groupBox_Central.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_Central.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox_Central.Location = new System.Drawing.Point(3, 3);
            this.groupBox_Central.Name = "groupBox_Central";
            this.groupBox_Central.Size = new System.Drawing.Size(340, 99);
            this.groupBox_Central.TabIndex = 1;
            this.groupBox_Central.TabStop = false;
            this.groupBox_Central.Text = "检测位置";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox_Tail);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(346, 324);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "尾部";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 102);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(340, 243);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "参数设置";
            // 
            // groupBox_Tail
            // 
            this.groupBox_Tail.BackColor = System.Drawing.Color.Transparent;
            this.groupBox_Tail.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_Tail.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox_Tail.Location = new System.Drawing.Point(3, 3);
            this.groupBox_Tail.Name = "groupBox_Tail";
            this.groupBox_Tail.Size = new System.Drawing.Size(340, 99);
            this.groupBox_Tail.TabIndex = 2;
            this.groupBox_Tail.TabStop = false;
            this.groupBox_Tail.Text = "检测位置";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(1, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(354, 77);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "定位";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.trackBar_Location_Erosion, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.numUp_Location_Thr, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numUp_Location_Erosion, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.trackBar_Location_Thr, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(348, 55);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // trackBar_Location_Erosion
            // 
            this.trackBar_Location_Erosion.BackColor = System.Drawing.SystemColors.Window;
            this.trackBar_Location_Erosion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Location_Erosion.Location = new System.Drawing.Point(141, 31);
            this.trackBar_Location_Erosion.Maximum = 300;
            this.trackBar_Location_Erosion.Name = "trackBar_Location_Erosion";
            this.trackBar_Location_Erosion.Size = new System.Drawing.Size(203, 20);
            this.trackBar_Location_Erosion.TabIndex = 5;
            this.trackBar_Location_Erosion.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Location_Erosion.Value = 250;
            this.trackBar_Location_Erosion.Scroll += new System.EventHandler(this.trackBar_Location_Erosion_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "阈值";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "定位";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numUp_Location_Thr
            // 
            this.numUp_Location_Thr.BackColor = System.Drawing.SystemColors.Window;
            this.numUp_Location_Thr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUp_Location_Thr.Location = new System.Drawing.Point(63, 2);
            this.numUp_Location_Thr.Margin = new System.Windows.Forms.Padding(1);
            this.numUp_Location_Thr.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numUp_Location_Thr.Name = "numUp_Location_Thr";
            this.numUp_Location_Thr.Size = new System.Drawing.Size(73, 23);
            this.numUp_Location_Thr.TabIndex = 2;
            this.numUp_Location_Thr.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numUp_Location_Thr.ValueChanged += new System.EventHandler(this.Inspect);
            // 
            // numUp_Location_Erosion
            // 
            this.numUp_Location_Erosion.BackColor = System.Drawing.SystemColors.Window;
            this.numUp_Location_Erosion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUp_Location_Erosion.Location = new System.Drawing.Point(63, 29);
            this.numUp_Location_Erosion.Margin = new System.Windows.Forms.Padding(1);
            this.numUp_Location_Erosion.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numUp_Location_Erosion.Name = "numUp_Location_Erosion";
            this.numUp_Location_Erosion.Size = new System.Drawing.Size(73, 23);
            this.numUp_Location_Erosion.TabIndex = 3;
            this.numUp_Location_Erosion.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numUp_Location_Erosion.ValueChanged += new System.EventHandler(this.Inspect);
            // 
            // trackBar_Location_Thr
            // 
            this.trackBar_Location_Thr.BackColor = System.Drawing.SystemColors.Window;
            this.trackBar_Location_Thr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Location_Thr.Location = new System.Drawing.Point(141, 4);
            this.trackBar_Location_Thr.Maximum = 250;
            this.trackBar_Location_Thr.Name = "trackBar_Location_Thr";
            this.trackBar_Location_Thr.Size = new System.Drawing.Size(203, 20);
            this.trackBar_Location_Thr.TabIndex = 4;
            this.trackBar_Location_Thr.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Location_Thr.Value = 250;
            this.trackBar_Location_Thr.Scroll += new System.EventHandler(this.trackBar_Location_Thr_Scroll);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label_name, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(354, 23);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.BackColor = System.Drawing.SystemColors.Control;
            this.label_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_name.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_name.Location = new System.Drawing.Point(0, 0);
            this.label_name.Margin = new System.Windows.Forms.Padding(0);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(354, 23);
            this.label_name.TabIndex = 0;
            this.label_name.Text = "导体";
            this.label_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Conductor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.tabControl_InspectItem);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "Conductor";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(356, 581);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabControl_InspectItem.ResumeLayout(false);
            this.tabPage_Head.ResumeLayout(false);
            this.tabPage_Central.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Location_Erosion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUp_Location_Thr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUp_Location_Erosion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Location_Thr)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TabControl tabControl_InspectItem;
        private System.Windows.Forms.TabPage tabPage_Head;
        private System.Windows.Forms.TabPage tabPage_Central;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox_Head;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox_Central;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox_Tail;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numUp_Location_Thr;
        private System.Windows.Forms.NumericUpDown numUp_Location_Erosion;
        private System.Windows.Forms.TrackBar trackBar_Location_Erosion;
        private System.Windows.Forms.TrackBar trackBar_Location_Thr;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label_name;
    }
}
