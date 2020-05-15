using System;
using System.Configuration;
using System.IO;
using System.Text;
using YouYiApp.config;

namespace YouYiApp.common
{
    public class LogHelper
    {

        public static void ShowLog(string format, object arg0)
        {
            if (BaseConfig.DEBUG)
            {
                string str = String.Format(format, arg0);
                Console.WriteLine(DateTime.Now.ToString() + "  " + str);
                WriteLog(str);
            }
        }

        public static void ShowLog(string format, params object[] arg)
        {
            if (BaseConfig.DEBUG)
            {
                string str = String.Format(format, arg);
                Console.WriteLine(DateTime.Now.ToString() + "  " + str);
                WriteLog(str);
            }
        }

        public static void WriteLog(string msg)
        {
            string path = string.Empty;
            try
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
            }
            catch (Exception)
            {
                path = @"c:\temp";
            }
            if (string.IsNullOrEmpty(path))
                path = @"c:\temp";
            try
            {
                //如果日志目录不存在,则创建该目录
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string logFileName = path + "\\程序日志_" + DateTime.Now.ToString("yyyy_MM_dd_HH") + ".log";
                StringBuilder logContents = new StringBuilder();
                byte[] b = Encoding.Default.GetBytes(msg);
                //转成 Base64 形式的 System.String  
                msg = Convert.ToBase64String(b);
                // 转回原来的string
                //byte[] c = Convert.FromBase64String(msg);
                //msg = Encoding.Default.GetString(c);
                logContents.AppendLine(msg);
                //当天的日志文件不存在则新建，否则追加内容
                StreamWriter sw = new StreamWriter(logFileName, true, Encoding.Unicode);
                sw.Write(DateTime.Now.ToString("yyyy-MM-dd hh:mm:sss") + " " + logContents.ToString());
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}