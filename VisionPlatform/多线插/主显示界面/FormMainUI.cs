#define DOG
using CamSDK;
using Chustange.Functional;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using WENYU_IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static VisionPlatform.TMData;
using hzjd_modbusRTU;
using System.Runtime.InteropServices;
using static VisionPlatform.Auxiliary.Dog;
using VisionPlatform.Auxiliary;
using static VisionPlatform.Auxiliary.Variable;
using static VisionPlatform.Security.Md5;
using StaticFun;
using DAL;

namespace VisionPlatform
{
    public partial class FormMainUI : Form
    {
        //private FormPLCCommQ formPLCCommQ = new FormPLCCommQ();
        //IO板卡测试界面
        
        private FormPLCRTU formPLCRTU = new FormPLCRTU();
        public static Login login;
        FormSaveGlobalData formSaveGlobalData;
        FormReadGlobalData formReadGlobalData;
        private FormLED formLED = new FormLED();
        public static Panel m_PanelShow;
        public static Show1 m_Show1;
        public static Show2 m_Show2;
        public static Show3 m_Show3;
        public static Show6 m_Show6;
        public static FormShowResult formShowResult = new FormShowResult(); //显示检测结果
        public static FormImageSave formImageSave = new FormImageSave();    //运行状态图像保存
       
        public bool formshow = false;
        public static bool bRun;  //是否处于生产状态
        [DllImport("user32.dll")]
        public static extern int MessageBoxTimeoutA(IntPtr hWnd, string msg, string Caps, int type, int Id, int time);   //引用DLL
        public FormMainUI(string title = null)
        {
            InitializeComponent();
            Function.InitSystem();      //初始化halcon
            RegisterDeviceNotification();//注册加密锁事件插拨通知
            RegisteredLogin();
            CamCommon.EnumCams();      //枚举相机
            LoadCamConfig();
            LoadUI();
            Text = title ?? Text;//<=>title==null?Text:title; 修改公司名称
            //timer1.Interval = 300000;
            timer1.Start();
            DeleteImages();
            m_PanelShow = this.panel_MainUI;
        }
        private void LoadCamConfig()
        {
            //加载相机配置文件及曝光等参数
            try
            {
                if (!File.Exists(GlobalPath.SavePath.CamConfigPath))
                    return;
                string OutData = System.IO.File.ReadAllText(GlobalPath.SavePath.CamConfigPath);
                var DynamicObj_cam = JsonConvert.DeserializeObject<GlobalData.Config.CamConfig>(OutData);
                GlobalData.Config._CamConfig = DynamicObj_cam;
                //如果没有相机配置文件,则随机为窗口分配序列号
                if (GlobalData.Config._CamConfig.camConfig.Count == 0)
                {
                    for (int i = 0; i < GlobalData.Config._InitConfig.initConfig.CamNum; i++)
                    {
                        GlobalData.Config._CamConfig.camConfig[i] = CamCommon.m_listCamSer[i];
                    }
                }
            }
            catch (SystemException ex)
            {

                StaticFun.MessageFun.ShowMessage("相机配置加载错误：" + ex.ToString());
            }
        }

