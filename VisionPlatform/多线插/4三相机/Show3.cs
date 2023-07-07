/***********************************************************
* CLR版本：4.0.30319.42000
* 类 名 称：Show3
* 机器名称：WIN-C8KIOVQPABN
* 命名空间：VisionPlatform.宜鑫端子机
* 文 件 名：Show3
* 创建时间：2022/1/10 11:16:45
* 作    者： Chustange
* 公    司：HaiLan Intelligent
* 说   明：
* 修改时间：
* 修 改 人：
* 修改说明：
* 深圳市海蓝智能科技有限公司  2021  保留所有权利.
***********************************************************/
using StaticFun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VisionPlatform
{
    public partial class Show3 : Form
    {
        public static FormCamShow formCamShow1;
        public static FormCamShow formCamShow2;
        public static FormCamShow formCamShow3;
        public static List<Function> m_listFun = new List<Function>();
        public static List<TMFunction> m_listTMFun = new List<TMFunction>();
        public static List<string> m_listCamSer = new List<string>();

        public Show3()
        {
            InitializeComponent();
            _InitFun();
            UIConfig.RefreshSTATS(tLPanel_resultShow, out TMFunction.m_ListFormSTATS);
            tLPanel_ImageSave.Controls.Add(FormMainUI.formImageSave, 1, 0);
        }
        private void label3_Edit_Click(object sender, EventArgs e)
        {
            if (!FormMainUI.bRun)
            {
                //StaticFun.UIConfig.CreateFormTeachMaster(3);
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
                //StaticFun.UIConfig.CreateFormTeachMaster(2);
            }
            else
            {
                //Run();
                //StaticFun.UIConfig.CreateFormTeachMaster(1);///???
            }
        }

        private void label1_Edit_Click(object sender, EventArgs e)
        {
            if (!FormMainUI.bRun)
            {
                //StaticFun.UIConfig.CreateFormTeachMaster(1);
            }
            else
            {
                //Run();
                //StaticFun.UIConfig.CreateFormTeachMaster(1);///???
            }
        }
        private void but_Run_Click(object sender, EventArgs e)
        {
            StaticFun.Run.LoadRun(but_Run, m_listFun, m_listTMFun, m_listCamSer);
        }
        
        private void Show3_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception)
            {
            }
        }

        //界面初始化
        private void _InitFun()
        {
            try
            {
                //#region 相机窗口1初始化
                //if (null == formCamShow1 || formCamShow1.IsDisposed)
                //{
                //    if (GlobalData.Config._CamConfig.camConfig.Keys.Contains(1))
                //    {
                //        formCamShow1 = new FormCamShow(GlobalData.Config._CamConfig.camConfig[1], 1);
                //    }
                //    else
                //    {
                //        formCamShow1 = new FormCamShow("", 1);
                //    }
                //}
                //formCamShow1.TopLevel = false;
                //formCamShow1.Visible = true;
                //formCamShow1.Dock = DockStyle.Fill;
                //this.panel1.Controls.Clear();
                //this.panel1.Controls.Add(formCamShow1);
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
                //m_listFun.Add(formCamShow2.fun);
                //m_listTMFun.Add(formCamShow2.TM_fun);
                //m_listCamSer.Add(formCamShow2.m_strCamSer);
                //#endregion

                //#region 相机窗口3初始化
                //if (GlobalData.Config._CamConfig.camConfig.Keys.Contains(3))
                //{
                //    formCamShow3 = new FormCamShow(GlobalData.Config._CamConfig.camConfig[3], 3);
                //}
                //else
                //{
                //    formCamShow3 = new FormCamShow("", 3);
                //}
                //formCamShow3.TopLevel = false;
                //formCamShow3.Visible = true;
                //formCamShow3.Dock = DockStyle.Fill;
                //this.panel3.Controls.Clear();
                //this.panel3.Controls.Add(formCamShow3);
                //m_listFun.Add(formCamShow3.fun);
                //m_listTMFun.Add(formCamShow3.TM_fun);
                //m_listCamSer.Add(formCamShow3.m_strCamSer);
                //#endregion

                //加载消息显示
                //FormMainUI.formShowResult.TopLevel = false;
                //FormMainUI.formShowResult.Visible = true;
                //FormMainUI.formShowResult.Dock = DockStyle.Fill;
                //this.panel_Message.Controls.Clear();
                //this.panel_Message.Controls.Add(FormMainUI.formShowResult);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

    }
}
