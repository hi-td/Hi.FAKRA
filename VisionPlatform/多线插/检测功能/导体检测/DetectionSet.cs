using BaseData;
using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VisionPlatform
{
    public partial class DetectionSet : UserControl
    {
        Conductor form;
        bool bLoad = false;                  //是否在加载数据中
        public DetectionSet(Conductor form)
        {
            InitializeComponent();
            this.form = form;
        }


    }
}
