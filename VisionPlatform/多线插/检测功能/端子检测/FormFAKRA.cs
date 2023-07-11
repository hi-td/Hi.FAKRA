using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseData;
using CamSDK;
using Chustange.Functional;
using HalconDotNet;
using Newtonsoft.Json;
using StaticFun;
using static BaseData.ColorSpace;
using static CamSDK.CamCommon;
using static VisionPlatform.TMData;
using static VisionPlatform.TMData_Serializer;

namespace VisionPlatform
{
    public partial class FormFAKRA : Form
    {
        private TMFunction TMFun;                                     //当前检测函数
        private Function Fun;                                         //当前底层函数
        string str_CamSer;                                            //当前相机序列号
        int m_ncam;                                                   //当前相机
        int sub_cam;                                                    //当前相机几号界面
        string  m_TMType= "teach";                                    //生产的线的类型：标准线or大小线
        //TMParam m_TMParam;
        Rect2 m_SkinWeldROIY = new Rect2();                           //示教时计算得到的绝缘皮压脚区域的大小
        Rect2 m_LineWeldROI = new Rect2();                            // 创建模板时绘制的线芯压脚区域
        LocateOutParams m_LocateResult = new LocateOutParams();       //模板匹配结果
        private List<Arbitrary> m_arbitrary = new List<Arbitrary>();  //绘制的线芯飞边区域
        int model_ID;                                                 //模板ID
        FormImageColorTrans formImageColorTrans;                      //图像颜色空间转换

