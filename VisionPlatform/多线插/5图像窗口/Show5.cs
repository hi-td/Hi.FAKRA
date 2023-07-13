using System;
using System.Windows.Forms;

namespace VisionPlatform
{
    public partial class Show5 : Form
    {
        public Show5()
        {
            InitializeComponent();
            _InitFun();
            FormMainUI.formImageSave.RefeshUI(2);
            tableLayoutPanel1.Controls.Add(FormMainUI.formImageSave, 1, 0);
        }
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
                FormMainUI.m_dicFormCamShows = StaticFun.UIConfig.InitShowUI(contextMenuStrip1, tLPanel_CamShow);
                panel1.ContextMenuStrip = contextMenuStrip1;
                panel2.ContextMenuStrip = contextMenuStrip1;
                panel3.ContextMenuStrip = contextMenuStrip1;
                panel4.ContextMenuStrip = contextMenuStrip1;
                panel5.ContextMenuStrip = contextMenuStrip1;
                //加载消息显示
                FormMainUI.formShowResult.TopLevel = false;
                FormMainUI.formShowResult.Visible = true;
                FormMainUI.formShowResult.Dock = DockStyle.Fill;
                this.tableLayoutPanel2.Controls.Add(FormMainUI.formShowResult,0,1);
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
