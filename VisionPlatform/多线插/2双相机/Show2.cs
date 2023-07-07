using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StaticFun;

namespace VisionPlatform
{
    public partial class Show2 : Form
    {
        public static FormCamShow formCamShow1;
        public static FormCamShow formCamShow2;
        public static List<Function> m_listFun = new List<Function>();
        public static List<TMFunction> m_listTMFun = new List<TMFunction>();
        public static List<string> m_listCamSer = new List<string>();

        public Show2()
        {
            InitializeComponent();
            _InitFun();
            UIConfig.RefreshSTATS(tLPanel, out TMFunction.m_ListFormSTATS);
            tableLayoutPanel7.Controls.Add(FormMainUI.formImageSave, 2, 0);
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

        //初始化Function
        private void _InitFun()
        {
            //#region 相机窗口1初始化
            //if (GlobalData.Config._CamConfig.camConfig.Keys.Contains(1))
            //{
            //    formCamShow1 = new FormCamShow(GlobalData.Config._CamConfig.camConfig[1], 1);
            //}
            //else
            //{
            //    formCamShow1 = new FormCamShow("", 1);
            //}
            //formCamShow1.TopLevel = false;
            //formCamShow1.Visible = true;
            //formCamShow1.Dock = DockStyle.Fill;
            //this.panel1.Controls.Clear();
            //this.panel1.Controls.Add(formCamShow1);
            //formCamShow1.label_x.Visible = false;
            //formCamShow1.label_d.Visible = false;
            //m_listFun.Add(formCamShow1.fun);
            //m_listTMFun.Add(formCamShow1.TM_fun);
            //m_listCamSer.Add(formCamShow1.m_strCamSer);
            //#endregion

            //#region 相机窗口2初始化
            //if (GlobalData.Config._CamConfig.camConfig.Keys.Contains(2))
            //{
            //    formCamShow2 = new FormCamShow(GlobalData.Config._CamConfig.camConfig[2], 2);
            //}
            //else
            //{
            //    formCamShow2 = new FormCamShow("", 2);
            //}
            //formCamShow2.TopLevel = false;
            //formCamShow2.Visible = true;
            //formCamShow2.Dock = DockStyle.Fill;
            //this.panel2.Controls.Clear();
            //this.panel2.Controls.Add(formCamShow2);
            //formCamShow2.label_x.Visible = false;
            //formCamShow2.label_d.Visible = false;
            //m_listFun.Add(formCamShow2.fun);
            //m_listTMFun.Add(formCamShow2.TM_fun);
            //m_listCamSer.Add(formCamShow2.m_strCamSer);
            //#endregion
            //加载检测结果显示
            //FormMainUI.formShowResult.TopLevel = false;
            //FormMainUI.formShowResult.Visible = true;
            //FormMainUI.formShowResult.Dock = DockStyle.Fill;
            //this.splitContainer1.Panel2.Controls.Clear();
            //this.splitContainer1.Panel2.Controls.Add(FormMainUI.formShowResult);
        }
        private void Show2_Load(object sender, EventArgs e)
        {
            // CheckSave();
        }

        private void but_Run_Click(object sender, EventArgs e)
        {
            StaticFun.Run.LoadRun(but_Run, m_listFun, m_listTMFun, m_listCamSer);
        }

        private void Show2_SizeChanged(object sender, EventArgs e)
        {
           StaticFun.Run.Zoom(m_listFun);
        }

        private void label1_Edit_Click(object sender, EventArgs e)
        {
            if (!FormMainUI.bRun)
            {
                //StaticFun.UIConfig.CreateFormTeachMaster(1);
            }
            else
            {
                MessageBox.Show("当前程序正在运行中，请停止检测按钮<运行中>后，再进行其它操作，谢谢配合。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void label2_Edit_Click(object sender, EventArgs e)
        {
            if (!FormMainUI.bRun)
            {
                //StaticFun.UIConfig.CreateFormTeachMaster(2);
            }
            else
            {
                MessageBox.Show("当前程序正在运行中，请停止检测按钮<运行中>后，再进行其它操作，谢谢配合。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
