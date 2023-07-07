using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using Chustange.Functional;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using WENYU_IO;
using static VisionPlatform.TMData;
using BaseData;
using StaticFun;
using System.Threading;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using static System.Windows.Forms.MonthCalendar;
using DAL;

namespace VisionPlatform
{
    public class TMFunction
    {
        Function Fun = null;
        public static bool isAuto = false;
        ushort[] registerBufferOK = new ushort[1] { 1 };
        ushort[] registerBufferNG = new ushort[1] { 0 };
        ushort[] registerBufferFinish = new ushort[1] { 1 };
        ushort[] registerBuffer = new ushort[1] { 0 };
        public static Dictionary<TMData.CamInspectItem, FormSTATS> m_ListFormSTATS = new Dictionary<TMData.CamInspectItem, FormSTATS>(); //计数用form窗体个数
        Dictionary<string, HTuple> dic_PicWnd = new Dictionary<string, HTuple>();
        public TMFunction(Function fun)
        {
            if (fun != null)
            {
                Fun = fun;
            }
        }
        ~TMFunction() { }

        public static string GetStrCheckItem(InspectItem item)
        {
            string str = "";
            try
            {
                if (item == InspectItem.FAKRA)
                {
                    str = "FAKRA检测";
                }
                else if (item == InspectItem.CoreNum)
                {
                    str = "线芯检测";
                }
            }
            catch (SystemException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
            }
            return str;
        }
        public void InitPicBox(Dictionary<string, PictureBox> pictureBox)
        {

            HTuple picWnd_gray = new HTuple();
            HTuple picWnd_Img1 = new HTuple(), picWnd_Img2 = new HTuple(), picWnd_Img3 = new HTuple();
            HTuple picWnd_result1 = new HTuple(), picWnd_result2 = new HTuple(), picWnd_result3 = new HTuple();
            try
            {
                HOperatorSet.GetImageSize(Fun.m_hImage, out HTuple hv_w, out HTuple hv_h);
                HOperatorSet.OpenWindow(0, 0, pictureBox["灰度图"].Width, pictureBox["灰度图"].Height, pictureBox["灰度图"].Handle, "visible", "", out picWnd_gray);
                HOperatorSet.OpenWindow(0, 0, pictureBox["Image1"].Width, pictureBox["Image1"].Height, pictureBox["Image1"].Handle, "visible", "", out picWnd_Img1);
                HOperatorSet.OpenWindow(0, 0, pictureBox["Image2"].Width, pictureBox["Image2"].Height, pictureBox["Image2"].Handle, "visible", "", out picWnd_Img2);
                HOperatorSet.OpenWindow(0, 0, pictureBox["Image3"].Width, pictureBox["Image3"].Height, pictureBox["Image3"].Handle, "visible", "", out picWnd_Img3);
                HOperatorSet.OpenWindow(0, 0, pictureBox["Result1"].Width, pictureBox["Result1"].Height, pictureBox["Result1"].Handle, "visible", "", out picWnd_result1);
                HOperatorSet.OpenWindow(0, 0, pictureBox["Result2"].Width, pictureBox["Result2"].Height, pictureBox["Result2"].Handle, "visible", "", out picWnd_result2);
                HOperatorSet.OpenWindow(0, 0, pictureBox["Result3"].Width, pictureBox["Result3"].Height, pictureBox["Result3"].Handle, "visible", "", out picWnd_result3);
                dic_PicWnd = new Dictionary<string, HTuple>();
                dic_PicWnd.Add("灰度图", picWnd_gray);
                dic_PicWnd.Add("Image1", picWnd_Img1);
                dic_PicWnd.Add("Image2", picWnd_Img2);
                dic_PicWnd.Add("Image3", picWnd_Img3);
                dic_PicWnd.Add("Result1", picWnd_result1);
                dic_PicWnd.Add("Result2", picWnd_result2);
                dic_PicWnd.Add("Result3", picWnd_result3);
            }
            catch (HalconException ex)
            {
                ex.ToString();
            }
        }

        public void PicShow(string strColorSpace)
        {
            HOperatorSet.GenEmptyObj(out HObject ho_GrayImage);
            HOperatorSet.GenEmptyObj(out HObject ho_Image1);
            HOperatorSet.GenEmptyObj(out HObject ho_Image2);
            HOperatorSet.GenEmptyObj(out HObject ho_Image3);
            HOperatorSet.GenEmptyObj(out HObject ho_result1);
            HOperatorSet.GenEmptyObj(out HObject ho_result2);
            HOperatorSet.GenEmptyObj(out HObject ho_result3);

            try
            {
                HOperatorSet.CountChannels(Fun.m_hImage, out HTuple hv_channels);
                if (3 != hv_channels.I)
                {
                    MessageFun.ShowMessage("非彩色图像。");
                    return;
                }
                ho_GrayImage.Dispose();
                HOperatorSet.Rgb1ToGray(Fun.m_hImage, out ho_GrayImage);
                ho_Image1.Dispose();
                ho_Image2.Dispose();
                ho_Image3.Dispose();
                HOperatorSet.Decompose3(Fun.m_hImage, out ho_Image1, out ho_Image2, out ho_Image3);
                HOperatorSet.DispObj(ho_GrayImage, dic_PicWnd["灰度图"]);
                HOperatorSet.DispObj(ho_Image1, dic_PicWnd["Image1"]);
                HOperatorSet.DispObj(ho_Image2, dic_PicWnd["Image2"]);
                HOperatorSet.DispObj(ho_Image3, dic_PicWnd["Image3"]);
                if ("" != strColorSpace)
                {
                    ho_result1.Dispose();
                    ho_result2.Dispose();
                    ho_result3.Dispose();
                    HOperatorSet.TransFromRgb(ho_Image1, ho_Image2, ho_Image3, out ho_result1, out ho_result2, out ho_result3, strColorSpace);
                    HOperatorSet.DispObj(ho_result1, dic_PicWnd["Result1"]);
                    HOperatorSet.DispObj(ho_result2, dic_PicWnd["Result2"]);
                    HOperatorSet.DispObj(ho_result3, dic_PicWnd["Result3"]);
                }

            }
            catch (HalconException ex)
            {
                ex.ToString();
                return;
            }
            finally
            {

            }
        }
        public static TMData.InspectItem GetEnumCheckItem(string str)
        {
            TMData.InspectItem item = new InspectItem();
            try
            {
                if (str == "FAKRA检测")
                {
                    item = InspectItem.FAKRA;
                }
                else if (str == "线芯检测")
                {
                    item = InspectItem.CoreNum;
                }
            }
            catch (SystemException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
            }
            return item;
        }

        public bool Rect2Trans(ref Rect2 rect2)
        {
            try
            {
                if (rect2.dLength1 == 0)
                {
                    return false;
                }
                if (rect2.dPhi < 0)
                {
                    rect2.dPhi = Math.PI + rect2.dPhi;
                }
                if (rect2.dPhi > (Math.PI / 4 * 3) && rect2.dPhi <= Math.PI)
                {
                    rect2.dPhi = rect2.dPhi - Math.PI;
                }
                else if (rect2.dPhi >= Math.PI / 4 && rect2.dPhi <= (Math.PI / 4 * 3))
                {
                    double dTemp = rect2.dLength1;
                    rect2.dLength1 = rect2.dLength2;
                    rect2.dLength2 = dTemp;
                    rect2.dPhi = rect2.dPhi - Math.PI / 2;
                }
                else if (rect2.dPhi <= (Math.PI / 2 + Math.PI / 12) && rect2.dPhi >= (Math.PI / 2 - Math.PI / 12))
                {
                    double dTemp = rect2.dLength1;
                    rect2.dLength1 = rect2.dLength2;
                    rect2.dLength2 = dTemp;
                    rect2.dPhi = rect2.dPhi - Math.PI / 2;
                }
                //else if (rect2.dPhi <= (Math.PI / 12) && rect2.dPhi >= (-Math.PI / 12))
                //{
                //    rect2.dPhi = 0;
                //}
                return true;
            }
            catch (SystemException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return false;
            }

        }

