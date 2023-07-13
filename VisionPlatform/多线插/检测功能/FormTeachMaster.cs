using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Chustange.Functional;
using static VisionPlatform.TMData;
using StaticFun;
using static GlobalData.Config;
using System.Diagnostics;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace VisionPlatform
{
    public partial class FormTeachMaster : Form
    {
        int m_cam;                                           //导入第几个相机
        int sub_cam;                                         //相机界面编号
        int nPreIO_in;                                       //输入/输出的IO点位
        private string m_SelNode_z = null;                   //关联相机时选中的根节点
        TMData.InspectItem m_item = new TMData.InspectItem();//当前选中的检测项目
        TreeNode CurrentNode;
        ToolStripComboBox cb_IO = new ToolStripComboBox();  //IO点位
        ToolStripMenuItem item_IOIn = new ToolStripMenuItem("I/O输入点");
        ToolStripMenuItem item_IOOut = new ToolStripMenuItem("I/O输出点");
        ToolStripMenuItem item_light = new ToolStripMenuItem("光源控制");
        ToolStripComboBox cb_light = new ToolStripComboBox(); //光源通道
        ToolStripItem del = new ToolStripMenuItem("删除");
        public static Panel m_panelWindow;
        FormFAKRA formFakra;

        public FormTeachMaster(int n_cam, int sub_cam)
        {
            InitializeComponent();
            m_cam = n_cam;
            this.sub_cam = sub_cam;
            if(0 == sub_cam)
            {
                ts_Label_cam.Text = "编辑：相机" + n_cam.ToString();
                label_SelCam.Text = "相机" + n_cam.ToString();
            }
            else
            {
                ts_Label_cam.Text = "编辑：相机" + n_cam.ToString() + "-" + sub_cam.ToString();
                label_SelCam.Text = "相机" + n_cam.ToString() + "-" + sub_cam.ToString();
            }
            //InitUI();
            LoadUI(n_cam, sub_cam);
        }

        /// <summary>
        /// 解决窗体加载慢、卡顿问题
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;              //用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }
        private void LoadUI(int n_cam, int sub_cam)
        {
            //加载图像显示窗口
            Refresh(n_cam, sub_cam);
            //加载消息显示
            FormMainUI.formShowResult.TopLevel = false;
            FormMainUI.formShowResult.Visible = true;
            FormMainUI.formShowResult.Dock = DockStyle.Fill;
            this.panel_message.Controls.Clear();
            this.panel_message.Controls.Add(FormMainUI.formShowResult);
            FormMainUI.formShowResult.tabPage1.Parent = null;
            //加载左边检测列表
            InitTreeView();
            if (TMData_Serializer._globalData.dicInspectList.ContainsKey(n_cam))
            {
                List<InspectItem> strCheck = TMData_Serializer._globalData.dicInspectList[n_cam];
                foreach (InspectItem strSel in strCheck)
                {
                    if (strSel == InspectItem.StripLen)
                    {
                        //FormStripLength formStripLength = new FormStripLength(n_cam);
                        //formStripLength.TopLevel = false;
                        //formStripLength.Visible = true;
                        //formStripLength.Dock = DockStyle.Fill;
                        //this.panel.Controls.Clear();
                        //this.panel.Controls.Add(formStripLength);
                    }
                    else if (strSel == InspectItem.TM)
                    {
                        //FormFAKRA formFakra = new FormFAKRA(n_cam, b);
                        //formFakra.TopLevel = false;
                        //formFakra.Visible = true;
                        //formFakra.Dock = DockStyle.Fill;
                        //this.panel.Controls.Clear();
                        //this.panel.Controls.Add(formFakra);
                    }
                    else if (strSel == InspectItem.Conductor)
                    {
                        Conductor formConductor = new Conductor(n_cam, ConductorType.front)
                        {
                            Visible = true,
                            Dock = DockStyle.Fill
                        };
                        this.panel.Controls.Clear();
                        this.panel.Controls.Add(formConductor);
                    }
                   
                    else if (strSel == InspectItem.Concentricity_male)
                    {
                        Concentricity formConcentricity = new Concentricity(n_cam, ConcentricityType.male)
                        {
                            Visible = true,
                            Dock = DockStyle.Fill
                        };
                        this.panel.Controls.Clear();
                        this.panel.Controls.Add(formConcentricity);
                    }
                    else if (strSel == InspectItem.Concentricity_female)
                    {
                        Concentricity formConcentricity = new Concentricity(n_cam, ConcentricityType.female)
                        {
                            Visible = true,
                            Dock = DockStyle.Fill
                        };
                        this.panel.Controls.Clear();
                        this.panel.Controls.Add(formConcentricity);
                    }
                }
            }
           
        }
        private void Refresh(int cam, int sub_cam)
        {
            try
            {
                int camNum = 0;
                for (int i = 0; i < GlobalData.Config._InitConfig.initConfig.CamNum; i++)
                {
                    camNum++;
                    if (GlobalData.Config._InitConfig.initConfig.dic_SubCam[i + 1] != 0)
                    {
                        camNum = camNum + GlobalData.Config._InitConfig.initConfig.dic_SubCam[i + 1];
                    }
                }
                switch (camNum)
                {
                    case 1:
                        Show1.formCamShow1.TopLevel = false;
                        Show1.formCamShow1.Visible = true;
                        Show1.formCamShow1.Dock = DockStyle.Fill;
                        this.panelWindow.Controls.Add(Show1.formCamShow1);
                        break;
                    case 2:
                        Show2.dic_formCamShow[cam][sub_cam].form.TopLevel = false;
                        Show2.dic_formCamShow[cam][sub_cam].form.Visible = true;
                        Show2.dic_formCamShow[cam][sub_cam].form.Dock = DockStyle.Fill;
                        this.panelWindow.Controls.Add(Show2.dic_formCamShow[cam][sub_cam].form);
                        break;
                    default:
                        this.panelWindow.Controls.Clear();
                        FormMainUI.m_dicFormCamShows[cam][sub_cam].form.TopLevel = false;
                        FormMainUI.m_dicFormCamShows[cam][sub_cam].form.Visible = true;
                        FormMainUI.m_dicFormCamShows[cam][sub_cam].form.Dock = DockStyle.Fill;
                        this.panelWindow.Controls.Add(FormMainUI.m_dicFormCamShows[cam][sub_cam].form);
                        break;
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }

        }

        private void InitTreeView()
        {
            try
            {
                foreach (int cam in GlobalData.Config._InitConfig.initConfig.dic_SubCam.Keys)
                {
                    string strCam = "相机" + cam.ToString();
                    treeViewFun.Nodes.Add(strCam);
                    int num = GlobalData.Config._InitConfig.initConfig.dic_SubCam[cam];
                    for (int i = 0; i < num; i++)
                    {
                        strCam = "相机" + cam.ToString() + "-" + (i + 1).ToString();
                        treeViewFun.Nodes.Add(strCam);
                    }
                }
                if (TMData_Serializer._globalData.dicInspectList.Count != 0)
                {
                    foreach (int cam in TMData_Serializer._globalData.dicInspectList.Keys)
                    {
                        List<InspectItem> strCheck = TMData_Serializer._globalData.dicInspectList[cam];
                        foreach (InspectItem str in strCheck)
                        {
                            TreeNode node = new TreeNode();
                            node.Text = TMFunction.GetStrCheckItem(str);
                            if ("" != node.Text || !treeViewFun.Nodes[cam - 1].Nodes.ContainsKey(node.Text))
                            {
                                bool bContains = false;
                                foreach(TreeNode node1 in treeViewFun.Nodes[cam - 1].Nodes)
                                {
                                    if(node1.Text == node.Text)
                                    {
                                        bContains = true;
                                    }
                                }
                                if(!bContains)
                                {
                                    treeViewFun.Nodes[cam - 1].Nodes.Add(node);
                                    CheckNodeAdd(node);
                                }
                            }
                        }
                    }
                }
                //treeViewFun.Nodes[ - 1].BackColor = Color.Green;
                //treeViewFun.Nodes[m_j - 1].FirstNode.BackColor = Color.Green;
                treeViewFun.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageFun.ShowMessage(ex.ToString());
            }
        }

        private void CheckNodeAdd(TreeNode node)
        {
            node.Nodes.Clear();
            if (node.Text == "同心度检测")
            {
                TreeNode node1 = new TreeNode();
                node1.Text = "公头";
                node.Nodes.Add(node1);
                TreeNode node2 = new TreeNode();
                node2.Text = "母头";
                node.Nodes.Add(node2);
            }
            else if (node.Text == "导体检测")
            {
                TreeNode node1 = new TreeNode();
                node1.Text = "正面";
                node.Nodes.Add(node1);
                TreeNode node2 = new TreeNode();
                node2.Text = "侧面";
                node.Nodes.Add(node2);
            }
            else if (node.Text == "端子检测")
            {

            }
        }

        private void Teachmaster_Load(object sender, EventArgs e)
        {
            m_panelWindow = this.panelWindow;
        }

        private void ts_But_close_Click(object sender, EventArgs e)
        {
            try
            {
                FormMainUI.formShowResult.tabPage1.Parent = FormMainUI.formShowResult.tabControl1;
                FormMainUI.formShowResult.tabControl1.SelectedTab = FormMainUI.formShowResult.tabPage1;
                int camNum = 0;
                for(int i=0;i < GlobalData.Config._InitConfig.initConfig.CamNum; i++)
                {
                    camNum++;
                    if (GlobalData.Config._InitConfig.initConfig.dic_SubCam[i+1]!=0)
                    {
                        camNum = camNum + GlobalData.Config._InitConfig.initConfig.dic_SubCam[i + 1];
                    }
                }
                FormMainUI.m_PanelShow.Controls.Clear();
                switch (camNum)
                {
                    case 1:
                        //FormMainUI.m_Show1.Run();
                        FormMainUI.m_PanelShow.Controls.Add(FormMainUI.m_Show1);
                        FormMainUI.m_Show1.panel1.Controls.Clear();
                        FormMainUI.m_Show1.panel1.Controls.Add(Show1.formCamShow1);
                        FormMainUI.m_Show1.splitContainer1.Panel2.Controls.Clear();
                        FormMainUI.m_Show1.splitContainer1.Panel2.Controls.Add(FormMainUI.formShowResult);
                        UIConfig.RefreshSTATS(FormMainUI.m_Show1.tLPanel, out TMFunction.m_ListFormSTATS);
                        break;
                    case 2:
                        
                        foreach(int cam in Show2.dic_formCamShow.Keys)
                        {
                            for(int n=0;n< Show2.dic_formCamShow[cam].Length;n++)
                            {
                                TMData.ShowItems showItems = Show2.dic_formCamShow[cam][n];
                                showItems.panel.Controls.Clear();
                                showItems.panel.Controls.Add(showItems.form);
                            }
                        }
                        FormMainUI.m_Show2.splitContainer1.Panel2.Controls.Clear();
                        FormMainUI.m_Show2.splitContainer1.Panel2.Controls.Add(FormMainUI.formShowResult);
                        FormMainUI.m_PanelShow.Controls.Add(FormMainUI.m_Show2);
                        //UIConfig.RefreshSTATS(FormMainUI.m_Show2.tLPanel, out TMFunction.m_ListFormSTATS);
                        break;
                    case 3:
                        StaticFun.UIConfig.RefeshCamShow(FormMainUI.m_Show3.tLPanel_CamShow, FormMainUI.m_dicFormCamShows);
                        FormMainUI.m_Show3.panel_Message.Controls.Clear();
                        FormMainUI.m_Show3.panel_Message.Controls.Add(FormMainUI.formShowResult);
                        FormMainUI.m_PanelShow.Controls.Add(FormMainUI.m_Show3);
                        break;
                    case 4:
                        
                        StaticFun.UIConfig.RefeshCamShow(FormMainUI.m_Show4.tLPanel_CamShow, FormMainUI.m_dicFormCamShows);
                        FormMainUI.m_Show4.tableLayoutPanel1.Controls.Add(FormMainUI.formShowResult,0,2);
                        FormMainUI.m_PanelShow.Controls.Add(FormMainUI.m_Show4);
                        //UIConfig.RefreshSTATS(FormMainUI.m_Show7.tLPanel2, out TMFunction.m_ListFormSTATS, 2);
                        break;
                    case 5:
                        StaticFun.UIConfig.RefeshCamShow(FormMainUI.m_Show5.tLPanel_CamShow, FormMainUI.m_dicFormCamShows);
                        FormMainUI.m_Show5.tableLayoutPanel2.Controls.Add(FormMainUI.formShowResult, 0, 1);
                        FormMainUI.m_PanelShow.Controls.Add(FormMainUI.m_Show5);
                        //UIConfig.RefreshSTATS(FormMainUI.m_Show7.tLPanel2, out TMFunction.m_ListFormSTATS, 2);
                        break;
                    case 6:
                        StaticFun.UIConfig.RefeshCamShow(FormMainUI.m_Show6.tableLayoutPanel3, FormMainUI.m_dicFormCamShows);
                        FormMainUI.m_Show6.panel13.Controls.Clear();
                        FormMainUI.m_Show6.panel13.Controls.Add(FormMainUI.formShowResult);
                        FormMainUI.m_PanelShow.Controls.Add(FormMainUI.m_Show6);
                        //UIConfig.RefreshSTATS(FormMainUI.m_Show7.tLPanel2, out TMFunction.m_ListFormSTATS, 2);
                        break;
                    default:
                        break;
                }
                GC.Collect();
                //
            }
            catch (Exception ex)
            {
                MessageFun.ShowMessage(ex.ToString());
            }
            this.Close();
        }

        private void AddCheckItem( object sender, EventArgs e)
        {
            try
            {
                string strItem = sender.ToString();
                if (m_SelNode_z != null)
                {
                    string strCam = m_SelNode_z;
                    if (null == treeViewFun.SelectedNode.Parent)
                    {
                        bool bContain = false;  //是否已经存在该检测项目
                        foreach (TreeNode node in treeViewFun.SelectedNode.Nodes)
                        {
                            if (node.Text == strItem)
                            {
                                bContain = true;
                                CheckNodeAdd(node);
                                break;
                            }
                            if (node.Nodes.Count > 0)
                            {
                                foreach (TreeNode node1 in node.Nodes)
                                {
                                    if (node1.Text == strItem)
                                    {
                                        bContain = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (!bContain)
                        {
                            TreeNode node = new TreeNode();
                            if (strItem == "公头"|| strItem == "母头")
                            {
                                TreeNode node1 = new TreeNode();
                                node1.Text = strItem;
                                bool bFlag = false;
                                foreach (TreeNode selNode in treeViewFun.SelectedNode.Nodes)
                                {
                                    if (selNode.Text == "同心度检测")
                                    {
                                        selNode.Nodes.Add(node1);
                                        bFlag = true;
                                        break;
                                    }
                                }
                                if (!bFlag)
                                {
                                    node.Text = "同心度检测";
                                    node.Nodes.Add(node1);
                                    treeViewFun.SelectedNode.Nodes.Add(node);
                                }
                            }
                            else     
                            {
                                node.Text = strItem;
                                CheckNodeAdd(node);
                                treeViewFun.SelectedNode.Nodes.Add(node);
                            }
                        }
                    }
                    treeViewFun.ExpandAll();
                    RefreshCheckList();
                }
            }
            catch (SystemException ex)
            {
                ex.ToString();
            }
        }

        private void RefreshCheckList()
        {
            try
            {
                foreach (TreeNode node0 in treeViewFun.Nodes)
                {
                    List<InspectItem> strCheckList = new List<InspectItem>();
                    foreach (TreeNode node1 in node0.Nodes)
                    {
                        if(node1.Nodes.Count>0)
                        {
                            foreach(TreeNode node2 in node1.Nodes)
                            {
                                strCheckList.Add(TMFunction.GetEnumCheckItem(node2.Text));
                            }
                        }
                        else
                        {
                            strCheckList.Add(TMFunction.GetEnumCheckItem(node1.Text));
                        }
                    }
                    if (node0.Text == "相机1")
                    {
                        if (TMData_Serializer._globalData.dicInspectList.ContainsKey(1))
                        {
                            TMData_Serializer._globalData.dicInspectList[1] = strCheckList;
                        }
                        else
                        {
                            TMData_Serializer._globalData.dicInspectList.Add(1, strCheckList);
                        }

                    }
                    else if (node0.Text == "相机2")
                    {
                        if (TMData_Serializer._globalData.dicInspectList.ContainsKey(2))
                        {
                            TMData_Serializer._globalData.dicInspectList[2] = strCheckList;
                        }
                        else
                        {
                            TMData_Serializer._globalData.dicInspectList.Add(2, strCheckList);
                        }
                    }
                    else if (node0.Text == "相机3")
                    {
                        if (TMData_Serializer._globalData.dicInspectList.ContainsKey(3))
                        {
                            TMData_Serializer._globalData.dicInspectList[3] = strCheckList;
                        }
                        else
                        {
                            TMData_Serializer._globalData.dicInspectList.Add(3, strCheckList);
                        }
                    }
                    else if (node0.Text == "相机4")
                    {
                        if (TMData_Serializer._globalData.dicInspectList.ContainsKey(4))
                        {
                            TMData_Serializer._globalData.dicInspectList[4] = strCheckList;
                        }
                        else
                        {
                            TMData_Serializer._globalData.dicInspectList.Add(4, strCheckList);
                        }
                    }
                }
            }
            catch (SystemException ex)
            {
                ex.ToString();
            }
        }

        private void toolStripMenuItem_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SelNode_z != null)
                {
                    TreeNode SelNode = treeViewFun.SelectedNode;
                    if (SelNode.Text != m_SelNode_z) return;
                    if (null == SelNode.Parent)
                    {
                        return;
                    }
                    SelNode.Parent.Nodes.Remove(SelNode);
                    string strCam = m_SelNode_z;
                    if (strCam == "剥皮检测" || strCam == "端子检测" || strCam == "导体检测" || strCam == "同心度检测")
                    {
                        RefreshCheckList();
                    }
                }
            }
            catch (SystemException ex)
            {
                ex.ToString();
            }
        }
        private void treeViewFun_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string strSel = treeViewFun.SelectedNode.Text;
            int ncam;
            int sub_cam=0;
            if (null != treeViewFun.SelectedNode.Parent)
            {
                TreeNode selNode = treeViewFun.SelectedNode;
                while (null != selNode.Parent)
                {
                    if(selNode.Parent!=null)
                    {
                        selNode = selNode.Parent;
                    }
                }
                label_SelCam.Text = selNode.Text;
                ts_Label_cam.Text = "编辑：" + selNode.Text;
                ncam = int.Parse(selNode.Text.Substring(2, 1));
                if(selNode.Text.Length>3)
                {
                    sub_cam = int.Parse(selNode.Text.Substring(4, 1));
                }
                Refresh(ncam, sub_cam);
                m_cam = ncam;
                //bh = b;
                e.Node.BackColor = Color.Green;
                e.Node.Parent.BackColor = Color.Green;
                foreach (TreeNode node0 in treeViewFun.Nodes)
                {
                    if (node0 != e.Node.Parent)
                    {
                        node0.BackColor = Color.LightSteelBlue;
                        foreach (TreeNode node1 in node0.Nodes)
                        {
                            if (node1 != e.Node.Parent)
                            {
                                node1.BackColor = Color.LightSteelBlue;
                            }
                        }
                    }
                }
                if (strSel == "端子检测")
                {
                    formFakra = new FormFAKRA(ncam, sub_cam);
                    formFakra.TopLevel = false;
                    formFakra.Visible = true;
                    formFakra.Dock = DockStyle.Fill;
                    this.panel.Controls.Clear();
                    this.panel.Controls.Add(formFakra);
                }
                else if (strSel == "公头" || strSel == "母头")
                {
                    TMData.ConcentricityType type = ConcentricityType.male;
                    if(strSel == "母头")
                    {
                        type = ConcentricityType.female;
                    }
                    Concentricity formConcentricity = new Concentricity(m_cam, type)
                    {
                        Visible = true,
                        Dock = DockStyle.Fill
                    };
                    this.panel.Controls.Clear();
                    this.panel.Controls.Add(formConcentricity);
                }

            }
            //else
            //{
            //    label_SelCam.Text = treeViewFun.SelectedNode.Text;
            //    ts_Label_cam.Text = "编辑：" + treeViewFun.SelectedNode.Text;
            //    ncam = int.Parse(treeViewFun.SelectedNode.Text.Substring(2, 1));
            //    b = treeViewFun.SelectedNode.Text.Substring(4, 1);
            //    Refresh(ncam, b);
            //    m_cam = ncam;
            //    bh = b;
            //    strSel = treeViewFun.SelectedNode.Nodes[0].Text;
            //    e.Node.BackColor = Color.Green;
            //    foreach (TreeNode node0 in treeViewFun.Nodes)
            //    {
            //        if (node0 != e.Node)
            //        {
            //            node0.BackColor = Color.LightSteelBlue;
            //        }
            //    }
            //}
        }
        private void treeViewFun_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)//判断点的是不是右键
                {
                    Point ClickPoint = new Point(e.X, e.Y);
                    CurrentNode = treeViewFun.GetNodeAt(ClickPoint);
                    if (CurrentNode != null && CurrentNode.Parent == null)//判断点的是不是一个节点
                    {
                        CurrentNode.ContextMenuStrip = contextMenuStrip1;
                        treeViewFun.SelectedNode = CurrentNode;
                        m_SelNode_z = CurrentNode.Text;
                    }
                    else if (CurrentNode != null && CurrentNode.Parent != null)
                    {
                        m_item = TMFunction.GetEnumCheckItem(CurrentNode.Text);
                        contextMenuStrip2.Items.Clear();
                        contextMenuStrip2.Items.Add(del);
                        del.Click += new EventHandler(toolStripMenuItem_Delete_Click);
                        contextMenuStrip2.Items.Add("检测项目");
                        contextMenuStrip2.Items.Add("显示设置");
                        //添加IO点位配置
                        if (GlobalData.Config._InitConfig.initConfig.comMode.TYPE == EnumData.COMType.IO)
                        {
                            item_IOIn.DropDownItems.Clear();
                            item_IOOut.DropDownItems.Clear();
                            cb_IO = new ToolStripComboBox();
                            int total_io = 8;
                            if (GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU16)
                            {
                                total_io = 16;
                            }
                            else if (GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU232)
                            {
                                total_io = 12;
                            }
                            for (int io = 0; io < total_io; io++)
                            {
                                cb_IO.Items.Add(io);
                            }
                            cb_IO.SelectedIndexChanged += new EventHandler(IO_SelectIn_Click);
                            bool bContain = false;
                            for (int n = 0; n < TMData_Serializer._COMConfig.listIOSet.Count; n++)
                            {
                                IOSet io = TMData_Serializer._COMConfig.listIOSet[n];
                                if (io.camItem.cam == m_cam && io.camItem.item == m_item)
                                {
                                    nPreIO_in = io.read;
                                    bContain = true;
                                }
                            }
                            if (!bContain)
                            {
                                nPreIO_in = -1;
                            }
                            item_IOIn.DropDownItems.Add("当前点位：" + nPreIO_in);
                            item_IOIn.DropDownItems.Add(cb_IO);
                            contextMenuStrip2.Items.Add(item_IOIn);
                            contextMenuStrip2.Items.Add(item_IOOut);

                        }

                        if (GlobalData.Config._InitConfig.initConfig.bDigitLight)
                        {
                            contextMenuStrip2.Items.Add("光源控制");
                        }
                        CurrentNode.ContextMenuStrip = contextMenuStrip2;
                        treeViewFun.SelectedNode = CurrentNode;
                        m_SelNode_z = CurrentNode.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString().ToLog();
            }
        }
        private void IO_SelectIn_Click(object sender, EventArgs e)
        {
            try
            {
                int nSelIO = cb_IO.SelectedIndex;
                item_IOIn.DropDownItems[0].Text = "当前点位：" + nSelIO;
                IOSet ioSet = new IOSet();
                ioSet.read = nSelIO;
                ioSet.camItem.cam = m_cam;
                ioSet.camItem.item = m_item;
                bool bContain = false;
                for (int n = 0; n < TMData_Serializer._COMConfig.listIOSet.Count; n++)
                {
                    IOSet io = TMData_Serializer._COMConfig.listIOSet[n];
                    if (m_cam == io.camItem.cam && m_item == io.camItem.item)
                    {
                        io.read = nSelIO;
                        TMData_Serializer._COMConfig.listIOSet[n] = io;
                        ioSet = io;
                        bContain = true;
                    }
                }
                if (!bContain)
                {
                    TMData_Serializer._COMConfig.listIOSet.Add(ioSet);
                }
                StaticFun.SaveData.SaveIOConfig(ioSet);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        FormShowSet formShowSet;
        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string strItem = e.ClickedItem.Text;
            string strNode = CurrentNode.Parent.Text;
            TreeNode node = CurrentNode;
            while (null != node.Parent)
            {
                node = node.Parent;
            }
            strNode = node.Text;
            int ncam = int.Parse(strNode.Substring(2, 1));
            int sub_cam = 0;
            if (strNode.Length>3)
            {
                sub_cam = int.Parse(strNode.Substring(4, 1));
            }
            if ("检测项目" == strItem)
            {
                FormTMCheckItem formTMCheckItem = new FormTMCheckItem(ncam, sub_cam.ToString(), c);
                formTMCheckItem.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                formTMCheckItem.ShowDialog();
                //添加界面刷新
                formFakra.Refreshpage();
            }
            else if ("显示设置" == strItem)
            {
                if (null == formShowSet || formShowSet.IsDisposed)
                {
                    formShowSet = new FormShowSet(ncam, sub_cam);
                    formShowSet.TopMost = true;
                    formShowSet.Show();
                }
                else
                {
                    formShowSet.TopMost = true;
                }
            }
            else if ("I/O输出点" == strItem)
            {
                FormIOSend formIOSend = new FormIOSend(m_cam, m_SelNode_z, m_item);
                formIOSend.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                formIOSend.ShowDialog();
            }
            else if ("光源控制" == strItem)
            {
                CamInspectItem camItem = new CamInspectItem();
                camItem.cam = m_cam;
                camItem.item = m_item;
                FormLightCH formLightCH = new FormLightCH(camItem);
                formLightCH.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                formLightCH.ShowDialog();
            }
        }


    }
}
