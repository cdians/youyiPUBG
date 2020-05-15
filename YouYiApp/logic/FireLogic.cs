using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using YouYiApp.common;
using YouYiApp.config;
using YouYiApp.model;
using YouYiApp.model.view;
using YouYiApp.mouse;

namespace YouYiApp.logic
{
    public class FireLogic
    {

        // 分段移动到指定值 
        public static void OpenFire()
        {
            Player player = Player.GetPlay();

            GunsViewModel gunsViewModel = GunsViewModel.GetGunsViewModel();

            JichuViewModel jichuViewModel = JichuViewModel.GetJichuViewModel();

            bool _HasAmmo = true;

            GlobalParam globalParam = GlobalParam.GetGlobalParam();

            MouseStatus mouseStatus = MouseStatus.GetMouseStatus();

            AppInfo appInfo = AppInfo.GetAppInfo();

            if (player.runLogic && player.isShoot && !player.useMissiles 
                && !string.IsNullOrEmpty(player.useWeapon.weapon) && !player.useWeapon.weapon.Equals("空"))
            {
                string[] str = player.useWeapon.modelData.datas.Split('|');
                double[] datas = Array.ConvertAll(str, double.Parse);
                int x = 0, y = 0;
                int count = (int)datas[6];
                int magNum = 40;
                int logicTime = (int) (datas[7] / datas[6] - 1);

                Task.Run(() =>
                {
                    ValiAmmo(ref _HasAmmo);
                });

                if (!player.runXueWuLogic && jichuViewModel.XueWu)
                {
                    player.runXueWuLogic = true;
                    Task.Run(() =>
                    {
                        ExecLogic(player);
                    });
                }

                int index = 0;

                while (mouseStatus.leftDown && _HasAmmo && player.isShoot)
                {
                    DateTime startTime = DateTime.Now;
                    // 判断是否没有子弹

                    double j = 0;
                    if (magNum == 40)
                    {
                        j = datas[0];
                        index = 0;
                    }
                    else if (magNum < 40 && magNum >= 34)
                    {
                        j = datas[1];
                        index = 1;
                    }
                    else if (magNum < 34 && magNum >= 27)
                    {
                        j = datas[2];
                        index = 2;
                    }
                    else if (magNum < 27 && magNum >= 19)
                    {
                        j = datas[3];
                        index = 3;
                    }
                    else if (magNum < 19 && magNum >= 9)
                    {
                        j = datas[4];
                        index = 4;
                    }
                    else
                    {
                        j = datas[5];
                        index = 5;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        if (_HasAmmo && player.isShoot)
                        {
                            YouyiSdk.M_MoveR2(globalParam.m_Handle, 0, (int)j);
                            
                            Task.Run(() =>
                            {
                                if (index != globalParam.DataIndex)
                                {
                                    globalParam.DataIndex = index;
                                    gunsViewModel.RaisePropertyChanged("Datas");
                                }
                                LogHelper.ShowLog("压枪耗时 {0} 压枪幅度：{1} 步进：{2}", 0, j, i);
                            });
                            Thread.Sleep(logicTime);
                        }
                    }
                    magNum -= 1;
                    Task.Run(() =>
                    {
                        ValiAmmo(ref _HasAmmo);
                        Task.Run(() =>
                        {
                            ValiShoot(globalParam, player);
                        });
                    });
                }   
            }
        }

        public static void AutoShoot()
        {
            GlobalParam globalParam = GlobalParam.GetGlobalParam();
            Player player = Player.GetPlay();
            while (player.autoShoot)
            {
                YouyiSdk.M_LeftClick(globalParam.m_Handle, 1);


                Thread.Sleep(10);
                YouyiSdk.M_MoveR2(globalParam.m_Handle, 0, 15);
            }
        }

        private static void ValiAmmo(ref bool HasAmmo)
        {
            GlobalParam globalParam = GlobalParam.GetGlobalParam();
            IntPtr hdc = SelectColor.GetHDC();
            string colorHex = SelectColor.GetColorHexByDC(hdc, globalParam.ValiAmmoPoint);
            if (colorHex.Equals("#FF0000") || colorHex.Equals("#916E3D"))
            {
                HasAmmo = false;
            }
            LogHelper.ShowLog("ammo color: {0}", colorHex);
        }

        public static void ExecLogic(Player player)
        {
            while (player.runXueWuLogic)
            {
                Thread.Sleep(100);
                XueWu();
            }
        }

        private static void XueWu()
        {
            AppInfo appInfo = AppInfo.GetAppInfo();
            GlobalParam globalParam = GlobalParam.GetGlobalParam();

            Bitmap image = new Bitmap(140, 60);
            Point cutPoint = new Point(((int)appInfo.screenWidth / 2 - (image.Width / 2)), ((int)appInfo.screenHeight / 2) - (image.Height / 2));
            Graphics imgGraphics = Graphics.FromImage(image);
            imgGraphics.CopyFromScreen(cutPoint, new Point(0,0) ,new Size(image.Width, image.Height)); //第一个参数是截图开始坐标，第二个参数是要拷贝到的Bitmap的开始位置，保持不变，最后是图片大小
            if (BaseConfig.DEBUG)
            {
                image.Save("cut.png");
            }

            int interval = 8;
            int x = 0;
            int y = 0;
            Color pixel;//颜色匹对
            Color color = ColorTranslator.FromHtml("#B50D6B");
            for (int i =0;i<image.Width;i++)
            {
                for (int j=0;j<image.Height;j++)
                {
                    pixel = image.GetPixel(i, j);

                    if (Math.Abs(pixel.R - color.R) < interval &&

                    Math.Abs(pixel.G - color.G) < interval &&

                    Math.Abs(pixel.B - color.B) < interval)

                    {
                        x = (cutPoint.X + i) - ((int)appInfo.screenWidth / 2);
                        //y = ((int)appInfo.screenHeight / 2) - (cutPoint.Y + j);
                        LogHelper.ShowLog("get Xuewu X: {0}  Y: {1}", x, y);
                        YouyiSdk.M_MoveR2(globalParam.m_Handle, x, 0);
                        return;
                    }

                }
            }
        }

        public static void ValiShoot(GlobalParam globalParam, Player player)
        {
            // TODO 验证射击颜色偏移数值
            int interval = 55;
            IntPtr hdc = SelectColor.GetHDC();
            string colorHex = "";
            string colorGRB = "#F5F5F3";
            switch (player.position)
            {
                case "蹲":
                    colorGRB = "#E3DDDA";
                    colorHex = SelectColor.GetColorHexByDC(hdc, globalParam.ValiDunShootPoint);
                    break;
                case "趴":
                    colorHex = SelectColor.GetColorHexByDC(hdc, globalParam.ValiPaShootPoint);
                    break;
                default:
                    colorHex = SelectColor.GetColorHexByDC(hdc, globalParam.ValiShootPoint);
                    break;
            }

            Color colorToSearch = ColorTranslator.FromHtml(colorHex);
            Color color = ColorTranslator.FromHtml(colorGRB);

            if (Math.Abs(colorToSearch.R - color.R) < interval &&

                    Math.Abs(colorToSearch.G - color.G) < interval &&

                    Math.Abs(colorToSearch.B - color.B) < interval)

            {
                player.isShoot = true;
                LogHelper.ShowLog("get Shoot");
            }
            else
            {
                player.isShoot = false;
            }
            if (colorHex.Equals("#EDE"))
            {
                player.isShoot = false;
            }
            LogHelper.ShowLog("shoot color: {0}", colorHex);
        }
    }
}
