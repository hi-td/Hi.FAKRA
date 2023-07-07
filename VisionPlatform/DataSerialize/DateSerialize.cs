using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace ATM_Online
{
    [Serializable]
    public class CSerialData
    {
        public CGlobalData globalData;
        public string m_strCutType; //刀具类型
        public string m_strCutNum; //刀具刃数
        public List<string> m_CheckList; //检测项目列表
    }
    
    [Serializable]
    public class CGlobalData
    {
        private static CGlobalData INSTANCE;
        
        public TopWide1Param topWide1ParamsData = new TopWide1Param();
        public Center_LineParam center_lineParamData = new Center_LineParam();
        public Center_CircleParam center_circleParamData = new Center_CircleParam();
        public Center_CircleLineParam center_circleLineParamData = new Center_CircleLineParam();
        public CenterOffsetParam centerOffsetParamsData = new CenterOffsetParam();
        public CenterOffsetResult centerOffsetResultData = new CenterOffsetResult();
        public TopSRParam topSRParamsData = new TopSRParam();
        public S1_Line_4CutParam s1_line_4CutParamsData = new S1_Line_4CutParam();
        public S1_Circle_4CutParam s1_circle_4CutParamsData = new S1_Circle_4CutParam();
        public S1_3CutParam s1_3CutParamsData = new S1_3CutParam();
        public S2_3CutParam s2_3CutParamData = new S2_3CutParam();
        public S2_Circle_4CutParam s2_Circle_4CutParam = new S2_Circle_4CutParam();
        public S2_4Cut_CircleLineParam s2_4Cut_CircleLineParamData = new S2_4Cut_CircleLineParam();
        public S2_4Cut_LineLineParam s2_4Cut_LineLineParamData = new S2_4Cut_LineLineParam();
        public S3_3CutParam s3_3CutParamData = new S3_3CutParam();
        public S3_4CutParam s3_4CutParamData = new S3_4CutParam();
        public T_NeckSizeParam t_NeckSizeParamsData = new T_NeckSizeParam();
        public CEDParam cedParamsData = new CEDParam();
        public DiffLevelParam diffLevelParamsData = new DiffLevelParam();
        public KeepSpaceParam keepSpaceParamsData = new KeepSpaceParam();

        public AngleRParam angleRParamsData = new AngleRParam();
        public WithEdgeModelParam withEdgeModelParamsData = new WithEdgeModelParam();
        public NoEdgeModelParam noEdgeModelParamsData = new NoEdgeModelParam();
       // public DiffLevelParam diffLevelParamsData = new DiffLevelParam();
        public CutterCrescentParam cutterCrescentParamsData = new CutterCrescentParam();
        public SideWideParam arcWideParamsData = new SideWideParam();
        public SideParam sideParamsData = new SideParam();
        public SideWideParam sideWideParamData = new SideWideParam();
        public ZGAMParam ZGAMParamData = new ZGAMParam();
        public ChamferAngleParam chamferAngleParamData = new ChamferAngleParam();
        public CalibrateResult calibrateResultData = new CalibrateResult();
        public InvertedTaperParam invertedTaperParamData = new InvertedTaperParam();

        public int inspectGap = 0;
        public double topGrabX = 0;
        public double topGrabY = 0;
        public double topGrabZ = 0;
        public double sideGrabX = 0;
        public double sideGrabY = 0;
        public double sideGrabCrescentZ = 0;
        public double sideGrabCEDZ = 0;
        public double radius = 0;
        public double tanZhenPos = 0;
        public double topGrabXNoEdge = 0;
        public double topGrabYNoEdge = 0;
        public double topGrabZNoEdge = 0;
        public double tanZhenPosNoEdge = 0;

        private CGlobalData()
        {

        }
        public static CGlobalData getInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new CGlobalData();
                INSTANCE.center_lineParamData.arrayLineParam = new LineParam[4];
                for (int n= 0; n < 4; n++)
                {
                    INSTANCE.center_lineParamData.arrayLineParam[n].lineIn = new Line();
                }

                INSTANCE.center_circleParamData.arrayCircleParam = new CircleParam[4];
                for(int n=0;n<4;n++)
                {
                    INSTANCE.center_circleParamData.arrayCircleParam[n].circleIn = new Circle();
                }
                INSTANCE.center_circleLineParamData.arrayCircleLineInterParam = new CircleLineInterParam[2];
                for(int n =0; n<2;n++)
                {
                    INSTANCE.center_circleLineParamData.arrayCircleLineInterParam[n].circleParam.circleIn = new Circle();
                    INSTANCE.center_circleLineParamData.arrayCircleLineInterParam[n].lineParam.lineIn = new Line();
                }
                
                INSTANCE.centerOffsetResultData.arryLine = new Line[2];

                INSTANCE.topSRParamsData.circleParam = new CircleParam();
                INSTANCE.topSRParamsData.circleParam.circleIn = new Circle();

                INSTANCE.s1_line_4CutParamsData.lineParam = new LineParam();
                INSTANCE.s1_line_4CutParamsData.lineParam.lineIn = new Line();

                INSTANCE.s1_circle_4CutParamsData.circleParam = new CircleParam();
                INSTANCE.s1_circle_4CutParamsData.circleParam.circleIn = new Circle();

                INSTANCE.s1_3CutParamsData.lineParam = new LineParam();
                INSTANCE.s1_3CutParamsData.lineParam.lineIn = new Line();

                INSTANCE.s2_3CutParamData.lineParam = new LineParam();
                INSTANCE.s2_3CutParamData.lineParam.lineIn = new Line();
                INSTANCE.s2_3CutParamData.circleParam = new CircleParam();
                INSTANCE.s2_3CutParamData.circleParam.circleIn = new Circle();

                INSTANCE.s2_Circle_4CutParam.arrayCircleParam = new CircleParam[2];
                INSTANCE.s2_Circle_4CutParam.arrayCircleParam[0].circleIn = new Circle();
                INSTANCE.s2_Circle_4CutParam.arrayCircleParam[1].circleIn = new Circle();
                INSTANCE.s2_Circle_4CutParam.lineParam = new LineParam();
                INSTANCE.s2_Circle_4CutParam.lineParam.lineIn = new Line();

                INSTANCE.s2_4Cut_CircleLineParamData.lineParam = new LineParam();
                INSTANCE.s2_4Cut_CircleLineParamData.lineParam.lineIn = new Line();
                INSTANCE.s2_4Cut_CircleLineParamData.circleLineInter = new CircleLineInterParam();
                INSTANCE.s2_4Cut_CircleLineParamData.circleLineInter.lineParam.lineIn = new Line();
                INSTANCE.s2_4Cut_CircleLineParamData.circleLineInter.circleParam.circleIn = new Circle();

                INSTANCE.s2_4Cut_LineLineParamData.lineParam = new LineParam();
                INSTANCE.s2_4Cut_LineLineParamData.lineParam.lineIn = new Line();
                INSTANCE.s2_4Cut_LineLineParamData.lineLineInterParam = new LineLineInterParam();
                INSTANCE.s2_4Cut_LineLineParamData.lineLineInterParam.arrayLineParam = new LineParam[2];
                INSTANCE.s2_4Cut_LineLineParamData.lineLineInterParam.arrayLineParam[0].lineIn = new Line();
                INSTANCE.s2_4Cut_LineLineParamData.lineLineInterParam.arrayLineParam[1].lineIn = new Line();

                INSTANCE.s3_3CutParamData.arrayLineParam = new LineParam[2];
                INSTANCE.s3_3CutParamData.arrayLineParam[0].lineIn = new Line();
                INSTANCE.s3_3CutParamData.arrayLineParam[1].lineIn = new Line();

                INSTANCE.s3_4CutParamData.arrayLineParam = new LineParam[2];
                INSTANCE.s3_4CutParamData.arrayLineParam[0].lineIn = new Line();
                INSTANCE.s3_4CutParamData.arrayLineParam[1].lineIn = new Line();

                INSTANCE.t_NeckSizeParamsData.doubleLineParam = new DoubleLineParam();
                INSTANCE.t_NeckSizeParamsData.doubleLineParam.rect2 = new Rect2();

                INSTANCE.chamferAngleParamData.doubleLineParam = new DoubleLineParam();
                INSTANCE.chamferAngleParamData.doubleLineParam.rect2 = new Rect2();
                //INSTANCE.t_NeckSizeParamsData.SideParam = new SideParam();
                //INSTANCE.t_NeckSizeParamsData.SideParam.midLine = new Line();

                INSTANCE.cedParamsData.rect1 = new Rect1();
                INSTANCE.diffLevelParamsData.rect1 = new Rect1();
                INSTANCE.keepSpaceParamsData.rect1 = new Rect1();


                INSTANCE.angleRParamsData.circleParam = new CircleParam();
                INSTANCE.angleRParamsData.circleParam.circleIn = new Circle();

                INSTANCE.noEdgeModelParamsData.modelCenter = new LocatOutParams();
                INSTANCE.noEdgeModelParamsData.modelParam = new LocatInParams();

                //INSTANCE.diffLevelParamsData.m_lineParam = new LineParam();
                //INSTANCE.diffLevelParamsData.m_lineParam.lineIn = new Line();

                //INSTANCE.diffLevelParamsData.m_SideParam = new SideParam();
                //INSTANCE.diffLevelParamsData.m_SideParam.midLine = new Line();

                INSTANCE.cutterCrescentParamsData.rect1 = new Rect1();
                //INSTANCE.cutterCrescentParamsData.lineParam = new LineParam();
                //INSTANCE.cutterCrescentParamsData.lineParam.lineIn = new Line();

                INSTANCE.arcWideParamsData.doubleLine = new DoubleLineParam();
                INSTANCE.arcWideParamsData.doubleLine.rect2 = new Rect2();

                INSTANCE.withEdgeModelParamsData.modelParam = new LocatInParams();
                INSTANCE.withEdgeModelParamsData.modelCenter = new LocatOutParams();
                INSTANCE.withEdgeModelParamsData.m_circle = new Circle();

                INSTANCE.sideParamsData.midLine = new Line();

                INSTANCE.sideWideParamData.doubleLine = new DoubleLineParam();

                INSTANCE.ZGAMParamData.point = new PointP();

                INSTANCE.calibrateResultData = new CalibrateResult();
                //INSTANCE.GASHParamData.circleParam = new CircleParam();
                //INSTANCE.GASHParamData.circleParam.circleIn = new Circle();

                INSTANCE.invertedTaperParamData = new InvertedTaperParam();
                INSTANCE.invertedTaperParamData.arrayRect1 = new Rect1[2];
            }
            return INSTANCE;
        }

        public static void setInstance(CGlobalData instance)
        {
            INSTANCE = instance;
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
    public struct Line
    {
        public double dStartRow;            //直线起始点row
        public double dStartCol;            //直线起始点col
        public double dEndRow;              //直线结束点row
        public double dEndCol;              //直线结束点col
    }

    [Serializable]
    public struct PointP
    {
        public double dRow;
        public double dCol;
    }

    [Serializable]
    public struct CircleLineInterParam
    {
        public CircleParam circleParam;
        public LineParam lineParam;
    }
    [Serializable]
    public struct LineLineInterParam
    {
        public LineParam[] arrayLineParam;
    }
    [Serializable]
    public struct LocatInParams
    {
        public int nModelType;         //创建模板的方式：1轮廓，0区域，2灰度
        public double dAngleStart;     //模板搜索起始角度  备注：弧度
        public double dAngleEnd;       //模板搜索结束角度  备注：弧度
        public string strModelName;           //模板名字
        public int nThd;
        public int nApplyObj;          //模板应用的对象：0开刃，1未开刃
        public Line midLine;
    }

    [Serializable]
    public struct LocatOutParams
    {
        public double dModelRow;
        public double dModelCol;
        public double dModelAngle;
        public int nModelID;
    }

    [Serializable]
    public struct CalibrateParams
    {
        public double dLength;    //棋盘格大小
        public double dArea;
        public int nThreshold;
    }

    [Serializable]
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

    [Serializable]
    public struct DoubleLineParam
    {
        public int nThd;                     //阈值分割
        public int nMeasureLen1;             //直线搜索框length1
        public int nMeasureLen2;             //直线搜索框length2
        public int nMeasureThd;              //边缘点阈值  
        public int nTrans;                   //边缘灰度变化：0=中间黑两边白，1=中间白两边黑
        public string strSelect;             //线段处理类型:first,last
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
        public int nMeasureLen1;             //直线搜索框length1
        public int nMeasureLen2;             //直线搜索框length2
        public int nMeasureThd;              //边缘点阈值  
        public string strTransition;         //边缘灰度变化,暗到亮positive，亮到暗negative
        public string strSelect;             //线段处理类型:first,last
                                             //   public int nMeasureNum;              //测量小框个数
        public Line lineIn;
    }

    [Serializable]
    public struct CircleParam
    {
        public int nMeasureLen1;             //直线搜索框length1
        public int nMeasureLen2;             //直线搜索框length2
        public int nMeasureThd;              //边缘点阈值  
        public string strTransition;         //边缘灰度变化,暗到亮positive，亮到暗negative
        public string strSelect;             //线段处理类型:first,last
                                             // public int nMeasureNum;              //测量小框个数
        public Circle circleIn;
    }

    [Serializable]
    public struct PointParam
    {
        public int nScaleMin;
        public int nScaleMax;
        public int nThd;
        public string strSegmentMode;

        public Rect2 rect2In;
    }

    #region 4刃过中心参数

    [Serializable]
    public struct Center_LineParam
    {
        public LineParam[] arrayLineParam;
    }

    [Serializable]
    public struct Center_CircleParam
    {
        public CircleParam[] arrayCircleParam;
    }

    [Serializable]
    public struct Center_CircleLineParam
    {
        public CircleLineInterParam[] arrayCircleLineInterParam;
    }

    [Serializable]
    public struct Center4
    {
        public LineParam[] arrayLineParam;
    }
    #endregion

    [Serializable]
    public struct S1_Line_4CutParam
    {
        public LineParam lineParam;
    }

    [Serializable]
    public struct S1_Circle_4CutParam
    {
        public CircleParam circleParam;
    }

    [Serializable]
    public struct S1_3CutParam
    {
        public LineParam lineParam;
    }

    [Serializable]
    public struct SideParam
    {
        public double dLeftUpRow;
        public double dLeftUpCol;
        public Line midLine;
    }

    [Serializable]
    public struct CEDParam
    {
        public Rect1 rect1;
    }

    [Serializable]
    public struct KeepSpaceParam
    {
        public Rect1 rect1;
    }

    [Serializable]
    public struct TopWide1Param
    {
        public DoubleLineParam doubleLineParam;
    }

    [Serializable]
    public struct CutterCrescentParam
    {
        public int nThd;     //月牙分割阈值
        public Rect1 rect1;  //有效監測區域
        public int nMeasureLen1;   //直线搜索框length1
        public int nMeasureLen2;   //直线搜索框length2
        public int nMeasureThd;    //边缘点阈值  
    }

    [Serializable]
    public struct SideWideParam
    {
        public DoubleLineParam doubleLine;
        public double dSpiralAngle;        //螺旋角
    }

    //add by Rocky @ 20181208 偏心距
    [Serializable]
    public struct CenterOffsetParam
    {

    }

    [Serializable]
    public struct CenterOffsetResult
    {
        public Line[] arryLine;
        public double dCenterOffset;
    }
    //add by Rocky @ 20181208 SR
    [Serializable]
    public struct TopSRParam
    {
        public CircleParam circleParam;
    }

    //add by Rocky @ 20181208 T_NeckSize
    [Serializable]
    public struct T_NeckSizeParam
    {
        public DoubleLineParam doubleLineParam;
      //  public SideParam sideParam;
    }

    //倒角角度参数
    [Serializable]
    public struct ChamferAngleParam
    {
        public DoubleLineParam doubleLineParam;
    }

    //add by Rocky @ 20181208 AngleR
    [Serializable]
    public struct AngleRParam
    {
        public CircleParam circleParam;
    }

    //add by Rocky @ 20181208 DiffLevel
    [Serializable]
    public struct DiffLevelParam
    {
        public Rect1 rect1;
    }

    //add by Rocky @ 20181208 TopMode
    [Serializable]
    public struct WithEdgeModelParam
    {
        public LocatInParams modelParam;  //模板匹配参数
        public LocatOutParams modelCenter; //模板中心
        public Circle m_circle;              //匹配区域
    }

    [Serializable]
    public struct NoEdgeModelParam
    {
        public LocatInParams modelParam;  //模板匹配参数
        public LocatOutParams modelCenter; //模板中心
    }

    [Serializable]
    public struct NoEdgePreProResult
    {
        public double[] dHomMat2D;  //旋转平移矩阵
        public double dCenterRow;   //中心点
        public double dCenterCol;
        public double dCoreDiam;     //芯径
        public double dCED;          //近似刃径
    }

    [Serializable]
    public struct _3TopPreProResult
    {
        public Line lineMid; //中線
        public System.Drawing.PointF shapePoint; //頂點
        public double dRadius;//近似半徑
        public double dAngle;//模板旋轉角度
        public double[] dHomMat2D; //旋轉平移矩陣
    }

    [Serializable]
    public struct _4TopPreProResult
    {
        public Line[] arryLine; //长刃边缘
        public double dRadius;//近似半徑
        public double dAngle;//模板旋轉角度
        public double[] dHomMat2D; //旋轉平移矩陣
    }

    [Serializable]
    public struct LocateResultParams
    {
        public double dRadius;//近似半徑
        public double dAngle;//模板旋轉角度
        public double[] dHomMat2D; //旋轉平移矩陣
    }

    [Serializable]
    public struct S2_3CutParam
    {
        public LineParam lineParam;
        public CircleParam circleParam;
    }

    [Serializable]
    public struct S3_3CutParam
    {
        public LineParam[] arrayLineParam;
    }

    [Serializable]
    public struct S3_4CutParam
    {
        public LineParam[] arrayLineParam;
    }
    [Serializable]
    public struct S2_Circle_4CutParam
    {
        public CircleParam[] arrayCircleParam;
        public LineParam lineParam;
    }
    //S2:交点为圆与直线
    [Serializable]
    public struct S2_4Cut_CircleLineParam
    {
        public CircleLineInterParam circleLineInter;
        public LineParam lineParam;
    }
    //S2:交点为直线与直线
    [Serializable]
    public struct S2_4Cut_LineLineParam
    {
        public LineLineInterParam lineLineInterParam;
        public LineParam lineParam;
    }
    [Serializable]
    public struct ZGAMParam
    {
        public PointP point;
    }

    [Serializable]
    public struct GASHParam
    {
        public CircleParam circleParam;
    }

    [Serializable]
    public struct InvertedTaperParam
    {
        public Rect1[] arrayRect1;
    }
    //public enum VarDirection
    //{
    //    Up = 0,
    //    Down = 1
    //}

    //[Serializable]
    //public enum HorDirection
    //{
    //    Left = 0,
    //    Right = 1
    //}
    //[Serializable]
    //public class SizeParams
    //{
    //    public int nSizeChecked;     //判断哪种尺寸被测量了 0：高度， 1：宽度， 2：both，-1：null
    //    public Dictionary<VarDirection, LineParam> dicHeightLine;
    //    public Dictionary<HorDirection, LineParam> dicWidthLine;
    //    public bool bHasVal;
    //}

    //[Serializable]
    //public class SizeResult
    //{
    //    public Dictionary<VarDirection, Line> dicVarLineResult;          //垂直方向两条直线
    //    public Dictionary<HorDirection, Line> dicHorLineResult;          //水平方向两条直线
    //    //垂直方向两条直线的长度
    //    public Dictionary<VarDirection, double> dicVarLength()
    //    {
    //        Dictionary<VarDirection, double> dicLineLength = new Dictionary<VarDirection,double>();
    //        VarDirection dir = new VarDirection();
    //        for (int i = 0; i < dicVarLineResult.Count; i++)
    //        {
    //            if(0==i) dir = VarDirection.Up;
    //            else dir = VarDirection.Down;
    //            double drow1 = dicVarLineResult[dir].dlineRow1;
    //            double dcol1 = dicVarLineResult[dir].dlineCol1;
    //            double drow2 = dicVarLineResult[dir].dlineRow2;
    //            double dcol2 = dicVarLineResult[dir].dlineCol2;
    //            HTuple dist = new HTuple();
    //            HOperatorSet.DistancePp(drow1, dcol1, drow2, dcol2, out dist);
    //            dicLineLength.Add(dir, dist.D);
    //        }
    //        return dicLineLength;
    //    }
    //    //水平方向两条直线的长度
    //    public Dictionary<HorDirection, double> dicHorLength()
    //    {
    //        Dictionary<HorDirection, double> dicLineLength = new Dictionary<HorDirection, double>();
    //        HorDirection dir = new HorDirection();
    //        for (int i = 0; i < dicHorLineResult.Count; i++)
    //        {
    //            if (0 == i) dir = HorDirection.Left;
    //            else dir = HorDirection.Right;
    //            double drow1 = dicHorLineResult[dir].dlineRow1;
    //            double dcol1 = dicHorLineResult[dir].dlineCol1;
    //            double drow2 = dicHorLineResult[dir].dlineRow2;
    //            double dcol2 = dicHorLineResult[dir].dlineCol2;
    //            HTuple dist = new HTuple();
    //            HOperatorSet.DistancePp(drow1, dcol1, drow2, dcol2, out dist);
    //            dicLineLength.Add(dir, dist.D);
    //        }
    //        return dicLineLength;
    //    }
    //    //高度
    //    public double dHeight()
    //    {
    //        HTuple h_uprow1 = dicVarLineResult[VarDirection.Up].dlineRow1;
    //        HTuple h_upcol1 = dicVarLineResult[VarDirection.Up].dlineCol1;
    //        HTuple h_uprow2 = dicVarLineResult[VarDirection.Up].dlineRow2;
    //        HTuple h_upcol2 = dicVarLineResult[VarDirection.Up].dlineCol2;

    //        HTuple h_downrow1 = dicVarLineResult[VarDirection.Down].dlineRow1;
    //        HTuple h_downcol1 = dicVarLineResult[VarDirection.Down].dlineCol1;
    //        HTuple h_downrow2 = dicVarLineResult[VarDirection.Down].dlineRow2;
    //        HTuple h_downcol2 = dicVarLineResult[VarDirection.Down].dlineCol2;
    //        HTuple hv_distMin, hv_distMax;
    //        HOperatorSet.DistanceSl(h_uprow1, h_upcol1, h_uprow2, h_upcol2, h_downrow1, h_downcol1, h_downrow2, h_downcol2, out hv_distMin, out hv_distMax);

    //        double dH = (hv_distMin.D + hv_distMax.D)/2;
    //        return dH;
    //    }
    //    //宽度
    //    public double dWidth()
    //    {
    //        HTuple h_leftrow1 = dicHorLineResult[HorDirection.Left].dlineRow1;
    //        HTuple h_leftcol1 = dicHorLineResult[HorDirection.Left].dlineCol1;
    //        HTuple h_leftrow2 = dicHorLineResult[HorDirection.Left].dlineRow2;
    //        HTuple h_leftcol2 = dicHorLineResult[HorDirection.Left].dlineCol2;

    //        HTuple h_rightrow1 = dicHorLineResult[HorDirection.Right].dlineRow1;
    //        HTuple h_rightcol1 = dicHorLineResult[HorDirection.Right].dlineCol1;
    //        HTuple h_rightrow2 = dicHorLineResult[HorDirection.Right].dlineRow2;
    //        HTuple h_rightcol2 = dicHorLineResult[HorDirection.Right].dlineCol2;
    //        HTuple hv_distMin, hv_distMax;
    //        HOperatorSet.DistanceSl(h_leftrow1, h_leftcol1, h_leftrow2, h_leftcol2, h_rightrow1, h_rightcol1, h_rightrow2, h_rightcol2, out hv_distMin, out hv_distMax);

    //        double dW = (hv_distMin.D + hv_distMax.D) / 2;
    //        return dW;

    //    }

    //    public bool bHasVal;
    //} 
    
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
