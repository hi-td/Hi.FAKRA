﻿namespace VisionPlatform
{
    partial class FormMainUI
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainUI));
            this.toolStrip_Home = new System.Windows.Forms.ToolStrip();
            this.toolStripBut_Home = new System.Windows.Forms.ToolStripButton();
            this.tSBut_InitCam = new System.Windows.Forms.ToolStripButton();
            this.toolStripBut_LightControl = new System.Windows.Forms.ToolStripButton();
            this.toolStripBut_com = new System.Windows.Forms.ToolStripButton();
            this.toolStripBut_importData = new System.Windows.Forms.ToolStripButton();
            this.toolStripBut_SaveGlobalData = new System.Windows.Forms.ToolStripButton();
            this.ts_But_Admin = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tSDropDownBut_help = new System.Windows.Forms.ToolStripDropDownButton();
            this.用户操作指导说明书 = new System.Windows.Forms.ToolStripMenuItem();
            this.操作视频 = new System.Windows.Forms.ToolStripMenuItem();
            this.联系我们 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog_importData = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.picBox_Logo = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.导入公司Logo = new System.Windows.Forms.ToolStripMenuItem();
            this.显示模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.填充 = new System.Windows.Forms.ToolStripMenuItem();
            this.按图像大小 = new System.Windows.Forms.ToolStripMenuItem();
            this.居中显示 = new System.Windows.Forms.ToolStripMenuItem();
            this.图像自适应 = new System.Windows.Forms.ToolStripMenuItem();
            this.清除公司LOGO = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label_SerialName = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label_Time = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_MainUI = new System.Windows.Forms.Panel();
            this.toolStrip_Home.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Logo)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip_Home
            // 
            resources.ApplyResources(this.toolStrip_Home, "toolStrip_Home");
            this.toolStrip_Home.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip_Home.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip_Home.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_Home.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBut_Home,
            this.tSBut_InitCam,
            this.toolStripBut_LightControl,
            this.toolStripBut_com,
            this.toolStripBut_importData,
            this.toolStripBut_SaveGlobalData,
            this.ts_But_Admin,
            this.toolStripLabel1,
            this.tSDropDownBut_help});
            this.toolStrip_Home.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip_Home.Name = "toolStrip_Home";
            this.toolStrip_Home.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // toolStripBut_Home
            // 
            resources.ApplyResources(this.toolStripBut_Home, "toolStripBut_Home");
            this.toolStripBut_Home.BackColor = System.Drawing.Color.Transparent;
            this.toolStripBut_Home.ForeColor = System.Drawing.Color.Black;
            this.toolStripBut_Home.Name = "toolStripBut_Home";
            this.toolStripBut_Home.Click += new System.EventHandler(this.toolStripBut_Home_Click);
            // 
            // tSBut_InitCam
            // 
            resources.ApplyResources(this.tSBut_InitCam, "tSBut_InitCam");
            this.tSBut_InitCam.BackColor = System.Drawing.Color.Transparent;
            this.tSBut_InitCam.ForeColor = System.Drawing.Color.Black;
            this.tSBut_InitCam.Name = "tSBut_InitCam";
            this.tSBut_InitCam.Click += new System.EventHandler(this.tSBut_InitCam_Click);
            // 
            // toolStripBut_LightControl
            // 
            resources.ApplyResources(this.toolStripBut_LightControl, "toolStripBut_LightControl");
            this.toolStripBut_LightControl.BackColor = System.Drawing.Color.Transparent;
            this.toolStripBut_LightControl.ForeColor = System.Drawing.Color.Black;
            this.toolStripBut_LightControl.Name = "toolStripBut_LightControl";
            this.toolStripBut_LightControl.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripBut_com
            // 
            resources.ApplyResources(this.toolStripBut_com, "toolStripBut_com");
            this.toolStripBut_com.BackColor = System.Drawing.Color.Transparent;
            this.toolStripBut_com.ForeColor = System.Drawing.Color.Black;
            this.toolStripBut_com.Name = "toolStripBut_com";
            this.toolStripBut_com.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripBut_importData
            // 
            resources.ApplyResources(this.toolStripBut_importData, "toolStripBut_importData");
            this.toolStripBut_importData.BackColor = System.Drawing.Color.Transparent;
            this.toolStripBut_importData.ForeColor = System.Drawing.Color.Black;
            this.toolStripBut_importData.Name = "toolStripBut_importData";
            this.toolStripBut_importData.Click += new System.EventHandler(this.toolStripBut_importData_Click);
            // 
            // toolStripBut_SaveGlobalData
            // 
            resources.ApplyResources(this.toolStripBut_SaveGlobalData, "toolStripBut_SaveGlobalData");
            this.toolStripBut_SaveGlobalData.BackColor = System.Drawing.Color.Transparent;
            this.toolStripBut_SaveGlobalData.ForeColor = System.Drawing.Color.Black;
            this.toolStripBut_SaveGlobalData.Name = "toolStripBut_SaveGlobalData";
            this.toolStripBut_SaveGlobalData.Click += new System.EventHandler(this.toolStripBut_SaveGlobalData_Click);
            // 
            // ts_But_Admin
            // 
            resources.ApplyResources(this.ts_But_Admin, "ts_But_Admin");
            this.ts_But_Admin.ForeColor = System.Drawing.Color.Black;
            this.ts_But_Admin.Name = "ts_But_Admin";
            this.ts_But_Admin.Click += new System.EventHandler(this.ts_But_Admin_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.ForeColor = System.Drawing.Color.Black;
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // tSDropDownBut_help
            // 
            resources.ApplyResources(this.tSDropDownBut_help, "tSDropDownBut_help");
            this.tSDropDownBut_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.用户操作指导说明书,
            this.操作视频,
            this.联系我们});
            this.tSDropDownBut_help.ForeColor = System.Drawing.Color.Black;
            this.tSDropDownBut_help.Name = "tSDropDownBut_help";
            // 
            // 用户操作指导说明书
            // 
            this.用户操作指导说明书.Name = "用户操作指导说明书";
            resources.ApplyResources(this.用户操作指导说明书, "用户操作指导说明书");
            this.用户操作指导说明书.Click += new System.EventHandler(this.用户操作指导说明书_Click);
            // 
            // 操作视频
            // 
            this.操作视频.Name = "操作视频";
            resources.ApplyResources(this.操作视频, "操作视频");
            this.操作视频.Click += new System.EventHandler(this.操作视频_Click);
            // 
            // 联系我们
            // 
            this.联系我们.Name = "联系我们";
            resources.ApplyResources(this.联系我们, "联系我们");
            this.联系我们.Click += new System.EventHandler(this.联系我们_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // openFileDialog_importData
            // 
            this.openFileDialog_importData.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picBox_Logo);
            this.panel1.Controls.Add(this.toolStrip_Home);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // picBox_Logo
            // 
            this.picBox_Logo.BackColor = System.Drawing.Color.LightSteelBlue;
            this.picBox_Logo.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.picBox_Logo, "picBox_Logo");
            this.picBox_Logo.Name = "picBox_Logo";
            this.picBox_Logo.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入公司Logo,
            this.显示模式ToolStripMenuItem,
            this.清除公司LOGO});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // 导入公司Logo
            // 
            this.导入公司Logo.Name = "导入公司Logo";
            resources.ApplyResources(this.导入公司Logo, "导入公司Logo");
            this.导入公司Logo.Click += new System.EventHandler(this.导入Logo_Click);
            // 
            // 显示模式ToolStripMenuItem
            // 
            this.显示模式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.填充,
            this.按图像大小,
            this.居中显示,
            this.图像自适应});
            this.显示模式ToolStripMenuItem.Name = "显示模式ToolStripMenuItem";
            resources.ApplyResources(this.显示模式ToolStripMenuItem, "显示模式ToolStripMenuItem");
            // 
            // 填充
            // 
            this.填充.Name = "填充";
            resources.ApplyResources(this.填充, "填充");
            this.填充.Click += new System.EventHandler(this.填充_Click);
            // 
            // 按图像大小
            // 
            this.按图像大小.Name = "按图像大小";
            resources.ApplyResources(this.按图像大小, "按图像大小");
            this.按图像大小.Click += new System.EventHandler(this.按图像大小_Click);
            // 
            // 居中显示
            // 
            this.居中显示.Name = "居中显示";
            resources.ApplyResources(this.居中显示, "居中显示");
            this.居中显示.Click += new System.EventHandler(this.居中显示_Click);
            // 
            // 图像自适应
            // 
            this.图像自适应.Name = "图像自适应";
            resources.ApplyResources(this.图像自适应, "图像自适应");
            this.图像自适应.Click += new System.EventHandler(this.图像自适应_Click);
            // 
            // 清除公司LOGO
            // 
            this.清除公司LOGO.Name = "清除公司LOGO";
            resources.ApplyResources(this.清除公司LOGO, "清除公司LOGO");
            this.清除公司LOGO.Click += new System.EventHandler(this.清除公司LOGO_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_SerialName, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Name = "label1";
            // 
            // label_SerialName
            // 
            resources.ApplyResources(this.label_SerialName, "label_SerialName");
            this.label_SerialName.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label_SerialName.Name = "label_SerialName";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.label_Time, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label_Time
            // 
            resources.ApplyResources(this.label_Time, "label_Time");
            this.label_Time.ForeColor = System.Drawing.Color.Black;
            this.label_Time.Name = "label_Time";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Name = "panel2";
            // 
            // panel_MainUI
            // 
            this.panel_MainUI.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.panel_MainUI, "panel_MainUI");
            this.panel_MainUI.Name = "panel_MainUI";
            // 
            // FormMainUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.panel_MainUI);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.Name = "FormMainUI";
            this.TransparencyKey = System.Drawing.Color.SteelBlue;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMainUI_FormClosing);
            this.Load += new System.EventHandler(this.FormMainUI_Load);
            this.toolStrip_Home.ResumeLayout(false);
            this.toolStrip_Home.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Logo)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStrip toolStrip_Home;
        private System.Windows.Forms.ToolStripButton toolStripBut_importData;
        private System.Windows.Forms.ToolStripButton toolStripBut_SaveGlobalData;
        private System.Windows.Forms.ToolStripButton toolStripBut_Home;
        private System.Windows.Forms.OpenFileDialog openFileDialog_importData;
        private System.Windows.Forms.ToolStripButton toolStripBut_LightControl;
        private System.Windows.Forms.ToolStripButton tSBut_InitCam;
        private System.Windows.Forms.ToolStripButton toolStripBut_com;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.ToolStripButton ts_But_Admin;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBox_Logo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 导入公司Logo;
        private System.Windows.Forms.ToolStripMenuItem 显示模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 填充;
        private System.Windows.Forms.ToolStripMenuItem 按图像大小;
        private System.Windows.Forms.ToolStripMenuItem 居中显示;
        private System.Windows.Forms.ToolStripMenuItem 图像自适应;
        private System.Windows.Forms.ToolStripMenuItem 清除公司LOGO;
        private System.Windows.Forms.ToolStripDropDownButton tSDropDownBut_help;
        private System.Windows.Forms.ToolStripMenuItem 用户操作指导说明书;
        private System.Windows.Forms.ToolStripMenuItem 操作视频;
        private System.Windows.Forms.ToolStripMenuItem 联系我们;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_SerialName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label_Time;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_MainUI;
    }
}

