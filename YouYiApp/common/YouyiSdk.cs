using System;
using System.Runtime.InteropServices;

namespace YouYiApp.common
{
    // 未完全引入所有功能
    public class YouyiSdk
    {

        [DllImport("youyi.dll", EntryPoint = "FdP2tnbvoSHxR", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr M_Open(int port);
        [DllImport("youyi.dll", EntryPoint = "GTQiKnPag", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr M_Open_VidPid(int Vid, int Pid);

        [DllImport("youyi.dll", EntryPoint = "D8dE6wlJNnD", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_Close(IntPtr m_hdl);

        //获取设备序列号
        [DllImport("youyi.dll", EntryPoint = "tfkY3KyF64VML", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_GetDevSn(IntPtr m_hdl, ref int dwp_LenResponse, ref byte ucp_Response);

        //写入用户数据
        [DllImport("youyi.dll", EntryPoint = "gYVIixiALO3k7", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_SetUserData(IntPtr m_hdl, int dw_LenUserData, ref byte ucp_UserData);
        //验证用户数据
        [DllImport("youyi.dll", EntryPoint = "bGD7541I2", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_VerifyUserData(IntPtr m_hdl, int dw_LenUserData, ref byte ucp_UserData);
        /***********键盘操作函数;以下函数中的m_hdl是指M_Open返回的句柄*******/
        /***********以下所有命令返回 0: 成功；-1: 失败*******/
        //单击(按下后立刻弹起)按键  //HidKeyCode: 键盘码; Nbr: 按下次数
        [DllImport("youyi.dll", EntryPoint = "dVMSEykhKbeLL", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_KeyPress(IntPtr m_hdl, int HidKeyCode, int Nbr);
        //按下某个按键不弹起        //HidKeyCode: 键盘码
        [DllImport("youyi.dll", EntryPoint = "WSj5LwkRv", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_KeyDown(IntPtr m_hdl, int HidKeyCode);
        //弹起某个按键              //HidKeyCode: 键盘码
        [DllImport("youyi.dll", EntryPoint = "KpEfM0hnIi", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_KeyUp(IntPtr m_hdl, int HidKeyCode);
        //读取按键状态              //HidKeyCode: 键盘码 //返回 0: 弹起状态；1:按下状态；-1: 失败(端口未打开)
        //使用该接口，不允许手工操作键盘，否则该接口返回值有可能不正确
        [DllImport("youyi.dll", EntryPoint = "mc5kEXs1ddNB6", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_KeyState(IntPtr m_hdl, int HidKeyCode);

        //单击(按下后立刻弹起)按键  //KeyCode: 键盘码; Nbr: 按下次数
        [DllImport("youyi.dll", EntryPoint = "rEMS2TxiVF", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_KeyPress2(IntPtr m_hdl, int KeyCode, int Nbr);
        //按下某个按键不弹起        //KeyCode: 键盘码
        [DllImport("youyi.dll", EntryPoint = "aqFpzOLHB1f", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_KeyDown2(IntPtr m_hdl, int KeyCode);
        //弹起某个按键              //KeyCode: 键盘码
        [DllImport("youyi.dll", EntryPoint = "fsKgfDT2i", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_KeyUp2(IntPtr m_hdl, int KeyCode);
        //读取按键状态              //KeyCode: 键盘码 //返回 0: 弹起状态；1:按下状态；-1: 失败(端口未打开)
        //使用该接口，不允许手工操作键盘，否则该接口返回值有可能不正确
        [DllImport("youyi.dll", EntryPoint = "LSfu6Bm5vjDjO", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_KeyState2(IntPtr m_hdl, int KeyCode);
        //弹起所有按键
        [DllImport("youyi.dll", EntryPoint = "L4mV0Hrw6oYVM", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_ReleaseAllKey(IntPtr m_hdl);
        /***********鼠标操作函数;以下函数中的m_hdl是指M_Open返回的句柄*******/
        /***********以下所有命令返回 0: 成功；-1: 失败*******/
        //左键单击   //Nbr: 左键在当前位置单击次数 
        [DllImport("youyi.dll", EntryPoint = "oIrmqycULtq0", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_LeftClick(IntPtr m_hdl, int Nbr);
        //左键双击   //Nbr: 左键在当前位置双击次数
        [DllImport("youyi.dll", EntryPoint = "xj6Sqo75Ps", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_LeftDoubleClick(IntPtr m_hdl, int Nbr);
        //按下左键不弹起
        [DllImport("youyi.dll", EntryPoint = "EaiMD3OWJ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_LeftDown(IntPtr m_hdl);
        //弹起左键
        [DllImport("youyi.dll", EntryPoint = "zTEY7ivfyMl", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_LeftUp(IntPtr m_hdl);
        //右键单击   //Nbr: 左键在当前位置单击次数
        [DllImport("youyi.dll", EntryPoint = "LVKOPrLjYFG", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_RightClick(IntPtr m_hdl, int Nbr);
        //按下右键不弹起
        [DllImport("youyi.dll", EntryPoint = "lXVtRfQDwiHJT", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_RightDown(IntPtr m_hdl);
        //弹起右键
        [DllImport("youyi.dll", EntryPoint = "B1k7uGyRJBLp", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_RightUp(IntPtr m_hdl);
        //中键单击   //Nbr: 左键在当前位置单击次数
        [DllImport("youyi.dll", EntryPoint = "el5Y3aSwd", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_MiddleClick(IntPtr m_hdl, int Nbr);
        //按下中键不弹起
        [DllImport("youyi.dll", EntryPoint = "gxVQSinZp", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_MiddleDown(IntPtr m_hdl);
        //弹起中键
        [DllImport("youyi.dll", EntryPoint = "fAWCtXWw5H", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_MiddleUp(IntPtr m_hdl);
        //弹起鼠标的所有按键
        [DllImport("youyi.dll", EntryPoint = "wq3C4TPV9k", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_ReleaseAllMouse(IntPtr m_hdl);
        //读取鼠标左中右键状态  //MouseKeyCode: 1=左键 2=右键 3=中键  //返回 0: 弹起状态；1:按下状态；-1: 失败
        //只能读取盒子中鼠标的状态，读取不到实体鼠标的状态
        [DllImport("youyi.dll", EntryPoint = "Twje2ofJ4U", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_MouseKeyState(IntPtr m_hdl, int MouseKeyCode);

        //将鼠标移动到原点(0,0)  在出现移动出现异常时，可以用该函数将鼠标复位
        [DllImport("youyi.dll", EntryPoint = "TcrGQ1LqId", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_ResetMousePos(IntPtr m_hdl);
        //从当前位置移动鼠标    x: x方向（横轴）的距离（正:向右; 负值:向左）; y: y方向（纵轴）的距离（正:向下; 负值:向上）
        [DllImport("youyi.dll", EntryPoint = "n9udiS2z8Du", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_MoveR(IntPtr M_hdl, int x, int y);
        //移动鼠标到指定坐标    x: x方向（横轴）的坐标; y: y方向（纵轴）的坐标。坐标原点(0, 0)在屏幕左上角
        //注意：如果出现过将鼠标移动的距离超过屏幕大小，再次MoveTo可能会出现无法正确移动到指定坐标的问题，如果出现该问题，需调用ResetMousePos复位
        [DllImport("youyi.dll", EntryPoint = "PBch1SFALDBkn", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_MoveTo(IntPtr m_hdl, int x, int y);
        //读取当前鼠标所在坐标  返回坐标在x、y中。 
        //注意：该函数必须在执行一次MoveTo或ResetMousePos函数后才能正确执行！
        //注意：如果曾经出现过将鼠标移动的距离超过屏幕大小，这里读取到的坐标值有可能是不正确的！如果出现该问题，需调用ResetMousePos复位
        [DllImport("youyi.dll", EntryPoint = "lGH1PbMLekC", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_GetCurrMousePos(IntPtr m_hdl, ref int x, ref int y);

        //以下接口仅适用主控机和被控机是同一台电脑的使用方式(单头模块；双头模块的两个USB头都连接到同一台电脑)
        //以下接口将调用系统的API来获取当前鼠标位置，DLL将不记录鼠标移动的位置
        //移动鼠标到指定坐标    x: x方向（横轴）的坐标; y: y方向（纵轴）的坐标。
        [DllImport("youyi.dll", EntryPoint = "cQb5SeO3o8qB", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_MoveR2(IntPtr m_hdl, int x, int y);
        //移动鼠标到指定坐标    x: x方向（横轴）的坐标; y: y方向（纵轴）的坐标。坐标原点(0, 0)在屏幕左上角
        [DllImport("youyi.dll", EntryPoint = "SFgtGo4kR", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_MoveTo2(IntPtr m_hdl, int x, int y);
        //读取当前鼠标所在坐标  返回坐标在x、y中。
        [DllImport("youyi.dll", EntryPoint = "P6uKqn92Qs5H", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_GetCurrMousePos2(ref int x, ref int y);

        //以下接口将使用绝对移动功能。该功能目前还不能支持安卓
        //在使用绝对移动功能前，必须先输入被控机的屏幕分辨率
        //x: x方向（横轴）的坐标; y: y方向（纵轴）的坐标。坐标原点(0, 0)在屏幕左上角
        //返回值如果是-10，表示该盒子不支持绝对移动功能。返回0表示执行正确。可以用该接口判断盒子是否支持绝对移动功能
        [DllImport("youyi.dll", EntryPoint = "Ma5uBpUfl", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int M_ResolutionUsed(IntPtr m_hdl, int x, int y);

    }
}
