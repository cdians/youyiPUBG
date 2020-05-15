using System.Collections.Generic;
using YouYiApp.model;

namespace YouYiApp.config
{
    public class ColorDict
    {
        public static ColorDict colorDict = null;

        private static object singleton_Lock = new object(); //锁同步

        public ColorDict() { }

        public static ColorDict GetColorDict()
        {
            lock (singleton_Lock)
            {
                if (null == colorDict)
                {
                    colorDict = new ColorDict();
                }
            }
            return colorDict;
        }

        public Dictionary<string,Dictionary<string, string>> Pixel_1440
        {
            get
            {
                return new Dictionary<string, Dictionary<string, string>>
                {
                    { 
                        "qiangkou", new Dictionary<string, string>
                        {
                            {"#002A", WeaponList.FLAME_DETECTOR},
                            {"#4F555A", WeaponList.COMPENSATOR},
                            {"#5A595A", WeaponList.MUFFLER},
                            {"#313842", WeaponList.COMPENSATOR},
                            {"#797778", WeaponList.FLAME_DETECTOR},
                            {"#726F6A", WeaponList.MUFFLER}
                        }
                    },
                    {
                        "woBa", new Dictionary<string, string>
                        {
                            {"#292926", WeaponList.WOBA_MUZHI},
                            {"#605145", WeaponList.WOBA_SANJIA},
                            {"#BA8378", WeaponList.WOBA_BANJIE},
                            {"#191918", WeaponList.WOBA_QX},
                            {"#81418",  WeaponList.WOBA_CHUIZHI},
                            {"#413F40", WeaponList.WOBA_LEISHE}
                        }
                    },
                    {
                        "magazine", new Dictionary<string, string>
                        {
                            {"#364C55", WeaponList.MAGA_KS},
                            {"#3D3F3D", WeaponList.MAGA_KR},
                            {"#81218",  WeaponList.MAGA_KK},
                            {"#3E3C35", WeaponList.MAGA_KR},
                            {"#606067", WeaponList.MAGA_KR}
                        }
                    },
                    {
                        "qiangTuo", new Dictionary<string, string>
                        {
                            {"#676560",  WeaponList.QT_QT},
                            {"#484748",  WeaponList.QT_TSB}
                        }
                    },
                    {
                        "scope", new Dictionary<string, string>
                        {
                            {"#151E23",  WeaponList.SCOPE8X},
                            {"#142025",  WeaponList.SCOPE8X},
                            {"#353A45",  WeaponList.SCOPE6X},
                            {"#38414B",  WeaponList.SCOPE6X},
                            {"#545757",  WeaponList.SCOPE4X},
                            {"#525356",  WeaponList.SCOPE4X},
                            {"#8491A3",  WeaponList.SCOPE3X},
                            {"#738190",  WeaponList.SCOPE3X},
                            {"#5C5959",  WeaponList.SCOPE2X},
                            {"#282A2F",  WeaponList.SCOPE2X},
                            {"#656665",  WeaponList.SCOPE1X_HD},
                            {"#424242",  WeaponList.SCOPE1X_QX}
                        }
                    }
                };
            }
        }

        public Dictionary<string, Dictionary<string, string>> Pixel_1080
        {
            get
            {
                return new Dictionary<string, Dictionary<string, string>>
                {
                    {
                        "qiangkou", new Dictionary<string, string>
                        {
                            {"#21252E", WeaponList.FLAME_DETECTOR},
                            {"#4F555A", WeaponList.COMPENSATOR},
                            {"#4A494A", WeaponList.MUFFLER},
                            {"#5C5F64", WeaponList.COMPENSATOR},
                            {"#484C4C", WeaponList.FLAME_DETECTOR},
                            {"#5C5B59", WeaponList.MUFFLER}
                        }
                    },
                    {
                        "woBa", new Dictionary<string, string>
                        {
                            {"#465054", WeaponList.WOBA_MUZHI},
                            {"#5F5F5C", WeaponList.WOBA_SANJIA},
                            {"#C56F64", WeaponList.WOBA_BANJIE},
                            {"#2E2C22", WeaponList.WOBA_QX},
                            {"#16171B",  WeaponList.WOBA_CHUIZHI},
                            {"#414647", WeaponList.WOBA_LEISHE}
                        }
                    },
                    {
                        "magazine", new Dictionary<string, string>
                        {
                            {"#3B535A", WeaponList.MAGA_KS},
                            {"#404241", WeaponList.MAGA_KR},
                            {"#424542",  WeaponList.MAGA_KK}
                        }
                    },
                    {
                        "qiangTuo", new Dictionary<string, string>
                        {
                            {"#797771",  WeaponList.QT_QT},
                            {"#484748",  WeaponList.QT_TSB}
                        }
                    },
                    {
                        "scope", new Dictionary<string, string>
                        {
                            {"#212C34",  WeaponList.SCOPE8X},
                            {"#2C2D34",  WeaponList.SCOPE6X},
                            {"#99B",  WeaponList.SCOPE4X},
                            {"#B2BFCB",  WeaponList.SCOPE3X},
                            {"#9F9C97",  WeaponList.SCOPE2X},
                            {"#676867",  WeaponList.SCOPE1X_HD},
                            {"#424242",  WeaponList.SCOPE1X_QX}
                        }
                    }
                };
            }
        }
    }
}
