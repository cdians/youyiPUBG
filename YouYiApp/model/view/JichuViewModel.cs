using System;
using YouYiApp.common;
using YouYiApp.config;

namespace YouYiApp.model.view
{
    public class JichuViewModel : NotificationObject
    {
        public static JichuViewModel _jichuViewModel;

        private static object singleton_Lock = new object(); //锁同步

        public static JichuViewModel GetJichuViewModel()
        {
            lock (singleton_Lock)
            {
                if (null == _jichuViewModel)
                {
                    _jichuViewModel = new JichuViewModel();
                }
            }
            return _jichuViewModel;
        }

        public JichuViewModel()
        {
            _AutoDiscern = Convert.ToBoolean(INIHelper.Read("Basic", "AutoDiscern", "True", BaseConfig.CONFIG_PATH));
            _XueWu = Convert.ToBoolean(INIHelper.Read("Basic", "XueWu", "True", BaseConfig.CONFIG_PATH));
            _Speaker = Convert.ToBoolean(INIHelper.Read("Basic", "Speaker", "True", BaseConfig.CONFIG_PATH));
            _KaiJingMeth = Convert.ToBoolean(INIHelper.Read("Basic", "KaiJingMeth", "True", BaseConfig.CONFIG_PATH));
            _LinMin = Convert.ToBoolean(INIHelper.Read("Basic", "LinMin", "True", BaseConfig.CONFIG_PATH));
            _NieLei = Convert.ToBoolean(INIHelper.Read("Basic", "NieLei", "True", BaseConfig.CONFIG_PATH));
        }

        bool _AutoDiscern;

        bool _XueWu;

        bool _Speaker;

        bool _KaiJingMeth;

        bool _LinMin;

        bool _NieLei;

        public bool AutoDiscern
        {
            get { return _AutoDiscern; }
            set
            {
                _AutoDiscern = value;
                INIHelper.Write("Basic", "AutoDiscern", Convert.ToString(value), BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("AutoDiscern");
            }
        }

        public bool XueWu
        {
            get { return _XueWu; }
            set
            {
                _XueWu = value;
                INIHelper.Write("Basic", "XueWu", Convert.ToString(value), BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("XueWu");
            }
        }

        public bool Speaker
        {
            get { return _Speaker; }
            set
            {
                _Speaker = value;
                INIHelper.Write("Basic", "Speaker", Convert.ToString(value), BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("Speaker");
            }
        }

        public bool KaiJingMeth
        {
            get { return _KaiJingMeth; }
            set
            {
                _KaiJingMeth = value;
                INIHelper.Write("Basic", "KaiJingMeth", Convert.ToString(value), BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("KaiJingMeth");
            }
        }

        public bool LinMin
        {
            get { return _LinMin; }
            set
            {
                _LinMin = value;
                LogHelper.ShowLog("LingMin {0}", value);
                INIHelper.Write("Basic", "LinMin", Convert.ToString(value), BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("LinMin");
            }
        }

        public bool NieLei
        {
            get { return _NieLei; }
            set
            {
                _NieLei = value;
                INIHelper.Write("Basic", "NieLei", Convert.ToString(value), BaseConfig.CONFIG_PATH);
                this.RaisePropertyChanged("NieLei");
            }
        }
    }
}
