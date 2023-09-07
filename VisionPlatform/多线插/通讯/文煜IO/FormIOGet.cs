using EnumData;
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
using static VisionPlatform.TMData;

namespace VisionPlatform
{
    public partial class FormIOGet : Form
    {
        private List<int> listIO;
        TMData.CamInspectItem m_camItem;
        bool is_Load = false;
        TMData.SurfaceType surfaceType;
        TMData.DetectionType type;

        public FormIOGet(int cam,int sub_cam, TMData.InspectItem inspectItem, TMData.SurfaceType surfaceType = default, TMData.DetectionType type = default)
        {
            InitializeComponent();
            m_camItem.cam = cam;
            m_camItem.item = inspectItem;
            m_camItem.sub_cam = sub_cam;
            this.surfaceType = surfaceType;
            this.type = type;
            InitUI();
            LoadDate();
            
        }
        private void InitUI()
        {
            try
            {
                int nRows = 0;
                int nCols = 4;
                tLPanel.ColumnCount = 4;
                if (GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU8)
                {
                    tLPanel.RowCount = 2;
                    nRows = 2;
                    listIO = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 };
                }
                else if (GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU232)
                {
                    tLPanel.RowCount = 3;
                    nRows = 3;
                    listIO = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                }
                else if(GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU16)
                {
                    tLPanel.RowCount = 4;
                    nRows = 4;
                    listIO = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                }
                tLPanel.RowStyles.Clear();
                tLPanel.ColumnStyles.Clear();
                // 计数
                int n = 0;
                // 设置行
                for (int row = 0; row < nRows; row++)
                {
                    tLPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f/nRows));
                    // 设置列
                    for (int col = 0; col < nCols; col++)
                    {
                        tLPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
                        TableLayoutPanel tLPanel_io = new TableLayoutPanel();
                        tLPanel_io.Dock = DockStyle.Fill;
                        tLPanel_io.RowCount = 1;
                        tLPanel_io.ColumnCount = 2;
                        tLPanel_io.RowStyles.Clear();
                        tLPanel_io.ColumnStyles.Clear();
                        tLPanel_io.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                        for (int col_io = 0; col_io < 2; col_io++)
                        {
                            tLPanel_io.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                            if (col_io == 0)
                            {
                                Label label = new Label();
                                label.Text = $"DI{n.ToString("D2")}:";
                                label.Dock = DockStyle.Fill;
                                label.TextAlign = ContentAlignment.MiddleCenter;
                                tLPanel_io.Controls.Add(label, col_io, 1);
                            }
                            else
                            {
                                CheckBox checkBox = new CheckBox();
                                checkBox.Name = $"Cb_DI{n.ToString("D2")}";
                                checkBox.Dock = DockStyle.Fill;
                                checkBox.TextAlign = ContentAlignment.MiddleCenter;
                                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                                tLPanel_io.Controls.Add(checkBox, col_io, 1);
                            }
                        }
                        tLPanel.Controls.Add(tLPanel_io, col, row);
                        n++;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void LoadDate()
        {
            try
            {
                is_Load = true;
                for (int n = 0; n < TMData_Serializer._COMConfig.listIOSet.Count; n++)
                {
                    IOSet io = TMData_Serializer._COMConfig.listIOSet[n];
                    if (io.camItem.cam == m_camItem.cam && io.camItem.item == m_camItem.item &&
                        io.camItem.surfaceType == surfaceType && io.camItem.type == type && io.camItem.sub_cam == m_camItem.sub_cam)
                    {
                        long nRead = io.read;
                        int nLen = 8;
                        if (GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU16)
                        {
                            nLen = 16;
                        }
                        else if (GlobalData.Config._InitConfig.initConfig.comMode.IO == EnumData.IO.WENYU232)
                        {
                            nLen = 12;
                        }
                        string binaryString = Convert.ToString(nRead, 2).PadLeft(nLen, '0');
                        int num = 0;
                        for (int i = binaryString.Length - 1; i >= 0; i--)
                        {
                            char c = binaryString[i];
                            CheckBox checkBox = this.Controls.Find($"Cb_DI{num.ToString("D2")}", true).FirstOrDefault() as CheckBox;
                            if (c == '0' && checkBox != null)
                            {
                                checkBox.Checked = true;
                            }
                            else
                            {
                                checkBox.Checked = false;
                            }
                            num++;
                        }
                    }

                }


                is_Load = false;

            }
            catch (Exception ex)
            {
                ex.ToString();
                is_Load = false;
            }
        }

        private void ChangeIO(CheckBox checkBox, int nNum)
        {
            string[] subStr = checkBox.Name.Split('I');
            listIO[int.Parse(subStr[1])] = nNum;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                ChangeIO(checkBox, 0);
            }
            else
            {
                ChangeIO(checkBox, 1);
            }
        }

        private bool SaveIOGet()
        {
            try
            {
                int decimalValue = 0;
                int baseValue = 1;
                for (int i = 0; i < listIO.Count; i++)
                {
                    if (listIO[i] == 1)
                    {
                        decimalValue += baseValue;
                    }
                    baseValue *= 2;
                }
                MessageFun.ShowMessage(decimalValue.ToString());
                IOSet ioSet = new IOSet();
                ioSet.camItem = m_camItem;
                ioSet.read = decimalValue;
                ioSet.camItem.surfaceType = this.surfaceType;
                ioSet.camItem.type = this.type;
                foreach (IOSet io in TMData_Serializer._COMConfig.listIOSet)
                {
                    if (io.camItem.cam == m_camItem.cam && io.camItem.item == m_camItem.item &&
                        io.camItem.surfaceType == surfaceType && io.camItem.type == type && io.camItem.sub_cam == m_camItem.sub_cam)
                    {
                        ioSet.send.bSendOK = io.send.bSendOK;
                        ioSet.send.bSendNG = io.send.bSendNG;
                        ioSet.send.sendOK = io.send.sendOK;
                        ioSet.send.sendNG = io.send.sendNG;
                        ioSet.send.bSendInvert = io.send.bSendInvert;
                        ioSet.send.nSleep = io.send.nSleep;
                    }
                }
                SaveData.SaveIOConfig(ioSet);
                return true;
            }
            catch (Exception ex)
            {
                MessageFun.ShowMessage(ex.ToString());
                return false;
            }
        }

        private void but_Save_Click(object sender, EventArgs e)
        {

            if (SaveIOGet())
            {
                this.Close();
            }
            else
            {
                MessageFun.ShowMessage("保存配置出错!");
            }
        }
    }
}
