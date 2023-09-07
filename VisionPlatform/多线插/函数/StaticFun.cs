using Chustange.Functional;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using VisionPlatform;
using System.IO;
using BaseData;
using HalconDotNet;
using static VisionPlatform.TMData;
using WENYU_IO;
using System.Drawing;
using CamSDK;
using DAL;
using EnumData;
using VisionPlatform.Properties;

namespace StaticFun
{
    public class Fun
    {
        public static bool isRunning;  //监听程序是否处于生产状态

        public static void ReadDisk()
        {
            try
            {
                string AppPath = Application.StartupPath.ToString();
                string volume = AppPath.Substring(0, AppPath.IndexOf(':'));
                long totalSize = 0;
                volume = volume + ":\\";
                System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
                foreach (System.IO.DriveInfo drive in drives)
                {
                    if (drive.Name == volume)
                    {
                        totalSize = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                    }
                }
                if (totalSize < 2)
                {
                    MessageBox.Show("磁盘空间不足2G，请及时清理图片！");
                }
            }
            catch (Exception ex)
            {
                ex.ToString().ToLog();
            }
        }
        public static long GetHardDiskSpace(string str_HardDiskName)
        {
            str_HardDiskName = "";
            long totalSize = 0;
            try
            {
                str_HardDiskName = str_HardDiskName + ":\\";
                System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
                foreach (System.IO.DriveInfo drive in drives)
                {
                    if (drive.Name == str_HardDiskName)
                    {
                        totalSize = drive.TotalFreeSpace;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString().ToLog();
            }
            return totalSize;
        }
    }

    public class Run
    {
        public static void LoadRun(System.Windows.Forms.Button but_Run)
        {
            try
            {
                StaticFun.Run.Zoom();
                if (but_Run.Text == "运行")
                {
                    but_Run.Image = Resources.runing;

                    #region test
                    if (StaticFun.Run.Start())
                    {
                        FormMainUI.bRun = true;
                        TMFunction.isAuto = true;
                        but_Run.Text = "运行中";
                        but_Run.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        FormMainUI.bRun = false;
                        TMFunction.isAuto = false;
                    }
                    #endregion
                    #region 原
                    //if (CamSDK.CamCommon.m_listCamSer.Count != 0)
                    //{
                    //    CamSDK.CamCommon.StopLiveAll();   //防止抓拍出来的图片不对
                    //    if (StaticFun.Run.Start())
                    //    {
                    //        FormMainUI.bRun = true;
                    //        TMFunction.isAuto = true;
                    //        but_Run.Text = "运行中";
                    //        but_Run.BackColor = Color.LightGreen;
                    //    }
                    //    else
                    //    {
                    //        FormMainUI.bRun = false;
                    //        TMFunction.isAuto = false;
                    //    }
                    //}
                    //else
                    //{
                    //    FormMainUI.bRun = false;
                    //    MessageBox.Show("相机连接异常！", "提示", MessageBoxButtons.OK);
                    //}
                    #endregion


                }
                else
                {
                    TMFunction.isAuto = false;
                    FormMainUI.bRun = false;
                    Thread.Sleep(3);
                    but_Run.Image = Resources.stop;
                    but_Run.Text = "运行";
                    but_Run.BackColor = default;
                }
            }
            catch (SystemException error)
            {
                MessageFun.ShowMessage("错误：" + error.ToString());
                return;
            }
        }
        public static bool Start()
        {
            try
            {
                //检测光源控制器是否链接正常
                if (GlobalData.Config._InitConfig.initConfig.bDigitLight)
                {
                    if (!LEDSet.isOpen)
                    {
                        int try_connect = 0;
                        while (!LEDSet.isOpen && try_connect < 3)
                        {
                            LEDSet.OpenLedcom(TMData_Serializer._COMConfig.Led);
                            try_connect++;
                            Thread.Sleep(20);
                        }
                    }
                    if (!LEDSet.isOpen)
                    {
                        MessageBox.Show("光源控制器链接异常，请检查光源控制器。");
                        return false;
                    }
                }
                //检测IO通讯是否正常
                if (GlobalData.Config._InitConfig.initConfig.comMode.TYPE == EnumData.COMType.IO)
                {

                    #region 原
                    //int try_connect = 0;
                    //while (!WENYU.isOpen && try_connect < 3)
                    //{
                    //    WENYU.OpenIO();
                    //    try_connect++;
                    //    Thread.Sleep(20);
                    //}
                    //if (!WENYU.isOpen)
                    //{
                    //    MessageBox.Show("板卡通讯异常！", "提示", MessageBoxButtons.OK);
                    //    return false;
                    //}
                    #endregion

                    TMFunction.isAuto = true;
                    foreach(int cam in FormMainUI.m_dicFormCamShows.Keys)
                    {
                        ShowItems[] arrayShows = FormMainUI.m_dicFormCamShows[cam];
                        for (int sub_cam = 0; sub_cam < arrayShows.Length - 1; sub_cam++)
                        {
                            TMFunction TMFun = arrayShows[sub_cam].form.TM_fun;
                            String strCamSer = arrayShows[sub_cam].form.m_strCamSer;
                            new Task(() => { TMFun.RunCam_IO(cam, sub_cam, strCamSer); }, TaskCreationOptions.LongRunning).Start();
                            Thread.Sleep(20);
                        }
                    }
                    
                }
                else if (GlobalData.Config._InitConfig.initConfig.comMode.TYPE == EnumData.COMType.NET)
                {
                }
                else if (GlobalData.Config._InitConfig.initConfig.comMode.TYPE == EnumData.COMType.COM)
                {
                    if (!Modbus_RTU.isOpen)
                    {
                        int try_connect = 0;
                        while (!Modbus_RTU.isOpen && try_connect < 3)
                        {
                            Modbus_RTU.OpenCom(TMData_Serializer._PlcRTU.PlcRTU);
                            try_connect++;
                            Thread.Sleep(20);
                        }
                    }
                    if (!Modbus_RTU.isOpen)
                    {
                        MessageBox.Show("PLC-ModbusRTU连接异常，请检查！！！", "提示", MessageBoxButtons.OK);
                        return false;
                    }
                    TMFunction.isAuto = true;
                    foreach (int cam in FormMainUI.m_dicFormCamShows.Keys)
                    {
                        ShowItems[] arrayShows = FormMainUI.m_dicFormCamShows[cam];
                        for (int i = 0; i < arrayShows.Length; i++)
                        {
                            TMFunction TMFun = arrayShows[i].form.TM_fun;
                            String strCamSer = arrayShows[i].form.m_strCamSer;
                            new Task(() => { TMFun.RunCam_ModbusRTU((i + 1), strCamSer); }, TaskCreationOptions.LongRunning).Start();
                            Thread.Sleep(20);
                        }
                    }
                }
                Thread.Sleep(10);
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
        public static void Zoom()
        {
            try
            {
                foreach(int cam in FormMainUI.m_dicFormCamShows.Keys)
                {
                    TMData.ShowItems[] arrayShows = FormMainUI.m_dicFormCamShows[cam];
                    for (int i = 0; i < arrayShows.Length; i++)
                    {
                        if (null != arrayShows[i].form.fun)
                        {
                            Function fun = arrayShows[i].form.fun;
                            fun.dReslutRow0 = 0;
                            fun.dReslutCol0 = 0;
                            fun.dReslutRow1 = 0;
                            fun.dReslutCol1 = 0;
                            fun.FitImageToWindow(ref fun.dReslutRow0, ref fun.dReslutCol0, ref fun.dReslutRow1, ref fun.dReslutCol1);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
    public class MessageFun
    {
        public static RichTextBox m_richTextBox = null;
        #region 初始化消息列表
        public static void GetRichText(RichTextBox richTextBox)
        {
            m_richTextBox = richTextBox;
        }
        //在richTextBox显示字符串消息
        public static void ShowMessage(string str)
        {
            try
            {
                if (null == m_richTextBox) return;
                string strDate = DateTime.Now.ToString() + "：";
                // m_richTextBox.SelectionColor = System.Drawing.Color.DimGray;  //可能引起跨线程调用
                if (null != m_richTextBox)
                    Append(m_richTextBox, strDate);

                //m_richTextBox.SelectionColor = System.Drawing.Color.Black;
                string mes = str + "\r\n";
                if (null != m_richTextBox)
                    Append(m_richTextBox, mes);
                Thread.Sleep(1);
            }
            catch (Exception error)
            {
                ("ShowMessage:" + error.Message).ToLog();
                return;
            }

        }
        static int q = 0; //当显示消息次数达到200次时，清空消息列表
        private static void Append(RichTextBox richTextBox, string txt)
        {
            try
            {
                q++;
                if (richTextBox.InvokeRequired)
                {
                    richTextBox.BeginInvoke(new Action(() =>
                    {
                        richTextBox.AppendText(txt);
                        richTextBox.ScrollToCaret();
                        if (q > 200)
                        {
                            richTextBox.Clear();
                            q = 0;
                        }
                    }));
                }
                else
                {
                    richTextBox.AppendText(txt);
                    richTextBox.ScrollToCaret();
                    if (q > 200)
                    {
                        richTextBox.Clear();
                        q = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ("Append" + ex.Message + ex.StackTrace).ToLog();
                return;
            }
        }
        #endregion
    }

    public class UIConfig
    {
        public static void CreateFormTeachMaster(int n_cam, int sub_cam)
        {
            FormTeachMaster teachmaster = new FormTeachMaster(n_cam, sub_cam);
            teachmaster.TopLevel = false;
            teachmaster.Visible = true;
            teachmaster.Dock = DockStyle.Fill;
            FormMainUI.m_PanelShow.Controls.Clear();
            FormMainUI.m_PanelShow.Controls.Add(teachmaster);
        }

        public static Dictionary<int, TMData.ShowItems[]> InitShowUI(ContextMenuStrip contextMenuStrip,TableLayoutPanel tableLayoutPanel)
        {
            Dictionary<int, TMData.ShowItems[]> dic_formCamShows = new Dictionary<int, ShowItems[]>();
            try
            {
                FormCamShow formCamShow;
                TMData.ShowItems[] showItems;
                foreach (int cam in GlobalData.Config._InitConfig.initConfig.dic_SubCam.Keys)
                {
                    string strCamSer = "";
                    if (GlobalData.Config._CamConfig.camConfig.ContainsKey(cam))
                    {
                        strCamSer = GlobalData.Config._CamConfig.camConfig[cam];
                    }
                    int num = GlobalData.Config._InitConfig.initConfig.dic_SubCam[cam];
                    formCamShow = new FormCamShow(strCamSer, cam, 0);
                    formCamShow.TopLevel = false;
                    formCamShow.Visible = true;
                    formCamShow.Dock = DockStyle.Fill;
                    if (0 != num)
                    {
                        showItems = new TMData.ShowItems[num + 1];
                        showItems[0].form = formCamShow;
                        for (int i = 0; i < num; i++)
                        {
                            formCamShow = new FormCamShow(strCamSer, cam, i + 1);
                            formCamShow.TopLevel = false;
                            formCamShow.Visible = true;
                            formCamShow.Dock = DockStyle.Fill;
                            showItems[i + 1].form = formCamShow;
                        }
                        dic_formCamShows.Add(cam, showItems);
                    }
                    else
                    {
                        dic_formCamShows.Add(cam, new TMData.ShowItems[1] { new TMData.ShowItems() { form = formCamShow } });
                    }
                }
                contextMenuStrip.Items.Clear();
                foreach (int cam in GlobalData.Config._InitConfig.initConfig.dic_SubCam.Keys)
                {
                    string strCam = "相机" + cam.ToString();
                    contextMenuStrip.Items.Add(strCam);
                    contextMenuStrip.Items[contextMenuStrip.Items.Count - 1].Image = Resources.camera2;
                    int num = GlobalData.Config._InitConfig.initConfig.dic_SubCam[cam];
                    for (int i = 0; i < num; i++)
                    {
                        strCam = "相机" + cam.ToString() + "-" + (i + 1).ToString();
                        contextMenuStrip.Items.Add(strCam);
                        contextMenuStrip.Items[contextMenuStrip.Items.Count - 1].Image = Resources.subcam;
                    }
                }
                //若已经配置，则加载已配置好的相机画面
                RefeshCamShow(tableLayoutPanel, dic_formCamShows);
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.ToString());
            }
            return dic_formCamShows;
        }

        public static void RefeshCamShow(TableLayoutPanel tableLayoutPanel, Dictionary<int, TMData.ShowItems[]> dic_formCamShows)
        {
            //若已经配置，则加载已配置好的相机画面
            foreach(int panelSer in TMData_Serializer._globalData.dicCamShowParam.Keys)
            {
                CamShowParam camshow = TMData_Serializer._globalData.dicCamShowParam[panelSer];
                foreach (Control panel in tableLayoutPanel.Controls)
                {
                    if (panel.Name == "panel" + panelSer.ToString())
                    {
                        panel.Controls.Clear();
                        panel.Controls.Add(dic_formCamShows[camshow.cam][camshow.sub_cam].form);
                    }
                }
            }
        }
        public static CamShowParam ConfigShowItems(object sender, ToolStripItemClickedEventArgs e, ref Dictionary<int, TMData.ShowItems[]> dic_formCamShows)
        {
            CamShowParam camshow = new CamShowParam();
            try
            {
                string strItem = e.ClickedItem.Text;
                int ncam = int.Parse(strItem.Substring(2, 1));
                int sub_cam = 0;
                if (strItem.Length > 3)
                {
                    sub_cam = int.Parse(strItem.Substring(4, 1));
                }
                if (sender is ContextMenuStrip cms)
                {
                    var panel = (Panel)cms.SourceControl;
                    dic_formCamShows[ncam][sub_cam].panel = panel;
                    panel.Controls.Clear();
                    panel.Controls.Add(dic_formCamShows[ncam][sub_cam].form);
                    camshow.cam = ncam;
                    camshow.sub_cam = sub_cam;
                    int ser = int.Parse(panel.Name.Substring(5, 1));
                    if(TMData_Serializer._globalData.dicCamShowParam.ContainsKey(ser))
                    {
                        TMData_Serializer._globalData.dicCamShowParam[ser]= camshow;
                    }
                    else
                    {
                        TMData_Serializer._globalData.dicCamShowParam.Add(ser, camshow);
                    }
                    
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            return camshow;
        }
        public static void RefreshFun(int m_ncam, int sub_cam, ref Function Fun, ref TMFunction TMFun, ref string str_CamSer)
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
                    TMFun = Show1.formCamShow1.TM_fun;
                    Fun = Show1.formCamShow1.fun;
                    str_CamSer = Show1.formCamShow1.m_strCamSer;
                    break;
                default:
                    TMFun = FormMainUI.m_dicFormCamShows[m_ncam][sub_cam].form.TM_fun;
                    Fun = FormMainUI.m_dicFormCamShows[m_ncam][sub_cam].form.fun;
                    str_CamSer = FormMainUI.m_dicFormCamShows[m_ncam][sub_cam].form.m_strCamSer;
                    break;
            }

        }
        /// <summary>
        /// 刷新界面的数据统计界面
        /// </summary>
        /// <param name="tLPanel"></param> 数据统计form依靠的控件
        /// <param name="dicFormSTATS"></param> 返回数据统计forms
        /// <param name="nRows"></param>  控件平均nrows行,if nRows=1(默认值)，否则 nRows=2,将nCols/2向上取整
        public static void RefreshSTATS(TableLayoutPanel tLPanel, out Dictionary<CamInspectItem, FormSTATS> dicFormSTATS, int nRows = 1)
        {
            dicFormSTATS = new Dictionary<CamInspectItem, FormSTATS>();   //计数用form窗体个数
            int nCheckItem = 0;
            double width = 0;
            try
            {
                if (null != TMData_Serializer._globalData.dicInspectList && TMData_Serializer._globalData.dicInspectList.Count != 0)
                {
                    dicFormSTATS.Clear();
                    tLPanel.Controls.Clear();
                    //foreach (int cam in TMData_Serializer._globalData.dicInspectList.Keys)
                    //{
                    //    if (cam > GlobalData.Config._InitConfig.initConfig.CamNum)
                    //    {
                    //        break;
                    //    }
                    //    nCheckItem = nCheckItem + TMData_Serializer._globalData.dicInspectList[cam].Count;
                    //}
                    if (1 == nRows)
                    {
                        tLPanel.ColumnCount = nCheckItem;
                        width = 100.0 / tLPanel.ColumnCount;
                    }
                    else //
                    {
                        //默认nRows == 2
                        tLPanel.ColumnCount = (int)Math.Ceiling(nCheckItem / 2d);
                        tLPanel.RowCount = 2;
                    }
                    width = 100.0 / tLPanel.ColumnCount;
                    tLPanel.RowStyles.Clear();
                    tLPanel.ColumnStyles.Clear();
                    List<TMData.CamInspectItem> list_camItem = new List<TMData.CamInspectItem>();
                    FormSTATS form = new FormSTATS(1, strCheckItem: "");
                    //foreach (int cam in TMData_Serializer._globalData.dicInspectList.Keys)
                    //{
                    //    CamInspectItem camItem = new CamInspectItem();
                    //    camItem.cam = cam;
                    //    List<InspectItem> listItem = TMData_Serializer._globalData.dicInspectList[cam];
                    //    foreach (InspectItem item in listItem)
                    //    {
                    //        camItem.item = item;
                    //        list_camItem.Add(camItem);
                    //    }
                    //}
                    int n = 0;
                    for (int row = 0; row < nRows; row++)
                    {
                        tLPanel.RowStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                        for (int col = 0; col < tLPanel.ColumnCount; col++)
                        {
                            tLPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)width));
                            int cam = list_camItem[n].cam;
                            InspectItem item = list_camItem[n].item;
                            if (list_camItem[n].item == InspectItem.StripLen)
                            {
                                form = new FormSTATS(list_camItem[n].cam, "剥皮");
                            }
                            if (list_camItem[n].item == InspectItem.TM)
                            {
                                form = new FormSTATS(list_camItem[n].cam, "端子");
                            }
                            form.TopLevel = false;
                            form.Visible = true;
                            form.Dock = DockStyle.Fill;
                            tLPanel.Controls.Add(form, col, row);
                            dicFormSTATS.Add(list_camItem[n], form);
                            n++;

                        }
                    }

                }
                else
                {
                    MessageBox.Show("请到【管理员】配置检测项目。");
                    return;
                }
            }
            catch (SystemException ex)
            {
                ex.ToString();
                return;
            }
        }

        /// <summary>
        /// 根据窗体大小调整控件大小
        /// </summary>
        /// <param name="newx">窗体宽度缩放比例</param>
        /// <param name="newy">窗体高度缩放比例</param>
        /// <param name="cons">随窗体改变控件大小</param>
        public void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {

                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
                float a = System.Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度
                con.Width = (int)a;//宽度
                a = System.Convert.ToSingle(mytag[1]) * newy;//高度
                con.Height = (int)(a);
                a = System.Convert.ToSingle(mytag[2]) * newx;//左边距离
                con.Left = (int)(a);
                a = System.Convert.ToSingle(mytag[3]) * newy;//上边缘距离
                con.Top = (int)(a);
                Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }

        /// <summary>
        /// 将控件的宽，高，左边距，顶边距和字体大小暂存到tag属性中,Load属性中加载
        /// </summary>
        /// <param name="cons">递归控件中的控件</param>
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)//循环窗体中的控件
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
    }

    public class LoadConfig
    {
        public static bool LoadTMData(string serDataName)
        {
            try
            {
                string loadFile = GlobalPath.SavePath.GlobalDataPath + serDataName + ".json";
                string strFile = System.IO.File.ReadAllText(loadFile);
                var DynamicObject = JsonConvert.DeserializeObject<TMData_Serializer.GlobalData>(strFile);
                TMData_Serializer._globalData = (TMData_Serializer.GlobalData)DynamicObject;
                //读取模板ID
                LoadModelID(serDataName);
                //设置相机曝光时间
                foreach (string strCamser in TMData_Serializer._globalData.camParam.Keys)
                {
                    CamCommon.SetExposure(strCamser, (int)TMData_Serializer._globalData.camParam[strCamser].exposure);
                }
                return true;
            }
            catch (Exception)
            {
                StaticFun.MessageFun.ShowMessage("未找到示教参数，请设置!");
                MessageBox.Show("未找到示教参数，请设置!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

        }
        //加载相机曝光等参数
        public static Dictionary<string, CamSDK.CamCommon.CamParam> LoadCamParam()
        {
            Dictionary<string, CamSDK.CamCommon.CamParam> camParam = new Dictionary<string, CamSDK.CamCommon.CamParam>();
            try
            {
                if (System.IO.File.Exists(GlobalPath.SavePath.CamConfigPath))
                {
                    string OutData = System.IO.File.ReadAllText(GlobalPath.SavePath.CamConfigPath);
                    var expourse_cam = JsonConvert.DeserializeObject<GlobalData.Config.CamConfig>(OutData);
                    //camParam = expourse_cam.camParam;
                }
            }
            catch (Exception)
            {
                StaticFun.MessageFun.ShowMessage("相机曝光等参数加载出错。");
            }
            return camParam;
        }

        /// <summary>
        /// 初始化导入模板ID
        /// </summary>
        /// <param name="fileName"></param> json文件名称
        /// <returns></returns>
        public static bool LoadModelID(string fileName)
        {
            try
            {
                //加载模板ID
                string strFilePath = System.IO.Path.Combine(GlobalPath.SavePath.ModelPath, fileName);
                DirectoryInfo theFolder = new DirectoryInfo(strFilePath);
                DirectoryInfo[] fileInfo = theFolder.GetDirectories();
                if (0 == fileInfo.Length)
                {
                    StaticFun.MessageFun.ShowMessage("无文件，模板ID导入失败！");
                    MessageBox.Show("模板ID导入失败！");
                    return false;
                }
                foreach (DirectoryInfo NextFile in fileInfo) //遍历文件
                {
                    //if (NextFile.Name.Contains("相机1"))
                    //{
                    //    ReadID(strFilePath, 1);
                    //}
                    //else if (NextFile.Name.Contains("相机2"))
                    //{
                    //    ReadID(strFilePath, 2);
                    //}
                    //else if (NextFile.Name.Contains("相机3"))
                    //{
                    //    ReadID(strFilePath, 3);
                    //}
                    //else if (NextFile.Name.Contains("相机4"))
                    //{
                    //    ReadID(strFilePath, 4);
                    //}

                }
                StaticFun.MessageFun.ShowMessage("模板ID导入成功！");
                return true;
            }
            catch (Exception ex)
            {
                ("模板ID导入失败:" + ex.Message + ex.StackTrace).ToLog();
                StaticFun.MessageFun.ShowMessage("模板ID导入失败！" + ex.ToString());
                return false;

            }
        }
        private static void CopyModelFile(string fileName, int ncam)
        {
            try
            {
                //目标文件夹
                string folderPath = GlobalPath.SavePath.ModelPath + "相机" + ncam.ToString();
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                //目标文件名
                string strName = Path.GetFileName(fileName);
                //目标路径
                string targetPath = Path.Combine(folderPath, strName);
                FileInfo file = new FileInfo(fileName);
                if (file.Exists)
                {
                    //true 覆盖已存在的同名文件，false不覆盖
                    file.CopyTo(targetPath, true);
                }
            }
            catch (Exception ex)
            {
                (ex.Message + ex.StackTrace).ToLog();
                StaticFun.MessageFun.ShowMessage(ex.ToString());
            }
        }
        public static SetImageSaveDays LoadImageSaveDays()
        {
            SetImageSaveDays days = new SetImageSaveDays();
            try
            {
                string strData = System.IO.File.ReadAllText(GlobalPath.SavePath.TimePath);
                strData = Registered.DESDecrypt(strData);
                var DynamicObject = JsonConvert.DeserializeObject<SetImageSaveDays>(strData);
                days = DynamicObject;
            }
            catch (Exception)
            {
                StaticFun.MessageFun.ShowMessage("图片保存天数加载错误。");
            }
            return days;
        }
        public static void LoadCOMConfig()
        {
            try
            {
                string strData = System.IO.File.ReadAllText(GlobalPath.SavePath.IOPath);
                var DynamicObject = JsonConvert.DeserializeObject<TMData_Serializer.GlobalData_COM>(strData);
                TMData_Serializer._COMConfig = DynamicObject;
            }
            catch (Exception)
            {
                StaticFun.MessageFun.ShowMessage("IO配置文件加载错误。");
            }
        }

        public static void LoadTMSet()
        {
            try
            {
                //将json返回dynamic对象
                string strData = System.IO.File.ReadAllText(GlobalPath.SavePath.TMShowSetPath);
                var DynamicObject = JsonConvert.DeserializeObject<TMData.TMShowSet>(strData);
                TMData_Serializer._globalData.tmShowSet = (TMData.TMShowSet)DynamicObject;
                //TMFunction.SetTMShow();
            }
            catch (Exception ex)
            {

            }
        }

        public static void LoadJsonData(System.Windows.Forms.ListView listView)
        {
            try
            {
                listView.Items.Clear();
                //路径
                DirectoryInfo folder = new DirectoryInfo(GlobalPath.SavePath.GlobalDataPath);
                FileInfo[] fileInfo = folder.GetFiles();

                foreach (FileInfo NextFile in fileInfo) //遍历文件
                {
                    if (NextFile.Name.Contains(".json") && NextFile.Name != "NewestFileName.json")
                    {
                        int len = NextFile.Extension.Length;
                        string name = NextFile.Name.Substring(0, (NextFile.Name.Length - len));
                        string createTime = NextFile.LastWriteTime.ToString();
                        int n = listView.Items.Count;
                        listView.Items.Add(n.ToString());
                        listView.Items[n].SubItems.Add(name);
                        listView.Items[n].SubItems.Add(createTime);
                        listView.Items[n].EnsureVisible();
                    }
                }
            }
            catch (Exception ex)
            {
                (ex.Message + ex.StackTrace).ToLog();
                StaticFun.MessageFun.ShowMessage(ex.ToString());
            }
        }

        /// <summary>
        /// 加载【联系我们】数据
        /// </summary>
        public static void LoadContactData()
        {
            try
            {
                string strData = System.IO.File.ReadAllText(GlobalPath.SavePath.ContactPath);
                var DynamicObject = JsonConvert.DeserializeObject<GlobalData.Config.ContactUs>(strData);
                GlobalData.Config._Contact = DynamicObject;
            }
            catch (Exception)
            {
                StaticFun.MessageFun.ShowMessage("IO配置文件加载错误。");
            }
        }
    }

    public class SaveData
    {
        public static void SaveCamConfig()
        {
            try
            {
                //保存到配置文件
                var json = JsonConvert.SerializeObject(GlobalData.Config._CamConfig);
                System.IO.File.WriteAllText(GlobalPath.SavePath.CamConfigPath, json);
            }
            catch (Exception)
            {
                //MessageBox.Show("修改保存失败！");
            }
        }
        public static void SaveOtherConfig(BaseData.OtherConfig otherConfiData)
        {
            try
            {
                GlobalData.Config.InitConfig config = GlobalData.Config._InitConfig;
                config.otherConfig = otherConfiData;
                var json = JsonConvert.SerializeObject(config);
                System.IO.File.WriteAllText(GlobalPath.SavePath.InitConfigPath, json);
            }
            catch (Exception)
            {
                MessageBox.Show("修改保存失败！");
            }
        }

        /// <summary>
        /// 保存IO配置
        /// </summary>
        /// <param name="strCheckItem"></param> 检测项
        /// <param name="nSelIO"></param>  该检测项的IO点位
        /// <param name="nInorOut"></param> 此次保存输入点位还是输出点位 0：输入 1：输出
        public static void SaveIOConfig(TMData.IOSet ioSet)
        {
            try
            {
                bool bContain = false;
                if (TMData_Serializer._COMConfig.listIOSet.Count != 0)
                {
                    for (int i = 0; i < TMData_Serializer._COMConfig.listIOSet.Count; i++)
                    {
                        IOSet io = TMData_Serializer._COMConfig.listIOSet[i];
                        if (ioSet.camItem.cam == io.camItem.cam && ioSet.camItem.item == io.camItem.item &&
                            ioSet.camItem.surfaceType == io.camItem.surfaceType && ioSet.camItem.type == io.camItem.type && io.camItem.sub_cam == ioSet.camItem.sub_cam
                            )
                        {
                            TMData_Serializer._COMConfig.listIOSet[i] = ioSet;
                            bContain = true;
                        }
                    }
                }
                if (!bContain)
                {
                    TMData_Serializer._COMConfig.listIOSet.Add(ioSet);
                }
                var json = JsonConvert.SerializeObject(TMData_Serializer._COMConfig);
                System.IO.File.WriteAllText(GlobalPath.SavePath.IOPath, json);
            }
            catch (Exception ex)
            {
                ("IO配置保存失败:" + ex.ToString()).ToLog();
                MessageFun.ShowMessage("IO配置保存失败:" + ex.ToString());
            }
        }

        public static void SaveLightConfig(LightCtrlSet lightCtrlSet)
        {
            try
            {
                bool bContain = false;
                List<int> Del = new List<int>();
                if (TMData_Serializer._globalData.listLightCtrl.Count != 0)
                {
                    for (int i = 0; i < TMData_Serializer._globalData.listLightCtrl.Count; i++)
                    {
                        LightCtrlSet orgLightSet = TMData_Serializer._globalData.listLightCtrl[i];
                        if (lightCtrlSet.camItem.cam == orgLightSet.camItem.cam && lightCtrlSet.camItem.item == orgLightSet.camItem.item &&
                            lightCtrlSet.camItem.surfaceType == orgLightSet.camItem.surfaceType && lightCtrlSet.camItem.type == orgLightSet.camItem.type && orgLightSet.camItem.sub_cam == lightCtrlSet.camItem.sub_cam)
                        {
                            orgLightSet.CH = new bool[6];
                            orgLightSet.CH = lightCtrlSet.CH;
                            orgLightSet.nBrightness = new int[6];
                            orgLightSet.nBrightness = lightCtrlSet.nBrightness;
                            TMData_Serializer._globalData.listLightCtrl[i] = orgLightSet;
                            bContain = true;
                        }
                    }
                }
                if (!bContain)
                {
                    TMData_Serializer._globalData.listLightCtrl.Add(lightCtrlSet);
                }
                
                var json = JsonConvert.SerializeObject(TMData_Serializer._COMConfig);
                System.IO.File.WriteAllText(GlobalPath.SavePath.IOPath, json);
            }
            catch (Exception ex)
            {
                ("光源配置保存失败:" + ex.ToString()).ToLog();
                MessageFun.ShowMessage("光源配置保存失败:" + ex.ToString());
            }
        }

        /// <summary>
        /// 保存【联系我们】数据
        /// </summary>
        public static void SaveContactData()
        {
            try
            {
                //保存到配置文件
                var json = JsonConvert.SerializeObject(GlobalData.Config._Contact);
                System.IO.File.WriteAllText(GlobalPath.SavePath.ContactPath, json);
            }
            catch (Exception)
            {
                //MessageBox.Show("修改保存失败！");
            }
        }
    }

    public class DelectData
    {
        public static void DelectJsonFile(string selectFile, System.Windows.Forms.ListView listView)
        {
            try
            {
                //删除json文件
                System.IO.File.Delete(GlobalPath.SavePath.GlobalDataPath + selectFile + ".json");
                //删除模板文件夹及其下文件
                Directory.Delete(GlobalPath.SavePath.ModelPath + selectFile, true);
                //删除模板图像
                Directory.Delete(GlobalPath.SavePath.ModelImagePath + selectFile, true);
                LoadConfig.LoadJsonData(listView);
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage("删除文件" + selectFile + "出错：" + ex.ToString());
                return;
            }
        }
    }
}
