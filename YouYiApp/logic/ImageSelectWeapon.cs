using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YouYiApp.common;
using YouYiApp.config;
using YouYiApp.model;
using YouYiApp.model.view;

namespace YouYiApp.logic
{
    public class ImageSelectWeapon
    {

        GlobalParam globalParam = GlobalParam.GetGlobalParam();

        PlayerViewModel playerViewModel = PlayerViewModel.GetPlayerViewModel();

        GunsViewModel gunsViewModel = GunsViewModel.GetGunsViewModel();

        JichuViewModel jichuViewModel = JichuViewModel.GetJichuViewModel();

        Player player = Player.GetPlay();

        ColorDict colorDict = ColorDict.GetColorDict();

        public static ImageSelectWeapon imageSelectWeapon = null;

        private static object singleton_Lock = new object(); //锁同步

        private bool _speakerStatus = false;

        public static ImageSelectWeapon GetImageSelectWeapon()
        {
            lock (singleton_Lock)
            {
                if (null == imageSelectWeapon)
                {
                    imageSelectWeapon = new ImageSelectWeapon();
                }
            }
            return imageSelectWeapon;
        }

        public void ValiLogic()
        {
            // 识别颜色
            DateTime startTime = DateTime.Now;

            if (!ValiOpenBag())
            {
                return;
            }

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/");
            if (System.IO.Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string name = "";
            string name1 = "";

            Bitmap image = new Bitmap(globalParam.weapon0Location["basicSize"].X, globalParam.weapon0Location["basicSize"].Y);
            Bitmap image1 = new Bitmap(globalParam.weapon1Location["basicSize"].X, globalParam.weapon1Location["basicSize"].Y);
                
            Graphics imgGraphics = Graphics.FromImage(image);
            imgGraphics.CopyFromScreen(globalParam.weapon0Location["basic"], new Point(0, 0), new Size(image.Width, image.Height)); //第一个参数是截图开始坐标，第二个参数是要拷贝到的Bitmap的开始位置，保持不变，最后是图片大小
            if (BaseConfig.DEBUG)
            {
                image.Save(path + Guid.NewGuid().ToString() + ".png");
            }
            name = path + GetRandomString(9, true, true, false, false, null);
            CutImage(image, name + ".png");

            Graphics imgGraphics1 = Graphics.FromImage(image1);
            imgGraphics1.CopyFromScreen(globalParam.weapon1Location["basic"], new Point(0, 0), new Size(image1.Width, image1.Height)); //第一个参数是截图开始坐标，第二个参数是要拷贝到的Bitmap的开始位置，保持不变，最后是图片大小
            if (BaseConfig.DEBUG)
            {
                image1.Save(path + Guid.NewGuid().ToString() + ".png");
            }
            name1 = path + GetRandomString(10, true, true, false, false, null);
            CutImage(image1, name1 + ".png");

            try
            {
                string lastWeaponName0 = player.weapon0.weapon;
                string lastWeaponName1 = player.weapon1.weapon;
                string useWeaponName = player.useWeapon.weapon;

                string weaponName = GetOCRWeapon(name + ".png");

                if (!String.IsNullOrEmpty(weaponName))
                {
                    // 配件信息
                    Dictionary<string, string> other = QueryAccessories(globalParam.weapon0Location, image);

                    player.weapon0.weapon = weaponName;
                    if (null != other)
                    {
                        player.weapon0.qiangkou = other["qiangkou"];
                        player.weapon0.woBa = other["woBa"];
                        player.weapon0.magazine = other["magazine"];
                        player.weapon0.qiangTuo = other["qiangTuo"];
                        player.weapon0.scope = other["scope"];
                        GetWeaponData(player.weapon0);
                        if (player.useWeapon.weapon.Equals("空"))
                        {
                            player.useWeapon = player.weapon0;
                        }
                    }
                } else
                {
                    player.weapon0.ClearAttr();
                }

                string weaponName1 = GetOCRWeapon(name1 + ".png");

                if (!String.IsNullOrEmpty(weaponName1))
                {
                    // 配件信息
                    Dictionary<string, string> other = QueryAccessories(globalParam.weapon1Location, image1);

                    player.weapon1.weapon = weaponName1;
                    if (null != other)
                    {
                        player.weapon1.qiangkou = other["qiangkou"];
                        player.weapon1.woBa = other["woBa"];
                        player.weapon1.magazine = other["magazine"];
                        player.weapon1.qiangTuo = other["qiangTuo"];
                        player.weapon1.scope = other["scope"];
                        GetWeaponData(player.weapon1);
                        if (player.useWeapon.weapon.Equals("空"))
                        {
                            player.useWeapon = player.weapon1;
                        }
                    }
                } else
                {
                    player.weapon1.ClearAttr();
                }

                if (!String.IsNullOrEmpty(weaponName) || !String.IsNullOrEmpty(weaponName1))
                {
                    // 交换武器位置识别
                    if (useWeaponName.Equals(weaponName) && lastWeaponName0.Equals(weaponName1) && lastWeaponName1.Equals(weaponName))
                    {
                        player.useWeapon = player.weapon0;
                    } else if (useWeaponName.Equals(weaponName1) && lastWeaponName0.Equals(weaponName1) && lastWeaponName1.Equals(weaponName))
                    {
                        player.useWeapon = player.weapon1;
                    }
                    
                    if (String.IsNullOrEmpty(weaponName) && !String.IsNullOrEmpty(weaponName1))
                    {
                        player.useWeapon = player.weapon1;
                    } else if (!String.IsNullOrEmpty(weaponName) && String.IsNullOrEmpty(weaponName1))
                    {
                        player.useWeapon = player.weapon0;
                    }

                    if (jichuViewModel.Speaker && !_speakerStatus)
                    {
                        _speakerStatus = true;
                        Task.Run(() =>
                        {
                            SpeechSynthesizer synthes = new SpeechSynthesizer();
                            synthes.Speak("识别成功");//异步
                            _speakerStatus = false;
                        });
                    }
                }
                playerViewModel.Refresh();
                gunsViewModel.Refresh();
            }
            catch (Exception ex)
            {
                LogHelper.ShowLog("识别枪械报错 错误信息: {0}", ex.Message);
            }

            LogHelper.ShowLog("枪械识别耗时: {0}", (DateTime.Now - startTime).ToString());
        }

        int interval = 20;

        private bool ValiOpenBag()
        {
            IntPtr hdc = SelectColor.GetHDC();
            string colorHex = SelectColor.GetColorHexByDC(hdc, globalParam.ValiOpenBagPoint);
            string colorGRB = "#AEACAA";

            Color colorToSearch = ColorTranslator.FromHtml(colorHex);
            Color color = ColorTranslator.FromHtml(colorGRB);
            if (Math.Abs(colorToSearch.R - color.R) < interval &&

                    Math.Abs(colorToSearch.G - color.G) < interval &&

                    Math.Abs(colorToSearch.B - color.B) < interval)

            {
                player.openBag = true;
                LogHelper.ShowLog("openBag");
                return true;
            }
            else
            {
                player.openBag = false;
            }
            LogHelper.ShowLog("shoot color: {0}", colorHex);
            return false;
        }

        public static string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }

