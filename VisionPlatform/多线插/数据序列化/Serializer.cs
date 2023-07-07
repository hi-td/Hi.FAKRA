using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BaseData;
using EnumData;

namespace GlobalData
{
    //框架通用序列化数据
    public class Config
    {
        //软件启动初始化配置：相机，通讯方式等
        public class InitConfig
        {
            public ConfigData initConfig = new ConfigData();
            public OtherConfig otherConfig = new OtherConfig();
        }
        public static InitConfig _InitConfig = new InitConfig();

        public class ContactUs
        {
            public ContactData contact = new ContactData();
        }
        public static ContactUs _Contact = new ContactUs();
        //相机配置
        public class CamConfig
        {
            public Dictionary<int , string> camConfig = new Dictionary<int, string>();   //相机及其对应的序列号                                           
            //public Dictionary<string, CamSDK.CamCommon.CamParam> camParam = new Dictionary<string, CamSDK.CamCommon.CamParam>();    //相机序列号及其对应的参数
            public Dictionary<int, List<Mirror>> ImageMirror = new Dictionary<int, List<Mirror>>();            //图像翻转
        }
        public static CamConfig _CamConfig = new CamConfig();

    }

}
