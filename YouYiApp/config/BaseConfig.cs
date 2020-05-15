using System;
using System.IO;

namespace YouYiApp.config
{
    class BaseConfig
    {
        // APP版本号
        public const string APP_VERSION = "0.0.3";

        // 日志debug开关
        public const bool DEBUG = true;

        // 设备vid
        public const string YOUXI_VID = "0329";

        // 激活秘钥
        public static string AC_SECRET = "f2Lr*qD5IVKS";

        // 设备pid
        public const string YOUXI_PID = "1202";

        public static string CONFIG_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");//在当前程序路径创建

        // 数据库存放路径
        public static string DATA_PATH_30 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "youyi.png");//在当前程序路径创建

        public static string DATA_PATH_50 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "youyi50.png");//在当前程序路径创建

        // 数据库秘钥
        public static string DATA_PATH_SECRET = "5fwXoZXWXOSkjcO&";

    }
}
