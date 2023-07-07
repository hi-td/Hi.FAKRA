using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThridLibray;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
//using System.Diagnostics;
using System.Runtime.InteropServices;
using Chustange.Functional;
//using HostLink;

namespace CamSDK
{
    class CameraOperator
    {
        DahuaCamera m_camera;
        private VisionPlatform.Function Fun;                                      //此相机对象对应的Function
        List<IGrabbedRawData> m_frameList = new List<IGrabbedRawData>();   // 图像缓存列表 | frame data list 
        Thread renderThread = null;                                        // 显示线程 | image display thread 
        public bool m_bShowLoop = true;                                    // 线程控制变量 | thread looping flag 
        Mutex m_mutex = new Mutex();                                       // 锁，保证多线程安全 | mutex 
        int m_index;
        bool m_bTrig = true;
        public delegate int GrabDelegate();

        public CameraOperator(int i, VisionPlatform.Function fun)
        {
            m_index = i;
            m_camera = DahuaCamera.m_vecs[i];
            m_camera.SetCameraOperator(this);
            Fun = fun;
            m_stopWatch.Start();
        }


        ~CameraOperator()
        {
        }


        private void ShowThread()
        {
            while (m_bShowLoop)
            {
                if (m_frameList.Count == 0)
                {
                    Thread.Sleep(10);
                    continue;
                }

                // 图像队列取最新帧 
                // always get the latest frame in list 
                m_mutex.WaitOne();
                IGrabbedRawData frame = m_frameList.ElementAt(m_frameList.Count - 1);
                m_frameList.Clear();
                m_mutex.ReleaseMutex();

                // 主动调用回收垃圾 
                //call garbage collection 
                GC.Collect();

                // 控制显示最高帧率为25FPS 
                // control frame display rate to be 25 FPS 
                if (false == isTimeToDisplay())
                {
                    continue;
                }

                try
                {
                    int nRGB = RGBFactory.EncodeLen(frame.Width, frame.Height, false);
                    IntPtr pData = Marshal.AllocHGlobal(nRGB);
                    Marshal.Copy(frame.Image, 0, pData, frame.ImageSize);

                    //add by 2021.5.25
                    var bitmap = frame.ToBitmap(false);
                    Fun.Bitmap2HObject(bitmap, m_bTrig);

                    //Fun.GetImageFromCam(CamDeviceChannel.mono, pData, frame.Width, frame.Height);
                    //Function.GetImageFromCam(m_index, m_hWndCtrl, CamDeviceChannel.mono, pData, frame.Width, frame.Height);
                    //调用Marshal.AllocHGlobal必须调用 Marshal.FreeHGlobal(ptr);来手动释放内存，即使调用GC.Collect();方法也无法释放。
                    Marshal.FreeHGlobal(pData);
                }
                catch (Exception exception)
                {
                    (exception.Message + exception.StackTrace).ToLog();
                    Catcher.Show(exception);
                }
            }
        }

        const int DEFAULT_INTERVAL = 40;
        Stopwatch m_stopWatch = new Stopwatch();
        private bool isTimeToDisplay()
        {
            m_stopWatch.Stop();
            long m_lDisplayInterval = m_stopWatch.ElapsedMilliseconds;
            if (m_lDisplayInterval <= DEFAULT_INTERVAL)
            {
                m_stopWatch.Start();
                return false;
            }
            else
            {
                m_stopWatch.Reset();
                m_stopWatch.Start();
                return true;
            }
        }

        public DahuaCamera.EStatus Open()
        {
            return m_camera.Open();
        }

        public DahuaCamera.EStatus Close()
        {
            return m_camera.Close();
        }

        public DahuaCamera.EStatus StartGrabbing()
        {
            m_bShowLoop = true;
            if (null == renderThread)
            {
                renderThread = new Thread(new ThreadStart(ShowThread));
                renderThread.Start();
            }
            return m_camera.StartGrabbing();
        }

        public DahuaCamera.EStatus StopGrabbing()
        {
            m_bShowLoop = false;
            if (null != renderThread)
            {
                renderThread.Join();
                renderThread = null;
            }
            return m_camera.StopGrabbing();
        }

        public void OnImageGrabbed(Object sender, GrabbedEventArgs e)
        {
            Trace.WriteLine("the" + m_index + "camera blockid is " + e.GrabResult.BlockID + "\n");
            m_mutex.WaitOne();
            m_frameList.Add(e.GrabResult.Clone());
            m_mutex.ReleaseMutex();
        }
        public int SetExposureTime(double dValue)
        {
            return m_camera.setFloatAttr("ExposureTime", dValue);
        }

        public double GetExposureTime()
        {
            return m_camera.getFloatAttr("ExposureTime");
        }
        public int SetGain(double dValue)
        {
            return m_camera.setFloatAttr("GainRaw", dValue);
        }

        public double GetGain()
        {
            return m_camera.getFloatAttr("GainRaw");
        }
        public bool SetTriggerClose()
        {
            return m_camera.SetTriggerClose();
        }
        //设置触发模式
        public bool SetTriggerMode(bool bValue)
        {
            m_bTrig = bValue;
            return m_camera.SetTriggerMode(bValue);
        }

        public bool TriggerOnce()
        {
            return m_camera.TriggerOnce();
        }

    }
}
