using Chustange.Functional;
using StaticFun;
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
    public partial class FormShowResult : Form
    {
        public FormShowResult()
        {
            InitializeComponent();
            MessageFun.GetRichText(richTextBox);
        }
        public void ShowResult(TMData.InspectResult inspectResult)
        {
            try
            {
                Action method = delegate()
                {
                    //添加序号
                    int m = this.listView1.Items.Count;
                    listView1.Items.Add(m.ToString());
                    //添加检测项和检测结果
                    Dictionary<string, string> item = inspectResult.outcome;
                    if (0 == item.Count)
                    {
                        listView1.Items[m].BackColor = Color.Red;
                    }
                    else
                    {
                        foreach (string str in item.Keys)
                        {
                            listView1.Items[m].SubItems.Add(str);
                            listView1.Items[m].SubItems.Add(item[str]);
                            if (item[str].Contains("NG"))
                            {
                                listView1.Items[m].BackColor = Color.Red;
                            }
                        }
                    }
                    //添加采图时间
                    listView1.Items[m].SubItems.Add(inspectResult.GrabTime.ToString());
                    //添加检测时间
                    listView1.Items[m].SubItems.Add(inspectResult.InspectTime.ToString());
                    //添加总时间
                    listView1.Items[m].SubItems.Add((inspectResult.GrabTime + inspectResult.InspectTime).ToString());
                    this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
                    if (this.listView1.Items.Count > 100)
                    {
                        this.listView1.Items.Clear();
                    }                  
                };
                Invoke(method);
            }
            catch (Exception ex)
            {
                ("显示检测结果错误：" + ex.ToString()).ToLog();
                StaticFun.MessageFun.ShowMessage("显示检测结果错误：" + ex.ToString());
            }
            inspectResult = new TMData.InspectResult();
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void 清除_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
        }
    }
}
