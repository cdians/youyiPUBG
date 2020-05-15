using YouYiApp.common;

namespace YouYiApp.model.view
{
    public class PlayerViewModel : NotificationObject
    {
        Player _player;

        public static PlayerViewModel _playerViewModel = null;

        private static object singleton_Lock = new object(); //锁同步

        public static PlayerViewModel GetPlayerViewModel()
        {
            lock (singleton_Lock)
            {
                if (null == _playerViewModel)
                {
                    _playerViewModel = new PlayerViewModel();
                }
            }
            return _playerViewModel;
        }

        public PlayerViewModel()
        {
            _player = Player.GetPlay();
        }

        public string position
        {
            get { return _player.position; }
            set { _player.position = value; this.RaisePropertyChanged("position"); }
        }

        public string useWeapon
        {
            get { return _player.useWeapon.weapon; }
            set { _player.useWeapon.weapon = value; this.RaisePropertyChanged("useWeapon"); }
        }

        public string weapon0
        {
            get { return _player.weapon0.weapon; }
            set { _player.weapon0.weapon = value; this.RaisePropertyChanged("weapon0"); }
        }

        // 瞄具
        public string scope0 
        {
            get { return _player.weapon0.scope; }
            set { _player.weapon0.scope = value; this.RaisePropertyChanged("scope0"); } 
        }

        // 枪口
        public string qiangkou0 
        { 
            get { return _player.weapon0.qiangkou; } 
            set { _player.weapon0.qiangkou = value; this.RaisePropertyChanged("qiangkou0"); } 
        }

        // 握把
        public string woBa0 
        { 
            get { return _player.weapon0.woBa; }
            set { _player.weapon0.woBa = value; this.RaisePropertyChanged("woBa0"); }
        }

        // 弹匣
        public string magazine0 
        { 
            get { return _player.weapon0.magazine; }
            set { _player.weapon0.magazine = value; this.RaisePropertyChanged("magazine0"); }
        }

        // 枪托
        public string qiangTuo0 
        { 
            get { return _player.weapon0.qiangTuo; }
            set { _player.weapon0.qiangTuo = value; this.RaisePropertyChanged("qiangTuo0"); }
        }

        public string weapon1
        {
            get { return _player.weapon1.weapon; }
            set { _player.weapon1.weapon = value; this.RaisePropertyChanged("weapon1"); }
        }

        // 瞄具
        public string scope1
        {
            get { return _player.weapon1.scope; }
            set { _player.weapon1.scope = value; this.RaisePropertyChanged("scope1"); }
        }

        // 枪口
        public string qiangkou1
        {
            get { return _player.weapon1.qiangkou; }
            set { _player.weapon1.qiangkou = value; this.RaisePropertyChanged("qiangkou1"); }
        }

        // 握把
        public string woBa1
        {
            get { return _player.weapon1.woBa; }
            set { _player.weapon1.woBa = value; this.RaisePropertyChanged("woBa1"); }
        }

        // 弹匣
        public string magazine1
        {
            get { return _player.weapon1.magazine; }
            set { _player.weapon1.magazine = value; this.RaisePropertyChanged("magazine1"); }
        }

        // 枪托
        public string qiangTuo1
        {
            get { return _player.weapon1.qiangTuo; }
            set { _player.weapon1.qiangTuo = value; this.RaisePropertyChanged("qiangTuo1"); }
        }

        public void Refresh()
        {
            this.RaisePropertyChanged("useWeapon");
            this.RaisePropertyChanged("weapon0");
            this.RaisePropertyChanged("scope0");
            this.RaisePropertyChanged("qiangkou0");
            this.RaisePropertyChanged("woBa0");
            this.RaisePropertyChanged("magazine0");
            this.RaisePropertyChanged("qiangTuo0");
            this.RaisePropertyChanged("weapon1");
            this.RaisePropertyChanged("scope1");
            this.RaisePropertyChanged("qiangkou1");
            this.RaisePropertyChanged("woBa1");
            this.RaisePropertyChanged("magazine1");
            this.RaisePropertyChanged("qiangTuo1");
        }
    }
}