        private bool TorF = false;
        public FormFAKRA(int ncam, int sub_cam)
        {
            InitializeComponent();
            m_ncam = ncam;
            this.sub_cam = sub_cam;
            UIConfig.RefreshFun(ncam, sub_cam, ref Fun, ref TMFun, ref str_CamSer);
            InitUI();
            selectcombox();
        }
        private void InitUI()
        {
            try
            {
                radioBut_ModelImage.Checked = true;      //默认使用模板图片
                tabPage_SkinWeld.Parent = null;
                tabPage_SkinPos.Parent = null;
                tabPage_LineWeld.Parent = null;
                tabPage_LinePos.Parent = null;
                tabPage_LineSide.Parent = null;
                tabPage_TMNose.Parent = null;
                tabPage_TMhead.Parent = null;
                tabPage_TMwing.Parent = null;
                tabPage_LineColorf1.Parent = null;          //线序检测 
                checkBox_bColorSpaceTrans.Checked = false;
                tLPanel_ColorSpace.Visible = false;

                radioBut_LinePos_methodLine.Checked = true;
                tabPage_BackGround.Parent = null;
                tabPage_LineLigth.Parent = tabControl1;
                tabPage_LineColor.Parent = null;
                tLPanel_LinePos_Rect2Del.Visible = true;

            }
            catch (Exception ex)
            {
                MessageFun.ShowMessage("InitUI()初始化异常：" + ex.ToString());
            }
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
        public void Refreshpage()
        {
            InitUI();
            LoadParam();
            tabCtrl_InspectItem.Refresh();
        }
        private void selectcombox()
        {
            try
            {
                string a = m_ncam.ToString() + sub_cam.ToString();
                comboBox_SelectModel.Items.Add("自动");
                comboBox_Model.Items.Add("teach");
                if (TMData_Serializer._globalData.dicTMCheckList != null && TMData_Serializer._globalData.dicTMCheckList.ContainsKey(a))
                {
                    foreach (var item in TMData_Serializer._globalData.dicTMCheckList[a].Keys)
                    {
                        if (item != "teach")
                        {
                            comboBox_Model.Items.Add(item);
                        }
                        comboBox_SelectModel.Items.Add(item);
                    }
                    if (TMData_Serializer._globalData.dic_selectmodel != null && TMData_Serializer._globalData.dic_selectmodel.ContainsKey(a))
                    {
                        if (TMData_Serializer._globalData.dic_selectmodel[a] != "自动")
                        {
                            comboBox_SelectModel.Text = TMData_Serializer._globalData.dic_selectmodel[a];
                            comboBox_Model.Text = TMData_Serializer._globalData.dic_selectmodel[a];
                            m_TMType = TMData_Serializer._globalData.dic_selectmodel[a];
                        }
                        else
                        {
                            comboBox_SelectModel.Text = "自动";
                            comboBox_Model.Text = "teach";
                        }
                    }
                    else
                    {
                        comboBox_SelectModel.Text = "teach";
                        comboBox_Model.Text = "teach";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageFun.ShowMessage("selectcombox()初始化异常：" + ex.ToString());
            }
        }
        private void LoadParam()
        {
            try
            {
                string a = m_ncam.ToString() + sub_cam.ToString();
                //如果相机实时，则停止实时
                try
                {
                    CamSDK.CamCommon.GrabImagestop(str_CamSer);
                }
                catch (Exception ex)
                {
                    MessageFun.ShowMessage(ex.ToString());
                }
                Fun.ClearObjShow();

                #region 导入模板图像
                string model_name = "camera" + m_ncam.ToString() + "_"+ sub_cam.ToString() + "-" + m_TMType.ToString();
                string strPath = System.IO.Path.Combine(GlobalPath.SavePath.ModelImagePath, model_name) + ".bmp";
                if (File.Exists(strPath))
                {
                    Fun.LoadImageFromFile(strPath);
                    TMFun.m_ModelImage?.Dispose();
                    TMFun.m_ModelImage = Fun.m_hImage.Clone();
                }
                else
                {
                    MessageFun.ShowMessage("无" + model_name + "的模板图像。");
                }
                #endregion

                #region 导入端子检测项列表
                if (TMData_Serializer._globalData.dicTMCheckList.ContainsKey(a))
                {

                    //comboBox_SelectModel.Text = m_TMType;
                    if (TMData_Serializer._globalData.dicTMCheckList[a].ContainsKey(m_TMType))
                    {
                        TMData.TMCheckItem TMCheckList = TMData_Serializer._globalData.dicTMCheckList[a][m_TMType];
                        if (TMCheckList.SkinWeld)
                        {
                            tabPage_SkinWeld.Parent = tabCtrl_InspectItem;
                        }
                        else
                        {
                            tabPage_SkinWeld.Parent = null;
                        }
                        if (TMCheckList.SkinPos)
                        {
                            tabPage_SkinPos.Parent = tabCtrl_InspectItem;
                        }
                        else
                        {
                            tabPage_SkinPos.Parent = null;
                        }
                        if (TMCheckList.LineWeld)
                        {
                            tabPage_LineWeld.Parent = tabCtrl_InspectItem;
                        }
                        else
                        {
                            tabPage_LineWeld.Parent = null;
                        }

                        if (TMCheckList.LinePos)
                        {
                            tabPage_LinePos.Parent = tabCtrl_InspectItem;
                        }
                        else
                        {
                            tabPage_LinePos.Parent = null;
                        }
                        if (TMCheckList.LineSide)
                        {
                            tabPage_LineSide.Parent = tabCtrl_InspectItem;
                        }
                        else
                        {
                            tabPage_LineSide.Parent = null;
                        }
                        if (TMCheckList.LineColor)
                        {
                            tabPage_LineColorf1.Parent = tabCtrl_InspectItem;
                        }
                        else
                        {
                            tabPage_LineColorf1.Parent = null;
                        }

                    }
                    #endregion

                    #region 导入检测参数
                    if (TMData_Serializer._globalData.fakraParam.ContainsKey(a))
                    {
                        if (TMData_Serializer._globalData.fakraParam[a].ContainsKey(m_TMType))
                        {
                            FakraParam m_TMParam = TMData_Serializer._globalData.fakraParam[a][m_TMType];

                        }
                        else
                        {
                            numUpD_Score.Value = (decimal)0.6;
                        }
                    }
                    else
                    {
                        numUpD_Score.Value = (decimal)0.6;
                    }
                    #endregion
                }
            }
            catch (SystemException ex)
            {
                MessageFun.ShowMessage("导入端子模板数据出错：" + ex.ToString());
            }
            finally
            {
                TorF = true;
            }
        }
        private void SkinWeldInspect()
           {
        //    try
        //    {
        //        if (null == TMFun || null == Fun.m_hImage)
        //        {
        //            return;
        //        }
        //        if (radioBut_ModelImage.Checked)
        //        {
        //            TMFun.TransModelImage();
        //        }
        //        if (radioBut_PreImage.Checked)
        //        {
        //            TMFun.ModelImageTransBack();
        //        }
        //        Fun.ClearObjShow();
        //        TMParam param = InitParam();
        //        TMFun.setseiz();
        //        if (!TMFun.FindNccModelYXSS(param, true, out Rect2 LineWeldROI, out _))
        //        {
        //            MessageFun.ShowMessage("定位失败！");
        //            return;
        //        }
        //        TMFun.SkinWeldInspect(param.skinWeld, LineWeldROI, true, out TMData.SkinWeldResult result);
        //        if (0 != result.skinWeldROI.dLength2)
        //        {
        //            m_SkinWeldROIY = result.skinWeldROI;      //保存示教时得到的绝缘皮区域，作用输入参数，用来判别OK/NG
        //            //numUpD_skinWeld_Rect2MidLen2.Maximum = (decimal)m_SkinWeldROIY.dLength2 * 2;
        //            //trackBar_skinWeld_Rect2MidLen2.Maximum = (int)m_SkinWeldROIY.dLength2 * 2;
        //        }

        //        if (0 != m_SkinWeldROIY.dLength1)
        //        {
        //            label_skinWeld_width.Text = Math.Round(m_SkinWeldROIY.dLength1 * 2, 2).ToString();
        //            label_skinWeld_height.Text = Math.Round(m_SkinWeldROIY.dLength2 * 2, 2).ToString();
        //            label_WMin.Text = Math.Round(m_SkinWeldROIY.dLength1 * 2 * (double)numUpD_SkinWeldHor_WMin.Value, 2).ToString();
        //            label_WMax.Text = Math.Round(m_SkinWeldROIY.dLength1 * 2 * (double)numUpD_SkinWeldHor_WMax.Value, 2).ToString();
        //            label_HMin.Text = Math.Round(m_SkinWeldROIY.dLength2 * 2 * (double)numUpD_SkinWeldVer_HMin.Value, 2).ToString();
        //            label_HMax.Text = Math.Round(m_SkinWeldROIY.dLength2 * 2 * (double)numUpD_SkinWeldVer_HMax.Value, 2).ToString();
        //            label_skinWeld_Area1.Text = Math.Round(result.dAreaRatio[0], 2).ToString();
        //            label_skinWeld_Area2.Text = Math.Round(result.dAreaRatio[1], 2).ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        }
        private void trackBar_JypyjGap_Scroll(object sender, EventArgs e)
        {
            numericUpDown_JypyjGap.Value = trackBar_JypyjGap.Value;
        }
        private void numericUpDown_JypyjGap_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //SkinWeldInspect();
            }

        }
        private void trackBar_JypyjWidth_Scroll(object sender, EventArgs e)
        {
            numericUpDown_JypyjWidth.Value = trackBar_JypyjWidth.Value;
        }
        private void numericUpDown_JypyjWidth_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //SkinWeldInspect();
            }

        }
        private void trackBar_JypyjHeight_Scroll(object sender, EventArgs e)
        {
            numericUpDown_JypyjHeight.Value = trackBar_JypyjHeight.Value;
        }
        private void numericUpDown_JypyjHeight_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //SkinWeldInspect();
            }

        }

        #region 绝缘皮位置
        //private SkinPosParam InitSkinPosParam()
        //{
        //    SkinPosParam param = new SkinPosParam();
        //    try
        //    {
        //        //检测框宽
        //        param.nROIWidth = (int)numericUpDown_SkinPosWidth.Value;
        //        trackBar_SkinPosWidth.Value = (int)numericUpDown_SkinPosWidth.Value;
        //        //检测框间距
        //        param.nROIGap = (int)numericUpDown_SkinPosGap.Value;
        //        trackBar_SkinPosGap.Value = (int)numericUpDown_SkinPosGap.Value;
        //        //检测框高
        //        param.nROIHeight = (int)numericUpDown_SkinPosHeight.Value;
        //        trackBar_SkinPosHeight.Value = (int)numericUpDown_SkinPosHeight.Value;
        //        //颜色空间转换
        //        param.colorSpace = GetColorSpace(label_ImageChannel, label_ColorSpace);
        //        param.colorSpace.bTrans = checkBox_bColorSpaceTrans.Checked;//必须放在赋值之后
        //        //动态阈值
        //        param.nDynThd = (int)numUpD_skinpos_dynThd.Value;
        //        trackBar_skinpos_dynThd.Value = (int)numUpD_skinpos_dynThd.Value;
        //        //芯线静态阈值
        //        param.nLineThd = (int)numUpD_SkinPos_LineThd.Value;
        //        trackBar_SkinPos_LineThd.Value = (int)numUpD_SkinPos_LineThd.Value;
        //        //绝缘皮面积占比
        //        param.dSkinAreaRatioMin = (double)numUpD_SkinPos_AreaRatioMin.Value;
        //        param.dSkinAreaRatioMax = (double)numUpD_SkinPos_AreaRatioMax.Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return param;
        //}
        //private void LoadSkinPosParam(SkinPosParam param)
        //{
        //    //检测框宽
        //    if (param.nROIWidth == 0)
        //    {
        //        trackBar_SkinPosWidth.Value = 50;
        //        numericUpDown_SkinPosWidth.Value = 50;
        //    }
        //    else
        //    {
        //        trackBar_SkinPosWidth.Value = (int)param.nROIWidth;
        //        numericUpDown_SkinPosWidth.Value = (int)param.nROIWidth;
        //    }
        //    //检测框间距
        //    trackBar_SkinPosGap.Value = param.nROIGap;
        //    numericUpDown_SkinPosGap.Value = param.nROIGap;
        //    //检测框高
        //    trackBar_SkinPosHeight.Value = param.nROIHeight;
        //    numericUpDown_SkinPosHeight.Value = param.nROIHeight;
        //    //颜色空间转换
        //    checkBox_bColorSpaceTrans.Checked = param.colorSpace.bTrans;  //必须放在赋值之后
        //    ShowColorSpace(label_ImageChannel, label_ColorSpace, param.colorSpace);
        //    //绝缘皮面积比
        //    if (param.dSkinAreaRatioMax == 0)
        //    {
        //        numUpD_SkinPos_AreaRatioMax.Value = (decimal)0.95;
        //    }
        //    else
        //    {
        //        numUpD_SkinPos_AreaRatioMax.Value = (decimal)param.dSkinAreaRatioMax;
        //    }
        //    if (param.dSkinAreaRatioMin == 0)
        //    {
        //        numUpD_SkinPos_AreaRatioMin.Value = (decimal)0.01;
        //    }
        //    else
        //    {
        //        numUpD_SkinPos_AreaRatioMin.Value = (decimal)param.dSkinAreaRatioMin;
        //    }
        //    //芯线静态阈值
        //    if (param.nLineThd == 0)
        //    {
        //        trackBar_SkinPos_LineThd.Value = 200;
        //    }
        //    else
        //    {
        //        trackBar_SkinPos_LineThd.Value = param.nLineThd;
        //    }
        //    numUpD_SkinPos_LineThd.Value = trackBar_SkinPos_LineThd.Value;
        //    //芯线动态阈值
        //    if (param.nDynThd == 0)
        //    {
        //        trackBar_skinpos_dynThd.Value = 5;
        //    }
        //    else
        //    {
        //        trackBar_skinpos_dynThd.Value = param.nDynThd;
        //    }
        //    numUpD_skinpos_dynThd.Value = trackBar_skinpos_dynThd.Value;
        //}
        private void SkinPosInspect()
        {
        //    try
        //    {
        //        if (null == TMFun || null == Fun.m_hImage)
        //        {
        //            return;
        //        }

        //        if (radioBut_ModelImage.Checked)
        //        {
        //            TMFun.TransModelImage();
        //        }
        //        if (radioBut_PreImage.Checked)
        //        {
        //            TMFun.ModelImageTransBack();
        //        }
        //        Fun.ClearObjShow();
        //        TMParam param = InitParam();
        //        TMFun.setseiz();
        //        if (!TMFun.FindNccModelYXSS(param, true, out Rect2 LineWeldROI, out _))
        //        {
        //            MessageFun.ShowMessage("定位失败！");
        //            return;
        //        }
        //        if (TMData_Serializer._globalData.dicCheckList[m_ncam][m_TMType].bSkinWeld && m_SkinWeldROIY.dLength1 == 0)
        //        {
        //            MessageFun.ShowMessage("请先示教绝缘皮压脚。");
        //            return;
        //        }
        //        TMFun.SkinPosInspect_22(TMData_Serializer._globalData.dicCheckList[m_ncam][m_TMType].bSkinWeld, param.skinPos, m_SkinWeldROIY, LineWeldROI, true, out TMData.SkinPosResult result);

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        }
        private void trackBar_SkinPosWidth_Scroll(object sender, EventArgs e)
        {
            numericUpDown_SkinPosWidth.Value = trackBar_SkinPosWidth.Value;
        }
        private void numericUpDown_SkinPosLen_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //SkinPosInspect();
            }
        }
        private void trackBar_SkinPosHeight_Scroll(object sender, EventArgs e)
        {
            numericUpDown_SkinPosHeight.Value = trackBar_SkinPosHeight.Value;
        }
        private void numericUpDown_SkinPosHeight_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //SkinPosInspect();
            }
        }
        private void trackBar_SkinPosGap_Scroll(object sender, EventArgs e)
        {
            numericUpDown_SkinPosGap.Value = trackBar_SkinPosGap.Value;
        }
        private void numericUpDown_SkinPosGap_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //SkinPosInspect();
            }
        }
        private void trackBar_SkinPosLineThd_Scroll(object sender, EventArgs e)
        {
            numUpD_SkinPos_LineThd.Value = trackBar_SkinPos_LineThd.Value;
        }
        private void numUpD_SkinPosLineThd_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //SkinPosInspect();
            }
        }
        private void trackBar_skinpos_dynThd_Scroll(object sender, EventArgs e)
        {
            numUpD_skinpos_dynThd.Value = trackBar_skinpos_dynThd.Value;
        }
        private void numUpD_skinpos_dynThd_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //SkinPosInspect();
            }
        }
        private void numUpD_SkinPos_AreaRatioMin_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //SkinPosInspect();
            }
        }
        private void numUpD_SkinPos_AreaRatioMax_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //SkinPosInspect();
            }
        }
        private void but_ReSelColorSpace_Click(object sender, EventArgs e)
        {
            //if (null == formImageColorTrans)
            //{
            //    formImageColorTrans?.Dispose();
            //    formImageColorTrans = new FormImageColorTrans(m_ncam, label_ImageChannel, label_ColorSpace);
            //    formImageColorTrans.TopMost = true;
            //}
            //else if (null != formImageColorTrans && formImageColorTrans.IsDisposed)
            //{
            //    formImageColorTrans?.Dispose();
            //    formImageColorTrans = new FormImageColorTrans(m_ncam, label_ImageChannel, label_ColorSpace);
            //    formImageColorTrans.TopMost = true;
            //}
            //formImageColorTrans.Show();
        }
        private void checkBox_bColorSpaceTrans_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_bColorSpaceTrans.Checked)
            {
                tLPanel_ColorSpace.Visible = true;
            }
            else
            {
                tLPanel_ColorSpace.Visible = false;
            }
        }
        private void but_SkinPos_Show_Click(object sender, EventArgs e)
        {
            //Fun.m_hWnd.DispObj(Fun.ColorSpaceTrans(GetColorSpace(label_ImageChannel, label_ColorSpace)));
        }
        #endregion

        #region 线芯压脚
        //private LineWeldParam InitLineWeldParam()
        //{
        //    LineWeldParam param = new LineWeldParam();
        //    try
        //    {
        //        param.nMidLen2 = (int)numUpD_LineWeld_Rect2MidLen2.Value;
        //        trackBar_LineWeld_Rect2MidLen2.Value = (int)numUpD_LineWeld_Rect2MidLen2.Value;
        //        param.dAreaRatioMin = (double)numUpD_LineWeld_AreaRatioMin.Value;
        //        param.dAreaRatioMax = (double)numUpD_LineWeld_AreaRatioMax.Value;
        //        param.dDislocation = (double)numUpD_LineWeld_Dislocation.Value;
        //        trackBar_LineWeld_Dislocation.Value = (int)numUpD_LineWeld_Dislocation.Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return param;
        //}

        //private void LoadLineWeldParam(LineWeldParam param)
        //{
        //    try
        //    {
        //        if (0 != param.nMidLen2)
        //        {
        //            trackBar_LineWeld_Rect2MidLen2.Value = param.nMidLen2;
        //        }
        //        else
        //        {
        //            trackBar_LineWeld_Rect2MidLen2.Value = (int)m_LineWeldROI.dLength2 / 4;
        //        }
        //        numUpD_LineWeld_Rect2MidLen2.Value = trackBar_LineWeld_Rect2MidLen2.Value;
        //        numUpD_LineWeld_AreaRatioMin.Value = (decimal)param.dAreaRatioMin;
        //        numUpD_LineWeld_AreaRatioMax.Value = (decimal)param.dAreaRatioMax;
        //        numUpD_LineWeld_Dislocation.Value = (decimal)param.dDislocation;
        //        trackBar_LineWeld_Dislocation.Value = (int)numUpD_LineWeld_Dislocation.Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}

        private void LineWeldInspect()
        {
        //    try
        //    {
        //        if (null == TMFun || null == Fun.m_hImage)
        //        {
        //            return;
        //        }

        //        if (radioBut_ModelImage.Checked)
        //        {
        //            TMFun.TransModelImage();
        //        }
        //        if (radioBut_PreImage.Checked)
        //        {
        //            TMFun.ModelImageTransBack();
        //        }
        //        Fun.ClearObjShow();
        //        TMParam param = InitParam();
        //        TMFun.setseiz();
        //        if (!TMFun.FindNccModelYXSS(param, true, out Rect2 LineWeldROI, out _))
        //        {
        //            MessageFun.ShowMessage("定位失败！");
        //            return;
        //        }
        //        TMFun.LineCoreWeldInspect(param.lineWeld, LineWeldROI, true, out TMData.LineWeldResult result);
        //        if (null != result.arrayAreaRatio)
        //        {
        //            label_LineWeld_AreaRatioUp.Text = result.arrayAreaRatio[0].ToString();
        //            label_LineWeld_AreaRatioDown.Text = result.arrayAreaRatio[1].ToString();
        //            label_LineWeld_Dislocation.Text = result.dDislocation.ToString();
        //            // label_LineWeld_AreaRatioMin.Text = Math.Round(result.arrayAreaRatio[0] * (double)numUpD_LineWeld_AreaRatioMin.Value, 2).ToString();
        //            //label_LineWeld_AreaRatioMax.Text = Math.Round(result.arrayAreaRatio[1] * (double)numUpD_LineWeld_AreaRatioMax.Value, 2).ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        }
        private void trackBar_LineWeld_Rect2MidLen2_Scroll(object sender, EventArgs e)
        {
            numUpD_LineWeld_Rect2MidLen2.Value = trackBar_LineWeld_Rect2MidLen2.Value;
        }

        private void numUpD_LineWeld_Rect2MidLen2_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //LineWeldInspect();
            }
        }

        private void numUpD_LineWeld_AreaRatioMax_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //LineWeldInspect();
            }
        }

        private void numUpD_LineWeld_AreaRatioMin_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
               // LineWeldInspect();
            }
        }

        private void trackBar_LineWeld_Dislocation_Scroll(object sender, EventArgs e)
        {
            numUpD_LineWeld_Dislocation.Value = trackBar_LineWeld_Dislocation.Value;
        }

        private void numUpD_LineWeld_Dislocation_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
               // LineWeldInspect();
            }
        }
        #endregion

        #region 线芯位置
        //private LinePosParam InitLinePosParam()
        //{
        //    LinePosParam param = new LinePosParam();
        //    try
        //    {
        //        #region ROI
        //        param.nROIWidth = (int)numericUpDown_LineCorePosWidth.Value;
        //        trackBar_LineCorePosWidth.Value = (int)numericUpDown_LineCorePosWidth.Value;

        //        param.nROIGap = (int)numericUpDown_LineCorePosGap.Value;
        //        trackBar_LineCorePosGap.Value = (int)numericUpDown_LineCorePosGap.Value;

        //        param.nROIHeight = (int)numUpDown_LineCorePosHeight.Value;
        //        trackBar_LineCorePosHeight.Value = (int)numUpDown_LineCorePosHeight.Value;
        //        #endregion
        //        //检测方法
        //        if (radioBut_LinePos_methodColor.Checked)
        //        {
        //            param.method = LinePosMethod.lineColor;
        //        }
        //        else if (radioBut_LinePos_methodBackGround.Checked)
        //        {
        //            param.method = LinePosMethod.BackGround;
        //        }
        //        else if (radioBut_LinePos_methodOutline.Checked)
        //        {
        //            param.method = LinePosMethod.lineOutline;
        //        }
        //        else
        //        {
        //            param.method = LinePosMethod.lineLight;
        //        }
        //        #region 背景
        //        param.backGround.nThd = (int)numUpD_back_lineLigth.Value;
        //        trackBar_back_lineLigth.Value = (int)numUpD_back_lineLigth.Value;
        //        param.backGround.dWRatioMin = (int)numUpD_back_WRatioMin.Value;
        //        param.backGround.dWRatioMax = (int)numUpD_back_WRatioMax.Value;
        //        #endregion

        //        #region  芯线亮度
        //        //动态阈值分割
        //        param.lineLight.nMeanMask = (int)numUpD_linePos_MeanMask.Value;
        //        trackBar_linePos_MeanMask.Value = (int)numUpD_linePos_MeanMask.Value;
        //        param.lineLight.nDynThd = (int)numUpD_linePos_DynThd.Value;
        //        trackBar_linePos_DynThd.Value = (int)numUpD_linePos_DynThd.Value;
        //        //静态阈值
        //        param.lineLight.nThd = (int)numUpD_LinePos_Thd.Value;
        //        trackBar_LinePos_Thd.Value = (int)numUpD_LinePos_Thd.Value;
        //        //芯线高度
        //        param.lineLight.dLineLen2 = (int)numUpD_LinePos_LineH.Value;                //芯线高度
        //        //宽度比
        //        param.lineLight.dWRatioMax = (double)numUpD_LinePos_WRatioMax.Value;
        //        param.lineLight.dWRatioMin = (double)numUpD_LinePos_WRatioMin.Value;
        //        #endregion

        //        #region 芯线颜色
        //        param.lineColor.colorSpace = GetColorSpace(label_linePos_ImageChannel, label_linePos_ColorSpace);
        //        param.lineColor.bThd0X = checkBox_linePos_Thd0x.Checked;
        //        param.lineColor.nThd = (int)numUpD_lineColor_Thd.Value;
        //        param.lineColor.dDistMin = (double)numUpD_lineColor_DistMin.Value;
        //        param.lineColor.dDistMax = (double)numUpD_lineColor_DistMax.Value;
        //        #endregion
        //        #region 芯线轮廓
        //        param.lineOutline.dDistMin = (double)numUpD_lineOutline_DistMin.Value;
        //        param.lineOutline.dDistMax = (double)numUpD_lineOutline_DistMax.Value;
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return param;
        //}
        //private void LoadLinePosParam(LinePosParam param)
        //{
        //    //检测框宽
        //    if (param.nROIWidth == 0)
        //    {
        //        trackBar_LineCorePosWidth.Value = 50;
        //    }
        //    else
        //    {
        //        trackBar_LineCorePosWidth.Value = (int)param.nROIWidth;
        //    }
        //    numericUpDown_LineCorePosWidth.Value = trackBar_LineCorePosWidth.Value;
        //    //检测框间距
        //    trackBar_LineCorePosGap.Value = param.nROIGap;
        //    numericUpDown_LineCorePosGap.Value = param.nROIGap;
        //    //检测框高度
        //    trackBar_LineCorePosHeight.Value = param.nROIHeight;
        //    numUpDown_LineCorePosHeight.Value = param.nROIHeight;
        //    //检测方法
        //    if (param.method == LinePosMethod.lineColor)
        //    {
        //        radioBut_LinePos_methodColor.Checked = true;
        //    }
        //    else if (param.method == LinePosMethod.BackGround)
        //    {
        //        radioBut_LinePos_methodBackGround.Checked = true;
        //    }
        //    else if (param.method == LinePosMethod.lineOutline)
        //    {
        //        radioBut_LinePos_methodOutline.Checked = true;
        //    }
        //    else
        //    {
        //        radioBut_LinePos_methodLine.Checked = true;
        //    }

        //    #region 背景
        //    trackBar_back_lineLigth.Value = param.backGround.nThd;
        //    numUpD_back_lineLigth.Value = param.backGround.nThd;
        //    numUpD_back_WRatioMin.Value = (decimal)param.backGround.dWRatioMin;
        //    numUpD_back_WRatioMax.Value = (decimal)param.backGround.dWRatioMax;
        //    #endregion

        //    #region 芯线亮度
        //    numUpD_linePos_DynThd.Value = param.lineLight.nDynThd;
        //    trackBar_linePos_DynThd.Value = param.lineLight.nDynThd;
        //    numUpD_linePos_MeanMask.Value = param.lineLight.nMeanMask;
        //    trackBar_linePos_MeanMask.Value = param.lineLight.nMeanMask;
        //    trackBar_LinePos_Thd.Value = param.lineLight.nThd;
        //    numUpD_LinePos_Thd.Value = param.lineLight.nThd;
        //    numUpD_LinePos_WRatioMax.Value = (decimal)param.lineLight.dWRatioMax;
        //    numUpD_LinePos_WRatioMin.Value = (decimal)param.lineLight.dWRatioMin;
        //    #endregion

        //    #region 芯线颜色
        //    ShowColorSpace(label_linePos_ImageChannel, label_linePos_ColorSpace, param.lineColor.colorSpace);
        //    numUpD_lineColor_Thd.Value = param.lineColor.nThd;
        //    trackBar_lineColor_Thd.Value = param.lineColor.nThd;
        //    checkBox_linePos_Thd0x.Checked = param.lineColor.bThd0X;
        //    numUpD_lineColor_DistMin.Value = (decimal)param.lineColor.dDistMin;
        //    numUpD_lineColor_DistMax.Value = (decimal)param.lineColor.dDistMax;
        //    #endregion
        //    #region 芯线轮廓
        //    numUpD_lineOutline_DistMin.Value = (decimal)param.lineOutline.dDistMin;
        //    numUpD_lineOutline_DistMax.Value = (decimal)param.lineOutline.dDistMax;
        //    #endregion
        //}
        private void LineCorePosInspect(object sender, EventArgs e)
        {
        //    if (null == TMFun || null == Fun.m_hImage || !TorF)
        //    {
        //        return;
        //    }
        //    if (radioBut_ModelImage.Checked)
        //    {
        //        TMFun.TransModelImage();
        //    }
        //    if (radioBut_PreImage.Checked)
        //    {
        //        TMFun.ModelImageTransBack();
        //    }
        //    Fun.ClearObjShow();
        //    TMParam param = InitParam();
        //    TMFun.setseiz();
        //    if (!TMFun.FindNccModelYXSS(param, true, out Rect2 LineWeldROI, out _))
        //    {
        //        MessageFun.ShowMessage("定位失败！");
        //        return;
        //    }
        //    if (TMFun.LineCorePosInspect(param.lineCorePos, LineWeldROI, true, out LinePosResult outData))
        //    {
        //        if (param.lineCorePos.method == LinePosMethod.BackGround)
        //        {
        //            //label_back_PreDist.Text = outData.dDist.ToString();
        //        }
        //        if (param.lineCorePos.method == LinePosMethod.lineColor)
        //        {
        //            label_lineColor_PreDist.Text = outData.dWRatio.ToString();
        //        }
        //        if (param.lineCorePos.method == LinePosMethod.lineLight)
        //        {
        //            label_lineLight_preWRatio.Text = outData.dWRatio.ToString();
        //        }
        //    }
        }
        private void trackBar_LineCorePosWidth_Scroll(object sender, EventArgs e)
        {
            numericUpDown_LineCorePosWidth.Value = trackBar_LineCorePosWidth.Value;
        }
        private void trackBar_LineCorePosHeight_Scroll(object sender, EventArgs e)
        {
            numUpDown_LineCorePosHeight.Value = trackBar_LineCorePosHeight.Value;
        }
        private void trackBar_LineCorePosGap_Scroll(object sender, EventArgs e)
        {
            numericUpDown_LineCorePosGap.Value = trackBar_LineCorePosGap.Value;

        }
        private void radioBut_LinePos_Line_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBut_LinePos_methodLine.Checked)
            {
                tabPage_BackGround.Parent = null;
                tabPage_LineLigth.Parent = tabControl1;
                tabPage_LineColor.Parent = null;
                tabPage_LineOutline.Parent = null;
            }
        }
        private void radioBut_LinePos_Color_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBut_LinePos_methodColor.Checked)
            {
                tabPage_BackGround.Parent = null;
                tabPage_LineLigth.Parent = null;
                tabPage_LineOutline.Parent = null;
                tabPage_LineColor.Parent = tabControl1;
            }
        }
        private void radioBut_LinePos_methodBackGround_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBut_LinePos_methodBackGround.Checked)
            {
                tabPage_BackGround.Parent = tabControl1;
                tabPage_LineLigth.Parent = null;
                tabPage_LineColor.Parent = null;
                tabPage_LineOutline.Parent = null;
            }

        }
        private void trackBar_linePos_MeanMask_Scroll(object sender, EventArgs e)
        {
            numUpD_linePos_MeanMask.Value = trackBar_linePos_MeanMask.Value;
        }
        private void trackBar_linePos_DynThd_Scroll(object sender, EventArgs e)
        {
            numUpD_linePos_DynThd.Value = trackBar_linePos_DynThd.Value;
        }

        private void trackBar_LinePos_Thd_Scroll(object sender, EventArgs e)
        {
            numUpD_LinePos_Thd.Value = trackBar_LinePos_Thd.Value;
        }
        private void trackBar_back_lineLigth_Scroll(object sender, EventArgs e)
        {
            numUpD_back_lineLigth.Value = trackBar_back_lineLigth.Value;
        }
        
        private void but_linePos_ColorSpace_Click(object sender, EventArgs e)
        {
            //if (null == formImageColorTrans)
            //{
            //    formImageColorTrans?.Dispose();
            //    formImageColorTrans = new FormImageColorTrans(m_ncam, label_linePos_ImageChannel, label_linePos_ColorSpace);
            //    formImageColorTrans.TopMost = true;
            //}
            //else if (null != formImageColorTrans && formImageColorTrans.IsDisposed)
            //{
            //    formImageColorTrans?.Dispose();
            //    formImageColorTrans = new FormImageColorTrans(m_ncam, label_linePos_ImageChannel, label_linePos_ColorSpace);
            //    formImageColorTrans.TopMost = true;
            //}
            //formImageColorTrans.Show();
        }

        private void checkBox_linePos_Thd0x_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_linePos_Thd0x.Checked)
            {
                checkBox_linePos_Thdx255.Checked = false;
            }
            else
            {
                checkBox_linePos_Thdx255.Checked = true;
            }
        }
        private void checkBox_linePos_Thdx255_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_linePos_Thdx255.Checked)
            {
                checkBox_linePos_Thd0x.Checked = false;
            }
            else
            {
                checkBox_linePos_Thd0x.Checked = true;
            }
        }

        private void trackBar_lineColor_Thd_Scroll(object sender, EventArgs e)
        {
            numUpD_lineColor_Thd.Value = trackBar_lineColor_Thd.Value;
        }
        private void but_lineColor_Show_Click(object sender, EventArgs e)
        {
            //Fun.m_hWnd.DispObj(Fun.ColorSpaceTrans(GetColorSpace(label_linePos_ImageChannel, label_linePos_ColorSpace)));
        }

        #endregion

        #region 线芯两侧飞边
        //private LineCoreSideParam InitLineSideParam()
        //{
        //    LineCoreSideParam param = new LineCoreSideParam();
        //    try
        //    {
        //        param.arbitrary = m_arbitrary;
        //        param.minthd = trackBar_minthd.Value;
        //        numUpD_minThd.Value = param.minthd;
        //        param.minarea = (int)numUpD_minArea.Value;
        //        trackBar_minArea.Value = param.minarea;

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return param;
        //}

        //private void LoadLineCoreSideParam(LineCoreSideParam param)
        //{
        //    try
        //    {
        //        if (param.arbitrary != null)
        //        {
        //            m_arbitrary = param.arbitrary;
        //        }
        //        for (int n = 0; n < param.arbitrary.Count; n++)
        //        {
        //            comBox_LineSide_ROI.Items.Add("检测区域" + (n + 1).ToString());
        //        }
        //        trackBar_minthd.Value = param.minthd;
        //        numUpD_minThd.Value = param.minthd;
        //        trackBar_minArea.Value = param.minarea;
        //        numUpD_minArea.Value = param.minarea;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}

        private void LineCoreSideInpect(object sender, EventArgs e)
        {
            //if (null == TMFun || null == Fun.m_hImage || !TorF)
            //{
            //    return;
            //}
            //if (radioBut_ModelImage.Checked)
            //{
            //    TMFun.TransModelImage();
            //}
            //if (radioBut_PreImage.Checked)
            //{
            //    TMFun.ModelImageTransBack();
            //}
            //Fun.ClearObjShow();
            //TMParam param = InitParam();
            //TMFun.setseiz();
            //if (!TMFun.FindNccModelYXSS(param, true, out Rect2 LineWeldROI, out LocateOutParams locateReult))
            //{
            //    MessageFun.ShowMessage("定位失败！");
            //    return;
            //}
            //TMFun.LineCoreSideInspectNEW(param.lineCoreSide, param.model_center, locateReult, true, out TMData.LineCoreSideResult outData);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Fun.m_arbitrary.dListCol == null)
            //    {
            //        if (GlobalData.Config._language == EnumData.Language.english)
            //        {
            //            MessageBox.Show("[Imaging window]-[The right mouse button]-[Draw]-[Aribtrary]");

            //        }
            //        else
            //        {
            //            MessageBox.Show("图像窗口-鼠标右键-绘制-选择“任意形状”");
            //        }

            //        return;
            //    }
            //    if (m_arbitrary.Contains(Fun.m_arbitrary)) //如果当前已添加，则返回
            //    {
            //        return;
            //    }
            //    m_arbitrary.Add(Fun.m_arbitrary);
            //    int n = comBox_LineSide_ROI.Items.Count;
            //    comBox_LineSide_ROI.Items.Add("检测区域" + (n + 1).ToString());
            //    if (GlobalData.Config._language == EnumData.Language.english)
            //    {
            //        comBox_LineSide_ROI.Items.Add("ROI" + (n + 1).ToString());
            //    }
            //    comBox_LineSide_ROI.SelectedIndex = n;
            //    Fun.m_arbitrary = new Arbitrary();
            //}
            //catch (SystemException ex)
            //{
            //    MessageFun.ShowMessage(ex.ToString());
            //    return;
            //}
        }

        Arbitrary pre_Arbitrary = new Arbitrary();
        private void comBox_LineSide_ROI_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int n = comBox_LineSide_ROI.SelectedIndex;

                if (n < m_arbitrary.Count())
                {
                    pre_Arbitrary = m_arbitrary[n];
                    //Fun.ShowArbitrary(pre_Arbitrary);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        private void but_LineSide_DelPreROI_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_arbitrary.Contains(pre_Arbitrary))
                {
                    m_arbitrary.Remove(pre_Arbitrary);
                    comBox_LineSide_ROI.Items.Clear();
                    foreach (Arbitrary arbitrary in m_arbitrary)
                    {
                        comBox_LineSide_ROI.Items.Add("检测区域" + (comBox_LineSide_ROI.Items.Count + 1).ToString());
                    }
                    if (0 != comBox_LineSide_ROI.Items.Count)
                    {
                        comBox_LineSide_ROI.SelectedIndex = 0;
                    }
                    else
                    {
                        comBox_LineSide_ROI.Text = "";
                    }
                    //Fun.GenArbitrary(m_arbitrary);
                    pre_Arbitrary = new Arbitrary();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void but_LineSide_DelAllROI_Click(object sender, EventArgs e)
        {
            try
            {
                m_arbitrary.Clear();
                Fun.m_arbitrary = new Arbitrary();
                Fun.m_hWnd.DispObj(Fun.m_hImage);
                Fun.ClearObjShow();
                pre_Arbitrary = new Arbitrary();
                comBox_LineSide_ROI.Items.Clear();
                comBox_LineSide_ROI.Text = "";
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void but_LineSide_ShowAllROI_Click(object sender, EventArgs e)
        {
            //Fun.GenArbitrary(m_arbitrary);
        }

        private void trackBar_minthd_Scroll(object sender, EventArgs e)
        {
            numUpD_minThd.Value = trackBar_minthd.Value;
        }
       
        private void trackBar_minArea_Scroll(object sender, EventArgs e)
        {

            numUpD_minArea.Value = trackBar_minArea.Value;
        }
        
        #endregion

        #region 线芯压脚上方飞丝

        //private LineOnWeldParam InitLineOnWeldParam()
        //{
        //    LineOnWeldParam param = new LineOnWeldParam();
        //    param.dDev = new double[2];
        //    param.dStandDev = new double[2];
        //    try
        //    {
        //        param.nThd = (int)numUpD_LineCopper_Thd.Value;
        //        if ("--" != label_LineCopper_Thd.Text)
        //        {
        //            param.nStandThd = int.Parse(label_LineCopper_Thd.Text);
        //        }
        //        trackBar_LineCopper_Thd.Value = (int)numUpD_LineCopper_Thd.Value;
        //        param.dDev[0] = (double)numUpD_LineCopper_Dev0.Value;
        //        if ("--" != label_LineCopper_Dev0.Text)
        //        {
        //            param.dStandDev[0] = double.Parse(label_LineCopper_Dev0.Text);
        //        }
        //        param.dDev[1] = (double)numUpD_LineCopper_Dev1.Value;
        //        if ("--" != label_LineCopper_Dev1.Text)
        //        {
        //            param.dStandDev[1] = double.Parse(label_LineCopper_Dev1.Text);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return param;
        //}

        //private void LoadLineOnWedlParam(LineOnWeldParam param)
        //{
        //    try
        //    {
        //        trackBar_LineCopper_Thd.Value = param.nThd;
        //        label_LineCopper_Thd.Text = param.nStandThd.ToString();
        //        numUpD_LineCopper_Thd.Value = param.nThd;
        //        if (null != param.dDev)
        //        {
        //            numUpD_LineCopper_Dev0.Value = (decimal)param.dDev[0];
        //            label_LineCopper_Dev0.Text = param.dStandDev[0].ToString();
        //            numUpD_LineCopper_Dev1.Value = (decimal)param.dDev[1];
        //            label_LineCopper_Dev1.Text = param.dStandDev[1].ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}
        private void LineWeldCopperInspect()
        {
            if (null == TMFun || null == Fun.m_hImage)
            {
                return;
            }
            if (radioBut_ModelImage.Checked)
            {
                //TMFun.TransModelImage();
            }
            if (radioBut_PreImage.Checked)
            {
                //TMFun.ModelImageTransBack();
            }
            Fun.ClearObjShow();
            //TMParam param = InitParam();
            //TMFun.setseiz();
            //if (!TMFun.FindNccModelYXSS(param, true, out Rect2 LineWeldROI, out _))
            //{
            //    MessageFun.ShowMessage("定位失败！");
            //    return;
            //}
            //TMFun.LineOnWeldInspect(param.lineOnWeld, m_LineWeldROI, out LineOnWeldResult outData);
            //label_LineCopper_Thd.Text = outData.nThd.ToString();
            //if (null != outData.dDeviation)
            //{
            //    label_LineCopper_Dev0.Text = outData.dDeviation[0].ToString();
            //    label_LineCopper_Dev1.Text = outData.dDeviation[1].ToString();
            //}

        }

        private void trackBar_LineCopper_Thd_Scroll(object sender, EventArgs e)
        {
            numUpD_LineCopper_Thd.Value = trackBar_LineCopper_Thd.Value;
        }

        private void numUpD_LineCopper_Thd_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                LineWeldCopperInspect();
            }

        }

        #endregion

        #region 线芯压脚位置露铜芯检测


        #endregion

        #region 端子鼻检测
        //private TMNoseParam InitTMNoseParam()
        //{
        //    TMNoseParam param = new TMNoseParam();
        //    try
        //    {
        //        //X偏移量
        //        param.nXMove = (int)numUpD_Nose_XMove.Value;
        //        trackBar_Nose_XMove.Value = (int)numUpD_Nose_XMove.Value;
        //        //Y偏移量
        //        param.nYMove = (int)numUpD_Nose_YMove.Value;
        //        trackBar_Nose_YMove.Value = (int)numUpD_Nose_YMove.Value;
        //        //宽度
        //        param.nROILen1 = (int)numUpD_Nose_Len1.Value;
        //        trackBar_Nose_Len1.Value = (int)numUpD_Nose_Len1.Value;
        //        //高度
        //        param.nROILen2 = (int)numUpD_Nose_Len2.Value;
        //        trackBar_Nose_Len2.Value = (int)numUpD_Nose_Len2.Value;
        //        //端子鼻长度
        //        param.dNoseLen = double.Parse(label_Nose_Len.Text);      //端子鼻长度
        //        param.nNoseLenLow = (int)numUpD_Nose_LenLow.Value;      //端子鼻长度下限
        //        param.nNoseLenHigh = (int)numUpD_Nose_LenHigh.Value;     //端子鼻长度上限
        //        //端子鼻角度
        //        param.dAngle = double.Parse(label_Nose_Angle.Text);        //端子鼻与端子的角度（非弧度）
        //        param.nAngleLow = (int)numUpD_Nose_AngleLow.Value;        //端子鼻与端子的角度下限（非弧度）
        //        param.nAngleHigh = (int)numUpD_Nose_AngleHigh.Value;       //端子鼻与端子的角度上限（非弧度）
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return param;
        //}
        //private void LoadTMNoseParam(TMNoseParam param)
        //{
        //    try
        //    {
        //        //X偏移量
        //        numUpD_Nose_XMove.Value = param.nXMove;
        //        trackBar_Nose_XMove.Value = (int)numUpD_Nose_XMove.Value;
        //        //Y偏移量
        //        numUpD_Nose_YMove.Value = param.nYMove;
        //        trackBar_Nose_YMove.Value = (int)numUpD_Nose_YMove.Value;
        //        //宽度
        //        numUpD_Nose_Len1.Value = param.nROILen1;
        //        trackBar_Nose_Len1.Value = (int)numUpD_Nose_Len1.Value;
        //        //高度
        //        numUpD_Nose_Len2.Value = param.nROILen2;
        //        trackBar_Nose_Len2.Value = (int)numUpD_Nose_Len2.Value;
        //        //端子鼻长度
        //        label_Nose_Len.Text = param.dNoseLen.ToString();      //端子鼻长度
        //        numUpD_Nose_LenLow.Value = param.nNoseLenLow;      //端子鼻长度下限
        //        label_Nose_LenMin.Text = (param.dNoseLen - param.nNoseLenLow).ToString();
        //        numUpD_Nose_LenHigh.Value = param.nNoseLenHigh;     //端子鼻长度上限
        //        label_Nose_LenMax.Text = (param.dNoseLen + param.nNoseLenHigh).ToString();
        //        //端子鼻角度
        //        label_Nose_Angle.Text = param.dAngle.ToString();        //端子鼻与端子的角度（非弧度）
        //        numUpD_Nose_AngleLow.Value = param.nAngleLow;        //端子鼻与端子的角度下限（非弧度）
        //        label_Nose_AngleMin.Text = (param.dAngle - param.nAngleLow).ToString();
        //        numUpD_Nose_AngleHigh.Value = param.nAngleHigh;       //端子鼻与端子的角度上限（非弧度）
        //        label_Nose_AngleMax.Text = (param.dAngle + param.nAngleHigh).ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}
        private void TMNoseInspect()
        {
        //    if (null == TMFun || null == Fun.m_hImage)
        //    {
        //        return;
        //    }
        //    if (radioBut_ModelImage.Checked)
        //    {
        //        TMFun.TransModelImage();
        //    }
        //    if (radioBut_PreImage.Checked)
        //    {
        //        TMFun.ModelImageTransBack();
        //    }
        //    Fun.ClearObjShow();
        //    TMParam param = InitParam();
        //    TMFun.setseiz();
        //    if (!TMFun.FindNccModelYXSS(param, true, out Rect2 LineWeldROI, out _))
        //    {
        //        MessageFun.ShowMessage("定位失败！");
        //        return;
        //    }
        //    if (TMFun.TMNoseInspect(param.tmNose, LineWeldROI, true, out TMNoseResult outData))
        //    {
        //        label_Nose_Len.Text = outData.dNoseLen.ToString();
        //        label_Nose_LenMin.Text = (outData.dNoseLen - param.tmNose.nNoseLenLow).ToString();
        //        label_Nose_LenMax.Text = (outData.dNoseLen + param.tmNose.nNoseLenHigh).ToString();
        //        label_Nose_Angle.Text = outData.dAngle.ToString();
        //        label_Nose_AngleMin.Text = (outData.dAngle - param.tmNose.nAngleLow).ToString();
        //        label_Nose_AngleMax.Text = (outData.dAngle + param.tmNose.nAngleHigh).ToString();
        //    }
        }
        private void trackBar_Nose_XMove_Scroll(object sender, EventArgs e)
        {
            numUpD_Nose_XMove.Value = trackBar_Nose_XMove.Value;
        }
        private void numUpD_Nose_XMove_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMNoseInspect();
            }

        }
        private void trackBar_Nose_YMove_Scroll(object sender, EventArgs e)
        {
            numUpD_Nose_YMove.Value = trackBar_Nose_YMove.Value;
        }
        private void numUpD_Nose_YMove_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMNoseInspect();
            }
        }

        private void trackBar_Nose_Len1_Scroll(object sender, EventArgs e)
        {
            numUpD_Nose_Len1.Value = trackBar_Nose_Len1.Value;
        }

        private void numUpD_Nose_Len1_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMNoseInspect();
            }
        }

        private void trackBar_Nose_Len2_Scroll(object sender, EventArgs e)
        {
            numUpD_Nose_Len2.Value = trackBar_Nose_Len2.Value;
        }

        private void numUpD_Nose_Len2_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMNoseInspect();
            }
        }
        private void numUpD_Nose_AngleHigh_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMNoseInspect();
            }
        }

        private void numUpD_Nose_LenHigh_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMNoseInspect();
            }
        }

        private void numUpD_Nose_AngleLow_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMNoseInspect();
            }
        }

        private void numUpD_Nose_LenLow_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMNoseInspect();
            }
        }
        #endregion
        private void but_SetModel_Click_1(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Fun.m_rect2.dLength1 == 0 && Fun.m_rect2.dLength2 == 0)
            //    {
            //        if (GlobalData.Config._language == EnumData.Language.english)
            //        {
            //            MessageBox.Show("Please go to Image window - Right mouse button - Draw and select “Rectangle 2”, draw the conductor crimp ROI, and right click to confirm!");
            //        }
            //        else
            //        {
            //            MessageBox.Show("请在图像窗口-鼠标右键-绘制-选择“矩形2”，画出线芯压脚区域,并右键确认完成。");
            //        }
            //        return;
            //    }
            //    if (TMFun.CreateModel(out model_ID, out m_LineWeldROI, out m_LocateResult))
            //    {
            //        //保存模板
            //        string model_name = "camera" + m_ncam.ToString() + "_" + m_TMType.ToString();
            //        Fun.WriteModel(model_name, ModelType.ncc, model_ID);
            //        //保存模板图像
            //        string strPath = System.IO.Path.Combine(GlobalPath.SavePath.ModelImagePath, model_name);
            //        Fun.SaveImageWithoutDate(strPath);
            //        TMFun.m_ModelImage = Fun.m_hImage.Clone();
            //        if (GlobalData.Config._language == EnumData.Language.english)
            //        {
            //            MessageBox.Show("The model is created successfully!");
            //        }
            //        else
            //        {
            //            MessageBox.Show("模板创建成功！");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //    if (GlobalData.Config._language == EnumData.Language.english)
            //    {
            //        MessageBox.Show("The model is created failed!");
            //    }
            //    else
            //    {
            //        MessageBox.Show("模板创建失败！");
            //    }

            //}

        }
        private void but_TestModel_Click(object sender, EventArgs e)
        {
            //TMFun.FindNccModelYXSS(InitParam(), true, out Rect2 newLineWeldROI, out LocateOutParams locateData);
        }
        private void radioBut_ModelImage_CheckedChanged(object sender, EventArgs e)
        {
            if (null != TMFun)
            {
                //TMFun.TransModelImage();
            }

        }
        private void radioBut_PreImage_CheckedChanged(object sender, EventArgs e)
        {
            if (null != TMFun)
            {
                //TMFun.ModelImageTransBack();
            }
        }
        
        private void button_text_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioBut_ModelImage.Checked)
                {
                    //TMFun.TransModelImage();
                }
                if (radioBut_PreImage.Checked)
                {
                    //TMFun.ModelImageTransBack();
                }
                Fun.ClearObjShow();
                //TMParam param = InitParam();
                //SaveParam();
                //TMFun.Duanzi(m_ncam, m_TMType, out TMData.TMResult tMResult);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return;
            }
        }
        //private void SaveParam()
        //{
        //    try
        //    {
        //        if (TMData_Serializer._globalData.dic_TMParam.ContainsKey(m_ncam))
        //        {
        //            if (TMData_Serializer._globalData.dic_TMParam[m_ncam].ContainsKey(m_TMType))
        //            {
        //                TMData_Serializer._globalData.dic_TMParam[m_ncam][m_TMType] = InitParam();
        //            }
        //            else
        //            {
        //                TMData_Serializer._globalData.dic_TMParam[m_ncam].Add(m_TMType, InitParam());
        //            }
        //        }
        //        else
        //        {
        //            Dictionary<string, TMData.TMParam> tMParams = new Dictionary<string, TMData.TMParam>();
        //            tMParams.Add(m_TMType, InitParam());
        //            TMData_Serializer._globalData.dic_TMParam.Add(m_ncam, tMParams);
        //        }
        //        Dictionary<string, TMData.TMParam> dicTMParam = TMData_Serializer._globalData.dic_TMParam[m_ncam];
        //        foreach (string nType in dicTMParam.Keys)
        //        {
        //            if (m_TMType == nType)
        //            {
        //                TMData.TMParam tmData = dicTMParam[m_TMType];
        //                tmData.lineColor.colorID = m_colorID;
        //                dicTMParam[m_TMType] = tmData;
        //                break;
        //            }
        //        }
        //        TMData_Serializer._globalData.dic_TMParam[m_ncam] = dicTMParam;
        //        //导入序列化参数
        //        SaveData.SaveTMData();

        //    }
        //    catch (SystemException error)
        //    {
        //        MessageFun.ShowMessage("端子检测数据保存出错！" + error.ToString(), "Error when saving terminal detect data!" + error.ToString());
        //        return;
        //    }

        //}
        private void FormTMTeach_Load(object sender, EventArgs e)
        {
            LoadParam();
        }
        //实时检测
        private void button4_Click(object sender, EventArgs e)
        {
            Fun.ClearObjShow();
            Fun.b_image = false;
            CamSDK.CamCommon.GrabImage(str_CamSer);
            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
            ////SaveParam();
            //while (true)
            //{
            //    if (Fun.b_image)
            //    {
            //        TimeSpan ts2_z = new TimeSpan(DateTime.Now.Ticks);
            //        double spanTotalSeconds2 = ts2_z.Subtract(ts).Duration().TotalMilliseconds;
            //        MessageFun.ShowMessage("拍照用时：" + spanTotalSeconds2.ToString(), "Image capture time：" + spanTotalSeconds2.ToString());
            //        if (TMFun.Duanzi(m_ncam, m_TMType, out TMData.TMResult tMResult))
            //        {
            //            SaveParam();
            //            break;
            //        }
            //        break;
            //    }
            //    TimeSpan ts3_z = new TimeSpan(DateTime.Now.Ticks);
            //    double spanTotalSeconds = ts3_z.Subtract(ts).Duration().TotalSeconds;
            //    if (spanTotalSeconds > 0.3)
            //    {
            //        Fun.WriteStringtoImage(25, 40, 20, "抓图超时！", "Capture timeout!");
            //        break;
            //    }
            //}
        }

        #region 端子头检测
        //private TMheadParam InitTMheadParam()
        //{
        //    TMheadParam param = new TMheadParam();
        //    try
        //    {
        //        //间距
        //        param.nROIGap = (int)numericUpDown_head_Gap.Value;
        //        trackBar_head_Gap.Value = (int)numericUpDown_head_Gap.Value;
        //        //宽度
        //        param.nROIWidth = (int)numericUpDown_head_Width.Value;
        //        trackBar_head_Width.Value = (int)numericUpDown_head_Width.Value;
        //        //高度
        //        param.nROIHeight = (int)numericUpDown_head_Height.Value;
        //        trackBar_head_Height.Value = (int)numericUpDown_head_Height.Value;
        //        //亮度
        //        param.nLineThd = (int)numericUpDown_head_Thd.Value;
        //        trackBar_head_Thd.Value = (int)numericUpDown_head_Thd.Value;
        //        //面积
        //        param.Area = int.Parse(label_head_minArea.Text);
        //        //最小面积
        //        param.AreaMin = (double)numericUpDown_haed_lowArea.Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return param;
        //}
        //private void LoadTMheadParam(TMheadParam param)
        //{
        //    try
        //    {
        //        //间距
        //        numericUpDown_head_Gap.Value = param.nROIGap;
        //        trackBar_head_Gap.Value = (int)numericUpDown_head_Gap.Value;
        //        //宽度
        //        numericUpDown_head_Width.Value = param.nROIWidth;
        //        trackBar_head_Width.Value = (int)numericUpDown_head_Width.Value;
        //        //高度
        //        numericUpDown_head_Height.Value = param.nROIHeight;
        //        trackBar_head_Height.Value = (int)numericUpDown_head_Height.Value;
        //        //亮度
        //        numericUpDown_head_Thd.Value = param.nLineThd;
        //        trackBar_head_Thd.Value = (int)numericUpDown_head_Thd.Value;
        //        //面积
        //        label_head_minArea.Text = param.Area.ToString();
        //        //最小面积
        //        numericUpDown_haed_lowArea.Value = (decimal)param.AreaMin;
        //        label_head_minArea.Text = (param.Area * param.AreaMin).ToString("F0");
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}
        private void TMheadInspect()
        {
        //    if (null == TMFun || null == Fun.m_hImage)
        //    {
        //        return;
        //    }
        //    if (radioBut_ModelImage.Checked)
        //    {
        //        TMFun.TransModelImage();
        //    }
        //    if (radioBut_PreImage.Checked)
        //    {
        //        TMFun.ModelImageTransBack();
        //    }
        //    Fun.ClearObjShow();
        //    TMParam param = InitParam();
        //    TMFun.setseiz();
        //    if (!TMFun.FindNccModelYXSS(param, true, out Rect2 LineWeldROI, out _))
        //    {
        //        MessageFun.ShowMessage("定位失败！", "Locate failed!");
        //        return;
        //    }
        //    if (TMFun.headInspect(param.tmhead, LineWeldROI, true, out TMheadResult outData))
        //    {
        //        label_head_minArea.Text = outData.dArea.ToString();
        //        label_head_minArea.Text = (outData.dArea * param.tmhead.AreaMin).ToString("F0");
        //    }
        }
        private void numericUpDown_head_Gap_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMheadInspect();
            }
        }
        #endregion

        private void numericUpDown_head_Width_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMheadInspect();
            }
        }

        private void numericUpDown_head_Height_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMheadInspect();
            }
        }

        private void numericUpDown_head_Thd_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMheadInspect();
            }
        }

        private void numericUpDown_haed_lowArea_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                //TMheadInspect();
            }
        }

        private void trackBar_head_Gap_Scroll(object sender, EventArgs e)
        {
            numericUpDown_head_Gap.Value = trackBar_head_Gap.Value;
        }

        private void trackBar_head_Width_Scroll(object sender, EventArgs e)
        {
            numericUpDown_head_Width.Value = trackBar_head_Width.Value;
        }

        private void trackBar_head_Height_Scroll(object sender, EventArgs e)
        {
            numericUpDown_head_Height.Value = trackBar_head_Height.Value;
        }

        private void trackBar_head_Thd_Scroll(object sender, EventArgs e)
        {
            numericUpDown_head_Thd.Value = trackBar_head_Thd.Value;
        }

        private void radioBut_LinePos_methodOutline_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBut_LinePos_methodOutline.Checked)
            {
                tabPage_BackGround.Parent = null;
                tabPage_LineLigth.Parent = null;
                tabPage_LineColor.Parent = null;
                tabPage_LineOutline.Parent = tabControl1;
            }
        }



        #region 线序检测
        //private TMData.LineColorParam InitLineColorParam()
        //{
        //    TMData.LineColorParam param = new TMData.LineColorParam();
        //    try
        //    {
        //        param.bInspect = true;
        //        param.colorID = m_colorID;
        //        param.nROIGap = (int)numUpD_lineColor_gap.Value;            //ROI间距
        //        param.nROIWidth = (int)numUpD_lineColor_width.Value;        //ROI宽度
        //        param.nROIHeight = (int)numUpD_lineColor_height.Value;      //ROI高度
        //        param.nGrayDiff = (int)numUpD_lineColor_GrayDiff.Value;     //灰度差
        //        param.nMinArea = (int)numUpD_LineColor_MinArea.Value;       //识别出的颜色区域的最小面积
        //    }
        //    catch (Exception ex)
        //    {
        //        StaticFun.MessageFun.ShowMessage(ex.ToString());
        //    }
        //    return param;
        //}
        //private void LoadLineColorParam(LineColorParam data)
        //{
        //    try
        //    {
        //        TMData.LineColorParam param = new LineColorParam();
        //        /*导入检测参数*/
        //        if (TMData_Serializer._globalData.dic_LineColor.ContainsKey(m_ncam))
        //        {

        //            param = TMData_Serializer._globalData.dic_LineColor[m_ncam];
        //        }
        //        else if (data.bInspect)
        //        {
        //            param = data;
        //        }
        //        numUpD_lineColor_gap.Value = param.nROIGap;
        //        numUpD_lineColor_width.Value = param.nROIWidth;
        //        numUpD_lineColor_height.Value = param.nROIHeight;
        //        numUpD_lineColor_GrayDiff.Value = param.nGrayDiff;
        //        if(param.nMinArea>=1)
        //            numUpD_LineColor_MinArea.Value = param.nMinArea;
        //        m_colorID = param.colorID;
        //    }
        //    catch (SystemException ex)
        //    {
        //        StaticFun.MessageFun.ShowMessage(ex.ToString());
        //    }
        //}

        private void LineColorInspect(object sender, EventArgs e)
        {
        //    if (null == TMFun || null == Fun.m_hImage ||!TorF)
        //    {
        //        return;
        //    }
        //    if (radioBut_ModelImage.Checked)
        //    {
        //        TMFun.TransModelImage();
        //    }
        //    if (radioBut_PreImage.Checked)
        //    {
        //        TMFun.ModelImageTransBack();
        //    }
        //    Fun.ClearObjShow();
        //    TMParam param = InitParam();
        //    TMFun.setseiz();
        //    if (!TMFun.FindNccModelYXSS(param, true, out Rect2 LineWeldROI, out _))
        //    {
        //        MessageFun.ShowMessage("定位失败！");
        //        return;
        //    }
        //    TMFun.LineColor_MLP(param.lineColor, LineWeldROI, true, out LineColorResult lineColorResult);
        }

        private void but_CreateColorModel_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    TMParam param = InitParam();
            //    if (!TMFun.FindNccModelYXSS(param, true, out Rect2 LineWeldROI, out _))
            //    {
            //        MessageFun.ShowMessage("定位失败！");
            //        return;
            //    }
            //    if (TMFun.CreateColorClassGmm(param.lineColor, LineWeldROI, true, out ColorID colorID))
            //    {
            //        //m_listID = new List<ColorID>();
            //        m_colorID = colorID;
            //        TMFun.WriteLineColor(m_ncam, "线序模板" + m_TMType.ToString(), colorID);
            //        MessageBox.Show("线序颜色模型创建成功！");
            //    }
            //    else
            //    {
            //        MessageBox.Show("线序颜色模型创建失败！");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //}
        }

        private void trackBar_lineColor_gap_Scroll(object sender, EventArgs e)
        {
            numUpD_lineColor_gap.Value = (int)trackBar_lineColor_gap.Value;
        }

        private void trackBar_lineColor_width_Scroll(object sender, EventArgs e)
        {
            numUpD_lineColor_width.Value = (int)trackBar_lineColor_width.Value;
        }

        private void trackBar_lineColor_height_Scroll(object sender, EventArgs e)
        {
            numUpD_lineColor_height.Value = (int)trackBar_lineColor_height.Value;
        }
        private void trackBar_LineColor_MinArea_Scroll(object sender, EventArgs e)
        {
            numUpD_LineColor_MinArea.Value = (int)trackBar_LineColor_MinArea.Value;
        }

        #endregion

        private void TMwingInspect()
        {
            if (null == TMFun || null == Fun.m_hImage)
            {
                return;
            }
            if (radioBut_ModelImage.Checked)
            {
               // TMFun.TransModelImage();
            }
            if (radioBut_PreImage.Checked)
            {
               // TMFun.ModelImageTransBack();
            }
            Fun.ClearObjShow();
            ////TMParam param = InitParam();
            //TMFun.setseiz();
            //if (!TMFun.FindNccModelYXSS(param, true, out Rect2 LineWeldROI, out _))
            //{
            //    MessageFun.ShowMessage("定位失败！", "Locate failed!");
            //    return;
            //}
            //if (TMFun.wingInspect(param.tmwing, LineWeldROI, true, out TMwingResult outData))
            //{
            //    label_wing_distance.Text = outData.ddistance.ToString("F0");
            //    label_wing_maxdistance.Text = (outData.ddistance *numericUpDown_wing_maxdistance.Value).ToString("F0");
            //    label_wing_mindistance.Text = (outData.ddistance *numericUpDown_wing_mindistance.Value).ToString("F0");
            //}
        }
        //private TMwingParam InitTMwingParam()
        //{
        //    TMwingParam param = new TMwingParam();
        //    try
        //    {
        //        //间距
        //        param.nROIGap = (int)numericUpDown_wing_Gap.Value;
        //        trackBar_wing_Gap.Value = (int)numericUpDown_wing_Gap.Value;
        //        //宽度
        //        param.nROIWidth = (int)numericUpDown_wing_Width.Value;
        //        trackBar_wing_Width.Value = (int)numericUpDown_wing_Width.Value;
        //        //高度
        //        param.nROIHeight = (int)numericUpDown_wing_Height.Value;
        //        trackBar_wing_Height.Value = (int)numericUpDown_wing_Height.Value;
        //        //亮度
        //        param.nLineThd = (int)numericUpDown_wing_Thd.Value;
        //        trackBar_wing_Thd.Value = (int)numericUpDown_wing_Thd.Value;
        //        //面积
        //        param.Area = (int)numericUpDown_wing_Area.Value;
        //        trackBar_wing_Area.Value = (int)numericUpDown_wing_Area.Value;
        //        //标准距离
        //        param.ddistance = double.Parse(label_wing_distance.Text);        //标准距离
        //        //最大百分比
        //        param.dmaxdistance= (double)numericUpDown_wing_maxdistance.Value;
        //        label_wing_maxdistance.Text = (param.ddistance * param.dmaxdistance).ToString("F0");
        //        //最小百分比
        //        param.dmindistance = (double)numericUpDown_wing_mindistance.Value;
        //        label_wing_mindistance.Text = (param.ddistance * param.dmindistance).ToString("F0");
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return param;
        //}
        //private void LoadTMwingParam(TMwingParam param)
        //{
        //    try
        //    {
        //        //间距
        //        numericUpDown_wing_Gap.Value = param.nROIGap;
        //        trackBar_wing_Gap.Value = (int)numericUpDown_wing_Gap.Value;
        //        //宽度
        //        numericUpDown_wing_Width.Value = param.nROIWidth;
        //        trackBar_wing_Width.Value = (int)numericUpDown_wing_Width.Value;
        //        //高度
        //        numericUpDown_wing_Height.Value = param.nROIHeight;
        //        trackBar_wing_Height.Value = (int)numericUpDown_wing_Height.Value;
        //        //亮度
        //        numericUpDown_wing_Thd.Value = param.nLineThd;
        //        trackBar_wing_Thd.Value = (int)numericUpDown_wing_Thd.Value;
        //        //面积
        //        numericUpDown_wing_Area.Value = param.Area;
        //        trackBar_wing_Area.Value = (int)numericUpDown_wing_Area.Value;

        //        //标准距离
        //         label_wing_distance.Text= param.ddistance.ToString();        //标准距离
        //        //最大百分比
        //         numericUpDown_wing_maxdistance.Value=(decimal)param.dmaxdistance;
        //        label_wing_maxdistance.Text = (param.ddistance * param.dmaxdistance).ToString("F0");
        //        //最小百分比
        //         numericUpDown_wing_mindistance.Value= (decimal)param.dmindistance;
        //        label_wing_mindistance.Text = (param.ddistance * param.dmindistance).ToString("F0");
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}
        private void trackBar_wing_Gap_Scroll(object sender, EventArgs e)
        {
            numericUpDown_wing_Gap.Value = trackBar_wing_Gap.Value;
        }

        private void trackBar_wing_Width_Scroll(object sender, EventArgs e)
        {
            numericUpDown_wing_Width.Value = trackBar_wing_Width.Value;
        }

        private void trackBar_wing_Height_Scroll(object sender, EventArgs e)
        {
            numericUpDown_wing_Height.Value = trackBar_wing_Height.Value;
        }

        private void numericUpDown_wing_Gap_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                TMwingInspect();
            }
        }

        private void numericUpDown_wing_Width_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                TMwingInspect();
            }
        }

        private void numericUpDown_wing_Height_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                TMwingInspect();
            }
        }

        private void trackBar_wing_Thd_Scroll(object sender, EventArgs e)
        {
            numericUpDown_wing_Thd.Value = trackBar_wing_Thd.Value;
        }

        private void trackBar_wing_Area_Scroll(object sender, EventArgs e)
        {
            numericUpDown_wing_Area.Value = trackBar_wing_Area.Value;
        }

        private void numericUpDown_wing_Thd_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                TMwingInspect();
            }
        }

        private void numericUpDown_wing_Area_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                TMwingInspect();
            }
        }

        private void numericUpDown_wing_lowArea_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                TMwingInspect();
            }
        }

        private void numericUpDown_wing_higArea_ValueChanged(object sender, EventArgs e)
        {
            if (TorF)
            {
                TMwingInspect();
            }
        }

        private void button_newModel_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox_Model.Text == "" || comboBox_Model.Text == "teach")
                {
                    return;
                }
                string a = m_ncam.ToString() + sub_cam.ToString();
                string c = comboBox_Model.Text;
                if (TMData_Serializer._globalData.dicTMCheckList.ContainsKey(a))
                {
                    if (TMData_Serializer._globalData.dicTMCheckList[a].ContainsKey(c))
                    {
                        MessageBox.Show("当前模板已存在！");
                        return;
                    }
                    else
                    {
                        TMCheckItem tmCheckItem = new();
                        TMData_Serializer._globalData.dicTMCheckList[a].Add(c, tmCheckItem);
                        comboBox_Model.Items.Add(a);
                        comboBox_SelectModel.Items.Add(a);
                    }
                    m_TMType = c;
                    TorF = false;
                    InitUI();
                    LoadParam();
                    tabCtrl_InspectItem.Refresh();
                }
                else
                {
                    TMCheckItem tmCheckItem = new();
                    Dictionary<string, TMCheckItem > tm= new();
                    tm.Add(c, tmCheckItem);
                    TMData_Serializer._globalData.dicTMCheckList.Add(a,tm);
                }
            }
            catch (Exception ex)
            {
                MessageFun.ShowMessage("添加端子模板出错：" + ex.ToString());
            }

        }
        //public TMData.TMCheckList InitTMCheckList()
        //{
        //    TMData.TMCheckList param = new TMData.TMCheckList();
        //    try
        //    {
        //        param.bSkinWeld = true;
        //        param.bSkinPos = true;
        //        param.bLineWeld = true;
        //        param.bLinePos = false;
        //        param.bLineSide = false;
        //        param.bLineOnWeld = false;
        //        param.bTMNose = false;
        //        param.bTMHead = false;
        //        param.bLineColor = false;
        //        param.bTMWing = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageFun.ShowMessage("端子检测配置出错：" + ex.ToString());
        //    }
        //    return param;
        //}

        private void button_delModel_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox_Model.Text == "" || comboBox_Model.Text == "teach")
                {
                    return;
                }
                string c = m_ncam.ToString() + sub_cam.ToString();
                if (TMData_Serializer._globalData.dicTMCheckList.ContainsKey(c))
                {
                    string a = comboBox_Model.Text;
                    string b = comboBox_SelectModel.Text;
                    if (TMData_Serializer._globalData.dicTMCheckList[c].ContainsKey(a))
                    {
                        TMData_Serializer._globalData.dicTMCheckList[c].Remove(a);

                        comboBox_Model.Items.Remove(a);
                        comboBox_SelectModel.Items.Remove(a);
                        if (b == a)
                        {
                            comboBox_SelectModel.Text = "teach";
                        }
                        comboBox_Model.Text = "teach";
                        TorF = false;
                        InitUI();
                        LoadParam();
                        tabCtrl_InspectItem.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("无当前选择模板！");
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageFun.ShowMessage("删除端子模板出错：" + ex.ToString());
            }
        }

        private void comboBox_SelectModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (TorF)
            //{
            //    if (comboBox_SelectModel.Text == "")
            //    {
            //        return;
            //    }
            //    TMData_Serializer._globalData.dic_selectmodel[m_ncam] = comboBox_SelectModel.Text;
            //    if (comboBox_SelectModel.Text != "自动")
            //    {
            //        comboBox_Model.Text = comboBox_SelectModel.Text;
            //    }
            //    //导入序列化参数
            //    //SaveData.SaveTMData();
            //}
        }

        private void comboBox_Model_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (TorF)
            //{
            //    if (comboBox_Model.Text == "")
            //    {
            //        return;
            //    }
            //    if (TMData_Serializer._globalData.dicCheckList.ContainsKey(m_ncam))
            //    {
            //        string a = comboBox_Model.Text;
            //        if (TMData_Serializer._globalData.dicCheckList[m_ncam].ContainsKey(a))
            //        {
            //            if (m_TMType == a)
            //            {
            //                return;
            //            }
            //            m_TMType = a;
            //            TorF = false;
            //            InitUI();
            //            LoadParam();
            //            tabCtrl_InspectItem.Refresh();
            //        }
            //        else
            //        {
            //            MessageBox.Show("无当前选择模板！");
            //            return;
            //        }
            //    }
            //}

        }
    }
}
