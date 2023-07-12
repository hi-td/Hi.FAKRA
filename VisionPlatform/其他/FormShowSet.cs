using Chustange.Functional;
using Newtonsoft.Json;
using StaticFun;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static VisionPlatform.TMData;

namespace VisionPlatform
{
    public partial class FormShowSet : Form
    {
        TMData.TMShowSet m_tmShowSet = new TMData.TMShowSet();
        Function Fun;
        TMFunction TMFun;
        int m_cam;
        int sub_cam;
        public FormShowSet(int ncam, int sub_cam)
        {
            InitializeComponent();
            m_cam = ncam;
            this.sub_cam = sub_cam;
            //RefreshFun();
            checkBox_LeftUp.Checked = true;
        }

        private void RefreshFun()
        {
            int camNum = GlobalData.Config._InitConfig.initConfig.CamNum;
            //string a = m_cam.ToString()+ bh;
            switch (camNum)
            {
                case 1:
                    TMFun = Show1.formCamShow1.TM_fun;
                    Fun = Show1.formCamShow1.fun;
                    break;
                case 2:
                    //if (m_cam == 1)
                    //{
                    //    TMFun = Show2.formCamShow1.TM_fun;
                    //    Fun = Show2.formCamShow1.fun;
                    //    break;
                    //}
                    //if (m_cam == 2)
                    //{
                    //    TMFun = Show2.formCamShow2.TM_fun;
                    //    Fun = Show2.formCamShow2.fun;
                    //}
                    break;
                //case 3:
                //    if (m_cam == 1)
                //    {
                //        TMFun = Show3.formCamShow1.TM_fun;
                //        Fun = Show3.formCamShow1.fun;
                //        str_CamSer = Show3.formCamShow1.m_strCamSer;
                //        break;
                //    }
                //    if (m_cam == 2)
                //    {
                //        TMFun = Show3.formCamShow2.TM_fun;
                //        Fun = Show3.formCamShow2.fun;
                //        str_CamSer = Show3.formCamShow2.m_strCamSer;
                //    }
                //    if (m_cam == 3)
                //    {
                //        TMFun = Show3.formCamShow3.TM_fun;
                //        Fun = Show3.formCamShow3.fun;
                //        str_CamSer = Show3.formCamShow3.m_strCamSer;
                //        break;
                //    }
                //    break;
                case 7:
                        //TMFun = Show6.formCamShows[a].TM_fun;
                        //Fun = Show6.formCamShows[a].fun;
                        break;
                default:
                    break;
            }

        }

        private void RefreshData()
        {
            try
            {
                m_tmShowSet.bLeftUp = checkBox_LeftUp.Checked;
                m_tmShowSet.nLeftUpShowPos = (int)numUpD_LeftUp.Value;
                m_tmShowSet.nLeftDownShowPos = (int)numUpD_LeftDown.Value;
                m_tmShowSet.nFrontSize = (int)numUpD_FrontSize.Value;
                m_tmShowSet.nRowGap = (int)numUpD_RowGap.Value;
                m_tmShowSet.nColGap = (int)numUpD_ColGap.Value;
                Save();
            }
            catch (Exception ex)
            {

            }
        }
        private void Save()
        {
            try
            {
                TMData_Serializer._globalData.tmShowSet = m_tmShowSet;
                var json = JsonConvert.SerializeObject(TMData_Serializer._globalData.tmShowSet);
                System.IO.File.WriteAllText(GlobalPath.SavePath.TMShowSetPath, json);
                Fun.m_hWnd.ClearWindow();
                Fun.m_hWnd.DispObj(Fun.m_hImage);
                //if (strCams == "端子检测")
                //{
                //    if (TMFun.MultiTMInspect(TMData_Serializer._globalData.dic_MultiTMParam[m_cam],
                //                             TMData_Serializer._globalData.dicTMCheckList[m_cam], false,
                //                             out TMData.MultiResult result))
                //    {
                //        TMFun.MultiTMShowResult(TMData_Serializer._globalData.dic_MultiTMParam[m_cam], result, TMData_Serializer._globalData.dicTMCheckList[m_cam], out int DzNumOK, out int DzNumNG);
                //    }

                //}             
            }

            catch (Exception ex)
            {
                ("打端检测字体设置出错：" + ex.ToString()).ToLog();
                return;
            }
        }

        private void FormShowSet_Load(object sender, EventArgs e)
        {
            try
            {
                StaticFun.LoadConfig.LoadTMSet();
                TMData.TMShowSet tmShowSet = TMData_Serializer._globalData.tmShowSet;
                checkBox_LeftUp.Checked = tmShowSet.bLeftUp;
                numUpD_LeftUp.Value = tmShowSet.nLeftUpShowPos;
                numUpD_LeftDown.Value = tmShowSet.nLeftDownShowPos;
                numUpD_FrontSize.Value = tmShowSet.nFrontSize;
                numUpD_RowGap.Value = tmShowSet.nRowGap;
                numUpD_ColGap.Value = tmShowSet.nColGap;
            }
            catch (Exception ex)
            {

            }
        }

        private void trackBar_LeftUp_Scroll(object sender, EventArgs e)
        {
            numUpD_LeftUp.Value = trackBar_LeftUp.Value;
        }
        private void trackBar_LeftDown_Scroll(object sender, EventArgs e)
        {
            numUpD_LeftDown.Value = trackBar_LeftDown.Value;
        }

        private void numUpD_LeftUp_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
            trackBar_LeftUp.Value = (int)numUpD_LeftUp.Value;
        }

        private void numUpD_LeftDown_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
            trackBar_LeftDown.Value = (int)numUpD_LeftDown.Value;
        }
        private void checkBox_LeftUp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_LeftUp.Checked)
            {
                checkBox_LeftDown.Checked = false;
            }
            else
            {
                checkBox_LeftDown.Checked = true;
            }
        }

        private void checkBox_LeftDown_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_LeftDown.Checked)
            {
                checkBox_LeftUp.Checked = false;
            }
            else
            {
                checkBox_LeftUp.Checked = true;
            }
        }


        private void numUpD_FrontSize_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void numUpD_RowGap_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
            trackBar_RowGap.Value = (int)numUpD_RowGap.Value;
        }

        private void trackBar_RowGap_Scroll(object sender, EventArgs e)
        {
            numUpD_RowGap.Value = trackBar_RowGap.Value;
        }

        private void numpD_ColGap_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
            trackBar_ColGap.Value = (int)numUpD_ColGap.Value;
        }

        private void trackBar_ColGap_Scroll(object sender, EventArgs e)
        {
            numUpD_ColGap.Value = trackBar_ColGap.Value;
        }


    }
}
