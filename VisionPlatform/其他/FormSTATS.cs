/***********************************************************
* CLR版本：4.0.30319.42000
* 类 名 称：FormSTATS
* 机器名称：WQ
* 命名空间：VisionPlatform.多线插.显示界面
* 文 件 名：FormSTATS
* 创建时间：2022/3/4 11:44:05
* 作    者： WQ
* 公    司：HaiLan Intelligent
* 说   明：
* 修改时间：
* 修 改 人：
* 修改说明：
* 深圳市海蓝智能科技有限公司  2021  保留所有权利.
***********************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionPlatform
{
    public partial class FormSTATS : Form
    {
        public int m_cam;
        public string m_CheckItem;
        public FormSTATS(int cam, string strCheckItem)
        {
            InitializeComponent();
            this.label_ItemName.Text = "相机" + cam + ":" + strCheckItem;
            m_cam = cam;
            m_CheckItem = strCheckItem;
        }

        /// <summary>
        /// 数据统计
        /// 
        /// </summary>
        /// <param name="ok"></param> 增加的OK数
        /// <param name="ng"></param>增加的NG数
        /// <param name="time"></param> 检测用时
        public void Add(int ok, int ng, double time)
        {
            try
            {
                int orgOK = int.Parse(this.text_OK.Text);
                int nTotalOK = orgOK + ok;
                int orgNG = int.Parse(this.text_NG.Text);
                int nTotalNG = orgNG + ng;
                int nTotal = nTotalOK + nTotalNG;
                BeginInvoke(new Action(() =>
                {
                    label_TimeSpan.Text = time.ToString() + "ms";
                    this.text_OK.Text = nTotalOK.ToString();
                    this.text_NG.Text = nTotalNG.ToString();
                    text_Total.Text = nTotal.ToString();
                    text_Yield.Text = (nTotalOK * 1.0 / nTotal).ToString("P"); //良率
                }));
            }
            catch (SystemException ex)
            {
                ex.ToString();
            }
        }

        private void 清除数据_Click(object sender, EventArgs e)
        {
            this.text_OK.Text = "0";
            this.text_NG.Text = "0";
            text_Total.Text = "0";
            text_Yield.Text = "0";
            label_TimeSpan.Text = "00ms";
        }
    }
}
