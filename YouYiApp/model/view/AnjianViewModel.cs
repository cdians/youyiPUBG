using System.Windows.Controls;
using System.Windows.Input;
using YouYiApp.common;
using YouYiApp.config;

namespace YouYiApp.model.view
{
    public class AnjianViewModel: NotificationObject
    {

        public static AnjianViewModel _anjianViewModel;

        private static object singleton_Lock = new object(); //锁同步

        public static AnjianViewModel GetAnjianViewModel()
        {
            lock (singleton_Lock)
            {
                if (null == _anjianViewModel)
                {
                    _anjianViewModel = new AnjianViewModel();
                }
            }
            return _anjianViewModel;
        }

        public AnjianViewModel()
        {
            _Key_Dun = INIHelper.Read("HotKey", "KeyDun", "C", BaseConfig.CONFIG_PATH);
            _Key_Pa = INIHelper.Read("HotKey", "KeyPa", "Z", BaseConfig.CONFIG_PATH);
            _Key_Tiao = INIHelper.Read("HotKey", "KeyTiao", "Space", BaseConfig.CONFIG_PATH);
            _Key_DiuLei1 = INIHelper.Read("HotKey", "KeyDiuLei1", "G", BaseConfig.CONFIG_PATH);
            _Key_DiuLei2 = INIHelper.Read("HotKey", "KeyDiuLei3", "D3", BaseConfig.CONFIG_PATH);
            _Key_DiuLei3 = INIHelper.Read("HotKey", "KeyDiuLei3", "D5", BaseConfig.CONFIG_PATH);
            _Key_ShouQiang = INIHelper.Read("HotKey", "KeyShouQiang", "X", BaseConfig.CONFIG_PATH);
            _Key_HSMainWindow = INIHelper.Read("HotKey", "KeyHSMainWindow", "X", BaseConfig.CONFIG_PATH);
            _Key_HSPlayerInfo = INIHelper.Read("HotKey", "KeyHSPlayerInfo", "X", BaseConfig.CONFIG_PATH);
            _Key_Speaker = INIHelper.Read("HotKey", "KeySpeaker", "X", BaseConfig.CONFIG_PATH);
            _Key_Logic = INIHelper.Read("HotKey", "KeyLogic", "X", BaseConfig.CONFIG_PATH);
            _Key_Discern = INIHelper.Read("HotKey", "KeyDiscern", "X", BaseConfig.CONFIG_PATH);
            _Key_AutoShoot = INIHelper.Read("HotKey", "KeyAutoShoot", "N", BaseConfig.CONFIG_PATH);
        }

        string _Key_Dun;

        string _Key_Pa;

        string _Key_Tiao;

        string _Key_DiuLei1;

        string _Key_DiuLei2;

        string _Key_DiuLei3;

        string _Key_ShouQiang;

        string _Key_HSMainWindow;

        string _Key_HSPlayerInfo;

        string _Key_Speaker;

        string _Key_Logic;

        string _Key_Discern;

        string _Key_AutoShoot;

