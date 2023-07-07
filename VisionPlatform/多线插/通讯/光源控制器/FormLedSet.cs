using StaticFun;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;


namespace VisionPlatform
{
    public partial class FormLedSet : Form
    {
        int nCH = -1;   //当前光源通道
        bool bLoadParam = false;      //控件值的改变是否是因为界面初始化加载参数
        int m_bright = -1;
        TMData.CamInspectItem m_camItem;
        public FormLedSet(TMData.CamInspectItem camItem, int CH, int brightness)
        {
            InitializeComponent();
            this.label_CH.Text = "通道" + CH.ToString();
            this.label_CheckItem.Text = TMFunction.GetStrCheckItem(camItem.item);
            m_camItem = camItem;
            nCH = CH;
            m_bright = brightness;
        }

        private void numUpD_Brightness_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (bLoadParam) return;
                if(LEDSet.SetBrightness(nCH, (int)numUpD_Brightness.Value))
                {
                    trackBar_Brightness.Value = (int)numUpD_Brightness.Value;
                    for (int i = 0; i < TMData_Serializer._globalData.listLightCtrl.Count; i++)
                    {
                            if(TMData_Serializer._globalData.listLightCtrl[i].camItem.cam == m_camItem.cam && TMData_Serializer._globalData.listLightCtrl[i].camItem.item == m_camItem.item)
                            {
                                TMData_Serializer._globalData.listLightCtrl[i].nBrightness[nCH - 1] = trackBar_Brightness.Value;
                            }
                    }
                    //int n = 0;
                    //foreach(TMData.LightCtrlSet lightCtrlSet in TMData_Serializer._COMConfig.listLightCtrl)
                    //{
                    //    if(lightCtrlSet.camItem.cam == m_camItem.cam && lightCtrlSet.camItem.item == m_camItem.item)
                    //    {
                    //        lightCtrlSet.nBrightness[nCH - 1] = trackBar_Brightness.Value;
                    //        TMData_Serializer._COMConfig.listLightCtrl[n] = lightCtrlSet;
                    //        return;
                    //    }
                    //    n++;
                    //}
                }
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void trackBar_Brightness_Scroll(object sender, EventArgs e)
        {
            numUpD_Brightness.Value = trackBar_Brightness.Value;
        }

        private void FormLedSet_Load(object sender, EventArgs e)
        {
            try
            {
                bLoadParam = true;
                trackBar_Brightness.Value = m_bright;
                numUpD_Brightness.Value = m_bright;
                bLoadParam = false;
            }
            catch (Exception)
            {

            }
        }
    }
}
