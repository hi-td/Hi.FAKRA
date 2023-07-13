using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
namespace VisionPlatform
{
    public partial class FormImageColorTrans : Form
    {
        FormCamShow formCamShow;
        Dictionary<string, PictureBox> dic_picBox = new Dictionary<string, PictureBox>();
        ColorSpace m_colorSpace;
        TMData.ColorSpaceParam m_colorSpaceParam;
        Label m_label_Img, m_label_colorSpace;
        public FormImageColorTrans(int cam, int sub_cam, Label label_Img, Label colorSpace)
        {
            InitializeComponent();
            m_label_Img = label_Img;
            m_label_colorSpace = colorSpace;
           //m_colorSpaceParam = colorSpaceParam;
            Refresh(cam, sub_cam);
            InitUI();
        }
        private void InitUI()
        {
            try
            {
                dic_picBox = new Dictionary<string, PictureBox>();
                dic_picBox.Add("灰度图", picBox_gray);
                dic_picBox.Add("Image1", picBox_Image1);
                dic_picBox.Add("Image2", picBox_Image2);
                dic_picBox.Add("Image3", picBox_Image3);
                dic_picBox.Add("Result1", picBox_Result1);
                dic_picBox.Add("Result2", picBox_Result2);
                dic_picBox.Add("Result3", picBox_Result3);
                formCamShow.TM_fun.InitPicBox(dic_picBox);
                checkBox_gray.Checked = true;
                formCamShow.TM_fun.PicShow("");
            }
            catch (Exception ex)
            {

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
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        private void FormImageColorTrans_Load(object sender, EventArgs e)
        {
            //return m_colorSpaceParam;
            if(m_colorSpaceParam.colorSpace == ColorSpace.gray)
            {
                checkBox_gray.Checked = true;
            }
            else if(m_colorSpaceParam.colorSpace == ColorSpace.rgb)
            {
                int nchannel = m_colorSpaceParam.nChannel;
                switch(nchannel)
                    {
                    case 1:
                        checkBox_image1.Checked = true;

                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default:
                        break;
                
                }

            }
        }
        private void FormImageColorTrans_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormTeachMaster.m_panelWindow.Controls.Clear();
            FormTeachMaster.m_panelWindow.Controls.Add(formCamShow);
        }

        private void checkBox_gray_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_gray.Checked)
            {
                checkBox_image1.Checked = false;
                checkBox_image2.Checked = false;
                checkBox_image3.Checked = false;
                checkBox_result1.Checked = false;
                checkBox_result2.Checked = false;
                checkBox_result3.Checked = false;
                textBox_gray.BackColor = Color.LightGreen;
                textBox_image1.BackColor = Color.White;
                textBox_image2.BackColor = Color.White;
                textBox_image3.BackColor = Color.White;
                textBox_result1.BackColor = Color.White;
                textBox_result2.BackColor = Color.White;
                textBox_result3.BackColor = Color.White;
                m_colorSpaceParam.colorSpace = ColorSpace.gray;
                m_colorSpaceParam.nChannel = 0;
                //formCamShow.fun.dic_colorSpace.Clear();
                //formCamShow.fun.dic_colorSpace.Add(ColorSpace.gray, 0);
                // m_label_Img.Text = "灰度图";
                m_label_Img.Text = "无";
                m_label_colorSpace.Text = ColorSpace.gray.ToString();
            }
        }