        public void GetLightCH(CamInspectItem camItem, out Dictionary<int, int> openLED, out Dictionary<int, int> closeLED)
        {
            openLED = new Dictionary<int, int>();
            closeLED = new Dictionary<int, int>();
            try
            {
                if (GlobalData.Config._InitConfig.initConfig.bDigitLight)
                {
                    //if (!TMData_Serializer._globalData.dicInspectList.ContainsKey(camItem.cam))
                    //{
                    //    return;
                    //}
                    if (TMData_Serializer._globalData.listLightCtrl.Count != 0)
                    {
                        for (int i = 0; i < TMData_Serializer._globalData.listLightCtrl.Count; i++)
                        {
                            LightCtrlSet orgLightSet = TMData_Serializer._globalData.listLightCtrl[i];
                            if (camItem.cam == orgLightSet.camItem.cam)
                            {
                                if (camItem.item == orgLightSet.camItem.item)
                                {
                                    //if (TMData_Serializer._globalData.dicInspectList[camItem.cam].Contains(camItem.item))
                                    //{
                                    //    if (null == orgLightSet.CH) return;
                                    //    for (int n = 0; n < orgLightSet.CH.Length; n++)
                                    //    {
                                    //        if (orgLightSet.CH[n])
                                    //        {
                                    //            openLED.Add(n + 1, orgLightSet.nBrightness[n]);
                                    //        }
                                    //        else
                                    //        {
                                    //            closeLED.Add(n + 1, orgLightSet.nBrightness[n]);
                                    //        }
                                    //    }
                                    //}
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ("获取光源通道错误：" + ex.ToString()).ToLog();
                StaticFun.MessageFun.ShowMessage("获取光源通道错误：" + ex.ToString());
            }
        }

        public void SetLED(Dictionary<int, int> openLED, Dictionary<int, int> closeLED)
        {
            try
            {
                if (!GlobalData.Config._InitConfig.initConfig.bDigitLight)
                {
                    return;
                }
                //先关闭同工位其他的检测项的光源
                foreach (int CH in closeLED.Keys)
                {
                    LEDSet.SetBrightness(CH, 0);
                }
                //再打开此工位当前检测项的光源
                foreach (int CH in openLED.Keys)
                {
                    LEDSet.SetBrightness(CH, openLED[CH]);
                }
            }
            catch (Exception ex)
            {
                ("设置光源亮度错误：" + ex.ToString()).ToLog();
                StaticFun.MessageFun.ShowMessage("设置光源亮度错误：" + ex.ToString());
            }

        }

        public void LEDOff(Dictionary<int, int> openLED)
        {
            try
            {
                if (!GlobalData.Config._InitConfig.initConfig.bDigitLight)
                {
                    return;
                }
                //先关闭同工位其他的检测项的光源
                foreach (int CH in openLED.Keys)
                {
                    LEDSet.SetBrightness(CH, 0);
                }
            }
            catch (Exception ex)
            {
                ("光源亮度设置0错误：" + ex.ToString()).ToLog();
                StaticFun.MessageFun.ShowMessage("光源亮度设置0错误：" + ex.ToString());
            }

        }

        public void GetIOPoint(CamInspectItem camItem, out int readIO, out IOSend sendIO)
        {
            readIO = -1;
            sendIO = new IOSend();
            sendIO.sendOK = -1;
            sendIO.sendNG = -1;
            try
            {
                bool bContains = false;
                foreach (IOSet ioSet in TMData_Serializer._COMConfig.listIOSet)
                {
                    if (ioSet.camItem.cam == camItem.cam && ioSet.camItem.item == camItem.item)
                    {
                        readIO = ioSet.read;
                        sendIO = ioSet.send;
                        bContains = true;
                    }
                }
                if (!bContains)
                {
                    StaticFun.MessageFun.ShowMessage("相机" + camItem.cam.ToString() + "无【" + GetStrCheckItem(camItem.item) + "】IO配置！");
                    return;
                }
            }
            catch (Exception ex)
            {
                ("获取相机" + camItem.cam.ToString() + "【" + GetStrCheckItem(camItem.item) + "】IO点位错误：" + ex.ToString()).ToLog();
                StaticFun.MessageFun.ShowMessage("获取相机" + camItem.cam.ToString() + "【" + GetStrCheckItem(camItem.item) + "】IO点位错误：" + ex.ToString());
            }
        }
        public void RunCam_IO(int cam, string strCamSer)
        {
            int bit1 = -1, bit2 = -1, bit3 = -1, bit4 = -1, bit5 = -1;
            bool BTF1 = false;
            bool BTF2 = false;
            bool BTF3 = false;
            bool BTF4 = false;
            bool BTF5 = false;
            TMData.CamInspectItem camItem1_stripLen = new TMData.CamInspectItem();
            TMData.CamInspectItem camItem1_Rubber = new TMData.CamInspectItem();
            TMData.CamInspectItem camItem1_TM = new TMData.CamInspectItem();
            TMData.CamInspectItem camItem1_LineColor = new TMData.CamInspectItem();
            TMData.CamInspectItem camItem1_CoreNum = new TMData.CamInspectItem();
            Dictionary<int, int> dicCH_open_stripLen = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_open_Rubber = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_open_TM = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_open_LineColor = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_open_CoreNum = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_close_stripLen = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_close_Rubber = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_close_TM = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_close_LineColor = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_close_CoreNum = new Dictionary<int, int>();
            object lc_write = new object();
            bool[] bCheckList = new bool[5];
            int[] readIO = new int[5];
            TMData.IOSend[] sendIO = new TMData.IOSend[5];

            try
            {
                camItem1_stripLen.cam = cam;
                camItem1_Rubber.cam = cam;
                camItem1_TM.cam = cam;
                camItem1_LineColor.cam = cam;
                camItem1_CoreNum.cam = cam;
                //if (!TMData_Serializer._globalData.dicInspectList.ContainsKey(cam))
                //{
                //    StaticFun.MessageFun.ShowMessage("未给相机" + cam.ToString() + "配置检测项！");
                //    return;
                //}
                List<TMData.InspectItem> listItems = new(); //TMData_Serializer._globalData.dicInspectList[cam];
                //LEDSet.SetLEDCH(cam, out int[] nSetLed, out Dictionary<int, List<int>> nDelLed);
                foreach (TMData.InspectItem item in listItems)
                {
                    //if (item == TMData.InspectItem.StripLen)
                    //{
                    //    bCheckList[0] = true;
                    //    camItem1_stripLen.item = TMData.InspectItem.StripLen;
                    //    GetIOPoint(camItem1_stripLen, out readIO[0], out sendIO[0]);
                    //    //光源控制
                    //    GetLightCH(camItem1_stripLen, out dicCH_open_stripLen, out dicCH_close_stripLen);
                    //}
                    //if (item == TMData.InspectItem.Rubber)
                    //{
                    //    bCheckList[1] = true;
                    //    camItem1_Rubber.item = TMData.InspectItem.Rubber;
                    //    GetIOPoint(camItem1_Rubber, out readIO[1], out sendIO[1]);
                    //    //光源控制
                    //    GetLightCH(camItem1_Rubber, out dicCH_open_Rubber, out dicCH_close_Rubber);
                    //}
                    //if (item == TMData.InspectItem.TM)
                    //{
                    //    bCheckList[2] = true;
                    //    camItem1_TM.item = TMData.InspectItem.TM;
                    //    GetIOPoint(camItem1_TM, out readIO[2], out sendIO[2]);
                    //    //光源控制
                    //    GetLightCH(camItem1_TM, out dicCH_open_TM, out dicCH_close_TM);
                    //}
                    //if (item == TMData.InspectItem.LineColor)
                    //{
                    //    bCheckList[3] = true;
                    //    camItem1_LineColor.item = TMData.InspectItem.LineColor;
                    //    GetIOPoint(camItem1_LineColor, out readIO[3], out sendIO[3]);
                    //    //光源控制
                    //    GetLightCH(camItem1_LineColor, out dicCH_open_LineColor, out dicCH_close_LineColor);
                    //}
                    if (item == TMData.InspectItem.CoreNum)
                    {
                        bCheckList[4] = true;
                        camItem1_CoreNum.item = TMData.InspectItem.CoreNum;
                        GetIOPoint(camItem1_CoreNum, out readIO[4], out sendIO[4]);
                        //光源控制
                        GetLightCH(camItem1_CoreNum, out dicCH_open_CoreNum, out dicCH_close_CoreNum);
                    }
                }
                while (isAuto)
                {
                    //lock (lc_write)
                    //{
                    //剥皮检测
                    if (bCheckList[0] && 0 == WENYU.ReadIO(readIO[0], ref bit1))
                    {
                        if (bit1 == 0 && !BTF1)
                        {
                            BTF1 = true;
                            SetLED(dicCH_open_stripLen, dicCH_close_stripLen);
                            Inspect(camItem1_stripLen, strCamSer, sendIO[0]);
                            LEDOff(dicCH_open_stripLen);
                        }
                        else if (bit1 == 1)
                        {
                            BTF1 = false;
                        }

                    }
                    //插壳检测
                    if (bCheckList[1] && 0 == WENYU.ReadIO(readIO[1], ref bit2))
                    {
                        if (bit2 == 0 && !BTF2)
                        {
                            BTF2 = true;
                            SetLED(dicCH_open_Rubber, dicCH_close_Rubber);
                            Inspect(camItem1_Rubber, strCamSer, sendIO[1]);
                            LEDOff(dicCH_open_Rubber);
                        }
                        else if (bit2 == 1)
                        {
                            BTF2 = false;
                        }
                    }
                    //打端检测
                    if (bCheckList[2] && 0 == WENYU.ReadIO(readIO[2], ref bit3))
                    {
                        if (bit3 == 0 && !BTF3)
                        {
                            BTF3 = true;
                            SetLED(dicCH_open_TM, dicCH_close_TM);
                            Inspect(camItem1_TM, strCamSer, sendIO[2]);
                            LEDOff(dicCH_open_TM);
                        }
                        else if (bit3 == 1)
                        {
                            BTF3 = false;
                        }
                    }
                    //线序检测
                    if (bCheckList[3] && 0 == WENYU.ReadIO(readIO[3], ref bit4))
                    {
                        if (bit4 == 0 && !BTF4)
                        {
                            BTF4 = true;
                            SetLED(dicCH_open_LineColor, dicCH_close_LineColor);
                            Inspect(camItem1_LineColor, strCamSer, sendIO[3]);
                            LEDOff(dicCH_open_LineColor);
                        }
                        else if (bit4 == 1)
                        {
                            BTF4 = false;
                        }
                    }
                    //芯线数量
                    if (bCheckList[4] && 0 == WENYU.ReadIO(readIO[4], ref bit5))
                    {
                        if (bit5 == 0 && !BTF5)
                        {
                            BTF5 = true;
                            SetLED(dicCH_open_CoreNum, dicCH_close_CoreNum);
                            Inspect(camItem1_CoreNum, strCamSer, sendIO[4]);
                            LEDOff(dicCH_open_CoreNum);
                        }
                        else if (bit5 == 1)
                        {
                            BTF5 = false;
                        }
                    }
                }
                Thread.Sleep(2);
                //}
            }
            catch (SystemException error)
            {
                ("[FormHome]->[Run]:/t" + error.Message + Environment.NewLine + error.StackTrace).ToLog();
            }
        }

        public void RunCam_ModbusRTU(int cam, string strCamSer)
        {
            TMData.CamInspectItem camItem1_stripLen = new TMData.CamInspectItem();
            TMData.CamInspectItem camItem1_Rubber = new TMData.CamInspectItem();
            TMData.CamInspectItem camItem1_TM = new TMData.CamInspectItem();
            TMData.CamInspectItem camItem1_LineColor = new TMData.CamInspectItem();
            TMData.CamInspectItem camItem1_CoreNum = new TMData.CamInspectItem();
            Dictionary<int, int> dicCH_open_stripLen = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_open_Rubber = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_open_TM = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_open_LineColor = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_open_CoreNum = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_close_stripLen = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_close_Rubber = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_close_TM = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_close_LineColor = new Dictionary<int, int>();
            Dictionary<int, int> dicCH_close_CoreNum = new Dictionary<int, int>();
            object lc_write = new object();
            bool[] bCheckList = new bool[5];
            try
            {
                camItem1_stripLen.cam = cam;
                camItem1_Rubber.cam = cam;
                camItem1_TM.cam = cam;
                camItem1_LineColor.cam = cam;
                camItem1_CoreNum.cam = cam;
                if (!TMData_Serializer._globalData.dicInspectList.ContainsKey(cam.ToString()))
                {
                    StaticFun.MessageFun.ShowMessage("未给相机" + cam.ToString() + "配置检测项！");
                    return;
                }
                List<TMData.InspectItem> listItems = new(); //TMData_Serializer._globalData.dicInspectList[];
                //LEDSet.SetLEDCH(cam, out int[] nSetLed, out Dictionary<int, List<int>> nDelLed);
                foreach (TMData.InspectItem item in listItems)
                {
                    //if (item == TMData.InspectItem.StripLen)
                    //{
                    //    bCheckList[0] = true;
                    //    camItem1_stripLen.item = TMData.InspectItem.StripLen;
                    //    //光源控制
                    //    GetLightCH(camItem1_stripLen, out dicCH_open_stripLen, out dicCH_close_stripLen);
                    //}
                    //if (item == TMData.InspectItem.Rubber)
                    //{
                    //    bCheckList[1] = true;
                    //    camItem1_Rubber.item = TMData.InspectItem.Rubber;
                    //    //光源控制
                    //    GetLightCH(camItem1_Rubber, out dicCH_open_Rubber, out dicCH_close_Rubber);
                    //}
                    //if (item == TMData.InspectItem.TM)
                    //{
                    //    bCheckList[2] = true;
                    //    camItem1_TM.item = TMData.InspectItem.TM;
                    //    //光源控制
                    //    GetLightCH(camItem1_TM, out dicCH_open_TM, out dicCH_close_TM);
                    //}
                    //if (item == TMData.InspectItem.LineColor)
                    //{
                    //    bCheckList[3] = true;
                    //    camItem1_LineColor.item = TMData.InspectItem.LineColor;
                    //    //光源控制
                    //    GetLightCH(camItem1_LineColor, out dicCH_open_LineColor, out dicCH_close_LineColor);
                    //}
                    if (item == TMData.InspectItem.CoreNum)
                    {
                        bCheckList[4] = true;
                        camItem1_CoreNum.item = TMData.InspectItem.CoreNum;
                        //光源控制
                        GetLightCH(camItem1_CoreNum, out dicCH_open_CoreNum, out dicCH_close_CoreNum);
                    }
                }
                ushort[] Res = new ushort[6];
                TMData.CamInspectItem DetectionItem = new CamInspectItem();
                DetectionItem.Init();
                while (isAuto)
                {
                    lock (lc_write)
                    {

                        Res = Modbus_RTU.Read_Register("03", TMData_Serializer._PlcRTU.PlcRTU.slaveAddress, 1880, 7);
                        if (Res != null && Res.Length == 7)
                        {
                            //剥皮检测
                            if (Res[0] == 1)
                            {
                                SetLED(dicCH_open_stripLen, dicCH_close_stripLen);
                                DetectionItem.cam = cam;
                                //DetectionItem.item = TMData.InspectItem.StripLen;
                                Modbus_RTU.Write_Register("06", TMData_Serializer._PlcRTU.PlcRTU.slaveAddress, 1880, registerBuffer);
                                    
                            }
                            //插壳检测
                            else if (Res[0] == 2)
                            {
                                SetLED(dicCH_open_Rubber, dicCH_close_Rubber);
                                DetectionItem.cam = cam;
                                //DetectionItem.item = TMData.InspectItem.Rubber;
                                Modbus_RTU.Write_Register("06", TMData_Serializer._PlcRTU.PlcRTU.slaveAddress, 1880, registerBuffer);
                            }
                            if(Res[6] == 1)
                            {
                                if (DetectionItem.item == camItem1_stripLen.item && DetectionItem.cam == camItem1_stripLen.cam)
                                {
                                    new Task(() => { ModbusRTUInspect(camItem1_stripLen, strCamSer, dicCH_open_stripLen); }, TaskCreationOptions.LongRunning).Start();
                                }
                                else if (DetectionItem.item == camItem1_Rubber.item && DetectionItem.cam == camItem1_Rubber.cam)
                                {
                                    new Task(() => { ModbusRTUInspect(camItem1_Rubber, strCamSer, dicCH_open_Rubber); }, TaskCreationOptions.LongRunning).Start();
                                }
                                Modbus_RTU.Write_Register("06", TMData_Serializer._PlcRTU.PlcRTU.slaveAddress, 1886, registerBuffer);
                                DetectionItem.Init();
                            }
                            //打端检测
                            if (Res[0] == 4)
                            {
                                SetLED(dicCH_open_TM, dicCH_close_TM);
                                DetectionItem.cam = cam;
                                //DetectionItem.item = TMData.InspectItem.TM;
                                Modbus_RTU.Write_Register("06", TMData_Serializer._PlcRTU.PlcRTU.slaveAddress, 1880, registerBuffer);
                            }
                            if (Res[6] == 1)
                            {
                                if (DetectionItem.item == camItem1_TM.item && DetectionItem.cam == camItem1_TM.cam)
                                {
                                    new Task(() => { ModbusRTUInspect(camItem1_TM, strCamSer, dicCH_open_TM); }, TaskCreationOptions.LongRunning).Start();
                                }
                                Modbus_RTU.Write_Register("06", TMData_Serializer._PlcRTU.PlcRTU.slaveAddress, 1886, registerBuffer);
                                DetectionItem.Init();
                            }
                        }
                        if (Res[0] == 3) //复位
                        {
                            LEDOff(dicCH_open_stripLen);
                            LEDOff(dicCH_open_Rubber);
                            LEDOff(dicCH_open_TM);
                            Modbus_RTU.Write_Register("06", TMData_Serializer._PlcRTU.PlcRTU.slaveAddress, 1880, registerBuffer);
                            
                        }
                    }
                    Thread.Sleep(2);
                }
            }
            catch (SystemException error)
            {
                ("[FormHome]->[Run]:/t" + error.Message + Environment.NewLine + error.StackTrace).ToLog();
            }
        }

        public void ModbusRTUInspect(TMData.CamInspectItem camItem, string strCamSer, Dictionary<int, int> dicCH_open)
        {
            TMData.InspectResult result = new InspectResult();
            result.outcome = new Dictionary<string, string>();
            bool bStart = true;
            try
            {
                Fun.b_image = false;
                TimeSpan ts1, ts2, ts3;
                bool bResult = false;
                string strInspectItem = "";
                int NumOK = 0;
                int NumNG = 0;
                CamSDK.CamCommon.GrabImage(strCamSer);
                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
                //StripLenResult slResult = new StripLenResult();
                //MultiResult tmResult = new MultiResult();
                RubberResult Result = new RubberResult();
                while (bStart)
                {
                    if (Fun.b_image)
                    {
                        ts1 = new TimeSpan(DateTime.Now.Ticks);
                        result.GrabTime = Math.Round((ts1.Subtract(ts).Duration().TotalSeconds) * 1000, 0);   //拍照时间
                        #region 剥皮检测
                        if (camItem.item == InspectItem.FAKRA)
                        {
                            ////Fun.LoadImageFromFile("D:\\image\\1.bmp");
                            //if (!StrippingInspect(TMData_Serializer._globalData.stripLenParam[camItem.cam - 1], false, out slResult, out bResult, out NumOK))
                            //{
                            //    NumOK = 0;
                            //    bResult = false;
                            //    MessageFun.ShowMessage("剥皮检测失败。");
                            //}
                            //ts2 = new TimeSpan(DateTime.Now.Ticks);
                            //result.InspectTime = Math.Round((ts2.Subtract(ts1).Duration().TotalSeconds) * 1000, 1);   //检测时间
                            //NumNG = (int)TMData_Serializer._globalData.stripLenParam[camItem.cam - 1].nLineNum - NumOK;
                            //strInspectItem = "剥皮检测";
                            //break;
                        }
                        #endregion

                        #region 插壳检测
                        else if (camItem.item == InspectItem.FAKRA)
                        {
                            ////Fun.LoadImageFromFile("D:\\image\\2.bmp");
                            //TMData.RubberParam rubberParam = TMData_Serializer._globalData.rubberParam[camItem.cam - 1];
                            //rubberParam.lineColor.listColorID = TMData_Serializer._globalData.listColorID;
                            //if (!RubberInsert(rubberParam, false, out Result, out bool bresult, out NumOK))
                            //{
                            //    NumOK = 0;
                            //    bresult = false;
                            //    MessageFun.ShowMessage("插壳检测失败。");
                            //}
                            //ts2 = new TimeSpan(DateTime.Now.Ticks);
                            //result.InspectTime = Math.Round((ts2.Subtract(ts1).Duration().TotalSeconds) * 1000, 1);   //检测时间
                            //NumNG = (int)TMData_Serializer._globalData.rubberParam[camItem.cam - 1].rubberLocate.nRubberNum - NumOK;
                            //strInspectItem = "插壳检测";
                            //bResult = bresult;
                            //break;
                        }
                        #endregion

                        #region 线芯检测
                        else if (camItem.item == InspectItem.CoreNum)
                        {
                            //if (1 == TMData_Serializer._globalData.dic_CoreNumParam[camItem.cam].method)
                            //{
                            //    if (!CountCoreNumNEW(TMData_Serializer._globalData.dic_CoreNumParam[camItem.cam], true, out int nCoreNum))
                            //    {
                            //        bResult = false;
                            //        MessageFun.ShowMessage("线芯检测失败。");
                            //    }
                            //    else
                            //    {
                            //        bResult = true;
                            //    }
                            //}
                            //else
                            //{
                            //    if (!CountCoreNumNEWArea(TMData_Serializer._globalData.dic_CoreNumParam[camItem.cam], true, out int nCoreNum))
                            //    {
                            //        bResult = false;
                            //        MessageFun.ShowMessage("线芯检测失败。");
                            //    }
                            //    else
                            //    {
                            //        bResult = true;
                            //    }
                            //}

                            ts2 = new TimeSpan(DateTime.Now.Ticks);
                            result.InspectTime = Math.Round((ts2.Subtract(ts1).Duration().TotalSeconds) * 1000, 0);   //检测时间
                            strInspectItem = "线芯检测";
                            break;
                        }
                        #endregion
                    }
                    ts3 = new TimeSpan(DateTime.Now.Ticks);
                    double spanTotalSeconds = ts3.Subtract(ts).Duration().TotalSeconds;
                    if (spanTotalSeconds > 0.3)
                    {
                        bResult = false;
                        Fun.WriteStringtoImage(20, 40, 20, "抓图超时！", "red");
                        //Fun.WriteStringtoImage(50, 80, (int)(Function.imageWidth / 2.1), "NG", "red");
                        break;
                    }
                    Thread.Sleep(2);
                }
                if (bResult)
                {
                    //如果产品OK,给PLC发送信号
                    Modbus_RTU.Write_Register("06", TMData_Serializer._PlcRTU.PlcRTU.slaveAddress, 1882, registerBufferOK);
                    //MessageFun.ShowMessage("1882-OK信号已给出");
                    Fun.WriteStringtoImage(30, 80, (int)(Function.imageWidth / 2.1), "OK", "green");
                    result.outcome.Add(strInspectItem, "OK");
                    //保存原始或结果OK图
                    Fun.SaveOKImage(camItem.cam, strInspectItem);
                }
                else
                {
                    //如果产品NG,给PLC发送信号
                    Modbus_RTU.Write_Register("06", TMData_Serializer._PlcRTU.PlcRTU.slaveAddress, 1882, registerBufferNG);
                    Fun.WriteStringtoImage(30, 80, (int)(Function.imageWidth / 2.1), "NG", "red");
                    result.outcome.Add(strInspectItem, "NG");
                    //保存原始NG图/结果NG图
                    Fun.SaveNGImage(camItem.cam, strInspectItem);
                }
                //为了节约显示的时间
                //if (camItem.item == InspectItem.TM)
                //{
                //    MultiTMShowResult(TMData_Serializer._globalData.dic_MultiTMParam[camItem.cam], tmResult, TMData_Serializer._globalData.dicTMCheckList[camItem.cam], out NumOK, out NumNG);
                //}
                //else if (camItem.item == InspectItem.Rubber)
                //{
                //    MultiCKShowResult(TMData_Serializer._globalData.rubberParam[camItem.cam - 1], Result);
                //}
                //else if (camItem.item == InspectItem.StripLen)
                //{
                //    MultiSLShowResult(TMData_Serializer._globalData.stripLenParam[camItem.cam - 1], slResult);
                //}
                m_ListFormSTATS[camItem].Add(NumOK, NumNG, result.InspectTime);
                LEDOff(dicCH_open);
                //FormMainUI.formShowResult.ShowResult(result);
                Modbus_RTU.Write_Register("06", TMData_Serializer._PlcRTU.PlcRTU.slaveAddress, 1884, registerBufferFinish);
                //MessageFun.ShowMessage("1884复位信号已给出");
                //MessageFun.ShowMessage("************************************");
            }
            catch (SystemException ex)
            {
                bStart = false;
                LEDOff(dicCH_open);
                MessageFun.ShowMessage(ex.ToString());
                return;
            }
        }

        /// <summary>
        /// 多线插检测函数
        /// </summary>
        /// <param name="ncam"></param>  相机
        /// <param name="strCamSer"></param> 相机ID
        /// <param name="item"></param> 检测项
        /// <param name="result"></param>检测结果
        public void Inspect(TMData.CamInspectItem camItem, string strCamSer, TMData.IOSend ioSend)
        {
            TMData.InspectResult result = new InspectResult();
            result.outcome = new Dictionary<string, string>();
            bool bStart = true;
            try
            {
                Fun.b_image = false;
                TimeSpan ts1, ts2, ts3;
                bool bResult = false;
                string strInspectItem = "";
                int NumOK = 0;
                int NumNG = 0;
                //TMData.StripLenResult slResult = new StripLenResult();
                //TMData.MultiResult tmResult = new MultiResult();
                TMData.RubberResult Result = new RubberResult();
                //若未设置信号翻转，在此会先将信号关闭
                WENYU.SendIO_Close(ioSend.sendOK);
                CamSDK.CamCommon.GrabImage(strCamSer);
                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
                while (bStart)
                {
                    if (Fun.b_image)
                    {
                        ts1 = new TimeSpan(DateTime.Now.Ticks);
                        result.GrabTime = Math.Round((ts1.Subtract(ts).Duration().TotalSeconds) * 1000, 0);   //拍照时间
                        #region 剥皮检测
                        if (camItem.item == InspectItem.FAKRA)
                        {
                        //    ////Fun.LoadImageFromFile("D:\\image\\1.bmp");
                        //    //if (!StrippingInspect(TMData_Serializer._globalData.stripLenParam[camItem.cam - 1], false, out slResult, out bResult, out NumOK))
                        //    //{
                        //    //    NumOK = 0;
                        //    //    bResult = false;
                        //    //    MessageFun.ShowMessage("剥皮检测失败。");
                        //    //}
                        //    ts2 = new TimeSpan(DateTime.Now.Ticks);
                        //    result.InspectTime = Math.Round((ts2.Subtract(ts1).Duration().TotalSeconds) * 1000, 1);   //检测时间
                        //    NumNG = (int)TMData_Serializer._globalData.stripLenParam[camItem.cam - 1].nLineNum - NumOK;
                        //    strInspectItem = "剥皮检测";
                        //    break;
                        }
                        #endregion

                        #region 线芯检测
                        else if (camItem.item == InspectItem.CoreNum)
                        {
                            //if (1 == TMData_Serializer._globalData.dic_CoreNumParam[camItem.cam].method)
                            //{
                            //    if (!CountCoreNumNEW(TMData_Serializer._globalData.dic_CoreNumParam[camItem.cam], true, out int nCoreNum))
                            //    {
                            //        bResult = false;
                            //        MessageFun.ShowMessage("线芯检测失败。");
                            //    }
                            //    else
                            //    {
                            //        bResult = true;
                            //    }
                            //}
                            //else
                            //{
                            //    if (!CountCoreNumNEWArea(TMData_Serializer._globalData.dic_CoreNumParam[camItem.cam], true, out int nCoreNum))
                            //    {
                            //        bResult = false;
                            //        MessageFun.ShowMessage("线芯检测失败。");
                            //    }
                            //    else
                            //    {
                            //        bResult = true;
                            //    }
                            //}

                            ts2 = new TimeSpan(DateTime.Now.Ticks);
                            result.InspectTime = Math.Round((ts2.Subtract(ts1).Duration().TotalSeconds) * 1000, 0);   //检测时间
                            strInspectItem = "线芯检测";
                            break;
                        }
                        #endregion
                    }
                    ts3 = new TimeSpan(DateTime.Now.Ticks);
                    double spanTotalSeconds = ts3.Subtract(ts).Duration().TotalSeconds;
                    if (spanTotalSeconds > 0.3)
                    {
                        bResult = false;
                        Fun.WriteStringtoImage(20, 40, 20, "抓图超时！", "red");
                        //Fun.WriteStringtoImage(50, 80, (int)(Function.imageWidth / 2.1), "NG", "red");
                        break;
                    }
                    Thread.Sleep(2);
                }
                if (bResult)
                {
                    //如果产品OK,给PLC发送信号
                    if (ioSend.bSendOK)
                    {
                        WENYU.SendIO(ioSend.sendOK, ioSend.bSendInvert, ioSend.nSleep);
                    }
                    Fun.WriteStringtoImage(30, 80, (int)(Function.imageWidth / 2.1), "OK", "green");
                    result.outcome.Add(strInspectItem, "OK");
                    //保存原始或结果OK图
                    Fun.SaveOKImage(camItem.cam, strInspectItem);
                }
                else
                {
                    //如果产品NG,给PLC发送信号
                    if (ioSend.bSendNG)
                    {
                        WENYU.SendIO(ioSend.sendNG, ioSend.bSendInvert, ioSend.nSleep);
                    }
                    Fun.WriteStringtoImage(30, 80, (int)(Function.imageWidth / 2.1), "NG", "red");
                    result.outcome.Add(strInspectItem, "NG");
                    //保存原始NG图/结果NG图
                    Fun.SaveNGImage(camItem.cam, strInspectItem);
                }
                m_ListFormSTATS[camItem].Add(NumOK, NumNG, result.InspectTime);
                //FormMainUI.formShowResult.ShowResult(result);
            }
            catch (SystemException ex)
            {
                bStart = false;
                MessageFun.ShowMessage(ex.ToString());
                return;
            }
        }



        #region 线芯检测 
        public bool GetSingleCoreRadius(int nThd, ref Circle circle)
        {

            HTuple hv_UsedThd = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Cloumn = new HTuple(), hv_Radius = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();

            HObject ho_Circle = null, ho_ImageReduced = null;
            HObject ho_Region = null, ho_SelectedRegions = null;

            HObject ho_SelRegTwo = null, ho_DistanceImage = null;
            HObject ho_ImageConverted = null, ho_ImageInvert = null, ho_ImageScaleMax = null;
            HObject ho_ImageGauss = null, ho_Basins = null, ho_RegionInter = null;
            HObject ho_RegionOpening = null, ho_ConnectedRegions = null;

            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelRegTwo);
            HOperatorSet.GenEmptyObj(out ho_DistanceImage);
            HOperatorSet.GenEmptyObj(out ho_ImageConverted);
            HOperatorSet.GenEmptyObj(out ho_ImageInvert);
            HOperatorSet.GenEmptyObj(out ho_ImageScaleMax);
            HOperatorSet.GenEmptyObj(out ho_ImageGauss);
            HOperatorSet.GenEmptyObj(out ho_Basins);
            HOperatorSet.GenEmptyObj(out ho_RegionInter);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            try
            {
                Fun.m_hWnd.SetDraw("margin");
                Fun.m_hWnd.SetColor("red");
                //提取芯线区域
                HOperatorSet.GetImageSize(Fun.m_GrayImage, out hv_Width, out hv_Height);
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, circle.dRow, circle.dCol, circle.dRadius);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Fun.m_GrayImage, ho_Circle, out ho_ImageReduced);
                ho_Region.Dispose();
                HOperatorSet.BinaryThreshold(ho_ImageReduced, out ho_Region, "max_separability", "light", out hv_UsedThd);
                //Fun.m_hWnd.DispObj(ho_Region);
                HOperatorSet.Union1(ho_Region, out ho_SelRegTwo);
                //Fun.m_hWnd.DispObj(ho_SelRegTwo);
                //分水岭分割
                ho_DistanceImage.Dispose();
                HOperatorSet.DistanceTransform(ho_SelRegTwo, out ho_DistanceImage, "euclidean", "true", hv_Width, hv_Height);
                ho_ImageConverted.Dispose();
                HOperatorSet.ConvertImageType(ho_DistanceImage, out ho_ImageConverted, "byte");
                ho_ImageInvert.Dispose();
                HOperatorSet.InvertImage(ho_ImageConverted, out ho_ImageInvert);
                ho_ImageScaleMax.Dispose();
                HOperatorSet.ScaleImageMax(ho_ImageInvert, out ho_ImageScaleMax);
                ho_ImageGauss.Dispose();
                HOperatorSet.GaussImage(ho_ImageScaleMax, out ho_ImageGauss, 11);
                ho_Basins.Dispose();
                HOperatorSet.WatershedsThreshold(ho_ImageGauss, out ho_Basins, 23);
                ho_RegionInter.Dispose();
                HOperatorSet.Intersection(ho_Basins, ho_SelRegTwo, out ho_RegionInter);
                ho_RegionOpening.Dispose();
                HOperatorSet.OpeningCircle(ho_RegionInter, out ho_RegionOpening, 5);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions);


                HOperatorSet.SelectShapeStd(ho_ConnectedRegions, out ho_SelectedRegions, "max_area", 70);
                //Fun.m_hWnd.DispObj(ho_SelectedRegions);
                //HOperatorSet.FillUp(ho_SelectedRegions,out ho_SelectedRegions);          
                //Fun.Clearwindow();
                //Fun.m_hWnd.DispObj(ho_SelectedRegions);
                HOperatorSet.SmallestCircle(ho_SelectedRegions, out hv_Row, out hv_Cloumn, out hv_Radius);
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_Row, hv_Cloumn, hv_Radius);
                Fun.m_hWnd.SetColor("blue");
                Fun.m_hWnd.DispObj(ho_Circle);
                Fun.m_hWnd.SetColor("red");
                circle.dRow = hv_Row.D;
                circle.dCol = hv_Cloumn.D;
                circle.dRadius = Math.Round(hv_Radius.D, 2);
                Fun.WriteStringtoImage(15, (int)(hv_Row.D), (int)(hv_Cloumn.D) - 10, circle.dRadius.ToString(), "red");
                return true;
            }
            catch (HalconException error)
            {
                MessageFun.ShowMessage("获取芯线半径出错！" + error.ToString());
                return false;
            }
            finally
            {
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_SelectedRegions.Dispose();

                ho_SelRegTwo.Dispose();
                ho_DistanceImage.Dispose();
                ho_ImageConverted.Dispose();
                ho_ImageInvert.Dispose();
                ho_ImageScaleMax.Dispose();
                ho_ImageGauss.Dispose();
                ho_Basins.Dispose();
                ho_RegionInter.Dispose();
                ho_RegionOpening.Dispose();
                ho_ConnectedRegions.Dispose();
            }
        }
        public bool trackbar_countCoreNum(TMData.CoreNumParam param, bool bShown, out int nCoreNum)
        {
            nCoreNum = 0;
            HObject ho_EdgeAmplitude = null, ho_RegionMax = null;
            HObject ho_Region = null, ho_RegionClosing = null, ho_RegionFillUp = null;
            HObject ho_ImageScaleMax = null, ho_RegionOpening = null, ho_ConnectedRegions = null;
            HObject ho_ImageScaled = null, ho_ImageReduced = null, ho_SelectedRegions = null;

            HTuple hv_AreaVal = null, hv_Width = new HTuple(), hv_Height = new HTuple();

            HOperatorSet.GenEmptyObj(out ho_EdgeAmplitude);
            HOperatorSet.GenEmptyObj(out ho_RegionMax);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_ImageScaleMax);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);

