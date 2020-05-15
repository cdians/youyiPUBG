using System;
using System.Runtime.InteropServices;
using YouYiApp.model;

namespace YouYiApp.common
{
    public class TopMostTool
    {
        public static int SW_SHOW = 5;
        public static int SW_NORMAL = 1;
        public static int SW_MAX = 3;
        public static int SW_HIDE = 0;
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);    //窗体置顶
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);    //取消窗体置顶
        public const uint SWP_NOMOVE = 0x0002;    //不调整窗体位置
        public const uint SWP_NOSIZE = 0x0001;    //不调整窗体大小
        public bool isFirst = true;

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public static void setTopCustomBar(String windowName)
        {
            IntPtr CustomBar = FindWindow(null, windowName);    //CustomBar是我的程序中需要置顶的窗体的名字
            if (CustomBar != null)
            {
                GlobalParam globalParam = GlobalParam.GetGlobalParam();
                IntPtr ptr = HWND_TOPMOST;
                if (globalParam.topWindow)
                {
                    ptr = HWND_NOTOPMOST;
                }
                SetWindowPos(CustomBar, ptr, 0, 0, 0, 0, TopMostTool.SWP_NOMOVE | TopMostTool.SWP_NOSIZE);
            }
        }
        
    }
}