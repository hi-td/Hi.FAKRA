using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseData;
using HalconDotNet;
using static VisionPlatform.TMData;

namespace VisionPlatform
{
    public class TMData
    {
        [Serializable]
        public enum InspectItem
        {
            Defult,
            StripLen,     //剥皮检测
            TM,           //端子检测
            Conductor,    //导体检测
            Concentricity //同心度检测
        }
        [Serializable]
        public enum ConcentricityType
        {
            Defult,
            male,      //公头
            female,    //母头
        }
        public struct ShowItems
        {
            public FormCamShow form;     //相机显示窗体
            public Panel panel;          //该窗体对应的panel
        }

        [Serializable]
        public struct CamInspectItem
        {
            public int cam;
            public InspectItem item;
            public void Init()
            {
                cam = -1;
                item = InspectItem.Defult;
            }
        }
        public struct InspectResult
        {
            public double InspectTime;  //检测时间
            public double GrabTime;     //拍照时间
            public Dictionary<string, string> outcome; //端子检测结果：检测项及其对应的OK/NG结果
        }

        #region WENYU8 或 WENYU16
        [Serializable]
        public struct IOSet
        {
            public CamInspectItem camItem;   //相机及其对应的检测
            public int read;                 //读取的点位
            public IOSend send;
        }

        [Serializable]
        public struct IOSend
        {
            public bool bSendOK;             //是否发送OK信号
            public bool bSendNG;             //是否发送NG信号
            public int sendOK;　　           //发送OK信号的点位
            public int sendNG;               //发送NG信号的点位
            public bool bSendInvert;         //是否立即反转发送的信号
            public int nSleep;               //发送信号的持续时间
        }
        #endregion

        #region 光源控制器
        [Serializable]
        public struct LightCtrlSet
        {
            public CamInspectItem camItem;      //相机及其对应的检测
            public bool[] CH;                   //当前检测项占用的光源通道，可能同时占用多个通道,占用的通道为true
            public int[] nBrightness;           //当前检测项配置的各个光源通道的亮度(可能出现同一光源被不同检测项使用的情况)    
        }
        //光源串口通讯设置
        [Serializable]
        public struct LEDRTU
        {
            public int BaudRate;
            public string PortName;
            public int DataBits;
            public Parity parity;
            public StopBits stopBits;
        }
        #endregion

        #region FAKRA检测
        [Serializable]
        public struct FakraParam
        {
            public TMLocateParam tMLocateParam;

        }
        #region 端子匹配参数
        public struct TMLocateParam
        {
            public double nminscore;              //最小分数
            public int nThd;               //端子亮度：静态阈值
            public int nminArea;           //端子筛选最小面积
        }
        #endregion
        public struct RubberResult
        {

            public List<double> dListWidth;         //端子的宽度
            public List<double> dListHelght;        //端子的高度
            public List<double> dListDist;          //端子到胶壳顶部的距离
            public List<double> dListArea;          //使用亮班进行检测时，小检测框内亮斑的总面积
            public double dLineArea;                //底部检测框，线的面积
            public List<bool> listOK;               //OK数
            public List<Rect2> listRubberRect2;     //定位框
        }


        #endregion
        [Serializable]
        public struct ROIParam
        {
            public int nDivideNum;                  //均分数
            public int nROIWidth;                   //检测框宽度
            public int nROIHeight;                  //检测框高度
            public int nMoveX;                      //检测框沿定位中心水平方向移动
            public int nMoveY;                      //检测框沿定位中心垂直方向移动
            public int nWidth;                      //小检测框宽
            public int nGap;                        //间距
        }


        [Serializable]
        public struct SlmpPara
        {
            public string IpAddress;
            public int ReadPort;
            public int WritePort;
        }
        //PLC串口通讯设置
        [Serializable]
        public struct PLCRTU
        {
            public int BaudRate;          //波特率
            public string PortName;       //串口号
            public int DataBits;          //数据位
            public Parity parity;         //校验位
            public StopBits stopBits;     //停止位
            public byte slaveAddress;      //站号
        }



        //端子检测项
        [Serializable]
        public struct TMCheckItem
        {
            public bool SkinWeld;   //绝缘皮压脚检测
            public bool SkinPos;    //绝缘皮位置检测
            public bool LineWeld;   //线芯压脚检测
            public bool LinePos;    //线芯位置检测
            public bool LineSide;   //线芯飞边检测
            public bool Waterproof; //防水栓检测
            public bool LineColor;
        }
        //检测数据显示设置
        public struct TMShowSet
        {
            public int cam;
            public InspectItem inspesItem;
            public bool bLeftUp;          //是否显示在左上角
            public int nLeftUpShowPos;    //显示位置
            public int nLeftDownShowPos;  //显示位置
            public int nFrontSize;        //字体大小
            public int nRowGap;           //行间距
            public int nColGap;           //列间距
        }

        //芯线检测
        [Serializable]
        public struct CoreNumParam
        {
            public int method;
            public int nCoreNum;      //线芯标准数量
            public int nCoreNumLess;  //线芯最少数量
            public int nCoreNumMuch;  //线芯最多数量
            public bool bSetROI;      //是否限定检测区域
            public Rect1 rect1;       //芯线检测位置
            public double nRadius;    //芯线半径
            public double dCoreArea;
            public int nThd;



            public int nCoreNum2;      //线芯标准数量
            public double nCoreNumLessArea2;  //线芯少百分比
            public double nCoreNumMuchArea2;  //线芯多百分比
            public int nCoreNumArea2;      //线芯标准面积
            public int nThd2;             //分割
        }

        public struct ColorSpaceParam
        {
            public bool bTrans;
            public ColorSpace colorSpace;
            public int nChannel;
        }

        public struct CamShowParam
        {
            public int cam;
            public int sub_cam;
        }


        #region 同心度参数
        [Serializable]
        public struct ConcentricityParam
        {
            public TMData.ConcentricityType type;    //同心圆类型
            public FitCircleParam outerCircle;       //外导体圆
            public bool bInsultationCircle;          //是否检测绝缘体圆
            public FitCircleParam insulationCircle;  //绝缘体圆
            public FemaleCircle femaleCircle;        //母头圆
            public MaleCircle maleCircle;            //公头圆
            public double dOuterInner;               //外导体-内导体同心度
            public double dOuterInnerHigh;           //外导体-内导体同心度上限
            public double dOuterInsulation;          //外导体-绝缘体同心度
            public double dOuterInsulationHigh;      //外导体-绝缘体同心度上限
        }

        [Serializable]
        public struct FemaleCircle
        {
            public int nRadiusROI;        //内导体圆检测范围
            public int nThd;              //内导体圆阈值
            public double dRadius;        //内导体圆半径
            public double dRadiusLow;     //内导体圆半径下限
            public double dRadiusHigh;    //内导体圆半径上限
        }

        [Serializable]
        public struct MaleCircle
        {
            public int nRadiusROI;        //内导体圆检测范围
            public int nThd;              //内导体圆阈值
        }

        [Serializable]
        public struct FitCircleParam
        {
            public EdgePointMeasure EPMeasure;   //圆拟合参数
            public int nRadius;                  //半径
        }

        public struct ConcentricityResult
        {
            public Circle outerCircle;
            public Circle innerCircle;
            public Circle insulationCircle;
            public double dDist1;           //外导体-内导体同心度
            public double dDist2;           //外导体-绝缘体同心度
        }
        #endregion
    }
}
