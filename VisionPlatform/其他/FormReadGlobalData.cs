//using Mewtocol;
using CamSDK;
using Chustange.Functional;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VisionPlatform
{
    public partial class FormReadGlobalData : Form
    {
        string selectFile;                   //选中的文件
        Label tsLabel_SerialName;
        public FormReadGlobalData(Label lable_SerialName)
        {
            InitializeComponent();
            tsLabel_SerialName = lable_SerialName;
            StaticFun.LoadConfig.LoadJsonData(this.listView1);
        }

        private void FormReadGlobalData_Load(object sender, EventArgs e)
        {
            StaticFun.LoadConfig.LoadJsonData(this.listView1);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                selectFile = listView1.SelectedItems[0].SubItems[1].Text;
                label_FileName.Text = selectFile;
            }
        }

        private void but_import_Click(object sender, EventArgs e)
        {
            try
            {
                //保存最新的序列化文件名称
               // System.IO.File.WriteAllText(GlobalPath.SavePath.NewestFile, JsonConvert.SerializeObject(selectFile));
                StaticFun.LoadConfig.LoadTMData(selectFile);
                //将导入的序列化参数名称显示到主页面
                tsLabel_SerialName.Text = selectFile;
                this.Close();
            }
            catch (Exception ex)
            {
                (ex.Message + ex.StackTrace).ToLog();
                MessageBox.Show(ex.Message);
            }
        }

        private void but_Delete_Click(object sender, EventArgs e)
        {
            StaticFun.DelectData.DelectJsonFile(selectFile, this.listView1);
        }
    }
}
