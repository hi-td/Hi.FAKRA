using Chustange.Functional;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace VisionPlatform
{
    public partial class FormShowSet : Form
    {
        TMData.TMShowSet m_tmShowSet = new TMData.TMShowSet();
        Function Fun;
        TMFunction TMFun;
        int m_cam;
        int sub_cam;
        string strCamSer;
        public FormShowSet(int ncam, int sub_cam)
        {
            InitializeComponent();
            m_cam = ncam;
            this.sub_cam = sub_cam;
            StaticFun.UIConfig.RefreshFun(m_cam, sub_cam, ref Fun, ref TMFun, ref strCamSer);
            checkBox_LeftUp.Checked = true;
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
