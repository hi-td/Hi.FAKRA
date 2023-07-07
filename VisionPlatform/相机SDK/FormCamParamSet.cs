using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CamSDK;
using static VisionPlatform.TMData;

namespace VisionPlatform
{
    public partial class FormCamParamSet : Form
    {
        CamCommon.CamParam camParam = new CamCommon.CamParam();
        FormLedSet formLedSet;
        string m_strCamSer;
        int m_cam;
        public FormCamParamSet(int cam, string strCamSer)
        {
            InitializeComponent();
            m_strCamSer = strCamSer;
            m_cam = cam;
            this.Text = "画质调节：" + strCamSer.ToString();
            LoadCam();
        }

        public bool LoadCam()
        {
            try
            {
                camParam = CamCommon.GetCamParam(m_strCamSer);
                textBox_expourse.Text = camParam.exposure.ToString("F1");
                textBox_gain.Text = camParam.gain.ToString("F1");
               // textBox_FrameRate.Text = camParam.frame.ToString("F1");
               // label_FrameRate.Text = camParam.frame.ToString("F1");
                this.groupBox_Light.Controls.Clear();
                if (GlobalData.Config._InitConfig.initConfig.bDigitLight)
                {
                    groupBox_Light.Visible = true;
                    foreach (LightCtrlSet ledSet in TMData_Serializer._globalData.listLightCtrl)
                    {
                        if (ledSet.camItem.cam == m_cam)
                        {
                            //string str_item = TMFunction.GetStrCheckItem(ledSet.camItem.item);
                            //if (TMData_Serializer._globalData.dicInspectList[m_cam].Contains(ledSet.camItem.item))
                            //{
                            //    for (int n=0;n<ledSet.CH.Length;n++)
                            //    {
                            //        if (ledSet.CH[n])
                            //        {
                            //            int brightness = 0;
                            //            if (null != ledSet.nBrightness)
                            //            {
                            //                brightness = ledSet.nBrightness[n];
                            //            }
                            //            int ch = n + 1;
                            //            CamInspectItem camInspect = new CamInspectItem()
                            //            { 
                            //                cam = m_cam, 
                            //                item = ledSet.camItem.item
                            //            };
                            //            formLedSet = new FormLedSet(camInspect, ch, brightness);
                            //            formLedSet.TopLevel = false;
                            //            formLedSet.Visible = true;
                            //            formLedSet.Dock = DockStyle.Top;
                            //            formLedSet.Font = new Font("宋体", 9);
                            //            this.groupBox_Light.Controls.Add(formLedSet);
                            //        }
                            //    }
                            //}
                        }
                    }
                }
                return true;

            }
            catch (SystemException ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.ToString());
                return false;
            }
        }

        private void trackBar_Exposure_Scroll(object sender, EventArgs e)
        {
            textBox_expourse.Text = trackBar_Exposure.Value.ToString();
            CamCommon.SetExposure(m_strCamSer, trackBar_Exposure.Value);
            camParam.exposure = trackBar_Exposure.Value;
        }

        private void trackBar_Gain_Scroll(object sender, EventArgs e)
        {
            textBox_gain.Text = trackBar_Gain.Value.ToString();
            CamCommon.SetGain(m_strCamSer, trackBar_Gain.Value);
            camParam.gain = trackBar_Gain.Value;
        }

        private void textBox_expourse_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox_expourse.Text != "")
                {
                    float expourse = -1F;
                    if (!float.TryParse(textBox_expourse.Text, out expourse))
                    {
                        textBox_expourse.Text = "";
                        MessageBox.Show("当前输入值类型有误，请检查!（只能输入数值！）");
                        return;
                    }

                    if (expourse < 0 || expourse > 99999)
                    {
                        MessageBox.Show("当前输入值不合法，请检查！取值范围（0--99999）");
                        return;
                    }
                    trackBar_Exposure.Value = (int)expourse;
                    label_expourse.Text = Convert.ToString(trackBar_Exposure.Value);
                }
                else
                {
                    label_expourse.Text = "0";
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("输入有误！ ");
            }
        }

        private void textBox_gain_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox_gain.Text != "")
                {
                    float gain = -1;
                    if (!float.TryParse(textBox_gain.Text, out gain))
                    {
                        MessageBox.Show("当前输入值类型有误，请检查!（只能输入数值！）");
                        textBox_gain.Text = "";
                        return;
                    }

                    if (gain < 0 || gain > 9999)
                    {
                        MessageBox.Show("当前输入值不合法，请检查！取值范围（0--9999）");
                        return;
                    }
                    trackBar_Gain.Value = (int)gain;
                    label_gain.Text = Convert.ToString(trackBar_Gain.Value);
                }
                else
                {
                    label_gain.Text = "0";
                    return;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("输入有误！ ");
            }
        }

        private void FormCamParamSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool bFlag = true;
            foreach (int n in GlobalData.Config._CamConfig.camConfig.Keys)
            {
                if (GlobalData.Config._CamConfig.camConfig[n] == m_strCamSer)
                {
                    bFlag = false;
                    TMData_Serializer._globalData.camParam[m_strCamSer] = camParam;
                    break;
                }
            }
            if (bFlag)
            {
                StaticFun.MessageFun.ShowMessage("请先配置相机。");
                return;
            }
            var json = JsonConvert.SerializeObject(GlobalData.Config._CamConfig);
            System.IO.File.WriteAllText(GlobalPath.SavePath.CamConfigPath, json);

            var json1 = JsonConvert.SerializeObject(TMData_Serializer._COMConfig);
            System.IO.File.WriteAllText(GlobalPath.SavePath.IOPath, json1);
        }
    }

}
