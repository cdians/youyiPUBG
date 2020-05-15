using System;
using System.Runtime.InteropServices;

namespace YouYiApp.common
{
    public class OCR
    {
        [DllImport("ML.dll", EntryPoint = "OCR", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetOCR(string file, int type);

        [DllImport("ML.dll", EntryPoint = "OCRpart", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OCRpart(string file, int type, int startX, int startY, int width, int height);

        [DllImport("ML.dll", EntryPoint = "OCRBarCodes", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OCRBarCodes(string file, int type);

        [DllImport("ML.dll", EntryPoint = "OCRpartBarCodes", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OCRpartBarCodes(string file, int type, int startX, int startY, int width, int height);

    }
}
