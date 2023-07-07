using BaseData;
using Chustange.Functional;
using EnumData;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionPlatform;

namespace CamSDK
{
    public class CamCommon
    {
        public struct CamParam
        {
            public float exposure;   //曝光
            public float gain;       //增益
            public float frame;      //帧率
        }
        public static List<string> m_listCamSer = new List<string>();        //相机名称列表，添加到comboBox控件，后续所有list的顺序与此变量保持一致
        public static List<CamParam> m_camParam = new List<CamParam>();      //相机曝光、增益、帧率等参数
        public static bool[] m_bOpen;                                        //相机是否打开
        public static bool[] m_bLive;                                        //相机是否处于实时显示状态
        public static bool[] m_bTriggerMode;                                 //相机是否处于触发状态

        public static EnumData.CamBrand m_camBrand = EnumData.CamBrand.DaHeng; //当前使用的相机品牌，默认为大恒（2022.10.14修改）


        //枚举所有相机，但并未打开实时显示
        public static void EnumCams()
        {
            try
            {
                switch(m_camBrand)
                {
                    case EnumData.CamBrand.DaHeng:
                        DaHengCam.__EnumDevice();
                        break;
                    case EnumData.CamBrand.HiKVision:
                        HikVisionCam.DeviceListAcq();
                        HikVisionCam.OpenListDevice();
                        break;
                    case EnumData.CamBrand.DaHua:
                        DahuaCamera.EnumCameras();
                        break;
                    case EnumData.CamBrand.Other:
                        break;
                    default:
                        "错误的相机品牌".ToLog();
                        break;
                }
                if (m_listCamSer.Count > 0)
                {
                    m_bOpen = new bool[m_listCamSer.Count];
                    m_bLive = new bool[m_listCamSer.Count];
                    m_bTriggerMode = new bool[m_listCamSer.Count];
                }
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
            }
        }
        //打开相机，讲相机和Function绑定
        public static void OpenCam(string strCamSer, Camimage fun)
        {
            try
            {
                int camID = GetCamID(strCamSer);
                if (-1 == camID)
                {
                    return;
                }
                if (m_bOpen[camID])       //如果该相机已经处理打开状态，则先关闭相机
                {
                    StopLive(strCamSer);
                }
                //EnumData.CamBrand camBrand = GlobalData.Config._InitConfig.initConfig.camBrand;
                switch(m_camBrand)
                {
                    case EnumData.CamBrand.DaHeng:
                        DaHengCam.OpenDevice(camID, fun);
                        if (TMData_Serializer._globalData.camParam.ContainsKey(strCamSer))
                        {
                            DaHengCam.SetShutter(camID, TMData_Serializer._globalData.camParam[strCamSer].exposure);
                        }
                        break;
                    case EnumData.CamBrand.HiKVision:
                        HikVisionCam.LiveThread(camID, fun);
                        if (TMData_Serializer._globalData.camParam.ContainsKey(strCamSer))
                        {
                            HikVisionCam.Exposure(camID, (int)TMData_Serializer._globalData.camParam[strCamSer].exposure);
                        }
                        break;
                    case EnumData.CamBrand.DaHua:
                      //DahuaCamera.OpenCam(camID, fun);
                        break;
                    case EnumData.CamBrand.Other:
                        break;
                    default:
                        StaticFun.MessageFun.ShowMessage("无法识别的相机品牌。");
                        break;
                }
                m_bOpen[camID] = true;

            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
            }
        }

        //实时显示
        public static void Live(string strCamSer)
        {
            try
            {
                int camID = GetCamID(strCamSer);
                if (-1 == camID) return;
                //EnumData.CamBrand camBrand = GlobalData.Config._InitConfig.initConfig.camBrand;
                switch (m_camBrand)
                {
                    case EnumData.CamBrand.DaHeng:
                        DaHengCam.Live(camID);
                        break;
                    case EnumData.CamBrand.HiKVision:
                        HikVisionCam.Live(camID);
                        break;
                    case EnumData.CamBrand.DaHua:
                        DahuaCamera.StartGrab(camID);
                        break;
                    case EnumData.CamBrand.Other:
                        break;
                    default:
                        StaticFun.MessageFun.ShowMessage("无法识别的相机品牌。");
                        break;
                }
                m_bLive[camID] = true;
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
            }
        }

