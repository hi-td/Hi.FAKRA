using EnumData;
using Newtonsoft.Json;
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
    public partial class Featureselection : Form
    {
        FormMainUI mainUI;
        public Featureselection(FormMainUI formMain)
        {
            InitializeComponent();
            mainUI = formMain;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioBut_1Cam.Checked == false && radioBut_2Cam.Checked == false && radioBut_2_2Cam.Checked == false)
            {
                MessageBox.Show("请先选择工作模式！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                try
                {
                    TMData.Feature feature = new TMData.Feature();
                    feature.tmCheckList.SkinWeld = checkBox_SkinWeld.Checked;
                    feature.tmCheckList.SkinPos = checkBox_SkinPos.Checked;
                    feature.tmCheckList.LineWeld = checkBox_LineWeld.Checked;
                    feature.tmCheckList.LinePos = checkBox_LinePos.Checked;
                    feature.tmCheckList.LineSide = checkBox_LineSide.Checked;
                    feature._1Cam = radioBut_1Cam.Checked;
                    feature._2Cam = radioBut_2Cam.Checked;
                    feature._2_2Cam = radioBut_2_2Cam.Checked;
                    TMData_Serializer._feature.Feature = feature;
                    var json = JsonConvert.SerializeObject(feature);
                    json = Registered.DESEncrypt(json);
                    // string tup_Path = System.AppDomain.CurrentDomain.BaseDirectory + "Runtime\\Glpeizi.json";
                    System.IO.File.WriteAllText(GlobalPath.SavePath.TMConfigPath, json);
                    MessageBox.Show("保存成功！退出软件并重启生效！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //关闭所有进程，退出软件并重启
                    //Application.ExitThread();
                    //Thread.Sleep(1000);
                    //Restart();
                }
                catch (Exception)
                {
                    MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Featureselection_Load(object sender, EventArgs e)
        {
            try
            {
                string strData = System.IO.File.ReadAllText(GlobalPath.SavePath.TMConfigPath);
                strData = Registered.DESDecrypt(strData);
                var DynamicObject = JsonConvert.DeserializeObject<TMData.Feature>(strData);
                TMData.Feature feature = DynamicObject;
                checkBox_SkinWeld.Checked = feature.tmCheckList.SkinWeld;
                checkBox_SkinPos.Checked = feature.tmCheckList.SkinPos;
                checkBox_LineWeld.Checked = feature.tmCheckList.LineWeld;
                checkBox_LinePos.Checked = feature.tmCheckList.LinePos;
                checkBox_LineSide.Checked = feature.tmCheckList.LineSide;
                radioBut_1Cam.Checked = feature._1Cam;
                radioBut_2Cam.Checked = feature._2Cam;
                radioBut_2_2Cam.Checked = feature._2_2Cam;
            }
            catch (Exception)
            {
                MessageBox.Show("读取失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    mainUI.Text = textBox1.Text;
                }));
                TMData.Name name = new TMData.Name();
                name.name = textBox1.Text;
                var json = JsonConvert.SerializeObject(name);
                json = Registered.DESEncrypt(json);
                string tup_Path = System.AppDomain.CurrentDomain.BaseDirectory + "Name.json";
                System.IO.File.WriteAllText(tup_Path, json);
                MessageBox.Show("修改保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("修改保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioButton_One1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_SkinWeld_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