        private void checkBox_image1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_image1.Checked)
            {
                checkBox_gray.Checked = false;
                checkBox_image2.Checked = false;
                checkBox_image3.Checked = false;
                checkBox_result1.Checked = false;
                checkBox_result2.Checked = false;
                checkBox_result3.Checked = false;
                textBox_gray.BackColor = Color.White;
                textBox_image1.BackColor = Color.LightGreen;
                textBox_image2.BackColor = Color.White;
                textBox_image3.BackColor = Color.White;
                textBox_result1.BackColor = Color.White;
                textBox_result2.BackColor = Color.White;
                textBox_result3.BackColor = Color.White;
                m_colorSpaceParam.colorSpace = ColorSpace.rgb;
                m_colorSpaceParam.nChannel = 1;
                //formCamShow.fun.dic_colorSpace.Clear();
                //formCamShow.fun.dic_colorSpace.Add(ColorSpace.rgb, 1);
                m_label_Img.Text = "通道1";
                m_label_colorSpace.Text = "rgb";
            }
        }

        private void checkBox_image2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_image2.Checked)
            {
                checkBox_gray.Checked = false;
                checkBox_image1.Checked = false;
                checkBox_image3.Checked = false;
                checkBox_result1.Checked = false;
                checkBox_result2.Checked = false;
                checkBox_result3.Checked = false;
                textBox_gray.BackColor = Color.White;
                textBox_image1.BackColor = Color.White;
                textBox_image2.BackColor = Color.LightGreen;
                textBox_image3.BackColor = Color.White;
                textBox_result1.BackColor = Color.White;
                textBox_result2.BackColor = Color.White;
                textBox_result3.BackColor = Color.White;
                //formCamShow.fun.dic_colorSpace.Clear();
                //formCamShow.fun.dic_colorSpace.Add(ColorSpace.rgb, 2);
                m_colorSpaceParam.colorSpace = ColorSpace.rgb;
                m_colorSpaceParam.nChannel = 2;
                m_label_Img.Text = "通道2";
                m_label_colorSpace.Text = "rgb";
            }
        }

        private void checkBox_image3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_image3.Checked)
            {
                checkBox_gray.Checked = false;
                checkBox_image2.Checked = false;
                checkBox_image1.Checked = false;
                checkBox_result1.Checked = false;
                checkBox_result2.Checked = false;
                checkBox_result3.Checked = false;
                textBox_gray.BackColor = Color.White;
                textBox_image1.BackColor = Color.White;
                textBox_image2.BackColor = Color.White;
                textBox_image3.BackColor = Color.LightGreen;
                textBox_result1.BackColor = Color.White;
                textBox_result2.BackColor = Color.White;
                textBox_result3.BackColor = Color.White;
                // formCamShow.fun.dic_colorSpace.Clear();
                // formCamShow.fun.dic_colorSpace.Add(ColorSpace.rgb, 3);
                m_colorSpaceParam.colorSpace = ColorSpace.rgb;
                m_colorSpaceParam.nChannel = 3;
                m_label_Img.Text = "通道3";
                m_label_colorSpace.Text = "rgb";
            }
        }

        private void checkBox_result1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_result1.Checked)
            {
                checkBox_gray.Checked = false;
                checkBox_image1.Checked = false;
                checkBox_image2.Checked = false;
                checkBox_image3.Checked = false;
                checkBox_result2.Checked = false;
                checkBox_result3.Checked = false;
                textBox_gray.BackColor = Color.White;
                textBox_image1.BackColor = Color.White;
                textBox_image2.BackColor = Color.White;
                textBox_image3.BackColor = Color.White;
                textBox_result1.BackColor = Color.LightGreen;
                textBox_result2.BackColor = Color.White;
                textBox_result3.BackColor = Color.White;
                //  formCamShow.fun.dic_colorSpace.Clear();
                // formCamShow.fun.dic_colorSpace.Add(m_colorSpace, 1);
                m_colorSpaceParam.colorSpace = m_colorSpace;
                m_colorSpaceParam.nChannel = 1;
                m_label_Img.Text = "转换图1";
                m_label_colorSpace.Text = comboBox1.Text;
            }
        }

        private void checkBox_result2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_result2.Checked)
            {
                checkBox_gray.Checked = false;
                checkBox_image1.Checked = false;
                checkBox_image2.Checked = false;
                checkBox_image3.Checked = false;
                checkBox_result1.Checked = false;
                checkBox_result3.Checked = false;
                textBox_gray.BackColor = Color.White;
                textBox_image1.BackColor = Color.White;
                textBox_image2.BackColor = Color.White;
                textBox_image3.BackColor = Color.White;
                textBox_result1.BackColor = Color.White;
                textBox_result2.BackColor = Color.LightGreen;
                textBox_result3.BackColor = Color.White;
                //formCamShow.fun.dic_colorSpace.Clear();
                // formCamShow.fun.dic_colorSpace.Add(m_colorSpace, 2);
                m_colorSpaceParam.colorSpace = m_colorSpace;
                m_colorSpaceParam.nChannel = 2;
                m_label_Img.Text = "转换图2";
                m_label_colorSpace.Text = comboBox1.Text;
            }
        }

        private void checkBox_result3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_result3.Checked)
            {
                checkBox_gray.Checked = false;
                checkBox_image1.Checked = false;
                checkBox_image2.Checked = false;
                checkBox_image3.Checked = false;
                checkBox_result2.Checked = false;
                checkBox_result1.Checked = false;
                textBox_gray.BackColor = Color.White;
                textBox_image1.BackColor = Color.White;
                textBox_image2.BackColor = Color.White;
                textBox_image3.BackColor = Color.White;
                textBox_result1.BackColor = Color.White;
                textBox_result2.BackColor = Color.White;
                textBox_result3.BackColor = Color.LightGreen;
                // formCamShow.fun.dic_colorSpace.Clear();
                //  formCamShow.fun.dic_colorSpace.Add(m_colorSpace, 3);
                m_colorSpaceParam.colorSpace = m_colorSpace;
                m_colorSpaceParam.nChannel = 3;
                m_label_Img.Text = "转换图3";
                m_label_colorSpace.Text = comboBox1.Text;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strColorSpace = comboBox1.Text;
            if ("" == strColorSpace)
            {
                return;
            }
            if (strColorSpace == "hsv")
            {
                m_colorSpace = ColorSpace.hsv;

            }
            else if (strColorSpace == "hsi")
            {
                m_colorSpace = ColorSpace.hsi;

            }
            else if (strColorSpace == "hls")
            {
                m_colorSpace = ColorSpace.hls;
            }
            else if (strColorSpace == "lms")
            {
                m_colorSpace = ColorSpace.lms;
            }
            else if (strColorSpace == "ihs")
            {
                m_colorSpace = ColorSpace.ihs;
            }
            formCamShow.TM_fun.PicShow(strColorSpace);
            m_colorSpaceParam.colorSpace = m_colorSpace;
        }
    }
}
