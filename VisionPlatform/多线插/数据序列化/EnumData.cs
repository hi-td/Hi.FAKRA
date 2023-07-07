using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumData
{
    //相机颜色：彩色或者黑白
    public enum CamColor
    {
        color,
        mono
    }
    //镜像图像
    public enum Mirror
    {
        Original,
        Up_Down,      //上下镜像
        Left_Right    //左右镜像
    }
   //相机品牌
   public enum CamBrand
    {
        DaHeng,
        HiKVision,
        DaHua,
        Other
    }

    public enum COMType
    {
        COM,
        NET,
        IO
    }

    public enum LEDType
    {
        NUM,
        SIM
    }

    //通讯方式：串口
    public enum COM
    {
        Modbus
    }

    //通讯方式：板卡
    public enum IO
    {
        //板卡品牌
        WENYU8,
        WENYU16,
        WENYU232
    }
    public enum LED
    {
        //数字光源通道
        CHTWO,
        CHFOUR,
        CHSIX
    }
   
}
