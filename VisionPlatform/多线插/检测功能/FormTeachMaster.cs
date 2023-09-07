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
using VisionPlatform.Auxiliary;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using EnumData;
using System.Collections;

namespace VisionPlatform
{
    public partial class FormTeachMaster : Form
    {
        int m_cam;                                           //导入第几个相机
        int sub_cam;                                         //相机界面编号
        int nPreIO_in;                                       //输入/输出的IO点位
        private string m_SelNode_z = null;                   //关联相机时选中的根节点
        TMData.CamInspectItem m_item = new TMData.CamInspectItem();//当前选中的检测项目
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
            if (0 == sub_cam)
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
        }
        private void Refresh(int cam, int sub_cam)
        {
            try
            {
                int camNum = 0;
                for (int i = 0; i < _InitConfig.initConfig.CamNum; i++)
                {
                    camNum++;
                    if (_InitConfig.initConfig.dic_SubCam[i + 1] != 0)
                    {
                        camNum = camNum + _InitConfig.initConfig.dic_SubCam[i + 1];
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
                    default:
                        this.panelWindow.Controls.Clear();
                        FormMainUI.m_dicFormCamShows[cam][sub_cam].form.TopLevel = false;
                        FormMainUI.m_dicFormCamShows[cam][sub_cam].form.Visible = true;
                        FormMainUI.m_dicFormCamShows[cam][sub_cam].form.Dock = DockStyle.Fill;
                        this.panelWindow.Controls.Add(FormMainUI.m_dicFormCamShows[cam][sub_cam].form);
                        break;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
        private void InitTreeView()
        {
            try
            {
                if (TMData_Serializer._globalData.dicInspectLists.Count != 0)
                {
                    foreach (var dicItem in TMData_Serializer._globalData.dicInspectLists)
                    {
                        foreach (var items in dicItem.Value)
                        {
                            int cam = dicItem.Key;
                            int sub_cam = items.Key;
                            string strCam = "";
                            if (sub_cam >= 1)
                            {
                                strCam = "相机" + cam.ToString() + "-" + sub_cam.ToString();
                            }
                            else
                            {
                                strCam = "相机" + cam.ToString();
                            }
                            treeViewFun.Nodes.Add(strCam);
                            if (items.Value.Count != 0)
                            {
                                foreach (var item in items.Value)
                                {
                                    TreeNode nodeItem = new TreeNode();
                                    if (item.data.Count != 0)
                                    {
                                        foreach (KeyValuePair<InspectItem, Dictionary<SurfaceType, List<DetectionType>>> LInspect in item.data)
                                        {
                                            InspectItem Inspect = LInspect.Key;
                                            nodeItem.Text = TMFunction.GetStrCheckItem(Inspect);
                                            treeViewFun.Nodes[treeViewFun.Nodes.Count - 1].Nodes.Add(nodeItem);
                                            foreach (KeyValuePair<SurfaceType, List<DetectionType>> LSurfaceType in LInspect.Value )
                                            {
                                                SurfaceType surfaceType = LSurfaceType.Key;
                                                if (surfaceType != SurfaceType.Defult)
                                                {
                                                    TreeNode surfaceItem = new TreeNode();
                                                    surfaceItem.Text = TMFunction.GetStrSurfaceType(surfaceType);
                                                    treeViewFun.Nodes[treeViewFun.Nodes.Count - 1].Nodes[
                                                        treeViewFun.Nodes[treeViewFun.Nodes.Count - 1].Nodes.Count - 1].
                                                        Nodes.Add(surfaceItem);
                                                    foreach (var type in LSurfaceType.Value)
                                                    {
                                                        TreeNode typeItem = new TreeNode();
                                                        typeItem.Text = TMFunction.GetStrDetectionType(type);
                                                        treeViewFun.Nodes[treeViewFun.Nodes.Count - 1].Nodes[
                                                            treeViewFun.Nodes[treeViewFun.Nodes.Count - 1].Nodes.Count - 1].Nodes[
                                                            treeViewFun.Nodes[treeViewFun.Nodes.Count - 1].Nodes[
                                                                treeViewFun.Nodes[treeViewFun.Nodes.Count - 1].Nodes.Count - 1].Nodes.Count - 1
                                                            ].Nodes.Add(typeItem);
                                                    }
                                                }
                                                else
                                                {
                                                    foreach (var type in LSurfaceType.Value)
                                                    {
                                                        TreeNode typeItem = new TreeNode();
                                                        typeItem.Text = TMFunction.GetStrDetectionType(type);
                                                        treeViewFun.Nodes[treeViewFun.Nodes.Count - 1].Nodes[
                                                        treeViewFun.Nodes[treeViewFun.Nodes.Count - 1].Nodes.Count - 1].
                                                        Nodes.Add(typeItem);
                                                    }
                                                }
                                            }
                                            
                                        }
                                    }
                                }
                            }
                            
                        }
                    }
                }
                else
                {
                    foreach (int cam in _InitConfig.initConfig.dic_SubCam.Keys)
                    {
                        string strCam = "相机" + cam.ToString();
                        treeViewFun.Nodes.Add(strCam);
                        int num = _InitConfig.initConfig.dic_SubCam[cam];
                        for (int i = 0; i < num; i++)
                        {
                            strCam = "相机" + cam.ToString() + "-" + (i + 1).ToString();
                            treeViewFun.Nodes.Add(strCam);
                        }
                    }
                }
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
            if (node.Text == "端子检测")
            {
                TreeNode node1 = new TreeNode();
                node1.Text = "公头";
                node.Nodes.Add(node1);
                TreeNode node2 = new TreeNode();
                node2.Text = "母头";
                node.Nodes.Add(node2);
            }
            else if (node.Text == "同心度检测")
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
                for (int i = 0; i < _InitConfig.initConfig.CamNum; i++)
                {
                    camNum++;
                    if (_InitConfig.initConfig.dic_SubCam[i + 1] != 0)
                    {
                        camNum = camNum + _InitConfig.initConfig.dic_SubCam[i + 1];
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
                        StaticFun.UIConfig.RefeshCamShow(FormMainUI.m_Show2.tLPanel_CamShow, FormMainUI.m_dicFormCamShows);
                        FormMainUI.m_Show2.splitContainer1.Panel2.Controls.Clear();
                        FormMainUI.m_Show2.splitContainer1.Panel2.Controls.Add(FormMainUI.formShowResult);
                        FormMainUI.m_PanelShow.Controls.Add(FormMainUI.m_Show2);
                        //UIConfig.RefreshSTATS(FormMainUI.m_Show7.tLPanel2, out TMFunction.m_ListFormSTATS, 2);
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
                        FormMainUI.m_Show4.tableLayoutPanel1.Controls.Add(FormMainUI.formShowResult, 0, 2);
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

        private void AddCheckItem(object sender, EventArgs e)
        {
            try
            {
                string strItem = sender.ToString();
                if (m_SelNode_z != null)
                {
                    string strCam = m_SelNode_z;
                    if (null == treeViewFun.SelectedNode.Parent)
                    {
                        bool bContain = false;
                        #region 是否已经存在该检测项目
                        //foreach (TreeNode node in treeViewFun.SelectedNode.Nodes)
                        //{
                        //    if (node.Text == strItem)
                        //    {
                        //        bContain = true;
                        //        CheckNodeAdd(node);
                        //        break;
                        //    }
                        //    if (node.Nodes.Count > 0)
                        //    {
                        //        foreach (TreeNode node1 in node.Nodes)
                        //        {
                        //            if (node1.Text == strItem)
                        //            {
                        //                bContain = true;
                        //                break;
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion

                        if (!bContain)
                        {
                            TreeNode node = new TreeNode();
                            if (sender is ToolStripMenuItem menuItem)
                            {
                                string OwnerOne = menuItem.OwnerItem.Text;
                                if (OwnerOne == "同心度检测")
                                {
                                    TreeNode node_one = new TreeNode();
                                    node_one.Text = strItem;
                                    bool bFlag = false;
                                    foreach (TreeNode selNode in treeViewFun.SelectedNode.Nodes)
                                    {
                                        if (selNode.Text == "同心度检测")
                                        {
                                            selNode.Nodes.Add(node_one);
                                            bFlag = true;
                                            break;
                                        }
                                    }
                                    if (!bFlag)
                                    {
                                        node.Text = "同心度检测";
                                        node.Nodes.Add(node_one);
                                        treeViewFun.SelectedNode.Nodes.Add(node);
                                    }
                                }
                                else
                                {
                                    string OwnerTwo = menuItem.OwnerItem.OwnerItem.Text;
                                    if (OwnerTwo == "端子检测")
                                    {
                                        TreeNode node_one = new TreeNode();
                                        TreeNode node_two = new TreeNode();
                                        node_one.Text = OwnerOne;
                                        node_two.Text = strItem;
                                        bool bFlag = false;
                                        foreach (TreeNode selNode in treeViewFun.SelectedNode.Nodes)
                                        {
                                            if (selNode.Text == "端子检测")
                                            {
                                                bool bF = false;
                                                foreach (TreeNode selNode_one in treeViewFun.SelectedNode.LastNode.Nodes)
                                                {
                                                    if (selNode_one.Text == OwnerOne)
                                                    {
                                                        selNode_one.Nodes.Add(strItem);
                                                        bF = true;
                                                        bFlag = true;
                                                        break;
                                                    }
                                                }
                                                if (!bF)
                                                {
                                                    selNode.Nodes.Add(node_one);
                                                    node_one.Nodes.Add(strItem);
                                                    bFlag = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (!bFlag)
                                        {
                                            node.Text = "端子检测";
                                            node.Nodes.Add(node_one);
                                            node_one.Nodes.Add(strItem);
                                            treeViewFun.SelectedNode.Nodes.Add(node);
                                        }
                                    }
                                    else if (OwnerTwo == "导体检测")
                                    {
                                        TreeNode node_one = new TreeNode();
                                        TreeNode node_two = new TreeNode();
                                        node_one.Text = OwnerOne;
                                        node_two.Text = strItem;
                                        bool bFlag = false;
                                        foreach (TreeNode selNode in treeViewFun.SelectedNode.Nodes)
                                        {
                                            if (selNode.Text == "导体检测")
                                            {
                                                bool bF = false;
                                                foreach (TreeNode selNode_one in treeViewFun.SelectedNode.LastNode.Nodes)
                                                {
                                                    if (selNode_one.Text == OwnerOne)
                                                    {
                                                        selNode_one.Nodes.Add(strItem);
                                                        bF = true;
                                                        bFlag = true;
                                                        break;
                                                    }
                                                }
                                                if (!bF)
                                                {
                                                    selNode.Nodes.Add(node_one);
                                                    node_one.Nodes.Add(strItem);
                                                    bFlag = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (!bFlag)
                                        {
                                            node.Text = "导体检测";
                                            node.Nodes.Add(node_one);
                                            node_one.Nodes.Add(strItem);
                                            treeViewFun.SelectedNode.Nodes.Add(node);
                                        }
                                    }
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
                foreach (TreeNode node in treeViewFun.Nodes)
                {
                    TreeNode SelNode = node;
                    CamShowParam camShowParam = new CamShowParam()
                    {
                        cam = 0,
                        sub_cam = 0,
                    };
                    while (null != SelNode.Parent)
                    {
                        if (SelNode.Parent != null)
                        {
                            SelNode = SelNode.Parent;
                        }
                    }
                    camShowParam.cam = int.Parse(SelNode.Text.Substring(2, 1));
                    if (SelNode.Text.Length > 3)
                    {
                        camShowParam.sub_cam = int.Parse(SelNode.Text.Substring(4, 1));
                    }
                    //检测项定义
                    List<InspectItemAll> strCheckList = new List<InspectItemAll>();
                    foreach (TreeNode node1 in node.Nodes)
                    {
                        InspectItemAll inspectItemAll = new InspectItemAll()
                        {
                            data = new Dictionary<InspectItem, Dictionary<SurfaceType, List<DetectionType>>>(),
                        };
                        
                        TMFunction.GetCheckItem(node1.Text, out InspectItem item);
                        
                        if (node1.Text == "同心度检测")
                        {
                            Dictionary<SurfaceType, List<DetectionType>> LsurfaceType = new Dictionary<SurfaceType, List<DetectionType>>();
                            List<DetectionType> Ltype = new List<DetectionType>();
                            foreach (TreeNode node2 in node1.Nodes)
                            {
                                TMFunction.GetDetectionType(node2.Text, out DetectionType detectionType);
                                Ltype.Add(detectionType);
                            }
                            LsurfaceType.Add(SurfaceType.Defult, Ltype);
                            inspectItemAll.data.Add(item, LsurfaceType);
                        }
                        else if (node1.Text == "端子检测" || node1.Text == "导体检测")
                        {
                            Dictionary<SurfaceType, List<DetectionType>> LsurfaceType = new Dictionary<SurfaceType, List<DetectionType>>();
                            foreach (TreeNode node2 in node1.Nodes)
                            {
                                TMFunction.GetSurfaceType(node2.Text, out SurfaceType surfaceType);
                                List<DetectionType> Ltype = new List<DetectionType>();
                                foreach (TreeNode node3 in node2.Nodes)
                                {
                                    TMFunction.GetDetectionType(node3.Text, out DetectionType detectionType);
                                    Ltype.Add(detectionType);
                                }
                                LsurfaceType.Add(surfaceType, Ltype);
                            }
                            inspectItemAll.data.Add(item, LsurfaceType);
                        }
                        strCheckList.Add(inspectItemAll);
                    }
                    if (TMData_Serializer._globalData.dicInspectLists.ContainsKey(camShowParam.cam))
                    {
                        if (TMData_Serializer._globalData.dicInspectLists[camShowParam.cam].ContainsKey(camShowParam.sub_cam))
                        {
                            TMData_Serializer._globalData.dicInspectLists[camShowParam.cam][camShowParam.sub_cam] = strCheckList;
                        }
                        else
                        {
                            TMData_Serializer._globalData.dicInspectLists[camShowParam.cam].Add(camShowParam.sub_cam, strCheckList);
                        }
                        
                    }
                    else
                    {
                        Dictionary<int, List<InspectItemAll>> dicInspectList = new Dictionary<int, List<InspectItemAll>>(); 
                        dicInspectList.Add(camShowParam.sub_cam, strCheckList);
                        TMData_Serializer._globalData.dicInspectLists.Add(camShowParam.cam,dicInspectList);
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
                    //string strCam = m_SelNode_z;
                    //if (strCam == "端子检测" || strCam == "导体检测" || strCam == "同心度检测")
                    //{
                    //    RefreshCheckList();
                    //}
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
            if (strSel == "公头" || strSel == "母头")
            {
                int ncam;
                int sub_cam = 0;
                if (null != treeViewFun.SelectedNode.Parent)
                {
                    TreeNode selNode = treeViewFun.SelectedNode;
                    while (null != selNode.Parent)
                    {
                        if (selNode.Parent != null)
                        {
                            selNode = selNode.Parent;
                        }
                    }
                    label_SelCam.Text = selNode.Text;
                    ts_Label_cam.Text = "编辑：" + selNode.Text;
                    ncam = int.Parse(selNode.Text.Substring(2, 1));
                    if (selNode.Text.Length > 3)
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
                    
                    string OwnerOne = e.Node.Parent.Text;
                    if (OwnerOne == "同心度检测")
                    {
                        if (strSel == "公头")
                        {
                            TMData.ConcentricityType type = ConcentricityType.male;
                            if (strSel == "母头")
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
                    else
                    {
                        string OwnerTwo = e.Node.Parent.Parent.Text;
                        if (OwnerTwo == "导体检测")
                        {
                            TMFunction.GetSurfaceType(OwnerOne, out SurfaceType surfaceType);
                            TMFunction.GetDetectionType(strSel, out DetectionType detectionType);
                            Conductor formConductor = new Conductor(m_cam, detectionType, surfaceType)
                            {
                                Visible = true,
                                Dock = DockStyle.Fill
                            };
                            this.panel.Controls.Clear();
                            this.panel.Controls.Add(formConductor);
                        }
                        else if (OwnerTwo == "端子检测")
                        {
                            TMFunction.GetSurfaceType(OwnerOne, out SurfaceType surfaceType);
                            TMFunction.GetDetectionType(strSel, out DetectionType detectionType);
                            FormFAKRA formFAKRA = new FormFAKRA(ncam, sub_cam, detectionType, surfaceType)
                            {
                                TopLevel = false,
                                Visible = true,
                                Dock = DockStyle.Fill
                            };
                            this.panel.Controls.Clear();
                            this.panel.Controls.Add(formFAKRA);

                        }
                    }
                }
            }
        }
        private void treeViewFun_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)//判断点的是不是右键
                {
                    TreeNode selNode = treeViewFun.SelectedNode;
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
                        m_item = TMFunction.GetEnumCheckItem(CurrentNode);
                        contextMenuStrip2.Items.Clear();
                        contextMenuStrip2.Items.Add(del);
                        del.Click += new EventHandler(toolStripMenuItem_Delete_Click);
                        contextMenuStrip2.Items.Add("检测项目");
                        contextMenuStrip2.Items.Add("显示设置");
                        //添加IO点位配置
                        if (_InitConfig.initConfig.comMode.TYPE == EnumData.COMType.IO)
                        {
                            item_IOIn.DropDownItems.Clear();
                            item_IOOut.DropDownItems.Clear();
                            //cb_IO = new ToolStripComboBox();
                            //int total_io = 8;
                            //if (_InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU16)
                            //{
                            //    total_io = 16;
                            //}
                            //else if (_InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU232)
                            //{
                            //    total_io = 12;
                            //}
                            //for (int io = 0; io < total_io; io++)
                            //{
                            //    cb_IO.Items.Add(io);
                            //}
                            //cb_IO.SelectedIndexChanged += new EventHandler(IO_SelectIn_Click);
                            //bool bContain = false;
                            //for (int n = 0; n < TMData_Serializer._COMConfig.listIOSet.Count; n++)
                            //{
                            //    IOSet io = TMData_Serializer._COMConfig.listIOSet[n];
                            //    if (io.camItem.cam == m_cam && io.camItem.item == m_item.item)
                            //    {
                            //        nPreIO_in = io.read;
                            //        bContain = true;
                            //    }
                            //}
                            //if (!bContain)
                            //{
                            //    nPreIO_in = -1;
                            //}
                            //item_IOIn.DropDownItems.Add("当前点位：" + nPreIO_in);
                            //item_IOIn.DropDownItems.Add(cb_IO);
                            contextMenuStrip2.Items.Add(item_IOIn);
                            contextMenuStrip2.Items.Add(item_IOOut);

                        }

                        if (_InitConfig.initConfig.bDigitLight)
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
                ioSet.camItem.item = m_item.item;
                bool bContain = false;
                for (int n = 0; n < TMData_Serializer._COMConfig.listIOSet.Count; n++)
                {
                    IOSet io = TMData_Serializer._COMConfig.listIOSet[n];
                    if (m_cam == io.camItem.cam && m_item.item == io.camItem.item)
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
            
            string c = "";
            if (null!= formFakra)
            {
                c = formFakra.comboBox_Model.Text;
            }
            TreeNode node = CurrentNode;
            while (null != node.Parent)
            {
                node = node.Parent;
            }
            strNode = node.Text;
            int ncam = int.Parse(strNode.Substring(2, 1));
            int sub_cam = 0;
            if (strNode.Length > 3)
            {
                sub_cam = int.Parse(strNode.Substring(4, 1));
            }
            if ("检测项目" == strItem)
            {
                FormTMCheckItem formTMCheckItem = new FormTMCheckItem(ncam, sub_cam, c);
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
            else if ("I/O输入点" == strItem)
            {
                string strDItem = CurrentNode.Parent.Text;
                if (strDItem == "同心度检测")
                {
                    TMFunction.GetDetectionType(m_SelNode_z, out DetectionType detectionType);
                    FormIOGet formIOGet = new FormIOGet(m_cam, sub_cam, InspectItem.Concentricity, default, detectionType);
                    formIOGet.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                    formIOGet.ShowDialog();
                }
                else
                {
                    string strDItem_ = CurrentNode.Parent.Parent.Text;
                    if (strDItem_ == "端子检测")
                    {
                        TMFunction.GetSurfaceType(strDItem, out SurfaceType surfaceType);
                        TMFunction.GetDetectionType(m_SelNode_z, out DetectionType detectionType);
                        FormIOGet formIOGet = new FormIOGet(m_cam, sub_cam, InspectItem.TM, surfaceType, detectionType);
                        formIOGet.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                        formIOGet.ShowDialog();
                    }
                    else if (strDItem_ == "导体检测")
                    {
                        TMFunction.GetSurfaceType(strDItem, out SurfaceType surfaceType);
                        TMFunction.GetDetectionType(m_SelNode_z, out DetectionType detectionType);
                        FormIOGet formIOGet = new FormIOGet(m_cam, sub_cam, InspectItem.Conductor, surfaceType, detectionType);
                        formIOGet.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                        formIOGet.ShowDialog();
                    }
                }
            }

            else if ("I/O输出点" == strItem)
            {
                string strDItem = CurrentNode.Parent.Text;
                if (strDItem == "同心度检测")
                {
                    TMFunction.GetDetectionType(m_SelNode_z, out DetectionType detectionType);
                    FormIOSend formIOSend = new FormIOSend(m_cam, sub_cam, InspectItem.Concentricity, default, detectionType);
                    formIOSend.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                    formIOSend.ShowDialog();
                }
                else
                {
                    string strDItem_ = CurrentNode.Parent.Parent.Text;
                    if (strDItem_ == "端子检测")
                    {
                        TMFunction.GetSurfaceType(strDItem, out SurfaceType surfaceType);
                        TMFunction.GetDetectionType(m_SelNode_z, out DetectionType detectionType);
                        FormIOSend formIOSend = new FormIOSend(m_cam, sub_cam, InspectItem.TM, surfaceType, detectionType);
                        formIOSend.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                        formIOSend.ShowDialog();
                    }
                    else if (strDItem_ == "导体检测")
                    {
                        TMFunction.GetSurfaceType(strDItem, out SurfaceType surfaceType);
                        TMFunction.GetDetectionType(m_SelNode_z, out DetectionType detectionType);
                        FormIOSend formIOSend = new FormIOSend(m_cam, sub_cam, InspectItem.Conductor, surfaceType, detectionType);
                        formIOSend.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                        formIOSend.ShowDialog();
                    }
                }

            }
            else if ("光源控制" == strItem)
            {
                string strDItem = CurrentNode.Parent.Text;
                if (strDItem == "同心度检测")
                {
                    TMFunction.GetDetectionType(m_SelNode_z, out DetectionType detectionType);
                    FormLightCH formLightCH = new FormLightCH(m_cam, sub_cam, InspectItem.Concentricity, default, detectionType);
                    formLightCH.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                    formLightCH.ShowDialog();
                }
                else
                {
                    string strDItem_ = CurrentNode.Parent.Parent.Text;
                    if (strDItem_ == "端子检测")
                    {
                        TMFunction.GetSurfaceType(strDItem, out SurfaceType surfaceType);
                        TMFunction.GetDetectionType(m_SelNode_z, out DetectionType detectionType);
                        FormLightCH formLightCH = new FormLightCH(m_cam, sub_cam, InspectItem.TM, surfaceType, detectionType);
                        formLightCH.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                        formLightCH.ShowDialog();
                    }
                    else if (strDItem_ == "导体检测")
                    {
                        TMFunction.GetSurfaceType(strDItem, out SurfaceType surfaceType);
                        TMFunction.GetDetectionType(m_SelNode_z, out DetectionType detectionType);
                        FormLightCH formLightCH = new FormLightCH(m_cam, sub_cam, InspectItem.Conductor, surfaceType, detectionType);
                        formLightCH.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                        formLightCH.ShowDialog();
                    }
                }
            }
        }
    }
}