        private string GetOCRWeapon(string imageName)
        {
            string OCRWeaponName = Regex.Replace(GetOCR(imageName).ToUpper().Replace("L", "1"), "[ \\[ \\] \\^ \\-_*×――(^)（^）$%~!@#$…&%￥—+=<>《》!！??？:：•`·、。，'；,.;\"‘’“”-]", "");
            LogHelper.ShowLog("识别武器名称：{0}", OCRWeaponName);
            string weaponName = "";
            weaponName = GetWeaponName(OCRWeaponName, weaponName);
            return weaponName;
        }

        // 剪切图片并且反转颜色
        private void CutImage(Bitmap source, string imageName)
        {
            int width = globalParam.IdenWeaponImg.Width;//获取图片宽度
            int height = globalParam.IdenWeaponImg.Height;//获取图片高度
            Bitmap newmap = new Bitmap(width, height);//保存新图片

            int x = globalParam.IdenWeaponPoint.X;
            int y = globalParam.IdenWeaponPoint.Y;
            int lengthX = x + width;
            int lengthY = y + height;

            int originX = 0;
            int originY = 0;

            Color pixel;//颜色匹对

            for (int i=x;i<lengthX;i++)
            {
                for (int j=y;j<lengthY;j++)
                {
                    pixel = source.GetPixel(i, j);//获取旧图片的颜色值（ARGB存储方式）
                    int r, g, b, a;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;

                    //白色RGB(255，255，255),黑色（0,0,0）

                    //判断是否属于白色背景
                    if (r == 255 && g == 255 && b == 255)
                    {
                        //设置新图片中指定像素的颜色为黑色
                        newmap.SetPixel(originX, originY, Color.FromArgb(0, 0, 0));
                    }
                    else if (r == 0 && g == 0 && b == 0)
                    {   //设置新图片中指定像素的颜色为白色
                        newmap.SetPixel(originX, originY, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        newmap.SetPixel(originX, originY, Color.FromArgb(255 - r, 255 - g, 255 - b));
                    }
                    originY++;
                }
                originY = 0;
                originX++;
            }
            newmap.Save(imageName);
        }

        // 文字识别
        private string GetOCR(string imageName)
        {
            string s = Marshal.PtrToStringAnsi(OCR.GetOCR(imageName, -1));
            LogHelper.ShowLog("图片识别结果 {0}", s);
            return Regex.Replace(s, @"\s", "");
        }

        private string GetWeaponName(string OCRWeaponName, string weaponName)
        {
            if (!string.IsNullOrEmpty(OCRWeaponName) && OCRWeaponName.Length > 2)
            {
                string SubOCRWeaponName = OCRWeaponName.Substring(0, 2);
                FieldInfo[] fis = typeof(WeaponList).GetFields();
                foreach (var a in fis)
                {
                    if (a.GetRawConstantValue().ToString().StartsWith(SubOCRWeaponName))
                    {
                        weaponName = a.GetRawConstantValue().ToString();
                    }
                }

                if ("BER".StartsWith(SubOCRWeaponName))
                {
                    weaponName = WeaponList.M762;
                }
                if ("MICROU".StartsWith(OCRWeaponName) || "M1CR0U".StartsWith(OCRWeaponName) || "MICRI".Equals(OCRWeaponName))
                {
                    weaponName = WeaponList.UZI;
                }
                if ("S1I1".StartsWith(OCRWeaponName))
                {
                    weaponName = WeaponList.SLR;
                }
                if ("M1N114".StartsWith(OCRWeaponName))
                {
                    weaponName = WeaponList.MINI14;
                }
                if ("KAR98K".StartsWith(OCRWeaponName))
                {
                    weaponName = WeaponList.K98;
                }
                if (WeaponList.M24.Equals(OCRWeaponName))
                {
                    weaponName = WeaponList.M24;
                }
                if (WeaponList.M249.Equals(OCRWeaponName))
                {
                    weaponName = WeaponList.M249;
                }
                if (WeaponList.QBZ.Equals(OCRWeaponName))
                {
                    weaponName = WeaponList.QBZ;
                }
                if (WeaponList.QBU.Equals(OCRWeaponName))
                {
                    weaponName = WeaponList.QBU;
                }
                if (OCRWeaponName.Equals("MZ4"))
                {
                    weaponName = WeaponList.M24;
                }
                if (OCRWeaponName.Equals("Z4R"))
                {
                    weaponName = WeaponList.M249;
                }
                if (OCRWeaponName.Equals("SIZK"))
                {
                    weaponName = WeaponList.S12K;
                }
                if (OCRWeaponName.Equals("PPIR"))
                {
                    weaponName = WeaponList.PP19;
                }
                if (WeaponList.S1897.StartsWith(OCRWeaponName))
                {
                    weaponName = WeaponList.S1897;
                }
                if (OCRWeaponName.Equals("Mkl4") || OCRWeaponName.Equals("MK14"))
                {
                    weaponName = WeaponList.MK14;
                }
                LogHelper.ShowLog("武器名称：{0}", weaponName);
                return weaponName;
            }
            return weaponName;
        }

        // 颜色转换
        private static Bitmap ChangeImagePixel(Bitmap source)
        {
            int width = source.Width;//获取图片宽度
            int height = source.Height;//获取图片高度
            Bitmap newmap = new Bitmap(width, height);//保存新图片
            Color pixel;//颜色匹对
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pixel = source.GetPixel(i, j);//获取旧图片的颜色值（ARGB存储方式）
                    int r, g, b, a;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    a = pixel.A;

                    //白色RGB(255，255，255),黑色（0,0,0）

                    //判断是否属于白色背景
                    if (r == 255 && g == 255 && b == 255)
                    {
                        //设置新图片中指定像素的颜色为黑色
                        newmap.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                    else if (r == 0 && g == 0 && b == 0)
                    {   //设置新图片中指定像素的颜色为白色
                        newmap.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        newmap.SetPixel(i, j, Color.FromArgb(255 - r, 255 - g, 255 - b));
                    }
                }
            }
            return newmap;
        }

