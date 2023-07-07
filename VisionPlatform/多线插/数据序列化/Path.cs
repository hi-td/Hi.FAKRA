using System.IO;

namespace GlobalPath
{
    public class SavePath
    {
        private static string configFileFold = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Config");
        private static string modelFileFlod = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Model");     //模型json文件保存路径
        private static string JosnFileFlod = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Josn");
        private static string ModelImageFold = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "模板图像");   //注：不包含文件名称
        private static string VideoFold = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "TeachVideo");   //视频教程
        private static string ImageFold = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "图像保存");
        static SavePath()
        {
            if (!Directory.Exists(VideoFold))
            {
                System.IO.Directory.CreateDirectory(VideoFold);
            }
            if (!Directory.Exists(configFileFold))
            {
                System.IO.Directory.CreateDirectory(configFileFold);
            }
            if (!Directory.Exists(modelFileFlod))
            {
                System.IO.Directory.CreateDirectory(modelFileFlod);
            }
            if (!Directory.Exists(ModelImagePath))
            {
                System.IO.Directory.CreateDirectory(ModelImagePath);
            }
            if (!Directory.Exists(JosnFileFlod))   
            {
                Directory.CreateDirectory(JosnFileFlod);
            }
            if (!Directory.Exists(ImageFold))
            {
                Directory.CreateDirectory(ImageFold);
            }
        }
        //软件配置初始化json文件保存路径
        public static string InitConfigPath = configFileFold + "\\InitConfig.json";
        //相机配置文件路径
        public static string CamConfigPath = configFileFold + "\\CamConfig.json";
        //端子检测组合配置文件
        public static string TMCheckListPath = configFileFold + "\\TMConfig.json";
        //端子检测数据显示位置
        public static string TMShowSetPath = configFileFold + "\\TMShowSet.json";
        //公司名字
        public static string CompanyNamePath = configFileFold + "\\CompanyName.json";
        //联系我们
        public static string ContactPath = configFileFold + "\\Contact.json";
        //IO时间
        public static string IOPath = configFileFold + "\\IO.json";
        //图片保存天数
        public static string TimePath = configFileFold + "\\Time.json";
        //数据保存路径
        public static string GlobalDataPath = JosnFileFlod + "\\";   //注：不包含文件名称
        //最新保存的数据名称路径
        public static string NewestFile = JosnFileFlod + "\\NewestFileName.json";
        //保存模板路径
        public static string ModelPath = modelFileFlod + "\\";   //注：不包含文件名称
        //模板图片保存路径 
        public static string ModelImagePath = ModelImageFold + "\\";
        //教学视频路径
        public static string VideoPath = VideoFold + "\\端子机视觉检测教学.mov";

        #region 相机1图像保存
        //保存原始OK图像
        public static string cam1_OrgImagePath_OK = ImageFold + "\\相机1\\原始图像\\OK\\";   //注：不包含文件名称
        //保存原始NG图像
        public static string cam1_OrgImagePath_NG = ImageFold + "\\相机1\\原始图像\\NG\\";   //注：不包含文件名称
        
        //保存结果OK图像                                                                                                                    
        public static string cam1_ReslutImagePath_OK = ImageFold + "\\相机1\\结果图像\\OK\\";   //注：不包含文件名称
        //保存结果NG图像                                
        public static string cam1_ReslutImagePath_NG = ImageFold+ "\\相机1\\结果图像\\NG\\";   //注：不包含文件名称
        #endregion

        #region 相机2图像保存
        //保存原始OK图像
        public static string cam2_OrgImagePath_OK = ImageFold + "\\相机2\\原始图像\\OK\\";   //注：不包含文件名称
        //保存原始NG图像
        public static string cam2_OrgImagePath_NG = ImageFold + "\\相机2\\原始图像\\NG\\" ;   //注：不包含文件名称

        //保存结果OK图像                                                                                                                    
        public static string cam2_ReslutImagePath_OK = ImageFold + "\\相机2\\结果图像\\OK\\";   //注：不包含文件名称
        //保存结果NG图像
        public static string cam2_ReslutImagePath_NG = ImageFold + "\\相机2\\结果图像\\NG\\";   //注：不包含文件名称
        #endregion

        #region 相机3图像保存
        //保存原始OK图像
        public static string cam3_OrgImagePath_OK = ImageFold + "\\相机3\\原始图像\\OK\\";   //注：不包含文件名称
        //保存原始NG图像
        public static string cam3_OrgImagePath_NG = ImageFold + "\\相机3\\原始图像\\NG\\";   //注：不包含文件名称

        //保存结果OK图像                                                                                                                    
        public static string cam3_ReslutImagePath_OK = ImageFold + "\\相机3\\结果图像\\OK\\";   //注：不包含文件名称
        //保存结果NG图像
        public static string cam3_ReslutImagePath_NG = ImageFold + "\\相机3\\结果图像\\NG\\";   //注：不包含文件名称
        #endregion

        #region 相机4图像保存
        //保存原始OK图像
        public static string cam4_OrgImagePath_OK = ImageFold + "\\相机4\\原始图像\\OK\\";   //注：不包含文件名称
        //保存原始NG图像
        public static string cam4_OrgImagePath_NG = ImageFold + "\\相机4\\原始图像\\NG\\";   //注：不包含文件名称

        //保存结果OK图像                                                                                                                    
        public static string cam4_ReslutImagePath_OK = ImageFold + "\\相机4\\结果图像\\OK\\";   //注：不包含文件名称
        //保存结果NG图像
        public static string cam4_ReslutImagePath_NG = ImageFold + "\\相机4\\结果图像\\NG\\";   //注：不包含文件名称
        #endregion
}


}
