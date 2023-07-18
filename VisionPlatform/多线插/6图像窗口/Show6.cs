using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static VisionPlatform.TMData;

namespace VisionPlatform
{
    public partial class Show6 : Form
    {
        public Show6()
        {
            InitializeComponent();
            _InitFun();
            //UIConfig.RefreshSTATS(tLPanel2, out TMFunction.m_ListFormSTATS, 2);
            FormMainUI.formImageSave.RefeshUI(2);
            tLPanel.Controls.Add(FormMainUI.formImageSave, 0, 0);
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
                FormMainUI.m_dicFormCamShows = StaticFun.UIConfig.InitShowUI(contextMenuStrip1, tableLayoutPanel3);
                panel1.ContextMenuStrip = contextMenuStrip1;
                panel2.ContextMenuStrip = contextMenuStrip1;
                panel3.ContextMenuStrip = contextMenuStrip1;
                panel4.ContextMenuStrip = contextMenuStrip1;
                panel5.ContextMenuStrip = contextMenuStrip1;
                panel6.ContextMenuStrip = contextMenuStrip1;
                //加载消息显示
                FormMainUI.formShowResult.TopLevel = false;
                FormMainUI.formShowResult.Visible = true;
                FormMainUI.formShowResult.Dock = DockStyle.Fill;
                this.panel13.Controls.Clear();
                this.panel13.Controls.Add(FormMainUI.formShowResult);
                //运行按钮
                FormMainUI.formRun.Visible = true;
                FormMainUI.formRun.Dock = DockStyle.Fill;
                this.tLPanel.Controls.Add(FormMainUI.formRun, 0, 1);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void Show4_Load(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            StaticFun.UIConfig.ConfigShowItems(sender, e, ref FormMainUI.m_dicFormCamShows);
        }
    }
}
