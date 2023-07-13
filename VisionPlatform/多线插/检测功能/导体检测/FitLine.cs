using BaseData;
using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VisionPlatform
{
    public partial class FitLine : UserControl
    {
        Concentricity form;
        bool bLoad = false;
        public FitLine(Concentricity form)
        {
            InitializeComponent();
            this.form = form;
        }

        public BaseData.EdgePointMeasure InitParam()
        {
            BaseData.EdgePointMeasure param = new EdgePointMeasure();
            try
            {
                param.nMeasureLen1 = (int)numUpD_Len2.Value;
                trackBar_Len2.Value = (int)numUpD_Len2.Value;
                param.dMeasureLen2 = 5;
                param.nMeasureThd = (int)numUpD_Thd.Value;
                trackBar_Thd.Value = (int)numUpD_Thd.Value;
                switch (comBox_Trans.SelectedIndex)
                {
                    case 0:
                        param.strTransition = "negative";
                        break;
                    case 1:
                        param.strTransition = "positive";
                        break;
                    default:
                        comBox_Trans.SelectedIndex = 0;
                        param.strTransition = "negative";
                        break;
                }
                switch (comBox_EdgePoint.SelectedIndex)
                {
                    case 0:
                        param.strSelect = "first";
                        break;
                    case 1:
                        param.strSelect = "last";
                        break;
                    default:
                        comBox_EdgePoint.SelectedIndex = 0;
                        param.strSelect = "first";
                        break;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return param;
        }

        public void LoadParam(BaseData.EdgePointMeasure param)
        {
            try
            {
                bLoad = true;
                numUpD_Len2.Value = param.nMeasureLen1;
                trackBar_Len2.Value = param.nMeasureLen1;
                numUpD_Thd.Value = param.nMeasureThd;
                trackBar_Thd.Value = param.nMeasureThd;
                if (param.strTransition == "negative")
                {
                    comBox_Trans.SelectedIndex = 0;
                }
                else if(param.strTransition == "positive")
                {
                    comBox_Trans.SelectedIndex = 1;
                }
                if (param.strSelect == "first")
                {
                    comBox_EdgePoint.SelectedIndex = 0;
                }
                else if (param.strSelect == "last")
                {
                    comBox_EdgePoint.SelectedIndex = 1;
                }
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
                if (bLoad) return;
                form.FitCircleValueChange(sender, e);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void trackBar_Len2_Scroll(object sender, EventArgs e)
        {
            numUpD_Len2.Value = trackBar_Len2.Value;
        }

        private void trackBar_Thd_Scroll(object sender, EventArgs e)
        {
            numUpD_Thd.Value = trackBar_Thd.Value;
        }
    }
}
