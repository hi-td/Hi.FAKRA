using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThridLibray;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Chustange.Functional;
using VisionPlatform;
//using HostLink;

namespace CamSDK
{
    class DaHuaCam
    {

        public static List<string> m_strSeriNum = new List<string>(); //相机序列号
        public static List<CameraOperator> m_cameraVec = new List<CameraOperator>(); //相机方法
       // public static CameraOperator[] m_cameraVec = new CameraOperator[2]; //相机方法
        public static List<bool> m_bLive = new List<bool>();
        //public static bool[] m_bLive; //是否实时显示

        public static void deviceListAcquire()
        {
            DahuaCamera.EnumCameras();
            m_strSeriNum.Clear();
            for (int i = 0; i < DahuaCamera.m_vecs.Count; i++)
            {
                /*获取一个相机对象*/
                IDevice dev = Enumerator.GetDeviceByIndex(i);

                if (dev.DeviceInfo.DeviceType == 0)
                {
                    StaticFun.MessageFun.ShowMessage("GigE: " + dev.DeviceInfo.ManufactureInfo + " " + dev.DeviceInfo.Model + " (" + dev.DeviceInfo.SerialNumber + ")");
                }
                else if (dev.DeviceInfo.DeviceType == 1)
                {
                    StaticFun.MessageFun.ShowMessage("USB: " + dev.DeviceInfo.ManufactureInfo + " " + dev.DeviceInfo.Model + " (" + dev.DeviceInfo.SerialNumber + ")");

                }
                //添加相机的序列号
                m_strSeriNum.Add(dev.DeviceInfo.SerialNumber);
                //m_cameraVec[i] = new CameraOperator(i, m_hWndCtrl[i]);
            }
            //初始化实时显示状态
            m_bLive.Clear();
            for (int n = 0; n < m_strSeriNum.Count; n++)
            {
                m_bLive.Add(false);
            }
            if (m_strSeriNum.Count == 0)
            {
                StaticFun.MessageFun.ShowMessage(DateTime.Now + " 没有搜索到相机设备，请检查相机设备是否安全连接！");
            }
        }
        //打开相机
        public static void OpenCam()
        {
            for (int i = 0; i < DahuaCamera.m_vecs.Count; i++)
            {

                if (DahuaCamera.EStatus.eStatusOK != m_cameraVec[i].Open())
                {
                    StaticFun.MessageFun.ShowMessage(DateTime.Now + " Camera " + m_strSeriNum[i] + " Open fail");
                }
                else
                {
                    StaticFun.MessageFun.ShowMessage(DateTime.Now + " Camera " + m_strSeriNum[i] + " Open success");
                }
            }
        }
        //关闭相机
        public static void CloseCam()
        {
            for (int i = 0; i < DahuaCamera.m_vecs.Count; i++)
            {
                if (DahuaCamera.EStatus.eStatusOK != m_cameraVec[i].Close())
                {
                    StaticFun.MessageFun.ShowMessage(DateTime.Now + " Camera " + m_strSeriNum[i] + " Close fail");
                }
                else
                {
                    StaticFun.MessageFun.ShowMessage(DateTime.Now + " Camera " + m_strSeriNum[i] + " Close success");
                }
            }
        }
        //开启抓图
        public static void StartGrab(int i)
        {
            if (i == -1)
            {
                StaticFun.MessageFun.ShowMessage(DateTime.Now + " 错误信息：相机异常，请检查相机是否安全连接！");
                return;
            }
            if (null == m_cameraVec[i]) return;
            m_cameraVec[i].SetTriggerClose();//关闭触发模式
            if (DahuaCamera.EStatus.eStatusOK != m_cameraVec[i].StartGrabbing())
            {
                StaticFun.MessageFun.ShowMessage(DateTime.Now + " Camera " + m_strSeriNum[i] + " StartGrabbing fail");
            }
            else
            {
                StaticFun.MessageFun.ShowMessage(DateTime.Now + " Camera " + m_strSeriNum[i] + " StartGrabbing success");
            }
            m_bLive[i] = true;
        }
        //停止抓图
        public static void StopGrab(int i)
        {
            if (i == -1)
            {
                StaticFun.MessageFun.ShowMessage(DateTime.Now + " 错误信息：相机异常，请检查相机是否安全连接！");
                return;
            }
            if (null == m_cameraVec[i]) return;
            if (DahuaCamera.EStatus.eStatusOK != m_cameraVec[i].StopGrabbing())
            {
                StaticFun.MessageFun.ShowMessage(DateTime.Now + " Camera " + m_strSeriNum[i] + " StopGrabbing fail");
            }
            else
            {
                StaticFun.MessageFun.ShowMessage(DateTime.Now + " Camera " + m_strSeriNum[i] + " StopGrabbing success");
            }
            m_bLive[i] = false;
        }
        //外部触发
        public static bool ExternalGabImg(int i)
        {
            try
            {
                if (i == -1)
                {
                    StaticFun.MessageFun.ShowMessage(DateTime.Now + " 错误信息：相机异常，请检查相机是否安全连接！");
                    return false;
                }
                if (!m_cameraVec[i].m_bShowLoop)
                    m_cameraVec[i].StartGrabbing();
                m_cameraVec[i].SetTriggerMode(false);
                return true;
            }
            catch (Exception ex)
            {
                (ex.Message + ex.StackTrace).ToLog();
                return false;
            }
        }
        //// 执行软触发 
        //// execute software trigger once 
        public static bool GrabImage(int i)
        {
            try
            {
                if (i == -1)
                {
                    StaticFun.MessageFun.ShowMessage(DateTime.Now + " 错误信息：相机异常，请检查相机是否安全连接！");
                    return false;
                }
                if (m_cameraVec[i] == null)
                    return false;
                if (!m_cameraVec[i].m_bShowLoop)
                    m_cameraVec[i].StartGrabbing();
                m_cameraVec[i].TriggerOnce();
                return true;
            }
            catch (Exception ex)
            {
                (ex.Message + ex.StackTrace).ToLog();
                return false;
            }
        }
    }
}
