using StaticFun;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using WENYU_IO;     //IO板卡
using System.Diagnostics;   //计算运行时间
using Chustange.Functional;  //IO通讯、错误日志
using Newtonsoft.Json;
using System.IO.Ports;

namespace VisionPlatform
{
    public partial class Show1 : Form
    {
        public static FormCamShow formCamShow1;
        public static List< Function> m_listFun = new List<Function>();
        public static List<TMFunction> m_listTMFun = new List<TMFunction>();
        public static List<string> m_listCamSer = new List<string>();

        public Show1()
        {
            InitializeComponent();
            _InitFun();
            //UIConfig.RefreshSTATS(tLPanel, out TMFunction.m_ListFormSTATS);
            //tableLayoutPanel7.Controls.Add(FormMainUI.formImageSave, 1, 0);
        }

        //初始化Function
        private void _InitFun()
        {
            #region 相机窗口1初始化
            if (GlobalData.Config._CamConfig.camConfig.Keys.Contains(1))
            {
                formCamShow1 = new FormCamShow(GlobalData.Config._CamConfig.camConfig[1], 1,"1");
            }
            else
            {
                formCamShow1 = new FormCamShow("", 1, "1");
            }
            formCamShow1.TopLevel = false;
            formCamShow1.Visible = true;
            formCamShow1.Dock = DockStyle.Fill;
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(formCamShow1);
            formCamShow1.label_x.Visible = false;
            formCamShow1.label_d.Visible = false;
            m_listFun.Add(formCamShow1.fun);
            m_listTMFun.Add(formCamShow1.TM_fun);
            m_listCamSer.Add(formCamShow1.m_strCamSer);
            #endregion

            //加载消息显示
            FormMainUI.formShowResult.TopLevel = false;
            FormMainUI.formShowResult.Visible = true;
            FormMainUI.formShowResult.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Clear();
            this.splitContainer1.Panel2.Controls.Add(FormMainUI.formShowResult);
        }

        private void Show1_Load(object sender, EventArgs e)
        {
            //if (GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU8 || 
            //    GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU16)
            //{
            //    long VersionNumber = -1;
            //    Fun.isOpenIO = WENYU_PIO32P.WY_GetCardVersion(WENYU.DevID, ref VersionNumber) == 0 ? true : false;
            //}
            //else if (GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU232)
            //{
            //    Fun.isOpenIO = JD_modbusRTU.WENYUIO.OpenIO();
            //}
            //if (GlobalData.Config._InitConfig.initConfig.bDigitLight)
            //{
            //    Fun.isOpenLed = LEDSet.OpenLedcom();
            //}

        }

        private void but_Run_Click(object sender, EventArgs e)
        {
            StaticFun.Run.LoadRun(but_Run, m_listFun, m_listTMFun, m_listCamSer);
        }

        private void Show1_SizeChanged(object sender, EventArgs e)
        {
            StaticFun.Run.Zoom(m_listFun);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (!FormMainUI.bRun)
            {
                
                StaticFun.UIConfig.CreateFormTeachMaster(1, "",1);
            }
            else
            {
                MessageBox.Show("当前程序正在运行中，请停止检测按钮<运行中>后，再进行其它操作，谢谢配合。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
