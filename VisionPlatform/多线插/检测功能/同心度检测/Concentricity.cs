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
    public partial class Concentricity : UserControl
    {
        public FitCircle OuterCircle;        //外导体圆
        public FitCircle InsulationCircle;   //绝缘体圆
        public FitCircle InnerCircle;        //内导体圆
        private TMFunction TMFun;            //当前检测函数
        private Function Fun;                //当前底层函数
        string str_CamSer;                   //当前相机序列号
        int m_ncam;                          //当前相机
        bool bLoad = false;                  //是否在加载数据中
        TMData.ConcentricityType type;       //同心度类型
        public event FitCircleValueChangedEventHandler FitCircleValueChanged;
        public void FitCircleValueChange(object sender, EventArgs e) => FitCircleValueChanged?.Invoke(sender, e);
        /// <summary>
        /// 同心度
        /// </summary>
        /// <param name="ncam"></param>  相机
        /// <param name="type"></param>   同心度类型
        public Concentricity(int ncam, TMData.ConcentricityType type)
        {
            InitializeComponent();
            if (type == TMData.ConcentricityType.male)
            {
                tabPage_male.Parent = tabControl1;
                tabPage_female.Parent = null;
            }
            else
            {
                tabPage_male.Parent = null;
                tabPage_female.Parent = tabControl1;
            }
            m_ncam = ncam;
            InitUI();
            UIConfig.RefreshFun(ncam, "", ref Fun, ref TMFun, ref str_CamSer);
            LoadParam(type);
            FitCircleValueChanged += Inspect;
        }

        private void InitUI()
        {
            try
            {
                OuterCircle = new FitCircle(this)
                {
                    Visible = true,
                    Dock = DockStyle.Fill,
                };
                tLPanel_OuterCircle.Controls.Add(OuterCircle, 1, 1);
                InsulationCircle = new FitCircle(this)
                {
                    Visible = true,
                    Dock = DockStyle.Fill,
                };
                tLPanel_InsulationCircle.Controls.Add(InsulationCircle, 1, 1);
                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private TMData.ConcentricityParam InitParam()
        {
            TMData.ConcentricityParam param = new TMData.ConcentricityParam();
            try
            {
                //外导体圆
                param.outerCircle.nRadius = (int)numUpD_OuterRadius.Value;
                trackBar_OuterRadius.Value = (int)numUpD_OuterRadius.Value;
                param.outerCircle.EPMeasure = OuterCircle.InitParam();
                //绝缘体圆
                param.bInsultationCircle = checkBox_Insulation.Checked;
                param.insulationCircle.nRadius = (int)numUpD_InsulationRadius.Value;
                trackBar_InsulationRadius.Value = (int)numUpD_InsulationRadius.Value;
                param.insulationCircle.EPMeasure = InsulationCircle.InitParam();
                //内导体圆
                param.femaleCircle.nRadiusROI = (int)numUpD_InnerRadius.Value;
                trackBar_InnerRadius.Value = (int)numUpD_InnerRadius.Value;
                param.femaleCircle.nThd = (int)numUpD_InnerThd.Value;
                trackBar_InnerThd.Value = (int)numUpD_InnerThd.Value;
                param.femaleCircle.dRadius = (double)numUpD_PreInnerRadius.Value;
                param.femaleCircle.dRadiusLow = (double)numUpD_PreInnerRadiusLow.Value;
                param.femaleCircle.dRadiusHigh = (double)numUpD_PreInnerRadiusHigh.Value;
                label_PreInnerRadiusHigh.Text = Math.Round((numUpD_PreInnerRadius.Value * numUpD_PreInnerRadiusHigh.Value),2).ToString();
                label_PreInnerRadiusLow.Text = Math.Round((numUpD_PreInnerRadius.Value * numUpD_PreInnerRadiusLow.Value),2).ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return param;
        }

        private void LoadParam(TMData.ConcentricityType type)
        {
            ConcentricityParam param = new ConcentricityParam();
            try
            {
                bLoad = true;
                if (type == ConcentricityType.male)
                {
                    param = TMData_Serializer._globalData.dicConcentricity[m_ncam][0];
                }
                else
                {
                    param = TMData_Serializer._globalData.dicConcentricity[m_ncam][1];
                }
                //外导体圆
                numUpD_OuterRadius.Value = (decimal)param.outerCircle.nRadius;
                trackBar_OuterRadius.Value = param.outerCircle.nRadius;
                OuterCircle.LoadParam(param.outerCircle.EPMeasure);
                //绝缘体圆
                checkBox_Insulation.Checked = param.bInsultationCircle;
                tLPanel_InsulationCircle.Enabled = param.bInsultationCircle;
                numUpD_InsulationRadius.Value = (decimal)param.insulationCircle.nRadius;
                trackBar_InsulationRadius.Value = param.insulationCircle.nRadius;
                InsulationCircle.LoadParam(param.insulationCircle.EPMeasure);
                //内导体圆
                //母头
                numUpD_InnerRadius.Value = (decimal)(param.femaleCircle.nRadiusROI);
                trackBar_InnerRadius.Value = param.femaleCircle.nRadiusROI;
                numUpD_InnerThd.Value = (decimal)param.femaleCircle.nThd;
                trackBar_InnerThd.Value = param.femaleCircle.nThd;
                numUpD_PreInnerRadius.Value = (decimal)param.femaleCircle.dRadius;
                numUpD_PreInnerRadiusLow.Value = (decimal)param.femaleCircle.dRadiusLow;
                numUpD_PreInnerRadiusHigh.Value = (decimal)param.femaleCircle.dRadiusHigh;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            bLoad = false;
        }

        private void Inspect(object sender, EventArgs e)
        {
            try
            {
                if(bLoad) { return; }
                TMData.ConcentricityParam param = InitParam();
                TMFun.Concentricity(param, true, out ConcentricityResult result);
                label_PreInnerRadius.Text = result.innerCircle.dRadius.ToString();
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.ToString());
            }
        }

        private void trackBar_OuterRadius_Scroll(object sender, EventArgs e)
        {
            numUpD_OuterRadius.Value = trackBar_OuterRadius.Value;
        }

        private void trackBar_InsulationRadius_Scroll(object sender, EventArgs e)
        {
            numUpD_InsulationRadius.Value = trackBar_InsulationRadius.Value;
        }

        private void trackBar_InnerRadius_Scroll(object sender, EventArgs e)
        {
            numUpD_InnerRadius.Value = trackBar_InnerRadius.Value;
        }
        private void trackBar_InnerThd_Scroll(object sender, EventArgs e)
        {
            numUpD_InnerThd.Value = trackBar_InnerThd.Value;
        }
        private void but_SaveData_Click(object sender, EventArgs e)
        {
            int n = 0;
            string str = "公头";
            try
            {
                if (this.type == ConcentricityType.female)
                {
                    n = 1;
                    str = "母头";
                }
                if (null != TMData_Serializer._globalData.dicConcentricity && TMData_Serializer._globalData.dicConcentricity.ContainsKey(m_ncam))
                {
                    TMData_Serializer._globalData.dicConcentricity[m_ncam][n] = InitParam();
                }
                else
                {
                    ConcentricityParam[] param = new ConcentricityParam[2];
                    param[n] = InitParam();
                    TMData_Serializer._globalData.dicConcentricity.Add(m_ncam, param);
                }
                MessageBox.Show("【同心度-" + str + "】数据保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("【同心度-" + str + "】数据保存失败！");
            }
        }

        private void checkBox_Insulation_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Insulation.Checked)
            {
                tLPanel_InsulationCircle.Enabled = true;
            }
            else
            {
                tLPanel_InsulationCircle.Enabled = false;
            }
        }

       
    }
}
