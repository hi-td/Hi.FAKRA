using Chustange.Functional;
using EnumData;
using StaticFun;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static VisionPlatform.TMData;

namespace VisionPlatform
{
    //光源控制
    class LEDSet
    {
        public static bool isOpen = false;      //监听光源控制器串口是否打开
        private static SerialPort ComDevice = new SerialPort();
        int[] nSetLed = new int[6];
        public static bool OpenLedcom(TMData.LEDRTU led)
        {
            try
            {
                if(null == led.PortName)
                {
                    return false;
                }
                ComDevice.PortName = led.PortName;
                ComDevice.BaudRate = led.BaudRate;
                ComDevice.Parity = led.parity;
                ComDevice.DataBits = led.DataBits;
                ComDevice.StopBits = led.stopBits;
                if (ComDevice.IsOpen)
                {
                    ComDevice.Close();
                }
                ComDevice.Open();
                isOpen = true;
                MessageFun.ShowMessage("光源控制器串口打开成功!");
                Thread.Sleep(2);
                return true;
            }
            catch (Exception ex)
            {
                isOpen = false;
                MessageBox.Show("光源控制器串口打开失败：" + ex.ToString());
                return false;
            }
        }

        public static void CloseLED()
        {
            ComDevice.Close();
            isOpen = false;
        }

