using System;
using System.Drawing;
using System.Windows.Forms;
using static VisionPlatform.TMData;

namespace VisionPlatform
{
    public partial class FormLightCH : Form
    {
        bool isLoad = true;
        CamInspectItem m_camItem;
        public FormLightCH(int cam, int sub_cam, TMData.InspectItem inspectItem, TMData.SurfaceType surfaceType = default, TMData.DetectionType type = default)
        {
            InitializeComponent();
            m_camItem.cam = cam;
            m_camItem.sub_cam = sub_cam;
            m_camItem.item = inspectItem;
            m_camItem.surfaceType = surfaceType;
            m_camItem.type = type;
            InitUI();
        }

        private void InitUI()
        {
            try
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox3.Visible = false;
                checkBox4.Visible = false;
                checkBox5.Visible = false;
                checkBox6.Visible = false;
                if (GlobalData.Config._InitConfig.initConfig.bDigitLight)
                {
                    if (GlobalData.Config._InitConfig.initConfig.nLightCH == 4)
                    {
                        checkBox3.Visible = true;
                        checkBox4.Visible = true;
                    }

                    if (GlobalData.Config._InitConfig.initConfig.nLightCH == 6)
                    {
                        checkBox3.Visible = true;
                        checkBox4.Visible = true;
                        checkBox5.Visible = true;
                        checkBox6.Visible = true;
                    }
                }
                this.Text = "相机" + m_camItem.cam.ToString() + "：" + TMFunction.GetStrCheckItem(m_camItem.item);
            }
            catch (Exception)
            {

            }
        }

        private LightCtrlSet InitParam()
        {
            LightCtrlSet param = new LightCtrlSet();
            param.CH = new bool[6];  //默认为false
            param.nBrightness = new int[6];  //默认为false
            if (!isLoad)
            {
                try
                {
                    param.camItem.cam = m_camItem.cam;
                    param.camItem.item = m_camItem.item;
                    param.camItem.sub_cam = m_camItem.sub_cam;
                    param.camItem.surfaceType = m_camItem.surfaceType;
                    param.camItem.type = m_camItem.type;
                    if (checkBox1.Checked)
                    {
                        param.CH[0] = true;
                    }
                    if (checkBox2.Checked)
                    {
                        param.CH[1] = true;
                    }
                    if (checkBox3.Checked)
                    {
                        param.CH[2] = true;
                    }
                    if (checkBox4.Checked)
                    {
                        param.CH[3] = true;
                    }
                    if (checkBox5.Checked)
                    {
                        param.CH[4] = true;
                    }
                    if (checkBox6.Checked)
                    {
                        param.CH[5] = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return param;
        }

        private void LightChannelChange(System.Windows.Forms.CheckBox checkBox, int CH)
        {
            try
            {
                if (!isLoad)
                {
                    if (checkBox.Checked)
                    {
                        checkBox.BackColor = Color.Green;
                    }
                    else
                    {
                        checkBox.BackColor = Color.Transparent;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void FormLightCH_Load(object sender, EventArgs e)
        {
            try
            {
                isLoad = true;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                //加载配置
                if (null != TMData_Serializer._globalData.listLightCtrl)
                {
                    for (int i = 0; i < TMData_Serializer._globalData.listLightCtrl.Count; i++)
                    {
                        LightCtrlSet lightCtrl = TMData_Serializer._globalData.listLightCtrl[i];
                        if (m_camItem.cam == lightCtrl.camItem.cam && m_camItem.item == lightCtrl.camItem.item &&
                            m_camItem.sub_cam == lightCtrl.camItem.sub_cam && m_camItem.type == lightCtrl.camItem.type &&
                            m_camItem.surfaceType == lightCtrl.camItem.surfaceType)
                        {
                            if (null != lightCtrl.CH)
                            {
                                if (lightCtrl.CH[0])
                                {
                                    checkBox1.Checked = true;
                                    checkBox1.BackColor = Color.Green;
                                }
                                if (lightCtrl.CH[1])
                                {
                                    checkBox2.Checked = true;
                                    checkBox2.BackColor = Color.Green;
                                }
                                if (lightCtrl.CH[2])
                                {
                                    checkBox3.Checked = true;
                                    checkBox3.BackColor = Color.Green;
                                }
                                if (lightCtrl.CH[3])
                                {
                                    checkBox4.Checked = true;
                                    checkBox4.BackColor = Color.Green;
                                }
                                if (lightCtrl.CH[4])
                                {
                                    checkBox5.Checked = true;
                                    checkBox5.BackColor = Color.Green;
                                }
                                if (lightCtrl.CH[5])
                                {
                                    checkBox6.Checked = true;
                                    checkBox6.BackColor = Color.Green;
                                }
                            }
                        }
                    }

                }
                isLoad = false;
            }
            catch (Exception)
            {
                isLoad = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            LightChannelChange(this.checkBox1, 1);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            LightChannelChange(this.checkBox2, 2);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            LightChannelChange(this.checkBox3, 3);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            LightChannelChange(this.checkBox4, 4);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            LightChannelChange(this.checkBox5, 5);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            LightChannelChange(this.checkBox6, 6);
        }

        private void but_Confirm_Click(object sender, EventArgs e)
        {
            StaticFun.SaveData.SaveLightConfig(InitParam());
            this.Close();
        }
    }

}