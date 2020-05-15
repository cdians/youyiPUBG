using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace YouYiApp.common
{
    public class SelectColor
    {

        [DllImport("user32.dll")]//取设备场景
        public static extern IntPtr GetDC(IntPtr hwnd);//返回设备场景句柄

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static public extern IntPtr CreateDC(string driverName, string deviceName, string output, IntPtr lpinitData);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]//取指定点颜色
        public static extern int GetPixel(IntPtr hdc, int x, int y);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static public extern bool DeleteDC(IntPtr DC);

        static public byte GetRValue(int color)
        {
            return (byte)color;
        }
        static public byte GetGValue(int color)
        {
            return ((byte)(((short)(color)) >> 8));
        }
        static public byte GetBValue(int color)
        {
            return ((byte)((color) >> 16));
        }
        static public byte GetAValue(int color)
        {
            return ((byte)((color) >> 24));
        }

        public static IntPtr GetHDC()
        {
            return CreateDC("DISPLAY", null, null, IntPtr.Zero);
        }

        public static Color GetColor(IntPtr hdc, Point screenPoint)
        {
            int colorref = GetPixel(hdc, screenPoint.X, screenPoint.Y);
            DeleteDC(hdc);
            byte Red = GetRValue(colorref);
            byte Green = GetGValue(colorref);
            byte Blue = GetBValue(colorref);
            return Color.FromArgb(Red, Green, Blue);
        }

        public static Color GetColor(Point screenPoint)
        {
            IntPtr displayDC = CreateDC("DISPLAY", null, null, IntPtr.Zero);
            int colorref = GetPixel(displayDC, screenPoint.X, screenPoint.Y);
            DeleteDC(displayDC);
            byte Red = GetRValue(colorref);
            byte Green = GetGValue(colorref);
            byte Blue = GetBValue(colorref);
            return Color.FromArgb(Red, Green, Blue);
        }

        public static String GetColorHex(IntPtr hdc, Point screenPoint)
        {
            int colorref = GetPixel(hdc, screenPoint.X, screenPoint.Y);
            Console.WriteLine("colorref {0}", colorref);
            byte Red = GetRValue(colorref);
            byte Green = GetGValue(colorref);
            byte Blue = GetBValue(colorref);
            Color color = Color.FromArgb(Red, Green, Blue);
            return ToHexColor(color);
        }

        public static String GetColorHex(Point screenPoint)
        {
            IntPtr displayDC = CreateDC("DISPLAY", null, null, IntPtr.Zero);

            int colorref = GetPixel(displayDC, screenPoint.X, screenPoint.Y);
            Console.WriteLine("colorref {0}", colorref);
            //int colorref = 7171469;
            DeleteDC(displayDC);
            byte Red = GetRValue(colorref);
            byte Green = GetGValue(colorref);
            byte Blue = GetBValue(colorref);
            Color color = Color.FromArgb(Red, Green, Blue);
            return ToHexColor(color);
        }

        public static String GetColorHexByDC(IntPtr displayDC, Point screenPoint)
        {
            int colorref = GetPixel(displayDC, screenPoint.X, screenPoint.Y);
            //Console.WriteLine("colorref {0}", colorref);
            //int colorref = 7171469;
            //DeleteDC(displayDC);
            byte Red = GetRValue(colorref);
            byte Green = GetGValue(colorref);
            byte Blue = GetBValue(colorref);
            Color color = Color.FromArgb(Red, Green, Blue);
            return ToHexColor(color);
        }

        public static int GetColorInt(IntPtr hdc, Point screenPoint)
        {
            int colorref = GetPixel(hdc, screenPoint.X, screenPoint.Y);
            return colorref;
        }

        public static int GetColorInt(Point screenPoint)
        {
            IntPtr displayDC = CreateDC("DISPLAY", null, null, IntPtr.Zero);
            int colorref = GetPixel(displayDC, screenPoint.X, screenPoint.Y);
            return colorref;
        }

        public static string ToHexColor(Color color)
        {
            if (null == color)
                return "#000000";
            string R = Convert.ToString(color.R, 16);
            if (R == "0")
                R = "00";
            string G = Convert.ToString(color.G, 16);
            if (G == "0")
                G = "00";
            string B = Convert.ToString(color.B, 16);
            if (B == "0")
                B = "00";
            string HexColor = "#" + R + G + B;
            return HexColor.ToUpper();
        }

    }
}
