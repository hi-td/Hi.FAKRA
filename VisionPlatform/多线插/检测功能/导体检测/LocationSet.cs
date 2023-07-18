using BaseData;
using System;
using System.Windows.Forms;

namespace VisionPlatform
{
    public partial class LocationSet : UserControl
    {
        Conductor form;
        bool bLoad = false;                  //是否在加载数据中
        public LocationSet(Conductor form)
        {
            InitializeComponent();
            this.form = form;
        }
        
        public LocationSetMeasure InitParam()
        {
            LocationSetMeasure param = new LocationSetMeasure();
            try
            {
                param.nSpace = (int)numUp_Space.Value;
                trackBar_Space.Value = (int)numUp_Space.Value;
                param.nWidth = (int)numUp_Width.Value;
                trackBar_Width.Value = (int)numUp_Width.Value;
                param.nHeight = (int)numUp_Height.Value;
                trackBar_Height.Value = (int)numUp_Height.Value;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return param;
        }

        public void LoadParam(LocationSetMeasure param)
        {
            try
            {
                bLoad = true;
                numUp_Space.Value = param.nSpace;
                trackBar_Space.Value = param.nSpace;
                numUp_Width.Value = param.nWidth;
                trackBar_Width.Value = param.nWidth;
                numUp_Height.Value = param.nHeight;
                trackBar_Height.Value = param.nHeight;
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
                //检测函数
                form.LocationSetValueChange(sender, e);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void trackBar__Space_Scroll(object sender, EventArgs e)
        {
            numUp_Space.Value = trackBar_Space.Value;
        }

        private void trackBar_Width_Scroll(object sender, EventArgs e)
        {
            numUp_Width.Value = trackBar_Width.Value;
        }

        private void trackBar_Height_Scroll(object sender, EventArgs e)
        {
            numUp_Height.Value = trackBar_Height.Value;
        }

        
    }
}
