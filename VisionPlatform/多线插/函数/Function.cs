using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HalconDotNet;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using BaseData;
using EnumData;
using Chustange.Functional;
using StaticFun;
using System.Drawing.Imaging;
using static VisionPlatform.TMData;

namespace VisionPlatform
{
    public class Function
    {
        public HWindow m_hWnd = null;
        public HWindowControl m_hWndCtrl = null;
        public HObject m_OrgImage = null;
        public HObject m_hImage = null;
        public HObject m_GrayImage = null;
        public HObject ho_Reduceimage = null;
        public HObject m_PreImage = null;    //当前图像：中转使用
        public static int imageWidth;
        public static int imageHeight;

        public static RichTextBox m_richTextBox = null;

        public Line m_line = new Line();
        public Circle m_circle = new Circle();
        public Rect1 m_rect1 = new Rect1();
        public Rect2 m_rect2 = new Rect2();
        public Ellipse m_ellipse = new Ellipse();
        public Arbitrary m_arbitrary = new Arbitrary();

        private static HObject m_ObjShow = new HObject(); //随窗体大小变化显示Object

        public ObjDraw m_lastDraw = new ObjDraw();  //最好一次绘制的形状


        public int m_nPreFontSize = 15;
        public List<int> m_listFontSize = new List<int> { 15 };                        //图像窗口显示的字体大小
        public List<int> m_listRowSite = new List<int> { 12 };
        public List<int> m_listColSite = new List<int> { 12 };
        public List<string> m_listStrshow = new List<string> { "" };
        public List<string> m_listColorFont = new List<string> { "red" };

        public string m_colorRegion = "red";

        private HObject m_XLDCont = new HObject();          //使用轮廓创建模板时使用

        public bool b_image = false;
        public List<Mirror> m_ListImgMirror = new List<Mirror>();
        public double dReslutRow0 = 0;
        public double dReslutCol0 = 0;
        public double dReslutRow1 = 0;
        public double dReslutCol1 = 0;

        public enum objType
        {
            image,
            region,
            xld
        }
        public class imageProPre
        {
            public ImageProType type;
            public objType objType;
            public HObject obj;
        }

        public static void InitSystem()
        {
            try
            {
                HOperatorSet.SetSystem("tsp_width", 3000);
                HOperatorSet.SetSystem("tsp_height", 3000);
                HOperatorSet.SetSystem("do_low_error", "false");//少报错
                HOperatorSet.SetSystem("clip_region", "false");//region在图像外不切掉
                HOperatorSet.SetSystem("border_shape_models", "true");//依然匹配边缘的图形
            }
            catch (Exception ex)
            {
                ex.ToString();
                return;
            }
        }
        public Function(HWindowControl hWndCtrl)
        {
            if (hWndCtrl != null)
            {
                //获取WindowControl的句柄
                m_hWnd = hWndCtrl.HalconWindow;
                m_hWndCtrl = hWndCtrl;

                m_hWnd.SetColor("red");
                m_hWnd.SetDraw("margin");
                HOperatorSet.GenEmptyObj(out m_ObjShow);
            }
        }

        ~Function()
        {
        }

        #region 运行中保存原始图像和结果图像

