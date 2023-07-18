using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Chustange.Functional;
using static VisionPlatform.TMData;
using StaticFun;

namespace VisionPlatform
{
    public partial class FormTMCheckItem : Form
    {
        int m_cam;
        int sub_cam;
        string selctModel;
        public FormTMCheckItem(int cam, int sub_cam, string c)
        {
            InitializeComponent();
            m_cam = cam;
            this.sub_cam = sub_cam;
            selctModel = c;
        }

        private void but_Set_Click(object sender, EventArgs e)
        {
            //保存打端检测项
            try
            {
                string a = m_cam.ToString() + sub_cam.ToString();
                TMData.TMCheckItem tmCheckItem = new TMData.TMCheckItem();
                tmCheckItem.SkinWeld = checkBox_SkinWeld.Checked;
                tmCheckItem.SkinPos = checkBox_SkinPos.Checked;
                tmCheckItem.LineWeld = checkBox_LineWeld.Checked;
                tmCheckItem.LinePos = checkBox_LinePos.Checked;
                tmCheckItem.LineSide = checkBox_LineSide.Checked;
                tmCheckItem.Waterproof = checkBox_Waterproof.Checked;
                tmCheckItem.LineColor = checkBox_LineColor.Checked;
                if (selctModel == "")
                {
                    MessageBox.Show("未选择模板！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
                if (TMData_Serializer._globalData.dicTMCheckList.ContainsKey(a))
                {

                    if (TMData_Serializer._globalData.dicTMCheckList[a].ContainsKey(selctModel))
                    {
                        TMData_Serializer._globalData.dicTMCheckList[a][selctModel] = tmCheckItem;
                    }
                    else
                    {
                        TMData_Serializer._globalData.dicTMCheckList[a].Add(selctModel, tmCheckItem);
                    }
                }
                else
                {
                    Dictionary<string, TMCheckItem> tm = new();
                    tm.Add(selctModel, tmCheckItem);
                    TMData_Serializer._globalData.dicTMCheckList.Add(a, tm);
                }
                var TMcheckList = JsonConvert.SerializeObject(TMData_Serializer._globalData.dicTMCheckList);
                TMcheckList = Registered.DESEncrypt(TMcheckList);
                System.IO.File.WriteAllText(GlobalPath.SavePath.TMCheckListPath, TMcheckList);
                MessageBox.Show("保存成功！");
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void FormTMCheckItem_Load(object sender, EventArgs e)
        {
            try
            {
                string a = m_cam.ToString() + sub_cam.ToString(); ;
                if (TMData_Serializer._globalData.dicTMCheckList.ContainsKey(a))
                {
                    if (TMData_Serializer._globalData.dicTMCheckList[a].ContainsKey(selctModel))
                    {
                        TMData.TMCheckItem checkItem = TMData_Serializer._globalData.dicTMCheckList[a][selctModel];
                        checkBox_SkinWeld.Checked = checkItem.SkinWeld;
                        checkBox_SkinPos.Checked = checkItem.SkinPos;
                        checkBox_LineWeld.Checked = checkItem.LineWeld;
                        checkBox_LinePos.Checked = checkItem.LinePos;
                        checkBox_LineSide.Checked = checkItem.LineSide;
                        checkBox_Waterproof.Checked = checkItem.Waterproof;
                        checkBox_LineColor.Checked = checkItem.LineColor;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageFun.ShowMessage(ex.ToString());
                ex.ToString().ToLog();
            }
        }
    }
}