        private void LoadUI()
        {
            #region 打开主界面:根据相机数量
            try
            {
                LoadData();
                int nCamNum = 0;
                if (null == GlobalData.Config._InitConfig.initConfig.dic_SubCam)
                {
                    GlobalData.Config._InitConfig.initConfig.dic_SubCam = new Dictionary<int, int>();
                    GlobalData.Config._InitConfig.initConfig.dic_SubCam.Add(1, 0);
                }
                foreach(int cam in GlobalData.Config._InitConfig.initConfig.dic_SubCam.Keys)
                {
                    nCamNum++;
                    int sub = GlobalData.Config._InitConfig.initConfig.dic_SubCam[cam];
                    if(0!=sub)
                    {
                        nCamNum = nCamNum + sub;
                    }
                }

                if (nCamNum == 1)
                {
                    m_Show1 = new Show1();
                    m_Show1.TopLevel = false;
                    m_Show1.Visible = true;
                    m_Show1.Dock = DockStyle.Fill;
                    this.panel_MainUI.Controls.Clear();
                    this.panel_MainUI.Controls.Add(m_Show1);
                    formshow = true;
                }
                else if (nCamNum == 2)
                {
                    m_Show2 = new Show2();
                    m_Show2.TopLevel = false;
                    m_Show2.Visible = true;
                    m_Show2.Dock = DockStyle.Fill;
                    this.panel_MainUI.Controls.Clear();
                    this.panel_MainUI.Controls.Add(m_Show2);
                    formshow = true;
                }
                else if (nCamNum == 3)
                {
                    m_Show3 = new Show3();
                    m_Show3.TopLevel = false;
                    m_Show3.Visible = true;
                    m_Show3.Dock = DockStyle.Fill;
                    this.panel_MainUI.Controls.Clear();
                    this.panel_MainUI.Controls.Add(m_Show3);
                    formshow = true;
                }
                else if (nCamNum == 6)
                {
                    m_Show6 = new Show6();
                    m_Show6.TopLevel = false;
                    m_Show6.Visible = true;
                    m_Show6.Dock = DockStyle.Fill;
                    this.panel_MainUI.Controls.Clear();
                    this.panel_MainUI.Controls.Add(m_Show6);
                    formshow = true;
                }
                else if (nCamNum == 7)
                {
                    //m_Show7 = new Show6();
                    //m_Show7.TopLevel = false;
                    //m_Show7.Visible = true;
                    //m_Show7.Dock = DockStyle.Fill;
                    //this.panel_MainUI.Controls.Clear();
                    //this.panel_MainUI.Controls.Add(m_Show7);
                    formshow = true;
                }
                else
                {
                    //默认打开双相机页面
                    m_Show2 = new Show2();
                    m_Show2.TopLevel = false;
                    m_Show2.Visible = true;
                    m_Show2.Dock = DockStyle.Fill;
                    this.panel_MainUI.Controls.Clear();
                    this.panel_MainUI.Controls.Add(m_Show2);
                    formshow = true;
                }
                formImageSave.TopLevel = false;
                formImageSave.Dock = DockStyle.Fill;
                formImageSave.Visible = true;
                //是否数字型光源控制器
                if(GlobalData.Config._InitConfig.initConfig.bDigitLight)
                {
                    toolStripBut_LightControl.Visible = true;
                }
                else
                {
                    toolStripBut_LightControl.Visible = false;
                }
            }
            catch (SystemException ex)
            {
                ex.ToString();
            }
            #endregion
        }
        private void LoadData()
        {
            try
            {
                //默认加载最新的序列化文件
                string strNewestFileName = System.IO.File.ReadAllText(GlobalPath.SavePath.NewestFile);
                //读取最新保存的序列化文件名称
                var name = JsonConvert.DeserializeObject<string>(strNewestFileName);
                //将导入的序列化参数名称显示到主页面
                label_SerialName.Text = name;
                LoadConfig.LoadTMData(name);
                string tup_Path = AppDomain.CurrentDomain.BaseDirectory + "PLC(RTU).json";
                if (!File.Exists(tup_Path))
                {
                    MessageBox.Show("当前软件通讯配置文件异常，请进入PLC调试界面进行设置！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string strPLCData = File.ReadAllText(tup_Path);
                    TMData_Serializer._PlcRTU.PlcRTU = JsonConvert.DeserializeObject<TMData.PLCRTU>(strPLCData);
                }
                #region 导入打端检测项
                string strData;
                if (File.Exists(GlobalPath.SavePath.TMCheckListPath))
                {
                    strData = File.ReadAllText(GlobalPath.SavePath.TMCheckListPath);
                    strData = Registered.DESDecrypt(strData);
                    var DynamicObject = JsonConvert.DeserializeObject<Dictionary<int, TMCheckItem>>(strData);
                    //TMData_Serializer._globalData.dicTMCheckList = DynamicObject;
                }
                //数据显示位置
                if (File.Exists(GlobalPath.SavePath.TMShowSetPath))
                {
                    LoadConfig.LoadTMSet();
                }
                #endregion

                //加载通讯
                StaticFun.LoadConfig.LoadCOMConfig();
                if (null != GlobalData.Config._InitConfig.initConfig.comMode)
                {
                    if (GlobalData.Config._InitConfig.initConfig.comMode.TYPE == EnumData.COMType.IO)
                    {
                        if (GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU8 ||
                            GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU16)
                        {
                            WENYU.OpenIO();
                        }
                        else if (GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU232)
                        {
                            WENYU.SearchIO();
                        }
                    }
                    else if (GlobalData.Config._InitConfig.initConfig.comMode.TYPE == EnumData.COMType.COM)
                    {
                        Modbus_RTU.OpenCom(TMData_Serializer._PlcRTU.PlcRTU);
                    }
                }
                else
                {
                    MessageBox.Show("未配置通讯方式，请先选择一种通讯模式。");
                }

                if (GlobalData.Config._InitConfig.initConfig.bDigitLight)
                {
                    LEDSet.OpenLedcom(TMData_Serializer._COMConfig.Led);
                    Thread.Sleep(10);       
                   
                    if (!LEDSet.isOpen)
                    {
                        MessageBox.Show("光源控制器链接异常，请检查光源控制器。");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("未找到工作模式配置文件，请到管理员界面设置!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ex.ToString();
            }

        }

        private void RegisteredLogin()
        {
            //判断当前软件是否注册
            try
            {
                RegistryKey localKey = Registry.LocalMachine;
                RegistryKey softWareKey = localKey.OpenSubKey("SOFTWARE", true);
                RegistryKey HLZNKey = softWareKey.OpenSubKey("BPY2\\PAR", true);
                if (null == HLZNKey)
                {
                    Registered registered = new Registered();
                    registered.ShowDialog();
                    HLZNKey = softWareKey.OpenSubKey("BPY2\\PAR", true);
                }
                string strInstalled = HLZNKey.GetValue("installed").ToString();
                if (strInstalled == Registered.SHA1(Registered.getMNum()))
                {
                    //当前软件已经注册过
                }
                else
                {
                    //没有注册
                    Registered registered = new Registered();
                    registered.ShowDialog();
                    strInstalled = HLZNKey.GetValue("installed").ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("注册异常！ 注册失败！" + ex.ToString());
                Process.GetCurrentProcess().Kill();
            }
        }

        private void FormMainUI_Load(object sender, EventArgs e)
        {
#if DOG

#endif
        }

        //保存数据
        private void toolStripBut_SaveGlobalData_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == formSaveGlobalData || formSaveGlobalData.IsDisposed)
                {
                    formSaveGlobalData = new FormSaveGlobalData();
                    formSaveGlobalData.Show();
                }
                else
                {
                    formSaveGlobalData.Activate(); //使子窗体获得焦点
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                (ex.Message + ex.StackTrace).ToLog();
            }
            string Address = System.AppDomain.CurrentDomain.BaseDirectory + "Address.json";
        }
        //导入数据
        private void toolStripBut_importData_Click(object sender, EventArgs e)
        {
            if (!bRun)
            {
                try
                {
                    if (null == formReadGlobalData || formReadGlobalData.IsDisposed)
                    {
                        formReadGlobalData = new FormReadGlobalData(label_SerialName);
                        formReadGlobalData.Show();
                    }
                    else
                    {
                        formReadGlobalData.Activate(); //使子窗体获得焦点
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    (ex.Message + ex.StackTrace).ToLog();
                }

            }
            else
            {
                MessageBox.Show("当前程序正在运行中，请停止检测按钮<运行中>后，再进行其它操作，谢谢配合。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        //主页
        private void toolStripBut_Home_Click(object sender, EventArgs e)
        {
            try
            {
                //FormMainUI.formShowResult.tabPage1.Parent = FormMainUI.formShowResult.tabControl1;
                //FormMainUI.formShowResult.tabControl1.SelectedTab = FormMainUI.formShowResult.tabPage1;
                switch (GlobalData.Config._InitConfig.initConfig.CamNum)
                {
                    case 1:
                        m_Show1.TopLevel = false;
                        m_Show1.Visible = true;
                        m_Show1.Dock = DockStyle.Fill;
                        this.panel_MainUI.Controls.Clear();
                        this.panel_MainUI.Controls.Add(m_Show1);
                        m_PanelShow.Controls.Add(FormMainUI.m_Show1);
                        m_Show1.panel1.Controls.Clear();
                        m_Show1.panel1.Controls.Add(Show1.formCamShow1);
                        //m_Show1.splitContainer1.Panel2.Controls.Clear();
                        //m_Show1.splitContainer1.Panel2.Controls.Add(FormMainUI.formShowResult);
                        //FormMainUI.formShowResult.tabPage1.Parent = FormMainUI.formShowResult.tabControl1;
                        StaticFun.Run.Zoom(Show1.m_listFun);
                        break;
                    case 2:
                        m_Show2.TopLevel = false;
                        m_Show2.Visible = true;
                        m_Show2.Dock = DockStyle.Fill;
                        this.panel_MainUI.Controls.Clear();
                        this.panel_MainUI.Controls.Add(m_Show2);
                        m_PanelShow.Controls.Add(FormMainUI.m_Show2);
                        //m_Show2.panel1.Controls.Clear();
                        //m_Show2.panel1.Controls.Add(Show2.formCamShow1);
                        //m_Show2.panel2.Controls.Clear();
                        //m_Show2.panel2.Controls.Add(Show2.formCamShow2);
                        //m_Show2.splitContainer1.Panel2.Controls.Clear();
                        //m_Show2.splitContainer1.Panel2.Controls.Add(FormMainUI.formShowResult);
                        StaticFun.Run.Zoom(Show2.m_listFun);
                        break;
                    case 3:
                        break;
                    case 7:
                        if (!formshow)
                        {
                            //m_Show7.TopLevel = false;
                            //m_Show7.Visible = true;
                            //m_Show7.Dock = DockStyle.Fill;
                            //this.panel_MainUI.Controls.Clear();
                            //this.panel_MainUI.Controls.Add(m_Show7);
                            //formshow = true;
                            //m_PanelShow.Controls.Add(FormMainUI.m_Show7);
                            //m_Show7.panel1.Controls.Clear();
                            //m_Show7.panel1.Controls.Add(m_Show7.formCamShow1);
                            //m_Show4.panel2.Controls.Clear();
                            //m_Show4.panel2.Controls.Add(Show4.formCamShow2);
                            //m_Show4.panel3.Controls.Clear();
                            //m_Show4.panel3.Controls.Add(Show4.formCamShow3);
                            //m_Show4.panel4.Controls.Clear();
                            //m_Show4.panel4.Controls.Add(Show4.formCamShow4);
                            //m_Show4.splitContainer2.Panel2.Controls.Clear();
                            //m_Show4.splitContainer2.Panel2.Controls.Add(FormMainUI.formShowResult);
                            //StaticFun.Run.Zoom(Show4.m_listFun);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ("【主页】初始化错位：" + ex.ToString()).ToLog();
            }
        }
        //通讯
        private void toolStripBut_PLCComm_Click(object sender, EventArgs e)
        {

            if (m_Show1.but_Run.Text == "运行")
            {
                LoginTX loginTX = new LoginTX();
                loginTX.ShowDialog();
                if (loginTX.Ba)
                {
                    formPLCRTU.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("当前程序正在运行中，请停止检测按钮<运行中>后，再进行其它操作，谢谢配合。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void FormMainUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您确定要关闭软件吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                try
                {
                    if (GlobalData.Config._InitConfig.initConfig.comMode.TYPE == EnumData.COMType.IO)
                    {
                        WENYU_PIO32P.WY_WriteOutPutBit0(WENYU.DevID, 1);
                        WENYU_PIO32P.WY_WriteOutPutBit1(WENYU.DevID, 1);
                        WENYU_PIO32P.WY_WriteOutPutBit2(WENYU.DevID, 1);
                        WENYU.CloseIO();
                    }
                    if (GlobalData.Config._InitConfig.initConfig.bDigitLight)
                    {
                        if (LEDSet.isOpen)
                        {
                            //将所有光源通道亮度值设置为0
                            for (int ch = 1; ch <= GlobalData.Config._InitConfig.initConfig.nLightCH; ch++)
                            {
                                LEDSet.SetBrightness(ch, 0);
                            }
                            //关闭光源控制器串口
                            LEDSet.CloseLED();
                        }
                    }
                    CamCommon.CloseAllCam();
                    Process.GetCurrentProcess().Kill();
                }
                catch
                {
                    Process.GetCurrentProcess().Kill();
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void tSBut_InitCam_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormMainUI.bRun)
                {
                    CamCommon.CloseAllCam();
                    CamCommon.EnumCams();
                }
                else
                {
                    MessageBox.Show("当前程序正在运行中，请停止检测按钮<运行中>后，再进行其它操作，谢谢配合。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.ToString());
                throw;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                formLED = new FormLED();
                formLED.TopMost = true;
                formLED.Show();
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.ToString());
                throw;
            }
        }
        private static void DeleteImages()
        {
            int days = 7;
            //日志保留时长 单位：天
            string logsDay = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "原始OK图像\\");
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    logsDay = GlobalPath.SavePath.cam1_OrgImagePath_OK;
                }
                else if (i == 1)
                {
                    logsDay = GlobalPath.SavePath.cam1_OrgImagePath_NG;
                }
                else if (i == 2)
                {
                    logsDay = GlobalPath.SavePath.cam1_ReslutImagePath_OK;
                }
                else if (i == 3)
                {
                    logsDay = GlobalPath.SavePath.cam1_ReslutImagePath_NG;
                }
                try
                {
                    if (Directory.Exists(logsDay))
                    {
                        var now = DateTime.Now;
                        foreach (var f in Directory.GetFileSystemEntries(logsDay))//).Where(f => File.Exists(f)
                        {
                            var t = File.GetCreationTime(f);
                            var elapsedTicks = now.Ticks - t.Ticks;
                            var elaspsedSpan = new TimeSpan(elapsedTicks);
                            if (elaspsedSpan.TotalDays > days)
                            {
                                Directory.Delete(f, true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    StaticFun.MessageFun.ShowMessage(ex.ToString());
                }
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormMainUI.bRun)
                {
                    if (GlobalData.Config._InitConfig.initConfig.comMode.TYPE == EnumData.COMType.IO)
                    {
                        if (GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU8 ||
                            GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU16)
                        {
                            FormIOcomm formIOcomm = new FormIOcomm();
                            formIOcomm.ShowDialog();
                        }
                        else if (GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU232)
                        {
                            if (WENYU.isOpen)
                            {
                                FormModbusIO formModbusIO = new FormModbusIO(TMData_Serializer._COMConfig.WENYU232_ComPort);
                                formModbusIO.StartPosition = FormStartPosition.CenterScreen;
                                formModbusIO.TopMost = true;
                                formModbusIO.Show();
                            }
                            else
                            {
                                FormSetCom formSetCom = new FormSetCom();
                                formSetCom.ShowDialog();
                            }
                            
                        }
                        
                    }
                    else if (GlobalData.Config._InitConfig.initConfig.comMode.TYPE == EnumData.COMType.NET)
                    {
                        //formPLCRTU.ShowDialog();
                    }
                    else if (GlobalData.Config._InitConfig.initConfig.comMode.TYPE == EnumData.COMType.COM)
                    {
                        formPLCRTU.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("当前程序正在运行中，请停止检测按钮<运行中>后，再进行其它操作，谢谢配合。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.ToString());
            }
        }

        int isReadDisk = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                label_Time.Text = DateTime.Now.ToString();
                isReadDisk++;
                if (isReadDisk > 72000)
                {
                    StaticFun.Fun.ReadDisk();    //读取硬盘存储空间
                    isReadDisk = 0;
                }
            }
            catch (Exception ex)
            {
                ex.ToString().ToLog();
            }
        }

        private void ts_But_Admin_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == login || login.IsDisposed)
                {
                    login = new Login(this);
                    login.TopMost = true;
                    login.ShowDialog();
                }
                else
                {
                    login.Activate(); //使子窗体获得焦点
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                (ex.Message + ex.StackTrace).ToLog();
            }
        }

        #region 导入公司Logo
        string str_LogoPath = "";
        private void 导入Logo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.InitialDirectory = ".";
                // file.Filter = "BMP图片|*.bmp|JPG图片|*.jpg|Gif图片|*.gif|PNG图片|*.png";
                file.Filter = "所有文件(*.*)|*.*";
                file.ShowDialog();
                if (file.FileName != string.Empty)
                {
                    try
                    {
                        str_LogoPath = file.FileName;   //获得文件的绝对路径
                        this.picBox_Logo.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.picBox_Logo.Load(str_LogoPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void 清除公司LOGO_Click(object sender, EventArgs e)
        {
            this.picBox_Logo.Image = null;
        }

        private void LoadCompanyLogo()
        {
            try
            {
                if ("" != str_LogoPath)
                {
                    this.picBox_Logo.Load(str_LogoPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void 填充_Click(object sender, EventArgs e)
        {
            this.picBox_Logo.SizeMode = PictureBoxSizeMode.StretchImage;
            LoadCompanyLogo();
        }

        private void 按图像大小_Click(object sender, EventArgs e)
        {
            this.picBox_Logo.SizeMode = PictureBoxSizeMode.AutoSize;
            LoadCompanyLogo();
        }

        private void 居中显示_Click(object sender, EventArgs e)
        {
            this.picBox_Logo.SizeMode = PictureBoxSizeMode.CenterImage;
            LoadCompanyLogo();
        }

        private void 图像自适应_Click(object sender, EventArgs e)
        {
            this.picBox_Logo.SizeMode = PictureBoxSizeMode.Zoom;
            LoadCompanyLogo();
        }
        #endregion

#if DOG
        #region<!--DOG-->
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_DEVICECHANGE: OnDeviceChange(ref m); break;
            }
            base.WndProc(ref m);
        }
        private void OnDeviceChange(ref System.Windows.Forms.Message msg)
        {
            int wParam = (int)msg.WParam;
            if (wParam == DBT_DEVICEREMOVECOMPLETE)//收到硬件拨出信息后，调用检查锁函数来检查是否锁被拨出
            {
                DEV_BROADCAST_DEVICEINTERFACE1 DeviceInfo = (DEV_BROADCAST_DEVICEINTERFACE1)Marshal.PtrToStructure(msg.LParam, typeof(DEV_BROADCAST_DEVICEINTERFACE1));
                // 这里使用 FindDogOnPnp检查加密锁是否被拨出，也可以使用其它检查锁函数来检查加密锁是否被拨出
                var path = string.Empty;
                if (FindPort(0, ref path) != 0)
                {
                    MessageBoxTimeoutA((IntPtr)0, "加密狗被拨出，程序将异常退出！", "消息框", 0, 0, 10000);    // 直接调用  3秒后自动关闭
                    //MessageBox.Show(" 加密狗被拨出，程序将异常退出！");
                    Environment.Exit(0);
                }
                else
                {
                    Company = GetValue("Company", DogPath);
                    Author = GetValue("Author", DogPath);
                    ModeChar = GetValue("ModeChar", DogPath);
                    Md5Value = GetValue("Md5", DogPath);
                    SerserialNumber = GetValue("SerserialNumber", DogPath);
                    GetOrder = GetValue("GetOrder", DogPath);
                    GetState = GetValue("GetState", DogPath);
                    GetAdaptImageSize = GetValue("GetAdaptImageSize", DogPath);
                    if (string.IsNullOrEmpty(Company) || string.IsNullOrEmpty(Author) || string.IsNullOrEmpty(ModeChar) || string.IsNullOrEmpty(Md5Value) || string.IsNullOrEmpty(SerserialNumber) || string.IsNullOrEmpty(GetOrder) || string.IsNullOrEmpty(GetState) || string.IsNullOrEmpty(GetAdaptImageSize))
                    {
                        MessageBox.Show("检测到当前加密狗异常，请联系厂家进行出厂校验！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(0);
                    }
                    else
                    {
                        if (!Md5Value.Equals(Encrypt(Company)))
                        {
                            MessageBox.Show("检测到当前加密狗异常！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(0);
                        }
                    }
                }
            }
        }
        private void RegisterDeviceNotification()
        {
            DEV_BROADCAST_DEVICEINTERFACE dbi = new DEV_BROADCAST_DEVICEINTERFACE();
            int size = Marshal.SizeOf(dbi);
            dbi.dbcc_size = size;
            dbi.dbcc_devicetype = DBT_DEVTYP_DEVICEINTERFACE;
            dbi.dbcc_reserved = 0;
            dbi.dbcc_classguid = GUID_DEVINTERFACE_USB_DEVICE;
            dbi.dbcc_name = 0;
            IntPtr buffer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(dbi, buffer, true);
            RegisterDeviceNotification(Handle, buffer, DEVICE_NOTIFY_WINDOW_HANDLE);
        }

        #endregion
#endif

        public static readonly Guid GUID_DEVINTERFACE_USB_DEVICE = new Guid("4D1E55B2-F16F-11CF-88CB-001111000030");
        public const int WM_DEVICECHANGE = 0x0219;
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004; // device is gone
        public const int DBT_DEVTYP_DEVICEINTERFACE = 0x00000005; // deviceinterface class
        public const int DEVICE_NOTIFY_WINDOW_HANDLE = 0x0;
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr
            NotificationFilter, int Flags);

        FormContactUs formContactUs;     //联系我们
        private void 联系我们_Click(object sender, EventArgs e)
        {
            try
            {
                if(null == formContactUs || !formContactUs.Created || formContactUs.IsDisposed)
                {
                    formContactUs = new FormContactUs();
                }
                formContactUs.Show();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }
        FormOperateInstruct formOperateInstruct;    //操作视频
        private void 操作视频_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == formOperateInstruct || !formOperateInstruct.Created || formOperateInstruct.IsDisposed)
                {
                    formOperateInstruct = new FormOperateInstruct();
                }
                formOperateInstruct.Show();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

        private void 用户操作指导说明书_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Application.StartupPath + @"\Hi.MultTM.chm");//打开debug文件夹下的chm文件
            //Help.ShowHelp(this, Application.StartupPath + @"\yourchm.chm", "yourchm_chapter.htm");//打开debug文件夹下的chm文件的yourchm_chapter.htm这一页
            //Help.ShowHelpIndex(this, Application.StartupPath + @"\yourchm.chm");//开debug文件夹下的chm文件的索引
        }

        private void panel_MainUI_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
