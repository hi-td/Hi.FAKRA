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

        public Show3()
        {
            InitializeComponent();
            _InitFun();
            UIConfig.RefreshSTATS(tLPanel_resultShow, out TMFunction.m_ListFormSTATS);
            tLPanel_ImageSave.Controls.Add(FormMainUI.formImageSave, 1, 0);
        }

        private void but_Run_Click(object sender, EventArgs e)
        {
            //StaticFun.Run.LoadRun(but_Run, m_listFun, m_listTMFun, m_listCamSer);
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
                FormMainUI.m_dicFormCamShows = StaticFun.UIConfig.InitShowUI(contextMenuStrip1, tLPanel_CamShow);
                panel1.ContextMenuStrip = contextMenuStrip1;
                panel2.ContextMenuStrip = contextMenuStrip1;
                panel3.ContextMenuStrip = contextMenuStrip1;
                //加载消息显示
                FormMainUI.formShowResult.TopLevel = false;
                FormMainUI.formShowResult.Visible = true;
                FormMainUI.formShowResult.Dock = DockStyle.Fill;
                this.panel_Message.Controls.Clear();
                this.panel_Message.Controls.Add(FormMainUI.formShowResult);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            StaticFun.UIConfig.ConfigShowItems(sender, e, ref FormMainUI.m_dicFormCamShows);
        }
    }
}
