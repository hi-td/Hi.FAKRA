using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using StaticFun;

namespace VisionPlatform
{
    public partial class Show2 : Form
    {
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
            FormMainUI.m_dicFormCamShows = StaticFun.UIConfig.InitShowUI(contextMenuStrip1, tLPanel_CamShow);
            panel1.ContextMenuStrip = contextMenuStrip1;
            panel2.ContextMenuStrip = contextMenuStrip1;
            //加载检测结果显示
            FormMainUI.formShowResult.TopLevel = false;
            FormMainUI.formShowResult.Visible = true;
            FormMainUI.formShowResult.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Clear();
            this.splitContainer1.Panel2.Controls.Add(FormMainUI.formShowResult);
            //运行按钮
            FormMainUI.formRun.Visible = true;
            FormMainUI.formRun.Dock = DockStyle.Fill;
            this.tableLayoutPanel7.Controls.Add(FormMainUI.formRun, 0, 0);
        }
        private void Show2_Load(object sender, EventArgs e)
        {
            // CheckSave();
        }

        private void Show2_SizeChanged(object sender, EventArgs e)
        {
           StaticFun.Run.Zoom();
        }

    }
}
