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
    public partial class Conductor : UserControl
    {
        public LocationSet LocationSet_Head;    //头部检测位置
        public LocationSet LocationSet_Central; //中部检测位置
        public LocationSet LocationSet_Tail;    //中部检测位置
        private TMFunction TMFun;               //当前检测函数
        private Function Fun;                   //当前底层函数
        string str_CamSer;                      //当前相机序列号
        int m_ncam;                             //当前相机
        bool bLoad = false;                     //是否在加载数据中
        TMData.ConductorType type;              //导体类型
        public event LocationSetValueChangedEvenHandler LocationSetValueChanged;
        public void LocationSetValueChange(object sender, EventArgs e) => LocationSetValueChanged?.Invoke(sender, e);


        public Conductor(int ncam, TMData.ConductorType type)
        {
            InitializeComponent();
            this.type = type;
            m_ncam = ncam;
            InitUI();
            UIConfig.RefreshFun(ncam, 0, ref Fun, ref TMFun, ref str_CamSer);
            LoadParam(type);
            LocationSetValueChanged += Inspect;
        }

        private void InitUI()
        {
            try
            {
                LocationSet_Head = new LocationSet(this)
                {
                    Visible = true,
                    Dock = DockStyle.Fill,
                };
                groupBox_Head.Controls.Add(LocationSet_Head);
                LocationSet_Central = new LocationSet(this)
                {
                    Visible = true,
                    Dock = DockStyle.Fill,
                };
                groupBox_Central.Controls.Add(LocationSet_Central);
                LocationSet_Tail = new LocationSet(this)
                {
                    Visible = true,
                    Dock = DockStyle.Fill,
                };
                groupBox_Tail.Controls.Add(LocationSet_Tail);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        
        private TMData.ConductorParam InitParam()
        {
            TMData.ConductorParam param = new TMData.ConductorParam();
            try
            {
                param.nLocation_Thr = (int)numUp_Location_Thr.Value;
                param.nLocation_Erosion = (int)numUp_Location_Erosion.Value;
                param.CHead = LocationSet_Head.InitParam();
                param.CCentral = LocationSet_Central.InitParam();
                param.CTail = LocationSet_Tail.InitParam();
                param.type = this.type;
            }
            catch (Exception ex)
            {

                throw;
            }

            return param;

        }

        private void LoadParam(TMData.ConductorType type)
        {
            ConductorParam param = new ConductorParam();
            try
            {
                bLoad = true;
                param = TMData_Serializer._globalData.dicConductor[m_ncam][0];
                numUp_Location_Thr.Value = param.nLocation_Thr;
                trackBar_Location_Thr.Value = param.nLocation_Thr;
                numUp_Location_Erosion.Value = param.nLocation_Erosion;
                trackBar_Location_Erosion.Value = param.nLocation_Erosion;
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
                if (bLoad) { return; }
                TMData.ConductorParam param = InitParam();
                Fun.m_hWnd.ClearWindow();
                Fun.m_hWnd.DispObj(Fun.m_hImage);
                TimeSpan time_start = new TimeSpan(DateTime.Now.Ticks);
                bool bResult = TMFun.Conductor(param, false, out var result);

            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.ToString());
            }
        }

        private void trackBar_Location_Thr_Scroll(object sender, EventArgs e)
        {
            numUp_Location_Thr.Value = trackBar_Location_Thr.Value;
        }

        private void trackBar_Location_Erosion_Scroll(object sender, EventArgs e)
        {
            numUp_Location_Erosion.Value = trackBar_Location_Erosion.Value;
        }

    }
}
