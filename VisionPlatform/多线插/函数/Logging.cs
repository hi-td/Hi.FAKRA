/***********************************************************
* CLR版本：4.0.30319.42000
* 类 名 称：Logging
* 机器名称：DESKTOP-5NCLPK1
* 命名空间：海蓝智能视觉软件1._0.函数
* 文 件 名：Logging
* 创建时间：2022/4/26 16:02:29
* 作    者：BaoPengYu
* 公    司：HaiLan Intelligent
* 说   明：
* 修改时间：
* 修 改 人：
* 修改说明：
* 深圳市海蓝智能科技有限公司  2021  保留所有权利.
***********************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VisionPlatform
{
    public static class Logging
    {
        private static readonly ReaderWriterLockSlim logWriteLock = new ReaderWriterLockSlim();

        public static string LOG_PATH = @"D:\Program Files\Hi.Ltd\Log";
        public static void Log(this string message)
        {
            try
            {
                logWriteLock.EnterWriteLock();
                if (Directory.GetLogicalDrives().Contains(@"D:\"))
                {
                    DirectoryInfo info = new DirectoryInfo(LOG_PATH);
                    if (!info.Exists)
                    {
                        info.Create();
                    }
                    using (StreamWriter sw = new StreamWriter(LOG_PATH + $@"\Logging{DateTime.Now:_yyyy-MM-dd}.log", true, Encoding.UTF8))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":\t" + message);
                    }
                }
                else
                {
                    string currentPath = $@"{Directory.GetParent(Environment.CurrentDirectory).FullName}\Log";
                    using (StreamWriter sw = new StreamWriter(currentPath + $@"\Logging{DateTime.Now:_yyyy-MM-dd}.log", true, Encoding.UTF8))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":\t" + message);
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Log(MethodBase.GetCurrentMethod());
            }
            finally
            {
                logWriteLock.ExitWriteLock();
            }
        }
        public static void Log(this Exception ex, MethodBase methodBase) => ($"[{methodBase.ReflectedType.Name}]->[{methodBase.Name}]:\t" + ex.Message + Environment.NewLine + ex.StackTrace).Log();
        public static void Log(this Exception ex, string className, string methodName) => ($"[{className}]->[{methodName}]:\t" + ex.Message + Environment.NewLine + ex.StackTrace).Log();
        public static void Log(this string message, MethodBase methodBase) => ($"[{methodBase.ReflectedType.Name}]->[{methodBase.Name}]:\t" + message).Log();
        public static void Log(this string message, string className, string methodName) => ($"[{className}]->[{methodName}]:\t" + message).Log();
        public static void Log(this Exception ex) => ($"Hi.Ltd.Errors->Logging:\t" + ex.Message + Environment.NewLine + ex.StackTrace).Log();
    }
}
