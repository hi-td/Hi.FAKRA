using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Drawing;
using EnumData;

namespace BaseData
{
    //通讯方式：网口
    [Serializable]
    public class ComMode
    {
        private COMType type;
        private COM com;
        private IO io;
        private WAN ip;
        public COMType TYPE
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

       

        public COM COM
        {
            get
            {
                return com;
            }
            set
            {
                com = value;
            }
        }
        public IO IO
        {
            get
            {
                return io;
            }
            set
            {
                io = value;
            }
        }

        public WAN wan
        {
            get
            {
                return ip;
            }
            set
            {
                ip = value;
            }
        }


    }
    [Serializable]
    public class WAN
    {
        private string brand;
        private string model;
        public string Brand
        {
            get
            {
                return brand;
            }
            set
            {
                brand = value;
            }
        }
        public string Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }
    }

    //初始化数据：软件启动时配置一次
    [Serializable]
    public struct ConfigData
    {
        public CamBrand camBrand;                 //相机品牌
        public int CamNum;                        //相机数量
        public Dictionary<int, int> dic_SubCam;   //相机对应的子画面数量
        public ComMode comMode;                   //通讯方式
        public bool CheckUSB;                     //是否USB相机
        public bool bDigitLight;                  //是否数字型光源控制器
        public int nLightCH;                      //光源控制器通道数
    }

    [Serializable]
    public struct OtherConfig
    {
        public string strCompanyName;   //公司名称
        public bool bDisplay;           //运行中，是否在图像上显示检测数据
        public bool bOrgOK;             //是否保存原始OK图像
        public bool bOrgNG;             //是否保存原始NG图像
        public bool bResultOK;          //是否保存结果OK图像
        public bool bResultNG;          //是否保存结果NG图像
        public int nImageSaveDays;      //图片保存天数
        public string strImgAddress;    //图片保存地址
        public int nErrorNum;           //管理员权限：芯线检测时，设置允许容错的个数
    }

    [Serializable]
    public struct SetImageSaveDays
    {
        public int days;
    }

    #region 联系我们
    [Serializable]
    public struct ContactData
    {
        public string strCompanyName;    //公司名称
        public string strWeb;            //公司网址
        public string strAdress;         //公司地址
        public string strContactName;    //联系人
        public string strTel;            //手机号码
        public string strCode2DPath;     //联系人二维码
        public string strMail;           //邮箱
        public string strPhone;          //电话号码
    }
    #endregion
    ////IO通讯和时间
    //[Serializable]
    //public struct CamIO
    //{
    //    public string cam1DI;
    //    public string cam1DO;
    //    public int cam1ytime;
    //    public int cam1stime;
    //    public string cam2DI;
    //    public string cam2DO;
    //    public int cam2ytime;
    //    public int cam2stime;
    //}

    [Serializable]
    public enum ObjDraw
    {
        empty,
        line,
        rect1,
        rect2,
        circle,
        ellipse,
        arbitrary,
        gap
    }


    public struct ColorSpace
    {
        public enum GRAY
        {
            gray
        }
        public enum RGB
        {
            R = 1,
            G,
            B
        }
        public enum HSV
        {
            H = 1,
            S,
            V
        }
    }

    
    [Serializable]
    //矩形1
    public struct Rect1
    {
        public double dRectRow1;
        public double dRectCol1;
        public double dRectRow2;
        public double dRectCol2;
    }

    [Serializable]
    //矩形2
    public struct Rect2
    {
        public double dRect2Row;
        public double dRect2Col;
        public double dPhi;
        public double dLength1;
        public double dLength2;
    }

    [Serializable]
    //圆
    public struct Circle
    {
        public double dRow;
        public double dCol;
        public double dRadius;
    }

    [Serializable]
    //椭圆
    public struct Ellipse
    {
        public double dRow;
        public double dColumn;
        public double dPhi;
        public double dRadius1;
        public double dRadius2;
    }

    [Serializable]
    //直线
    public struct Line
    {
        public double dStartRow;            //直线起始点row
        public double dStartCol;            //直线起始点col
        public double dEndRow;              //直线结束点row
        public double dEndCol;              //直线结束点col
    }
    [Serializable]
    public struct Arbitrary
    {
        public List<double> dListRow;
        public List<double> dListCol;
    }

    [Serializable]
    //边缘梯度变化点参数
    public struct EdgePointMeasure
    {
        public int nMeasureLen1;             //直线搜索框length1
        public double dMeasureLen2;          //直线搜索框length2
        public int nMeasureThd;              //边缘点阈值  
        public string strTransition;         //边缘灰度变化,暗到亮positive，亮到暗negative
        public string strSelect;             //边缘点选择:first,last
    }

    [Serializable]
    //导体检测位置
    public struct LocationSetMeasure
    {
        public int nSpace;               //间距
        public int nWidth;               //宽度
        public int nHeight;              //高度
    }
    //导体检测参数
    public struct DetectionSetMeasure
    {
        public int nThr;                //阈值

    }

    [Serializable]
    public struct SizeParam
    {
        public dynamic LEFT;
        public dynamic RIGHT;
        public ObjDraw LeftType;
        public ObjDraw RightType;
    }

    #region 定位
    [Serializable]
    public enum ModelType
    {
        contour,
        region,
        ncc,
        gmm
    }

    [Serializable]

    //定位模板输入参数
    public struct LocateInParams
    {
        public ModelType modelType;         //创建模板的方式：1轮廓，0区域，2灰度
        public double dAngleStart;     //模板搜索起始角度  备注：弧度
        public double dAngleEnd;       //模板搜索结束角度  备注：弧度
        public string strModelName;    //模板名字
        public bool bScale;            //是否使用缩放匹配
        public double dScaleMin;       //最小缩放倍率
        public double dScaleMax;       //最大缩放倍率
        public double dScore;          //匹配分数   
    }

    [Serializable]
    //定位模板输出参数
    public struct LocateOutParams
    {
        public double dModelRow;
        public double dModelCol;
        public double dModelAngle;
        public double dScore;
    }

    #endregion

    #region 相机标定
    public enum CamCalibType
    {
        Checker,
        CirclePoint,
        Code2D
    };

    //棋盘格标定
    [Serializable]
    public struct CheckerCalibParam
    {
        public double dLength;    //棋盘格大小
        public double dRectangularity; //棋盘格矩形度
        public int nThreshold;     //阈值：白色背景，黑色格子
    }

    //圆点标定
    [Serializable]
    public struct CirclePointCalibParam
    {
        public double dCircleDiam;  //圆点直径
        public double dCircleSpace; //圆点间距    
        public int nThd;            //阈值：白色背景，黑色圆点
        public double dCircularity; //圆形度
    }
    //二维码标定
    [Serializable]
    public struct Code2DCalibParam
    {
        public string strCodeType;
    }

    [Serializable]
    //标定结果
    public struct CalibrateResult
    {
        public double dXCalib;    //水平方向标定系数
        public double dYCalib;    //垂直方向标定系数
        public double dCalibVal() //平均标定系数
        {
            double dCalib = (dXCalib + dYCalib) / 2.0;
            return dCalib;
        }
    }
    #endregion

    #region XY标定
    [Serializable]
    public struct CalibParam
    {
        public Line line;             //起点-终点
        public double dCalibVal;      //标定系数
        public double dAngleCol;      //与图像水平方向的夹角
        public double dAngleRow;      //与图像垂直方向的夹角
    }

    public struct XYCalibParam
    {
        public CalibParam calibX;      //X轴标定
        public CalibParam calibY;      //Y轴标定
    }
    [Serializable]
    public struct CenterParam
    {
        public PointF point;    //定位中心点
        public double dAngle;   //定位角度：与图像水平方向的夹角
    }
    [Serializable]
    public struct LocateParam
    {
        public CenterParam modelCenter;  //定位点
        public Line lineX;               //运动轴X轴：需沿X轴正方向
        public Line lineY;               //运动轴Y轴：需沿Y轴正方向
    }
    #endregion

    [Serializable]
    public struct DoubleLineParam
    {
        public int nThd;                     //阈值分割
        public int nTransType;
        public EdgePointMeasure EPMeasure;
        public Rect2 rect2;
    }

    [Serializable]
    public struct DoubleLineOut
    {
        public Line lineLeft;
        public Line lineRight;
        public double dDist;     //像素距离
        public double dAngle;
    }

    [Serializable]
    public struct LineParam
    {
        public EdgePointMeasure measure;
        public Line lineIn;
    }

    [Serializable]
    public struct CircleParam
    {
        public EdgePointMeasure EPMeasure;
        public Circle circleIn;
    }

    [Serializable]
    public struct EllipseParam
    {
        public EdgePointMeasure EPMeasure;
        public Ellipse ellipseIn;
    }
    [Serializable]
    public struct Rect2Param
    {
        public EdgePointMeasure EPMeasure;
        public Rect2 rect2In;
    }
    
    [Serializable]
    public struct LineInterLineResult
    {
        public bool bIsSegInter;   //作为线段是否相交
        public bool bIsLineInter;  //作为直线是否相交
        public PointF InterPoint;  //交点
        public double dDistMin;    //最小距离
        public double dDistMax;    //最大距离
        public double dAngle;
    }

    [Serializable]
    public struct CircleInterCircleResult
    {
        public bool bIsIntersect;             //是否相交
        public double dDistCenter;          //两圆圆心距离
        public List<PointF> listInterPoint; //交点

    }

    [Serializable]
    public struct Code1DParam
    {
        public string strCodeType;
        public double dMeasThreshAbs;
        public int nContrastMin;
    }

    [Serializable]
    public struct Code1DDecode
    {
        public string strDecode;
        public string strCodeType;
    }
    [Serializable]
    public struct Code1DResult
    {
        public List<Code1DDecode> listCode1DDecode;
    }

    [Serializable]
    public enum ImageProType
    {
        roi,
        color_space_trans,
        mean,
        median,
        equhist,
        scale,
        threshold,
        dyn_threshold,
        connect,
        union,
        dilation,
        erosion,
        opening,
        closing,
        thd_sub_pixel,
        edges_sub_pixel,
        feature_select,
        fill_up,
        fill_up_shape,
        gen_contour_region
    }


    [Serializable]
    public struct roiParam
    {
        public ObjDraw objDraw;
        public Rect1 rect1;
        public Rect2 rect2;
        public Circle circle;
        public Line line;
        public Ellipse ellipse;
    }
    [Serializable]
    public struct MeanImageParam
    {
        public int nMaskWidth;
        public int nMaskHeight;
    }
    [Serializable]
    public struct MedianImageParam
    {
        public string strMaskType;
        public int nMaskRadius;
    }

    [Serializable]
    public struct ScaleImageParam
    {
        public int nScaleMin;
        public int nScaleMax;
    }
    [Serializable]
    public struct ThresholdParam
    {
        public int nThdMin;
        public int nThdMax;
    }
    [Serializable]
    public struct DynThresholdParam
    {
        public ImageProType orgImage;
        public ImageProType thdImage;
        public string strRegionType;
        public int nOffset;
    }
    [Serializable]
    public struct MorphologyParam
    {
        public string strFilterType;
        public int nMaskWidth;
        public int nMaskHeight;
        public double dMaskRadius;
    }
    [Serializable]
    public struct ThdEdgeParam
    {
        public int nThd;
    }
    [Serializable]
    public struct EdgesSubParam
    {
        public string strFilterType;
        public double dAlpha;
        public int nMin;
        public int nMax;
    }

    [Serializable]
    public struct ColorSpaceParam
    {
        public string strColorSpace;
        public int nSpaceSel; 
       
    }

    [Serializable]
    public struct FeatureSelectParam
    {
        public List<string> listFeature;
        public List<double> listMinValue;
        public List<double> listMaxValue;
    }
    [Serializable]
    public struct  FillUpParam
    {
        public string strShape;
        public int nMinVal;
        public int nMaxVal;
    }
    [Serializable]
    public struct ImageProcessParam
    {
        public roiParam roi;
        public ColorSpaceParam color_space_trans;
        public MeanImageParam mean;
        public MedianImageParam median;
        public ScaleImageParam scale;
        public ThresholdParam threshold;
        public MorphologyParam dilation;
        public DynThresholdParam dyn_threshold;
        public MorphologyParam erosion;
        public MorphologyParam opening;
        public MorphologyParam closing;
        public ThdEdgeParam thd_sub_pixel;
        public EdgesSubParam edges_sub_pixel;
        public FeatureSelectParam feature_select;
        public FillUpParam fill_up_shape;
    }
   
    [Serializable]
    public struct OCRParam
    {
        public string strMethod;
        public string strCharType;
        public int nBackGround;
        public string strDirect;
        public double dMinWidth;
        public double dMaxWidth;
        public double dMinHeight;
        public double dMaxHeight;
        
    }
    //[Serializable]
    //public class Code2DResults
    //{
    //    public List<string> strListCodeVal; //二维码值
    //    public List<double> dListCodeRow;   //二维码中心点坐标Row
    //    public List<double> dListCodeCol;   //二维码中心点坐标Col
    //    public List<double> dListCodeArea;  //二维码的面积
    //    public bool bHasVal;
    //}

}
