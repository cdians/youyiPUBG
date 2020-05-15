using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;

namespace YouYiApp.model
{

    // 全局参数
    public class GlobalParam
    {
        public static GlobalParam globalParam = null;

        private static object singleton_Lock = new object(); //锁同步

        public GlobalParam()
        {
        }

        public bool runGame { get; set; }

        // 识别武器坐标点
        public System.Drawing.Point IdenWeaponPoint { get; set; }

        // 验证是否有子弹
        public System.Drawing.Point ValiAmmoPoint { get; set; }

        // 判断射击模式
        public System.Drawing.Point ValiShootPoint { get; set; }

        public System.Drawing.Point ValiDunShootPoint { get; set; }

        public System.Drawing.Point ValiPaShootPoint { get; set; }

        // 验证是否打开背包
        public System.Drawing.Point ValiOpenBagPoint { get; set; }

        // 识别武器图片尺寸
        public Bitmap IdenWeaponImg { get; set; }

        // 识别武器保存图片大小
        public System.Drawing.Size IdenWeaponSize { get; set; }

        // 识别武器图片名称
        public string IdenWeaponImgName { get; set; }

        // 屏幕高度
        public int ScreenHeight { get; set; }

        // 是否置顶窗口
        public bool topWindow { get; set; }

        // 芯片句柄
        public IntPtr m_Handle { get; set; }

        // 主窗口
        public Window MainWindow { get; set; }

        // 玩家信息窗口
        public PlayerInfoWindow PlayerWin { get; set; }

        public int DataIndex = 0;

        public Dictionary<string, System.Drawing.Point> weapon0Location = new Dictionary<string, System.Drawing.Point>();

        public Dictionary<string, System.Drawing.Point> weapon1Location = new Dictionary<string, System.Drawing.Point>();

        public static GlobalParam GetGlobalParam()
        {
            lock (singleton_Lock)
            {
                if (null == globalParam)
                {
                    globalParam = new GlobalParam();
                    // 初始化
                    globalParam.ScreenHeight = Convert.ToInt32(SystemParameters.PrimaryScreenHeight);
                    globalParam.IdenWeaponImgName = "a.png";
                    if (globalParam.ScreenHeight == 1080)
                    {
                        // 初始化武器识别位置
                        globalParam.IdenWeaponPoint = new System.Drawing.Point(58, 19);
                        globalParam.IdenWeaponImg = new Bitmap(47, 26);
                        globalParam.IdenWeaponSize = new System.Drawing.Size(47, 26);

                        // 识别是否有子弹
                        globalParam.ValiAmmoPoint = new System.Drawing.Point(954, 999);

                        // 识别射击姿势
                        globalParam.ValiShootPoint = new System.Drawing.Point(721, 1016);
                        globalParam.ValiDunShootPoint = new System.Drawing.Point(719, 1028);
                        globalParam.ValiPaShootPoint = new System.Drawing.Point(729, 1033);

                        // 识别是否打开背包
                        globalParam.ValiOpenBagPoint = new System.Drawing.Point(384, 64);

                        // 初始化配件识别位置
                        globalParam.weapon0Location.Add("basic", new System.Drawing.Point(1295, 78));
                        globalParam.weapon0Location.Add("basicSize", new System.Drawing.Point(491, 219));

                        globalParam.weapon0Location.Add("qiangkou", new System.Drawing.Point(1340, 269));
                        globalParam.weapon0Location.Add("woba", new System.Drawing.Point(1441, 272));
                        globalParam.weapon0Location.Add("danxia", new System.Drawing.Point(1552, 269));
                        globalParam.weapon0Location.Add("qiangtuo", new System.Drawing.Point(1764, 269));
                        globalParam.weapon0Location.Add("scope", new System.Drawing.Point(1611, 138));

                        globalParam.weapon1Location.Add("basic", new System.Drawing.Point(1295, 308));
                        globalParam.weapon1Location.Add("basicSize", new System.Drawing.Point(491, 219));

                        globalParam.weapon1Location.Add("qiangkou", new System.Drawing.Point(1340, 499));
                        globalParam.weapon1Location.Add("woba", new System.Drawing.Point(1441, 502));
                        globalParam.weapon1Location.Add("danxia", new System.Drawing.Point(1552, 499));
                        globalParam.weapon1Location.Add("qiangtuo", new System.Drawing.Point(1764, 499));
                        globalParam.weapon1Location.Add("scope", new System.Drawing.Point(1611, 368));
                    } else if (globalParam.ScreenHeight == 1440)
                    {
                        // 初始化武器识别位置
                        globalParam.IdenWeaponPoint = new System.Drawing.Point(78, 24);
                        globalParam.IdenWeaponImg = new Bitmap(101, 34);
                        globalParam.IdenWeaponSize = new System.Drawing.Size(101, 34);

                        // 识别是否有子弹
                        globalParam.ValiAmmoPoint = new System.Drawing.Point(1279, 1326);

                        // 识别射击姿势
                        globalParam.ValiShootPoint = new System.Drawing.Point(965, 1347);
                        globalParam.ValiDunShootPoint = new System.Drawing.Point(958, 1371);
                        globalParam.ValiPaShootPoint = new System.Drawing.Point(971, 1379);

                        // 识别是否打开背包
                        globalParam.ValiOpenBagPoint = new System.Drawing.Point(512, 86);

                        // 初始化配件识别位置
                        globalParam.weapon0Location.Add("basic", new System.Drawing.Point(1726, 105));
                        globalParam.weapon0Location.Add("basicSize", new System.Drawing.Point(655, 290));

                        globalParam.weapon0Location.Add("qiangkou", new System.Drawing.Point(1775, 370));
                        globalParam.weapon0Location.Add("woba", new System.Drawing.Point(1922, 356));
                        globalParam.weapon0Location.Add("danxia", new System.Drawing.Point(2067, 356));
                        globalParam.weapon0Location.Add("qiangtuo", new System.Drawing.Point(2338, 368));
                        globalParam.weapon0Location.Add("scope", new System.Drawing.Point(2146, 187));

                        globalParam.weapon1Location.Add("basic", new System.Drawing.Point(1726, 412));
                        globalParam.weapon1Location.Add("basicSize", new System.Drawing.Point(655, 290));

                        globalParam.weapon1Location.Add("qiangkou", new System.Drawing.Point(1775, 677));
                        globalParam.weapon1Location.Add("woba", new System.Drawing.Point(1922, 663));
                        globalParam.weapon1Location.Add("danxia", new System.Drawing.Point(2067, 663));
                        globalParam.weapon1Location.Add("qiangtuo", new System.Drawing.Point(2338, 675));
                        globalParam.weapon1Location.Add("scope", new System.Drawing.Point(2146, 494));
                    }
                }
            }
            return globalParam;
        }

    }
}