        // 刷新全局武器属性
        public void GetWeaponData(Weapon weapon)
        {
            if (null != weapon.weapon && !weapon.weapon.Equals("空"))
            {
                SQLiteHelper sql = new SQLiteHelper();
                string sqlStr = "select * from " + weapon.weapon + " where descs = '";
                string tem = String.Empty;
                tem += weapon.qiangkou == null ? "枪口空" : weapon.qiangkou;
                tem += "--" + (weapon.woBa == null ? "握把空" : weapon.woBa);
                tem += "--" + (weapon.qiangTuo == null ? "枪托空" : weapon.qiangTuo);
                tem += "--" + player.position;
                tem += "--" + (weapon.scope == null ? "1倍镜" : weapon.scope) + "'";
                LogHelper.ShowLog("Sql exec：{0}", sqlStr + tem);
                SqlDataModel sqlDataModel = sql.Query<SqlDataModel>(sqlStr + tem)[0];
                weapon.modelData = sqlDataModel;
            }
        }

        private string GetImage(Bitmap image, Point basic, Point getPoint)
        {
            int x = getPoint.X - basic.X;
            int y = getPoint.Y - basic.Y;
            Color color = image.GetPixel(x, y);
            return SelectColor.ToHexColor(color);
        }

        private Dictionary<string, string> QueryAccessories(Dictionary<string, Point> dic, Bitmap image)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            Point basePoint = dic["basic"];
            Point baseSize = dic["basicSize"];
            Point points0 = dic["qiangkou"];
            Point points1 = dic["woba"];
            Point points2 = dic["danxia"];
            Point points3 = dic["qiangtuo"];
            Point points4 = dic["scope"];

