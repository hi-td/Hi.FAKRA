#define DOG
using CamSDK;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionPlatform.Auxiliary;
using static VisionPlatform.Auxiliary.Dog;
using static VisionPlatform.Auxiliary.Variable;
using static VisionPlatform.Security.Md5;
using static VisionPlatform.Auxiliary.Data;

namespace VisionPlatform
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //获取欲启动进程名
            var strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //检查进程是否已经启动，已经启动则显示报错信息退出程序。 
            if (System.Diagnostics.Process.GetProcessesByName(strProcessName).Length > 1)
            {
                MessageBox.Show("程序正在运行中,请勿重新打开！", "重复打开提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(0);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");    //英文版
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");  //中文版

                //读取软件初始化配置文件
                string path = GlobalPath.SavePath.InitConfigPath;
                if (File.Exists(path))
                {
                    //将json返回dynamic对象
                    string strConfigData = System.IO.File.ReadAllText(path);
                    var DynamicObject = JsonConvert.DeserializeObject<GlobalData.Config.InitConfig>(strConfigData);
                    GlobalData.Config._InitConfig = DynamicObject;
                    CamCommon.m_camBrand = GlobalData.Config._InitConfig.initConfig.camBrand;
                    //导入公司名称
                    string str_company = null;
                    try
                    {
                        if (File.Exists(GlobalPath.SavePath.CompanyNamePath))
                        {
                            string strData = System.IO.File.ReadAllText(GlobalPath.SavePath.CompanyNamePath);
                            strData = Registered.DESDecrypt(strData);
                            var DynamicObject1 = JsonConvert.DeserializeObject<string>(strData);
                            str_company = DynamicObject1;
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        StaticFun.MessageFun.ShowMessage("公司名称导入出错。");
                    }
                    var main = new FormMainUI(str_company);

#if DOG
                    if (FindPort(0, ref DogPath) != 0)
                    {
                        MessageBox.Show("未找到加密狗,请插入加密狗后，再进行操作。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(0);
                    }
                    else
                    {
                        Company = GetValue("Company", DogPath);
                        Author = GetValue("Author", DogPath);
                        ModeChar = GetValue("ModeChar", DogPath);
                        Md5Value = GetValue("Md5", DogPath);
                        SerserialNumber = GetValue("SerserialNumber", DogPath);
                        GetOrder = GetValue("GetOrder", DogPath);
                        GetState = GetValue("GetState", DogPath);
                        GetAdaptImageSize = GetValue("GetAdaptImageSize", DogPath);
                        GetAdministrator = GetValue("GetAdministrator", DogPath);
                        GetEnvPathName = GetValue("GetEnvPathName", DogPath);
                        var filed = GetValue("Filed", DogPath);
                        var fsn = GetValue("FSN", DogPath);
                        //从加密狗中加载公钥
                        RSAKeyValues = new RSAKeyValue()
                        {
                            Modulus = GetValue("GetModulus1", DogPath) + GetValue("GetModulus2", DogPath) + GetValue("GetModulus3", DogPath) + GetValue("GetModulus4", DogPath) + GetValue("GetModulus5", DogPath) + GetValue("GetModulus6", DogPath) + GetValue("GetModulus7", DogPath),
                            D = GetValue("GetD1", DogPath) + GetValue("GetD2", DogPath) + GetValue("GetD3", DogPath) + GetValue("GetD4", DogPath),
                            DP = GetValue("GetDP1", DogPath) + GetValue("GetDP2", DogPath) + GetValue("GetDP3", DogPath) + GetValue("GetDP4", DogPath) + GetValue("GetDP5", DogPath) + GetValue("GetDP6", DogPath),
                            DQ = GetValue("GetDQ1", DogPath) + GetValue("GetDQ2", DogPath) + GetValue("GetDQ3", DogPath) + GetValue("GetDQ4", DogPath) + GetValue("GetDQ5", DogPath) + GetValue("GetDQ6", DogPath),
                            Exponent = GetValue("GetExponent", DogPath),
                            InverseQ = GetValue("GetInverseQ1", DogPath) + GetValue("GetInverseQ2", DogPath) + GetValue("GetInverseQ3", DogPath),
                            P = GetValue("GetP1", DogPath) + GetValue("GetP2", DogPath) + GetValue("GetP3", DogPath) + GetValue("GetP4", DogPath) + GetValue("GetP5", DogPath),
                            Q = GetValue("GetQ1", DogPath) + GetValue("GetQ2", DogPath) + GetValue("GetQ3", DogPath)
                        };
                        if (string.IsNullOrEmpty(Company) || string.IsNullOrEmpty(Author) || string.IsNullOrEmpty(ModeChar) || string.IsNullOrEmpty(Md5Value) || string.IsNullOrEmpty(SerserialNumber) || string.IsNullOrEmpty(GetOrder) || string.IsNullOrEmpty(GetState) || string.IsNullOrEmpty(GetAdaptImageSize))
                        {
                            MessageBox.Show("当前加密狗未经检验，请联系厂家进行出厂校验！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(0);
                        }
                        if (filed != "WiringHarness")
                        {
                            MessageBox.Show("当前加密狗不是此项目的，请联系厂家进行更改！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(0);
                        }
                        else
                        {
                            if (Md5Value.Equals(Encrypt(Company)))
                            {
                                var rsaScurity = @"D:\Program Files\VisionPlatform\License.lic".DeserializeData<byte[]>();
                                if (rsaScurity != null)
                                {
                                    if (rsaScurity.GetString().Equals(Encrypt(RSAKeyValues.P.GetByte().GetString())))
                                    {
                                        GetParameter = Analyze(ModeChar, SerserialNumber, Author);
                                        if (fsn.Equals(Encrypt(filed + GetParameter)))
                                        {
                                            Application.Run(main);
                                        }
                                        else
                                        {
                                            MessageBox.Show("当前加密狗未经授权，请联系厂家进行授权使用！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            Environment.Exit(0);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("当前加密狗未经授权，请联系厂家进行授权使用！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        Environment.Exit(0);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("当前找不到许可证文件，请联系厂家！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Environment.Exit(0);
                                }
                            }
                        }
                    }
#endif

                }
                else
                {
                    Application.Run(new FormUIConfig());
                }
            }
        }
        public static string GetString(this byte[] source) => Convert.ToBase64String(source);
        public static byte[] GetByte(this string source) => Convert.FromBase64String(source);
    }
}
