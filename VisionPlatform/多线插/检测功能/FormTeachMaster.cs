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
        string bh;                                           //相机界面编号
        int m_j;                                           //导入第几个界面
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

        public FormTeachMaster(int n_cam, string b,int j)
        {
            InitializeComponent();
            m_cam = n_cam;
            bh = b;
            m_j = j;
            if(""== b)
            {
                ts_Label_cam.Text = "编辑：相机" + n_cam.ToString();
            }
            else
            {
                ts_Label_cam.Text = "编辑：相机" + n_cam.ToString() + "-" + bh;
            }
            
            label_SelCam.Text = "相机" + n_cam.ToString() + "_" + bh;
            //InitUI();
            LoadUI(n_cam, bh);
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
        private void LoadUI(int n_cam, string b)
        {
            //加载图像显示窗口
            Refresh(n_cam, b);
            //加载消息显示
            FormMainUI.formShowResult.TopLevel = false;
            FormMainUI.formShowResult.Visible = true;
            FormMainUI.formShowResult.Dock = DockStyle.Fill;
            this.panel_message.Controls.Clear();
            this.panel_message.Controls.Add(FormMainUI.formShowResult);
            FormMainUI.formShowResult.tabPage1.Parent = null;
            //加载左边检测列表
            InitTreeView();
            // string a = n_cam.ToString() + b;
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
                        FormFAKRA formFakra = new FormFAKRA(n_cam, b);
                        formFakra.TopLevel = false;
                        formFakra.Visible = true;
                        formFakra.Dock = DockStyle.Fill;
                        this.panel.Controls.Clear();
                        this.panel.Controls.Add(formFakra);
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
                   
                    else if (strSel == InspectItem.Concentricity)
                    {
                        Concentricity formConcentricity = new Concentricity(n_cam, ConcentricityType.male)
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
        private void Refresh(int cam, string b)
        {
            int camNum = GlobalData.Config._InitConfig.initConfig.CamNum;
            this.panelWindow.Controls.Clear();
            string a = cam.ToString() + b;
            switch (camNum)
            {
                case 1:
                    Show1.formCamShow1.TopLevel = false;
                    Show1.formCamShow1.Visible = true;
                    Show1.formCamShow1.Dock = DockStyle.Fill;
                    this.panelWindow.Controls.Add(Show1.formCamShow1);
                    break;
                case 2:
                    if (cam == 1)
                    {
                        Show2.formCamShow1.TopLevel = false;
                        Show2.formCamShow1.Visible = true;
                        Show2.formCamShow1.Dock = DockStyle.Fill;
                        this.panelWindow.Controls.Add(Show2.formCamShow1);
                        break;
                    }
                    if (cam == 2)
                    {
                        Show2.formCamShow2.TopLevel = false;
                        Show2.formCamShow2.Visible = true;
                        Show2.formCamShow2.Dock = DockStyle.Fill;
                        this.panelWindow.Controls.Add(Show2.formCamShow2);
                    }
                    break;
                //case 3:
                //    if (cam == 1)
                //    {
                //        Show3.formCamShow1.TopLevel = false;
                //        Show3.formCamShow1.Visible = true;
                //        Show3.formCamShow1.Dock = DockStyle.Fill;
                //        this.panelWindow.Controls.Add(Show3.formCamShow1);
                //        break;
                //    }
                //    if (cam == 2)
                //    {
                //        Show3.formCamShow2.TopLevel = false;
                //        Show3.formCamShow2.Visible = true;
                //        Show3.formCamShow2.Dock = DockStyle.Fill;
                //        this.panelWindow.Controls.Add(Show3.formCamShow2);

                //    }
                //    if (cam == 3)
                //    {
                //        Show3.formCamShow3.TopLevel = false;
                //        Show3.formCamShow3.Visible = true;
                //        Show3.formCamShow3.Dock = DockStyle.Fill;
                //        this.panelWindow.Controls.Add(Show3.formCamShow3);

                //    }
                //    break;

                case 7:
                    Show7.formCamShows[a].TopLevel = false;
                    Show7.formCamShows[a].Visible = true;
                    Show7.formCamShows[a].Dock = DockStyle.Fill;
                    this.panelWindow.Controls.Add(Show7.formCamShows[a]);
                    Show7.formCamShows[a].label_x.Visible = false;
                    Show7.formCamShows[a].label_d.Visible = false;
                    break;
                default:
                    break;
            }

        }

        private void InitTreeView()
        {
            try
            {
                if (GlobalData.Config._InitConfig.initConfig.CamNum == 1)
                {
                    treeViewFun.Nodes.Add("相机1");
                }
                else if (GlobalData.Config._InitConfig.initConfig.CamNum == 2)
                {
                    treeViewFun.Nodes.Add("相机1");
                    treeViewFun.Nodes.Add("相机2");
                }
                else if (GlobalData.Config._InitConfig.initConfig.CamNum == 4)
                {
                    treeViewFun.Nodes.Add("相机1");
                    treeViewFun.Nodes.Add("相机2");
                    treeViewFun.Nodes.Add("相机3");
                    treeViewFun.Nodes.Add("相机4");
                }
                else if (GlobalData.Config._InitConfig.initConfig.CamNum == 7)
                {

                    treeViewFun.Nodes.Add("相机1_1");
                    treeViewFun.Nodes.Add("相机1_2");
                    treeViewFun.Nodes.Add("相机1_3");
                    treeViewFun.Nodes.Add("相机1_4");
                    treeViewFun.Nodes.Add("相机2_1");
                    treeViewFun.Nodes.Add("相机3_1");

                }
                if (TMData_Serializer._globalData.dicInspectList.Count != 0)
                {
                    foreach (int cam in TMData_Serializer._globalData.dicInspectList.Keys)
                    {
                        List<InspectItem> strCheck = TMData_Serializer._globalData.dicInspectList[cam];
                        switch (cam)
                        {
                            case 1:
                                foreach (InspectItem str in strCheck)
                                {
                                    TreeNode node = new TreeNode();
                                    node.Text = TMFunction.GetStrCheckItem(str);
                                    treeViewFun.Nodes[0].Nodes.Add(node);
                                    ConcentricityAdd(node);
                                    ConductorAdd(node);
                                    TMAdd(node);
                                }
                                break;
                            case 2:
                                foreach (InspectItem str in strCheck)
                                {
                                    TreeNode node = new TreeNode();
                                    node.Text = TMFunction.GetStrCheckItem(str);
                                    treeViewFun.Nodes[1].Nodes.Add(node);
                                    ConcentricityAdd(node);
                                    ConductorAdd(node);
                                    TMAdd(node);
                                }
                                break;
                            case 3:
                                foreach (InspectItem str in strCheck)
                                {
                                    TreeNode node = new TreeNode();
                                    node.Text = TMFunction.GetStrCheckItem(str);
                                    treeViewFun.Nodes[2].Nodes.Add(node);
                                    ConcentricityAdd(node);
                                    ConductorAdd(node);
                                    TMAdd(node);
                                }
                                break;
                            case 4:
                                foreach (InspectItem str in strCheck)
                                {
                                    TreeNode node = new TreeNode();
                                    node.Text = TMFunction.GetStrCheckItem(str);
                                    treeViewFun.Nodes[3].Nodes.Add(node);
                                    ConcentricityAdd(node);
                                    ConductorAdd(node);
                                    TMAdd(node);
                                }
                                break;
                            default:
                                break;
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

        private void ConcentricityAdd(TreeNode node)
        {
            if (node.Text == "同心度检测")
            {
                TreeNode node1 = new TreeNode();
                node1.Text = "公头";
                node.Nodes.Add(node1);
                TreeNode node2 = new TreeNode();
                node2.Text = "母头";
                node.Nodes.Add(node2);
            }
        }
        private void ConductorAdd(TreeNode node)
        {
            if (node.Text == "导体检测")
            {
                TreeNode node1 = new TreeNode();
                node1.Text = "正面";
                node.Nodes.Add(node1);
                TreeNode node2 = new TreeNode();
                node2.Text = "侧面";
                node.Nodes.Add(node2);
            }
        }

        private void TMAdd(TreeNode node)
        {
            if (node.Text == "端子检测")
            {
                TreeNode node1 = new TreeNode();
                node1.Text = "正面";
                node.Nodes.Add(node1);
                TreeNode node2 = new TreeNode();
                node2.Text = "侧面";
                node.Nodes.Add(node2);
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
                //FormMainUI.formShowResult.tabPage1.Parent = FormMainUI.formShowResult.tabControl1;
                //FormMainUI.formShowResult.tabControl1.SelectedTab = FormMainUI.formShowResult.tabPage1;
                int camNum = GlobalData.Config._InitConfig.initConfig.CamNum;
                switch (camNum)
                {
                    case 1:
                        //FormMainUI.m_Show1.Run();
                        FormMainUI.m_PanelShow.Controls.Add(FormMainUI.m_Show1);
                        FormMainUI.m_Show1.panel1.Controls.Clear();
                        FormMainUI.m_Show1.panel1.Controls.Add(Show1.formCamShow1);
                        FormMainUI.m_Show1.splitContainer1.Panel2.Controls.Clear();
                        FormMainUI.m_Show1.splitContainer1.Panel2.Controls.Add(FormMainUI.formShowResult);
                        FormMainUI.formShowResult.tabPage1.Parent = FormMainUI.formShowResult.tabControl1;
                        UIConfig.RefreshSTATS(FormMainUI.m_Show1.tLPanel, out TMFunction.m_ListFormSTATS);
                        break;
                    case 2:
                        FormMainUI.m_PanelShow.Controls.Add(FormMainUI.m_Show2);
                        FormMainUI.m_Show2.panel1.Controls.Clear();
                        FormMainUI.m_Show2.panel1.Controls.Add(Show2.formCamShow1);
                        FormMainUI.m_Show2.panel2.Controls.Clear();
                        FormMainUI.m_Show2.panel2.Controls.Add(Show2.formCamShow2);
                        //FormMainUI.m_Show2.splitContainer1.Panel2.Controls.Clear();
                        //FormMainUI.m_Show2.splitContainer1.Panel2.Controls.Add(FormMainUI.formShowResult);
                        //UIConfig.RefreshSTATS(FormMainUI.m_Show2.tLPanel, out TMFunction.m_ListFormSTATS);
                        break;
                    case 3:
                        //FormMainUI.m_Show3.Run();
                        //FormMainUI.m_PanelShow.Controls.Add(FormMainUI.m_Show3);
                        //FormMainUI.m_Show3.panel1.Controls.Clear();
                        //FormMainUI.m_Show3.panel1.Controls.Add(Show3.formCamShow1);
                        //FormMainUI.m_Show3.panel2.Controls.Clear();
                        //FormMainUI.m_Show3.panel2.Controls.Add(Show3.formCamShow2);
                        //FormMainUI.m_Show3.panel3.Controls.Clear();
                        //FormMainUI.m_Show3.panel3.Controls.Add(Show3.formCamShow3);
                        //FormMainUI.m_Show3.panel_Message.Controls.Clear();
                        //FormMainUI.m_Show3.panel_Message.Controls.Add(FormMainUI.formMessage);
                        break;
                    case 7:
                        //FormMainUI.m_Show7.Run();
                        FormMainUI.m_PanelShow.Controls.Add(FormMainUI.m_Show7);
                        FormMainUI.m_Show7.panel1.Controls.Clear();
                        FormMainUI.m_Show7.panel1.Controls.Add(Show7.formCamShows["11"]);
                        FormMainUI.m_Show7.panel2.Controls.Clear();
                        FormMainUI.m_Show7.panel2.Controls.Add(Show7.formCamShows["12"]);
                        FormMainUI.m_Show7.panel3.Controls.Clear();
                        FormMainUI.m_Show7.panel3.Controls.Add(Show7.formCamShows["13"]);
                        FormMainUI.m_Show7.panel4.Controls.Clear();
                        FormMainUI.m_Show7.panel4.Controls.Add(Show7.formCamShows["14"]);
                        FormMainUI.m_Show7.panel5.Controls.Clear();
                        FormMainUI.m_Show7.panel5.Controls.Add(Show7.formCamShows["21"]);
                        FormMainUI.m_Show7.panel6.Controls.Clear();
                        FormMainUI.m_Show7.panel6.Controls.Add(Show7.formCamShows["31"]);
                        FormMainUI.m_Show7.panel13.Controls.Clear();
                        FormMainUI.m_Show7.panel13.Controls.Add(FormMainUI.formShowResult);
                        //UIConfig.RefreshSTATS(FormMainUI.m_Show7.tLPanel2, out TMFunction.m_ListFormSTATS, 2);
                        //Show7.formCamShow1.label_x.Visible = true;
                        //Show7.formCamShow1.label_d.Visible = true;
                        //Show7.formCamShow2.label_x.Visible = true;
                        //Show7.formCamShow2.label_d.Visible = true;
                        //Show7.formCamShow3.label_x.Visible = true;
                        //Show7.formCamShow3.label_d.Visible = true;
                        //Show7.formCamShow4.label_x.Visible = true;
                        //Show7.formCamShow4.label_d.Visible = true;

                        break;
                    default:
                        break;
                }
                //GC.Collect();
                //
            }
            catch (Exception ex)
            {
                MessageFun.ShowMessage(ex.ToString());
            }
            this.Close();
        }

        private void AddCheckItem(string strItem)
        {
            try
            {
                if (m_SelNode_z != null)
                {
                    string strCam = m_SelNode_z;
                    if (strCam == "相机1" || strCam == "相机2" || strCam == "相机3" || strCam == "相机4")
                    {
                        bool bContain = false;  //是否已经存在该检测项目
                        foreach (TreeNode node in treeViewFun.SelectedNode.Nodes)
                        {
                            if (node.Text == strItem)
                            {
                                bContain = true;
                                break;
                            }
                        }
                        if (!bContain)
                        {
                            treeViewFun.SelectedNode.Nodes.Add(strItem);
                        }
                    }
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
                        strCheckList.Add(TMFunction.GetEnumCheckItem(node1.Text));
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

        private void 剥皮检测_Click(object sender, EventArgs e)
        {
            AddCheckItem("剥皮检测");
        }
        private void 导体检测_Click(object sender, EventArgs e)
        {
            AddCheckItem("导体检测");
        }
        private void 端子检测_Click(object sender, EventArgs e)
        {
            AddCheckItem("端子检测");
        }
        private void 同心度检测_Click(object sender, EventArgs e)
        {
            if(sender.ToString() == "公头")
            {

            }
            AddCheckItem("同心度检测");
        }
        private void toolStripMenuItem_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SelNode_z != null)
                {
                    string strCam = m_SelNode_z;
                    if (strCam == "剥皮检测" || strCam == "插壳检测" || strCam == "端子检测" || strCam == "线序检测" || strCam == "线芯检测")
                    {
                        if (null == treeViewFun.SelectedNode.Parent)
                        {
                            return;
                        }
                        foreach (TreeNode node in treeViewFun.SelectedNode.Parent.Nodes)
                        {
                            if (node.Text == strCam)
                            {
                                treeViewFun.SelectedNode.Parent.Nodes.Remove(node);
                                break;
                            }
                        }

                    }
                    RefreshCheckList();
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
            string b;
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
                b = selNode.Text.Substring(2, 1);
                Refresh(ncam, b);
                m_cam = ncam;
                bh = b;
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
                if (strSel == "FAKRA检测")
                {
                    formFakra = new FormFAKRA(ncam, b);
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
            int ncam = int.Parse(strNode.Substring(2, 1));
            string b = treeViewFun.SelectedNode.Parent.Text.Substring(4, 1);
            string c = formFakra.comboBox_Model.Text;

            if ("检测项目" == strItem)
            {
                FormTMCheckItem formTMCheckItem = new FormTMCheckItem(ncam, b,c);
                formTMCheckItem.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                formTMCheckItem.ShowDialog();
                //添加界面刷新
                formFakra.Refreshpage();
            }
            else if ("显示设置" == strItem)
            {
                if (null == formShowSet || formShowSet.IsDisposed)
                {
                    formShowSet = new FormShowSet(ncam, b);
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