            //设置截屏区域
            
            string colorHex4 = GetImage(image, basePoint, points4);
            string colorHex3 = GetImage(image, basePoint, points3);
            string colorHex2 = GetImage(image, basePoint, points2);
            string colorHex1 = GetImage(image, basePoint, points1);
            string colorHex0 = GetImage(image, basePoint, points0);

            LogHelper.ShowLog("Get Color: {0}, {1}, {2}, {3} {4}", colorHex0, colorHex1, colorHex2, colorHex3, colorHex4);

            Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>>();

            if (globalParam.ScreenHeight == 1080)
            {
                dict = colorDict.Pixel_1080;
            }
            else if (globalParam.ScreenHeight == 1440)
            {
                dict = colorDict.Pixel_1440;
            }

            Dictionary<string, string> qiangkouDict = dict["qiangkou"];
            qiangkouDict.TryGetValue(colorHex0, out string qiangkouValue);
            result.Add("qiangkou", qiangkouValue != null ? qiangkouValue : "枪口空");

            Dictionary<string, string> woBauDict = dict["woBa"];
            woBauDict.TryGetValue(colorHex1, out string woBaValue);
            result.Add("woBa", woBaValue != null ? woBaValue : "握把空");

            Dictionary<string, string> magazineDict = dict["magazine"];
            magazineDict.TryGetValue(colorHex2, out string magazineValue);
            result.Add("magazine", magazineValue != null ? magazineValue : "无");

            Dictionary<string, string> qiangTuoDict = dict["qiangTuo"];
            qiangTuoDict.TryGetValue(colorHex3, out string qiangTuoValue);
            result.Add("qiangTuo", qiangTuoValue != null ? qiangTuoValue : "枪托空");

            Dictionary<string, string> scopeDict = dict["scope"];
            scopeDict.TryGetValue(colorHex4, out string scopeValue);
            result.Add("scope", scopeValue != null ? scopeValue : "1倍镜");

            return result;
        }

    }
}
