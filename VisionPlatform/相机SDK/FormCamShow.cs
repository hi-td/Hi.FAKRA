using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BaseData;
using CamSDK;
using Chustange.Functional;
using EnumData;
using HalconDotNet;
using Newtonsoft.Json;

namespace VisionPlatform
{
    public partial class FormCamShow : Form
    {
        FormCamParamSet formCamParamSet;
        public int m_ncam;                      //打开的第几个相机
        public int sub_cam;                     //相机的第x个子画面
        public Function fun;
        public TMFunction TM_fun;
        public string m_strCamSer = "";         //相机的序列号
        private Point m_pMuoseDown;             //用来记录按下鼠标时的坐标位置
        private bool m_bMoving = false;         //是否处于移动状态，初始值为关闭
        private bool bDrawing = false;          //是否处于绘制状态

        /// <summary>
        /// 函数定义
        /// </summary>
        /// <param name="strCamSer"></param> 相机序列号
        /// <param name="ncam"></param>      相机
        /// <param name="sub_cam"></param>   第sub_cam个子相机画面
        public FormCamShow(string strCamSer, int ncam, int sub_cam) //输入相机的序列号
        {
            InitializeComponent();
            panel2.BackColor = Color.FromArgb(255, 0, green: 0, 0);
            InitCamSer();
            m_strCamSer = strCamSer;
            m_ncam = ncam;
            this.sub_cam = sub_cam;
            ts_Label_state.Dock = DockStyle.Right;
            fun = new Function(hWndCtrl);
            TM_fun = new TMFunction(fun);
            if (sub_cam == 0)
            {
                //camimage = new Camimage();
                //camimages.Add(ncam, camimage);
                label_Cam.Text = "相机" + ncam.ToString();
                Init(strCamSer, ncam);
            }
            else
            {
                label_Cam.Text = "相机" + ncam.ToString()+"-"+sub_cam.ToString();
                //camimage = camimages[ncam];
            }
            //camimage.a = ncam;
            //if (funs.ContainsKey(ncam))
            //{
            //    funs[ncam].Add(a, fun);
            //}
            //else
            //{
            //    Dictionary<string, Function> fun_a = new Dictionary<string, Function>();
            //    fun_a.Add(a, fun);
            //    funs.Add(ncam, fun_a);
            //}
            //设置线条粗细
            ComboBox_LineWith.SelectedIndex = 0;
        }
        private void InitCamSer()
        {
            try
            {
                if (0 != CamSDK.CamCommon.m_listCamSer.Count)
                {
                    ((ToolStripDropDownItem)contextMenuStrip1.Items[0]).DropDownItems.Add("(空)");
                    foreach (string strCamID in CamSDK.CamCommon.m_listCamSer)
                    {
                        ToolStripItem ts = new ToolStripMenuItem(strCamID);
                        ts.Text = strCamID;
                        ts.Click += new EventHandler(关联相机_Click);
                        ((ToolStripDropDownItem)contextMenuStrip1.Items[0]).DropDownItems.Add(ts);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString().ToLog();
                return;
            }
        }

        private void Init(string strCamSer, int ncam)
        {
            try
            {
                m_strCamSer = strCamSer;
                m_ncam = ncam;
                ts_Label_CamSer.Text = "相机序号：" + strCamSer.ToString();
                if (GlobalData.Config._CamConfig.ImageMirror.ContainsKey(m_ncam))
                {
                    fun.m_ListImgMirror = GlobalData.Config._CamConfig.ImageMirror[m_ncam];
                    if (fun.m_ListImgMirror.Contains(EnumData.Mirror.Left_Right))
                    {
                        LoR.Checked = true;
                        LoR.BackColor = Color.Green;
                    }
                    if (fun.m_ListImgMirror.Contains(EnumData.Mirror.Up_Down))
                    {
                        UoD.Checked = true;
                        UoD.BackColor = Color.Green;
                    }
                }

                if ("" != strCamSer&& "空" != strCamSer)
                {
                    CamCommon.OpenCam(strCamSer, fun);
                    CamCommon.Live(strCamSer);
                    CamCommon.GrabImage(strCamSer);
                    ts_Label_state.Text = "当前状态：拍照";
                }
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.ToString());
                return;
            }
        }

        private void 画质调节ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == formCamParamSet || formCamParamSet.IsDisposed)
                {
                    formCamParamSet = new FormCamParamSet(m_ncam, m_strCamSer);
                    formCamParamSet.Show();
                }
                else
                {
                    formCamParamSet.Activate(); //使子窗体获得焦点
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                (ex.Message + ex.StackTrace).ToLog();
            }
        }