        //停止所有相机的实时模式：改为触发模式
        public static void StopLiveAll()
        {
            try
            {
                // EnumData.CamBrand camBrand = GlobalData.Config._InitConfig.initConfig.camBrand;
                switch (m_camBrand)
                {
                    case EnumData.CamBrand.DaHeng:
                        DaHengCam.StopLiveAll();
                        break;
                    case EnumData.CamBrand.HiKVision:
                        HikVisionCam.StopLiveAll();
                        break;
                    case EnumData.CamBrand.DaHua:
                        break;
                    case EnumData.CamBrand.Other:
                        break;
                    default:
                        StaticFun.MessageFun.ShowMessage("无法识别的相机品牌。");
                        break;
                }
                m_bLive = new bool[GlobalData.Config._InitConfig.initConfig.CamNum];
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
                ex.Message.ToLog();
            }
        }


        //停止相机流
        public static void StopLive(string strCamSer)
        {
            try
            {
                int camID = GetCamID(strCamSer);
                if (-1 == camID) return;
                //EnumData.CamBrand camBrand = GlobalData.Config._InitConfig.initConfig.camBrand;
                switch (m_camBrand)
                {
                    case EnumData.CamBrand.DaHeng:
                        DaHengCam.StopDevice(camID);
                        break;
                    case EnumData.CamBrand.HiKVision:
                        HikVisionCam.StopDevice(camID);
                        break;
                    case EnumData.CamBrand.DaHua:
                        DahuaCamera.StopGrab(camID);
                        break;
                    case EnumData.CamBrand.Other:
                        break;
                    default:
                        StaticFun.MessageFun.ShowMessage("无法识别的相机品牌。");
                        break;
                }
                m_bLive[camID] = false;
                m_bOpen[camID] = false;
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
            }
        }


        //拍照:软触发
        public static void GrabImage(string strCamSer)
        {

            try
            {
                int camID = GetCamID(strCamSer);
                if (-1 == camID) return;
                //EnumData.CamBrand camBrand = GlobalData.Config._InitConfig.initConfig.camBrand;
                switch (m_camBrand)
                {
                    case EnumData.CamBrand.DaHeng:
                        DaHengCam.GrabImage(camID);
                        break;
                    case EnumData.CamBrand.HiKVision:
                        HikVisionCam.GrabImage(camID);
                        break;
                    case EnumData.CamBrand.DaHua:
                        DahuaCamera.GrabImage(camID);
                        break;
                    case EnumData.CamBrand.Other:
                        break;
                    default:
                        StaticFun.MessageFun.ShowMessage("无法识别的相机品牌。");
                        break;
                }
                m_bLive[camID] = false;
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
            }

        }
        //抓图停止
        public static void GrabImagestop(string strCamSer)
        {

            try
            {
                int camID = GetCamID(strCamSer);
                if (-1 == camID) return;
                EnumData.CamBrand camBrand = GlobalData.Config._InitConfig.initConfig.camBrand;
                if (camBrand == EnumData.CamBrand.DaHeng)
                {
                    DaHengCam.TouchImage(camID);
                }
                else if (camBrand == EnumData.CamBrand.HiKVision)
                {
                    HikVisionCam.GrabImage(camID);
                }
                else if (camBrand == EnumData.CamBrand.DaHua)
                {
                    DahuaCamera.GrabImage(camID);
                }
                else if (camBrand == EnumData.CamBrand.Other)
                {

                }
                else
                {
                    StaticFun.MessageFun.ShowMessage("无法识别的相机品牌。");
                    return;
                }
                m_bLive[camID] = false;
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
            }

        }
        /// <summary>
        /// 设置相机的触发模式
        /// </summary>
        /// <param name="strCamSer"></param> 相机的序列号
        /// <param name="bTriggerMode"></param> true:软触发，false:外触发
        public static void SetTriggerMode(string strCamSer, bool bTriggerMode)
        {
            try
            {
                int camID = GetCamID(strCamSer);
                if (-1 == camID) return;
                //EnumData.CamBrand camBrand = GlobalData.Config._InitConfig.initConfig.camBrand;
                switch(GlobalData.Config._InitConfig.initConfig.camBrand)
                {
                    case CamBrand.DaHeng:
                        m_bLive[camID] = false;
                        break;
                    case CamBrand.HiKVision:
                        HikVisionCam.SetTriggerMode(camID, bTriggerMode);
                        m_bTriggerMode[camID] = bTriggerMode;
                        m_bLive[camID] = false;
                        break;
                    case CamBrand.DaHua:
                        break;
                    case CamBrand.Other:
                        break;
                    default:
                        StaticFun.MessageFun.ShowMessage("无法识别的相机品牌。");
                        break;
                }
               
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
            }
        }

