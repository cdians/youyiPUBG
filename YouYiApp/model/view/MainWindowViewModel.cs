using System;
using YouYiApp.common;
using YouYiApp.control;

namespace YouYiApp.model.view
{
    public class MainWindowViewModel : NotificationObject
    {

        private object _currentView;

        private object _dandaoView;

        private object _jichuView;

        private object _anjianView;

        private BaseCommand changeUserControlCom;

        public static MainWindowViewModel _mainWindowViewModel;

        private static object singleton_Lock = new object(); //锁同步

        public static MainWindowViewModel GetMainWindowViewModel()
        {
            lock (singleton_Lock)
            {
                if (null == _mainWindowViewModel)
                {
                    _mainWindowViewModel = new MainWindowViewModel();
                }
            }
            return _mainWindowViewModel;
        }

        public MainWindowViewModel()
        {
            this._dandaoView = new DandaoUserControl();
            this._jichuView = new JichuUserControl();
            this._anjianView = new AnjianUserControl();
            this._currentView = this._jichuView;
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                this.RaisePropertyChanged("CurrentView");
            }
        }

        public string FenBianLv
        {
            get { return AppInfo.GetAppInfo().screenWidth + "×" + AppInfo.GetAppInfo().screenHeight; }
        }

        public BaseCommand ChangeUserControlCom
        {
            get
            {
                if (changeUserControlCom == null)
                {
                    changeUserControlCom = new BaseCommand(new Action<object>(o => 
                    {
                        string param = (string)o;
                        switch (param)
                        {
                            case "anjian":
                                CurrentView = _anjianView;
                                break;
                            case "dandao":
                                CurrentView = _dandaoView;
                                break;
                            case "zuixiaohua":
                                GlobalParam.GetGlobalParam().MainWindow.Hide();
                                break;
                            default:
                                CurrentView = _jichuView;
                                break;
                        }
                        LogHelper.ShowLog("视图切换 param: {0}", o);
                    }));
                }
                return changeUserControlCom;
            }
        }
    }
}