        public void SaveOKImage(int cam, string strCheckItem)
        {
            try
            {
                string ao = "";
                string ar = "";
                switch (cam)
                {
                    case 1:
                        ao = GlobalPath.SavePath.cam1_OrgImagePath_OK;
                        ar = GlobalPath.SavePath.cam1_ReslutImagePath_OK;
                        break;
                    case 2:
                        ao = GlobalPath.SavePath.cam2_OrgImagePath_OK;
                        ar = GlobalPath.SavePath.cam2_ReslutImagePath_OK;
                        break;
                    case 3:
                        ao = GlobalPath.SavePath.cam3_OrgImagePath_OK;
                        ar = GlobalPath.SavePath.cam3_ReslutImagePath_OK;
                        break;
                    case 4:
                        ao = GlobalPath.SavePath.cam4_OrgImagePath_OK;
                        ar = GlobalPath.SavePath.cam4_ReslutImagePath_OK;
                        break;
                }
                //保存原始OK图
                if (GlobalData.Config._InitConfig.otherConfig.bOrgOK)
                {
                    SaveImage(ao + strCheckItem + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\" + DateTime.Now.Hour.ToString() + "\\");
                }
                //保存结果OK图
                if (GlobalData.Config._InitConfig.otherConfig.bResultOK)
                {
                    SaveResultImage(ar + strCheckItem + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\" + DateTime.Now.Hour.ToString() + "\\");
                }
            }
            catch (Exception ex)
            {
                StaticFun.MessageFun.ShowMessage("OK图像保存出错：" + ex.ToString());
                return;
            }
        }

        public void SaveNGImage(int cam, string strCheckItem)
        {
            try
            {
                string ao = "";
                string ar = "";
                switch (cam)
                {
                    case 1:
                        ao = GlobalPath.SavePath.cam1_OrgImagePath_NG;
                        ar = GlobalPath.SavePath.cam1_ReslutImagePath_NG;
                        break;
                    case 2:

                        ao = GlobalPath.SavePath.cam2_OrgImagePath_NG;
                        ar = GlobalPath.SavePath.cam2_ReslutImagePath_NG;
                        break;
                    case 3:

                        ao = GlobalPath.SavePath.cam3_OrgImagePath_NG;
                        ar = GlobalPath.SavePath.cam3_ReslutImagePath_NG;
                        break;
                    case 4:
                        ao = GlobalPath.SavePath.cam4_OrgImagePath_NG;
                        ar = GlobalPath.SavePath.cam4_ReslutImagePath_NG;
                        break;
                }
                //保存原始NG图
                if (GlobalData.Config._InitConfig.otherConfig.bOrgNG)
                {
                    SaveImage(ao + strCheckItem + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\" + DateTime.Now.Hour.ToString() + "\\");
                }
                //保存结果NG图
                if (GlobalData.Config._InitConfig.otherConfig.bResultNG)
                {
                    SaveResultImage(ar + strCheckItem + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\" + DateTime.Now.Hour.ToString() + "\\");
                }
            }
            catch (Exception ex)
            {
                MessageFun.ShowMessage("NG图像保存出错：" + ex.ToString());
                return;
            }
        }

        #endregion
        public void SaveModelImage(int ncam, string strImageName)
        {
            try
            {
                string strSavePath = GlobalPath.SavePath.ModelImagePath + "相机" + ncam.ToString() + "\\";
                if (string.IsNullOrWhiteSpace(strSavePath))
                    return;
                var folder = Path.GetDirectoryName(strSavePath);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                strSavePath = strSavePath + strImageName;//不能包含中文
                string str = strSavePath.Replace("\\", "/");
                if (null != m_hImage)
                {
                    HOperatorSet.WriteImage(m_hImage, "bmp", 0, str);
                }
            }
            catch (HalconException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return;
            }
        }
        //从相机获取图像

        public void MirrorImage(ref HObject ho_Image)
        {
            try
            {
                if (null == m_ListImgMirror)
                {
                    return;
                }
                foreach (Mirror mir in m_ListImgMirror)
                {
                    if (mir == Mirror.Left_Right)
                    {
                        HOperatorSet.MirrorImage(ho_Image, out ho_Image, "column");
                    }
                    else if (mir == Mirror.Up_Down)
                    {
                        HOperatorSet.MirrorImage(ho_Image, out ho_Image, "row");
                    }
                }
            }
            catch (HalconException ex)
            {
                StaticFun.MessageFun.ShowMessage("镜像图像错误：" + ex.ToString());
            }
        }
        public void GetImageFromCam(EnumData.CamColor channel, IntPtr pImageBuf, int nWidth, int nHeight)
        {
            HObject image = new HObject();
            try
            {
                if (m_hWnd == null)
                {
                    return;
                }
                if (channel == CamColor.color)
                {
                    HOperatorSet.GenImageInterleaved(out image, (HTuple)pImageBuf, (HTuple)"bgr", (HTuple)nWidth, (HTuple)nHeight, -1, "byte", 0, 0, 0, 0, -1, 0);
                }
                else if (channel == CamColor.mono)
                {
                    HOperatorSet.GenImage1Extern(out image, "byte", nWidth, nHeight, pImageBuf, IntPtr.Zero);
                }
                else
                {
                    return;
                }
                m_hImage?.Dispose();
                m_hImage = image.Clone();
                MirrorImage(ref m_hImage);
                m_GrayImage?.Dispose();
                HOperatorSet.Rgb1ToGray(m_hImage, out m_GrayImage);  //必须放在mirror图像后面
                b_image = true;
                FitImageToWindow(ref dReslutRow0, ref dReslutCol0, ref dReslutRow1, ref dReslutCol1);
                return;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            finally
            {
                image.Dispose();
            }
        }
        public void GetImage(HObject ho_Image)
        {
            try
            {
                m_hImage = ho_Image;
                MirrorImage(ref m_hImage);
                m_GrayImage?.Dispose();
                HOperatorSet.Rgb1ToGray(m_hImage, out m_GrayImage);  //必须放在mirror图像后面
                b_image = true;
                FitImageToWindow(ref dReslutRow0, ref dReslutCol0, ref dReslutRow1, ref dReslutCol1);
                return;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        //适用于大华相机
        public void Bitmap2HObject(Bitmap bitmap, bool m_bTrig)
        {
            // double dReslutRow0 = 0, dReslutCol0 = 0, dReslutRow1 = 0, dReslutCol1 = 0;

            HTuple hv_Channels = new HTuple();
            try
            {
                Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bitmapData = bitmap.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
                IntPtr intPtr = bitmapData.Scan0;
                b_image = false;
                //m_hImage?.Dispose();
                HOperatorSet.GenEmptyObj(out m_hImage);
                HOperatorSet.GenImageInterleaved(out m_hImage, intPtr, "bgrx", bitmap.Width, bitmap.Height, -1, "byte", 0, 0, 0, 0, -1, 0);
                HOperatorSet.CountChannels(m_hImage, out hv_Channels);

                {
                    HOperatorSet.Rgb1ToGray(m_hImage, out m_hImage);
                    //HTuple hv_Gray = new HTuple(); 
                    //HOperatorSet.GetGrayval(m_hImage, 12, 12, out hv_Gray);
                    HOperatorSet.ConvertImageType(m_hImage, out m_hImage, "byte");
                }
                b_image = true;
                if (!m_bTrig)
                {
                    //list集合保存图片
                    //  m_listImage.Add(m_hImage);
                }
                FitImageToWindow(ref dReslutRow0, ref dReslutCol0, ref dReslutRow1, ref dReslutCol1);
                bitmap.UnlockBits(bitmapData);

            }
            catch (SystemException ex)
            {
                (ex.Message + ex.StackTrace).ToLog();
                ex.ToString();
                return;
            }
        }

        #region 工具函数
        public void DispRegion(HObject obj, string strColor = "green", string draw = "margin")
        {

            var run = Task.Run(() =>
            {
                m_colorRegion = strColor;
                if (!m_ObjShow.IsInitialized())
                {
                    m_ObjShow = new HObject(obj);
                }
                else
                {
                    m_ObjShow = m_ObjShow.ConcatObj(obj);
                }
                m_hWnd.SetColor(strColor);
                m_hWnd.SetDraw(draw);
                m_hWnd.DispObj(obj);
                m_hWnd.SetColor("green");
                m_hWnd.SetDraw("margin");
            });
            run.Wait();
        }
        //清除m_ObjShow
        public void ClearObjShow()
        {
            try
            {
                var task = Task.Run(() =>
                {
                    m_listFontSize.Clear();
                    m_listRowSite.Clear();
                    m_listColorFont.Clear();
                    m_listColSite.Clear();
                    m_listStrshow.Clear();
                    m_ObjShow?.Dispose();
                    HOperatorSet.GenEmptyObj(out m_ObjShow);
                    //m_nLastDraw = -1;
                    m_lastDraw = ObjDraw.empty;
                    HOperatorSet.ClearWindow(m_hWnd);
                    if (null != m_hImage)
                        m_hWnd.DispObj(m_hImage);
                });
                task.Wait();

            }
            catch (Exception)
            {
                return;
            }

        }
        //清除m_ObjShow
        public void ClearObjShowS()
        {
            try
            {
                m_ObjShow = new HObject();
                m_lastDraw = ObjDraw.empty;
                ShowRegion(m_ObjShow);
            }
            catch (Exception)
            {
                return;
            }

        }

        ////获取WindowControl的句柄
        //public static void GetHalconWnd(HWindowControl hWndCtrl)
        //{
        //    m_hWnd = hWndCtrl.HalconWindow;
        //    m_hWndCtrl = hWndCtrl;
        //}

        //获取鼠标按下时的图像坐标
        public Point GetMousePos()
        {
            Point point = new Point();
            int row, col, button;
            try
            {
                m_hWnd.GetMposition(out row, out col, out button);
                point.X = col;
                point.Y = row;
                return point;
            }
            catch (HalconException ex)
            {
                MessageBox.Show(ex.ToString());
                return point;
            }


        }
        //画矩形1
        public Rect1 DrawRect1()
        {
            Rect1 rect1 = new Rect1();

            HObject ho_Rect1 = null;
            HOperatorSet.GenEmptyObj(out ho_Rect1);

            HTuple hRectRow1 = new HTuple(), hRectCol1 = new HTuple(), hRectRow2 = new HTuple(), hRectCol2 = new HTuple();

            try
            {
                if (null == m_hWnd || null == m_hImage) return rect1;
                m_hWnd.SetColor("red");
                m_hWnd.SetDraw("margin");
                m_hWnd.DispObj(m_hImage);
                HOperatorSet.DrawRectangle1(m_hWnd, out hRectRow1, out hRectCol1, out hRectRow2, out hRectCol2);
                HOperatorSet.GenRectangle1(out ho_Rect1, hRectRow1, hRectCol1, hRectRow2, hRectCol2);
                m_hWnd.DispObj(ho_Rect1);
                if (0 != ho_Rect1.CountObj())
                {
                    rect1.dRectRow1 = hRectRow1.D;
                    rect1.dRectCol1 = hRectCol1.D;
                    rect1.dRectRow2 = hRectRow2.D;
                    rect1.dRectCol2 = hRectCol2.D;
                }
                return rect1;
            }
            catch (HalconException ex)
            {
                MessageBox.Show(ex.ToString());
                return rect1;
            }
            finally
            {
                if (null != ho_Rect1) ho_Rect1.Dispose();
            }
        }
        //画矩形2
        public Rect2 DrawRect2()
        {
            Rect2 rect2 = new Rect2();
            HObject ho_Rect2 = null;
            HOperatorSet.GenEmptyObj(out ho_Rect2);

            HTuple hRect2Row = new HTuple(), hRect2Col = new HTuple(), hPhi = new HTuple(), hLength1 = new HTuple(), hLength2 = new HTuple();

            try
            {
                if (null == m_hImage || null == m_hWnd) return rect2;
                m_hWnd.DispObj(m_hImage);
                m_hWnd.SetDraw("margin");
                WriteStringtoImage(20, 30, 30, "请点击鼠标左键开始绘制检测区域，点击鼠标右键结束", "red");
                HOperatorSet.DrawRectangle2(m_hWnd, out hRect2Row, out hRect2Col, out hPhi, out hLength1, out hLength2);
                HOperatorSet.GenRectangle2(out ho_Rect2, hRect2Row, hRect2Col, hPhi, hLength1, hLength2);
                m_hWnd.DispObj(m_hImage);
                m_hWnd.DispObj(ho_Rect2);
                if (0 != ho_Rect2.CountObj())
                {
                    rect2.dRect2Row = hRect2Row.D;
                    rect2.dRect2Col = hRect2Col.D;
                    rect2.dPhi = hPhi.D;
                    rect2.dLength1 = hLength1.D;
                    rect2.dLength2 = hLength2.D;
                }
                return rect2;
            }
            catch (HalconException ex)
            {
                MessageBox.Show(ex.ToString());
                rect2.dRect2Row = 0;
                rect2.dRect2Col = 0;
                rect2.dPhi = 0;
                rect2.dLength1 = 0;
                rect2.dLength2 = 0;
                return rect2;
            }
            finally
            {
                if (null != ho_Rect2) ho_Rect2.Dispose();
            }
        }

        //画圆形
        public Circle DrawCircle()
        {
            Circle circle = new Circle();

            HObject ho_Circle = null;
            HOperatorSet.GenEmptyObj(out ho_Circle);

            HTuple hv_Row = new HTuple(), hv_Col = new HTuple(), hv_Radius = new HTuple();

            try
            {
                if (null == m_hImage || null == m_hWnd) return circle;
                m_hWnd.SetColor("red");
                m_hWnd.SetDraw("margin");
                m_hWnd.DispObj(m_hImage);
                HOperatorSet.DrawCircle(m_hWnd, out hv_Row, out hv_Col, out hv_Radius);
                HOperatorSet.GenCircle(out ho_Circle, hv_Row, hv_Col, hv_Radius);
                m_hWnd.DispObj(ho_Circle);
                if (0 != ho_Circle.CountObj())
                {
                    circle.dRow = hv_Row.D;
                    circle.dCol = hv_Col.D;
                    circle.dRadius = hv_Radius.D;
                }
                return circle;
            }
            catch (HalconException ex)
            {
                MessageBox.Show(ex.ToString());
                return circle;
            }
            finally
            {
                if (null != ho_Circle) ho_Circle.Dispose();
            }
        }

        public Arbitrary DrawArbitrary()
        {
            Arbitrary arbitrary = new Arbitrary();
            arbitrary.dListRow = new List<double>();
            arbitrary.dListCol = new List<double>();
            HTuple hv_arrayRow = new HTuple(), hv_arrayCol = new HTuple();
            HTuple hv_Button = new HTuple(), hv_row = new HTuple(), hv_column = new HTuple();

            HObject ho_Contour = null; ;

            HOperatorSet.GenEmptyObj(out ho_Contour);
            try
            {
                if (null == m_hImage || m_hWnd == null)
                {
                    if (null == m_hImage)
                        MessageFun.ShowMessage("无图像");
                    else if (null == m_hWnd)
                        MessageFun.ShowMessage("无窗体句柄");
                    return arbitrary;
                }
                m_hWnd.DispObj(m_hImage);
                m_hWnd.SetDraw("margin");
                WriteStringtoImage(15, 12, 12, "鼠标左键开始（连续单击），右键结束（单击）。", "red");
                hv_Button = 1;
                while (hv_Button.I == 1)
                {
                    HOperatorSet.GetMbutton(m_hWnd, out hv_row, out hv_column, out hv_Button);
                    m_hWnd.SetColor("green");
                    m_hWnd.DispCross(hv_row.D, hv_column.D, 30, 0);
                    hv_arrayRow = hv_arrayRow.TupleConcat(hv_row);
                    hv_arrayCol = hv_arrayCol.TupleConcat(hv_column);
                    m_hWnd.SetColor("red");
                    m_hWnd.DispPolygon(hv_arrayRow, hv_arrayCol);
                    arbitrary.dListRow.Add(hv_row.D);
                    arbitrary.dListCol.Add(hv_column.D);
                }
                hv_arrayRow = hv_arrayRow.TupleConcat(hv_arrayRow[0]);
                hv_arrayCol = hv_arrayCol.TupleConcat(hv_arrayCol[0]);
                arbitrary.dListRow.Add(hv_arrayRow[0].D);
                arbitrary.dListCol.Add(hv_arrayCol[0].D);
                if (arbitrary.dListRow.Count <= 3)
                {
                    arbitrary = new Arbitrary();
                    MessageFun.ShowMessage("点数少于3个点，请重新绘制。");
                    return arbitrary;
                }
                HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_arrayRow, hv_arrayCol);
                m_hWnd.DispObj(ho_Contour);
                return arbitrary;
            }
            catch (HalconException ex)
            {
                MessageFun.ShowMessage(ex.ToString());
                (ex.Message + ex.StackTrace).ToLog();
                return arbitrary;
            }
            finally
            {
                ho_Contour?.Dispose();
            }
        }
        //画直线
        private static void gen_arrow_contour_xld(out HObject ho_Arrow, HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Row2, HTuple hv_Column2, HTuple hv_HeadLength, HTuple hv_HeadWidth)
        {
            HObject ho_TempArrow = null;

            HTuple hv_Length = null, hv_ZeroLengthIndices = null;
            HTuple hv_DR = null, hv_DC = null, hv_HalfHeadWidth = null;
            HTuple hv_RowP1 = null, hv_ColP1 = null, hv_RowP2 = null;
            HTuple hv_ColP2 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_TempArrow);

            ho_Arrow.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Arrow);

            HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Length);
            //
            //Mark arrows with identical start and end point
            //(set Length to -1 to avoid division-by-zero exception)
            hv_ZeroLengthIndices = hv_Length.TupleFind(0);
            if (-1 != hv_ZeroLengthIndices[0])
            {
                if (hv_Length == null)
                    hv_Length = new HTuple();
                hv_Length[hv_ZeroLengthIndices] = -1;
            }
            //
            //Calculate auxiliary variables.
            hv_DR = (1.0 * (hv_Row2 - hv_Row1)) / hv_Length;
            hv_DC = (1.0 * (hv_Column2 - hv_Column1)) / hv_Length;
            hv_HalfHeadWidth = hv_HeadWidth / 2.0;
            //
            //Calculate end points of the arrow head.
            hv_RowP1 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) + (hv_HalfHeadWidth * hv_DC);
            hv_ColP1 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) - (hv_HalfHeadWidth * hv_DR);
            hv_RowP2 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) - (hv_HalfHeadWidth * hv_DC);
            hv_ColP2 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) + (hv_HalfHeadWidth * hv_DR);
            //
            //Finally create output XLD contour for each input point pair
            for (int i = 0; i <= (hv_Length.TupleLength() - 1); i++)
            {
                if (-1 == hv_Length[i].D)
                {
                    //Create_ single points for arrows with identical start and end point
                    ho_TempArrow.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_TempArrow, hv_Row1[i], hv_Column1[i]);
                }
                else
                {
                    //Create arrow contour
                    ho_TempArrow.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_TempArrow, new HTuple(hv_RowP1[i].D).TupleConcat(hv_Row2[i].D).TupleConcat(hv_RowP2[i].D).TupleConcat(hv_RowP1[i].D),
                        new HTuple(hv_ColP1[i].D).TupleConcat(hv_Column2[i].D).TupleConcat(hv_ColP2[i].D).TupleConcat(hv_ColP1[i].D));
                    HOperatorSet.GenRegionContourXld(ho_TempArrow, out ho_TempArrow, "filled");
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_Arrow, ho_TempArrow, out ExpTmpOutVar_0);
                    ho_Arrow.Dispose();
                    ho_Arrow = ExpTmpOutVar_0;
                }
            }
            ho_TempArrow.Dispose();
            return;
        }
        public Line DrawLine()
        {
            Line line = new Line();

            HObject ho_Line = null, ho_Arrow = null;
            HOperatorSet.GenEmptyObj(out ho_Line);
            HOperatorSet.GenEmptyObj(out ho_Arrow);

            HTuple hv_StartRow = new HTuple(), hv_StartCol = new HTuple();
            HTuple hv_EndRow = new HTuple(), hv_EndCol = new HTuple();

            try
            {
                if (null == m_hImage || null == m_hWnd) return line;
                m_hWnd.DispObj(m_hImage);
                HOperatorSet.DrawLine(m_hWnd, out hv_StartRow, out hv_StartCol, out hv_EndRow, out hv_EndCol);
                HTuple hv_midRow = (hv_StartRow + hv_EndRow) / 2.0;
                HTuple hv_midCol = (hv_EndCol + hv_StartCol) / 2.0;
                double dDist = Math.Sqrt(Math.Pow((hv_midRow - hv_StartRow), 2) + Math.Pow((hv_midCol - hv_StartCol), 2)) / 10;
                gen_arrow_contour_xld(out ho_Arrow, hv_StartRow, hv_StartCol, hv_midRow, hv_midCol, dDist, dDist * 1.5);
                HOperatorSet.GenRegionLine(out ho_Line, hv_StartRow, hv_StartCol, hv_EndRow, hv_EndCol);
                m_hWnd.DispObj(ho_Line);
                m_hWnd.SetColor("green");
                // m_hWnd.SetDraw("fill");
                m_hWnd.DispObj(ho_Arrow);
                // SetShow(m_showParam);
                if (0 != ho_Line.CountObj())
                {
                    line.dStartRow = hv_StartRow.D;
                    line.dStartCol = hv_StartCol.D;
                    line.dEndRow = hv_EndRow.D;
                    line.dEndCol = hv_EndCol.D;
                }
                return line;
            }
            catch (HalconException ex)
            {
                MessageBox.Show(ex.ToString());
                return line;
            }
            finally
            {
                if (null != ho_Line) ho_Line.Dispose();
            }
        }
        //绘制椭圆
        public Ellipse DrawEllipse()
        {
            Ellipse ellipse = new Ellipse();

            HObject ho_Ellipse = null;
            HOperatorSet.GenEmptyObj(out ho_Ellipse);

            HTuple hv_row = new HTuple(), hv_column = new HTuple(), hv_phi = new HTuple(), hv_radius1 = new HTuple(), hv_radius2 = new HTuple();

            try
            {
                m_ObjShow.Dispose();
                if (null == m_hImage || null == m_hWnd) return ellipse;
                m_hWnd.DispObj(m_hImage);
                HOperatorSet.DrawEllipse(m_hWnd, out hv_row, out hv_column, out hv_phi, out hv_radius1, out hv_radius2);
                HOperatorSet.GenEllipse(out ho_Ellipse, hv_row, hv_column, hv_phi, hv_radius1, hv_radius2);
                m_hWnd.DispObj(ho_Ellipse);
                if (0 != ho_Ellipse.CountObj())
                {
                    ellipse.dRow = hv_row.D;
                    ellipse.dColumn = hv_column.D;
                    ellipse.dPhi = hv_phi.D;
                    ellipse.dRadius1 = hv_radius1.D;
                    ellipse.dRadius2 = hv_radius2.D;
                }
                return ellipse;
            }
            catch (HalconException ex)
            {
                MessageBox.Show(ex.ToString());
                ellipse.dRow = 0;
                ellipse.dColumn = 0;
                ellipse.dPhi = 0;
                ellipse.dRadius1 = 0;
                ellipse.dRadius2 = 0;
                return ellipse;
            }
            finally
            {
                if (null != ho_Ellipse) ho_Ellipse.Dispose();
            }
        }
        //显示十字线
        public void ShowCross(double dRow0, double dCol0, double dRow1, double dCol1)
        {
            try
            {
                int row, col, button;
                m_hWnd.GetMposition(out row, out col, out button);
                ShowImages(dRow0, dCol0, dRow1, dCol1);
                m_hWnd.DispLine(row, 0, row, (double)imageWidth);
                m_hWnd.DispLine(0, col, (double)imageHeight, col);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return;
            }
            finally
            {
            }
        }
        //显示坐标和灰度值
        public void ShowCoordinateGrayVal(out int nRow, out int nCol, out List<double> listGrayVal)
        {
            nRow = 0;
            nCol = 0;
            listGrayVal = new List<double>();
            HTuple hv_GrayVal = new HTuple(), hv_channels = new HTuple();

            try
            {
                if (null == m_hImage)
                    return;
                int button;
                m_hWnd.GetMposition(out nRow, out nCol, out button);
                HOperatorSet.GetGrayval(m_hImage, nRow, nCol, out hv_GrayVal);
                for (int i = 0; i < hv_GrayVal.TupleLength(); i++)
                {
                    listGrayVal.Add(hv_GrayVal[i].D);
                }
            }
            catch (HalconException error)
            {
                error.ToString();
                return;
            }
        }
        public void ShowArbitrary(Arbitrary arbitrary)
        {
            try
            {
                if (null == arbitrary.dListRow)
                {
                    MessageFun.ShowMessage("无任意形状轮廓所需轮廓点。");
                    return;
                }
                if (null != m_hImage)
                {
                    m_hWnd.DispObj(m_hImage);
                }
                HTuple hv_row = arbitrary.dListRow.ToArray();
                HTuple hv_col = arbitrary.dListCol.ToArray();
                m_hWnd.SetColor("green");
                m_hWnd.DispCross(hv_row, hv_col, 20, 0);
                m_hWnd.SetColor("red");
                HOperatorSet.GenContourPolygonXld(out m_XLDCont, hv_row, hv_col);
                DispRegion(m_XLDCont, "green");
                return;
            }
            catch (HalconException ex)
            {
                MessageFun.ShowMessage("获取任意形状轮廓出错：" + ex.ToString());
                return;
            }
        }
        public void GenArbitrary(List<Arbitrary> arbitrary)
        {
            HObject ho_Contour = null, ho_bitrary = null;
            HTuple hv_row = new HTuple(), hv_column = new HTuple();

            HOperatorSet.GenEmptyObj(out ho_Contour);
            HOperatorSet.GenEmptyObj(out ho_bitrary);

            try
            {
                foreach (var item in arbitrary)
                {
                    hv_row = item.dListRow.ToArray();
                    hv_column = item.dListCol.ToArray();

                    HOperatorSet.GenRegionPolygonFilled(out ho_Contour, hv_row, hv_column);
                    HOperatorSet.ConcatObj(ho_bitrary, ho_Contour, out ho_bitrary);
                }
                ClearObjShow();
                m_hWnd.SetDraw("margin");
                m_hWnd.DispObj(m_hImage);
                DispRegion(ho_bitrary, "green");
            }
            catch (HalconException ex)
            {
                MessageFun.ShowMessage(ex.ToString());
                return;
            }
            finally
            {
                ho_Contour?.Dispose();
                ho_bitrary?.Dispose();
            }

        }
        //画点
        public PointF DrawPoint()
        {
            PointF point = new PointF();

            HTuple hv_row = new HTuple(), hv_col = new HTuple(), hv_button = new HTuple();
            HObject ho_Cross = null;
            HOperatorSet.GenEmptyObj(out ho_Cross);
            try
            {
                m_hWnd.DispObj(m_hImage);
                HOperatorSet.GetMbuttonSubPix(m_hWnd, out hv_row, out hv_col, out hv_button);
                HOperatorSet.GenCrossContourXld(out ho_Cross, hv_row, hv_col, 60, 0);
                m_hWnd.DispObj(ho_Cross);
                point.Y = (float)hv_row.D;
                point.X = (float)hv_col.D;
                return point;
            }
            catch (HalconException error)
            {
                MessageBox.Show(error.ToString());
                return point;
            }
            finally
            {
                if (null != ho_Cross) ho_Cross.Dispose();
            }
        }

        //辅助：计算与水平方向夹角
        public bool CalAngleLx(Line line, out double dAngle)
        {
            dAngle = 0;

            HObject ho_Line = null, ho_Arrow = null, ho_LineX = null, ho_ArrowX = null;
            HObject ho_contCircle = null;
            HOperatorSet.GenEmptyObj(out ho_Line);
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_LineX);
            HOperatorSet.GenEmptyObj(out ho_ArrowX);
            HOperatorSet.GenEmptyObj(out ho_contCircle);

            HTuple hv_angle = new HTuple();

            try
            {
                HOperatorSet.GenContourPolygonXld(out ho_Line, ((HTuple)line.dStartRow).TupleConcat(line.dEndRow), ((HTuple)line.dStartCol).TupleConcat(line.dEndCol));
                HOperatorSet.GenContourPolygonXld(out ho_LineX, ((HTuple)line.dStartRow).TupleConcat(line.dStartRow), ((HTuple)line.dStartCol).TupleConcat(imageWidth));
                double dDist = Math.Sqrt(Math.Pow((line.dEndRow - line.dStartRow), 2) + Math.Pow((line.dEndCol - line.dStartCol), 2)) / 30;
                gen_arrow_contour_xld(out ho_Arrow, line.dStartRow, line.dStartCol, line.dEndRow, line.dEndCol, dDist, dDist * 1.5);
                double dDist1 = (imageWidth - line.dStartCol) / 30;
                gen_arrow_contour_xld(out ho_ArrowX, line.dStartRow, line.dStartCol, line.dStartRow, imageWidth, dDist1, dDist1 * 1.5);

                HOperatorSet.AngleLx(line.dStartRow, line.dStartCol, line.dEndRow, line.dEndCol, out hv_angle);
                if (hv_angle.D < 0)
                    HOperatorSet.GenCircleContourXld(out ho_contCircle, line.dStartRow, line.dStartCol, dDist * 2, hv_angle, 0, "positive", 1);
                else
                    HOperatorSet.GenCircleContourXld(out ho_contCircle, line.dStartRow, line.dStartCol, dDist * 2, 0, hv_angle, "positive", 1);
                HOperatorSet.SetTposition(m_hWnd, line.dStartRow + dDist, line.dStartCol + dDist);
                m_hWnd.SetColor("green");
                m_hWnd.WriteString(Math.Round(hv_angle.TupleDeg().D, 2).ToString());
                m_hWnd.SetColor("red");
                m_hWnd.DispObj(ho_Line);
                m_hWnd.DispObj(ho_LineX);
                m_hWnd.SetDraw("fill");
                m_hWnd.DispObj(ho_Arrow);
                m_hWnd.DispObj(ho_ArrowX);
                m_hWnd.DispObj(ho_contCircle);
                return true;
            }
            catch (HalconException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                if (null != ho_Line) ho_Line.Dispose();
                if (null != ho_Arrow) ho_Arrow.Dispose();
                if (null != ho_LineX) ho_LineX.Dispose();
                if (null != ho_ArrowX) ho_ArrowX.Dispose(); ;
                if (null != ho_contCircle) ho_contCircle.Dispose();
            }
        }
        //辅助：计算夹角
        public double DrawAngleLl()
        {
            double dAngle = 0;
            int nRow = 0, nCol = 0, nButton = 0; ;
            double dDist = 0;

            HTuple hv_Angle = new HTuple(), hv_button = new HTuple();
            HTuple hv_angle1 = new HTuple(), hv_angle2 = new HTuple();

            HObject ho_Line1 = null, ho_Line2 = null;
            HObject ho_arrow1 = null, ho_arrow2 = null, ho_contCircle = null;

            HOperatorSet.GenEmptyObj(out ho_Line1);
            HOperatorSet.GenEmptyObj(out ho_Line2);
            HOperatorSet.GenEmptyObj(out ho_arrow1);
            HOperatorSet.GenEmptyObj(out ho_arrow2);
            HOperatorSet.GenEmptyObj(out ho_contCircle);
            try
            {
                m_hWnd.SetColor("red");
                m_hWnd.SetDraw("fill");
                int[] arrayRows = new int[3];
                int[] arrayCols = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    //m_hWnd.DrawPoint(out nRow, out nCol);
                    m_hWnd.GetMbutton(out nRow, out nCol, out nButton);
                    m_hWnd.SetColor("green");
                    m_hWnd.DispCross((double)nRow, (double)nCol, 25, 0);
                    m_hWnd.SetColor("red");
                    arrayRows[i] = nRow;
                    arrayCols[i] = nCol;
                    if (1 == i)
                    {
                        HOperatorSet.GenContourPolygonXld(out ho_Line1, ((HTuple)arrayRows[0]).TupleConcat(arrayRows[1]), ((HTuple)arrayCols[0]).TupleConcat(arrayCols[1]));
                        dDist = Math.Sqrt(Math.Pow((arrayRows[1] - arrayRows[0]), 2) + Math.Pow((arrayCols[1] - arrayCols[0]), 2)) / 30;
                        gen_arrow_contour_xld(out ho_arrow1, arrayRows[1], arrayCols[1], arrayRows[0], arrayCols[0], dDist, dDist * 1.5);
                        HOperatorSet.AngleLx(arrayRows[1], arrayCols[1], arrayRows[0], arrayCols[0], out hv_angle1);
                        m_hWnd.DispObj(ho_Line1);
                        m_hWnd.DispObj(ho_arrow1);
                    }
                    if (2 == i)
                    {
                        HOperatorSet.GenContourPolygonXld(out ho_Line2, ((HTuple)arrayRows[2]).TupleConcat(arrayRows[1]), ((HTuple)arrayCols[2]).TupleConcat(arrayCols[1]));
                        double dDist1 = Math.Sqrt(Math.Pow((arrayRows[1] - arrayRows[2]), 2) + Math.Pow((arrayCols[1] - arrayCols[2]), 2)) / 30;
                        gen_arrow_contour_xld(out ho_arrow2, arrayRows[1], arrayCols[1], arrayRows[2], arrayCols[2], dDist1, dDist1 * 1.5);
                        HOperatorSet.AngleLx(arrayRows[1], arrayCols[1], arrayRows[2], arrayCols[2], out hv_angle2);
                        m_hWnd.DispObj(ho_Line2);
                        m_hWnd.DispObj(ho_arrow2);
                    }
                }

                HOperatorSet.AngleLl(arrayRows[1], arrayCols[1], arrayRows[0], arrayCols[0], arrayRows[1], arrayCols[1], arrayRows[2], arrayCols[2], out hv_Angle);
                if (hv_angle2.D < hv_angle1.D)
                    HOperatorSet.GenCircleContourXld(out ho_contCircle, arrayRows[1], arrayCols[1], dDist * 1.5, hv_angle2, hv_angle1, "positive", 1);
                else
                    HOperatorSet.GenCircleContourXld(out ho_contCircle, arrayRows[1], arrayCols[1], dDist * 1.5, hv_angle1, hv_angle2, "positive", 1);
                m_hWnd.SetColor("green");
                m_hWnd.DispObj(ho_contCircle);
                m_hWnd.SetTposition((int)(arrayRows[1]), (int)(arrayCols[1] + dDist * 2));
                m_hWnd.WriteString(Math.Round(hv_Angle.TupleDeg().D, 2).ToString());
                m_hWnd.SetColor("red");
                return dAngle = hv_Angle.TupleDeg().D;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return dAngle;
            }
            finally
            {
                if (null != ho_Line1) ho_Line1.Dispose();
                if (null != ho_Line2) ho_Line2.Dispose();
                if (null != ho_arrow1) ho_arrow1.Dispose();
                if (null != ho_arrow2) ho_arrow2.Dispose();
                if (null != ho_contCircle) ho_contCircle.Dispose();
            }
        }
        //設置圖片顯示比例
        public void FitImageToWindow(ref double dReslutRow0, ref double dReslutCol0, ref double dReslutRow1, ref double dReslutCol1)
        {
            HTuple hv_width = null, hv_height = null;
            try
            {
                if (dReslutRow0 == 0 && dReslutCol0 == 0 && dReslutRow1 == 0 && dReslutCol1 == 0)
                {
                    if (m_hImage != null)
                    {
                        HOperatorSet.GetImageSize(m_hImage, out hv_width, out hv_height);
                    }
                    else
                    {
                        return;
                    }
                    double dRow0 = 0, dCol0 = 0, dRow1 = hv_height.D - 1, dCol1 = hv_width.D - 1;
                    if (hv_height != null && hv_width != null)
                    {
                        float fImage = (float)hv_width / (float)hv_height;
                        float fWindow = (float)m_hWndCtrl.Width / m_hWndCtrl.Height;
                        if (fWindow > fImage)
                        {
                            float w = fWindow * (float)hv_height;
                            dRow0 = 0;
                            dCol0 = -(w - hv_width) / 2;
                            dRow1 = hv_height - 1;
                            dCol1 = hv_width + (w - hv_width) / 2;
                        }
                        else
                        {
                            float h = (float)hv_width / fWindow;
                            dRow0 = -(h - hv_height) / 2;
                            dCol0 = 0;
                            dRow1 = hv_height + (h - hv_height) / 2;
                            dCol1 = hv_width.D - 1;
                        }
                    }
                    dReslutRow0 = dRow0;
                    dReslutCol0 = dCol0;
                    dReslutRow1 = dRow1;
                    dReslutCol1 = dCol1;

                    imageWidth = hv_width.I;
                    imageHeight = hv_height.I;
                }
                ShowImages(dReslutRow0, dReslutCol0, dReslutRow1, dReslutCol1);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return;
            }


        }

        //读取图像
        public void ReadImage(string strImagePath)
        {
            try
            {
                if (null != m_ObjShow)
                    m_ObjShow.Dispose();
                //m_nLastDraw = -1;
                LoadImageFromFile(strImagePath);
                //HOperatorSet.ReadImage(out m_hImage, strImagePath);
                //m_hWnd.ClearWindow();
                //m_hWnd.DispObj(m_hImage);
                //ShowRegion(m_ObjShow);
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return;
            }
        }
        //清除界面显示
        public void Clearwindow()
        {
            HOperatorSet.ClearWindow(m_hWnd);
        }
        //显示图片
        private void ShowImages(double dRow0, double dCol0, double dRow1, double dCol1)
        {
            try
            {
                if (null != m_hImage)
                {
                    HOperatorSet.SetSystem("flush_graphic", "false");
                    HOperatorSet.ClearWindow(m_hWnd);
                    if (m_hImage != null)
                    {
                        HOperatorSet.SetPart(m_hWnd, dRow0, dCol0, dRow1 - 1, dCol1 - 1);
                        m_hWnd.DispObj(m_hImage);
                    }
                    //ShowRegion(m_ObjShow);
                    HOperatorSet.SetSystem("flush_graphic", "true");
                    HObject emptyObject = null;
                    HOperatorSet.GenEmptyObj(out emptyObject);
                    m_hWnd.DispObj(emptyObject);
                }
            }
            catch (HalconException error)
            {
                error.ToString();
                return;
            }
        }

        //获取主界面最后一次绘制的图形
        public HObject GetLastDrawObj(ObjDraw lastDraw)
        {
            HObject ho_region = null;

            HOperatorSet.GenEmptyObj(out ho_region);
            try
            {
                switch (lastDraw)
                {
                    case ObjDraw.line:
                        HOperatorSet.GenRegionLine(out ho_region, m_line.dStartRow, m_line.dStartCol, m_line.dEndRow, m_line.dEndCol);
                        break;
                    case ObjDraw.rect1:
                        HOperatorSet.GenRectangle1(out ho_region, m_rect1.dRectRow1, m_rect1.dRectCol1, m_rect1.dRectRow2, m_rect1.dRectCol2);
                        break;
                    case ObjDraw.rect2:
                        HOperatorSet.GenRectangle2(out ho_region, m_rect2.dRect2Row, m_rect2.dRect2Col, m_rect2.dPhi, m_rect2.dLength1, m_rect2.dLength2);
                        break;
                    case ObjDraw.circle:
                        HOperatorSet.GenCircle(out ho_region, m_circle.dRow, m_circle.dCol, m_circle.dRadius);
                        break;
                    case ObjDraw.ellipse:
                        HOperatorSet.GenEllipse(out ho_region, m_ellipse.dRow, m_ellipse.dColumn, m_ellipse.dPhi, m_ellipse.dRadius1, m_ellipse.dRadius2);
                        break;
                    default:
                        break;
                }
                m_hWnd.DispObj(ho_region);
                m_ObjShow = m_ObjShow.ConcatObj(ho_region);
                return ho_region;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return ho_region;
            }
            finally
            {
                //if (null != ho_region) ho_region.Dispose();
            }
        }

        //随图片放大缩小显示图形
        private void ShowRegion(HObject obj)
        {
            HObject ho_region = null;

            HOperatorSet.GenEmptyObj(out ho_region);
            try
            {
                roiParam roi = new roiParam();
                roi.objDraw = m_lastDraw;
                ho_region = GetLastDrawObj(m_lastDraw);
                HOperatorSet.GenEmptyObj(out m_ObjShow);

                if (0 != ho_region.CountObj())
                    m_ObjShow = m_ObjShow.ConcatObj(ho_region);
                if (obj.IsInitialized())
                {
                    m_ObjShow = m_ObjShow.ConcatObj(obj);

                }
                m_hWnd.DispObj(m_hImage);
                m_hWnd.DispObj(m_ObjShow);
            }
            catch (HalconException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return;
            }
            finally
            {
                if (null != ho_region) ho_region.Dispose();
            }
        }
        public bool LoadImageFromFile(string strFilePath)
        {

            try
            {
                if (null != m_ObjShow)
                    m_ObjShow.Dispose();
                HOperatorSet.ReadImage(out m_hImage, strFilePath);
                dReslutRow0 = 0;
                dReslutCol0 = 0;
                dReslutRow1 = 0;
                dReslutCol1 = 0;
                FitImageToWindow(ref dReslutRow0, ref dReslutCol0, ref dReslutRow1, ref dReslutCol1);
                HOperatorSet.Rgb1ToGray(m_hImage, out m_GrayImage);
                return true;
            }
            catch (System.Exception ex)
            {
                StaticFun.MessageFun.ShowMessage(ex.ToString());
                return false;
            }

        }
        //縮放图片
        public void ZoomImage(double x, double y, double zoom)
        {
            try
            {
                double lengthC, lengthR;
                double percentC, percentR;

                percentC = (x - dReslutCol0) / (dReslutCol1 - dReslutCol0);
                percentR = (y - dReslutRow0) / (dReslutRow1 - dReslutRow0);

                lengthC = (dReslutCol1 - dReslutCol0) * zoom;
                lengthR = (dReslutRow1 - dReslutRow0) * zoom;

                dReslutCol0 = x - lengthC * percentC;

                dReslutCol1 = x + lengthC * (1 - percentC);

                dReslutRow0 = y - lengthR * percentR;
                dReslutRow1 = y + lengthR * (1 - percentR);
                ShowImages(dReslutRow0, dReslutCol0, dReslutRow1, dReslutCol1);
            }

            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        //平移图像   
        public void MoveImage(Point pMouseDown, Point pMouseUp)
        {
            if (pMouseDown.X == 0 || pMouseDown.Y == 0) //像素坐标
                return;
            //int row, col, button;
            try
            {
                //m_hWnd.GetMposition(out row, out col, out button);

                double dbRowMove, dbColMove;
                dbRowMove = (pMouseDown.Y - pMouseUp.Y);// * (dReslutRow1 - dReslutRow0) / hWndCtrl.Height;//计算光标在X轴拖动的距离
                dbColMove = (pMouseDown.X - pMouseUp.X);// (dReslutCol1 - dReslutCol0) / hWndCtrl.Width;//计算光标在Y轴拖动的距离

                dReslutRow0 = dReslutRow0 + dbRowMove;
                dReslutCol0 = dReslutCol0 + dbColMove;
                dReslutRow1 = dReslutRow1 + dbRowMove;
                dReslutCol1 = dReslutCol1 + dbColMove;

                ShowImages(dReslutRow0, dReslutCol0, dReslutRow1, dReslutCol1);
            }
            catch (HalconException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return;
            }
        }

        //保存结果图片
        public void SaveResultImage(string strFilePath)  //输入文件夹路径，不包含文件名字
        {
            try
            {
                if (string.IsNullOrWhiteSpace(strFilePath))
                    return;
                var folder = Path.GetDirectoryName(strFilePath);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string str = strFilePath + DateTime.Now.ToString().Replace("/", ".").Replace(":", ".");
                str = str.Replace("\\", "/");
                if ("" != strFilePath)
                {
                    //HOperatorSet.DumpWindow(m_hWnd, "bmp", strFilePath);
                    HOperatorSet.DumpWindow(m_hWnd, "jpg", str);
                }
            }
            catch (HalconException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return;
            }
        }
        //保存BMP图片
        public void SaveBmpImage(string strFilePath)  //输入文件夹路径，不包含文件名字
        {
            try
            {
                if (string.IsNullOrWhiteSpace(strFilePath))
                    return;
                var folder = Path.GetDirectoryName(strFilePath);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string str = strFilePath.Replace("\\", "/");
                if ("" != strFilePath)
                {
                    HOperatorSet.WriteImage(m_hImage, "bmp", 0, str);
                }
            }
            catch (HalconException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return;
            }
        }
        public void SaveResultImageToByte(string strFilePath)
        {
            HTuple hv_Pointer = new HTuple(), hv_Type = new HTuple(), hv_Width = new HTuple(), hv_Height = new HTuple();


            HObject ho_image = null;

            HOperatorSet.GenEmptyObj(out ho_image);
            try
            {
                if ("" != strFilePath)
                {
                    HOperatorSet.DumpWindowImage(out ho_image, m_hWnd);
                    HOperatorSet.GetImagePointer1(ho_image, out hv_Pointer, out hv_Type, out hv_Width, out hv_Height);
                    unsafe
                    {
                        byte* p = (byte*)hv_Pointer[0].L;
                        int height = hv_Height.I;
                        int width = hv_Width.I;
                    }


                }
            }
            catch (HalconException error)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                return;
            }
        }

        //保存图片
        public void SaveImage(string strSavePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(strSavePath))
                    return;
                var folder = Path.GetDirectoryName(strSavePath);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string str = strSavePath + DateTime.Now.ToString().Replace("/", ".").Replace(":", ".");
                str = str.Replace("\\", "/");
                if (null != m_hImage)
                {
                    HOperatorSet.WriteImage(m_hImage, "jpg", 0, str);
                    //HOperatorSet.WriteImage(m_hImage, "bmp", 0, str);
                }
            }
            catch (HalconException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return;
            }


        }
        //导入图像文件夹
        public static void list_image_files(string strImageDirectory, HTuple hv_Extensions, out List<string> listImageFiles)
        {
            HTuple hv_HalconImages = null, hv_OS = null;
            HTuple hv_Directories = null, hv_Length = null;
            HTuple hv_network_drive = null, hv_Substring = new HTuple();
            HTuple hv_FileExists = new HTuple(), hv_AllFiles = new HTuple();
            HTuple hv_Selection = new HTuple();
            HTuple hv_Extensions_COPY_INP_TMP = hv_Extensions.Clone();
            string strImageDirectoryTemp = strImageDirectory;
            // HTuple hv_ImageDirectory_COPY_INP_TMP = hv_ImageDirectory.Clone();

            //output parameter:
            //ImageFiles: A tuple of all found image file names
            //
            //            if (Extensions == [] or Extensions == '' or Extensions == 'default')
            //    Extensions:= ['ima', 'tif', 'tiff', 'gif', 'bmp', 'jpg', 'jpeg', 'jp2', 'jxr', 'png', 'pcx', 'ras', 'xwd', 'pbm', 'pnm', 'pgm', 'ppm']
            //   *
            //endif
            if (/*hv_Extensions_COPY_INP_TMP[0] == 0 ||*/ hv_Extensions_COPY_INP_TMP == "" || hv_Extensions_COPY_INP_TMP == "default")
            {
                hv_Extensions_COPY_INP_TMP = new HTuple();
                hv_Extensions_COPY_INP_TMP[0] = "ima";
                hv_Extensions_COPY_INP_TMP[1] = "tif";
                hv_Extensions_COPY_INP_TMP[2] = "tiff";
                hv_Extensions_COPY_INP_TMP[3] = "gif";
                hv_Extensions_COPY_INP_TMP[4] = "bmp";
                hv_Extensions_COPY_INP_TMP[5] = "jpg";
                hv_Extensions_COPY_INP_TMP[6] = "jpeg";
                hv_Extensions_COPY_INP_TMP[7] = "jp2";
                hv_Extensions_COPY_INP_TMP[8] = "jxr";
                hv_Extensions_COPY_INP_TMP[9] = "png";
                hv_Extensions_COPY_INP_TMP[10] = "pcx";
                hv_Extensions_COPY_INP_TMP[11] = "ras";
                hv_Extensions_COPY_INP_TMP[12] = "xwd";
                hv_Extensions_COPY_INP_TMP[13] = "pbm";
                hv_Extensions_COPY_INP_TMP[14] = "pnm";
                hv_Extensions_COPY_INP_TMP[15] = "pgm";
                hv_Extensions_COPY_INP_TMP[16] = "ppm";
                //
            }
            if ("" == strImageDirectoryTemp)
            {
                strImageDirectoryTemp = ".";
            }
            HOperatorSet.GetSystem("image_dir", out hv_HalconImages);
            HOperatorSet.GetSystem("operating_system", out hv_OS);
            if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
            {
                hv_HalconImages = hv_HalconImages.TupleSplit(";");
            }
            else
            {
                hv_HalconImages = hv_HalconImages.TupleSplit(":");
            }
            hv_Directories = strImageDirectoryTemp;
            for (int i = 0; i <= (hv_HalconImages.TupleLength() - 1); i++)
            {
                hv_Directories = hv_Directories.TupleConcat((hv_HalconImages[i] + "/") + strImageDirectoryTemp);
            }
            HOperatorSet.TupleStrlen(hv_Directories, out hv_Length);
            HOperatorSet.TupleGenConst(hv_Length.TupleLength(), 0, out hv_network_drive);
            if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
            {
                for (int i = 0; i <= (hv_Length.TupleLength() - 1); i++)
                {

                    if (new HTuple(hv_Directories[i].ToString()).TupleStrlen() > 1)
                    {
                        HOperatorSet.TupleStrFirstN(hv_Directories[i], 1, out hv_Substring);
                        if (hv_Substring != "//")
                        {
                            if (hv_network_drive == null)
                                hv_network_drive = new HTuple();
                            hv_network_drive[i] = 1;
                        }
                    }
                }
            }
            listImageFiles = new List<string>();
            for (int i = 0; i <= (hv_Directories.TupleLength() - 1); i++)
            {
                HOperatorSet.FileExists(hv_Directories[i], out hv_FileExists);
                if ((int)(hv_FileExists) != 0)
                {
                    HOperatorSet.ListFiles(hv_Directories[i], (new HTuple("files")).TupleConcat(new HTuple()), out hv_AllFiles);
                    HTuple hv_ImageFiles = new HTuple();
                    for (int j = 0; j <= (hv_Extensions_COPY_INP_TMP.TupleLength() - 1); j++)
                    {
                        HOperatorSet.TupleRegexpSelect(hv_AllFiles, (((".*" + (hv_Extensions_COPY_INP_TMP[j])) + "$")).TupleConcat("ignore_case"), out hv_Selection);
                        hv_ImageFiles = hv_ImageFiles.TupleConcat(hv_Selection);
                    }

                    //HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HTuple("\\\\")).TupleConcat("replace_all"), "/", out hv_ImageFiles);
                    //if (hv_network_drive[i].I != 0)
                    //{
                    //    HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HTuple("//")).TupleConcat("replace_all"), "/", out hv_ImageFiles);
                    //    hv_ImageFiles = "/" + hv_ImageFiles;
                    //}
                    //else
                    //{
                    //    HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HTuple("//")).TupleConcat("replace_all"), "/", out hv_ImageFiles);
                    //}
                    for (int n = 0; n < hv_ImageFiles.TupleLength(); n++)
                    {
                        listImageFiles.Add(hv_ImageFiles[n]);
                    }
                    listImageFiles.Sort();
                    return;
                }

            }

            return;
        }

        //定位函数

        //拟合直线

        public bool FitLine(LineParam lineParam, bool bShow, out Line lineOut)
        {
            lineOut.dStartRow = 0;
            lineOut.dStartCol = 0;
            lineOut.dEndRow = 0;
            lineOut.dEndCol = 0;

            HTuple hv_Width = new HTuple(), hv_Height = new HTuple(), hv_MetrologyHandle = new HTuple(), hv_Indices = new HTuple();
            HTuple hv_RowBegin = new HTuple(), hv_ColBegin = new HTuple(), hv_RowEnd = new HTuple(), hv_ColEnd = new HTuple();
            HTuple hv_AllRow = new HTuple(), hv_AllColumn = new HTuple(), hv_Nr = new HTuple(), hv_Nc = new HTuple(), hv_Dist = new HTuple();
            HTuple hv_LineLen = new HTuple();


            HObject ho_MeasureCross = null, ho_MeasureLineContours = null, ho_MeasuredLines = null;
            HObject ho_ContLine = null;

            HOperatorSet.GenEmptyObj(out ho_MeasureCross);
            HOperatorSet.GenEmptyObj(out ho_MeasureLineContours);
            HOperatorSet.GenEmptyObj(out ho_MeasuredLines);
            HOperatorSet.GenEmptyObj(out ho_ContLine);
            try
            {

                HOperatorSet.GetImageSize(m_hImage, out hv_Width, out hv_Height);
                int nThd = lineParam.measure.nMeasureThd;
                int minThd = nThd - 10;
                if (minThd < 1)
                    minThd = 1;
                int maxThd = nThd + 10;
                if (maxThd > 255)
                    maxThd = 255;
                bool bLoop = true;
                while (nThd >= minThd && nThd <= maxThd)
                {
                    HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);
                    HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandle, hv_Width, hv_Height);
                    //m_hWnd.DispLine(lineParam.lineIn.dStartRow, lineParam.lineIn.dStartCol, lineParam.lineIn.dEndRow, lineParam.lineIn.dEndCol);
                    HOperatorSet.AddMetrologyObjectLineMeasure(hv_MetrologyHandle, lineParam.lineIn.dStartRow, lineParam.lineIn.dStartCol, lineParam.lineIn.dEndRow, lineParam.lineIn.dEndCol,
                                 lineParam.measure.nMeasureLen1, lineParam.measure.dMeasureLen2, 1, nThd, new HTuple(), new HTuple(), out hv_Indices);
                    HOperatorSet.DistancePp(lineParam.lineIn.dStartRow, lineParam.lineIn.dStartCol, lineParam.lineIn.dEndRow, lineParam.lineIn.dEndCol, out hv_LineLen);
                    int nMeasureNum = (int)(hv_LineLen.D / 5.0);
                    //设置直线拟合参数
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Indices, "num_instances", 1);
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Indices, "measure_select", lineParam.measure.strSelect);
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Indices, "measure_transition", lineParam.measure.strTransition);
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Indices, "num_measures", nMeasureNum);
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Indices, "min_score", 0.5);
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Indices, "measure_interpolation", "bicubic");
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Indices, "instances_outside_measure_regions", "true");
                    //获取直线拟合结果
                    HOperatorSet.ApplyMetrologyModel(m_hImage, hv_MetrologyHandle);
                    HOperatorSet.GetMetrologyObjectMeasures(out ho_MeasureLineContours, hv_MetrologyHandle, "all", "all", out hv_AllRow, out hv_AllColumn);
                    HOperatorSet.GenCrossContourXld(out ho_MeasureCross, hv_AllRow, hv_AllColumn, 8, 0);
                    HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Indices, "all", "result_type", "row_begin", out hv_RowBegin);
                    HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Indices, "all", "result_type", "column_begin", out hv_ColBegin);
                    HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Indices, "all", "result_type", "row_end", out hv_RowEnd);
                    HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Indices, "all", "result_type", "column_end", out hv_ColEnd);
                    HOperatorSet.GetMetrologyObjectResultContour(out ho_MeasuredLines, hv_MetrologyHandle, "all", "all", 1.5);
                    //清除直线拟合句柄
                    HOperatorSet.ClearMetrologyModel(hv_MetrologyHandle);
                    if (hv_AllRow.TupleLength() > 2)
                        break;
                    if (bLoop)
                    {
                        nThd = minThd - 1;
                        bLoop = false;
                    }
                    nThd = nThd + 1;
                }
                if (0 == hv_RowBegin.TupleLength() && hv_AllRow.TupleLength() >= 2)
                {
                    HOperatorSet.GenContourPolygonXld(out ho_ContLine, hv_AllRow, hv_AllColumn);
                    HOperatorSet.FitLineContourXld(ho_ContLine, "tukey", -1, 0, 5, 2, out hv_RowBegin, out hv_ColBegin, out hv_RowEnd, out hv_ColEnd,
                                                   out hv_Nr, out hv_Nc, out hv_Dist);
                    if (hv_RowBegin.TupleLength() == 0)
                        return false;
                    HOperatorSet.GenRegionLine(out ho_MeasuredLines, hv_RowBegin, hv_ColBegin, hv_RowEnd, hv_ColEnd);

                }
                else if (1 >= hv_AllRow.TupleLength())
                {
                    return false;
                }
                lineOut.dStartRow = hv_RowBegin.D;
                lineOut.dStartCol = hv_ColBegin.D;
                lineOut.dEndRow = hv_RowEnd.D;
                lineOut.dEndCol = hv_ColEnd.D;
                //
                if (bShow)
                {
                    //m_hWnd.DispObj(m_hImage);
                    m_hWnd.SetColor("pink");
                    //m_hWnd.DispObj(ho_MeasureLineContours);
                    //m_hWnd.SetColor("green");
                    m_hWnd.DispObj(ho_MeasureCross);
                }
                //SetShow(m_showParam);
                m_hWnd.SetColor("yellow");
                m_hWnd.DispObj(ho_MeasuredLines);
                return true;
            }
            catch (HalconException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return false;
            }
            finally
            {
                if (null != ho_MeasureCross) ho_MeasureCross.Dispose();
                if (null != ho_MeasureLineContours) ho_MeasureLineContours.Dispose();
                if (null != ho_MeasuredLines) ho_MeasuredLines.Dispose();
                if (null != ho_ContLine) ho_ContLine.Dispose();
            }
        }
        //拟合圆
        public bool FitCircle(CircleParam param, bool bShow, out Circle circleOut)
        {
            circleOut.dRow = 0;
            circleOut.dCol = 0;
            circleOut.dRadius = 0;

            HTuple hv_MetrologyHandle, hv_Width, hv_Height;
            HTuple hv_Index = new HTuple(), hv_Rows = new HTuple(), hv_Cols = new HTuple();
            HTuple hv_CircleRow = new HTuple(), hv_CircleCol = new HTuple(), hv_Radius = new HTuple();
            HTuple hv_StartPhi = new HTuple(), hv_EndPhi = new HTuple(), hv_PointOrder = new HTuple();

            HOperatorSet.GenEmptyObj(out HObject ho_Contours);
            HOperatorSet.GenEmptyObj(out HObject ho_ContRect);
            HOperatorSet.GenEmptyObj(out HObject ho_Circle);
            HOperatorSet.GenEmptyObj(out HObject ho_Cross);
            try
            {
                int nMeasureNum = (int)((Math.PI * 2 * param.circleIn.dRadius) / (2 * param.EPMeasure.dMeasureLen2));
                HOperatorSet.GetImageSize(m_hImage, out hv_Width, out hv_Height);

                HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);
                HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandle, hv_Width, hv_Height);
                HOperatorSet.AddMetrologyObjectCircleMeasure(hv_MetrologyHandle, param.circleIn.dRow, param.circleIn.dCol, param.circleIn.dRadius,
                                                             param.EPMeasure.nMeasureLen1, param.EPMeasure.dMeasureLen2, 1.2, param.EPMeasure.nMeasureThd, new HTuple(), new HTuple(), out hv_Index);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Index, "num_instances", 1);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Index, "num_measures", nMeasureNum);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Index, "measure_transition", param.EPMeasure.strTransition);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Index, "measure_select", param.EPMeasure.strSelect);
                HOperatorSet.ApplyMetrologyModel(m_hImage, hv_MetrologyHandle);
                HOperatorSet.GetMetrologyObjectMeasures(out ho_ContRect, hv_MetrologyHandle, "all", "all", out hv_Rows, out hv_Cols);
                if (0 == hv_Rows.TupleLength())
                    return false;
                HOperatorSet.GenCrossContourXld(out ho_Cross, hv_Rows, hv_Cols, 25, 0);
                HOperatorSet.GetMetrologyObjectResultContour(out ho_Contours, hv_MetrologyHandle, "all", "all", 1.5);
                // DispRegion(ho_Contours, "blue");
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "row", out hv_CircleRow);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "column", out hv_CircleCol);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "radius", out hv_Radius);
                if (0 == hv_CircleRow.TupleLength() && hv_Rows.TupleLength() >= 5)
                {
                    HOperatorSet.GenContourPolygonXld(out ho_Contours, hv_Rows, hv_Cols);
                    HOperatorSet.FitCircleContourXld(ho_Contours, "algebraic", -1, 0, 0, 3, 2, out hv_CircleRow, out hv_CircleCol, out hv_Radius,
                                                     out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);

                }
                else if (0 == hv_CircleRow.TupleLength() && hv_Rows.TupleLength() < 5)
                    return false;
                HOperatorSet.GenCircle(out ho_Circle, hv_CircleRow, hv_CircleCol, hv_Radius);
                HOperatorSet.ClearMetrologyModel(hv_MetrologyHandle);

                circleOut.dRow = hv_CircleRow.D;
                circleOut.dCol = hv_CircleCol.D;
                circleOut.dRadius = hv_Radius.D;

                //显示
                if (bShow)
                {
                    m_hWnd.DispObj(m_hImage);
                    DispRegion(ho_Cross, "blue");
                    DispRegion(ho_ContRect, "green");
                }
                m_hWnd.SetLineWidth(3);
                DispRegion(ho_Circle, "red");
                m_hWnd.DispCross(hv_CircleRow, hv_CircleCol, hv_Radius / 5, 0);
                m_hWnd.SetLineWidth(1);


                return true;
            }
            catch (HalconException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return false;
            }
            finally
            {
                if (null != ho_Contours) ho_Contours.Dispose();
                if (null != ho_ContRect) ho_ContRect.Dispose();
                if (null != ho_Circle) ho_Circle.Dispose();
                if (null != ho_Cross) ho_Cross.Dispose();
            }
        }
        //拟合椭圆
        public bool FitEllipse(EllipseParam param, out Ellipse ellipseOut)
        {
            ellipseOut = new Ellipse();

            HTuple hv_MetrologyHandle, hv_Width, hv_Height;
            HTuple hv_Index, hv_Rows = null, hv_Cols;
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple(), hv_Phi = new HTuple(), hv_Radius1 = new HTuple(), hv_Radius2 = new HTuple();
            HTuple hv_StartPhi = new HTuple(), hv_EndPhi = new HTuple(), hv_PointOrder = new HTuple();

            HObject ho_Contours = null, ho_ContRect = null, ho_Ellipse = null;
            HObject ho_Cross = null;

            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_ContRect);
            HOperatorSet.GenEmptyObj(out ho_Ellipse);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            try
            {
                m_hWnd.DispObj(m_hImage);
                //椭圆周长公式：L=2πb+4（a-b）
                int nMeasureNum = (int)((Math.PI * 2 * param.ellipseIn.dRadius2 + 4 * (param.ellipseIn.dRadius1 - param.ellipseIn.dRadius2)) / (2 * param.EPMeasure.dMeasureLen2));
                HOperatorSet.GetImageSize(m_hImage, out hv_Width, out hv_Height);

                HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);
                HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandle, hv_Width, hv_Height);
                HOperatorSet.AddMetrologyObjectEllipseMeasure(hv_MetrologyHandle, param.ellipseIn.dRow, param.ellipseIn.dColumn, param.ellipseIn.dPhi, param.ellipseIn.dRadius1, param.ellipseIn.dRadius2,
                                                             param.EPMeasure.nMeasureLen1, param.EPMeasure.dMeasureLen2, 1.2, param.EPMeasure.nMeasureThd, new HTuple(), new HTuple(), out hv_Index);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Index, "num_instances", 1);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Index, "num_measures", nMeasureNum);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Index, "measure_transition", param.EPMeasure.strTransition);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Index, "measure_select", param.EPMeasure.strSelect);

                HOperatorSet.ApplyMetrologyModel(m_hImage, hv_MetrologyHandle);
                HOperatorSet.GetMetrologyObjectMeasures(out ho_ContRect, hv_MetrologyHandle, "all", "all", out hv_Rows, out hv_Cols);
                if (0 == hv_Rows.TupleLength())
                    return false;
                HOperatorSet.GenCrossContourXld(out ho_Cross, hv_Rows, hv_Cols, 6, 0);

                HOperatorSet.GetMetrologyObjectResultContour(out ho_Contours, hv_MetrologyHandle, "all", "all", 1.5);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "row", out hv_Row);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "column", out hv_Column);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "phi", out hv_Phi);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "radius1", out hv_Radius1);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "radius2", out hv_Radius2);
                if (0 == hv_Row.TupleLength() && hv_Rows.TupleLength() >= 5)
                {
                    HOperatorSet.GenContourPolygonXld(out ho_Contours, hv_Rows, hv_Cols);
                    HOperatorSet.FitEllipseContourXld(ho_Contours, "fitzgibbon", -1, 0, 0, 200, 3, 2, out hv_Row, out hv_Column, out hv_Phi, out hv_Radius1, out hv_Radius2,
                                                      out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);

                }
                else if (0 == hv_Row.TupleLength() && hv_Rows.TupleLength() < 5)
                    return false;
                HOperatorSet.GenEllipse(out ho_Ellipse, hv_Row, hv_Column, hv_Phi, hv_Radius1, hv_Radius2);
                HOperatorSet.ClearMetrologyModel(hv_MetrologyHandle);

                ellipseOut.dRow = hv_Row.D;
                ellipseOut.dColumn = hv_Column.D;
                ellipseOut.dPhi = hv_Phi.D;
                ellipseOut.dRadius1 = hv_Radius1.D;
                ellipseOut.dRadius2 = hv_Radius2.D;

                //显示
                m_hWnd.SetColor("green");
                m_ObjShow = ho_Cross;
                m_hWnd.DispObj(m_ObjShow);
                m_hWnd.SetColor("red");
                m_hWnd.DispObj(ho_ContRect);
                //SetShow(m_showParam);
                ShowRegion(ho_Ellipse);
                m_hWnd.DispCross(hv_Row, hv_Column, hv_Radius2 / 5, hv_Phi);
                return true;
            }
            catch (HalconException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return false;
            }
            finally
            {
                if (null != ho_Contours) ho_Contours.Dispose();
                if (null != ho_ContRect) ho_ContRect.Dispose();
                if (null != ho_Ellipse) ho_Ellipse.Dispose();
                if (null != ho_Cross) ho_Cross.Dispose();
            }
        }
        //拟合矩形2
        public bool FitRect2(Rect2Param param, out Rect2 rect2)
        {
            rect2 = new Rect2();

            HTuple hv_MetrologyHandle, hv_Width, hv_Height;
            HTuple hv_Index, hv_Rows = null, hv_Cols;
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple(), hv_Phi = new HTuple(), hv_Length1 = new HTuple(), hv_Length2 = new HTuple();
            HTuple hv_StartPhi = new HTuple(), hv_EndPhi = new HTuple(), hv_PointOrder = new HTuple();

            HObject ho_Contours = null, ho_ContRect = null, ho_Rect2 = null;
            HObject ho_Cross = null;

            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_ContRect);
            HOperatorSet.GenEmptyObj(out ho_Rect2);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            try
            {
                m_hWnd.DispObj(m_hImage);
                //长方形周长
                int nMeasureNum = (int)((4 * param.rect2In.dLength1 + param.rect2In.dLength2) / (2 * param.EPMeasure.dMeasureLen2));
                HOperatorSet.GetImageSize(m_hImage, out hv_Width, out hv_Height);

                HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);
                HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandle, hv_Width, hv_Height);
                HOperatorSet.AddMetrologyObjectRectangle2Measure(hv_MetrologyHandle, param.rect2In.dRect2Row, param.rect2In.dRect2Col, param.rect2In.dPhi, param.rect2In.dLength1, param.rect2In.dLength2,
                                                             param.EPMeasure.nMeasureLen1, param.EPMeasure.dMeasureLen2, 1.2, param.EPMeasure.nMeasureThd, new HTuple(), new HTuple(), out hv_Index);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Index, "num_instances", 1);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Index, "num_measures", nMeasureNum);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Index, "measure_transition", param.EPMeasure.strTransition);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, hv_Index, "measure_select", param.EPMeasure.strSelect);

                HOperatorSet.ApplyMetrologyModel(m_hImage, hv_MetrologyHandle);
                HOperatorSet.GetMetrologyObjectMeasures(out ho_ContRect, hv_MetrologyHandle, "all", "all", out hv_Rows, out hv_Cols);
                if (0 == hv_Rows.TupleLength())
                    return false;
                HOperatorSet.GenCrossContourXld(out ho_Cross, hv_Rows, hv_Cols, 6, 0);

                HOperatorSet.GetMetrologyObjectResultContour(out ho_Contours, hv_MetrologyHandle, "all", "all", 1.5);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "row", out hv_Row);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "column", out hv_Column);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "phi", out hv_Phi);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "length1", out hv_Length1);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type", "length2", out hv_Length2);
                if (0 == hv_Row.TupleLength() && hv_Rows.TupleLength() >= 5)
                {
                    HOperatorSet.GenContourPolygonXld(out ho_Contours, hv_Rows, hv_Cols);
                    HOperatorSet.FitRectangle2ContourXld(ho_Contours, "regression", -1, 0, 0, 3, 2, out hv_Row, out hv_Column, out hv_Phi, out hv_Length1, out hv_Length2,
                                                         out hv_PointOrder);

                }
                else if (0 == hv_Row.TupleLength() && hv_Rows.TupleLength() < 5)
                    return false;
                HOperatorSet.GenRectangle2(out ho_Rect2, hv_Row, hv_Column, hv_Phi, hv_Length1, hv_Length2);
                HOperatorSet.ClearMetrologyModel(hv_MetrologyHandle);

                rect2.dRect2Row = hv_Row.D;
                rect2.dRect2Col = hv_Column.D;
                rect2.dPhi = hv_Phi.D;
                rect2.dLength1 = hv_Length1.D;
                rect2.dLength2 = hv_Length2.D;

                //显示
                m_hWnd.SetColor("green");
                m_ObjShow = ho_Cross;
                m_hWnd.DispObj(m_ObjShow);
                m_hWnd.SetColor("red");
                m_hWnd.DispObj(ho_ContRect);
                //SetShow(m_showParam);
                m_ObjShow = m_ObjShow.ConcatObj(ho_Rect2);
                m_hWnd.DispObj(m_ObjShow);
                m_hWnd.DispCross(hv_Row, hv_Column, hv_Length2 / 5, hv_Phi);
                return true;
            }
            catch (HalconException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return false;
            }
            finally
            {
                if (null != ho_Contours) ho_Contours.Dispose();
                if (null != ho_ContRect) ho_ContRect.Dispose();
                if (null != ho_Rect2) ho_Rect2.Dispose();
                if (null != ho_Cross) ho_Cross.Dispose();
            }
        }
        //双线段
        public bool FitDoubleLine(DoubleLineParam param, out DoubleLineOut result)
        {
            /*边缘搜索方法：从中间往两边*/
            result = new DoubleLineOut();

            HObject ho_Rect2 = null, ho_ImageReduced = null;
            HObject ho_Border = null, ho_UnionContours = null, ho_SelectedXLD = null, ho_SortedContours = null;
            HObject ho_ObjSelected = null, ho_line1 = null, ho_line2 = null;

            HTuple hv_Number = null, hv_RowBegin = new HTuple(), hv_Orientation = new HTuple();
            HTuple hv_ColBegin = new HTuple(), hv_RowEnd = new HTuple(), hv_ColEnd = new HTuple(), hv_Nr = new HTuple();
            HTuple hv_Nc = new HTuple(), hv_Dist = new HTuple(), hv_DistMin = new HTuple(), hv_DistMax = new HTuple();
            HTuple hv_AngleLl = new HTuple();

            HOperatorSet.GenEmptyObj(out ho_Rect2);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Border);
            HOperatorSet.GenEmptyObj(out ho_UnionContours);
            HOperatorSet.GenEmptyObj(out ho_SelectedXLD);
            HOperatorSet.GenEmptyObj(out ho_SortedContours);
            HOperatorSet.GenEmptyObj(out ho_ObjSelected);
            HOperatorSet.GenEmptyObj(out ho_line1);
            HOperatorSet.GenEmptyObj(out ho_line2);

            try
            {
                HOperatorSet.GenRectangle2(out ho_Rect2, param.rect2.dRect2Row, param.rect2.dRect2Col, param.rect2.dPhi, param.rect2.dLength1, param.rect2.dLength2);
                m_hWnd.DispObj(ho_Rect2);
                HOperatorSet.ReduceDomain(m_hImage, ho_Rect2, out ho_ImageReduced);
                int nThd = param.nThd;
                int nStep = 5;
                hv_Number = 1;
                while (hv_Number != 2 && nThd < 255)
                {
                    HOperatorSet.ThresholdSubPix(ho_ImageReduced, out ho_Border, nThd);
                    HOperatorSet.UnionAdjacentContoursXld(ho_Border, out ho_UnionContours, 5, 1, "attr_keep");
                    //m_hWnd.DispObj(ho_UnionContours);
                    // HOperatorSet.UnionCollinearContoursXld(ho_Border, out ho_UnionContours, 10, 1, 2, 0.1, "attr_keep");
                    //if (ho_UnionContours.CountObj() > 100)
                    //    continue;
                    HOperatorSet.SelectShapeXld(ho_UnionContours, out ho_SelectedXLD, (new HTuple("rect2_len1")).TupleConcat("rect2_len2"), "and",
                                               ((new HTuple(param.rect2.dLength2)) / 1.5).TupleConcat(0), (new HTuple(param.rect2.dLength2) * 1.5).TupleConcat(10));
                    HOperatorSet.CountObj(ho_SelectedXLD, out hv_Number);
                    nThd = nThd + nStep;
                }
                if (2 != hv_Number.I)
                {
                    return false;
                }
                m_hWnd.DispObj(ho_SelectedXLD);
                //HOperatorSet.SortContoursXld(ho_SelectedXLD, out ho_SortedContours, "upper_left", "true", "column");
                List<Line> listLine = new List<Line>();
                LineParam lineParam = new LineParam();
                lineParam.measure.nMeasureLen1 = 5;
                lineParam.measure.dMeasureLen2 = param.EPMeasure.dMeasureLen2;
                lineParam.measure.nMeasureThd = param.EPMeasure.nMeasureThd;
                switch (param.nTransType)
                {
                    case 0: //中间黑两边白
                        lineParam.measure.strTransition = "positive";
                        break;
                    case 1: //中间白两边黑
                        lineParam.measure.strTransition = "negative";
                        break;
                    default:
                        break;
                }
                lineParam.measure.strSelect = "first";
                for (int i = 1; i <= 2; i++)
                {
                    HOperatorSet.SelectObj(ho_SelectedXLD, out ho_ObjSelected, i);
                    HOperatorSet.FitLineContourXld(ho_ObjSelected, "tukey", -1, 0, 5, 2, out hv_RowBegin, out hv_ColBegin, out hv_RowEnd, out hv_ColEnd,
                                                   out hv_Nr, out hv_Nc, out hv_Dist);

                    lineParam.lineIn.dStartRow = hv_RowBegin.D;
                    lineParam.lineIn.dStartCol = hv_ColBegin.D;
                    lineParam.lineIn.dEndRow = hv_RowEnd.D;
                    lineParam.lineIn.dEndCol = hv_ColEnd.D;

                    Line outLine = new Line();
                    if (!FitLine(lineParam, false, out outLine))
                    {
                        lineParam.lineIn.dStartRow = hv_RowEnd.D;
                        lineParam.lineIn.dStartCol = hv_ColEnd.D;
                        lineParam.lineIn.dEndRow = hv_RowBegin.D;
                        lineParam.lineIn.dEndCol = hv_ColBegin.D;
                        if (!FitLine(lineParam, false, out outLine))
                            return false;
                    }
                    listLine.Add(outLine);
                }
                result.lineLeft = listLine[0];
                result.lineRight = listLine[1];
                HOperatorSet.DistanceSl(listLine[0].dStartRow, listLine[0].dStartCol, listLine[0].dEndRow, listLine[0].dEndCol,
                                        listLine[1].dStartRow, listLine[1].dStartCol, listLine[1].dEndRow, listLine[1].dEndCol, out hv_DistMin, out hv_DistMax);
                result.dDist = (hv_DistMin.D + hv_DistMax.D) / 2.0;
                HOperatorSet.AngleLl(listLine[0].dStartRow, listLine[0].dStartCol, listLine[0].dEndRow, listLine[0].dEndCol,
                                     listLine[1].dEndRow, listLine[1].dEndCol, listLine[1].dStartRow, listLine[1].dStartCol, out hv_AngleLl);
                result.dAngle = hv_AngleLl.TupleDeg().D;

                //显示
                m_hWnd.DispObj(m_hImage);
                m_hWnd.SetColor("green");
                HOperatorSet.GenContourPolygonXld(out ho_line1, ((HTuple)listLine[0].dStartRow).TupleConcat(listLine[0].dEndRow),
                                                                ((HTuple)listLine[0].dStartCol).TupleConcat(listLine[0].dEndCol));
                HOperatorSet.GenContourPolygonXld(out ho_line2, ((HTuple)listLine[1].dStartRow).TupleConcat(listLine[1].dEndRow),
                                                                ((HTuple)listLine[1].dStartCol).TupleConcat(listLine[1].dEndCol));
                //m_hWnd.DispLine(listLine[0].dStartRow, listLine[0].dStartCol, listLine[0].dEndRow, listLine[0].dEndCol);
                //m_hWnd.DispLine(listLine[1].dStartRow, listLine[1].dStartCol, listLine[1].dEndRow, listLine[1].dEndCol);
                HObject obj = ho_line1.ConcatObj(ho_line2);
                ShowRegion(obj);
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                if (null != ho_Rect2) ho_Rect2.Dispose();
                if (null != ho_ImageReduced) ho_ImageReduced.Dispose();
                if (null != ho_Border) ho_Border.Dispose();
                if (null != ho_UnionContours) ho_UnionContours.Dispose();
                if (null != ho_SelectedXLD) ho_SelectedXLD.Dispose();
                if (null != ho_SortedContours) ho_SortedContours.Dispose();
                if (null != ho_ObjSelected) ho_ObjSelected.Dispose();
                if (null != ho_line1) ho_line1.Dispose();
                if (null != ho_line2) ho_line2.Dispose();
            }
        }

        /*直线到直线的距离*/
        /*返回交点，最大距离，最小距离，和夹角*/
        public bool LineInterLine(Line line1, Line line2, out LineInterLineResult result)
        {
            result = new LineInterLineResult();
            result.InterPoint = new PointF();

            HTuple hv_DistMin = new HTuple(), hv_DistMax = new HTuple(), hv_InterRow = new HTuple(), hv_InterCol = new HTuple();
            HTuple hv_isOverlapping = new HTuple(), hv_isParallel = new HTuple(), hv_angle = new HTuple();
            HTuple hv_SegInterRow = new HTuple(), hv_SegInterCol = new HTuple();
            HTuple hv_rowProj1 = new HTuple(), hv_colProj1 = new HTuple();
            HTuple hv_rowProj2 = new HTuple(), hv_colProj2 = new HTuple();

            HObject obj = new HObject(), ho_cross = new HObject(), ho_line1 = new HObject(), ho_line2 = new HObject();
            HObject ho_arrow1 = new HObject(), ho_arrow2 = new HObject(), ho_arrowline1 = new HObject(), ho_arrowline2 = new HObject();

            HOperatorSet.GenEmptyObj(out obj);
            HOperatorSet.GenEmptyObj(out ho_cross);
            HOperatorSet.GenEmptyObj(out ho_line1);
            HOperatorSet.GenEmptyObj(out ho_line2);
            HOperatorSet.GenEmptyObj(out ho_arrow1);
            HOperatorSet.GenEmptyObj(out ho_arrow2);
            HOperatorSet.GenEmptyObj(out ho_arrowline1);
            HOperatorSet.GenEmptyObj(out ho_arrowline2);

            try
            {
                HOperatorSet.IntersectionLl(line1.dStartRow, line1.dStartCol, line1.dEndRow, line1.dEndCol,
                                            line2.dStartRow, line2.dStartCol, line2.dEndRow, line2.dEndCol, out hv_InterRow, out hv_InterCol, out hv_isParallel);
                HOperatorSet.DistanceSl(line1.dStartRow, line1.dStartCol, line1.dEndRow, line1.dEndCol,
                                        line2.dStartRow, line2.dStartCol, line2.dEndRow, line2.dEndCol, out hv_DistMin, out hv_DistMax);
                result.dDistMin = hv_DistMin.D;
                result.dDistMax = hv_DistMax.D;
                if (0 == hv_InterRow.TupleLength())//两套直线平行
                {
                    result.bIsLineInter = false;
                    result.bIsSegInter = false;
                    result.dAngle = 0;
                }
                else
                {
                    HOperatorSet.AngleLl(line1.dStartRow, line1.dStartCol, line1.dEndRow, line1.dEndCol,
                                            line2.dStartRow, line2.dStartCol, line2.dEndRow, line2.dEndCol, out hv_angle);
                    result.dAngle = hv_angle.TupleDeg().D;
                    HOperatorSet.DistanceSl(line1.dStartRow, line1.dStartCol, line1.dEndRow, line1.dEndCol,
                                                line2.dStartRow, line2.dStartCol, line2.dEndRow, line2.dEndCol, out hv_DistMin, out hv_DistMax);

                    HOperatorSet.DistanceSs(line1.dStartRow, line1.dStartCol, line1.dEndRow, line1.dEndCol,
                                            line2.dStartRow, line2.dStartCol, line2.dEndRow, line2.dEndCol, out hv_DistMin, out hv_DistMax);
                    if (0 == hv_DistMin.D)//两条线段相交
                    {
                        result.bIsLineInter = true;
                        result.bIsSegInter = true;
                        result.dDistMin = hv_DistMin.D;
                        result.dDistMax = hv_DistMax.D;
                        HOperatorSet.IntersectionSegments(line1.dStartRow, line1.dStartCol, line1.dEndRow, line1.dEndCol,
                                                          line2.dStartRow, line2.dStartCol, line2.dEndRow, line2.dEndCol, out hv_SegInterRow, out hv_SegInterCol, out hv_isOverlapping);
                        if (0 != hv_InterRow.TupleLength())
                        {
                            result.InterPoint.Y = (float)hv_SegInterRow.D;
                            result.InterPoint.X = (float)hv_SegInterCol.D;
                        }
                    }
                    else
                    {
                        result.bIsLineInter = true;
                        result.bIsSegInter = false;
                        result.InterPoint.Y = (float)hv_InterRow.D;
                        result.InterPoint.X = (float)hv_InterCol.D;
                    }
                }
                //显示
                if (result.bIsLineInter || result.bIsSegInter)
                    HOperatorSet.GenCrossContourXld(out ho_cross, result.InterPoint.Y, result.InterPoint.X, 20, 0);
                HOperatorSet.GenContourPolygonXld(out ho_line1, ((HTuple)line1.dStartRow).TupleConcat(line1.dEndRow), ((HTuple)line1.dStartCol).TupleConcat(line1.dEndCol));
                HOperatorSet.GenContourPolygonXld(out ho_line2, ((HTuple)line2.dStartRow).TupleConcat(line2.dEndRow), ((HTuple)line2.dStartCol).TupleConcat(line2.dEndCol));
                HOperatorSet.ProjectionPl(line1.dStartRow, line1.dStartCol, line2.dStartRow, line2.dStartCol, line2.dEndRow, line2.dEndCol, out hv_rowProj1, out hv_colProj1);
                HOperatorSet.ProjectionPl(line1.dEndRow, line1.dEndCol, line2.dStartRow, line2.dStartCol, line2.dEndRow, line2.dEndCol, out hv_rowProj2, out hv_colProj2);
                gen_arrow_contour_xld(out ho_arrow1, line1.dStartRow, line1.dStartCol, hv_rowProj1, hv_colProj1, 10, 3);
                gen_arrow_contour_xld(out ho_arrow2, line1.dEndRow, line1.dEndCol, hv_rowProj2, hv_colProj2, 10, 3);
                HOperatorSet.GenContourPolygonXld(out ho_arrowline1, ((HTuple)line1.dStartRow).TupleConcat(hv_rowProj1), ((HTuple)line1.dStartCol).TupleConcat(hv_colProj1));
                HOperatorSet.GenContourPolygonXld(out ho_arrowline2, ((HTuple)line1.dEndRow).TupleConcat(hv_rowProj2), ((HTuple)line1.dEndCol).TupleConcat(hv_colProj2));
                obj = obj.ConcatObj(ho_cross);
                obj = obj.ConcatObj(ho_line1);
                obj = obj.ConcatObj(ho_line2);
                //ShowRegion(obj);
                //m_hWnd.SetColor("green");
                //HOperatorSet.GenEmptyObj(out obj);
                obj = obj.ConcatObj(ho_arrow1);
                obj = obj.ConcatObj(ho_arrow2);
                obj = obj.ConcatObj(ho_arrowline1);
                obj = obj.ConcatObj(ho_arrowline2);
                ShowRegion(obj);
                m_hWnd.SetColor("red");
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                if (null != obj) obj.Dispose();
                if (null != ho_cross) ho_cross.Dispose();
                if (null != ho_line1) ho_line1.Dispose();
                if (null != ho_line2) ho_line2.Dispose();
                if (null != ho_arrow1) ho_arrow1.Dispose();
                if (null != ho_arrow2) ho_arrow2.Dispose();
                if (null != ho_arrowline1) ho_arrowline1.Dispose();
                if (null != ho_arrowline2) ho_arrowline2.Dispose();
            }
        }

        /*直线到圆的距离*/
        public static bool LineInterCircle(Line line, Circle circle, out List<PointF> listPoint, out double dDistMin, out double dDistMax)
        {
            listPoint = new List<PointF>();
            dDistMax = -1;
            dDistMin = -1;
            HTuple hv_interRow = new HTuple(), hv_interCol = new HTuple(), hv_DistMin = new HTuple(), hv_DistMax = new HTuple();

            HObject ho_contCircle = new HObject();

            HOperatorSet.GenEmptyObj(out ho_contCircle);
            try
            {
                HOperatorSet.IntersectionLineCircle(line.dStartRow, line.dStartCol, line.dEndRow, line.dEndCol, circle.dRow, circle.dCol, circle.dRadius, 0, 6.28318,
                                                    "positive", out hv_interRow, out hv_interCol);
                if (0 == hv_interRow.TupleLength())
                {//直线与圆不相交
                    HOperatorSet.GenCircleContourXld(out ho_contCircle, circle.dRow, circle.dCol, circle.dRadius, 0, 6.28318, "positive", 1);
                    HOperatorSet.DistanceLc(ho_contCircle, line.dStartRow, line.dStartCol, line.dEndRow, line.dEndCol, out hv_DistMin, out hv_DistMax);
                }
                else
                {
                    int n = hv_interRow.TupleLength();
                    if (1 == n)
                    {
                        HOperatorSet.DistancePs(hv_interRow, hv_interCol, line.dStartRow, line.dStartCol, line.dEndRow, line.dEndCol, out hv_DistMin, out hv_DistMax);
                        if (0 == hv_DistMin.D)
                        {

                        }
                    }
                    else if (2 == n)
                    {

                    }
                    //HOperatorSet.DistancePs()
                }
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }

        }

        /*圆到圆的距离*/
        public bool CircleInterCircle(Circle circle1, Circle circle2, out CircleInterCircleResult result)
        {
            result = new CircleInterCircleResult();
            result.listInterPoint = new List<PointF>();

            HTuple hv_InterRow = new HTuple(), hv_InterCol = new HTuple(), hv_isOverLapping = new HTuple();
            HTuple hv_DistCenters = new HTuple();

            HObject ho_Obj = new HObject(), ho_circle1 = new HObject(), ho_circle2 = new HObject();
            HObject ho_cross1 = new HObject(), ho_cross2 = new HObject();

            HOperatorSet.GenEmptyObj(out ho_Obj);
            HOperatorSet.GenEmptyObj(out ho_circle1);
            HOperatorSet.GenEmptyObj(out ho_circle2);
            HOperatorSet.GenEmptyObj(out ho_circle1);
            HOperatorSet.GenEmptyObj(out ho_circle2);
            try
            {
                HOperatorSet.IntersectionCircles(circle1.dRow, circle1.dCol, circle1.dRadius, 0, 6.28318, "positive",
                                                 circle2.dRow, circle2.dCol, circle2.dRadius, 0, 6.28318, "positive", out hv_InterRow, out hv_InterCol, out hv_isOverLapping);
                //不相交
                if (0 == hv_InterRow.TupleLength())
                {
                    result.bIsIntersect = false;
                }
                else
                {
                    result.bIsIntersect = true;
                    for (int i = 0; i < hv_InterCol.TupleLength(); i++)
                    {
                        PointF point = new PointF();
                        point.X = (float)hv_InterCol.D;
                        point.Y = (float)hv_InterRow.D;
                        result.listInterPoint.Add(point);
                        HOperatorSet.GenCrossContourXld(out ho_cross1, hv_InterRow[i], hv_InterCol[i], circle1.dRadius / 5, 0);
                        ShowRegion(ho_cross1);
                    }
                }
                //计算两个圆心的距离
                HOperatorSet.DistancePp(circle1.dRow, circle1.dCol, circle2.dRow, circle2.dCol, out hv_DistCenters);
                result.dDistCenter = hv_DistCenters.D;

                //显示
                HOperatorSet.GenCircle(out ho_circle1, circle1.dRow, circle1.dCol, circle1.dRadius);
                HOperatorSet.GenCircle(out ho_circle2, circle2.dRow, circle2.dCol, circle2.dRadius);
                ho_Obj = ho_Obj.ConcatObj(ho_circle1);
                ho_Obj = ho_Obj.ConcatObj(ho_circle2);
                HOperatorSet.GenCrossContourXld(out ho_cross1, circle1.dRow, circle1.dCol, circle1.dRadius / 3, 0);
                HOperatorSet.GenCrossContourXld(out ho_cross2, circle2.dRow, circle2.dCol, circle2.dRadius / 3, 0);
                ho_Obj = ho_Obj.ConcatObj(ho_cross1);
                ho_Obj = ho_Obj.ConcatObj(ho_cross2);
                ShowRegion(ho_Obj);
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                if (null != ho_Obj) ho_Obj.Dispose();
                if (null != ho_circle1) ho_circle1.Dispose();
                if (null != ho_circle2) ho_circle2.Dispose();
                if (null != ho_circle1) ho_circle1.Dispose();
                if (null != ho_circle2) ho_circle2.Dispose();
            }
        }

        /*点到直线的距离*/
        public static bool DistPL(double dRow, double dCol, Line line, out double dDistPl)
        {
            dDistPl = -1;
            HTuple hv_Distance = null;

            try
            {
                dDistPl = -1;
                HOperatorSet.DistancePl(dRow, dCol, line.dStartRow, line.dStartCol, line.dEndRow, line.dEndCol, out hv_Distance);
                if (0 == hv_Distance.TupleLength())
                {
                    return false;
                }
                dDistPl = hv_Distance.D;
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        //
        public static void list_image_files(HTuple hv_ImageDirectory, HTuple hv_Extensions, HTuple hv_Options, out HTuple hv_ImageFiles)
        {
            // Local iconic variables 

            // Local control variables 

            HTuple hv_HalconImages = null, hv_OS = null;
            HTuple hv_Directories = null, hv_Index = null, hv_Length = null;
            HTuple hv_network_drive = null, hv_Substring = new HTuple();
            HTuple hv_FileExists = new HTuple(), hv_AllFiles = new HTuple();
            HTuple hv_i = new HTuple(), hv_Selection = new HTuple();
            HTuple hv_Extensions_COPY_INP_TMP = hv_Extensions.Clone();
            HTuple hv_ImageDirectory_COPY_INP_TMP = hv_ImageDirectory.Clone();

            // Initialize local and output iconic variables 
            //This procedure returns all files in a given directory
            //with one of the suffixes specified in Extensions.
            //
            //input parameters:
            //ImageDirectory: as the name says
            //   If a tuple of directories is given, only the images in the first
            //   existing directory are returned.
            //   If a local directory is not found, the directory is searched
            //   under %HALCONIMAGES%/ImageDirectory. If %HALCONIMAGES% is not set,
            //   %HALCONROOT%/images is used instead.
            //Extensions: A string tuple containing the extensions to be found
            //   e.g. ['png','tif',jpg'] or others
            //If Extensions is set to 'default' or the empty string '',
            //   all image suffixes supported by HALCON are used.
            //Options: as in the operator list_files, except that the 'files'
            //   option is always used. Note that the 'directories' option
            //   has no effect but increases runtime, because only files are
            //   returned.
            //
            //output parameter:
            //ImageFiles: A tuple of all found image file names
            //
            if ((int)((new HTuple((new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(""))))).TupleOr(new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(
                "default")))) != 0)
            {
                hv_Extensions_COPY_INP_TMP = new HTuple();
                hv_Extensions_COPY_INP_TMP[0] = "ima";
                hv_Extensions_COPY_INP_TMP[1] = "tif";
                hv_Extensions_COPY_INP_TMP[2] = "tiff";
                hv_Extensions_COPY_INP_TMP[3] = "gif";
                hv_Extensions_COPY_INP_TMP[4] = "bmp";
                hv_Extensions_COPY_INP_TMP[5] = "jpg";
                hv_Extensions_COPY_INP_TMP[6] = "jpeg";
                hv_Extensions_COPY_INP_TMP[7] = "jp2";
                hv_Extensions_COPY_INP_TMP[8] = "jxr";
                hv_Extensions_COPY_INP_TMP[9] = "png";
                hv_Extensions_COPY_INP_TMP[10] = "pcx";
                hv_Extensions_COPY_INP_TMP[11] = "ras";
                hv_Extensions_COPY_INP_TMP[12] = "xwd";
                hv_Extensions_COPY_INP_TMP[13] = "pbm";
                hv_Extensions_COPY_INP_TMP[14] = "pnm";
                hv_Extensions_COPY_INP_TMP[15] = "pgm";
                hv_Extensions_COPY_INP_TMP[16] = "ppm";
                //
            }
            if ((int)(new HTuple(hv_ImageDirectory_COPY_INP_TMP.TupleEqual(""))) != 0)
            {
                hv_ImageDirectory_COPY_INP_TMP = ".";
            }
            HOperatorSet.GetSystem("image_dir", out hv_HalconImages);
            HOperatorSet.GetSystem("operating_system", out hv_OS);
            if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
            {
                hv_HalconImages = hv_HalconImages.TupleSplit(";");
            }
            else
            {
                hv_HalconImages = hv_HalconImages.TupleSplit(":");
            }
            hv_Directories = hv_ImageDirectory_COPY_INP_TMP.Clone();
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_HalconImages.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_Directories = hv_Directories.TupleConcat(((hv_HalconImages.TupleSelect(hv_Index)) + "/") + hv_ImageDirectory_COPY_INP_TMP);
            }
            HOperatorSet.TupleStrlen(hv_Directories, out hv_Length);
            HOperatorSet.TupleGenConst(new HTuple(hv_Length.TupleLength()), 0, out hv_network_drive);
            if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
            {
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                {
                    if ((int)(new HTuple(((((hv_Directories.TupleSelect(hv_Index))).TupleStrlen())).TupleGreater(1))) != 0)
                    {
                        HOperatorSet.TupleStrFirstN(hv_Directories.TupleSelect(hv_Index), 1, out hv_Substring);
                        if ((int)(new HTuple(hv_Substring.TupleEqual("//"))) != 0)
                        {
                            if (hv_network_drive == null)
                                hv_network_drive = new HTuple();
                            hv_network_drive[hv_Index] = 1;
                        }
                    }
                }
            }
            hv_ImageFiles = new HTuple();
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Directories.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
            {
                HOperatorSet.FileExists(hv_Directories.TupleSelect(hv_Index), out hv_FileExists);
                if ((int)(hv_FileExists) != 0)
                {
                    HOperatorSet.ListFiles(hv_Directories.TupleSelect(hv_Index), (new HTuple("files")).TupleConcat(
                        hv_Options), out hv_AllFiles);
                    hv_ImageFiles = new HTuple();
                    for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Extensions_COPY_INP_TMP.TupleLength())) - 1); hv_i = (int)hv_i + 1)
                    {
                        HOperatorSet.TupleRegexpSelect(hv_AllFiles, (((".*" + (hv_Extensions_COPY_INP_TMP.TupleSelect(
                            hv_i))) + "$")).TupleConcat("ignore_case"), out hv_Selection);
                        hv_ImageFiles = hv_ImageFiles.TupleConcat(hv_Selection);
                    }
                    HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HTuple("\\\\")).TupleConcat(
                        "replace_all"), "/", out hv_ImageFiles);
                    if ((int)(hv_network_drive.TupleSelect(hv_Index)) != 0)
                    {
                        HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HTuple("//")).TupleConcat(
                            "replace_all"), "/", out hv_ImageFiles);
                        hv_ImageFiles = "/" + hv_ImageFiles;
                    }
                    else
                    {
                        HOperatorSet.TupleRegexpReplace(hv_ImageFiles, (new HTuple("//")).TupleConcat(
                            "replace_all"), "/", out hv_ImageFiles);
                    }

                    return;
                }
            }

            return;
        }

        ////圆与直线的交点
        //public static bool Circle_Inter_Line(CircleLineInterParam circleLineParam, out PointF intersection)
        //{
        //    intersection = new PointF();

        //    HTuple hv_InterRow = new HTuple(), hv_InterCol = new HTuple();
        //    HTuple hv_Dist1 = new HTuple(), hv_Dist2 = new HTuple();
        //    HTuple hv_Row = new HTuple(), hv_Col = new HTuple();

        //    try
        //    {
        //        Circle circle = new Circle();
        //        if (!FitCircle(circleLineParam.circleParam, out circle))
        //            return false;
        //        Line line = new Line();
        //        if (!FitLine(circleLineParam.lineParam, out line))
        //            return false;
        //        HOperatorSet.IntersectionLineCircle(line.dStartRow, line.dStartCol, line.dEndRow, line.dEndCol,  circle.dRow, circle.dCol, circle.dRadius,
        //                                            new HTuple(0), new HTuple(Math.PI * 2), "positive", out hv_InterRow, out hv_InterCol);
        //        if (1 >= hv_InterRow.TupleLength())
        //            return false;

        //        double dHalfRow = (hv_InterRow[0] + hv_InterRow[1]) / 2.0;
        //        double dHalfCol = (hv_InterCol[0] + hv_InterCol[1]) / 2.0;
        //        HOperatorSet.DistancePp(dHalfRow, dHalfCol, line.dStartRow, line.dStartCol, out hv_Dist1);
        //        HOperatorSet.DistancePp(dHalfRow, dHalfCol, line.dEndRow, line.dEndCol, out hv_Dist2);
        //        double dLineRow=0, dLineCol = 0;
        //        if (hv_Dist1.D > hv_Dist2.D)
        //        {
        //            dLineRow = line.dStartRow;
        //            dLineCol = line.dStartCol;
        //        }
        //        else
        //        {
        //            dLineRow = line.dEndRow;
        //            dLineCol = line.dEndCol;
        //        }
        //        HOperatorSet.IntersectionSegmentCircle(dLineRow, dLineCol, dHalfRow, dHalfCol, circle.dRow, circle.dCol, circle.dRadius,
        //                                            new HTuple(0), new HTuple(Math.PI * 2), "positive", out hv_Row, out hv_Col);
        //        if (1 != hv_Row.TupleLength())
        //            return false;
        //        intersection.X = (float)hv_Col.D;
        //        intersection.Y = (float)hv_Row.D;
        //        //显示
        //        //m_hWnd.DispObj(m_hImage);
        //        m_hWnd.SetColor("green");
        //        m_hWnd.DispCircle(circle.dRow, circle.dCol, circle.dRadius);
        //        m_hWnd.DispLine(dLineRow, dLineCol, dHalfRow, dHalfCol);
        //        m_hWnd.SetColor("red");
        //        m_hWnd.DispCross(hv_Row, hv_Col, 20, 0);
        //        return true; 
        //    }
        //    catch(HalconException error)
        //    {
        //        return false;
        //    }
        //    finally
        //    {

        //    }
        //}

        ////直线与直线的交点
        //public static bool Line_Inter_Line(LineLineInterParam lineLineParam, out PointP intersection)
        //{
        //    intersection = new PointP();
        //    HTuple hv_InterRow = new HTuple(), hv_InterCol = new HTuple(), hv_isParallel = new HTuple();

        //    try
        //    {
        //        Line line1 = new Line();
        //        if (!FitLine(lineLineParam.arrayLineParam[0], out line1))
        //            return false;
        //        Line line2 = new Line();
        //        if (!FitLine(lineLineParam.arrayLineParam[1], out line2))
        //            return false;
        //        HOperatorSet.IntersectionLl(line1.dStartRow, line1.dStartCol, line1.dEndRow, line1.dEndCol,
        //                                    line2.dStartRow, line2.dStartCol, line2.dEndRow, line2.dEndCol, out hv_InterRow, out hv_InterCol, out hv_isParallel);
        //        if (1 != hv_InterRow.TupleLength())
        //            return false;
        //        intersection.dRow = hv_InterRow.D;
        //        intersection.dCol = hv_InterCol.D;
        //        //显示
        //        m_hWnd.SetColor("green");
        //        m_hWnd.DispLine(line1.dStartRow, line1.dStartCol, line1.dEndRow, line1.dEndCol);
        //        m_hWnd.DispLine(line2.dStartRow, line2.dStartCol, line2.dEndRow, line2.dEndCol);
        //        m_hWnd.SetColor("red");
        //        m_hWnd.DispCross(hv_InterRow, hv_InterCol, 40, 0);

        //        return true;

        //    }
        //    catch(HalconException error)
        //    {
        //        ErrorPrinter.WriteLine(string.Format("直线与直线的交点计算出错！錯誤信息：{0}", error.Message), ETraceDisplay.LISTBOX);
        //        return false;
        //    }
        //}


        public void set_display_font(HTuple hv_Size, HTuple hv_Font, HTuple hv_Bold, HTuple hv_Slant)
        {
            HTuple hv_OS = null, hv_Fonts = new HTuple();
            HTuple hv_Style = null, hv_Exception = new HTuple(), hv_AvailableFonts = null;
            HTuple hv_Fdx = null, hv_Indices = new HTuple();
            HTuple hv_Font_COPY_INP_TMP = hv_Font.Clone();
            HTuple hv_Size_COPY_INP_TMP = hv_Size.Clone();
            //
            //Input parameters:
            //WindowHandle: The graphics window for which the font will be set
            //Size: The font size. If Size=-1, the default of 16 is used.
            //Bold: If set to 'true', a bold font is used
            //Slant: If set to 'true', a slanted font is used
            //
            HOperatorSet.GetSystem("operating_system", out hv_OS);
            if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
            {
                hv_Size_COPY_INP_TMP = 16;
            }
            if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
            {
                //Restore previous behaviour
                hv_Size_COPY_INP_TMP = ((1.13677 * hv_Size_COPY_INP_TMP)).TupleInt();
            }
            else
            {
                hv_Size_COPY_INP_TMP = hv_Size_COPY_INP_TMP.TupleInt();
            }
            if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))) != 0)
            {
                hv_Fonts = new HTuple();
                hv_Fonts[0] = "Courier";
                hv_Fonts[1] = "Courier 10 Pitch";
                hv_Fonts[2] = "Courier New";
                hv_Fonts[3] = "CourierNew";
                hv_Fonts[4] = "Liberation Mono";
            }
            else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
            {
                hv_Fonts = new HTuple();
                hv_Fonts[0] = "Consolas";
                hv_Fonts[1] = "Menlo";
                hv_Fonts[2] = "Courier";
                hv_Fonts[3] = "Courier 10 Pitch";
                hv_Fonts[4] = "FreeMono";
                hv_Fonts[5] = "Liberation Mono";
            }
            else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
            {
                hv_Fonts = new HTuple();
                hv_Fonts[0] = "Luxi Sans";
                hv_Fonts[1] = "DejaVu Sans";
                hv_Fonts[2] = "FreeSans";
                hv_Fonts[3] = "Arial";
                hv_Fonts[4] = "Liberation Sans";
            }
            else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
            {
                hv_Fonts = new HTuple();
                hv_Fonts[0] = "Times New Roman";
                hv_Fonts[1] = "Luxi Serif";
                hv_Fonts[2] = "DejaVu Serif";
                hv_Fonts[3] = "FreeSerif";
                hv_Fonts[4] = "Utopia";
                hv_Fonts[5] = "Liberation Serif";
            }
            else
            {
                hv_Fonts = hv_Font_COPY_INP_TMP.Clone();
            }
            hv_Style = "";
            if ((int)(new HTuple(hv_Bold.TupleEqual("true"))) != 0)
            {
                hv_Style = hv_Style + "Bold";
            }
            else if ((int)(new HTuple(hv_Bold.TupleNotEqual("false"))) != 0)
            {
                hv_Exception = "Wrong value of control parameter Bold";
                throw new HalconException(hv_Exception);
            }
            if ((int)(new HTuple(hv_Slant.TupleEqual("true"))) != 0)
            {
                hv_Style = hv_Style + "Italic";
            }
            else if ((int)(new HTuple(hv_Slant.TupleNotEqual("false"))) != 0)
            {
                hv_Exception = "Wrong value of control parameter Slant";
                throw new HalconException(hv_Exception);
            }
            if ((int)(new HTuple(hv_Style.TupleEqual(""))) != 0)
            {
                hv_Style = "Normal";
            }
            HOperatorSet.QueryFont(m_hWnd, out hv_AvailableFonts);
            hv_Font_COPY_INP_TMP = "";
            for (hv_Fdx = 0; (int)hv_Fdx <= (int)((new HTuple(hv_Fonts.TupleLength())) - 1); hv_Fdx = (int)hv_Fdx + 1)
            {
                hv_Indices = hv_AvailableFonts.TupleFind(hv_Fonts.TupleSelect(hv_Fdx));
                if ((int)(new HTuple((new HTuple(hv_Indices.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)(new HTuple(((hv_Indices.TupleSelect(0))).TupleGreaterEqual(0))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(hv_Fdx);
                        break;
                    }
                }
            }
            if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(""))) != 0)
            {
                throw new HalconException("Wrong value of control parameter Font");
            }
            hv_Font_COPY_INP_TMP = (((hv_Font_COPY_INP_TMP + "-") + hv_Style) + "-") + hv_Size_COPY_INP_TMP;
            HOperatorSet.SetFont(m_hWnd, hv_Font_COPY_INP_TMP);

            return;
        }

        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
       HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {

            // Local iconic variables 

            // Local control variables 

            HTuple hv_GenParamName = null, hv_GenParamValue = null;
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_CoordSystem_COPY_INP_TMP = hv_CoordSystem.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();

            // Initialize local and output iconic variables 
            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   A tuple of values is allowed to display text at different
            //   positions.
            //Column: The column coordinate of the desired text position
            //   A tuple of values is allowed to display text at different
            //   positions.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically...
            //   - if |Row| == |Column| == 1: for each new textline
            //   = else for each text position.
            //Box: If Box[0] is set to 'true', the text is written within an orange box.
            //     If set to' false', no box is displayed.
            //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
            //       the text is written in a box of that color.
            //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
            //       'true' -> display a shadow in a default color
            //       'false' -> display no shadow
            //       otherwise -> use given string as color string for the shadow color
            //
            //It is possible to display multiple text strings in a single call.
            //In this case, some restrictions apply:
            //- Multiple text positions can be defined by specifying a tuple
            //  with multiple Row and/or Column coordinates, i.e.:
            //  - |Row| == n, |Column| == n
            //  - |Row| == n, |Column| == 1
            //  - |Row| == 1, |Column| == n
            //- If |Row| == |Column| == 1,
            //  each element of String is display in a new textline.
            //- If multiple positions or specified, the number of Strings
            //  must match the number of positions, i.e.:
            //  - Either |String| == n (each string is displayed at the
            //                          corresponding position),
            //  - or     |String| == 1 (The string is displayed n times).
            //
            //
            //Convert the parameters for disp_text.
            if ((int)((new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(new HTuple())))) != 0)
            {

                return;
            }
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            //
            //Convert the parameter Box to generic parameters.
            hv_GenParamName = new HTuple();
            hv_GenParamValue = new HTuple();
            hv_GenParamName = hv_GenParamName.TupleConcat("box");
            hv_GenParamValue = hv_GenParamValue.TupleConcat("false");
            //if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(0))) != 0)
            //{
            //    if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleEqual("false"))) != 0)
            //    {
            //        //Display no box
            //        hv_GenParamName = hv_GenParamName.TupleConcat("box");
            //        hv_GenParamValue = hv_GenParamValue.TupleConcat("false");
            //    }
            //    else if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleNotEqual("true"))) != 0)
            //    {
            //        //Set a color other than the default.
            //        hv_GenParamName = hv_GenParamName.TupleConcat("box_color");
            //        hv_GenParamValue = hv_GenParamValue.TupleConcat(hv_Box.TupleSelect(0));
            //    }
            //}
            //if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(1))) != 0)
            //{
            //    if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleEqual("false"))) != 0)
            //    {
            //        //Display no shadow.
            //        hv_GenParamName = hv_GenParamName.TupleConcat("shadow");
            //        hv_GenParamValue = hv_GenParamValue.TupleConcat("false");
            //    }
            //    else if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleNotEqual("true"))) != 0)
            //    {
            //        //Set a shadow color other than the default.
            //        hv_GenParamName = hv_GenParamName.TupleConcat("shadow_color");
            //        hv_GenParamValue = hv_GenParamValue.TupleConcat(hv_Box.TupleSelect(1));
            //    }
            //}
            //Restore default CoordSystem behavior.
            if ((int)(new HTuple(hv_CoordSystem_COPY_INP_TMP.TupleNotEqual("window"))) != 0)
            {
                hv_CoordSystem_COPY_INP_TMP = "image";
            }
            //
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(""))) != 0)
            {
                //disp_text does not accept an empty string for Color.
                hv_Color_COPY_INP_TMP = new HTuple();
            }
            //
            HOperatorSet.DispText(hv_WindowHandle, hv_String, hv_CoordSystem_COPY_INP_TMP,
                    hv_Row_COPY_INP_TMP, hv_Column_COPY_INP_TMP, hv_Color_COPY_INP_TMP, "box",
                    hv_GenParamValue);

            return;
        }

        /// <summary>
        /// 字符显示设置
        /// </summary>
        /// <param name="nFontSize"></param> 字体大小
        /// <param name="nRowSite"></param> 字体显示位置Row
        /// <param name="nColSite"></param> 字体显示位置Col
        /// <param name="str"></param>      要显示的字符
        /// <param name="color"></param>    显示的颜色
        /// <param name="bBold"></param>    是否加粗
        /// <param name="bBox"></param>     是否背景填充
        public void WriteStringtoImage(int nFontSize, int nRowSite, int nColSite, string str, string color, bool bBold = false, bool bBox = false, string strColor = "")
        {
            HTuple hv_Font = new HTuple();

            try
            {

                //设置字体大小
                // hv_Font = m_hWnd.QueryFont();
                // hv_FontWithSize = HTuple(hv_Font[0]) + "20-";
                // HTuple strFontWithSize = hv_Font[0];

                // strFontWithSize = strFontWithSize + nFontSize.ToString()+"-";
                //* -*-0-0-1-0-GB2312_CHARSET-
                //*-*-0-[下划线]-[中划线]-[粗体]-[编码格式]    

                string strFontWithSize = "-Microsoft YaHei UI-" + nFontSize + " * -*-0-0-0-0-GB2312_CHARSET-";
                if (bBold)
                {
                    strFontWithSize = "-Microsoft YaHei UI-" + nFontSize + " * -*-0-1-0-1-GB2312_CHARSET-";
                }
                m_hWnd.SetFont(strFontWithSize);

                //if(bBox)
                //{
                //    disp_message(m_hWnd, str, "image", nRowSite, nColSite, color, "true");
                //}
                //else
                //{
                //设置字体显示的位置
                m_hWnd.SetTposition(nRowSite, nColSite);
                // 字体内容
                m_hWnd.SetColor(color);
                m_hWnd.WriteString(str);
                //}
            }
            catch (HalconException error)
            {
                MessageFun.ShowMessage(error.ToString());
                //ErrorPrinter.WriteLine(string.Format("显示字符出错！错误信息：{0}", error.Message), ETraceDisplay.LISTBOX);
            }
            m_hWnd.SetLineWidth(1);
            return;
        }
        #endregion

        #region 标定
        /*圆点标定板*/
        public bool CirclePointCalib(CirclePointCalibParam param, out double dMeanDiam, out CalibrateResult calibResult)
        {
            dMeanDiam = 0;
            calibResult = new CalibrateResult();

            HTuple hv_DiamVal = new HTuple(), hv_areas = new HTuple(), hv_areaDiff = new HTuple();
            HTuple hv_area = new HTuple(), hv_row = new HTuple(), hv_column = new HTuple();

            HObject ho_Region = null, ho_SelectedRegions = null, ho_UnionRegions = null;
            HObject ho_RegionDilaX = null, ho_RegionDilaXConnect = null;
            HObject ho_SelObj = null, ho_RegionInter = null, ho_SortRegion = null;
            HObject ho_RegionDilaY = null;

            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_UnionRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionDilaX);
            HOperatorSet.GenEmptyObj(out ho_RegionDilaXConnect);
            HOperatorSet.GenEmptyObj(out ho_SelObj);
            HOperatorSet.GenEmptyObj(out ho_RegionInter);
            HOperatorSet.GenEmptyObj(out ho_SortRegion);
            HOperatorSet.GenEmptyObj(out ho_RegionDilaY);

            try
            {
                HTuple hv_channels = new HTuple();
                HOperatorSet.CountChannels(m_hImage, out hv_channels);
                if (1 != hv_channels.I)
                    HOperatorSet.Rgb1ToGray(m_hImage, out m_hImage);
                HOperatorSet.Threshold(m_hImage, out ho_Region, 0, param.nThd);
                HOperatorSet.Connection(ho_Region, out ho_Region);
                if (param.dCircularity > 1)
                    param.dCircularity = 1;
                HOperatorSet.SelectShape(ho_Region, out ho_SelectedRegions, "circularity", "and", param.dCircularity, 1);
                HOperatorSet.RegionFeatures(ho_SelectedRegions, "area", out hv_areas);
                HTuple hv_max = hv_areas.TupleMax();
                HTuple hv_min = hv_areas.TupleMin();
                HOperatorSet.TupleDifference(hv_areas, hv_max.TupleConcat(hv_min), out hv_areaDiff);
                double dAreaMean = hv_areaDiff.TupleMean();
                HOperatorSet.SelectShape(ho_SelectedRegions, out ho_SelectedRegions, "area", "and", dAreaMean * 0.8, dAreaMean * 2);
                HOperatorSet.RegionFeatures(ho_SelectedRegions, "max_diameter", out hv_DiamVal);
                m_hWnd.DispObj(m_hImage);
                m_hWnd.DispObj(ho_SelectedRegions);
                dMeanDiam = (param.dCircleDiam / hv_DiamVal).TupleMean();
                //
                HOperatorSet.Union1(ho_SelectedRegions, out ho_UnionRegions);
                //计算X方向的圆点间距
                HOperatorSet.DilationRectangle1(ho_UnionRegions, out ho_RegionDilaX, imageWidth / 2, 1);
                HOperatorSet.Connection(ho_RegionDilaX, out ho_RegionDilaXConnect);
                int nCol = ho_RegionDilaXConnect.CountObj();
                HTuple hv_xLen = new HTuple();
                for (int i = 1; i < nCol; i++)
                {
                    HOperatorSet.SelectObj(ho_RegionDilaXConnect, out ho_SelObj, i);
                    HOperatorSet.Intersection(ho_SelectedRegions, ho_SelObj, out ho_RegionInter);
                    HOperatorSet.SortRegion(ho_RegionInter, out ho_SortRegion, "first_point", "true", "column");
                    HOperatorSet.AreaCenter(ho_SortRegion, out hv_area, out hv_row, out hv_column);
                    for (int j = 0; j < hv_column.TupleLength() - 2; j++)
                    {
                        double dXLen = Math.Sqrt(Math.Pow((hv_column[j + 1] - hv_column[j]), 2) + Math.Pow((hv_row[j + 1] - hv_row[j]), 2));
                        hv_xLen = hv_xLen.TupleConcat(dXLen);
                    }
                }
                calibResult.dXCalib = param.dCircleSpace / hv_xLen.TupleMean();
                //计算Y方向的圆点间距
                HOperatorSet.DilationRectangle1(ho_SelectedRegions, out ho_RegionDilaY, 1, imageHeight / 2);
                HOperatorSet.Connection(ho_RegionDilaY, out ho_RegionDilaY);
                int nRow = ho_RegionDilaY.CountObj();
                HTuple hv_yLen = new HTuple();
                for (int m = 1; m < nRow; m++)
                {
                    HOperatorSet.SelectObj(ho_RegionDilaY, out ho_SelObj, m);
                    HOperatorSet.Intersection(ho_SelectedRegions, ho_SelObj, out ho_RegionInter);
                    ho_SortRegion.Dispose();
                    HOperatorSet.SortRegion(ho_RegionInter, out ho_SortRegion, "first_point", "true", "row");
                    HOperatorSet.AreaCenter(ho_SortRegion, out hv_area, out hv_row, out hv_column);
                    for (int n = 0; n < hv_row.TupleLength() - 2; n++)
                    {
                        double dYLen = Math.Sqrt(Math.Pow((hv_column[n + 1] - hv_column[n]), 2) + Math.Pow((hv_row[n + 1] - hv_row[n]), 2));
                        hv_yLen = hv_yLen.TupleConcat(dYLen);
                    }
                }
                calibResult.dYCalib = param.dCircleSpace / hv_yLen.TupleMean();
                return true;
            }
            catch (HalconException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return false;
            }
            finally
            {
                if (null != ho_Region) ho_Region.Dispose();
                if (null != ho_SelectedRegions) ho_SelectedRegions.Dispose();
                if (null != ho_UnionRegions) ho_UnionRegions.Dispose();
                if (null != ho_RegionDilaX) ho_RegionDilaX.Dispose();
                if (null != ho_RegionDilaXConnect) ho_RegionDilaXConnect.Dispose();
                if (null != ho_SelObj) ho_SelObj.Dispose();
                if (null != ho_RegionInter) ho_RegionInter.Dispose();
                if (null != ho_SortRegion) ho_SortRegion.Dispose();
                if (null != ho_RegionDilaY) ho_RegionDilaY.Dispose();
            }
        }
        /*棋盘格标定*/
        public bool CheckerCalib(CheckerCalibParam param, out CalibrateResult calibResult)
        {
            calibResult = new CalibrateResult();

            HObject ho_Region = null, ho_RegionOpening = null, ho_ConnectedRegions = null;
            HObject ho_SelectedRegions = null, ho_SortedRegions = null, ho_ObjectSelected = null;

            HTuple hv_Number = new HTuple(), hv_Areas = new HTuple(), hv_AreaDiff = new HTuple();
            HTuple hv_Dist1 = new HTuple(), hv_Dist2 = new HTuple();

            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);

            try
            {
                HOperatorSet.Threshold(m_hImage, out ho_Region, 0, param.nThreshold);
                HOperatorSet.OpeningCircle(ho_Region, out ho_RegionOpening, 2);
                HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions);
                HOperatorSet.RegionFeatures(ho_ConnectedRegions, "area", out hv_Areas);
                HTuple hv_max = hv_Areas.TupleMax();
                HTuple hv_Min = hv_Areas.TupleMin();
                HOperatorSet.TupleDifference(hv_Areas, hv_max.TupleConcat(hv_Min), out hv_AreaDiff);
                HTuple hv_AreaMean = hv_AreaDiff.TupleMean();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, new HTuple("rectangularity").TupleConcat("area"), "and",
                                                                                      new HTuple(param.dRectangularity).TupleConcat(hv_AreaMean * 0.8),
                                                                                      new HTuple(1).TupleConcat(hv_AreaMean * 2));
                HOperatorSet.ShapeTrans(ho_SelectedRegions, out ho_SelectedRegions, "rectangle2");
                HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                if (0 == hv_Number.I)
                    return false;
                HOperatorSet.SortRegion(ho_SelectedRegions, out ho_SortedRegions, "first_point", "true", "row");

                HTuple hv_x_Len = new HTuple();
                HTuple hv_y_Len = new HTuple();

                for (int i = 1; i <= hv_Number.I; i++)
                {
                    HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected, i);
                    if (1 == ho_ObjectSelected.CountObj())
                    {
                        HOperatorSet.RegionFeatures(ho_ObjectSelected, "rect2_len1", out hv_Dist1);
                        HOperatorSet.RegionFeatures(ho_ObjectSelected, "rect2_len2", out hv_Dist2);
                        hv_x_Len = (hv_x_Len.TupleConcat(2.0 * hv_Dist1));
                        hv_y_Len = (hv_y_Len.TupleConcat(2.0 * hv_Dist2));
                    }
                }
                if (0 == hv_x_Len.TupleLength())
                    return false;
                calibResult.dXCalib = param.dLength / (hv_x_Len.TupleMean().D);
                calibResult.dYCalib = param.dLength / (hv_y_Len.TupleMean().D);
                //显示
                m_hWnd.DispObj(m_hImage);
                m_hWnd.DispObj(ho_SortedRegions);

                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                if (null != ho_Region) ho_Region.Dispose();
                if (null != ho_RegionOpening) ho_RegionOpening.Dispose();
                if (null != ho_ConnectedRegions) ho_ConnectedRegions.Dispose();
                if (null != ho_SelectedRegions) ho_SelectedRegions.Dispose();
                if (null != ho_SortedRegions) ho_SortedRegions.Dispose();
                if (null != ho_ObjectSelected) ho_ObjectSelected.Dispose();
            }
        }
        /*二维码标定*/
        public bool BarCode2DCalib(Code2DCalibParam param, out CalibrateResult calibResult)
        {
            calibResult = new CalibrateResult();

            HTuple hv_width = new HTuple(), hv_height = new HTuple();
            HTuple hv_DataCodeHandle = new HTuple(), hv_ResultHandles = new HTuple(), hv_DecodedDataString = new HTuple();
            HTuple hv_TupleNum = new HTuple(), hv_area = new HTuple(), hv_row = new HTuple(), hv_column = new HTuple(), hv_pointOrder = new HTuple();
            HTuple hv_indices = new HTuple(), hv_dist = new HTuple(), hv_SubString = new HTuple(), hv_SubString1 = new HTuple();
            HTuple hv_BarCodeDist = new HTuple();

            HObject ho_SymbolXlds = null, ho_SymbolRegion = null, ho_HRegion = null, ho_HSortRegion = null;
            HObject ho_VRegion = null, ho_VSortRegion = null;
            HObject ho_selectObj = null, ho_ReiongInter = null, ho_ImgReduced = null;

            HOperatorSet.GenEmptyObj(out ho_SymbolXlds);
            HOperatorSet.GenEmptyObj(out ho_SymbolRegion);
            HOperatorSet.GenEmptyObj(out ho_HRegion);
            HOperatorSet.GenEmptyObj(out ho_HSortRegion);
            HOperatorSet.GenEmptyObj(out ho_VRegion);
            HOperatorSet.GenEmptyObj(out ho_VSortRegion);
            HOperatorSet.GenEmptyObj(out ho_selectObj);
            HOperatorSet.GenEmptyObj(out ho_ReiongInter);
            HOperatorSet.GenEmptyObj(out ho_ImgReduced);

            try
            {
                HOperatorSet.GetImageSize(m_hImage, out hv_width, out hv_height);
                HOperatorSet.CreateDataCode2dModel(param.strCodeType, new HTuple(), new HTuple(), out hv_DataCodeHandle);
                HOperatorSet.FindDataCode2d(m_hImage, out ho_SymbolXlds, hv_DataCodeHandle, "stop_after_result_num", 99, out hv_ResultHandles, out hv_DecodedDataString);
                if (0 == ho_SymbolXlds.CountObj())
                    return false;
                HOperatorSet.AreaCenterXld(ho_SymbolXlds, out hv_area, out hv_row, out hv_column, out hv_pointOrder);
                for (int i = 0; i < ho_SymbolXlds.CountObj(); i++)
                {
                    m_hWnd.SetTposition(hv_row.TupleSelect(i).TupleInt().I, hv_column.TupleSelect(i).TupleInt().I);
                    m_hWnd.WriteString(hv_DecodedDataString.TupleSelect(i));
                }
                m_hWnd.DispObj(ho_SymbolXlds);
                HOperatorSet.GenRegionContourXld(ho_SymbolXlds, out ho_SymbolRegion, "filled");
                HOperatorSet.Union1(ho_SymbolRegion, out ho_SymbolRegion);
                HOperatorSet.DilationRectangle1(ho_SymbolRegion, out ho_HRegion, hv_width, 40);
                HOperatorSet.Connection(ho_HRegion, out ho_HRegion);
                HOperatorSet.SortRegion(ho_HRegion, out ho_HSortRegion, "first_point", "true", "row");
                //X方向
                HTuple hv_xCalib = new HTuple();
                for (int i = 1; i <= ho_HSortRegion.CountObj(); i++)
                {
                    ho_SymbolXlds.Dispose();
                    hv_ResultHandles = new HTuple();
                    hv_DecodedDataString = new HTuple();
                    HOperatorSet.SelectObj(ho_HSortRegion, out ho_selectObj, i);
                    //  m_hWnd.DispObj(ho_selectObj);
                    HOperatorSet.ReduceDomain(m_hImage, ho_selectObj, out ho_ImgReduced);
                    HOperatorSet.FindDataCode2d(ho_ImgReduced, out ho_SymbolXlds, hv_DataCodeHandle, "stop_after_result_num", 3, out hv_ResultHandles, out hv_DecodedDataString);
                    if (1 >= hv_DecodedDataString.TupleLength())
                        return false;
                    HOperatorSet.TupleNumber(hv_DecodedDataString, out hv_TupleNum);
                    HOperatorSet.AreaCenterXld(ho_SymbolXlds, out hv_area, out hv_row, out hv_column, out hv_pointOrder);
                    HOperatorSet.TupleSortIndex(hv_column, out hv_indices);
                    for (int n = 0; n < hv_column.TupleLength() - 1; n++)
                    {

                        HOperatorSet.DistancePp(hv_row.TupleSelect(hv_indices[n]), hv_column.TupleSelect(hv_indices[n]),
                                                hv_row.TupleSelect(hv_indices[n + 1]), hv_column.TupleSelect(hv_indices[n + 1]), out hv_dist);
                        HOperatorSet.TupleSplit(hv_DecodedDataString.TupleSelect(hv_indices[n]), ",", out hv_SubString);
                        HOperatorSet.TupleSplit(hv_DecodedDataString.TupleSelect(hv_indices[n + 1]), ",", out hv_SubString1);
                        HOperatorSet.TupleNumber(hv_SubString, out hv_SubString);
                        HOperatorSet.TupleNumber(hv_SubString1, out hv_SubString1);
                        HOperatorSet.DistancePp(hv_SubString[0].D, hv_SubString[1].D, hv_SubString1[0].D, hv_SubString1[1].D, out hv_BarCodeDist);
                        hv_xCalib = hv_xCalib.TupleConcat(hv_BarCodeDist / hv_dist);
                    }
                }
                if (1 == ho_HSortRegion.CountObj())
                {
                    calibResult.dXCalib = hv_xCalib.TupleMean().D;
                    calibResult.dYCalib = calibResult.dXCalib;
                    return true;
                }
                //Y方向
                HOperatorSet.DilationRectangle1(ho_SymbolRegion, out ho_VRegion, 40, hv_height);
                HOperatorSet.Connection(ho_VRegion, out ho_VRegion);
                HOperatorSet.SortRegion(ho_VRegion, out ho_VSortRegion, "first_point", "true", "col");
                HTuple hv_yCalib = new HTuple();
                for (int i = 1; i <= ho_VSortRegion.CountObj(); i++)
                {
                    ho_SymbolXlds.Dispose();
                    hv_ResultHandles = new HTuple();
                    hv_DecodedDataString = new HTuple();
                    HOperatorSet.SelectObj(ho_VSortRegion, out ho_selectObj, i);
                    // m_hWnd.DispObj(ho_selectObj);
                    HOperatorSet.ReduceDomain(m_hImage, ho_selectObj, out ho_ImgReduced);
                    HOperatorSet.FindDataCode2d(ho_ImgReduced, out ho_SymbolXlds, hv_DataCodeHandle, "stop_after_result_num", 3, out hv_ResultHandles, out hv_DecodedDataString);
                    if (1 >= hv_DecodedDataString.TupleLength())
                        return false;
                    HOperatorSet.TupleNumber(hv_DecodedDataString, out hv_TupleNum);
                    HOperatorSet.AreaCenterXld(ho_SymbolXlds, out hv_area, out hv_row, out hv_column, out hv_pointOrder);
                    HOperatorSet.TupleSortIndex(hv_row, out hv_indices);
                    for (int n = 0; n < hv_row.TupleLength() - 1; n++)
                    {

                        HOperatorSet.DistancePp(hv_row.TupleSelect(hv_indices[n]), hv_column.TupleSelect(hv_indices[n]),
                                                hv_row.TupleSelect(hv_indices[n + 1]), hv_column.TupleSelect(hv_indices[n + 1]), out hv_dist);
                        HOperatorSet.TupleSplit(hv_DecodedDataString.TupleSelect(hv_indices[n]), ",", out hv_SubString);
                        HOperatorSet.TupleSplit(hv_DecodedDataString.TupleSelect(hv_indices[n + 1]), ",", out hv_SubString1);
                        HOperatorSet.TupleNumber(hv_SubString, out hv_SubString);
                        HOperatorSet.TupleNumber(hv_SubString1, out hv_SubString1);
                        HOperatorSet.DistancePp(hv_SubString[0].D, hv_SubString[1].D, hv_SubString1[0].D, hv_SubString1[1].D, out hv_BarCodeDist);
                        hv_yCalib = hv_yCalib.TupleConcat(hv_BarCodeDist / hv_dist);
                    }
                }
                calibResult.dXCalib = hv_xCalib.TupleMean().D;
                calibResult.dYCalib = hv_yCalib.TupleMean().D;
                return true;

            }
            catch (HalconException error)
            {
                StaticFun.MessageFun.ShowMessage(error.ToString());
                return false;
            }
            finally
            {
                if (null != ho_SymbolXlds) ho_SymbolXlds.Dispose();
                if (null != ho_SymbolRegion) ho_SymbolRegion.Dispose();
                if (null != ho_HRegion) ho_HRegion.Dispose();
                if (null != ho_HSortRegion) ho_HSortRegion.Dispose();
                if (null != ho_VRegion) ho_VRegion.Dispose();
                if (null != ho_VSortRegion) ho_VSortRegion.Dispose();
                if (null != ho_selectObj) ho_selectObj.Dispose();
                if (null != ho_ReiongInter) ho_ReiongInter.Dispose();
                if (null != ho_ImgReduced) ho_ImgReduced.Dispose();
            }

        }

        public static bool LocateCalib(double[] dCol, double[] dRow, //9 Image Point
                                       double[] dX, double[] dY,     //9 machine point 
                                       double[] homMat2d)
        {
            homMat2d = new double[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            HTuple hv_Col = new HTuple(), hv_Row = new HTuple(), hv_X = new HTuple(), hv_Y = new HTuple();
            HTuple hv_homMat2d = new HTuple();
            try
            {
                for (int i = 0; i < 9; i++)
                {
                    hv_Col[i] = dCol[i];
                    hv_Row[i] = dRow[i];
                    hv_X[i] = dX[i];
                    hv_Y[i] = dY[i];
                }
                HOperatorSet.VectorToHomMat2d(hv_Col, hv_Row, hv_X, hv_Y, out hv_homMat2d);
                if (hv_homMat2d.TupleLength() != 0)
                {
                    for (int i = 0; i < hv_homMat2d.TupleLength(); i++)
                    {
                        homMat2d[i] = hv_homMat2d[i];
                    }
                }
                return true;
            }
            catch (System.Exception error)
            {
                MessageBox.Show("错误：" + error.ToString());
                return false;
            }
        }
        #endregion

        public void SetImageSize(int width, int height)
        {
            imageWidth = width;
            imageHeight = height;
        }

        public void GetImage(byte[] imageData)
        {
            try
            {
                if (imageData == null || imageData.Length < 1)
                    throw new Exception(string.Format("图像数据转换失败！原因：图像数据为空！"));

                if (m_hImage == null)
                    m_hImage = new HObject();

                GCHandle gc = GCHandle.Alloc(imageData, GCHandleType.Pinned);
                IntPtr ptr = gc.AddrOfPinnedObject();

                if (gc.IsAllocated)
                {
                    gc.Free();
                }

                m_hImage.Dispose();
                HOperatorSet.GenImage1(out m_hImage, "byte", imageWidth, imageHeight, ptr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void GetImage(byte[] imageData, HWindowControl hWndCtrl)
        {
            try
            {
                if (imageData == null || imageData.Length < 1)
                    throw new Exception(string.Format("图像数据转换失败！原因：图像数据为空！"));

                if (m_hImage == null)
                    m_hImage = new HObject();

                GCHandle gc = GCHandle.Alloc(imageData, GCHandleType.Pinned);
                IntPtr ptr = gc.AddrOfPinnedObject();

                if (gc.IsAllocated)
                {
                    gc.Free();
                }

                m_hImage.Dispose();
                HOperatorSet.GenImage1(out m_hImage, "byte", imageWidth, imageHeight, ptr);

                double dReslutRow0 = 0, dReslutCol0 = 0, dReslutRow1 = 0, dReslutCol1 = 0;
                FitImageToWindow(ref dReslutRow0, ref dReslutCol0, ref dReslutRow1, ref dReslutCol1);
                //ShowImages(dReslutRow0, dReslutCol0, dReslutRow1, dReslutCol1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ShowRect1(Rect1 rect1)
        {
            HObject ho_Rect1 = null;

            HOperatorSet.GenEmptyObj(out ho_Rect1);
            try
            {
                HOperatorSet.GenRectangle1(out ho_Rect1, rect1.dRectRow1, rect1.dRectCol1, rect1.dRectRow2, rect1.dRectCol2);
                m_hWnd.DispObj(ho_Rect1);
            }
            catch (Exception ex)
            {
                (ex.Message + ex.StackTrace).ToLog();
            }
            finally
            {
                ho_Rect1?.Dispose();
            }
        }

        public void ShowRect2(Rect2 rect2)
        {
            HOperatorSet.GenEmptyObj(out HObject ho_Rect2);
            try
            {
                HOperatorSet.GenRectangle2(out ho_Rect2, rect2.dRect2Row, rect2.dRect2Col, rect2.dPhi, rect2.dLength1, rect2.dLength2);
                m_hWnd.DispObj(m_hImage);
                m_hWnd.DispObj(ho_Rect2);
            }
            catch (Exception ex)
            {
                (ex.Message + ex.StackTrace).ToLog();
            }
            finally
            {
                ho_Rect2?.Dispose();
            }
        }
        private bool ColorSpaceTrans_extend(HObject ho_ImageIn, string strColorSpace, int nSpaceSel, out HObject ho_ImageOut)
        {
            ho_ImageOut = new HObject();

            HTuple hv_channels = new HTuple();

            HObject ho_GrayImage = null;
            HObject ho_ImageRed = null, ho_ImageGreen = null, ho_ImageBlue = null;
            HObject ho_ImageResult1 = null, ho_ImageResult2 = null, ho_ImageResult3 = null;

            HOperatorSet.GenEmptyObj(out ho_GrayImage);
            HOperatorSet.GenEmptyObj(out ho_ImageRed);
            HOperatorSet.GenEmptyObj(out ho_ImageGreen);
            HOperatorSet.GenEmptyObj(out ho_ImageBlue);
            HOperatorSet.GenEmptyObj(out ho_ImageResult1);
            HOperatorSet.GenEmptyObj(out ho_ImageResult2);
            HOperatorSet.GenEmptyObj(out ho_ImageResult3);

            try
            {
                HObject ho_ImgOrgTemp = ho_ImageIn.Clone();
                HOperatorSet.CountChannels(ho_ImgOrgTemp, out hv_channels);
                if (3 != hv_channels.I)
                    return false;
                HOperatorSet.Decompose3(ho_ImgOrgTemp, out ho_ImageRed, out ho_ImageGreen, out ho_ImageBlue);
                if ("" == strColorSpace || null == strColorSpace)
                {
                    m_hWnd.DispObj(ho_ImageIn);
                    return true;
                }
                else if ("gray" == strColorSpace)
                {
                    HOperatorSet.Rgb1ToGray(ho_ImgOrgTemp, out ho_GrayImage);
                }
                else if ("rgb" == strColorSpace)
                {
                    ho_ImageResult1 = ho_ImageRed.Clone();
                    ho_ImageResult2 = ho_ImageGreen.Clone();
                    ho_ImageResult3 = ho_ImageBlue.Clone();
                }
                else
                {
                    HOperatorSet.TransFromRgb(ho_ImageRed, ho_ImageGreen, ho_ImageBlue, out ho_ImageResult1, out ho_ImageResult2, out ho_ImageResult3, strColorSpace);
                }
                switch (nSpaceSel)
                {
                    case 0:
                        ho_ImageOut = ho_GrayImage.Clone();
                        m_hWnd.DispObj(ho_GrayImage);
                        //m_hImage = ho_ImgOrgTemp.Clone();
                        break;
                    case 1:
                        ho_ImageOut = ho_ImageResult1.Clone();
                        m_hWnd.DispObj(ho_ImageResult1);
                        //m_hImage = ho_ImageResult1.Clone();
                        break;
                    case 2:
                        ho_ImageOut = ho_ImageResult2.Clone();
                        m_hWnd.DispObj(ho_ImageResult2);
                        //m_hImage = ho_ImageResult2.Clone();
                        break;
                    case 3:
                        ho_ImageOut = ho_ImageResult3.Clone();
                        m_hWnd.DispObj(ho_ImageResult3);
                        //m_hImage = ho_ImageResult3.Clone();
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                if (null != ho_GrayImage) ho_GrayImage.Dispose();
                if (null != ho_ImageRed) ho_ImageRed.Dispose();
                if (null != ho_ImageGreen) ho_ImageGreen.Dispose();
                if (null != ho_ImageBlue) ho_ImageBlue.Dispose();
                if (null != ho_ImageResult1) ho_ImageResult1.Dispose();
                if (null != ho_ImageResult2) ho_ImageResult2.Dispose();
                if (null != ho_ImageResult3) ho_ImageResult3.Dispose();
            }
        }
        private bool ImageProcessAll_extend_1(List<ImageProType> listCheck, ImageProcessParam param, out HObject ho_ImageResult, out HObject ho_Region, out HObject ho_xld)
        {
            ho_ImageResult = null;
            ho_Region = null;
            ho_xld = null;
            HOperatorSet.GenEmptyObj(out ho_ImageResult);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_xld);

            HObject ho_ImageTemp = null, ho_ImageOut = null;
            HObject ho_RegionIn = null, ho_RegionOut = null;
            HObject ho_XLDIn = null, ho_XLDOut = null;
            HObject ho_ImgReduced = null;

            HOperatorSet.GenEmptyObj(out ho_ImgReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageTemp);
            HOperatorSet.GenEmptyObj(out ho_ImageOut);
            HOperatorSet.GenEmptyObj(out ho_RegionIn);
            HOperatorSet.GenEmptyObj(out ho_RegionOut);
            HOperatorSet.GenEmptyObj(out ho_XLDIn);
            HOperatorSet.GenEmptyObj(out ho_XLDOut);

            try
            {
                ho_ImageTemp = m_hImage.Clone();
                int n = listCheck.Count();
                for (int i = 0; i < n; i++)
                {
                    ImageProType check = listCheck[i];
                    if (check == ImageProType.roi)
                    {
                        HObject obj = GetLastDrawObj(m_lastDraw);
                        ho_ImageOut.Dispose();
                        HOperatorSet.ReduceDomain(ho_ImageTemp, obj, out ho_ImageOut);
                        ho_ImageTemp = ho_ImageOut.Clone();
                    }
                    else if (check == ImageProType.color_space_trans)
                    {
                        ho_ImageOut.Dispose();
                        ColorSpaceTrans_extend(ho_ImageTemp, param.color_space_trans.strColorSpace, param.color_space_trans.nSpaceSel, out ho_ImageOut);
                        ho_ImageTemp = ho_ImageOut.Clone();

                    }
                    else if (check == ImageProType.mean)
                    {
                        ho_ImageOut.Dispose();
                        HOperatorSet.MeanImage(ho_ImageTemp, out ho_ImageOut, param.mean.nMaskWidth, param.mean.nMaskHeight);
                        ho_ImageTemp = ho_ImageOut.Clone();
                    }
                    else if (check == ImageProType.median)
                    {
                        ho_ImageOut.Dispose();
                        HOperatorSet.MedianImage(ho_ImageTemp, out ho_ImageOut, param.median.strMaskType, param.median.nMaskRadius, "mirrored");
                        ho_ImageTemp = ho_ImageOut.Clone();
                    }
                    else if (check == ImageProType.equhist)
                    {
                        ho_ImageOut.Dispose();
                        HOperatorSet.EquHistoImage(ho_ImageTemp, out ho_ImageOut);
                        ho_ImageTemp = ho_ImageOut.Clone();
                    }
                    else if (check == ImageProType.scale)
                    {
                        ho_ImageOut.Dispose();
                        scale_image_range(ho_ImageTemp, out ho_ImageOut, param.scale.nScaleMin, param.scale.nScaleMax);
                        ho_ImageTemp = ho_ImageOut.Clone();
                    }
                    else if (check == ImageProType.threshold)
                    {
                        HOperatorSet.Threshold(ho_ImageTemp, out ho_RegionOut, param.threshold.nThdMin, param.threshold.nThdMax);
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();
                    }
                    else if (check == ImageProType.dyn_threshold)
                    {

                    }
                    else if (check == ImageProType.connect)
                    {
                        ho_RegionOut.Dispose();
                        HOperatorSet.Connection(ho_RegionIn, out ho_RegionOut);
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();
                    }
                    else if (check == ImageProType.union)
                    {
                        ho_RegionOut.Dispose();
                        HOperatorSet.Union1(ho_RegionIn, out ho_RegionOut);
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();
                    }
                    else if (check == ImageProType.dilation)
                    {
                        ho_RegionOut.Dispose();
                        if ("circle" == param.dilation.strFilterType)
                        {
                            HOperatorSet.DilationCircle(ho_RegionIn, out ho_RegionOut, param.dilation.dMaskRadius);
                        }
                        else if ("rectangle1" == param.dilation.strFilterType)
                        {
                            HOperatorSet.DilationRectangle1(ho_RegionIn, out ho_RegionOut, param.dilation.nMaskWidth, param.dilation.nMaskHeight);
                        }
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();
                    }
                    else if (check == ImageProType.erosion)
                    {
                        ho_RegionOut.Dispose();
                        if ("circle" == param.erosion.strFilterType)
                        {
                            HOperatorSet.ErosionCircle(ho_RegionIn, out ho_RegionOut, param.erosion.dMaskRadius);
                        }
                        else if ("rectangle1" == param.erosion.strFilterType)
                        {
                            HOperatorSet.DilationRectangle1(ho_RegionIn, out ho_RegionOut, param.erosion.nMaskWidth, param.erosion.nMaskHeight);
                        }
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();
                    }
                    else if (check == ImageProType.opening)
                    {
                        ho_RegionOut.Dispose();
                        if ("circle" == param.opening.strFilterType)
                        {
                            HOperatorSet.OpeningCircle(ho_RegionIn, out ho_RegionOut, param.opening.dMaskRadius);
                        }
                        else if ("rectangle1" == param.erosion.strFilterType)
                        {
                            HOperatorSet.OpeningRectangle1(ho_RegionIn, out ho_RegionOut, param.opening.nMaskWidth, param.opening.nMaskHeight);
                        }
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();
                    }
                    else if (check == ImageProType.closing)
                    {
                        ho_RegionOut.Dispose();
                        if ("circle" == param.closing.strFilterType)
                        {
                            HOperatorSet.ClosingCircle(ho_RegionIn, out ho_RegionOut, param.closing.dMaskRadius);
                        }
                        else if ("rectangle1" == param.closing.strFilterType)
                        {
                            HOperatorSet.ClosingRectangle1(ho_RegionIn, out ho_RegionOut, param.closing.nMaskWidth, param.closing.nMaskHeight);
                        }
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();
                    }
                    else if (check == ImageProType.thd_sub_pixel)
                    {
                        HOperatorSet.ThresholdSubPix(ho_ImageTemp, out ho_XLDOut, param.thd_sub_pixel.nThd);
                        ho_XLDIn = ho_XLDOut.Clone();
                    }
                    else if (check == ImageProType.edges_sub_pixel)
                    {
                        HOperatorSet.EdgesSubPix(ho_ImageTemp, out ho_XLDOut, param.edges_sub_pixel.strFilterType, param.edges_sub_pixel.dAlpha, param.edges_sub_pixel.nMin, param.edges_sub_pixel.nMax);
                    }
                }
                m_hWnd.DispObj(ho_ImageOut);
                m_hWnd.DispObj(ho_RegionOut);
                m_hWnd.DispObj(ho_XLDOut);

                ho_ImageResult = ho_ImageOut.Clone();
                ho_Region = ho_RegionOut.Clone();
                ho_xld = ho_XLDOut.Clone(); ;
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {

            }
        }

        private bool ImageProcessAll_extend(List<ImageProType> listCheck, ImageProcessParam param, out List<imageProPre> listResult)
        {
            listResult = new List<imageProPre>();

            HObject ho_ImageTemp = null, ho_ImageOut = null;
            HObject ho_RegionIn = null, ho_RegionOut = null;
            HObject ho_XLDIn = null, ho_XLDOut = null;
            HObject ho_ImgReduced = null;

            HOperatorSet.GenEmptyObj(out ho_ImgReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageTemp);
            HOperatorSet.GenEmptyObj(out ho_ImageOut);
            HOperatorSet.GenEmptyObj(out ho_RegionIn);
            HOperatorSet.GenEmptyObj(out ho_RegionOut);
            HOperatorSet.GenEmptyObj(out ho_XLDIn);
            HOperatorSet.GenEmptyObj(out ho_XLDOut);

            try
            {
                ho_ImageTemp = m_hImage.Clone();
                int n = listCheck.Count();
                for (int i = 0; i < n; i++)
                {
                    ImageProType check = listCheck[i];
                    if (check == ImageProType.roi)
                    {
                        HObject ho_roi = new HObject();
                        if (param.roi.objDraw == ObjDraw.rect1)
                        {
                            HOperatorSet.GenRectangle1(out ho_roi, param.roi.rect1.dRectRow1, param.roi.rect1.dRectCol1, param.roi.rect1.dRectRow2, param.roi.rect1.dRectCol2);
                        }
                        else if (param.roi.objDraw == ObjDraw.rect2)
                        {
                            HOperatorSet.GenRectangle2(out ho_roi, param.roi.rect2.dRect2Row, param.roi.rect2.dRect2Col, param.roi.rect2.dPhi, param.roi.rect2.dLength1, param.roi.rect2.dLength2);
                        }
                        else if (param.roi.objDraw == ObjDraw.circle)
                        {
                            HOperatorSet.GenCircle(out ho_roi, param.roi.circle.dRow, param.roi.circle.dCol, param.roi.circle.dRadius);
                        }
                        ho_ImageOut.Dispose();
                        HOperatorSet.ReduceDomain(ho_ImageTemp, ho_roi, out ho_ImageOut);
                        ho_ImageTemp.Dispose();
                        ho_ImageTemp = ho_ImageOut.Clone();
                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.roi;
                        reslut.objType = objType.image;
                        reslut.obj = ho_ImageOut.Clone();
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.color_space_trans)
                    {
                        ho_ImageOut.Dispose();
                        ColorSpaceTrans_extend(ho_ImageTemp, param.color_space_trans.strColorSpace, param.color_space_trans.nSpaceSel, out ho_ImageOut);
                        ho_ImageTemp.Dispose();
                        ho_ImageTemp = ho_ImageOut.Clone();
                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.color_space_trans;
                        reslut.objType = objType.image;
                        reslut.obj = ho_ImageOut.Clone();
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.mean)
                    {
                        ho_ImageOut.Dispose();
                        HOperatorSet.MeanImage(ho_ImageTemp, out ho_ImageOut, param.mean.nMaskWidth, param.mean.nMaskHeight);
                        ho_ImageTemp.Dispose();
                        ho_ImageTemp = ho_ImageOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.mean;
                        reslut.obj = ho_ImageOut.Clone();
                        reslut.objType = objType.image;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.median)
                    {
                        ho_ImageOut.Dispose();
                        HOperatorSet.MedianImage(ho_ImageTemp, out ho_ImageOut, param.median.strMaskType, param.median.nMaskRadius, "mirrored");
                        ho_ImageTemp.Dispose();
                        ho_ImageTemp = ho_ImageOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.median;
                        reslut.obj = ho_ImageOut.Clone();
                        reslut.objType = objType.image;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.equhist)
                    {
                        ho_ImageOut.Dispose();
                        HOperatorSet.EquHistoImage(ho_ImageTemp, out ho_ImageOut);
                        ho_ImageTemp.Dispose();
                        ho_ImageTemp = ho_ImageOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.equhist;
                        reslut.obj = ho_ImageOut.Clone();
                        reslut.objType = objType.image;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.scale)
                    {
                        ho_ImageOut.Dispose();
                        scale_image_range(ho_ImageTemp, out ho_ImageOut, param.scale.nScaleMin, param.scale.nScaleMax);
                        ho_ImageTemp.Dispose();
                        ho_ImageTemp = ho_ImageOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.scale;
                        reslut.obj = ho_ImageOut.Clone();
                        reslut.objType = objType.image;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.threshold)
                    {
                        ho_RegionOut.Dispose();
                        HOperatorSet.Threshold(ho_ImageTemp, out ho_RegionOut, param.threshold.nThdMin, param.threshold.nThdMax);
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.threshold;
                        reslut.obj = ho_RegionOut.Clone();
                        reslut.objType = objType.region;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.dyn_threshold)
                    {

                    }
                    else if (check == ImageProType.connect)
                    {
                        ho_RegionOut.Dispose();
                        HOperatorSet.Connection(ho_RegionIn, out ho_RegionOut);
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.connect;
                        reslut.obj = ho_RegionOut.Clone();
                        reslut.objType = objType.region;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.union)
                    {
                        ho_RegionOut.Dispose();
                        HOperatorSet.Union1(ho_RegionIn, out ho_RegionOut);
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.union;
                        reslut.obj = ho_RegionOut.Clone();
                        reslut.objType = objType.region;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.dilation)
                    {
                        ho_RegionOut.Dispose();
                        if ("circle" == param.dilation.strFilterType)
                        {
                            HOperatorSet.DilationCircle(ho_RegionIn, out ho_RegionOut, param.dilation.dMaskRadius);
                        }
                        else if ("rectangle1" == param.dilation.strFilterType)
                        {
                            HOperatorSet.DilationRectangle1(ho_RegionIn, out ho_RegionOut, param.dilation.nMaskWidth, param.dilation.nMaskHeight);
                        }
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.dilation;
                        reslut.obj = ho_RegionOut.Clone();
                        reslut.objType = objType.region;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.erosion)
                    {
                        ho_RegionOut.Dispose();
                        if ("circle" == param.erosion.strFilterType)
                        {
                            HOperatorSet.ErosionCircle(ho_RegionIn, out ho_RegionOut, param.erosion.dMaskRadius);
                        }
                        else if ("rectangle1" == param.erosion.strFilterType)
                        {
                            HOperatorSet.DilationRectangle1(ho_RegionIn, out ho_RegionOut, param.erosion.nMaskWidth, param.erosion.nMaskHeight);
                        }
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.erosion;
                        reslut.obj = ho_RegionOut.Clone();
                        reslut.objType = objType.region;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.opening)
                    {
                        ho_RegionOut.Dispose();
                        if ("circle" == param.opening.strFilterType)
                        {
                            HOperatorSet.OpeningCircle(ho_RegionIn, out ho_RegionOut, param.opening.dMaskRadius);
                        }
                        else if ("rectangle1" == param.erosion.strFilterType)
                        {
                            HOperatorSet.OpeningRectangle1(ho_RegionIn, out ho_RegionOut, param.opening.nMaskWidth, param.opening.nMaskHeight);
                        }
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.opening;
                        reslut.obj = ho_RegionOut.Clone();
                        reslut.objType = objType.region;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.closing)
                    {
                        ho_RegionOut.Dispose();
                        if ("circle" == param.closing.strFilterType)
                        {
                            HOperatorSet.ClosingCircle(ho_RegionIn, out ho_RegionOut, param.closing.dMaskRadius);
                        }
                        else if ("rectangle1" == param.closing.strFilterType)
                        {
                            HOperatorSet.ClosingRectangle1(ho_RegionIn, out ho_RegionOut, param.closing.nMaskWidth, param.closing.nMaskHeight);
                        }
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.closing;
                        reslut.obj = ho_RegionOut.Clone();
                        reslut.objType = objType.region;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.thd_sub_pixel)
                    {
                        ho_XLDOut.Dispose();
                        HOperatorSet.ThresholdSubPix(ho_ImageTemp, out ho_XLDOut, param.thd_sub_pixel.nThd);
                        ho_XLDIn.Dispose();
                        ho_XLDIn = ho_XLDOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.thd_sub_pixel;
                        reslut.obj = ho_XLDOut.Clone();
                        reslut.objType = objType.xld;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.edges_sub_pixel)
                    {
                        ho_XLDOut.Dispose();
                        HOperatorSet.EdgesSubPix(ho_ImageTemp, out ho_XLDOut, param.edges_sub_pixel.strFilterType, param.edges_sub_pixel.dAlpha, param.edges_sub_pixel.nMin, param.edges_sub_pixel.nMax);
                        ho_XLDIn.Dispose();
                        ho_XLDIn = ho_XLDOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.edges_sub_pixel;
                        reslut.obj = ho_XLDOut.Clone();
                        reslut.objType = objType.xld;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.feature_select)
                    {
                        ho_RegionOut.Dispose();
                        int num = param.feature_select.listFeature.Count();
                        if (1 == num)
                        {
                            HOperatorSet.SelectShape(ho_RegionIn, out ho_RegionOut, param.feature_select.listFeature[0], "and", param.feature_select.listMinValue[0], param.feature_select.listMaxValue[0]);
                        }
                        else if (2 == num)
                        {
                            m_hWnd.DispObj(ho_RegionIn);
                            HOperatorSet.SelectShape(ho_RegionIn, out ho_RegionOut, new HTuple(param.feature_select.listFeature[0]).TupleConcat(param.feature_select.listFeature[1]), "and",
                                                                                    new HTuple(param.feature_select.listMinValue[0]).TupleConcat(param.feature_select.listMinValue[1]),
                                                                                    new HTuple(param.feature_select.listMaxValue[0]).TupleConcat(param.feature_select.listMaxValue[1]));
                        }
                        else
                        {
                            return false;
                        }
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.feature_select;
                        reslut.obj = ho_RegionOut.Clone();
                        reslut.objType = objType.region;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.fill_up)
                    {
                        ho_RegionOut.Dispose();
                        HOperatorSet.FillUp(ho_RegionIn, out ho_RegionOut);
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.fill_up;
                        reslut.obj = ho_RegionOut.Clone();
                        reslut.objType = objType.region;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.fill_up_shape)
                    {
                        ho_RegionOut.Dispose();
                        HOperatorSet.FillUpShape(ho_RegionIn, out ho_RegionOut, param.fill_up_shape.strShape, param.fill_up_shape.nMinVal, param.fill_up_shape.nMaxVal);
                        ho_RegionIn.Dispose();
                        ho_RegionIn = ho_RegionOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.fill_up_shape;
                        reslut.obj = ho_RegionOut.Clone();
                        reslut.objType = objType.region;
                        listResult.Add(reslut);
                    }
                    else if (check == ImageProType.gen_contour_region)
                    {
                        ho_XLDOut.Dispose();
                        HOperatorSet.GenContourRegionXld(ho_RegionIn, out ho_XLDOut, "border");
                        ho_XLDIn.Dispose();
                        ho_XLDIn = ho_XLDOut.Clone();

                        imageProPre reslut = new imageProPre();
                        reslut.type = ImageProType.gen_contour_region;
                        reslut.obj = ho_XLDOut.Clone();
                        reslut.objType = objType.xld;
                        listResult.Add(reslut);
                    }
                }
                m_hWnd.DispObj(ho_ImageOut);
                m_hWnd.DispObj(ho_RegionOut);
                m_hWnd.DispObj(ho_XLDOut);
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {

            }
        }
        //基于区域创建模板
        public bool CreateRegionModel(LocateInParams inPara, out int nModelID, out LocateOutParams outData)
        {
            outData = new LocateOutParams();
            outData.dModelRow = 0;
            outData.dModelCol = 0;
            outData.dModelAngle = 0;
            nModelID = -1;
            HObject ho_RegionROI = null, ho_ImageReduced = null, ho_contModel = null;


            HTuple hv_ModelID, hv_homMat2D = new HTuple();
            HTuple hv_Row, hv_Column, hv_Angle, hv_Score;

            HOperatorSet.GenEmptyObj(out ho_RegionROI);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_contModel);

            try
            {
                ho_RegionROI = GetLastDrawObj(m_lastDraw);
                m_hWnd.SetColor("green");
                m_hWnd.DispObj(ho_RegionROI);
                HOperatorSet.ReduceDomain(m_hImage, ho_RegionROI, out ho_ImageReduced);
                if (!inPara.bScale)
                {
                    HOperatorSet.CreateShapeModel(ho_ImageReduced, "auto", new HTuple(inPara.dAngleStart).TupleRad(), new HTuple(inPara.dAngleEnd).TupleRad(),
                                                 "auto", "auto", "use_polarity", "auto", "auto", out hv_ModelID);
                }
                else
                {
                    HOperatorSet.CreateScaledShapeModel(ho_ImageReduced, "auto", new HTuple(inPara.dAngleStart).TupleRad(), new HTuple(inPara.dAngleEnd).TupleRad(),
                                                        "auto", inPara.dScaleMin, inPara.dScaleMax, "auto", "auto", "use_polarity", "auto", "auto", out hv_ModelID);
                }

                HOperatorSet.FindShapeModel(m_hImage, hv_ModelID, new HTuple(inPara.dAngleStart).TupleRad(), new HTuple(inPara.dAngleEnd).TupleRad(), 0.5, 1, 0.5,
                                            "least_squares", 0, 0.9, out hv_Row, out hv_Column, out hv_Angle, out hv_Score);
                if (0 == hv_Row.TupleLength())
                {
                    MessageBox.Show("模板创建失败！");
                    return false;
                }
                HOperatorSet.GetShapeModelContours(out ho_contModel, hv_ModelID, 1);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_Row, hv_Column, hv_Angle, out hv_homMat2D);
                HOperatorSet.AffineTransContourXld(ho_contModel, out ho_contModel, hv_homMat2D);
                m_hWnd.SetColor("red");
                m_hWnd.DispObj(ho_contModel);
                HOperatorSet.DispCross(m_hWnd, hv_Row, hv_Column, 20, hv_Angle);
                outData.dModelRow = hv_Row.D;
                outData.dModelCol = hv_Column.D;
                outData.dModelAngle = hv_Angle.D;
                nModelID = hv_ModelID.I;
                return true;

            }
            catch (HalconException error)
            {
                MessageBox.Show(error.ToString());
                return false;
            }
            finally
            {
                if (null != ho_RegionROI) ho_RegionROI.Dispose();
                if (null != ho_ImageReduced) ho_ImageReduced.Dispose();
                if (null != ho_contModel) ho_contModel.Dispose();
            }
        }
        //基于轮廓创建模板
        public bool CreateXLDModel_1(LocateInParams Param, int nEdgeThd, out int nModelID, out LocateOutParams outData)
        {
            outData = new LocateOutParams();
            nModelID = -1;
            HTuple hv_ModelID, hv_Area = new HTuple(), hv_AreaMax = new HTuple();
            HTuple hv_Row, hv_Column, hv_Angle, hv_Score;

            HObject ho_RegionROI = null, ho_ImageReduced = null;
            HObject ho_Region = null, ho_SelRegion = null, ho_RegClosing = null, ho_RegionSelect = null;
            HObject ho_ModelCont = null;

            HOperatorSet.GenEmptyObj(out ho_RegionROI);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_SelRegion);
            HOperatorSet.GenEmptyObj(out ho_RegClosing);
            HOperatorSet.GenEmptyObj(out ho_ModelCont);
            try
            {
                ho_RegionROI = GetLastDrawObj(m_lastDraw);
                m_hWnd.DispObj(ho_RegionROI);
                HOperatorSet.ReduceDomain(m_hImage, ho_RegionROI, out ho_ImageReduced);
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, nEdgeThd, 255);
                HOperatorSet.FillUp(ho_Region, out ho_Region);
                HOperatorSet.Connection(ho_Region, out ho_Region);
                HOperatorSet.SelectShapeStd(ho_Region, out ho_SelRegion, "max_area", 70);
                HOperatorSet.RegionFeatures(ho_Region, "area", out hv_Area);
                HOperatorSet.RegionFeatures(ho_SelRegion, "area", out hv_AreaMax);
                HOperatorSet.SelectShape(ho_Region, out ho_RegionSelect, "area", "and", hv_Area.TupleMean() * 2, hv_AreaMax + 20);
                HOperatorSet.ClosingCircle(ho_RegionSelect, out ho_RegClosing, 5);
                HOperatorSet.Union1(ho_RegClosing, out ho_RegClosing);
                HOperatorSet.GenContourRegionXld(ho_RegClosing, out ho_ModelCont, "border");
                HOperatorSet.CreateShapeModelXld(ho_ModelCont, "auto", Param.dAngleStart, Param.dAngleEnd, "auto", "auto", "ignore_local_polarity", 5, out hv_ModelID);
                HOperatorSet.FindShapeModel(m_hImage, hv_ModelID, new HTuple(Param.dAngleStart).TupleRad(), new HTuple(Param.dAngleEnd).TupleRad(), 0.5, 1, 0.5,
                                            "least_squares", 0, 0.9, out hv_Row, out hv_Column, out hv_Angle, out hv_Score);
                if (0 == hv_Row.TupleLength())
                {
                    MessageBox.Show("轮廓模板创建失败！");
                    return false;
                }
                else
                {
                    m_hWnd.DispObj(ho_ModelCont);
                    HOperatorSet.DispCross(m_hWnd, hv_Row, hv_Column, 20, hv_Angle);
                    outData.dModelRow = hv_Row.D;
                    outData.dModelCol = hv_Column.D;
                    outData.dModelAngle = hv_Angle.D;

                    return true;
                }
            }
            catch (HalconException ex)
            {
                ex.ToString();
                MessageBox.Show("轮廓模板创建失败。");
                return false;
            }
            finally
            {
                // if (null != ho_Rectangle) ho_Rectangle.Dispose();
                if (null != ho_ImageReduced) ho_ImageReduced.Dispose();
                if (null != ho_Region) ho_Region.Dispose();
                if (null != ho_SelRegion) ho_SelRegion.Dispose();
                if (null != ho_RegClosing) ho_RegClosing.Dispose();
                if (null != ho_ModelCont) ho_ModelCont.Dispose();
            }

        }

        public bool CreateXLDModel(List<ImageProType> listCheck, ImageProcessParam proParam, LocateInParams Param, out int nModelID, out LocateOutParams outData)
        {
            outData = new LocateOutParams();
            nModelID = -1;
            HTuple hv_ModelID = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple(), hv_Angle = new HTuple(), hv_Score = new HTuple();

            //HObject ho_RegionROI = null, ho_ImageReduced = null;
            //HObject ho_Region = null, ho_SelRegion = null, ho_RegClosing = null, ho_RegionSelect = null;
            HObject ho_ModelCont = null;

            //HOperatorSet.GenEmptyObj(out ho_RegionROI);
            //HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            //HOperatorSet.GenEmptyObj(out ho_Region);
            //HOperatorSet.GenEmptyObj(out ho_SelRegion);
            //HOperatorSet.GenEmptyObj(out ho_RegClosing);
            HOperatorSet.GenEmptyObj(out ho_ModelCont);
            try
            {
                List<imageProPre> listResult = new List<imageProPre>();
                if (!ImageProcessAll_extend(listCheck, proParam, out listResult))
                    return false;
                foreach (imageProPre proPre in listResult)
                {
                    if (proPre.objType == objType.xld)
                    {
                        ho_ModelCont = proPre.obj.Clone();
                    }
                }
                m_hWnd.DispObj(ho_ModelCont);
                HOperatorSet.CreateShapeModelXld(ho_ModelCont, "auto", Param.dAngleStart, Param.dAngleEnd, "auto", "auto", "ignore_local_polarity", 5, out hv_ModelID);
                HOperatorSet.FindShapeModel(m_hImage, hv_ModelID, new HTuple(Param.dAngleStart).TupleRad(), new HTuple(Param.dAngleEnd).TupleRad(), 0.5, 1, 0.5,
                                            "least_squares", 0, 0.9, out hv_Row, out hv_Column, out hv_Angle, out hv_Score);
                if (0 == hv_Row.TupleLength())
                {
                    MessageBox.Show("轮廓模板创建失败！");
                    return false;
                }
                else
                {
                    m_hWnd.DispObj(ho_ModelCont);
                    HOperatorSet.DispCross(m_hWnd, hv_Row, hv_Column, 20, hv_Angle);
                    outData.dModelRow = hv_Row.D;
                    outData.dModelCol = hv_Column.D;
                    outData.dModelAngle = hv_Angle.D;
                    nModelID = hv_ModelID.I;
                    return true;
                }
            }
            catch (HalconException ex)
            {
                MessageBox.Show("轮廓模板创建失败。" + ex.ToString());
                return false;
            }
            finally
            {
                // if (null != ho_Rectangle) ho_Rectangle.Dispose();
                //if (null != ho_ImageReduced) ho_ImageReduced.Dispose();
                //if (null != ho_Region) ho_Region.Dispose();
                //if (null != ho_SelRegion) ho_SelRegion.Dispose();
                //if (null != ho_RegClosing) ho_RegClosing.Dispose();
                if (null != ho_ModelCont) ho_ModelCont.Dispose();
            }

        }
        //基于ncc创建模板：待优化
        public bool CreateNccModel(LocateInParams inParam, out int nModelID, out List<LocateOutParams> listOutData)
        {

            listOutData = new List<LocateOutParams>();
            nModelID = -1;

            HTuple hv_ModelID = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple(), hv_Angle = new HTuple(), hv_Score = new HTuple();

            HObject ho_RegionROI = null, ho_imgReduced = null, ho_cross = null;

            HOperatorSet.GenEmptyObj(out ho_RegionROI);
            HOperatorSet.GenEmptyObj(out ho_imgReduced);
            HOperatorSet.GenEmptyObj(out ho_cross);

            try
            {
                ho_RegionROI.Dispose();
                ho_RegionROI = GetLastDrawObj(m_lastDraw);
                ho_imgReduced.Dispose();
                HOperatorSet.ReduceDomain(m_GrayImage, ho_RegionROI, out ho_imgReduced);
                HOperatorSet.CreateNccModel(ho_imgReduced, "auto", new HTuple(inParam.dAngleStart).TupleRad(), new HTuple(inParam.dAngleEnd).TupleRad(), "auto", "use_polarity", out hv_ModelID);
                HOperatorSet.FindNccModel(m_GrayImage, hv_ModelID, new HTuple(inParam.dAngleStart).TupleRad(), new HTuple(inParam.dAngleEnd).TupleRad(), 0.6, 0, 0.5, "true", 0, out hv_Row, out hv_Column, out hv_Angle, out hv_Score);
                if (0 == hv_Row.TupleLength())
                {
                    MessageBox.Show("模板创建失败！");
                    return false;
                }
                for (int i = 0; i < hv_Row.TupleLength(); i++)
                {
                    LocateOutParams outData = new LocateOutParams();
                    outData.dModelRow = hv_Row[i].D;
                    outData.dModelCol = hv_Column[i].D;
                    outData.dModelAngle = hv_Angle.TupleSelect(i).TupleDeg();
                    outData.dScore = hv_Score[i].D;
                    ho_cross.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_cross, hv_Row[i], hv_Column[i], 30, 0);
                    m_hWnd.DispObj(ho_cross);
                    listOutData.Add(outData);
                }

                nModelID = hv_ModelID.I;
                return true;

            }
            catch (HalconException ex)
            {
                MessageBox.Show("模板创建失败！" + ex.ToString());
                return false;
            }
            finally
            {
                if (null != ho_RegionROI) ho_RegionROI.Dispose();
                if (null != ho_imgReduced) ho_imgReduced.Dispose();
                if (null != ho_cross) ho_cross.Dispose();
            }
        }
        public bool CreateXldModel(LocateInParams inParam, TMLocateParam param, out int nModelID, out LocateOutParams listOutData)
        {
            listOutData = new LocateOutParams();
            nModelID = -1;
            HTuple hv_ModelID = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple(), hv_Angle = new HTuple(), hv_Score = new HTuple();

            HObject ho_RegionROI = null, ho_ImageReduced = null, ho_Region = null, ho_SelRegion = null;
            HObject ho_RegionSelect1 = null, ho_RegionSelect = null, ho_ModelCont = null;

            HOperatorSet.GenEmptyObj(out ho_RegionROI);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_SelRegion);
            HOperatorSet.GenEmptyObj(out ho_RegionSelect1);
            HOperatorSet.GenEmptyObj(out ho_RegionSelect);
            HOperatorSet.GenEmptyObj(out ho_ModelCont);
            try
            {
                ho_RegionROI.Dispose();
                ho_RegionROI = GetLastDrawObj(m_lastDraw);
                ho_Region.Dispose();
                HOperatorSet.Threshold(m_GrayImage, out ho_Region, 0, param.nThd);
                ho_SelRegion.Dispose();
                HOperatorSet.FillUp(ho_Region, out ho_SelRegion);
                ho_RegionSelect.Dispose();
                HOperatorSet.Connection(ho_SelRegion, out ho_RegionSelect);
                ho_RegionSelect1.Dispose();
                HOperatorSet.SelectShape(ho_RegionSelect, out ho_RegionSelect1, "area",
       "and", param.nminArea, 1000000);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_RegionSelect1, ho_RegionROI, out ho_ImageReduced);
                ho_ModelCont.Dispose();
                HOperatorSet.GenContourRegionXld(ho_ImageReduced, out ho_ModelCont, "border");

                HOperatorSet.CreateShapeModelXld(ho_ModelCont, 5, inParam.dAngleStart, inParam.dAngleEnd, "auto", "auto", "ignore_local_polarity", 5, out hv_ModelID);
                HOperatorSet.FindShapeModel(m_hImage, hv_ModelID, new HTuple(inParam.dAngleStart).TupleRad(), new HTuple(inParam.dAngleEnd).TupleRad(), 0.5, 1, 0.5,
                                            "least_squares", 0, 0.9, out hv_Row, out hv_Column, out hv_Angle, out hv_Score);
                if (0 == hv_Row.TupleLength())
                {
                    MessageBox.Show("轮廓模板创建失败！");
                    return false;
                }
                else
                {

                    //m_hWnd.DispObj(ho_ModelCont);
                    //HOperatorSet.DispCross(m_hWnd, hv_Row, hv_Column, 20, hv_Angle);
                    DispRegion(ho_ModelCont, "green");
                    listOutData.dModelRow = hv_Row.D;
                    listOutData.dModelCol = hv_Column.D;
                    listOutData.dModelAngle = hv_Angle.D;
                    listOutData.dScore = hv_Score.D;
                    nModelID = hv_ModelID.I;
                    return true;
                }
            }
            catch (HalconException ex)
            {
                MessageBox.Show("轮廓模板创建失败。" + ex.ToString());
                return false;
            }
            finally
            {
                ho_RegionROI?.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region?.Dispose();
                ho_SelRegion?.Dispose();
                ho_RegionSelect1?.Dispose();
                ho_RegionSelect?.Dispose();
                ho_ModelCont?.Dispose();
            }

        }
        //模板匹配
        public bool FindModel(int nModelID,bool show, LocateInParams inParam, out LocateOutParams listOutData)
        {
            listOutData = new LocateOutParams();

            HTuple hv_Row = new HTuple(), hv_Column = new HTuple(), hv_Angle = new HTuple();
            HTuple hv_Score = new HTuple(), hv_HomMat2D = new HTuple();

            HObject ho_ModelContours = null, ho_ContoursAffinTrans = null;
            HObject ho_cross = null;

            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_ContoursAffinTrans);
            HOperatorSet.GenEmptyObj(out ho_cross);

            try
            {
                if (inParam.modelType == ModelType.contour || inParam.modelType == ModelType.region)
                {
                    HOperatorSet.FindShapeModel(m_hImage, nModelID, new HTuple(inParam.dAngleStart).TupleRad(), new HTuple(inParam.dAngleEnd).TupleRad(), 0.5, 1, 0.5,
                                               "least_squares", 0, 0.9, out hv_Row, out hv_Column, out hv_Angle, out hv_Score);
                }
                else
                {
                    HOperatorSet.FindNccModel(m_hImage, nModelID, new HTuple(inParam.dAngleStart).TupleRad(), new HTuple(inParam.dAngleEnd).TupleRad(), 0.5, 0, 0.5, "true", 0,
                                              out hv_Row, out hv_Column, out hv_Angle, out hv_Score);
                }
                if (0 == hv_Row.TupleLength())
                {
                    return false;
                }
                if (inParam.modelType == ModelType.contour || inParam.modelType == ModelType.region)
                {
                    ho_ModelContours.Dispose();
                    HOperatorSet.GetShapeModelContours(out ho_ModelContours, nModelID, 1);
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_Row, hv_Column, hv_Angle, out hv_HomMat2D);
                    HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffinTrans, hv_HomMat2D);
                    m_hWnd.DispObj(ho_ContoursAffinTrans);
                }

                listOutData.dModelRow = hv_Row.D;
                listOutData.dModelCol = hv_Column.D;
                listOutData.dModelAngle = hv_Angle.D;
                listOutData.dScore = hv_Score.D;
                HOperatorSet.GenCrossContourXld(out ho_cross, hv_Row, hv_Column, 30, 0);
                m_hWnd.DispObj(ho_cross);
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                if (null != ho_ModelContours) ho_ModelContours.Dispose();
                if (null != ho_ContoursAffinTrans) ho_ContoursAffinTrans.Dispose();
                ho_cross?.Dispose();
            }

        }

        public bool FindModel_Several(HObject ho_Image, int nModelID, LocateInParams inParam, out List<LocateOutParams> listOutData)
        {
            listOutData = new List<LocateOutParams>();

            HTuple hv_Row = new HTuple(), hv_Column = new HTuple(), hv_Angle = new HTuple();
            HTuple hv_Score = new HTuple(), hv_HomMat2D = new HTuple();

            HOperatorSet.GenEmptyObj(out HObject ho_ModelContours);
            HOperatorSet.GenEmptyObj(out HObject ho_ContoursAffinTrans);
            HOperatorSet.GenEmptyObj(out HObject ho_cross);

            try
            {

                if (inParam.modelType == ModelType.contour || inParam.modelType == ModelType.region)
                {
                    HOperatorSet.FindShapeModel(ho_Image, nModelID, new HTuple(inParam.dAngleStart).TupleRad(), new HTuple(inParam.dAngleEnd).TupleRad(), inParam.dScore, 1, 0.5,
                                               "least_squares", 0, 0.9, out hv_Row, out hv_Column, out hv_Angle, out hv_Score);
                }
                else
                {
                    HOperatorSet.CountChannels(ho_Image, out HTuple hv_channels);
                    if (1 != hv_channels.I)
                    {
                        StaticFun.MessageFun.ShowMessage("请输入单通道图像。");
                        return false;
                    }
                    HOperatorSet.FindNccModel(ho_Image, nModelID, new HTuple(inParam.dAngleStart).TupleRad(), new HTuple(inParam.dAngleEnd).TupleRad(), inParam.dScore, 0, 0.5, "true", 0, out hv_Row, out hv_Column, out hv_Angle, out hv_Score);
                }
                if (0 == hv_Row.TupleLength())
                {
                    return false;
                }
                if (inParam.modelType == ModelType.contour || inParam.modelType == ModelType.region)
                {
                    ho_ModelContours.Dispose();
                    HOperatorSet.GetShapeModelContours(out ho_ModelContours, nModelID, 1);
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_Row, hv_Column, hv_Angle, out hv_HomMat2D);
                    HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffinTrans, hv_HomMat2D);
                    m_hWnd.DispObj(ho_ContoursAffinTrans);
                }
                for (int i = 0; i < hv_Row.TupleLength(); i++)
                {
                    LocateOutParams outData = new LocateOutParams();
                    outData.dModelRow = hv_Row[i].D;
                    outData.dModelCol = hv_Column[i].D;
                    outData.dModelAngle = hv_Angle[i];
                    outData.dScore = hv_Score[i].D;
                    listOutData.Add(outData);
                }
                return true;
            }
            catch (HalconException ex)
            {
                ("定位失败：" + ex.ToString()).ToLog();
                StaticFun.MessageFun.ShowMessage("定位失败：" + ex.ToString());
                return false;
            }
            finally
            {
                ho_ModelContours?.Dispose();
                ho_ContoursAffinTrans?.Dispose();
                ho_cross?.Dispose();
            }

        }
        //保存模板
        public bool WriteModel( string strModelName, ModelType modelType, int nModelID)
        {
            try
            {
                if (nModelID < 0)
                {
                    return false;
                }
                string strFilePath = GlobalPath.SavePath.ModelPath;

                if (!Directory.Exists(strFilePath))
                    Directory.CreateDirectory(strFilePath);


                if (modelType == ModelType.contour || modelType == ModelType.region)
                {
                    strFilePath = strFilePath + strModelName + ".shm";//不能包含中文
                    HOperatorSet.WriteShapeModel(nModelID, strFilePath);
                }
                else if (modelType == ModelType.ncc)
                {
                    strFilePath = strFilePath + strModelName + ".ncm";//不能包含中文
                    HOperatorSet.WriteNccModel(nModelID, strFilePath);
                }
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("模板保存失败！" + ex.ToString());
                HOperatorSet.ClearNccModel(nModelID);
                HOperatorSet.ClearShapeModel(nModelID);
                return false;
            }
        }

        //查找指定目录下的.shm文件
        public static bool FindModelFile(out List<string> strModelName)
        {
            strModelName = new List<string>();
            try
            {
                string strCurPath = Environment.CurrentDirectory.ToString();
                string strFilePath = strCurPath + "\\";//不能包含中文

                string[] filedir = Directory.GetFiles(strFilePath, "*.shm", SearchOption.AllDirectories);
                int n = 0;
                string str1 = ".shm";
                int num = str1.Length;
                foreach (string str in filedir)
                {
                    string newStr = str.Remove(0, strFilePath.Count());
                    newStr = newStr.TrimEnd('.', 's', 'h', 'm');
                    //  string newName = newStr.Remove(newStr.Length- 1, num);
                    strModelName.Add(newStr);
                    n++;
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;

            }
        }
        //显示模板形状：只针对基于轮廓和基于区域的//
        public bool ShowModelshape(HWindowControl hWndCtrl, string strFilePath, out int modelID)
        {
            modelID = -1;
            HTuple hv_ModelID = new HTuple(), hv_homMat2D = new HTuple();
            HTuple hv_row1 = new HTuple(), hv_col1 = new HTuple(), hv_row2 = new HTuple(), hv_col2 = new HTuple();

            HObject ho_modelCont = null, ho_modelRegion = null, ho_objSel = null;
            HOperatorSet.GenEmptyObj(out ho_modelCont);
            HOperatorSet.GenEmptyObj(out ho_modelRegion);
            HOperatorSet.GenEmptyObj(out ho_objSel);
            try
            {
                HWindow hWnd = hWndCtrl.HalconWindow;

                HOperatorSet.ReadShapeModel(strFilePath, out hv_ModelID);
                HOperatorSet.GetShapeModelContours(out ho_modelCont, hv_ModelID, 1);
                HOperatorSet.UnionAdjacentContoursXld(ho_modelCont, out ho_modelCont, 20, 2, "attr_keep");
                HOperatorSet.SmallestRectangle1Xld(ho_modelCont, out hv_row1, out hv_col1, out hv_row2, out hv_col2);

                double height = (hv_row2.TupleMax() - hv_row1.TupleMin()) * 1.2;
                double width = (hv_col2.TupleMax() - hv_col1.TupleMin()) * 1.2;

                double dRow0 = 0, dCol0 = 0, dRow1 = height - 1, dCol1 = width - 1;
                float fImage = (float)dCol1 / (float)dRow1;
                float fWindow = (float)hWndCtrl.Width / hWndCtrl.Height;

                if (fWindow > fImage)
                {
                    float w = fWindow * (float)height;
                    dRow0 = 0;
                    dCol0 = -(w - width) / 2;
                    dRow1 = height - 1;
                    dCol1 = width + (w - width) / 2;
                }
                else
                {
                    float h = (float)width / fWindow;
                    dRow0 = -(h - height) / 2;
                    dCol0 = 0;
                    dRow1 = height + (h - height) / 2;
                    dCol1 = width - 1;
                }

                HOperatorSet.SetSystem("flush_graphic", "false");
                hWnd.ClearWindow();
                hWnd.SetColor("red");
                hWnd.SetPart(dRow0, dCol0, dRow1, dCol1);

                HOperatorSet.VectorAngleToRigid(0, 0, 0, height / 2, width / 2, 0, out hv_homMat2D);
                HOperatorSet.AffineTransContourXld(ho_modelCont, out ho_modelCont, hv_homMat2D);
                hWnd.DispObj(ho_modelCont);

                HOperatorSet.SetSystem("flush_graphic", "true");
                HObject emptyObject = null;
                HOperatorSet.GenEmptyObj(out emptyObject);
                hWnd.DispObj(emptyObject);
                modelID = hv_ModelID.I;
                return true;
            }
            catch (HalconException error)
            {
                MessageBox.Show("模板读取出错！" + error.ToString());
                return false;
            }
            finally
            {
                if (null != ho_modelCont) ho_modelCont.Dispose();
            }
        }

        //读取模板//
        public static bool ReadModelFromFile(ModelType modelType, string strFilePath, out int modelID)
        {
            modelID = -1;
            HTuple hv_ModelID = new HTuple();
            try
            {
                if (modelType == ModelType.contour || modelType == ModelType.region)
                {
                    HOperatorSet.ReadShapeModel(strFilePath, out hv_ModelID);

                }
                else if (modelType == ModelType.ncc)
                {
                    HOperatorSet.ReadNccModel(strFilePath, out hv_ModelID);
                }
                modelID = hv_ModelID.I;
                return true;
            }
            catch (HalconException error)
            {
                MessageBox.Show("模板读取出错！" + error.ToString());
                return false;
            }

        }

        public bool Code1DIdentify(Code1DParam param, out Code1DResult result)
        {
            result = new Code1DResult();
            result.listCode1DDecode = new List<Code1DDecode>();
            HObject ho_SymbolRegions = null;

            HTuple hv_BarCodeHandle = null;
            HTuple hv_DecodedDataStrings = new HTuple();
            HTuple hv_BarCodeResults = new HTuple();

            HOperatorSet.GenEmptyObj(out ho_SymbolRegions);
            try
            {
                HOperatorSet.CreateBarCodeModel(new HTuple(), new HTuple(), out hv_BarCodeHandle);
                //meas_thresh:在条形码区域受到干扰或噪声水平较高的情况下，应增加“meas_thresh”的值
                double dMeasThresh = 0.05;
                while (dMeasThresh <= 0.2)
                {
                    HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "meas_thresh", dMeasThresh);
                    HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "meas_thresh_abs", param.dMeasThreshAbs);
                    HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "contrast_min", param.nContrastMin);
                    HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, (new HTuple("orientation")).TupleConcat("orientation_tol"), (new HTuple(-90)).TupleConcat(90));
                    HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, "merge_scanlines", "true");
                    ho_SymbolRegions.Dispose();
                    HOperatorSet.FindBarCode(m_hImage, out ho_SymbolRegions, hv_BarCodeHandle, param.strCodeType, out hv_DecodedDataStrings);
                    if (0 == hv_DecodedDataStrings.TupleLength())
                    {
                        HOperatorSet.SetBarCodeParam(hv_BarCodeHandle, (new HTuple("orientation")).TupleConcat("orientation_tol"), (new HTuple(0)).TupleConcat(90));
                        ho_SymbolRegions.Dispose();
                        HOperatorSet.FindBarCode(m_hImage, out ho_SymbolRegions, hv_BarCodeHandle, param.strCodeType, out hv_DecodedDataStrings);
                        if (0 != hv_DecodedDataStrings.TupleLength())
                        {
                            break;
                        }
                    }
                    else
                        break;
                    dMeasThresh = dMeasThresh + 0.01;
                }
                ShowRegion(ho_SymbolRegions);
                if (0 == hv_DecodedDataStrings.TupleLength())
                    return false;
                HOperatorSet.GetBarCodeResult(hv_BarCodeHandle, "all", "decoded_types", out hv_BarCodeResults);
                for (int i = 0; i < hv_DecodedDataStrings.TupleLength(); i++)
                {
                    Code1DDecode decode = new Code1DDecode();
                    decode.strDecode = hv_DecodedDataStrings[i].S;
                    decode.strCodeType = hv_BarCodeResults[i].S;
                    result.listCode1DDecode.Add(decode);
                }
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                HOperatorSet.ClearBarCodeModel(hv_BarCodeHandle);
                if (null != ho_SymbolRegions) ho_SymbolRegions.Dispose();
            }
        }

        public bool GetRect2CornerPoint(Rect2 rect, out PointF[] pionts)
        {
            pionts = new PointF[4]; //顺序：左上角、右上角、右下角、
            try
            {
                //
                double dCos = Math.Cos(rect.dPhi);
                double dSin = Math.Sin(rect.dPhi);
                //左上角
                double a = ((-rect.dLength1) * dCos) - (rect.dLength2 * dSin);
                double b = ((-rect.dLength1) * dSin) + (rect.dLength2 * dCos);
                pionts[0] = new PointF((float)(rect.dRect2Row - b), (float)(rect.dRect2Col + a));
                //右上角
                double c = (rect.dLength1 * dCos) - (rect.dLength2 * dSin);
                double d = (rect.dLength1 * dSin) + (rect.dLength2 * dCos);
                pionts[1] = new PointF((float)(rect.dRect2Row - d), (float)(rect.dRect2Col + c));
                //右下角
                double e = (rect.dLength1 * dCos) + (rect.dLength2 * dSin);
                double f = (rect.dLength1 * dSin) - (rect.dLength2 * dCos);
                pionts[2] = new PointF((float)(rect.dRect2Row - f), (float)(rect.dRect2Col + e));
                //左下角
                double g = ((-rect.dLength1) * dCos) + (rect.dLength2 * dSin);
                double h = ((-rect.dLength1) * dSin) - (rect.dLength2 * dCos);
                pionts[3] = new PointF((float)(rect.dRect2Row - h), (float)(rect.dRect2Col + g));
                return true;
            }
            catch (SystemException error)
            {
                MessageFun.ShowMessage(error.ToString());
                return false;
            }
        }

        static List<imageProPre> listResult = new List<imageProPre>();

        //颜色空间转换
        public bool ColorSpaceTrans(string strColorSpace, int nSpaceSel)
        {
            HTuple hv_channels = new HTuple();

            HObject ho_GrayImage = null;
            HObject ho_ImageRed = null, ho_ImageGreen = null, ho_ImageBlue = null;
            HObject ho_ImageResult1 = null, ho_ImageResult2 = null, ho_ImageResult3 = null;

            HOperatorSet.GenEmptyObj(out ho_GrayImage);
            HOperatorSet.GenEmptyObj(out ho_ImageRed);
            HOperatorSet.GenEmptyObj(out ho_ImageGreen);
            HOperatorSet.GenEmptyObj(out ho_ImageBlue);
            HOperatorSet.GenEmptyObj(out ho_ImageResult1);
            HOperatorSet.GenEmptyObj(out ho_ImageResult2);
            HOperatorSet.GenEmptyObj(out ho_ImageResult3);

            try
            {
                HObject ho_ImgOrgTemp = m_hImage.Clone();
                HOperatorSet.CountChannels(ho_ImgOrgTemp, out hv_channels);
                if (3 != hv_channels.I)
                    return false;
                HOperatorSet.Decompose3(ho_ImgOrgTemp, out ho_ImageRed, out ho_ImageGreen, out ho_ImageBlue);
                if ("" == strColorSpace)
                {
                    m_hWnd.DispObj(m_hImage);
                    return true;
                }
                else if ("gray" == strColorSpace)
                {
                    HOperatorSet.Rgb1ToGray(ho_ImgOrgTemp, out ho_GrayImage);
                }
                else if ("rgb" == strColorSpace)
                {
                    ho_ImageResult1 = ho_ImageRed.Clone();
                    ho_ImageResult2 = ho_ImageGreen.Clone();
                    ho_ImageResult3 = ho_ImageBlue.Clone();
                }
                else
                {
                    HOperatorSet.TransFromRgb(ho_ImageRed, ho_ImageGreen, ho_ImageBlue, out ho_ImageResult1, out ho_ImageResult2, out ho_ImageResult3, strColorSpace);
                }
                switch (nSpaceSel)
                {
                    case 0:
                        m_hWnd.DispObj(ho_GrayImage);
                        //m_hImage = ho_ImgOrgTemp.Clone();
                        break;
                    case 1:
                        m_hWnd.DispObj(ho_ImageResult1);
                        //m_hImage = ho_ImageResult1.Clone();
                        break;
                    case 2:
                        m_hWnd.DispObj(ho_ImageResult2);
                        //m_hImage = ho_ImageResult2.Clone();
                        break;
                    case 3:
                        m_hWnd.DispObj(ho_ImageResult3);
                        //m_hImage = ho_ImageResult3.Clone();
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                if (null != ho_GrayImage) ho_GrayImage.Dispose();
                if (null != ho_ImageRed) ho_ImageRed.Dispose();
                if (null != ho_ImageGreen) ho_ImageGreen.Dispose();
                if (null != ho_ImageBlue) ho_ImageBlue.Dispose();
                if (null != ho_ImageResult1) ho_ImageResult1.Dispose();
                if (null != ho_ImageResult2) ho_ImageResult2.Dispose();
                if (null != ho_ImageResult3) ho_ImageResult3.Dispose();
            }
        }

        public bool ImageProcessAll(List<ImageProType> listCheck, ImageProcessParam param)
        {

            try
            {
                if (!ImageProcessAll_extend(listCheck, param, out listResult))
                {
                    return false;
                }
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {

            }
        }

        public void Result2Shown(int n)
        {
            HObject obj = null;

            HOperatorSet.GenEmptyObj(out obj);
            try
            {
                if (0 != listResult.Count())
                {
                    m_hWnd.DispObj(listResult[n].obj);
                }
                return;
            }
            catch (SystemException error)
            {
                error.ToString();
                return;
            }
        }

        //图像灰度拉伸
        public void scale_image_range(HObject ho_Image, out HObject ho_ImageScaled, HTuple hv_Min, HTuple hv_Max)
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_SelectedChannel = null, ho_LowerRegion = null;
            HObject ho_UpperRegion = null;

            // Local copy input parameter variables 
            HObject ho_Image_COPY_INP_TMP;
            ho_Image_COPY_INP_TMP = ho_Image.CopyObj(1, -1);

            // Local control variables 

            HTuple hv_LowerLimit = new HTuple(), hv_UpperLimit = new HTuple();
            HTuple hv_Mult = null, hv_Add = null, hv_Channels = null;
            HTuple hv_Index = null, hv_MinGray = new HTuple(), hv_MaxGray = new HTuple();
            HTuple hv_Range = new HTuple();
            HTuple hv_Max_COPY_INP_TMP = hv_Max.Clone();
            HTuple hv_Min_COPY_INP_TMP = hv_Min.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);
            HOperatorSet.GenEmptyObj(out ho_SelectedChannel);
            HOperatorSet.GenEmptyObj(out ho_LowerRegion);
            HOperatorSet.GenEmptyObj(out ho_UpperRegion);
            //Convenience procedure to scale the gray values of the
            //input image Image from the interval [Min,Max]
            //to the interval [0,255] (default).
            //Gray values < 0 or > 255 (after scaling) are clipped.
            //
            //If the image shall be scaled to an interval different from [0,255],
            //this can be achieved by passing tuples with 2 values [From, To]
            //as Min and Max.
            //Example:
            //scale_image_range(Image:ImageScaled:[100,50],[200,250])
            //maps the gray values of Image from the interval [100,200] to [50,250].
            //All other gray values will be clipped.
            //
            //input parameters:
            //Image: the input image
            //Min: the minimum gray value which will be mapped to 0
            //     If a tuple with two values is given, the first value will
            //     be mapped to the second value.
            //Max: The maximum gray value which will be mapped to 255
            //     If a tuple with two values is given, the first value will
            //     be mapped to the second value.
            //
            //output parameter:
            //ImageScale: the resulting scaled image
            //
            if (hv_Min_COPY_INP_TMP.TupleLength() == 2)
            {
                hv_LowerLimit = hv_Min_COPY_INP_TMP[1];
                hv_Min_COPY_INP_TMP = hv_Min_COPY_INP_TMP[0];
            }
            else
            {
                hv_LowerLimit = 0.0;
            }
            if (hv_Max_COPY_INP_TMP.TupleLength() == 2)
            {
                hv_UpperLimit = hv_Max_COPY_INP_TMP[1];
                hv_Max_COPY_INP_TMP = hv_Max_COPY_INP_TMP[0];
            }
            else
            {
                hv_UpperLimit = 255.0;
            }
            //
            //Calculate scaling parameters
            hv_Mult = (((hv_UpperLimit - hv_LowerLimit)).TupleReal()) / (hv_Max_COPY_INP_TMP - hv_Min_COPY_INP_TMP);
            hv_Add = ((-hv_Mult) * hv_Min_COPY_INP_TMP) + hv_LowerLimit;
            //
            //Scale image
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.ScaleImage(ho_Image_COPY_INP_TMP, out ExpTmpOutVar_0, hv_Mult, hv_Add);
                ho_Image_COPY_INP_TMP.Dispose();
                ho_Image_COPY_INP_TMP = ExpTmpOutVar_0;
            }
            //
            //Clip gray values if necessary
            //This must be done for each channel separately
            HOperatorSet.CountChannels(ho_Image_COPY_INP_TMP, out hv_Channels);
            HTuple end_val48 = hv_Channels;
            HTuple step_val48 = 1;
            for (hv_Index = 1; hv_Index.Continue(end_val48, step_val48); hv_Index = hv_Index.TupleAdd(step_val48))
            {
                ho_SelectedChannel.Dispose();
                HOperatorSet.AccessChannel(ho_Image_COPY_INP_TMP, out ho_SelectedChannel, hv_Index);
                HOperatorSet.MinMaxGray(ho_SelectedChannel, ho_SelectedChannel, 0, out hv_MinGray, out hv_MaxGray, out hv_Range);
                ho_LowerRegion.Dispose();
                HOperatorSet.Threshold(ho_SelectedChannel, out ho_LowerRegion, ((hv_MinGray.TupleConcat(hv_LowerLimit))).TupleMin(), hv_LowerLimit);
                ho_UpperRegion.Dispose();
                HOperatorSet.Threshold(ho_SelectedChannel, out ho_UpperRegion, hv_UpperLimit,
                    ((hv_UpperLimit.TupleConcat(hv_MaxGray))).TupleMax());
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.PaintRegion(ho_LowerRegion, ho_SelectedChannel, out ExpTmpOutVar_0, hv_LowerLimit, "fill");
                    ho_SelectedChannel.Dispose();
                    ho_SelectedChannel = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.PaintRegion(ho_UpperRegion, ho_SelectedChannel, out ExpTmpOutVar_0, hv_UpperLimit, "fill");
                    ho_SelectedChannel.Dispose();
                    ho_SelectedChannel = ExpTmpOutVar_0;
                }
                if ((int)(new HTuple(hv_Index.TupleEqual(1))) != 0)
                {
                    ho_ImageScaled.Dispose();
                    HOperatorSet.CopyObj(ho_SelectedChannel, out ho_ImageScaled, 1, 1);
                }
                else
                {
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.AppendChannel(ho_ImageScaled, ho_SelectedChannel, out ExpTmpOutVar_0);
                        ho_ImageScaled.Dispose();
                        ho_ImageScaled = ExpTmpOutVar_0;
                    }
                }
            }
            ho_Image_COPY_INP_TMP.Dispose();
            ho_SelectedChannel.Dispose();
            ho_LowerRegion.Dispose();
            ho_UpperRegion.Dispose();

            return;
        }
        //
        public bool MeanImage(HObject orgImage, HObject roi, out HObject filterImage, MeanImageParam param)
        {
            filterImage = new HObject();

            HObject ho_ImageMean = null;
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            try
            {
                if (roi.IsInitialized())
                {
                    HOperatorSet.ReduceDomain(orgImage, roi, out ho_ImageMean);
                }
                else
                {
                    ho_ImageMean = orgImage.Clone();
                }
                HOperatorSet.MeanImage(ho_ImageMean, out filterImage, param.nMaskWidth, param.nMaskHeight);
                m_hWnd.DispObj(filterImage);
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                if (null != ho_ImageMean) ho_ImageMean.Dispose();
            }
        }

        public bool MedianImage(HObject orgImage, HObject roi, out HObject filterImage, MedianImageParam param)
        {
            filterImage = new HObject();
            HObject ho_ImageMedian = null;
            HOperatorSet.GenEmptyObj(out ho_ImageMedian);
            try
            {
                if (roi.IsInitialized())
                {
                    HOperatorSet.ReduceDomain(orgImage, roi, out ho_ImageMedian);
                }
                else
                {
                    ho_ImageMedian = orgImage.Clone();
                }
                HOperatorSet.MedianImage(ho_ImageMedian, out filterImage, param.strMaskType, param.nMaskRadius, "mirrored");
                m_hWnd.DispObj(filterImage);
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                if (null != ho_ImageMedian) ho_ImageMedian.Dispose();
            }
        }

        public bool EquHistImage(HObject orgImage, HObject roi, out HObject enhancedImage)
        {
            enhancedImage = new HObject();
            HObject ho_ImageEquHist = null;
            HOperatorSet.GenEmptyObj(out ho_ImageEquHist);
            try
            {
                m_hWnd.DispObj(roi);
                if (roi.IsInitialized())
                {
                    HOperatorSet.ReduceDomain(orgImage, roi, out ho_ImageEquHist);
                }
                else
                {
                    ho_ImageEquHist = orgImage.Clone();
                }
                HOperatorSet.EquHistoImage(ho_ImageEquHist, out enhancedImage);
                m_hWnd.DispObj(enhancedImage);
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                if (null != ho_ImageEquHist) ho_ImageEquHist.Dispose();
            }
        }


        public bool OCR_Class(List<ImageProType> listCheck, ImageProcessParam proParam, OCRParam ocrParam, out string strChars)
        {
            strChars = "";
            HTuple hv_OCRHandleMlp = new HTuple(), hv_OCRHandleCnn = new HTuple(), hv_OCRHandleKnn = new HTuple(), hv_OCRHandleSvm = new HTuple();
            HTuple hv_class = new HTuple(), hv_confidence = new HTuple();
            HTuple hv_row1 = new HTuple(), hv_col1 = new HTuple(), hv_row2 = new HTuple(), hv_col2 = new HTuple();

            HObject ho_ImageResult = null, ho_charRegion = null, ho_xld = null;
            HObject ho_SelectedRegion = null, ho_SortedRegion = null;
            HObject hImage = null;

            HOperatorSet.GenEmptyObj(out ho_ImageResult);
            HOperatorSet.GenEmptyObj(out ho_charRegion);
            HOperatorSet.GenEmptyObj(out ho_xld);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegion);
            HOperatorSet.GenEmptyObj(out ho_SortedRegion);
            HOperatorSet.GenEmptyObj(out hImage);
            try
            {
                List<imageProPre> listResult = new List<imageProPre>();
                if (!ImageProcessAll_extend(listCheck, proParam, out listResult))
                    return false;
                foreach (imageProPre proPre in listResult)
                {
                    if (proPre.objType == objType.image)
                    {
                        ho_ImageResult = proPre.obj.Clone();
                    }
                    if (proPre.objType == objType.region)
                    {
                        ho_charRegion = proPre.obj.Clone();
                    }
                    if (proPre.objType == objType.xld)
                    {
                        ho_xld = proPre.obj.Clone();
                    }

                }
                m_hWnd.DispObj(ho_ImageResult);
                m_hWnd.DispObj(ho_charRegion);
                if (0 == ho_charRegion.CountObj())
                {
                    return false;
                }
                HOperatorSet.SelectShape(ho_charRegion, out ho_SelectedRegion, new HTuple("height").TupleConcat("width"), "and",
                                         new HTuple(ocrParam.dMinHeight).TupleConcat(ocrParam.dMinWidth),
                                         new HTuple(ocrParam.dMaxHeight).TupleConcat(ocrParam.dMaxWidth));
                if (0 == ho_SelectedRegion.CountObj())
                    return false;
                HOperatorSet.SortRegion(ho_SelectedRegion, out ho_SortedRegion, "character", "true", ocrParam.strDirect);
                if (0 == ho_SortedRegion.CountObj())
                {
                    return false;
                }
                m_hWnd.DispObj(ho_SortedRegion);
                switch (ocrParam.nBackGround)
                {
                    case 0:
                        hImage = ho_ImageResult.Clone();
                        break;
                    case 1:
                        HOperatorSet.InvertImage(ho_ImageResult, out hImage);
                        break;
                    default:
                        break;
                }
                //hImage = ho_ImageResult.Clone();
                m_hWnd.DispObj(hImage);
                m_hWnd.DispObj(ho_SortedRegion);
                if ("mlp" == ocrParam.strMethod)
                {
                    HOperatorSet.ReadOcrClassMlp(ocrParam.strCharType, out hv_OCRHandleMlp);
                    HOperatorSet.DoOcrMultiClassMlp(ho_SortedRegion, hImage, hv_OCRHandleMlp, out hv_class, out hv_confidence);
                }
                else if ("cnn" == ocrParam.strMethod)
                {
                    HOperatorSet.ReadOcrClassCnn(ocrParam.strCharType, out hv_OCRHandleCnn);
                    HOperatorSet.DoOcrMultiClassCnn(ho_SortedRegion, hImage, hv_OCRHandleCnn, out hv_class, out hv_confidence);
                }
                else if ("knn" == ocrParam.strMethod)
                {
                    HOperatorSet.ReadOcrClassKnn(ocrParam.strCharType, out hv_OCRHandleKnn);
                    HOperatorSet.DoOcrMultiClassKnn(ho_SortedRegion, hImage, hv_OCRHandleKnn, out hv_class, out hv_confidence);
                }
                else if ("sum" == ocrParam.strMethod)
                {
                    HOperatorSet.ReadOcrClassSvm(ocrParam.strCharType, out hv_OCRHandleSvm);
                    HOperatorSet.DoOcrMultiClassSvm(ho_SortedRegion, hImage, hv_OCRHandleSvm, out hv_class);
                }
                else
                {
                    return false;
                }
                strChars = hv_class.TupleSum().ToString();
                //显示
                HOperatorSet.SmallestRectangle1(ho_SortedRegion, out hv_row1, out hv_col1, out hv_row2, out hv_col2);

                //  m_hWnd.SetColor("black");
                for (int i = 0; i < hv_class.TupleLength(); i++)
                {
                    double dRow = (hv_row1[i] + hv_row2[i]) / 2.0;
                    double dCol = (hv_col1[i] + hv_col2[i]) / 2.0;
                    if ("row" == ocrParam.strDirect)
                    {
                        m_hWnd.SetTposition((int)dRow, hv_col2.I + 10);
                    }
                    else if ("column" == ocrParam.strDirect)
                    {
                        m_hWnd.SetTposition(hv_row2.I + 10, (int)dCol);
                    }
                    else
                    {
                        return false;
                    }
                    m_hWnd.WriteString(hv_class[i].S);
                }
                //  m_hWnd.SetColor("red");
                return true;
            }
            catch (HalconException ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                if (0 != hv_OCRHandleMlp.TupleLength())
                    HOperatorSet.ClearOcrClassMlp(hv_OCRHandleMlp);
                if (0 != hv_OCRHandleCnn.TupleLength())
                    HOperatorSet.ClearOcrClassCnn(hv_OCRHandleCnn);
            }

        }

        //public bool TrainVariationModel(LocateInParams locateParam, LocateOutParams modelCenter, int nModelID)
        //{
        //    HTuple hv_width = new HTuple(), hv_height = new HTuple();
        //    HTuple hv_VariationModelID = new HTuple(), hv_HomMat2D = new HTuple();

        //    HObject ho_ImageTrans = null, ho_MeanImage = null, ho_VarImage = null;
        //    HOperatorSet.GenEmptyObj(out ho_ImageTrans);
        //    HOperatorSet.GenEmptyObj(out ho_MeanImage);
        //    HOperatorSet.GenEmptyObj(out ho_VarImage);

        //    try
        //    {
        //        HOperatorSet.GetImageSize(m_hImage, out hv_width, out hv_height);
        //        HOperatorSet.CreateVariationModel(hv_width, hv_height, "byte", "standard", out hv_VariationModelID);
        //        LocateOutParams pre_model = new LocateOutParams();
        //        if (!FindModel(nModelID, locateParam, out pre_model))
        //        {
        //            return false;
        //        }
        //        HOperatorSet.VectorAngleToRigid(modelCenter.dModelRow, modelCenter.dModelCol, modelCenter.dModelAngle,
        //                                           pre_model.dModelRow, pre_model.dModelCol, pre_model.dModelAngle, out hv_HomMat2D);
        //        HOperatorSet.AffineTransImage(m_hImage, out ho_ImageTrans, hv_HomMat2D, "constant", "false");
        //        HOperatorSet.TrainVariationModel(ho_ImageTrans, hv_VariationModelID);
        //        m_hWnd.DispObj(ho_ImageTrans);

        //        HOperatorSet.GetVariationModel(out ho_MeanImage, out ho_VarImage, hv_VariationModelID);
        //        HOperatorSet.PrepareVariationModel(hv_VariationModelID, 20, 3);
        //        //We can now free the training data to save some memory.

        //        return true;
        //    }
        //    catch (HalconException error)
        //    {
        //        MessageBox.Show(error.ToString());
        //        return false;
        //    }
        //    finally
        //    {
        //        HOperatorSet.ClearTrainDataVariationModel(hv_VariationModelID);
        //        if (null != ho_ImageTrans) ho_ImageTrans.Dispose();
        //        if (null != ho_MeanImage) ho_MeanImage.Dispose();
        //        if (null != ho_VarImage) ho_VarImage.Dispose();
        //    }
        //}

        /*适用于印刷体检测*/
        //public bool VariationCheck(LocateInParams locateParam, LocateOutParams modelCenter, int nModelID)
        //{
        //    HTuple hv_width = new HTuple(), hv_height = new HTuple();
        //    HTuple hv_VariationModelID = new HTuple(), hv_HomMat2D = new HTuple();

        //    HObject ho_ImageTrans = null, ho_modelCont = null;
        //    HObject ho_contTrans = null, ho_RegionROI = null, ho_ImageReduced = null, ho_RegionDiff = null;

        //    HOperatorSet.GenEmptyObj(out ho_ImageTrans);
        //    HOperatorSet.GenEmptyObj(out ho_modelCont);
        //    HOperatorSet.GenEmptyObj(out ho_contTrans);
        //    HOperatorSet.GenEmptyObj(out ho_RegionROI);
        //    HOperatorSet.GenEmptyObj(out ho_ImageReduced);
        //    HOperatorSet.GenEmptyObj(out ho_RegionDiff);

        //    try
        //    {
        //        LocateOutParams pre_model = new LocateOutParams();
        //        if (!FindModel(nModelID, locateParam, out pre_model))
        //        {
        //            return false;
        //        }
        //        HOperatorSet.VectorAngleToRigid(modelCenter.dModelRow, modelCenter.dModelCol, modelCenter.dModelAngle,
        //                                           pre_model.dModelRow, pre_model.dModelCol, pre_model.dModelAngle, out hv_HomMat2D);
        //        HOperatorSet.AffineTransImage(m_hImage, out ho_ImageTrans, hv_HomMat2D, "constant", "false");
        //        //设定对比区域
        //        HOperatorSet.GetShapeModelContours(out ho_modelCont, nModelID, 1);
        //        HOperatorSet.AffineTransContourXld(ho_modelCont, out ho_contTrans, hv_HomMat2D);
        //        HOperatorSet.ShapeTransXld(ho_contTrans, out ho_contTrans, "convex");
        //        HOperatorSet.GenRegionContourXld(ho_contTrans, out ho_RegionROI, "filled");
        //        HOperatorSet.ReduceDomain(ho_ImageTrans, ho_RegionROI, out ho_ImageReduced);
        //        //与模板比较，找出异常点
        //        HOperatorSet.CompareVariationModel(ho_ImageReduced, out ho_RegionDiff, hv_VariationModelID);
        //        HOperatorSet.Connection(ho_RegionDiff, out ho_RegionDiff);
        //        //筛选
        //        //HOperatorSet.SelectShape(ho_RegionDiff, out ho_RegionsError, "area", "and", 20, 1000000);
        //        //HOperatorSet.CountObj(ho_RegionsError, out hv_NumError);

        //        return true;
        //    }
        //    catch (HalconException error)
        //    {
        //        error.ToString();
        //        return false;
        //    }
        //    finally
        //    {

        //    }
        //}

        #region XY标定
        public static double m_dCalibVal = 0.018;    //标定系数
        public bool GetCalibVal(PointF pStart, PointF pEnd, double[] pMm, out double dCalibVal, out double dAngleX, out double dAngleY)
        {
            dCalibVal = 0;
            dAngleX = 0;
            dAngleY = 0;
            try
            {
                HOperatorSet.DistancePp(pStart.Y, pStart.X, pEnd.Y, pEnd.X, out HTuple hv_DistPixel);
                double dmm = pMm[1] - pMm[0];
                dCalibVal = Math.Round(dmm / hv_DistPixel.D, 3);
                HOperatorSet.GetImageSize(m_hImage, out HTuple hv_width, out HTuple hv_height);
                m_hWnd.SetColor("red");
                m_hWnd.SetDraw("fill");
                m_hWnd.SetLineWidth(2);
                m_hWnd.DispCross((double)pStart.Y, pStart.X, 25, 0);
                m_hWnd.DispCross((double)pEnd.Y, pEnd.X, 25, 0);
                m_hWnd.DispArrow((double)pStart.Y, (double)pStart.X, (double)pEnd.Y, (double)pEnd.X, 10);
                m_hWnd.SetColor("green");

                m_hWnd.SetLineStyle(10);
                m_hWnd.DispLine(pStart.Y, pStart.X, pStart.Y, hv_width);
                m_hWnd.DispLine((double)pStart.Y, pStart.X, 0, pStart.X);
                m_hWnd.SetLineStyle(new HTuple());
                //与水平方向的夹角:顺时针方向为负值，逆时针方向为正值，取值范围-PI ~ PI
                HOperatorSet.AngleLl(pStart.Y, pStart.X, pEnd.Y, pEnd.X,
                                     pStart.Y, pStart.X, pStart.Y, hv_width, out HTuple hv_angleX);
                dAngleX = hv_angleX.TupleDeg();
                //与垂直方向的夹角
                HOperatorSet.AngleLl(pStart.Y, pStart.X, pEnd.Y, pEnd.X,
                                     pStart.Y, pStart.X, 0, pStart.X, out HTuple hv_angleY);
                dAngleY = hv_angleY.TupleDeg();
                return true;
            }
            catch (HalconException ex)
            {
                StaticFun.MessageFun.ShowMessage("计算标定系数错误：" + ex.ToString());
                return false;
            }
            finally
            {
                m_hWnd.SetDraw("margin");
            }
        }

        public bool Offset(LocateParam modelLocate, CenterParam nowCenter, out double dOffsetX, out double dOffsetY)
        {
            dOffsetX = 0;
            dOffsetY = 0;

            HTuple hv_Dist = new HTuple(), hv_AngleXY = new HTuple(), hv_AngleYX = new HTuple();
            HTuple hv_AngleX = new HTuple(), hv_AngleY = new HTuple(), hv_AngleZ = new HTuple();
            try
            {
                //当前点到模板点的距离
                HOperatorSet.DistancePp(modelLocate.modelCenter.point.X, modelLocate.modelCenter.point.Y, nowCenter.point.X, nowCenter.point.Y, out hv_Dist);
                double dDist = hv_Dist.D * m_dCalibVal;
                //偏移量与运动轴X的夹角
                HOperatorSet.AngleLl(nowCenter.point.Y, nowCenter.point.X, modelLocate.modelCenter.point.Y, modelLocate.modelCenter.point.X,
                                     modelLocate.lineX.dStartRow, modelLocate.lineX.dStartCol, modelLocate.lineX.dEndRow, modelLocate.lineX.dEndCol, out hv_AngleX);
                //偏移量与运动轴Y的夹角
                HOperatorSet.AngleLl(nowCenter.point.Y, nowCenter.point.X, modelLocate.modelCenter.point.Y, modelLocate.modelCenter.point.X,
                                     modelLocate.lineY.dStartRow, modelLocate.lineY.dStartCol, modelLocate.lineY.dEndRow, modelLocate.lineY.dEndCol, out hv_AngleY);

                //计算偏移量dX,dY
                double dAngleX = Math.Abs(hv_AngleX.D);
                double dAngleY = Math.Abs(hv_AngleY.D);
                //double dAngleYX = Math.Abs(hv_AngleYX.D);
                if (dAngleX > (Math.PI / 2))
                {
                    dAngleX = Math.PI - dAngleX;
                }
                if (dAngleY > (Math.PI / 2))
                {
                    dAngleY = Math.PI - dAngleY;
                }
                double dAngleZ = Math.PI - dAngleX - dAngleY;
                double dX = Math.Sin(dAngleY) / Math.Sin(dAngleZ) * dDist;
                double dY = Math.Sin(dAngleX) / Math.Sin(dAngleZ) * dDist;
                //在第一项目和第四项目为正
                //运动轴X与运动轴Y的夹角：输入为图像坐标
                HOperatorSet.AngleLl(modelLocate.lineY.dStartRow, modelLocate.lineY.dStartCol, modelLocate.lineY.dEndRow, modelLocate.lineY.dEndCol,
                                     modelLocate.lineX.dStartRow, modelLocate.lineX.dStartCol, modelLocate.lineX.dEndRow, modelLocate.lineX.dEndCol, out hv_AngleYX);
                //计算偏移量的符号：（不能基于标定坐标系）
                HOperatorSet.TupleSgn(hv_AngleX - 0, out HTuple hv_sgnX);
                HOperatorSet.TupleSgn(hv_AngleYX - 0, out HTuple hv_sgnYX);
                //如果角度方向相同
                dOffsetX = -1 * dX;
                if (hv_sgnX.I == hv_sgnYX.I && Math.Abs(hv_AngleX.D) < Math.Abs(hv_AngleYX.D))
                {
                    dOffsetX = dX;
                }
                HOperatorSet.AngleLl(modelLocate.lineY.dEndRow, modelLocate.lineY.dEndCol, modelLocate.lineY.dStartRow, modelLocate.lineY.dStartCol,
                                    modelLocate.lineX.dStartRow, modelLocate.lineX.dStartCol, modelLocate.lineX.dEndRow, modelLocate.lineX.dEndCol, out HTuple hv_AngleYX1);
                if (hv_sgnX.I != hv_sgnYX.I && Math.Abs(hv_AngleX.D) < Math.Abs(hv_AngleYX1.D))
                {
                    dOffsetX = dX;
                }
                //运动轴X与运动轴Y的夹角：输入为图像坐标
                HOperatorSet.AngleLl(modelLocate.lineX.dStartRow, modelLocate.lineX.dStartCol, modelLocate.lineX.dEndRow, modelLocate.lineX.dEndCol,
                                     modelLocate.lineY.dStartRow, modelLocate.lineY.dStartCol, modelLocate.lineY.dEndRow, modelLocate.lineY.dEndCol, out hv_AngleXY);
                HOperatorSet.TupleSgn(hv_AngleY - 0, out HTuple hv_sgnY);
                HOperatorSet.TupleSgn(hv_AngleXY - 0, out HTuple hv_sgnXY);
                dOffsetY = -1 * dY;
                if (hv_sgnY.I == hv_sgnXY.I && Math.Abs(hv_AngleY.D) < Math.Abs(hv_AngleXY.D))
                {
                    dOffsetY = dY;
                    // dY = dY;
                }
                HOperatorSet.AngleLl(modelLocate.lineX.dEndRow, modelLocate.lineX.dEndCol, modelLocate.lineX.dStartRow, modelLocate.lineX.dStartCol,
                                     modelLocate.lineY.dStartRow, modelLocate.lineY.dStartCol, modelLocate.lineY.dEndRow, modelLocate.lineY.dEndCol, out HTuple hv_AngleXY1);
                if (hv_sgnY.I != hv_sgnXY.I && Math.Abs(hv_AngleY.D) < Math.Abs(hv_AngleXY1.D))
                {
                    dOffsetY = dY;
                }
                return true;

            }
            catch (SystemException ex)
            {
                ex.ToString().ToLog();
                return false;
            }
        }
        #endregion

    }

}



