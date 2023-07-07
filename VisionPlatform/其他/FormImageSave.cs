/***********************************************************
* CLR版本：4.0.30319.42000
* 类 名 称：FormImageSave
* 机器名称：WQ
* 命名空间：VisionPlatform
* 文 件 名：FormImageSave
* 创建时间：2022/7/26 13:41:55
* 作    者： WQ
* 公    司：HaiLan Intelligent
* 说   明：
* 修改时间：
* 修 改 人：
* 修改说明：
* 深圳市海蓝智能科技有限公司  2021  保留所有权利.
***********************************************************/
using BaseData;
using StaticFun;
using System;
using System.Windows.Forms;

namespace VisionPlatform
{
    public partial class FormImageSave : Form
    {
        public FormImageSave()
        {
            InitializeComponent();
            this.TopLevel = false;
        }

        private void FormImageSave_Load(object sender, EventArgs e)
        {
            try
            {
                OtherConfig otherConfig = GlobalData.Config._InitConfig.otherConfig;
                checkBox_ShowData.Checked = otherConfig.bDisplay;
                checkBox_SaveOrgImageOK.Checked = otherConfig.bOrgOK;
                checkBox_SaveResultImgOK.Checked = otherConfig.bResultOK;
                checkBox_SaveOrgImageNG.Checked = otherConfig.bOrgNG;
                checkBox_SaveResultImgNG.Checked = otherConfig.bResultNG;
            }
            catch (Exception)
            {
            }
        }

        private void Save(object sender, EventArgs e)
        {
            try
            {
                OtherConfig otherConfig = GlobalData.Config._InitConfig.otherConfig;
                otherConfig.bDisplay = checkBox_ShowData.Checked;
                otherConfig.bOrgOK = checkBox_SaveOrgImageOK.Checked;
                otherConfig.bResultOK = checkBox_SaveResultImgOK.Checked;
                otherConfig.bOrgNG = checkBox_SaveOrgImageNG.Checked;
                otherConfig.bResultNG = checkBox_SaveResultImgNG.Checked;
                GlobalData.Config._InitConfig.otherConfig = otherConfig;
                SaveData.SaveOtherConfig(GlobalData.Config._InitConfig.otherConfig);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