            try
            {
                if (null == Fun.m_hImage) return false;
                Fun.m_hWnd.DispObj(Fun.m_hImage);
                //if (0 == param.nRadius)
                //{
                //    MessageFun.ShowMessage("请先设定半径。");
                //    Fun.WriteStringtoImage(15, 12, 12, "请先设定半径。", "red");
                //    return false;
                //}
                //double dAreaSingle = Math.PI * Math.Pow(param.nRadius, 2); //单根芯线的面积
                //定位线芯位置
                ho_EdgeAmplitude.Dispose();
                HOperatorSet.KirschAmp(Fun.m_GrayImage, out ho_EdgeAmplitude);
                ho_ImageScaled.Dispose();
                Fun.scale_image_range(ho_EdgeAmplitude, out ho_ImageScaled, 100, 200);
                //Fun.m_hWnd.DispObj(ho_ImageScaled);
                ho_Region.Dispose();
                //HOperatorSet.BinaryThreshold(ho_EdgeAmplitude, out ho_Region, "max_separability", "light", out hv_UsedThreshold);
                HOperatorSet.Threshold(ho_ImageScaled, out ho_Region, 200, 255);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_Region, out ho_RegionClosing, 3);
                HOperatorSet.Connection(ho_RegionClosing, out ho_Region);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_Region, out ho_SelectedRegions, "area", "and", 200, 999999999);
                ho_Region.Dispose();
                HOperatorSet.Union1(ho_SelectedRegions, out ho_Region);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_Region, out ho_RegionClosing, 50);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_RegionClosing, out ho_RegionFillUp);
                HOperatorSet.Connection(ho_RegionFillUp, out ho_RegionFillUp);
                ho_RegionMax.Dispose();
                HOperatorSet.SelectShapeStd(ho_RegionFillUp, out ho_RegionMax, "max_area", 70);
                //Fun.m_hWnd.DispObj(ho_RegionMax);
                //判别是否有芯线
                HOperatorSet.RegionFeatures(ho_RegionMax, "area", out hv_AreaVal);
                HOperatorSet.GetImageSize(Fun.m_GrayImage, out hv_Width, out hv_Height);
                double dArea = hv_Width.D * hv_Height.D / 10;
                if (hv_AreaVal.D > dArea /*|| ho_RegionFillUp.CountObj() > 5*/)
                {
                    Fun.WriteStringtoImage(40, 20, 20, "NG：无线芯", "red");
                    return false;
                }

                HOperatorSet.ReduceDomain(Fun.m_GrayImage, ho_RegionMax, out ho_ImageReduced);
                //Fun.m_hWnd.DispObj(ho_ImageReduced);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, param.nThd, 255);
                Fun.m_hWnd.SetDraw("margin");
                Fun.m_hWnd.SetColor("red");
                Fun.m_hWnd.DispObj(ho_Region);
                //ho_ImageReduced.Dispose();
                //HOperatorSet.ReduceDomain(ho_EdgeAmplitude, ho_RegionMax, out ho_ImageReduced);
                ////Fun.m_hWnd.DispObj(ho_ImageReduced);
                //HOperatorSet.Threshold(ho_ImageReduced, out ho_Region2, 120, 255);
                //ho_Region.Dispose();
                //HOperatorSet.Union2(ho_Region1, ho_Region2, out ho_Region);
                //ho_RegionClosing.Dispose();
                //HOperatorSet.ClosingCircle(ho_Region, out ho_RegionClosing, 1.5);
                //HOperatorSet.Connection(ho_RegionClosing, out ho_RegionClosing);
                //ho_RegionFillUp.Dispose();
                //HOperatorSet.FillUpShape(ho_RegionClosing, out ho_RegionFillUp, "area", 1, dAreaSingle * 0.8);
                //ho_CircleOther.Dispose();
                //HOperatorSet.SelectShape(ho_RegionFillUp, out ho_CircleOther, "area", "and", 100, 999999);
                ////Fun.m_hWnd.DispObj(Fun.m_GrayImage);
                ////Fun.m_hWnd.DispObj(ho_CircleOther);
                return true;

            }
            catch (HalconException ex)
            {
                MessageFun.ShowMessage("芯线检测错误：" + ex.ToString());
                return false;
            }
            finally
            {
                ho_EdgeAmplitude.Dispose();
                ho_RegionMax.Dispose();
                ho_Region.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionFillUp.Dispose();
                ho_ImageScaleMax.Dispose();
                ho_RegionOpening.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_ImageScaled.Dispose();
                ho_ImageReduced.Dispose();
                ho_SelectedRegions.Dispose();
            }
        }
        int nErrorTimes = 0;        //线芯连续错误次数
        public bool CountCoreNumNEW(TMData.CoreNumParam param, bool bTest, out int nCoreNum)
        {
            nCoreNum = 0;
            HObject ho_Reduceimage = null, ho_EdgeAmplitude = null, ho_RegionMax = null, ho_MedianImg = null;
            HObject ho_Region = null, ho_Region1 = null, ho_Region2 = null, ho_RegionClosing = null, ho_RegionFillUp = null;
            HObject ho_RegionEdge = null, ho_ConnectedRegions10 = null, ho_RegionCore = null;
            HObject ho_SelRegSingle = null, ho_Circle = null, ho_SelRegTwo = null;
            HObject ho_RegionDiff = null;
            HObject ho_DistanceImage = null, ho_ImageConverted = null, ho_ImageInvert = null;
            HObject ho_ImageScaleMax = null, ho_ImageGauss = null, ho_Basins = null;
            HObject ho_RegionInter = null, ho_RegionOpening = null;
            HObject ho_ConnectedRegions = null, ho_Circle2 = null, ho_CircleTemp = null;
            HObject ho_ImageScaled = null, ho_ImageReduced = null;
            HObject ho_SelectedRegions = null, ho_CircleOther = null;
            HObject ho_Circles = null, ho_SelectedRegions1 = null;

            HTuple hv_AreaVal = null, hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_UsedThreshold = new HTuple(), hv_Number2 = new HTuple();
            HTuple hv_Length = new HTuple(), hv_Sgn = new HTuple();
            HTuple hv_Indices2 = new HTuple(), hv_Indices1 = new HTuple(), hv_Value = new HTuple();
            HTuple hv_Number5 = new HTuple(), hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Radius1 = new HTuple();
            HTuple hv_Row2 = new HTuple(), hv_Column2 = new HTuple(), hv_Radius2 = new HTuple();
            HTuple hv_Area = new HTuple(), hv_Row3 = new HTuple(), hv_Column3 = new HTuple();
            HTuple hv_Mean = new HTuple(), hv_Deviation = new HTuple();
            HTuple hv_Row4 = new HTuple(), hv_Column4 = new HTuple();
            HTuple hv_Radius3 = new HTuple(), hv_ReducedRow = new HTuple();
            HTuple hv_ReducedCol = new HTuple(), hv_ReducedRadius = new HTuple();
            HTuple hv_Value1 = new HTuple(), hv_Value2 = new HTuple();

            HOperatorSet.GenEmptyObj(out ho_MedianImg);
            HOperatorSet.GenEmptyObj(out ho_Reduceimage);
            HOperatorSet.GenEmptyObj(out ho_EdgeAmplitude);
            HOperatorSet.GenEmptyObj(out ho_RegionMax);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_Region2);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionEdge);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions10);
            HOperatorSet.GenEmptyObj(out ho_RegionCore);
            HOperatorSet.GenEmptyObj(out ho_SelRegSingle);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_SelRegTwo);
            HOperatorSet.GenEmptyObj(out ho_RegionDiff);
            HOperatorSet.GenEmptyObj(out ho_DistanceImage);
            HOperatorSet.GenEmptyObj(out ho_ImageConverted);
            HOperatorSet.GenEmptyObj(out ho_ImageInvert);
            HOperatorSet.GenEmptyObj(out ho_ImageScaleMax);
            HOperatorSet.GenEmptyObj(out ho_ImageGauss);
            HOperatorSet.GenEmptyObj(out ho_Basins);
            HOperatorSet.GenEmptyObj(out ho_RegionInter);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Circle2);
            HOperatorSet.GenEmptyObj(out ho_CircleTemp);
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_CircleOther);
            HOperatorSet.GenEmptyObj(out ho_Circles);

            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            try
            {
                if (null == Fun.m_hImage)
                {
                    MessageFun.ShowMessage("输入图像为空！");
                    return false;
                }
                if (0 == param.nRadius)
                {
                    MessageFun.ShowMessage("请先设定半径。");
                    Fun.WriteStringtoImage(15, 12, 12, "请先设定半径。", "red");
                    return false;
                }
                Fun.m_hWnd.SetDraw("margin");
                ho_MedianImg.Dispose();
                HOperatorSet.MedianImage(Fun.m_GrayImage, out ho_MedianImg, "circle", 3, "mirrored");
                double dAreaSingle = Math.Round(Math.PI * Math.Pow(param.nRadius, 2), 0); //单根芯线的面积
                if (0 != param.rect1.dRectRow2)
                {
                    ho_Region.Dispose();
                    HOperatorSet.GenRectangle1(out ho_Region, param.rect1.dRectRow1, param.rect1.dRectCol1, param.rect1.dRectRow2, param.rect1.dRectCol2);
                    HOperatorSet.ReduceDomain(ho_MedianImg, ho_Region, out ho_Reduceimage);
                    //Fun.DispRegion(ho_Region, "red");
                    Fun.m_hWnd.SetColor("red");
                    Fun.m_hWnd.DispObj(ho_Region);
                }
                else
                {
                    ho_Reduceimage = ho_MedianImg.Clone();
                }
                Fun.m_hWnd.SetColor("blue");
                //定位线芯位置:先确定大致范围
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_Reduceimage, out ho_Region, 200, 255);
                // Fun.m_hWnd.DispObj(Fun.m_GrayImage);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                // Fun.m_hWnd.DispObj(ho_ConnectedRegions);
                HOperatorSet.RegionFeatures(ho_ConnectedRegions, "outer_radius", out hv_Value);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, new HTuple("area").TupleConcat("outer_radius"), "and",
                                         new HTuple(100).TupleConcat(param.nRadius * 0.7), new HTuple(99999999).TupleConcat(150));
                //Fun.m_hWnd.DispObj(ho_SelectedRegions);
                ho_Region.Dispose();
                HOperatorSet.Union1(ho_SelectedRegions, out ho_Region);
                HOperatorSet.SmallestCircle(ho_Region, out HTuple hv_row, out HTuple hv_col, out HTuple hv_radius);
                ho_Region.Dispose();
                HOperatorSet.GenCircle(out ho_Region, hv_row, hv_col, hv_radius + 5);
                //Fun.m_hWnd.DispObj(ho_Region);
                //定位线芯位置:精确定位到芯线范围
                ho_EdgeAmplitude.Dispose();
                HOperatorSet.KirschAmp(ho_Reduceimage, out ho_EdgeAmplitude);
                ho_ImageScaled.Dispose();
                Fun.scale_image_range(ho_EdgeAmplitude, out ho_ImageScaled, 20, 168);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_ImageScaled, ho_Region, out ho_ImageReduced);
                ho_Region.Dispose();
                //HOperatorSet.BinaryThreshold(ho_EdgeAmplitude, out ho_Region, "max_separability", "light", out hv_UsedThreshold);
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 168, 255);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_Region, out ho_RegionClosing, 5);
                HOperatorSet.Connection(ho_RegionClosing, out ho_Region);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_Region, out ho_SelectedRegions, "area", "and", dAreaSingle * 0.2, 999999999);
                ho_Region.Dispose();
                HOperatorSet.SelectGray(ho_SelectedRegions, ho_Reduceimage, out ho_Region, "max", "and", 200, 255);
                HOperatorSet.Union1(ho_Region, out ho_Region);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_Region, out ho_RegionClosing, 38);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_RegionClosing, out ho_RegionFillUp);
                HOperatorSet.Connection(ho_RegionFillUp, out ho_RegionFillUp);
                ho_RegionMax.Dispose();
                HOperatorSet.SelectShapeStd(ho_RegionFillUp, out ho_RegionMax, "max_area", 70);
                //Fun.m_hWnd.DispObj(ho_RegionMax);
                //判别是否有芯线
                HOperatorSet.RegionFeatures(ho_RegionMax, "area", out hv_AreaVal);
                if (0 == hv_AreaVal.TupleLength())
                {
                    StaticFun.MessageFun.ShowMessage("芯线区域面积为0。");
                    return false;
                }
                HOperatorSet.GetImageSize(Fun.m_GrayImage, out hv_Width, out hv_Height);
                double dArea = hv_Width.D * hv_Height.D / 10;
                if (hv_AreaVal.D > dArea /*|| ho_RegionFillUp.CountObj() > 5*/)
                {
                    Fun.WriteStringtoImage(40, 20, 20, "无芯线：0", "red");
                    return false;
                }
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_ImageScaled, ho_RegionMax, out ho_ImageReduced);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 148, 255);
                HOperatorSet.Connection(ho_Region, out ho_Region);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_Region, out ho_SelectedRegions1, "area", "and", 200, 999999999);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegions1, out ho_RegionFillUp);
                ho_RegionDiff.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp, ho_SelectedRegions1, out ho_RegionDiff);
                ho_RegionClosing.Dispose();
                HOperatorSet.OpeningCircle(ho_RegionDiff, out ho_RegionClosing, 3);
                ho_ConnectedRegions10.Dispose();
                HOperatorSet.Connection(ho_RegionClosing, out ho_ConnectedRegions10);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions10, out ho_SelectedRegions, "area", "and", dAreaSingle * 0.1, 9999999);
                //剔除黑色背景区域
                ho_Region.Dispose();
                HOperatorSet.SelectGray(ho_SelectedRegions, ho_Reduceimage, out ho_Region, "mean", "and", 200, 255);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_Region, out ho_RegionCore, 3);
                //HOperatorSet.Union2(ho_SelectedRegions1, ho_RegionCore, out ho_RegionCore);
                //Fun.m_hWnd.DispObj(ho_ImageScaled);
                //Fun.m_hWnd.SetDraw("fill");
                //Fun.m_hWnd.DispObj(ho_RegionCore);
                //Fun.m_hWnd.SetDraw("margin");
                int nCoreRegion = ho_RegionCore.CountObj();
                int nTimes = 0;
                ho_Circles.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Circles);
                HTuple listCircleRow = new HTuple();
                HTuple listCircleCol = new HTuple();
                HTuple listRadius = new HTuple();     //用于最后误判时，显示和实际芯线数一样的圆圈数
                                                      // HOperatorSet.GenEmptyObj(out HObject ho_arrayCircles);   
                while (nCoreRegion != 0)
                {
                    double dRadiusMin = Math.Round(param.nRadius * 0.65, 0);
                    double dRadiusMax = Math.Round(param.nRadius * 1.1, 0);
                    double dAreaMin = Math.Round(dAreaSingle * 0.3, 0);
                    double dAreaMax = Math.Round(dAreaSingle * 1.25, 0);
                    //选取确定是一个的
                    ho_SelRegSingle.Dispose();
                    HOperatorSet.SelectShape(ho_RegionCore, out ho_SelRegSingle, new HTuple("outer_radius").TupleConcat("area"), "and",
                                             new HTuple(dRadiusMin).TupleConcat(dAreaMin), new HTuple(dRadiusMax).TupleConcat(dAreaMax));
                    int nNumSingle = ho_SelRegSingle.CountObj();
                    if (nNumSingle > 0)
                    {
                        HOperatorSet.SmallestCircle(ho_SelRegSingle, out hv_Row1, out hv_Column1, out hv_Radius1);
                        ho_Circle.Dispose();
                        HOperatorSet.GenCircle(out ho_Circle, hv_Row1, hv_Column1, hv_Radius1 + 3);
                        Fun.m_hWnd.DispObj(ho_Circle);
                        //Fun.DispRegion(ho_Circle, "blue");
                        HOperatorSet.Union2(ho_Circles, ho_Circle, out ho_Circles);
                        listCircleRow = listCircleRow.TupleConcat(hv_Row1);
                        listCircleCol = listCircleCol.TupleConcat(hv_Column1);
                        listRadius = listRadius.TupleConcat(hv_Radius1 + 3);
                        //去掉已经选出的区域
                        ho_RegionDiff.Dispose();
                        HOperatorSet.Difference(ho_RegionCore, ho_Circle, out ho_RegionDiff);
                        HOperatorSet.Connection(ho_RegionDiff, out ho_RegionDiff);
                        ho_RegionCore.Dispose();
                        HOperatorSet.SelectShape(ho_RegionDiff, out ho_RegionCore, "area", "and", 200, 999999);
                    }
                    nCoreNum = nCoreNum + nNumSingle;
                    //选出两个区域及以上
                    //ho_SelRegTwo.Dispose();
                    //HOperatorSet.SelectShape(ho_RegionCore, out ho_SelRegTwo, (new HTuple("area")).TupleConcat("outer_radius"), "and", (new HTuple(dAreaSingle * 1.2)).TupleConcat(param.nRadius * 1.4), (new HTuple(dAreaSingle * 2)).TupleConcat(param.nRadius * 2.3));
                    //HOperatorSet.SelectShape(ho_RegionCore, out ho_SelRegTwo, (new HTuple("area")).TupleConcat("outer_radius"), "and", (new HTuple(dAreaSingle * 0.65)).TupleConcat(param.nRadius * 1.1), (new HTuple(99999999)).TupleConcat(99999999));
                    //Fun.m_hWnd.DispObj(Fun.m_GrayImage);
                    //Fun.m_hWnd.DispObj(ho_SelRegTwo);
                    if (nTimes == 0)
                    {
                        ho_RegionDiff.Dispose();
                        HOperatorSet.Difference(ho_RegionMax, ho_Circles, out ho_RegionDiff);
                        //Fun.m_hWnd.DispObj(ho_ImageScaled);
                        //Fun.m_hWnd.SetDraw("fill");
                        //Fun.m_hWnd.DispObj(ho_RegionDiff);
                        //ho_ImageReduced.Dispose();
                        //HOperatorSet.ReduceDomain(ho_ImageScaled, ho_RegionDiff, out ho_ImageReduced);
                        ho_ImageReduced.Dispose();
                        HOperatorSet.ReduceDomain(ho_Reduceimage, ho_RegionDiff, out ho_ImageReduced);
                        ho_Region.Dispose();
                        HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, param.nThd, 255);
                        HOperatorSet.Connection(ho_Region, out ho_Region);
                        ho_SelectedRegions.Dispose();
                        HOperatorSet.SelectShape(ho_Region, out ho_SelectedRegions, "area", "and", 200, 999999999);
                        HOperatorSet.Union2(ho_SelectedRegions, ho_RegionCore, out ho_RegionCore);
                        ho_RegionFillUp.Dispose();
                        HOperatorSet.FillUpShape(ho_RegionCore, out ho_RegionFillUp, "area", 1, 50);

                        HOperatorSet.ErosionCircle(ho_RegionFillUp, out ho_RegionCore, 3.5);
                        //Fun.m_hWnd.DispObj(ho_ImageScaled);
                        //Fun.m_hWnd.SetDraw("fill");
                        //Fun.m_hWnd.DispObj(ho_RegionCore);
                        //ho_ImageReduced.Dispose();
                        //HOperatorSet.ReduceDomain(Fun.m_GrayImage, ho_RegionDiff, out ho_ImageReduced);
                        //// Fun.m_hWnd.DispObj(ho_ImageReduced);
                        //HOperatorSet.Threshold(ho_ImageReduced, out ho_RegionCore, param.nThd, 255);

                    }
                    HOperatorSet.Union1(ho_RegionCore, out ho_SelRegTwo);
                    //Fun.m_hWnd.DispObj(Fun.m_GrayImage);
                    // Fun.m_hWnd.DispObj(ho_SelRegTwo);
                    //分水岭分割
                    ho_DistanceImage.Dispose();
                    HOperatorSet.DistanceTransform(ho_SelRegTwo, out ho_DistanceImage, "euclidean", "true", hv_Width, hv_Height);
                    ho_ImageConverted.Dispose();
                    HOperatorSet.ConvertImageType(ho_DistanceImage, out ho_ImageConverted, "byte");
                    ho_ImageInvert.Dispose();
                    HOperatorSet.InvertImage(ho_ImageConverted, out ho_ImageInvert);

                    ho_ImageScaleMax.Dispose();
                    HOperatorSet.ScaleImageMax(ho_ImageInvert, out ho_ImageScaleMax);
                    //Fun.m_hWnd.DispObj(ho_ImageScaleMax);
                    //Fun.scale_image_range(ho_ImageScaleMax, out HObject ho_ImageScaleMax1, 10, 200);
                    //Fun.m_hWnd.DispObj(ho_ImageScaleMax);
                    ho_ImageGauss.Dispose();
                    HOperatorSet.GaussImage(ho_ImageScaleMax, out ho_ImageGauss, 9);
                    ho_Basins.Dispose();
                    HOperatorSet.WatershedsThreshold(ho_ImageGauss, out ho_Basins, 15);
                    ho_RegionInter.Dispose();
                    HOperatorSet.Intersection(ho_Basins, ho_SelRegTwo, out ho_RegionInter);
                    ho_RegionOpening.Dispose();
                    HOperatorSet.OpeningCircle(ho_RegionInter, out ho_RegionOpening, 5);
                    ho_ConnectedRegions.Dispose();
                    HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions);
                    ho_SelectedRegions.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, (new HTuple("area")).TupleConcat("outer_radius"), "and", (new HTuple(dAreaSingle * 0.35)).TupleConcat(param.nRadius * 0.55), (new HTuple(dAreaSingle * 1.8)).TupleConcat(param.nRadius * 1.4));
                    //Fun.m_hWnd.DispObj(ho_ImageScaleMax);
                    //Fun.m_hWnd.DispObj(ho_SelectedRegions);
                    int nNumBasin = ho_SelectedRegions.CountObj();
                    ho_Circle2.Dispose();
                    if (nNumBasin > 1)
                    {
                        HOperatorSet.SmallestCircle(ho_SelectedRegions, out hv_Row2, out hv_Column2, out hv_Radius2);
                        // HOperatorSet.GenCircle(out ho_Circle2, hv_Row2, hv_Column2, hv_Radius2);
                        HTuple hv_array = new HTuple();
                        for (int n = 0; n <= (hv_Row2.TupleLength() - 1); n++)
                        {
                            if (Math.Round(hv_Radius2[n].D, 2) < Math.Round(param.nRadius * 0.6, 2))
                            {
                                hv_array = hv_array.TupleConcat(n);
                                continue;
                            }
                            ho_CircleTemp.Dispose();
                            HOperatorSet.GenCircle(out ho_CircleTemp, hv_Row2[n], hv_Column2[n], hv_Radius2[n]);
                            //如果半径过大，则重新求外接圆
                            if (Math.Round(hv_Radius2[n].D, 0) > Math.Round(param.nRadius * 1.2, 0))
                            {
                                HOperatorSet.Intensity(ho_CircleTemp, ho_Reduceimage, out hv_Mean, out hv_Deviation);
                                ho_ImageScaled.Dispose();
                                Fun.scale_image_range(ho_Reduceimage, out ho_ImageScaled, hv_Mean * 0.5, hv_Mean);
                                ho_ImageReduced.Dispose();
                                HOperatorSet.ReduceDomain(ho_ImageScaled, ho_CircleTemp, out ho_ImageReduced);
                                ho_Region.Dispose();
                                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 220, 255);
                                ho_RegionFillUp.Dispose();
                                HOperatorSet.FillUpShape(ho_Region, out ho_RegionFillUp, "area", 1, 50);
                                ho_RegionOpening.Dispose();
                                HOperatorSet.OpeningCircle(ho_RegionFillUp, out ho_RegionOpening, 3.5);
                                ho_ConnectedRegions.Dispose();
                                HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions);
                                ho_SelectedRegions.Dispose();
                                HOperatorSet.SelectShapeStd(ho_ConnectedRegions, out ho_SelectedRegions, "max_area", 70);
                                ho_DistanceImage.Dispose();
                                HOperatorSet.DistanceTransform(ho_SelectedRegions, out ho_DistanceImage, "euclidean", "true", hv_Width, hv_Height);
                                ho_ImageConverted.Dispose();
                                HOperatorSet.ConvertImageType(ho_DistanceImage, out ho_ImageConverted, "byte");
                                ho_ImageInvert.Dispose();
                                HOperatorSet.InvertImage(ho_ImageConverted, out ho_ImageInvert);
                                ho_ImageScaleMax.Dispose();
                                HOperatorSet.ScaleImageMax(ho_ImageInvert, out ho_ImageScaleMax);
                                ho_ImageGauss.Dispose();
                                HOperatorSet.GaussImage(ho_ImageScaleMax, out ho_ImageGauss, 9);
                                ho_Basins.Dispose();
                                HOperatorSet.WatershedsThreshold(ho_ImageGauss, out ho_Basins, 15);
                                ho_RegionInter.Dispose();
                                HOperatorSet.Intersection(ho_Basins, ho_SelectedRegions, out ho_RegionInter);
                                ho_RegionOpening.Dispose();
                                HOperatorSet.OpeningCircle(ho_RegionInter, out ho_RegionOpening, 5);
                                ho_ConnectedRegions.Dispose();
                                HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions);
                                ho_SelectedRegions.Dispose();
                                HOperatorSet.SelectShapeStd(ho_ConnectedRegions, out ho_SelectedRegions, "max_area", 70);

                                HOperatorSet.SmallestCircle(ho_SelectedRegions, out hv_Row4, out hv_Column4, out hv_Radius3);
                                ho_CircleTemp.Dispose();
                                HOperatorSet.GenCircle(out ho_CircleTemp, hv_Row4, hv_Column4, hv_Radius3);
                                //Fun.m_hWnd.DispObj(ho_CircleTemp);
                                if (Math.Round(hv_Radius3.D, 0) > Math.Round(param.nRadius * 1.2, 0))
                                {
                                    hv_Row2[n] = hv_Row4;
                                    hv_Column2[n] = hv_Column4;
                                    hv_Radius2[n] = hv_Radius3;
                                }
                            }
                            //把当前圆移出集合
                            HOperatorSet.TupleRemove(hv_Row2, n, out hv_ReducedRow);
                            HOperatorSet.TupleRemove(hv_Column2, n, out hv_ReducedCol);
                            HOperatorSet.TupleRemove(hv_Radius2, n, out hv_ReducedRadius);
                            if (0 != hv_ReducedRow.TupleLength())
                            {
                                //求其他外接圆
                                ho_CircleOther.Dispose();
                                HOperatorSet.GenCircle(out ho_CircleOther, hv_ReducedRow, hv_ReducedCol, hv_ReducedRadius);
                                ho_RegionInter.Dispose();
                                //当前圆与其他圆的交集
                                HOperatorSet.Intersection(ho_CircleTemp, ho_CircleOther, out ho_RegionInter);
                                //交集面积与当前圆的面积比
                                HOperatorSet.RegionFeatures(ho_RegionInter, "area", out hv_Value1);
                                HOperatorSet.RegionFeatures(ho_CircleTemp, "area", out hv_Value2);
                                double dAreaRatio = (hv_Value1 / hv_Value2).D;
                                if (dAreaRatio > 0.45)
                                {
                                    hv_array = hv_array.TupleConcat(n);
                                }
                            }
                        }
                        HOperatorSet.TupleRemove(hv_Row2, hv_array, out hv_ReducedRow);
                        HOperatorSet.TupleRemove(hv_Column2, hv_array, out hv_ReducedCol);
                        HOperatorSet.TupleRemove(hv_Radius2, hv_array, out hv_ReducedRadius);
                        ho_Circle2.Dispose();
                        HOperatorSet.GenCircle(out ho_Circle2, hv_ReducedRow, hv_ReducedCol, hv_ReducedRadius + 2.5);
                        nNumBasin = ho_Circle2.CountObj();
                        HOperatorSet.Union2(ho_Circles, ho_Circle2, out ho_Circles);
                        listCircleRow = listCircleRow.TupleConcat(hv_ReducedRow);
                        listCircleCol = listCircleCol.TupleConcat(hv_ReducedCol);
                        listRadius = listRadius.TupleConcat(hv_ReducedRadius + 2.5);
                        // ho_arrayCircles = ho_arrayCircles.ConcatObj(ho_Circle2);
                        Fun.m_hWnd.DispObj(ho_Circle2);
                        //Fun.DispRegion(ho_Circle2, "blue");
                    }
                    else if (nNumBasin == 1)
                    {
                        HOperatorSet.SmallestCircle(ho_SelectedRegions, out hv_Row2, out hv_Column2, out hv_Radius2);
                        ho_Circle2.Dispose();
                        HOperatorSet.GenCircle(out ho_Circle2, hv_Row2, hv_Column2, hv_Radius2);
                        //nNumBasin = ho_Circle2.CountObj();
                        if (Math.Round(hv_Radius2.D, 2) > Math.Round(param.nRadius * 0.7, 2))
                        {
                            HOperatorSet.Union2(ho_Circles, ho_Circle2, out ho_Circles);
                            //ho_arrayCircles = ho_arrayCircles.ConcatObj(ho_Circle2);

                            listCircleRow = listCircleRow.TupleConcat(hv_Row2);
                            listCircleCol = listCircleCol.TupleConcat(hv_Column2);
                            listRadius = listRadius.TupleConcat(hv_Radius2);
                            Fun.m_hWnd.DispObj(ho_Circle2);
                            //Fun.DispRegion(ho_Circle2, "blue");
                        }
                        else
                        {
                            nNumBasin = 0;
                        }
                    }
                    nCoreNum = nCoreNum + nNumBasin;
                    //Fun.m_hWnd.DispObj(ho_ImageScaleMax);
                    //Fun.m_hWnd.DispObj(ho_Circles);
                    ho_Region.Dispose();
                    HOperatorSet.Union1(ho_Circles, out ho_Region);
                    HOperatorSet.DilationCircle(ho_Region, out ho_Region, 3);
                    ho_RegionDiff.Dispose();
                    HOperatorSet.Difference(ho_RegionMax, ho_Region, out ho_RegionDiff);
                    // ho_ImageScaled.Dispose();
                    // Fun.scale_image_range(ho_EdgeAmplitude, out ho_ImageScaled, 20, 120);
                    // ho_ImageReduced.Dispose();
                    // HOperatorSet.ReduceDomain(ho_ImageScaled, ho_RegionDiff, out ho_ImageReduced);
                    //// Fun.m_hWnd.DispObj(ho_ImageReduced);
                    // ho_Region1.Dispose();
                    // HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, 120, 255);
                    ho_ImageReduced.Dispose();
                    HOperatorSet.ReduceDomain(ho_Reduceimage, ho_RegionDiff, out ho_ImageReduced);
                    ho_Region2.Dispose();
                    HOperatorSet.Threshold(ho_ImageReduced, out ho_Region2, param.nThd, 255);
                    //ho_Region.Dispose();
                    //HOperatorSet.Union2(ho_Region1, ho_Region2, out ho_Region);
                    //HOperatorSet.ClosingCircle(ho_Region, out ho_Region, 3);
                    //ho_RegionFillUp.Dispose();
                    //HOperatorSet.FillUpShape(ho_Region, out ho_RegionFillUp, "area", 1, dAreaSingle * 0.65);
                    //ho_RegionClosing.Dispose();
                    //HOperatorSet.OpeningCircle(ho_RegionFillUp, out ho_RegionClosing, 7);
                    //ho_Region.Dispose();
                    //HOperatorSet.ErosionCircle(ho_RegionClosing, out ho_Region, 2);
                    ho_RegionClosing.Dispose();
                    HOperatorSet.Connection(ho_Region2, out ho_RegionClosing);
                    ho_Region.Dispose();
                    HOperatorSet.SelectShape(ho_RegionClosing, out ho_Region, "area", "and", dAreaSingle * 0.25, 99999999);
                    ho_RegionCore.Dispose();
                    HOperatorSet.SelectGray(ho_Region, ho_Reduceimage, out ho_RegionCore, "max", "and", 200, 255);
                    nCoreRegion = ho_RegionCore.CountObj();
                    //Fun.Clearwindow();
                    //Fun.m_hWnd.DispObj(Fun.m_hImage);
                    //Fun.m_hWnd.DispObj(ho_RegionCore);
                    if (0 == nCoreRegion)
                    {
                        break;
                    }
                    nTimes = nTimes + 1;
                    if (nTimes >= 3)
                    {
                        if (nCoreRegion != 0)
                        {
                            ho_ImageReduced.Dispose();
                            HOperatorSet.ReduceDomain(ho_Reduceimage, ho_RegionCore, out ho_ImageReduced);
                            ho_Region.Dispose();
                            HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, param.nThd, 255);
                            ho_ConnectedRegions.Dispose();
                            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                            //Fun.m_hWnd.DispObj(ho_ConnectedRegions);
                            //判断是否可构成一个线芯
                            ho_SelRegSingle.Dispose();
                            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelRegSingle, (new HTuple("area")).TupleConcat("outer_radius"), "and", (new HTuple(dAreaSingle * 0.25)).TupleConcat(param.nRadius * 0.5), (new HTuple(dAreaSingle * 1.5)).TupleConcat(param.nRadius * 1.5));
                            int nNumLast = ho_SelRegSingle.CountObj();
                            if (nNumLast == 0)
                            {
                                HOperatorSet.RegionFeatures(ho_ConnectedRegions, new HTuple("area").TupleConcat("outer_radius"), out hv_Area);
                                double d = hv_Area[0].D / dAreaSingle;
                                double r = hv_Area[1].D / param.nRadius;
                                nNumLast = (int)((Math.Ceiling(d) > Math.Round(r)) ? Math.Ceiling(d) : Math.Round(r));
                                //Fun.DispRegion(ho_RegionCore, "blue");
                                Fun.m_hWnd.DispObj(ho_RegionCore);
                            }
                            else if (nNumLast > 0)
                            {
                                HOperatorSet.SmallestCircle(ho_SelRegSingle, out hv_Row1, out hv_Column1, out hv_Radius1);
                                ho_Circle.Dispose();
                                HOperatorSet.GenCircle(out ho_Circle, hv_Row1, hv_Column1, hv_Radius1);
                                //ho_arrayCircles = ho_arrayCircles.ConcatObj(ho_Circle);
                                listCircleRow = listCircleRow.TupleConcat(hv_Row1);
                                listCircleCol = listCircleCol.TupleConcat(hv_Column1);
                                listRadius = listRadius.TupleConcat(hv_Radius1);
                                Fun.m_hWnd.DispObj(ho_Circle);
                                //Fun.DispRegion(ho_Circle, "blue");
                            }
                            nCoreNum = nCoreNum + nNumLast;
                        }
                        break;
                    }
                }

                if (nCoreNum >= (param.nCoreNum - param.nCoreNumLess) && nCoreNum <= (param.nCoreNum + param.nCoreNumMuch))
                {
                    nErrorTimes = 0;
                    Fun.WriteStringtoImage(20, 20, 20, "线芯数量：" + nCoreNum, "green");
                    //Fun.m_hWnd.SetColor("green");
                    Fun.WriteStringtoImage(40, 50, hv_Width.I / 2 - 40, "OK", "green");
                    Fun.m_hWnd.SetColor("red");
                    return true;
                }
                else
                {
                    if (!bTest)
                    {
                        //检测数量不对时：
                        nErrorTimes++;
                        int nError = 0;
                        //图像上显示和设定数量一致的圆圈数量
                        int nErrorNum = nCoreNum - param.nCoreNum;
                        if (nErrorNum >= 0)
                        {
                            // HOperatorSet.RegionFeatures(ho_arrayCircles, "area", out HTuple hv_arrayArea);
                            HTuple hv_AreaDiff = (listRadius - param.nRadius).TupleAbs();
                            HOperatorSet.TupleSortIndex(hv_AreaDiff, out HTuple hv_sortID);
                            HOperatorSet.TupleSelectRange(hv_sortID, 0, hv_sortID.TupleLength() - nErrorNum - 1, out HTuple hv_selID);
                            //HOperatorSet.SelectObj(ho_arrayCircles, out HObject ho_selCircle, hv_selID + 1);
                            HOperatorSet.TupleSelect(listCircleRow, hv_selID, out HTuple hv_SelRow);
                            HOperatorSet.TupleSelect(listCircleCol, hv_selID, out HTuple hv_SelCol);
                            HOperatorSet.TupleSelect(listRadius, hv_selID, out HTuple hv_SelRadius);
                            ho_SelectedRegions1.Dispose();
                            HOperatorSet.GenCircle(out ho_SelectedRegions1, hv_SelRow, hv_SelCol, hv_SelRadius);
                            Fun.m_hWnd.DispObj(Fun.m_hImage);
                            Fun.m_hWnd.DispObj(ho_SelectedRegions1);
                            //Fun.DispRegion(ho_SelectedRegions1, "blue");

                        }
                        else
                        {

                        }
                        //管理员设定的额外的允许错的根数
                        if (0 != GlobalData.Config._InitConfig.otherConfig.nErrorNum)
                        {
                            nError = GlobalData.Config._InitConfig.otherConfig.nErrorNum;
                            if (nErrorTimes > 3)
                            {
                                nErrorTimes = 0;
                                Fun.WriteStringtoImage(20, 20, 20, "线芯数量：" + nCoreNum, "red");
                                Fun.WriteStringtoImage(40, 50, hv_Width.I / 2 - 40, "NG", "red");
                                return false;
                            }
                            else
                            {
                                if (nCoreNum >= (param.nCoreNum - nError))
                                {
                                    Fun.WriteStringtoImage(20, 20, 20, "线芯数量：" + param.nCoreNum, "green");
                                    Fun.WriteStringtoImage(40, 50, hv_Width.I / 2 - 40, "OK", "green");
                                    return true;
                                }
                                else
                                {
                                    nErrorTimes = 0;
                                    Fun.WriteStringtoImage(20, 20, 20, "线芯数量：" + nCoreNum, "red");
                                    Fun.WriteStringtoImage(40, 50, hv_Width.I / 2 - 40, "NG", "red");
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            Fun.WriteStringtoImage(20, 20, 20, "线芯数量：" + nCoreNum, "red");
                            Fun.WriteStringtoImage(40, 50, hv_Width.I / 2 - 40, "NG", "red");
                            return false;
                        }
                    }
                    else
                    {
                        Fun.WriteStringtoImage(20, 20, 20, "线芯数量：" + nCoreNum, "red");
                        Fun.WriteStringtoImage(40, 50, hv_Width.I / 2 - 40, "NG", "red");
                        return false;
                    }
                }
            }
            catch (HalconException ex)
            {
                Fun.WriteStringtoImage(20, 20, 20, "芯线检测错误!", "red");
                MessageFun.ShowMessage("芯线检测错误：" + ex.ToString());
                return false;
            }
            finally
            {
                ho_MedianImg?.Dispose();
                ho_Reduceimage?.Dispose();
                ho_EdgeAmplitude?.Dispose();
                ho_RegionMax?.Dispose();
                ho_Region?.Dispose();
                ho_Region1?.Dispose();
                ho_Region2?.Dispose();
                ho_RegionClosing?.Dispose();
                ho_RegionFillUp?.Dispose();
                ho_RegionEdge?.Dispose();
                ho_ConnectedRegions10?.Dispose();
                ho_RegionCore?.Dispose();
                ho_SelRegSingle?.Dispose();
                ho_Circle?.Dispose();
                ho_SelRegTwo?.Dispose();
                ho_RegionDiff?.Dispose();
                ho_DistanceImage?.Dispose();
                ho_ImageConverted?.Dispose();
                ho_ImageInvert?.Dispose();
                ho_ImageScaleMax?.Dispose();
                ho_ImageGauss?.Dispose();
                ho_Basins?.Dispose();
                ho_RegionInter?.Dispose();
                ho_RegionOpening?.Dispose();
                ho_ConnectedRegions?.Dispose();
                ho_Circle2?.Dispose();
                ho_CircleTemp?.Dispose();
                ho_ImageScaled?.Dispose();
                ho_ImageReduced?.Dispose();
                ho_SelectedRegions?.Dispose();
                ho_CircleOther?.Dispose();
                ho_Circles?.Dispose();
                ho_SelectedRegions1?.Dispose();
            }

        }
        public bool CountCoreNumNEWArea(TMData.CoreNumParam param, bool bTest, out int nCoreNum)
        {
            nCoreNum = 0;
            HObject ho_Reduceimage = null;
            HObject ho_Region = null, ho_MedianImg = null;


            HTuple hv_AreaVal = null, hv_Row = new HTuple(), hv_Column = new HTuple(), hv_Width = new HTuple(), hv_Height = new HTuple();


            HOperatorSet.GenEmptyObj(out ho_Reduceimage);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_MedianImg);

            try
            {
                if (null == Fun.m_hImage)
                {
                    MessageFun.ShowMessage("输入图像为空！");
                    return false;
                }
                if (bTest)
                {
                    if (0 == param.nRadius)
                    {
                        MessageFun.ShowMessage("请先设定线芯标准面积。");
                        Fun.WriteStringtoImage(15, 12, 12, "请先设定线芯标准面积", "red");
                        return false;
                    }
                }
                Fun.m_hWnd.SetDraw("margin");
                Fun.scale_image_range(Fun.m_GrayImage, out ho_MedianImg, 20, 120);
                if (0 != param.rect1.dRectRow2)
                {

                    ho_Region.Dispose();
                    HOperatorSet.GenRectangle1(out ho_Region, param.rect1.dRectRow1, param.rect1.dRectCol1, param.rect1.dRectRow2, param.rect1.dRectCol2);
                    HOperatorSet.ReduceDomain(ho_MedianImg, ho_Region, out ho_Reduceimage);
                    Fun.m_hWnd.SetColor("red");
                    Fun.m_hWnd.DispObj(ho_Region);
                    //Fun.DispRegion(ho_Region, "red");
                }
                else
                {
                    ho_Reduceimage = ho_MedianImg.Clone();
                }
                HOperatorSet.GetImageSize(Fun.m_GrayImage, out hv_Width, out hv_Height);
                //定位线芯位置:先确定大致范围
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_Reduceimage, out ho_Region, param.nThd2, 255);
                HOperatorSet.AreaCenter(ho_Region, out hv_AreaVal, out hv_Row, out hv_Column);
                nCoreNum = (int)(Math.Round(hv_AreaVal.D, 0));
                if (!bTest)
                {
                    Fun.m_hWnd.SetDraw("fill");
                    Fun.m_hWnd.SetColor("blue");
                    Fun.m_hWnd.DispObj(ho_Region);
                    //Fun.DispRegion(ho_Region, "blue");

                    Fun.m_hWnd.SetDraw("margin");
                    return true;
                }
                Fun.m_hWnd.SetColor("blue");
                Fun.m_hWnd.DispObj(ho_Region);
                //Fun.DispRegion(ho_Region, "blue");
                Fun.m_hWnd.SetColor("red");
                if (nCoreNum >= (param.nCoreNumArea2 * (1 - param.nCoreNumLessArea2)) && nCoreNum <= (param.nCoreNumArea2 * (1 + param.nCoreNumMuchArea2)))
                {
                    Fun.WriteStringtoImage(20, 20, 20, "线芯面积：" + nCoreNum, "green");
                    Fun.WriteStringtoImage(40, 50, hv_Width.I / 2 - 40, "OK", "green");
                    return true;
                }
                else
                {
                    if (nCoreNum <= (param.nCoreNumArea2 * (1 - param.nCoreNumLessArea2)))
                    {
                        Fun.WriteStringtoImage(20, 20, 20, "线芯面积少：" + nCoreNum, "red");
                        Fun.WriteStringtoImage(40, 50, hv_Width.I / 2 - 40, "NG", "red");
                        return false;
                    }
                    else
                    {
                        Fun.WriteStringtoImage(20, 20, 20, "线芯面积多：" + nCoreNum, "red");
                        Fun.WriteStringtoImage(40, 50, hv_Width.I / 2 - 40, "NG", "red");
                        return false;
                    }
                }
            }
            catch (HalconException ex)
            {
                Fun.WriteStringtoImage(20, 20, 20, "芯线检测错误!", "red");
                MessageFun.ShowMessage("芯线检测错误：" + ex.ToString());
                return false;
            }
            finally
            {
                ho_Reduceimage?.Dispose();
                ho_Region?.Dispose();
                ho_MedianImg?.Dispose();
            }

        }

        #endregion
    }

}