        //获取相机的曝光、增益和帧率等参数
        public static CamParam GetCamParam(string strCamSer)
        {
            CamParam camParam = new CamParam();

            try
            {
                int camID = GetCamID(strCamSer);
                if (-1 == camID) return camParam;
                //EnumData.CamBrand camBrand = GlobalData.Config._InitConfig.initConfig.camBrand;
                switch (m_camBrand)
                {
                    case EnumData.CamBrand.DaHeng:
                        camParam = DaHengCam.GetCamParam(camID);
                        break;
                    case EnumData.CamBrand.HiKVision:
                        camParam = HikVisionCam.GetCamParam(camID);
                        break;
                    case EnumData.CamBrand.DaHua:
                        camParam = DahuaCamera.GetCamParam(camID);
                        break;
                    case EnumData.CamBrand.Other:
                        break;
                    default:
                        StaticFun.MessageFun.ShowMessage("无法识别的相机品牌。");
                        break;
                }
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
            }
            return camParam;
        }

        public static void SetExposure(string strCamSer, int value)
        {
            try
            {
                int camID = GetCamID(strCamSer);
                if (-1 == camID) return;
                //EnumData.CamBrand camBrand = GlobalData.Config._InitConfig.initConfig.camBrand;
                switch (m_camBrand)
                {
                    case EnumData.CamBrand.DaHeng:
                        DaHengCam.SetShutter(camID, value);
                        break;
                    case EnumData.CamBrand.HiKVision:
                        HikVisionCam.Exposure(camID, value);
                        break;
                    case EnumData.CamBrand.DaHua:
                        DahuaCamera.SetExposure(camID, value);
                        break;
                    case EnumData.CamBrand.Other:
                        break;
                    default:
                        StaticFun.MessageFun.ShowMessage("无法识别的相机品牌。");
                        break;
                }
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
            }
        }

        public static void SetGain(string strCamSer, int value)
        {
            try
            {
                int camID = GetCamID(strCamSer);
                if (-1 == camID) return;
                // EnumData.CamBrand camBrand = GlobalData.Config._InitConfig.initConfig.camBrand;
                switch (m_camBrand)
                {
                    case EnumData.CamBrand.DaHeng:
                        DaHengCam.SetGain(camID, value);
                        break;
                    case EnumData.CamBrand.HiKVision:
                        HikVisionCam.Gain(camID, value);
                        break;
                    case EnumData.CamBrand.DaHua:
                        DahuaCamera.SetGain(camID, value);
                        break;
                    case EnumData.CamBrand.Other:
                        break;
                    default:
                        StaticFun.MessageFun.ShowMessage("无法识别的相机品牌。");
                        break;
                }
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
            }
        }

        public static void CloseAllCam()
        {
            try
            {
                // EnumData.CamBrand camBrand = GlobalData.Config._InitConfig.initConfig.camBrand;
                switch (m_camBrand)
                {
                    case EnumData.CamBrand.DaHeng:
                        DaHengCam.__CloseAll();
                        break;
                    case EnumData.CamBrand.HiKVision:
                        HikVisionCam.DestroyDev();
                        break;
                    case EnumData.CamBrand.DaHua:
                        DahuaCamera.CloseCam();
                        break;
                    case EnumData.CamBrand.Other:
                        break;
                    default:
                        StaticFun.MessageFun.ShowMessage("无法识别的相机品牌。");
                        break;
                }
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
            }
        }

        public static void ReConnect()
        {
            try
            {
                CloseAllCam();
                EnumCams();
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
            }
        }

        private static int GetCamID(string strCamSer)
        {
            int camID = -1;
            try
            {
                foreach (string str in m_listCamSer)
                {
                    camID++;
                    if (str == strCamSer)
                    {
                        return camID;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.Message);
                return -1;
            }
            
        }


    }
}