        /// <summary>
        /// 设置光源某通道的亮度
        /// </summary>
        /// <param name="CH"></param>  通道，从1开始
        /// <param name="brightness"></param> 亮度
        public static bool SetBrightness(int CH, int brightness)
        {
            string str_Bright = "";
            try
            {
                int length = brightness.ToString().Length;
                if (length == 1)
                {
                    str_Bright = "00" + brightness.ToString();
                }
                else if (length == 2)
                {
                    str_Bright = "0" + brightness.ToString();
                }
                else
                {
                    str_Bright = brightness.ToString();
                }
                string SendData = "";
                switch (CH)
                {
                    case 1:
                        SendData = "SA0";
                        break;
                    case 2:
                        SendData = "SB0";
                        break;
                    case 3:
                        SendData = "SC0";
                        break;
                    case 4:
                        SendData = "SD0";
                        break;
                    case 5:
                        SendData = "SE0";
                        break;
                    case 6:
                        SendData = "SF0";
                        break;
                    default:
                        break;
                }
                SendData = SendData + str_Bright + "#";
                if (isOpen)
                {
                    byte[] SendBytes = null;
                    SendBytes = System.Text.Encoding.Default.GetBytes(SendData);
                    ComDevice.Write(SendBytes, 0, SendBytes.Length);//发送数据
                }
                else
                {
                    MessageBox.Show("光源控制器通讯异常，请检查串口！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage("设置光源通道" + CH.ToString() + "亮度错误：" + ex.ToString());
                return false;
            }
        }
       
        private static readonly object setCHLock = new object();

        //public static void SetLEDCH(int cam, out int[] nSetLed, out Dictionary<int, List<int>> nDelLed)
        //{
        //    nSetLed = new int[5];
        //    nDelLed = new Dictionary<int, List<int>>();
        //    try
        //    {
        //        List<TMData.InspectItem> listItems = TMData_Serializer._globalData.dicInspectList[cam];
        //        TMData.CamInspectItem camItem1_stripLen = new TMData.CamInspectItem();
        //        TMData.CamInspectItem camItem1_Rubber = new TMData.CamInspectItem();
        //        TMData.CamInspectItem camItem1_TM = new TMData.CamInspectItem();
        //        TMData.CamInspectItem camItem1_LineColor = new TMData.CamInspectItem();
        //        TMData.CamInspectItem camItem1_CoreNum = new TMData.CamInspectItem();
        //        camItem1_stripLen.cam = cam;
        //        camItem1_Rubber.cam = cam;
        //        camItem1_TM.cam = cam;
        //        camItem1_LineColor.cam = cam;
        //        camItem1_CoreNum.cam = cam;

        //        //线芯检测
        //        var StripLen = listItems.Where(x => x == InspectItem.StripLen).FirstOrDefault();
        //        if (StripLen == InspectItem.StripLen)
        //        {
        //            camItem1_stripLen.item = TMData.InspectItem.StripLen;
        //            bool bContains = false;
        //            var tempStripLen = new List<int>();
        //            foreach (LightCtrlSet LEDSet in TMData_Serializer._COMConfig.listLightCtrl)
        //            {
        //                if (LEDSet.camItem.cam == camItem1_stripLen.cam && LEDSet.camItem.item == camItem1_stripLen.item)
        //                {
        //                    nSetLed[0] = LEDSet.nCH;
        //                }
        //                else
        //                {
        //                    tempStripLen.Add(LEDSet.nCH);
        //                }
        //            }
        //            nDelLed.Add(0, tempStripLen);
        //            if (!bContains)
        //            {
        //                StaticFun.MessageFun.ShowMessage("相机" + cam.ToString() + "无【剥皮检测】检测项光源配置！");
        //                return;
        //            }
        //        }

        //        //插壳检测
        //        var Rubber = listItems.Where(x => x == InspectItem.Rubber).FirstOrDefault();
        //        if (Rubber == TMData.InspectItem.Rubber)
        //        {
        //            camItem1_Rubber.item = TMData.InspectItem.Rubber;
        //            bool bContains = false;
        //            var tempRubber = new List<int>();
        //            foreach (LightCtrlSet LEDSet in TMData_Serializer._COMConfig.listLightCtrl)
        //            {
        //                if (LEDSet.camItem.cam == camItem1_Rubber.cam && LEDSet.camItem.item == camItem1_Rubber.item)
        //                {
        //                    nSetLed[1] = LEDSet.nCH;
        //                }
        //                else
        //                {
        //                    tempRubber.Add(LEDSet.nCH);
        //                }
        //            }
        //            nDelLed.Add(1, tempRubber);
        //            if (!bContains)
        //            {
        //                StaticFun.MessageFun.ShowMessage("相机" + cam.ToString() + "无【插壳检测】检测项光源配置！");
        //                return;
        //            }
        //        }

        //        //打端检测
        //        var TM = listItems.Where(x => x == InspectItem.TM).FirstOrDefault();
        //        if (TM == TMData.InspectItem.TM)
        //        {
        //            camItem1_TM.item = TMData.InspectItem.TM;
        //            bool bContains = false;
        //            var tempTM = new List<int>();
        //            foreach (LightCtrlSet LEDSet in TMData_Serializer._COMConfig.listLightCtrl)
        //            {
        //                if (LEDSet.camItem.cam == camItem1_TM.cam && LEDSet.camItem.item == camItem1_TM.item)
        //                {
        //                    nSetLed[2] = LEDSet.nCH;
        //                }
        //                else
        //                {
        //                    tempTM.Add(LEDSet.nCH);
        //                }
        //            }
        //            nDelLed.Add(2, tempTM);
        //            if (!bContains)
        //            {
        //                StaticFun.MessageFun.ShowMessage("相机" + cam.ToString() + "无【端子检测】检测项光源配置！");
        //                return;
        //            }
        //        }

        //        //线序检测
        //        var LineColor = listItems.Where(x => x == InspectItem.LineColor).FirstOrDefault();
        //        if (LineColor == TMData.InspectItem.LineColor)
        //        {
        //            camItem1_LineColor.item = TMData.InspectItem.LineColor;
        //            bool bContains = false;
        //            var tempLineColor = new List<int>();
        //            foreach (LightCtrlSet LEDSet in TMData_Serializer._COMConfig.listLightCtrl)
        //            {
        //                if (LEDSet.camItem.cam == camItem1_LineColor.cam && LEDSet.camItem.item == camItem1_LineColor.item)
        //                {
        //                    nSetLed[3] = LEDSet.nCH;
        //                }
        //                else
        //                {
        //                    tempLineColor.Add(LEDSet.nCH);
        //                }
        //            }
        //            nDelLed.Add(3, tempLineColor);
        //            if (!bContains)
        //            {
        //                StaticFun.MessageFun.ShowMessage("相机" + cam.ToString() + "无【线序检测】检测项光源配置！");
        //                return;
        //            }
        //        }

        //        //线芯检测
        //        var CoreNum = listItems.Where(x => x == InspectItem.CoreNum).FirstOrDefault();
        //        if (CoreNum == TMData.InspectItem.CoreNum)
        //        {
        //            camItem1_CoreNum.item = TMData.InspectItem.StripLen;
        //            bool bContains = false;
        //            var tempCoreNum = new List<int>();
        //            foreach (LightCtrlSet LEDSet in TMData_Serializer._COMConfig.listLightCtrl)
        //            {
        //                if (LEDSet.camItem.cam == camItem1_CoreNum.cam && LEDSet.camItem.item == camItem1_CoreNum.item)
        //                {
        //                    nSetLed[4] = LEDSet.nCH;
        //                }
        //                else
        //                {
        //                    tempCoreNum.Add(LEDSet.nCH);
        //                }
        //            }
        //            nDelLed.Add(4, tempCoreNum);
        //            if (!bContains)
        //            {
        //                StaticFun.MessageFun.ShowMessage("相机" + cam.ToString() + "无【线芯检测】检测项光源配置！");
        //                return;
        //            }
        //        }

        //        //foreach (TMData.InspectItem item in listItems)
        //        //{
        //        //    if (item == TMData.InspectItem.StripLen)
        //        //    {

        //        //    }
        //        //    if (item == TMData.InspectItem.Rubber)
        //        //    {
        //        //        camItem1_Rubber.item = TMData.InspectItem.Rubber;
        //        //        bool bContains = false;
        //        //        foreach (LightCtrlSet LEDSet in TMData_Serializer._IOConfig.listLightCtrl)
        //        //        {
        //        //            if (LEDSet.camItem.cam == camItem1_Rubber.cam && LEDSet.camItem.item == camItem1_Rubber.item)
        //        //            {
        //        //                nSetLed[1] = LEDSet.nCH;
        //        //            }
        //        //        }
        //        //        if (!bContains)
        //        //        {
        //        //            StaticFun.MessageFun.ShowMessage("相机" + cam.ToString() + "无【插壳检测】检测项光源配置！");
        //        //            return;
        //        //        }
        //        //    }
        //        //    if (item == TMData.InspectItem.TM)
        //        //    {
        //        //        camItem1_TM.item = TMData.InspectItem.TM;
        //        //        bool bContains = false;
        //        //        foreach (LightCtrlSet LEDSet in TMData_Serializer._IOConfig.listLightCtrl)
        //        //        {
        //        //            if (LEDSet.camItem.cam == camItem1_TM.cam && LEDSet.camItem.item == camItem1_TM.item)
        //        //            {
        //        //                nSetLed[2] = LEDSet.nCH;
        //        //            }
        //        //        }
        //        //        if (!bContains)
        //        //        {
        //        //            StaticFun.MessageFun.ShowMessage("相机" + cam.ToString() + "无【端子检测】检测项光源配置！");
        //        //            return;
        //        //        }
        //        //    }
        //        //    if (item == TMData.InspectItem.LineColor)
        //        //    {
        //        //        camItem1_LineColor.item = TMData.InspectItem.LineColor;
        //        //        bool bContains = false;
        //        //        foreach (LightCtrlSet LEDSet in TMData_Serializer._IOConfig.listLightCtrl)
        //        //        {
        //        //            if (LEDSet.camItem.cam == camItem1_LineColor.cam && LEDSet.camItem.item == camItem1_LineColor.item)
        //        //            {
        //        //                nSetLed[3] = LEDSet.nCH;
        //        //            }
        //        //        }
        //        //        if (!bContains)
        //        //        {
        //        //            StaticFun.MessageFun.ShowMessage("相机" + cam.ToString() + "无【线序检测】检测项光源配置！");
        //        //            return;
        //        //        }
        //        //    }
        //        //    if (item == TMData.InspectItem.CoreNum)
        //        //    {
        //        //        camItem1_CoreNum.item = TMData.InspectItem.StripLen;
        //        //        bool bContains = false;
        //        //        foreach (LightCtrlSet LEDSet in TMData_Serializer._IOConfig.listLightCtrl)
        //        //        {
        //        //            if (LEDSet.camItem.cam == camItem1_CoreNum.cam && LEDSet.camItem.item == camItem1_CoreNum.item)
        //        //            {
        //        //                nSetLed[0] = LEDSet.nCH;
        //        //            }
        //        //        }
        //        //        if (!bContains)
        //        //        {
        //        //            StaticFun.MessageFun.ShowMessage("相机" + cam.ToString() + "无【线芯检测】检测项光源配置！");
        //        //            return;
        //        //        }
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageFun.ShowMessage(ex.ToString());
        //    }
        //}
    }
}
