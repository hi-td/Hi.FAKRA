using StaticFun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VisionPlatform
{
    public partial class Show7 : Form
    {
        public static Dictionary<string, FormCamShow> formCamShows = new Dictionary<string, FormCamShow>();

        public static Dictionary<string, Function> m_listFun = new Dictionary<string, Function>();
        public static Dictionary<string, TMFunction> m_listTMFun = new Dictionary<string, TMFunction>();
        public static Dictionary<string, string> m_listCamSer = new Dictionary<string, string>();
        //public static List<Function> m_listFun = new List<Function>();
        //public static List<TMFunction> m_listTMFun = new List<TMFunction>();
        //public static List<string> m_listCamSer = new List<string>();

        public Show7()
        {
            InitializeComponent();
            _InitFun();
            //UIConfig.RefreshSTATS(tLPanel2, out TMFunction.m_ListFormSTATS, 2);
            //tLPanel1.Controls.Add(FormMainUI.formImageSave, 0, 0);
        }
        /// <summary>
        /// 解决窗体加载慢、卡顿问题
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;              //用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }
        //界面初始化
        private void _InitFun()
        {
            try
            {
                FormCamShow formCamShow;
                #region 相机窗口1初始化

                if (GlobalData.Config._CamConfig.camConfig.Keys.Contains(1))
                {
                    for (int i = 1; i < 5; i++)
                    {
                        formCamShow = new FormCamShow(GlobalData.Config._CamConfig.camConfig[1], 1, i.ToString());
                        formCamShow.TopLevel = false;
                        formCamShow.Visible = true;
                        formCamShow.Dock = DockStyle.Fill;
                        var panel_name = tableLayoutPanel1.Controls.Find($"panel{i}", true).FirstOrDefault();
                        panel_name.Controls.Clear();
                        panel_name.Controls.Add(formCamShow);
                        formCamShows.Add("1" + i.ToString(), formCamShow);
                    }
                }
                else
                {
                    for (int i = 1; i < 5; i++)
                    {
                        formCamShow = new FormCamShow("", 1, i.ToString());
                        formCamShow.TopLevel = false;
                        formCamShow.Visible = true;
                        formCamShow.Dock = DockStyle.Fill;
                        var panel_name = tableLayoutPanel1.Controls.Find($"panel{i}", true).FirstOrDefault();
                        panel_name.Controls.Clear();
                        panel_name.Controls.Add(formCamShow);
                        formCamShows.Add("1" + i.ToString(), formCamShow);
                    }
                }

                #endregion

                #region 相机窗口2初始化
                if (GlobalData.Config._CamConfig.camConfig.Keys.Contains(2))
                {
                    formCamShow = new FormCamShow(GlobalData.Config._CamConfig.camConfig[2], 2,"1");
                }
                else
                {
                    formCamShow = new FormCamShow("", 2,"1");
                }
                formCamShow.TopLevel = false;
                formCamShow.Visible = true;
                formCamShow.Dock = DockStyle.Fill;
                panel5.Controls.Clear();
                panel5.Controls.Add(formCamShow);
                formCamShows.Add("21", formCamShow);

                #endregion

                #region 相机窗口3初始化
                if (GlobalData.Config._CamConfig.camConfig.Keys.Contains(3))
                {
                    formCamShow = new FormCamShow(GlobalData.Config._CamConfig.camConfig[3], 3,"1");
                }
                else
                {
                    formCamShow = new FormCamShow("", 3,"1");
                }
                formCamShow.TopLevel = false;
                formCamShow.Visible = true;
                formCamShow.Dock = DockStyle.Fill;
                panel6.Controls.Clear();
                panel6.Controls.Add(formCamShow);
                formCamShows.Add("31", formCamShow);

                #endregion
                //加载消息显示
                FormMainUI.formShowResult.TopLevel = false;
                FormMainUI.formShowResult.Visible = true;
                FormMainUI.formShowResult.Dock = DockStyle.Fill;
                this.panel13.Controls.Clear();
                this.panel13.Controls.Add(FormMainUI.formShowResult);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void but_Run_Click(object sender, EventArgs e)
        {
            //StaticFun.Run.LoadRun(but_Run, m_listFun, m_listTMFun, m_listCamSer);
        }

        private void Show4_Load(object sender, EventArgs e)
        {

        }

        private void label1_Edit_Click(object sender, EventArgs e)
        {
            if (!FormMainUI.bRun)
            {
                StaticFun.UIConfig.CreateFormTeachMaster(1,"1");
            }
            else
            {
                //Run();
                //StaticFun.UIConfig.CreateFormTeachMaster(1);///???
            }
        }

        private void label2_Edit_Click(object sender, EventArgs e)
        {
            if (!FormMainUI.bRun)
            {
                StaticFun.UIConfig.CreateFormTeachMaster(1, "2");
            }
            else
            {
                //Run();
                //StaticFun.UIConfig.CreateFormTeachMaster(2); ///???
            }
        }

        private void label3_Edit_Click(object sender, EventArgs e)
        {
            if (!FormMainUI.bRun)
            {
                StaticFun.UIConfig.CreateFormTeachMaster(1, "3");
            }
            else
            {
                //Run();
                //StaticFun.UIConfig.CreateFormTeachMaster(3);///???
            }
        }

        private void label4_Edit_Click(object sender, EventArgs e)
        {
            if (!FormMainUI.bRun)
            {
                StaticFun.UIConfig.CreateFormTeachMaster(1,"4");
            }
            else
            {
                //Run();
                //StaticFun.UIConfig.CreateFormTeachMaster(4);///???
            }
        }

        private void label5_Edit_Click(object sender, EventArgs e)
        {
            if (!FormMainUI.bRun)
            {
                StaticFun.UIConfig.CreateFormTeachMaster(2, "1");
            }
            else
            {
                //Run();
                //StaticFun.UIConfig.CreateFormTeachMaster(4);///???
            }
        }

        private void label6_Edit_Click(object sender, EventArgs e)
        {
            if (!FormMainUI.bRun)
            {
                StaticFun.UIConfig.CreateFormTeachMaster(3, "1");
            }
            else
            {
                //Run();
                //StaticFun.UIConfig.CreateFormTeachMaster(4);///???
            }
        }
    }
}
