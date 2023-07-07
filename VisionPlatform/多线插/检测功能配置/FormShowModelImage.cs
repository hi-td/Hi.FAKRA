using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VisionPlatform
{
    public partial class FormShowModelImage : Form
    {
        private string m_strImgPath = "";
        private  Function funmodel =new Function(null);
        private TMFunction tMFunction=null;

        TMData.DataAll m_global { get; set; }
        public FormShowModelImage(TMData.DataAll global)
        {
            InitializeComponent();
            m_global = global;
        }

        private void LoadModel()
        {
            try
            {
                int n = (int)m_global.Camera;
                funmodel.m_hWndCtrl =hWndCtrl;
                funmodel.m_hWnd = hWndCtrl.HalconWindow;
                tMFunction = new TMFunction(funmodel);
                m_strImgPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "模板图像\\ModelImage" + "相机" + n);
                funmodel.ReadImage(m_strImgPath);
            }
            catch(SystemException error)
            {
                Function.ShowMessage("模板导入错误：" + error.ToString());
                return;
            }
        }
        
        private void FormShowModelImage_Load(object sender, EventArgs e)
        {
            LoadModel();
        }

        private bool Inspect(Dictionary<TMData.SelectedCam, List<string>> dicCheckList, TMData.ProPrecessParam[] proPrecessParam)
        {
            bool bResult = true;
            //try
            //{
            //    if (0 == dicCheckList.Count || null == proPrecessParam)
            //    {
            //        MessageBox.Show("无检测项！");
            //        return false;
            //    }
            //    if ("" != m_strImgPath)
            //    {
            //        funmodel.ReadImage(m_strImgPath);
            //    }
            //    //压端子检测
            //    if (0 != dicCheckList.Count)
            //    {
            //            bool[] bFlags = new bool[dicCheckList[m_global.Camera].Count];
            //            int i = 0;
            //            int n = (int)m_global.Camera - 1;
            //            Rect2 skinWeldROISDSs = new Rect2();
            //            Rect2 skinWeldROISDS = new Rect2();
            //            //if (!tMFunction.FindNccModelYXSS(m_global.Camera, TMData_Serializer._globalData.proPrecessParam[n], out skinWeldROISDSs))
            //            //{
            //            //    MessageBox.Show("未匹配到模板。");
            //            //    return false;
            //            //}
            //            //Rect2 m_SkinWeldROI = new Rect2();
            //            //Rect2 ROI = new Rect2();
            //            //ROI.dRect2Row = 3000;
            //            //ROI.dRect2Col = 3000;
            //            //ROI.dPhi = 0;
            //            //ROI.dLength1 = 3000;
            //            //ROI.dLength2 = 3000;
            //            //tMFunction.PreProcess(ROI, false, TMData.SelRegion.right, out m_SkinWeldROI);
            //           // tMFunction.PreProcessSD();
            //            if (dicCheckList[m_global.Camera].Count == 0)
            //            {
            //                MessageBox.Show("无压端子检测子项。");
            //                return false;
            //            }
            //           // tMFunction.SelectRegion(skinWeldROISDSs, false, TMData.SelRegion.max, out skinWeldROISDS);
            //            foreach (string strCheck in dicCheckList[m_global.Camera])
            //            {

            //                if (strCheck == "绝缘皮压脚")
            //                {
            //                    TMData.SkinWeldInspectResult result = new TMData.SkinWeldInspectResult();
            //                    if (tMFunction.SkinWeldInspect(proPrecessParam[n], skinWeldROISDS, out result))
            //                    {
            //                        bFlags[i] = result.bFlag;
            //                        i++;
            //                    }
            //                    else
            //                    {
            //                        // "绝缘皮压脚检测出错。".ToLog();
            //                        bResult = false;
            //                        return false;
            //                    }
            //                }
            //                if (strCheck == "绝缘皮位置")
            //                {
            //                    TMData.SkinPosInspectResult result = new TMData.SkinPosInspectResult();
            //                    if (tMFunction.SkinPosInspect(proPrecessParam[n], skinWeldROISDS, out result))
            //                    {
            //                        bFlags[i] = result.bFlag;
            //                        i++;
            //                    }
            //                    else
            //                    {
            //                        // "绝缘皮位置检测出错。".ToLog();
            //                        bResult = false;
            //                        return false;
            //                    }
            //                }
            //                if (strCheck == "线芯压脚")
            //                {
            //                    TMData.LineCoreWeldResult result = new TMData.LineCoreWeldResult();
            //                    if (tMFunction.LineCoreWeldInspect(proPrecessParam[n], skinWeldROISDS, out result))
            //                    {
            //                        bFlags[i] = result.bFlag;
            //                        i++;
            //                    }
            //                    else
            //                    {
            //                        // "线芯压脚检测出错。".ToLog();
            //                        bResult = false;
            //                        return false;
            //                    }
            //                }
            //                if (strCheck == "线芯位置")
            //                {
            //                    TMData.LineCorePosInspectResult result = new TMData.LineCorePosInspectResult();
            //                    if (tMFunction.LineCorePosInspect(proPrecessParam[n], skinWeldROISDS, out result))
            //                    {
            //                        bFlags[i] = result.bFlag;
            //                        i++;

            //                    }
            //                    else
            //                    {
            //                        //  "线芯位置检测出错。".ToLog();
            //                        bResult = false;
            //                        return false;
            //                    }
            //                }
            //                if (strCheck == "线芯飞边")
            //                {
            //                    TMData.LineCoreSideInspectResult result = new TMData.LineCoreSideInspectResult();
            //                    if (tMFunction.LineCoreSideInspect(proPrecessParam[n], skinWeldROISDS, out result))
            //                    {
            //                        bFlags[i] = result.bFlag;
            //                        i++;
            //                    }
            //                    else
            //                    {
            //                        //  "线芯位置检测出错。".ToLog();
            //                        bResult = false;
            //                        return false;
            //                    }
            //                }
            //            }
            //            foreach (bool flag in bFlags)
            //            {
            //                if (!flag)
            //                {
            //                    bResult = false;
            //                    funmodel.WriteStringtoImage(30, 12, 12, "NG");
            //                    break;
            //                }
            //            }
            //            if(bResult)
            //            {
            //                funmodel.WriteStringtoImage(30, 12, 12, "OK");
            //            }
                    
            //    }
               return true;
            //}
            //catch (SystemException ex)
            //{
            //    Function.ShowMessage(ex.ToString());
            //    return false;
            //}
        }

        private void but_ModelData_Click(object sender, EventArgs e)
        {
            try
            {
                int n = (int)m_global.Camera;
                Dictionary<TMData.SelectedCam, List<string>> dicCheckList = new Dictionary<TMData.SelectedCam, List<string>>();
                dicCheckList = TMData_Serializer._globalData.dicCheckList;
                TMData.ProPrecessParam[] proPrecessParam = new TMData.ProPrecessParam[dicCheckList.Keys.Count];
                proPrecessParam = TMData_Serializer._globalData.proPrecessParam;
                //Function.GetHalc6onWnd(hWndCtrl);
                Inspect(dicCheckList, proPrecessParam);
                //Function.GetHalconWnd(m_HWndCtrl);
            }
            catch(SystemException ex)
            {
                ex.ToString();
            }
        }

        private void but_ProTestData_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<TMData.SelectedCam, List<string>> dicCheckList = new Dictionary<TMData.SelectedCam, List<string>>();
                if(null == m_global.m_dicCheckList)
                {
                    return;
                }
                dicCheckList = m_global.m_dicCheckList;
                TMData.ProPrecessParam[] proPrecessParam = new TMData.ProPrecessParam[dicCheckList.Keys.Count];
                proPrecessParam = m_global.proPrecessParams;
                //Function.GetHalconWnd(hWndCtrl);
                Inspect(dicCheckList, proPrecessParam);
                //Function.GetHalconWnd(m_HWndCtrl);
            }
            catch(SystemException ex)
            {
                ex.ToString();
            }
        }

        private void FormShowModelImage_Resize(object sender, EventArgs e)
        {
            int n = (int)m_global.Camera;
            //Function.GetHalconWnd(hWndCtrl);
            funmodel.ReadImage(m_strImgPath);
        }

        private void FormShowModelImage_MouseLeave(object sender, EventArgs e)
        {
            //Function.GetHalconWnd(m_HWndCtrl);
            //this.TopMost = false;
        }

        private void FormShowModelImage_MouseEnter(object sender, EventArgs e)
        {
            //Function.GetHalconWnd(hWndCtrl);
        }

        private void FormShowModelImage_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Function.GetHalconWnd(m_HWndCtrl);
        }
    }
}
