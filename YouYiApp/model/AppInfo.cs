using System;
using System.Windows;
using YouYiApp.common;
using YouYiApp.config;

namespace YouYiApp.model
{
    public class AppInfo
    {

        public static AppInfo appInfo = null;

        private static object singleton_Lock = new object(); //锁同步

        public AppInfo()
        {
            isInit = false;
            version = BaseConfig.APP_VERSION;
            // 获取设备信息
            screenWidth = SystemParameters.PrimaryScreenWidth; // 屏幕整体宽度
            screenHeight = SystemParameters.PrimaryScreenHeight; // 屏幕整体高度
            LogHelper.ShowLog("设备分辨率：{0} * {1}", screenWidth, screenHeight);
        }

        public bool isInit { get; set; }

        public String version { get; set; }

        public double screenWidth { get; set; }

        public double screenHeight { get; set; }

        public static AppInfo GetAppInfo()
        {
            lock (singleton_Lock)
            {
                if (null == appInfo)
                {
                    appInfo = new AppInfo();
                }
            }
            return appInfo;
        }
    }

}
