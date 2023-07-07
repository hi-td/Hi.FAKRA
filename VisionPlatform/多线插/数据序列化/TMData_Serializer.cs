using BaseData;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VisionPlatform.TMData;

namespace VisionPlatform
{
    public class TMData_Serializer
    {
        [Serializable]
        public class GlobalData
        {
            ////相机对应的检测功能列表
            public Dictionary<string,InspectItem> dicInspectList = new Dictionary<string,InspectItem>();

            //端子检测项
            public Dictionary<string, Dictionary<string, TMCheckItem>>  dicTMCheckList = new Dictionary<string, Dictionary<string, TMCheckItem>>();//string由相机与界面几组成
            //显示数据
            public List<TMShowSet> CamTmShowSet = new List<TMShowSet>();
            public TMShowSet tmShowSet = new TMShowSet();
            //FAKRA检测
            public Dictionary<string, Dictionary<string, FakraParam>> fakraParam = new Dictionary<string, Dictionary<string, FakraParam>>();      
            //相机曝光时间等参数
            public Dictionary<string, CamSDK.CamCommon.CamParam> camParam = new Dictionary<string, CamSDK.CamCommon.CamParam>();    //相机序列号及其对应的参数
            //光源控制器各通道对应的亮度值
            //public Dictionary<int, int> dic_Brightness = new Dictionary<int, int>();
            public List<LightCtrlSet> listLightCtrl = new List<LightCtrlSet>()
            {
                new LightCtrlSet()
            };  //光源通道配置
                //相机及其对应选择模板名称
            public Dictionary<string, string> dic_selectmodel = new Dictionary<string, string>();
        }
        public static GlobalData _globalData = new GlobalData();
        //标定
        [Serializable]
        public class GlobalData_Calib
        {
            public Dictionary<int, BaseData.XYCalibParam> dic_XYCalibParam = new Dictionary<int, XYCalibParam>();
        }
        public static GlobalData_Calib _globalCalib = new GlobalData_Calib();
        [Serializable]
        public class GlobalData_COM
        {
            public List<IOSet> listIOSet = new List<IOSet>();                    //WENYU8或WENYU16-IO信号配置
            public int WENYU232_ComPort = -1;                                    //WENYU232转IO通讯端口号
            public TMData.LEDRTU Led = new TMData.LEDRTU();                      //光源控制器串口配置
        }
        public static GlobalData_COM _COMConfig = new GlobalData_COM();
        //plc串口配置
        public class GlobalData_Mobus
        {
            public TMData.PLCRTU PlcRTU = new TMData.PLCRTU();
        }
        public static GlobalData_Mobus _PlcRTU = new GlobalData_Mobus();
      
    }
}
