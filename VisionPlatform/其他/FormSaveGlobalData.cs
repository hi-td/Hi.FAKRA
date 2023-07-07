using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;
using Chustange.Functional;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using Mewtocol;

namespace VisionPlatform
{
    public partial class FormSaveGlobalData : Form
    {
        string selectFile;
        public FormSaveGlobalData()
        {
            InitializeComponent();
            StaticFun.LoadConfig.LoadJsonData(this.listView1);
        }

        private void but_SaveGlobalData_Click(object sender, EventArgs e)
        {
            try
            {
                string strFileName = textBox_SerialDataName.Text;
                if ("" == strFileName)
                {
                    MessageBox.Show("请输入要保存的文件名称。");
                    return;
                }
                if(strFileName.Contains("\r\n") || strFileName.Contains(" "))
                {
                    MessageBox.Show("文件名跨行或有空格，请重新输入。");
                    return;
                }
                //保存最新的序列化文件名称
                System.IO.File.WriteAllText(GlobalPath.SavePath.NewestFile, JsonConvert.SerializeObject(strFileName));
                //保存路径 + 序列化文件名称
                string savePath = GlobalPath.SavePath.GlobalDataPath;
                string save = savePath + strFileName + ".json";
                //判断是否已经存在相同的文件名称
                if (System.IO.File.Exists(save))
                {
                    DialogResult dr = MessageBox.Show("文件已经存在，是否重盖？", "提示：", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr != DialogResult.OK)
                    {
                        return;
                    }
                }
                //将模板图片移动到新建的json文件目录下
                MoveFiles(GlobalPath.SavePath.ModelImagePath, strFileName);
                //将模板移动到新建的json文件目录下
                MoveFiles(GlobalPath.SavePath.ModelPath, strFileName);
                //C#对象转Json
                var json = JsonConvert.SerializeObject(TMData_Serializer._globalData);
                System.IO.File.WriteAllText(save, json);
                StaticFun.LoadConfig.LoadJsonData(this.listView1);
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }

        }

        //将模板相关文件移动到新建立的json名称文件目录下
        private void MoveFiles(string destFilesPath, string fileName)
        {
            try
            {
                bool bflag = true;
                string newPath = System.IO.Path.Combine(destFilesPath, fileName);
                bool pathExists = System.IO.Directory.Exists(newPath);
                if (!pathExists)
                {
                    Directory.CreateDirectory(newPath);
                }
                DirectoryInfo theFolder = new DirectoryInfo(destFilesPath);
                DirectoryInfo[] fileInfo = theFolder.GetDirectories();
                if (0 == fileInfo.Length)
                {
                    StaticFun.MessageFun.ShowMessage("无模板文件，请先示教");
                    return;
                }
                DirectoryInfo theFolder1 = new DirectoryInfo(newPath);
                foreach (DirectoryInfo NextFile in fileInfo) //遍历文件
                {
                    if (NextFile.Name.Contains("相机1") || NextFile.Name.Contains("相机2") || NextFile.Name.Contains("相机3") || NextFile.Name.Contains("相机4"))
                    {
                        foreach (DirectoryInfo NextFile1 in theFolder1.GetDirectories()) //遍历文件
                        {
                            DirectoryInfo thFolder2 = new DirectoryInfo(Path.Combine(destFilesPath, NextFile.Name));
                            if (NextFile1.Name.Contains(NextFile.Name))
                            {
                                foreach (FileInfo fileInfo1 in thFolder2.GetFiles())
                                {
                                    File.Copy(Path.Combine(Path.Combine(destFilesPath, NextFile.Name), fileInfo1.Name), Path.Combine(newPath, Path.Combine(NextFile.Name, fileInfo1.Name)), true);
                                }
                                NextFile.Delete(true);
                                bflag = false;
                                break;
                            }
                        }
                        if (bflag)
                        {
                            Directory.Move(Path.Combine(destFilesPath, NextFile.Name), Path.Combine(newPath, NextFile.Name));
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                (ex.Message + ex.StackTrace).ToLog();
                StaticFun.MessageFun.ShowMessage(ex.ToString());
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string str = listView1.SelectedItems[0].SubItems[1].Text;
                textBox_SerialDataName.Text = str;
                selectFile = str;
            }
            catch(Exception ex)
            {
                (ex.Message + ex.StackTrace).ToLog();
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StaticFun.DelectData.DelectJsonFile(selectFile, this.listView1);
        }
    }
}
