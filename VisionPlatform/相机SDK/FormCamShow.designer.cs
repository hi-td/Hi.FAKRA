
namespace VisionPlatform
{
    partial class FormCamShow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCamShow));
            this.label_d = new System.Windows.Forms.Label();
            this.label_x = new System.Windows.Forms.Label();
            this.panel_hWnd = new System.Windows.Forms.Panel();
            this.hWndCtrl = new HalconDotNet.HWindowControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关联相机 = new System.Windows.Forms.ToolStripMenuItem();
            this.画质调节ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.绘制 = new System.Windows.Forms.ToolStripMenuItem();
            this.矩形1 = new System.Windows.Forms.ToolStripMenuItem();
            this.矩形2带方向 = new System.Windows.Forms.ToolStripMenuItem();
            this.圆 = new System.Windows.Forms.ToolStripMenuItem();
            this.任意形状 = new System.Windows.Forms.ToolStripMenuItem();
            this.直线 = new System.Windows.Forms.ToolStripMenuItem();
            this.显示设置 = new System.Windows.Forms.ToolStripMenuItem();
            this.默认设置 = new System.Windows.Forms.ToolStripMenuItem();
            this.线条 = new System.Windows.Forms.ToolStripMenuItem();
            this.ComboBox_LineWith = new System.Windows.Forms.ToolStripComboBox();
            this.颜色 = new System.Windows.Forms.ToolStripMenuItem();
            this.red = new System.Windows.Forms.ToolStripMenuItem();
            this.green = new System.Windows.Forms.ToolStripMenuItem();
            this.蓝色 = new System.Windows.Forms.ToolStripMenuItem();
            this.黄色 = new System.Windows.Forms.ToolStripMenuItem();
            this.显示轮廓 = new System.Windows.Forms.ToolStripMenuItem();
            this.填充显示 = new System.Windows.Forms.ToolStripMenuItem();
            this.字体大小 = new System.Windows.Forms.ToolStripMenuItem();
            this.ComboBox_FontSize = new System.Windows.Forms.ToolStripComboBox();
            this.图像镜像 = new System.Windows.Forms.ToolStripMenuItem();
            this.LoR = new System.Windows.Forms.ToolStripMenuItem();
            this.UoD = new System.Windows.Forms.ToolStripMenuItem();
            this.清除 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.but_ImageList = new System.Windows.Forms.Button();
            this.but_LoadImage = new System.Windows.Forms.Button();
            this.but_RecoverImg = new System.Windows.Forms.Button();
            this.but_SaveImg = new System.Windows.Forms.Button();
            this.but_GrabImage = new System.Windows.Forms.Button();
            this.but_Live = new System.Windows.Forms.Button();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.ts_Label_CamSer = new System.Windows.Forms.ToolStripLabel();
            this.ts_Label_state = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel_pos = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel_gray = new System.Windows.Forms.ToolStripLabel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox_ImageList = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listView_ImgList = new System.Windows.Forms.ListView();
            this.but_PreImage = new System.Windows.Forms.Button();
            this.but_NextImage = new System.Windows.Forms.Button();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.ts_But_ImageListClose = new System.Windows.Forms.ToolStripButton();
            this.tsBut_LoadOK = new System.Windows.Forms.ToolStripButton();
            this.tsBut_LoadNG = new System.Windows.Forms.ToolStripButton();
            this.tsBut_LoadFile = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label_Edit = new System.Windows.Forms.Label();
            this.label_Cam = new System.Windows.Forms.Label();
            this.panel_hWnd.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.groupBox_ImageList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_d
            // 
            resources.ApplyResources(this.label_d, "label_d");
            this.label_d.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label_d.Name = "label_d";
            // 
            // label_x
            // 
            resources.ApplyResources(this.label_x, "label_x");
            this.label_x.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label_x.Name = "label_x";
            // 
            // panel_hWnd
            // 
            this.panel_hWnd.Controls.Add(this.hWndCtrl);
            this.panel_hWnd.Controls.Add(this.panel2);
            this.panel_hWnd.Controls.Add(this.toolStrip2);
            resources.ApplyResources(this.panel_hWnd, "panel_hWnd");
            this.panel_hWnd.Name = "panel_hWnd";
            // 
            // hWndCtrl
            // 
            this.hWndCtrl.BackColor = System.Drawing.Color.Black;
            this.hWndCtrl.BorderColor = System.Drawing.Color.Black;
            this.hWndCtrl.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.hWndCtrl, "hWndCtrl");
            this.hWndCtrl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWndCtrl.Name = "hWndCtrl";
            this.hWndCtrl.WindowSize = new System.Drawing.Size(582, 369);
            this.hWndCtrl.HMouseMove += new HalconDotNet.HMouseEventHandler(this.hWndCtrl_HMouseMove);
            this.hWndCtrl.HMouseDown += new HalconDotNet.HMouseEventHandler(this.hWndCtrl_HMouseDown);
            this.hWndCtrl.HMouseWheel += new HalconDotNet.HMouseEventHandler(this.hWndCtrl_HMouseWheel);
            this.hWndCtrl.Resize += new System.EventHandler(this.hWndCtrl_Resize);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关联相机,
            this.画质调节ToolStripMenuItem,
            this.绘制,
            this.显示设置,
            this.清除});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // 关联相机
            // 
            this.关联相机.Name = "关联相机";
            resources.ApplyResources(this.关联相机, "关联相机");
            this.关联相机.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.关联相机_DropDownItemClicked);
            this.关联相机.Click += new System.EventHandler(this.关联相机_Click);
            // 
            // 画质调节ToolStripMenuItem
            // 
            resources.ApplyResources(this.画质调节ToolStripMenuItem, "画质调节ToolStripMenuItem");
            this.画质调节ToolStripMenuItem.Name = "画质调节ToolStripMenuItem";
            this.画质调节ToolStripMenuItem.Click += new System.EventHandler(this.画质调节ToolStripMenuItem_Click);
            // 
            // 绘制
            // 
            resources.ApplyResources(this.绘制, "绘制");
            this.绘制.BackColor = System.Drawing.SystemColors.Control;
            this.绘制.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.矩形1,
            this.矩形2带方向,
            this.圆,
            this.任意形状,
            this.直线});
            this.绘制.Name = "绘制";
            // 
            // 矩形1
            // 
            resources.ApplyResources(this.矩形1, "矩形1");
            this.矩形1.Name = "矩形1";
            this.矩形1.Click += new System.EventHandler(this.矩形1_Click);
            // 
            // 矩形2带方向
            // 
            resources.ApplyResources(this.矩形2带方向, "矩形2带方向");
            this.矩形2带方向.Name = "矩形2带方向";
            this.矩形2带方向.Click += new System.EventHandler(this.矩形2带方向_Click);
            // 
            // 圆
            // 
            resources.ApplyResources(this.圆, "圆");
            this.圆.Name = "圆";
            this.圆.Click += new System.EventHandler(this.圆_Click);
            // 
            // 任意形状
            // 
            resources.ApplyResources(this.任意形状, "任意形状");
            this.任意形状.Name = "任意形状";
            this.任意形状.Click += new System.EventHandler(this.任意形状_Click);
            // 
            // 直线
            // 
            resources.ApplyResources(this.直线, "直线");
            this.直线.Name = "直线";
            this.直线.Click += new System.EventHandler(this.直线_Click);
            // 
            // 显示设置
            // 
            resources.ApplyResources(this.显示设置, "显示设置");
            this.显示设置.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.默认设置,
            this.线条,
            this.颜色,
            this.显示轮廓,
            this.填充显示,
            this.字体大小,
            this.图像镜像});
            this.显示设置.Name = "显示设置";
            // 
            // 默认设置
            // 
            resources.ApplyResources(this.默认设置, "默认设置");
            this.默认设置.Name = "默认设置";
            this.默认设置.Click += new System.EventHandler(this.默认设置_Click);
            // 
            // 线条
            // 
            resources.ApplyResources(this.线条, "线条");
            this.线条.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ComboBox_LineWith});
            this.线条.Name = "线条";
            // 
            // ComboBox_LineWith
            // 
            resources.ApplyResources(this.ComboBox_LineWith, "ComboBox_LineWith");
            this.ComboBox_LineWith.Items.AddRange(new object[] {
            resources.GetString("ComboBox_LineWith.Items"),
            resources.GetString("ComboBox_LineWith.Items1"),
            resources.GetString("ComboBox_LineWith.Items2"),
            resources.GetString("ComboBox_LineWith.Items3"),
            resources.GetString("ComboBox_LineWith.Items4"),
            resources.GetString("ComboBox_LineWith.Items5"),
            resources.GetString("ComboBox_LineWith.Items6")});
            this.ComboBox_LineWith.Name = "ComboBox_LineWith";
            this.ComboBox_LineWith.SelectedIndexChanged += new System.EventHandler(this.ComboBox_LineWith_SelectedIndexChanged);
            this.ComboBox_LineWith.Click += new System.EventHandler(this.ComboBox_LineWith_Click);
            // 
            // 颜色
            // 
            resources.ApplyResources(this.颜色, "颜色");
            this.颜色.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.red,
            this.green,
            this.蓝色,
            this.黄色});
            this.颜色.Name = "颜色";
            // 
            // red
            // 
            resources.ApplyResources(this.red, "red");
            this.red.Name = "red";
            this.red.Click += new System.EventHandler(this.red_Click);
            // 
            // green
            // 
            resources.ApplyResources(this.green, "green");
            this.green.Name = "green";
            this.green.Click += new System.EventHandler(this.green_Click);
            // 
            // 蓝色
            // 
            resources.ApplyResources(this.蓝色, "蓝色");
            this.蓝色.Name = "蓝色";
            this.蓝色.Click += new System.EventHandler(this.蓝色_Click);
            // 
            // 黄色
            // 
            resources.ApplyResources(this.黄色, "黄色");
            this.黄色.Name = "黄色";
            this.黄色.Click += new System.EventHandler(this.黄色_Click);
            // 
            // 显示轮廓
            // 
            resources.ApplyResources(this.显示轮廓, "显示轮廓");
            this.显示轮廓.Name = "显示轮廓";
            this.显示轮廓.Click += new System.EventHandler(this.显示轮廓_Click);
            // 
            // 填充显示
            // 
            resources.ApplyResources(this.填充显示, "填充显示");
            this.填充显示.Name = "填充显示";
            this.填充显示.Click += new System.EventHandler(this.填充显示_Click);
            // 
            // 字体大小
            // 
            this.字体大小.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ComboBox_FontSize});
            resources.ApplyResources(this.字体大小, "字体大小");
            this.字体大小.Name = "字体大小";
            // 
            // ComboBox_FontSize
            // 
            this.ComboBox_FontSize.Items.AddRange(new object[] {
            resources.GetString("ComboBox_FontSize.Items"),
            resources.GetString("ComboBox_FontSize.Items1"),
            resources.GetString("ComboBox_FontSize.Items2"),
            resources.GetString("ComboBox_FontSize.Items3"),
            resources.GetString("ComboBox_FontSize.Items4"),
            resources.GetString("ComboBox_FontSize.Items5"),
            resources.GetString("ComboBox_FontSize.Items6"),
            resources.GetString("ComboBox_FontSize.Items7"),
            resources.GetString("ComboBox_FontSize.Items8"),
            resources.GetString("ComboBox_FontSize.Items9"),
            resources.GetString("ComboBox_FontSize.Items10"),
            resources.GetString("ComboBox_FontSize.Items11"),
            resources.GetString("ComboBox_FontSize.Items12"),
            resources.GetString("ComboBox_FontSize.Items13"),
            resources.GetString("ComboBox_FontSize.Items14"),
            resources.GetString("ComboBox_FontSize.Items15"),
            resources.GetString("ComboBox_FontSize.Items16"),
            resources.GetString("ComboBox_FontSize.Items17"),
            resources.GetString("ComboBox_FontSize.Items18"),
            resources.GetString("ComboBox_FontSize.Items19"),
            resources.GetString("ComboBox_FontSize.Items20"),
            resources.GetString("ComboBox_FontSize.Items21"),
            resources.GetString("ComboBox_FontSize.Items22"),
            resources.GetString("ComboBox_FontSize.Items23"),
            resources.GetString("ComboBox_FontSize.Items24"),
            resources.GetString("ComboBox_FontSize.Items25"),
            resources.GetString("ComboBox_FontSize.Items26")});
            this.ComboBox_FontSize.Name = "ComboBox_FontSize";
            resources.ApplyResources(this.ComboBox_FontSize, "ComboBox_FontSize");
            this.ComboBox_FontSize.Click += new System.EventHandler(this.ComboBox_FontSize_Click);
            // 
            // 图像镜像
            // 
            this.图像镜像.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoR,
            this.UoD});
            resources.ApplyResources(this.图像镜像, "图像镜像");
            this.图像镜像.Name = "图像镜像";
            // 
            // LoR
            // 
            this.LoR.Name = "LoR";
            resources.ApplyResources(this.LoR, "LoR");
            this.LoR.Click += new System.EventHandler(this.LoR_Click);
            // 
            // UoD
            // 
            this.UoD.Name = "UoD";
            resources.ApplyResources(this.UoD, "UoD");
            this.UoD.Click += new System.EventHandler(this.UoD_Click);
            // 
            // 清除
            // 
            resources.ApplyResources(this.清除, "清除");
            this.清除.Name = "清除";
            this.清除.Click += new System.EventHandler(this.清除_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.but_ImageList);
            this.panel2.Controls.Add(this.but_LoadImage);
            this.panel2.Controls.Add(this.but_RecoverImg);
            this.panel2.Controls.Add(this.but_SaveImg);
            this.panel2.Controls.Add(this.but_GrabImage);
            this.panel2.Controls.Add(this.but_Live);
            this.panel2.Controls.Add(this.label_x);
            this.panel2.Controls.Add(this.label_d);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // but_ImageList
            // 
            this.but_ImageList.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.but_ImageList, "but_ImageList");
            this.but_ImageList.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.but_ImageList.FlatAppearance.BorderSize = 0;
            this.but_ImageList.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.but_ImageList.Name = "but_ImageList";
            this.but_ImageList.UseVisualStyleBackColor = false;
            this.but_ImageList.Click += new System.EventHandler(this.but_ImageList_Click);
            // 
            // but_LoadImage
            // 
            this.but_LoadImage.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.but_LoadImage, "but_LoadImage");
            this.but_LoadImage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.but_LoadImage.FlatAppearance.BorderSize = 0;
            this.but_LoadImage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.but_LoadImage.Name = "but_LoadImage";
            this.but_LoadImage.UseVisualStyleBackColor = false;
            this.but_LoadImage.Click += new System.EventHandler(this.but_LoadImage_Click);
            // 
            // but_RecoverImg
            // 
            this.but_RecoverImg.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.but_RecoverImg, "but_RecoverImg");
            this.but_RecoverImg.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.but_RecoverImg.FlatAppearance.BorderSize = 0;
            this.but_RecoverImg.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.but_RecoverImg.Name = "but_RecoverImg";
            this.but_RecoverImg.UseVisualStyleBackColor = false;
            this.but_RecoverImg.Click += new System.EventHandler(this.but_RecoverImg_Click);
            // 
            // but_SaveImg
            // 
            this.but_SaveImg.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.but_SaveImg, "but_SaveImg");
            this.but_SaveImg.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.but_SaveImg.FlatAppearance.BorderSize = 0;
            this.but_SaveImg.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.but_SaveImg.Name = "but_SaveImg";
            this.but_SaveImg.UseVisualStyleBackColor = false;
            this.but_SaveImg.Click += new System.EventHandler(this.but_SaveImg_Click);
            // 
            // but_GrabImage
            // 
            this.but_GrabImage.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.but_GrabImage, "but_GrabImage");
            this.but_GrabImage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.but_GrabImage.FlatAppearance.BorderSize = 0;
            this.but_GrabImage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.but_GrabImage.Name = "but_GrabImage";
            this.but_GrabImage.UseVisualStyleBackColor = false;
            this.but_GrabImage.Click += new System.EventHandler(this.but_GrabImage_Click);
            // 
            // but_Live
            // 
            this.but_Live.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.but_Live, "but_Live");
            this.but_Live.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.but_Live.FlatAppearance.BorderSize = 0;
            this.but_Live.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.but_Live.Name = "but_Live";
            this.but_Live.UseVisualStyleBackColor = false;
            this.but_Live.Click += new System.EventHandler(this.but_Live_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.toolStrip2, "toolStrip2");
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_Label_CamSer,
            this.ts_Label_state,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripLabel_pos,
            this.toolStripLabel2,
            this.toolStripLabel_gray});
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // ts_Label_CamSer
            // 
            resources.ApplyResources(this.ts_Label_CamSer, "ts_Label_CamSer");
            this.ts_Label_CamSer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ts_Label_CamSer.Name = "ts_Label_CamSer";
            // 
            // ts_Label_state
            // 
            this.ts_Label_state.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.ts_Label_state, "ts_Label_state");
            this.ts_Label_state.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ts_Label_state.Name = "ts_Label_state";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // toolStripLabel_pos
            // 
            resources.ApplyResources(this.toolStripLabel_pos, "toolStripLabel_pos");
            this.toolStripLabel_pos.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripLabel_pos.Name = "toolStripLabel_pos";
            // 
            // toolStripLabel2
            // 
            resources.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
            this.toolStripLabel2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripLabel2.Name = "toolStripLabel2";
            // 
            // toolStripLabel_gray
            // 
            resources.ApplyResources(this.toolStripLabel_gray, "toolStripLabel_gray");
            this.toolStripLabel_gray.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripLabel_gray.Name = "toolStripLabel_gray";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // groupBox_ImageList
            // 
            this.groupBox_ImageList.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox_ImageList.Controls.Add(this.tableLayoutPanel1);
            this.groupBox_ImageList.Controls.Add(this.toolStrip3);
            resources.ApplyResources(this.groupBox_ImageList, "groupBox_ImageList");
            this.groupBox_ImageList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox_ImageList.Name = "groupBox_ImageList";
            this.groupBox_ImageList.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.listView_ImgList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.but_PreImage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.but_NextImage, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // listView_ImgList
            // 
            resources.ApplyResources(this.listView_ImgList, "listView_ImgList");
            this.listView_ImgList.FullRowSelect = true;
            this.listView_ImgList.HideSelection = false;
            this.listView_ImgList.Name = "listView_ImgList";
            this.listView_ImgList.UseCompatibleStateImageBehavior = false;
            this.listView_ImgList.Click += new System.EventHandler(this.listView_ImgList_Click);
            // 
            // but_PreImage
            // 
            resources.ApplyResources(this.but_PreImage, "but_PreImage");
            this.but_PreImage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.but_PreImage.Name = "but_PreImage";
            this.but_PreImage.UseVisualStyleBackColor = true;
            this.but_PreImage.Click += new System.EventHandler(this.but_PreImage_Click);
            // 
            // but_NextImage
            // 
            resources.ApplyResources(this.but_NextImage, "but_NextImage");
            this.but_NextImage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.but_NextImage.Name = "but_NextImage";
            this.but_NextImage.UseVisualStyleBackColor = true;
            this.but_NextImage.Click += new System.EventHandler(this.but_NextImage_Click);
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_But_ImageListClose,
            this.tsBut_LoadOK,
            this.tsBut_LoadNG,
            this.tsBut_LoadFile});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            resources.ApplyResources(this.toolStrip3, "toolStrip3");
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // ts_But_ImageListClose
            // 
            this.ts_But_ImageListClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_But_ImageListClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ts_But_ImageListClose, "ts_But_ImageListClose");
            this.ts_But_ImageListClose.Name = "ts_But_ImageListClose";
            this.ts_But_ImageListClose.Click += new System.EventHandler(this.ts_But_ImageListClose_Click);
            // 
            // tsBut_LoadOK
            // 
            this.tsBut_LoadOK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsBut_LoadOK, "tsBut_LoadOK");
            this.tsBut_LoadOK.Name = "tsBut_LoadOK";
            this.tsBut_LoadOK.Click += new System.EventHandler(this.tsBut_LoadOK_Click);
            // 
            // tsBut_LoadNG
            // 
            this.tsBut_LoadNG.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsBut_LoadNG, "tsBut_LoadNG");
            this.tsBut_LoadNG.Name = "tsBut_LoadNG";
            this.tsBut_LoadNG.Click += new System.EventHandler(this.tsBut_LoadNG_Click);
            // 
            // tsBut_LoadFile
            // 
            this.tsBut_LoadFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsBut_LoadFile, "tsBut_LoadFile");
            this.tsBut_LoadFile.Name = "tsBut_LoadFile";
            this.tsBut_LoadFile.Click += new System.EventHandler(this.tsBut_LoadFile_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_hWnd);
            this.panel1.Controls.Add(this.groupBox_ImageList);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.imageList1, "imageList1");
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Controls.Add(this.label_Edit);
            this.panel4.Controls.Add(this.label_Cam);
            this.panel4.Name = "panel4";
            // 
            // label_Edit
            // 
            resources.ApplyResources(this.label_Edit, "label_Edit");
            this.label_Edit.BackColor = System.Drawing.Color.Transparent;
            this.label_Edit.Name = "label_Edit";
            this.label_Edit.Click += new System.EventHandler(this.label_Edit_Click);
            // 
            // label_Cam
            // 
            resources.ApplyResources(this.label_Cam, "label_Cam");
            this.label_Cam.BackColor = System.Drawing.Color.Transparent;
            this.label_Cam.Name = "label_Cam";
            // 
            // FormCamShow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCamShow";
            this.Load += new System.EventHandler(this.FormCamShow_Load);
            this.panel_hWnd.ResumeLayout(false);
            this.panel_hWnd.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox_ImageList.ResumeLayout(false);
            this.groupBox_ImageList.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_hWnd;
        private HalconDotNet.HWindowControl hWndCtrl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 画质调节ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel ts_Label_CamSer;
        private System.Windows.Forms.ToolStripLabel ts_Label_state;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem 绘制;
        private System.Windows.Forms.ToolStripMenuItem 矩形1;
        private System.Windows.Forms.ToolStripMenuItem 矩形2带方向;
        private System.Windows.Forms.ToolStripMenuItem 圆;
        private System.Windows.Forms.ToolStripMenuItem 直线;
        private System.Windows.Forms.ToolStripMenuItem 显示设置;
        private System.Windows.Forms.ToolStripMenuItem 线条;
        private System.Windows.Forms.ToolStripMenuItem 颜色;
        private System.Windows.Forms.ToolStripMenuItem red;
        private System.Windows.Forms.ToolStripMenuItem green;
        private System.Windows.Forms.ToolStripComboBox ComboBox_LineWith;
        private System.Windows.Forms.ToolStripMenuItem 蓝色;
        private System.Windows.Forms.ToolStripMenuItem 黄色;
        private System.Windows.Forms.ToolStripMenuItem 显示轮廓;
        private System.Windows.Forms.ToolStripMenuItem 填充显示;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem 默认设置;
        private System.Windows.Forms.ToolStripMenuItem 清除;
        private System.Windows.Forms.ToolStripMenuItem 字体大小;
        private System.Windows.Forms.ToolStripComboBox ComboBox_FontSize;
        private System.Windows.Forms.ToolStripMenuItem 关联相机;
        private System.Windows.Forms.ToolStripMenuItem 任意形状;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Label label_d;
        public System.Windows.Forms.Label label_x;
        private System.Windows.Forms.ToolStripMenuItem 图像镜像;
        private System.Windows.Forms.ToolStripMenuItem LoR;
        private System.Windows.Forms.ToolStripMenuItem UoD;
        private System.Windows.Forms.GroupBox groupBox_ImageList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton ts_But_ImageListClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button but_PreImage;
        private System.Windows.Forms.Button but_NextImage;
        private System.Windows.Forms.ListView listView_ImgList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton tsBut_LoadOK;
        private System.Windows.Forms.ToolStripButton tsBut_LoadNG;
        private System.Windows.Forms.ToolStripButton tsBut_LoadFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button but_ImageList;
        private System.Windows.Forms.Button but_LoadImage;
        private System.Windows.Forms.Button but_RecoverImg;
        private System.Windows.Forms.Button but_SaveImg;
        private System.Windows.Forms.Button but_GrabImage;
        private System.Windows.Forms.Button but_Live;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_pos;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_gray;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label_Edit;
        private System.Windows.Forms.Label label_Cam;
    }
}