        private void ts_But_Live_Click(object sender, EventArgs e)
        {

            fun.ClearObjShow();
            CamCommon.Live(m_strCamSer);
            ts_Label_state.Text = "当前状态：实时显示中";
        }

        private void ts_But_GrabImage_Click(object sender, EventArgs e)
        {
            fun.ClearObjShow();
            CamCommon.GrabImage(m_strCamSer);
            ts_Label_state.Text = "当前状态：拍照";
        }

        private void ts_But_RecoverImg_Click(object sender, EventArgs e)
        {
            
        }

        private void hWndCtrl_HMouseDown(object sender, HalconDotNet.HMouseEventArgs e)
        {
            if (fun.m_hImage == null) return;
            if (e.Button == MouseButtons.Left && !bDrawing)
            {
                m_pMuoseDown.X = (int)e.X;
                m_pMuoseDown.Y = (int)e.Y;
                m_bMoving = true;
                ts_Label_state.Text = "当前状态：图像平移中";
            }
            else if (e.Button == MouseButtons.Right)
            {
                m_bMoving = false;
                bDrawing = false;
            }
        }

        private void hWndCtrl_HMouseMove(object sender, HalconDotNet.HMouseEventArgs e)
        {
            HTuple hv_Row = new HTuple(), hv_Col = new HTuple();
            try
            {
                if (fun.m_hImage == null) return;
                if (!bDrawing)
                {
                    if (e.Button == MouseButtons.Left && m_bMoving)
                    {
                        Point pt = new Point();
                        pt.X = (int)e.X;
                        pt.Y = (int)e.Y;
                        fun.MoveImage(m_pMuoseDown, pt);
                        ts_Label_state.Text = "当前状态：图像平移中";
                    }
                }
                HOperatorSet.GetMposition(this.hWndCtrl.HalconWindow, out hv_Row, out hv_Col, out HTuple hv_Button);
                if (hv_Row?.TupleLength() != 0 && hv_Col?.TupleLength() != 0)
                {
                    this.toolStripLabel_pos.Text = $"{hv_Col[0].I.ToString()},{hv_Row[0].I.ToString()}";
                    HOperatorSet.GetGrayval(fun.m_GrayImage, hv_Row, hv_Col, out var grayval);
                    toolStripLabel_gray.Text = grayval[0].D.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                toolStripLabel_gray.Text = "*";
            }

        }

        private void hWndCtrl_HMouseWheel(object sender, HalconDotNet.HMouseEventArgs e)
        {
            try
            {
                if (fun.m_hImage == null) return;
                double scale;
                if (e.Delta >= 0) //鼠标滚轮向上滑动
                {
                    scale = 0.9;
                }
                else
                {
                    scale = 1 / 0.9;
                }
                fun.ZoomImage(e.X, e.Y, scale);
                ts_Label_state.Text = "当前状态：图像缩放中";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void ta_But_SaveImg_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.InitialDirectory = "e:/";
                saveFileDialog.Filter = "BMP图片|*.bmp|JPG图片|*.jpg|Gif图片|*.gif";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string strImageFile = saveFileDialog.FileName;
                    fun.SaveImage(strImageFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void 矩形1_Click(object sender, EventArgs e)
        {
            try
            {
                bDrawing = true;
                hWndCtrl.ContextMenuStrip = null;
                fun.ClearObjShow();
                Rect1 rect1 = fun.DrawRect1();
                fun.m_rect1 = rect1;
                fun.m_lastDraw = ObjDraw.rect1;
                hWndCtrl.ContextMenuStrip = contextMenuStrip1;
            }
            catch (SystemException ex)
            {
                ex.ToString();
                return;
            }
        }

        private void 任意形状_Click(object sender, EventArgs e)
        {
            try
            {
                bDrawing = true;
                hWndCtrl.ContextMenuStrip = null;
                Arbitrary m_arbitrary = fun.DrawArbitrary();
                fun.m_arbitrary = m_arbitrary;
                fun.m_lastDraw = ObjDraw.arbitrary;
                hWndCtrl.ContextMenuStrip = contextMenuStrip1;
            }
            catch (SystemException ex)
            {
                ex.ToString();
                return;
            }
        }

        private void 矩形2带方向_Click(object sender, EventArgs e)
        {
            try
            {
                bDrawing = true;
                hWndCtrl.ContextMenuStrip = null;
                fun.ClearObjShow();
                Rect2 m_Rect2 = fun.DrawRect2();
                fun.m_rect2 = m_Rect2;
                fun.m_lastDraw = ObjDraw.rect2;
                hWndCtrl.ContextMenuStrip = contextMenuStrip1;
            }
            catch (SystemException ex)
            {
                ex.ToString();
                return;
            }
        }

        private void 直线_Click(object sender, EventArgs e)
        {
            try
            {
                bDrawing = true;
                hWndCtrl.ContextMenuStrip = null;
                fun.m_line = fun.DrawLine();
                fun.m_lastDraw = ObjDraw.line;
                hWndCtrl.ContextMenuStrip = contextMenuStrip1;
            }
            catch (SystemException ex)
            {
                ex.ToString();
                return;
            }
        }

        private void 圆_Click(object sender, EventArgs e)
        {
            try
            {
                bDrawing = true;
                hWndCtrl.ContextMenuStrip = null;
                fun.ClearObjShow();
                Circle m_Circle = fun.DrawCircle();
                fun.m_circle = m_Circle;
                fun.m_lastDraw = ObjDraw.circle;
                hWndCtrl.ContextMenuStrip = contextMenuStrip1;
            }
            catch (SystemException ex)
            {
                ex.ToString();
                return;
            }
        }

        private void ComboBox_LineWith_Click(object sender, EventArgs e)
        {
            if ("" != ComboBox_LineWith.Text)
            {
                fun.m_hWnd.SetLineWidth(double.Parse(ComboBox_LineWith.Text));
            }
        }

        private void 显示轮廓_Click(object sender, EventArgs e)
        {
            fun.m_hWnd.SetDraw("margin");
        }

        private void 填充显示_Click(object sender, EventArgs e)
        {
            fun.m_hWnd.SetDraw("fill");
        }

        private void red_Click(object sender, EventArgs e)
        {
            fun.m_hWnd.SetColor("red");
        }

        private void green_Click(object sender, EventArgs e)
        {
            fun.m_hWnd.SetColor("green");
        }

        private void 蓝色_Click(object sender, EventArgs e)
        {
            fun.m_hWnd.SetColor("blue");
        }

        private void 黄色_Click(object sender, EventArgs e)
        {
            fun.m_hWnd.SetColor("yellow");
        }

        private void ts_But_LoadImage_Click(object sender, EventArgs e)
        {
            try
            {
                Stream inputStream = null;
                openFileDialog.InitialDirectory = "e:/";
                openFileDialog.Filter = "BMP图片|*.bmp|JPG图片|*.jpg|Gif图片|*.gif|PNG图片|*.png";
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((inputStream = openFileDialog.OpenFile()) != null)
                    {
                        using (inputStream)
                        {
                            string strImageFile = openFileDialog.FileName;
                            if (!fun.LoadImageFromFile(strImageFile))
                            {
                                MessageBox.Show("图片读取错误！");
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void 默认设置_Click(object sender, EventArgs e)
        {
            fun.m_hWnd.SetColor("red");
            fun.m_hWnd.SetDraw("margin");
            fun.m_hWnd.SetLineWidth(1);
        }

        private void 清除_Click(object sender, EventArgs e)
        {
            fun.ClearObjShow();
        }

        private void ComboBox_LineWith_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (null != fun && "" != ComboBox_LineWith.Text)
            //{
            //    fun.m_hWnd.SetLineWidth(double.Parse(ComboBox_LineWith.Text));
            //}
        }

        private void ComboBox_FontSize_Click(object sender, EventArgs e)
        {
            if ("" != ComboBox_FontSize.Text)
                fun.m_nPreFontSize = int.Parse(ComboBox_FontSize.Text);
        }

        private void 关联相机_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                string str = e.ClickedItem.Text;
                Init(str, m_ncam);
                CamCommon.Live(str);
                if (GlobalData.Config._CamConfig.camConfig.ContainsKey(m_ncam))
                {
                    GlobalData.Config._CamConfig.camConfig[m_ncam] = str;
                }
                else
                {
                    GlobalData.Config._CamConfig.camConfig.Add(m_ncam, str);
                }
                //保存到配置文件
                var json = JsonConvert.SerializeObject(GlobalData.Config._CamConfig);
                System.IO.File.WriteAllText(GlobalPath.SavePath.CamConfigPath, json);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return;
            }
        }

        private void 关联相机_Click(object sender, EventArgs e)
        {

        }

        private void hWndCtrl_Resize(object sender, EventArgs e)
        {
            if (null != fun)
            {
                fun.dReslutRow0 = 0;
                fun.dReslutCol0 = 0;
                fun.dReslutRow1 = 0;
                fun.dReslutCol1 = 0;
                fun.FitImageToWindow(ref fun.dReslutRow0, ref fun.dReslutCol0, ref fun.dReslutRow1, ref fun.dReslutCol1);
            }
        }

        private void MirrorChange(EnumData.Mirror mirrorType, ToolStripMenuItem item)
        {
            try
            {
                if (fun.m_ListImgMirror.Contains(mirrorType))
                {
                    item.Checked = false;
                    item.BackColor = Color.Transparent;
                }
                else
                {
                    item.Checked = true;
                    item.BackColor = Color.Green;
                }
                if (GlobalData.Config._CamConfig.ImageMirror.ContainsKey(m_ncam))
                {
                    List<Mirror> org = GlobalData.Config._CamConfig.ImageMirror[m_ncam];
                    if (item.Checked)
                    {
                        org.Add(mirrorType);
                    }
                    else
                    {
                        org.Remove(mirrorType);
                    }
                    GlobalData.Config._CamConfig.ImageMirror[m_ncam] = org;
                }
                else
                {
                    List<Mirror> newMir = new List<Mirror>();
                    newMir.Add(mirrorType);
                    GlobalData.Config._CamConfig.ImageMirror.Add(m_ncam, newMir);
                }
                fun.m_ListImgMirror = GlobalData.Config._CamConfig.ImageMirror[m_ncam];
                StaticFun.SaveData.SaveCamConfig();
                fun.MirrorImage(ref fun.m_hImage);
                fun.FitImageToWindow(ref fun.dReslutRow0, ref fun.dReslutCol0, ref fun.dReslutRow1, ref fun.dReslutCol1);
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage("镜像图像错误：" + ex.ToString());
            }
        }
        private void LoR_Click(object sender, EventArgs e)
        {
            MirrorChange(EnumData.Mirror.Left_Right, this.LoR);
        }

        private void UoD_Click(object sender, EventArgs e)
        {
            MirrorChange(EnumData.Mirror.Up_Down, this.UoD);
        }
        #region 图像集测试
        List<string> imagePathList = new List<string>();
        int index = 0;

        private void RefreshListViewImages(string strFolderPath)
        {
            try
            {
                this.imageList1.Images.Clear();
                this.listView_ImgList.Controls.Clear();
                imageList1.ImageSize = new Size((int)(listView_ImgList.Width / 2), (int)(listView_ImgList.Width / 2));
                Function.list_image_files(strFolderPath, "default", out imagePathList);
                foreach (string path in imagePathList)
                {
                    this.imageList1.Images.Add(Image.FromFile(path));
                }
                this.listView_ImgList.Items.Clear();
                this.listView_ImgList.LargeImageList = this.imageList1;
                this.listView_ImgList.View = View.LargeIcon;

                this.listView_ImgList.BeginUpdate();
                for (int i = 0; i < imageList1.Images.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.ImageIndex = i;
                    lvi.Text = i.ToString();
                    this.listView_ImgList.Items.Add(lvi);
                }
                this.listView_ImgList.EndUpdate();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        private void LoadImageSet(string strNG_OK)
        {
            string strImgFilePath = "";
            try
            {
                switch (m_ncam)
                {
                    case 1:
                        if (strNG_OK == "OK")
                        {
                            strImgFilePath = GlobalPath.SavePath.cam1_OrgImagePath_OK;
                        }
                        if (strNG_OK == "NG")
                        {
                            strImgFilePath = GlobalPath.SavePath.cam1_OrgImagePath_NG;
                        }
                        break;
                    case 2:
                        if (strNG_OK == "OK")
                        {
                            strImgFilePath = GlobalPath.SavePath.cam2_OrgImagePath_OK;
                        }
                        if (strNG_OK == "NG")
                        {
                            strImgFilePath = GlobalPath.SavePath.cam2_OrgImagePath_NG;
                        }
                        break;
                    case 3:
                        if (strNG_OK == "OK")
                        {
                            strImgFilePath = GlobalPath.SavePath.cam3_OrgImagePath_OK;
                        }
                        if (strNG_OK == "NG")
                        {
                            strImgFilePath = GlobalPath.SavePath.cam3_OrgImagePath_NG;
                        }
                        break;
                    case 4:
                        if (strNG_OK == "OK")
                        {
                            strImgFilePath = GlobalPath.SavePath.cam4_OrgImagePath_OK;
                        }
                        if (strNG_OK == "NG")
                        {
                            strImgFilePath = GlobalPath.SavePath.cam4_OrgImagePath_NG;
                        }
                        break;

                }
                if ("" != strImgFilePath)
                {
                    RefreshListViewImages(strImgFilePath);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                return;
            }
        }

        private void ts_But_ImageList_Click(object sender, EventArgs e)
        {
            groupBox_ImageList.Visible = true;
            LoadImageSet("NG");
        }

        private void ts_But_ImageListClose_Click(object sender, EventArgs e)
        {
            groupBox_ImageList.Visible = false;
        }

        private void but_PreImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (index > 0)
                {
                    index--;
                }
                else if (index == 0)
                {
                    index = imagePathList.Count;
                    index--;
                }
                foreach (ListViewItem lv in this.listView_ImgList.Items)
                {
                    lv.Selected = false;
                }
                this.listView_ImgList.Items[index].Selected = true;
                this.listView_ImgList.Items[index].EnsureVisible();
                fun.LoadImageFromFile(imagePathList[index]);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void but_NextImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (index == imagePathList.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
                foreach (ListViewItem lv in this.listView_ImgList.Items)
                {
                    lv.Selected = false;
                }
                this.listView_ImgList.Items[index].Selected = true;
                this.listView_ImgList.Items[index].EnsureVisible();
                fun.LoadImageFromFile(imagePathList[index]);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void listView_ImgList_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView_ImgList.SelectedItems.Count == 0)
                {
                    return;
                }
                index = this.listView_ImgList.SelectedItems[0].Index;
                fun.LoadImageFromFile(imagePathList[index]);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //private void Inspect()
        //{
        //    TMData.InspectResult result = new InspectResult();
        //    result.outcome = new Dictionary<string, string>();
        //    bool bStart = true;
        //    try
        //    {
        //        TimeSpan ts1, ts2, ts3;
        //        bool bResult = false;
        //        string strInspectItem = "";
        //        int NumOK = 0;
        //        int NumNG = 0;
        //        TMData.StripLenResult slResult = new StripLenResult();
        //        TMData.MultiResult tmResult = new MultiResult();
        //        TMData.RubberResult Result = new RubberResult();
        //        if (TMData_Serializer._globalData.dicInspectList.ContainsKey(m_ncam))
        //        {
        //            List<TMData.InspectItem> listItem = TMData_Serializer._globalData.dicInspectList[m_ncam];
        //            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
        //            if (listItem.Contains(InspectItem.TM))
        //            {
        //                if (!TM_fun.MultiTMInspect(TMData_Serializer._globalData.dic_MultiTMParam[m_ncam],
        //                                           TMData_Serializer._globalData.dicTMCheckList[m_ncam], true, out tmResult))
        //                {
        //                    tmResult.bResult = false;
        //                    MessageFun.ShowMessage("端子检测失败。");
        //                }
        //                ts2 = new TimeSpan(DateTime.Now.Ticks);
        //                result.InspectTime = Math.Round((ts2.Subtract(ts).Duration().TotalSeconds) * 1000, 0);   //检测时间
        //                strInspectItem = "端子检测";
        //                bResult = tmResult.bResult;
        //                TM_fun.MultiTMShowResult(TMData_Serializer._globalData.dic_MultiTMParam[m_ncam], tmResult, TMData_Serializer._globalData.dicTMCheckList[m_ncam], out NumOK, out NumNG);
        //            }
        //            if (listItem.Contains(InspectItem.Rubber))
        //            {
        //                TMData.RubberParam rubberParam = TMData_Serializer._globalData.rubberParam[m_ncam - 1];
        //                rubberParam.lineColor.listColorID = TMData_Serializer._globalData.listColorID;
        //                if (!TM_fun.RubberInsert(rubberParam, false, out Result, out bool bresult, out NumOK))
        //                {
        //                    NumOK = 0;
        //                    bresult = false;
        //                    MessageFun.ShowMessage("插壳检测失败。");
        //                }
        //                ts2 = new TimeSpan(DateTime.Now.Ticks);
        //                result.InspectTime = Math.Round((ts2.Subtract(ts).Duration().TotalSeconds) * 1000, 1);   //检测时间
        //                NumNG = (int)TMData_Serializer._globalData.rubberParam[m_ncam - 1].rubberLocate.nRubberNum - NumOK;
        //                strInspectItem = "插壳检测";
        //                bResult = bresult;
        //                TM_fun.MultiCKShowResult(TMData_Serializer._globalData.rubberParam[m_ncam - 1], Result);
        //            }
        //            if (listItem.Contains(InspectItem.StripLen))
        //            {
        //                if (!TM_fun.StrippingInspect(TMData_Serializer._globalData.stripLenParam[m_ncam - 1], false, out slResult, out bool bresult, out NumOK))
        //                {
        //                    NumOK = 0;
        //                    bResult = false;
        //                    MessageFun.ShowMessage("剥皮检测失败。");
        //                }
        //                ts2 = new TimeSpan(DateTime.Now.Ticks);
        //                result.InspectTime = Math.Round((ts2.Subtract(ts).Duration().TotalSeconds) * 1000, 1);   //检测时间
        //                NumNG = (int)TMData_Serializer._globalData.stripLenParam[m_ncam - 1].nLineNum - NumOK;
        //                strInspectItem = "剥皮检测";
        //                bResult = bresult;
        //                TM_fun.MultiSLShowResult(TMData_Serializer._globalData.stripLenParam[m_ncam - 1], slResult);

        //            }
        //            if (listItem.Contains(InspectItem.LineColor))
        //            {
        //                LineColorParam lineColorParam = TMData_Serializer._globalData.dic_LineColor[m_ncam];
        //                lineColorParam.listColorID = TMData_Serializer._globalData.listColorID;
        //                if (!TM_fun.LineColor_MLP(TMData_Serializer._globalData.dic_LineColor[m_ncam], null, false, out LineColorResult lineColorResult))
        //                {
        //                    bResult = false;
        //                    MessageFun.ShowMessage("线序检测失败。");
        //                }
        //                ts2 = new TimeSpan(DateTime.Now.Ticks);
        //                result.InspectTime = Math.Round((ts2.Subtract(ts).Duration().TotalSeconds) * 1000, 0);   //检测时间
        //                strInspectItem = "线序检测";
        //            }

        //        }
        //    }
        //    catch (SystemException ex)
        //    {
        //        bStart = false;
        //        MessageFun.ShowMessage(ex.ToString());
        //        return;
        //    }
        //}


        private void tsBut_LoadOK_Click(object sender, EventArgs e)
        {
            LoadImageSet("OK");
        }

        private void tsBut_LoadNG_Click(object sender, EventArgs e)
        {
            LoadImageSet("NG");
        }

        private void tsBut_LoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择文件路径";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string foldPath = dialog.SelectedPath;
                    RefreshListViewImages(foldPath);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return;
            }
        }

        #endregion

        private void FormCamShow_Load(object sender, EventArgs e)
        {


        }

        private void but_Live_Click(object sender, EventArgs e)
        {
            //camimage.a = m_ncam;
            //camimage.b = ad;
            fun.ClearObjShow();
            CamCommon.Live(m_strCamSer);
            ts_Label_state.Text = "当前状态：实时显示中";
        }

        private void but_GrabImage_Click(object sender, EventArgs e)
        {
            //camimage.a = m_ncam;
            //camimage.b = ad;
            fun.ClearObjShow();
            CamCommon.GrabImage(m_strCamSer);
            ts_Label_state.Text = "当前状态：拍照";
        }

        private void but_SaveImg_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.InitialDirectory = "e:/";
                saveFileDialog.Filter = "BMP图片|*.bmp|JPG图片|*.jpg|Gif图片|*.gif";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string strImageFile = saveFileDialog.FileName;
                    fun.SaveImage(strImageFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void but_RecoverImg_Click(object sender, EventArgs e)
        {
            try
            {
                if (fun != null && fun.m_hImage != null)
                {
                    fun.dReslutRow0 = 0;
                    fun.dReslutCol0 = 0;
                    fun.dReslutRow1 = 0;
                    fun.dReslutCol1 = 0;
                    fun.FitImageToWindow(ref fun.dReslutRow0, ref fun.dReslutCol0, ref fun.dReslutRow1, ref fun.dReslutCol1);
                    ts_Label_state.Text = "当前状态：原比例显示";
                }

            }
            catch (Exception)
            {
            }
        }

        private void but_LoadImage_Click(object sender, EventArgs e)
        {
            try
            {
                Stream inputStream = null;
                openFileDialog.InitialDirectory = "e:/";
                openFileDialog.Filter = "BMP图片|*.bmp|JPG图片|*.jpg|Gif图片|*.gif|PNG图片|*.png";
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((inputStream = openFileDialog.OpenFile()) != null)
                    {
                        using (inputStream)
                        {
                            string strImageFile = openFileDialog.FileName;
                            if (!fun.LoadImageFromFile(strImageFile))
                            {
                                MessageBox.Show("图片读取错误！");
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void but_ImageList_Click(object sender, EventArgs e)
        {
            groupBox_ImageList.Visible = true;
            LoadImageSet("NG");
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_Edit_Click(object sender, EventArgs e)
        {
            if (!FormMainUI.bRun)
            {

                StaticFun.UIConfig.CreateFormTeachMaster(m_ncam, this.sub_cam, 1);
            }
            else
            {
                MessageBox.Show("当前程序正在运行中，请停止检测按钮<运行中>后，再进行其它操作，谢谢配合。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
