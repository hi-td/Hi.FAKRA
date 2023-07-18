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
    public partial class but_run : UserControl
    {
        public but_run()
        {
            InitializeComponent();
        }

        private void butRun_Click(object sender, EventArgs e)
        {
            StaticFun.Run.LoadRun(this.butRun);
        }
    }
}
