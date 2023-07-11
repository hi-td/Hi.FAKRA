using StaticFun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VisionPlatform
{
    public partial class Show6 : Form
    {
        public static Dictionary<int, TMData.ShowItems[]> dic_formCamShows = new Dictionary<int, TMData.ShowItems[]>();

        public static Dictionary<string, Function> m_listFun = new Dictionary<string, Function>();
        public static Dictionary<string, TMFunction> m_listTMFun = new Dictionary<string, TMFunction>();
        public static Dictionary<string, string> m_listCamSer = new Dictionary<string, string>();
        //public static List<Function> m_listFun = new List<Function>();
        //public static List<TMFunction> m_listTMFun = new List<TMFunction>();
        //public static List<string> m_listCamSer = new List<string>();

        public Show6()
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
                TMData.ShowItems[] showItems;
                foreach (int cam in GlobalData.Config._CamConfig.camConfig.Keys)
                {
                    string strCamSer = GlobalData.Config._CamConfig.camConfig[cam];
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
                            showItems[i+1].form = formCamShow;
                        }
                        dic_formCamShows.Add(cam, showItems);
                    }
                    else
                    {
                        dic_formCamShows.Add(cam, new TMData.ShowItems[1] {new TMData.ShowItems(){ form = formCamShow}});
                    }
                }
                dic_formCamShows[1][0].panel = this.panel1;
                dic_formCamShows[1][1].panel = this.panel2;
                dic_formCamShows[1][2].panel = this.panel3;
                dic_formCamShows[1][3].panel = this.panel4;
                dic_formCamShows[2][0].panel = this.panel5;
                dic_formCamShows[3][0].panel = this.panel6;
                panel1.Controls.Clear();
                panel1.Controls.Add(dic_formCamShows[1][0].form);
                panel2.Controls.Clear();
                panel2.Controls.Add(dic_formCamShows[1][1].form);
                panel3.Controls.Clear();
                panel3.Controls.Add(dic_formCamShows[1][2].form);
                panel4.Controls.Clear();
                panel4.Controls.Add(dic_formCamShows[1][3].form);
                panel5.Controls.Clear();
                panel5.Controls.Add(dic_formCamShows[2][0].form);
                panel6.Controls.Clear();
                panel6.Controls.Add(dic_formCamShows[3][0].form);

                //#region 相机窗口1初始化

                //if (GlobalData.Config._CamConfig.camConfig.Keys.Contains(1))
                //{
                //    for (int i = 1; i < 5; i++)
                //    {
                //        formCamShow = new FormCamShow(GlobalData.Config._CamConfig.camConfig[1], 1, i);
                //        formCamShow.TopLevel = false;
                //        formCamShow.Visible = true;
                //        formCamShow.Dock = DockStyle.Fill;
                //        var panel_name = tableLayoutPanel1.Controls.Find($"panel{i}", true).FirstOrDefault();
                //        panel_name.Controls.Clear();
                //        panel_name.Controls.Add(formCamShow);
                //        formCamShows.Add("1" + i.ToString(), formCamShow);
                //    }
                //}
                //else
                //{
                //    for (int i = 1; i < 5; i++)
                //    {
                //        formCamShow = new FormCamShow("", 1, i);
                //        formCamShow.TopLevel = false;
                //        formCamShow.Visible = true;
                //        formCamShow.Dock = DockStyle.Fill;
                //        var panel_name = tableLayoutPanel1.Controls.Find($"panel{i}", true).FirstOrDefault();
                //        panel_name.Controls.Clear();
                //        panel_name.Controls.Add(formCamShow);
                //        formCamShows.Add("1" + i.ToString(), formCamShow);
                //    }
                //}

                //#endregion

                //#region 相机窗口2初始化
                //if (GlobalData.Config._CamConfig.camConfig.Keys.Contains(2))
                //{
                //    formCamShow = new FormCamShow(GlobalData.Config._CamConfig.camConfig[2], 2, 0);
                //}
                //else
                //{
                //    formCamShow = new FormCamShow("", 2, 0);
                //}
                //formCamShow.TopLevel = false;
                //formCamShow.Visible = true;
                //formCamShow.Dock = DockStyle.Fill;
                //panel5.Controls.Clear();
                //panel5.Controls.Add(formCamShow);
                //formCamShows.Add("21", formCamShow);

                //#endregion

                //#region 相机窗口3初始化
                //if (GlobalData.Config._CamConfig.camConfig.Keys.Contains(3))
                //{
                //    formCamShow = new FormCamShow(GlobalData.Config._CamConfig.camConfig[3], 3, 0);
                //}
                //else
                //{
                //    formCamShow = new FormCamShow("", 3, 0);
                //}
                //formCamShow.TopLevel = false;
                //formCamShow.Visible = true;
                //formCamShow.Dock = DockStyle.Fill;
                //panel6.Controls.Clear();
                //panel6.Controls.Add(formCamShow);
                //formCamShows.Add("31", formCamShow);

                //#endregion
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

    }
}