        public string KeyDun
        {
            get { return _Key_Dun; }
            set
            {
                _Key_Dun = value;
                INIHelper.Write("HotKey", "KeyDun", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeyDun");
            }
        }

        public string KeyPa
        {
            get { return _Key_Pa; }
            set
            {
                _Key_Pa = value;
                INIHelper.Write("HotKey", "KeyPa", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeyPa");
            }
        }

        public string KeyTiao
        {
            get { return _Key_Tiao; }
            set
            {
                _Key_Tiao = value;
                INIHelper.Write("HotKey", "KeyTiao", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeyTiao");
            }
        }

        public string KeyDiuLei1
        {
            get { return _Key_DiuLei1; }
            set
            {
                _Key_DiuLei1 = value;
                INIHelper.Write("HotKey", "KeyDiuLei1", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeyDiuLei1");
            }
        }

        public string KeyDiuLei2
        {
            get { return _Key_DiuLei2; }
            set
            {
                _Key_DiuLei2 = value;
                INIHelper.Write("HotKey", "KeyDiuLei2", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeyDiuLei2");
            }
        }

        public string KeyDiuLei3
        {
            get { return _Key_DiuLei3; }
            set
            {
                _Key_DiuLei3 = value;
                INIHelper.Write("HotKey", "KeyDiuLei3", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeyDiuLei3");
            }
        }

        public string KeyShouQiang
        {
            get { return _Key_ShouQiang; }
            set
            {
                _Key_ShouQiang = value;
                INIHelper.Write("HotKey", "KeyShouQiang", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeyShouQiang");
            }
        }

        public string KeyHSMainWindow
        {
            get { return _Key_HSMainWindow; }
            set
            {
                _Key_HSMainWindow = value;
                INIHelper.Write("HotKey", "KeyHSMainWindow", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeyHSMainWindow");
            }
        }

        public string KeyHSPlayerInfo
        {
            get { return _Key_HSPlayerInfo; }
            set
            {
                _Key_HSPlayerInfo = value;
                INIHelper.Write("HotKey", "KeyHSPlayerInfo", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeyHSPlayerInfo");
            }
        }

        public string KeySpeaker
        {
            get { return _Key_Speaker; }
            set
            {
                _Key_Speaker = value;
                INIHelper.Write("HotKey", "KeySpeaker", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeySpeaker");
            }
        }

        public string KeyLogic
        {
            get { return _Key_Logic; }
            set
            {
                _Key_Logic = value;
                INIHelper.Write("HotKey", "KeyLogic", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeyLogic");
            }
        }

        public string KeyDiscern
        {
            get { return _Key_Discern; }
            set
            {
                _Key_Discern = value;
                INIHelper.Write("HotKey", "KeyDiscern", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeyDiscern");
            }
        }

        public string KeyAutoShoot
        {
            get { return _Key_AutoShoot; }
            set
            {
                _Key_AutoShoot = value;
                INIHelper.Write("HotKey", "KeyAutoShoot", value, BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KeyAutoShoot");
            }
        }


        public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            LogHelper.ShowLog("key: {0} ToolTip: {1}", e.ChangedButton.ToString(), tb.ToolTip);
            switch (tb.ToolTip)
            {
                case "蹲":
                    KeyDun = e.ChangedButton.ToString();
                    break;
                case "趴":
                    KeyPa = e.ChangedButton.ToString();
                    break;
                case "跳":
                    KeyTiao = e.ChangedButton.ToString();
                    break;
                case "丢雷1":
                    KeyDiuLei1 = e.ChangedButton.ToString();
                    break;
                case "丢雷2":
                    KeyDiuLei2 = e.ChangedButton.ToString();
                    break;
                case "丢雷3":
                    KeyDiuLei3 = e.ChangedButton.ToString();
                    break;
                case "收枪":
                    KeyShouQiang = e.ChangedButton.ToString();
                    break;
                case "隐藏界面":
                    KeyHSMainWindow = e.ChangedButton.ToString();
                    break;
                case "武器界面":
                    KeyHSPlayerInfo = e.ChangedButton.ToString();
                    break;
                case "语音提示":
                    KeySpeaker = e.ChangedButton.ToString();
                    break;
                case "压枪开关":
                    KeyLogic = e.ChangedButton.ToString();
                    break;
                case "手动识别":
                    KeyDiscern = e.ChangedButton.ToString();
                    break;
                case "自动连发":
                    KeyAutoShoot = e.ChangedButton.ToString();
                    break;
                default:
                    break;
            }
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            LogHelper.ShowLog("key: {0} ToolTip: {1}", e.Key, tb.ToolTip);
            switch (tb.ToolTip)
            {
                case "蹲":
                    KeyDun = e.Key.ToString();
                    break;
                case "趴":
                    KeyPa = e.Key.ToString();
                    break;
                case "跳":
                    KeyTiao = e.Key.ToString();
                    break;
                case "丢雷1":
                    KeyDiuLei1 = e.Key.ToString();
                    break;
                case "丢雷2":
                    KeyDiuLei2 = e.Key.ToString();
                    break;
                case "丢雷3":
                    KeyDiuLei3 = e.Key.ToString();
                    break;
                case "收枪":
                    KeyShouQiang = e.Key.ToString();
                    break;
                case "隐藏界面":
                    KeyHSMainWindow = e.Key.ToString();
                    break;
                case "武器界面":
                    KeyHSPlayerInfo = e.Key.ToString();
                    break;
                case "语音提示":
                    KeySpeaker = e.Key.ToString();
                    break;
                case "压枪开关":
                    KeyLogic = e.Key.ToString();
                    break;
                case "手动识别":
                    KeyDiscern = e.Key.ToString();
                    break;
                default:
                    break;
            }
        }

    }
}
