using System;
using System.Collections.Generic;
using YouYiApp.common;

namespace YouYiApp.model.view
{
    public class GunsViewModel : NotificationObject
    {
        public static GunsViewModel _gunsViewModel = null;

        Player _player;

        List<WeaponDataModel> _datas;

        private BaseCommand valueChange;

        SQLiteHelper sql = new SQLiteHelper();

        private static object singleton_Lock = new object(); //锁同步

        public static GunsViewModel GetGunsViewModel()
        {
            lock (singleton_Lock)
            {
                if (null == _gunsViewModel)
                {
                    _gunsViewModel = new GunsViewModel();
                }
            }
            return _gunsViewModel;
        }

        public GunsViewModel()
        {
            _player = Player.GetPlay();
            _datas = new List<WeaponDataModel>();
        }

       public string WeaponSort
        {
            get
            {
                if (_player.useWeapon == _player.weapon0)
                {
                    return "一号";
                } else if (_player.useWeapon == _player.weapon1)
                {
                    return "二号";
                } else 
                {
                    return "一号";
                }
            }
        }

        public string Position
        {
            get { return _player.position; }
            set { _player.position = value; this.RaisePropertyChanged("Position"); }
        }

        public string WeaponName
        {
            get { return _player.useWeapon.weapon; }
        }

        public string Scope
        {
            get { return _player.useWeapon.scope; }
        }

        public string Qiangkou
        {
            get { return _player.useWeapon.qiangkou; }
        }

        public string WoBa
        {
            get { return _player.useWeapon.woBa; }
        }

        public string QiangTuo
        {
            get { return _player.useWeapon.qiangTuo; }
        }

        public List<WeaponDataModel> Datas
        {
            get
            {
                _datas = new List<WeaponDataModel>(8);
                if (null != _player.useWeapon.modelData)
                {
                    string[] strs = _player.useWeapon.modelData.datas.Split('|');
                    for (int i=0;i<strs.Length;i++)
                    {
                        double d = Convert.ToDouble(strs[i]);
                        _datas.Add(new WeaponDataModel(d, i));
                    }
                } else
                {
                    _datas = new List<WeaponDataModel>(8);
                    for (int i = 0; i < 8; i++)
                    {
                        _datas.Add(new WeaponDataModel(0, i));
                    }
                }
                return _datas;
            }
            set
            {
                _datas = value;
                if (null != _player.useWeapon.modelData)
                {
                    string str = ArrayTransformString(value);
                    _player.useWeapon.modelData.datas = str;
                }
                this.RaisePropertyChanged("Datas");
            }
        }

        public BaseCommand ValueChange
        {
            get
            {
                if (valueChange == null)
                {
                    valueChange = new BaseCommand(new Action<object>(o =>
                    {
                        LogHelper.ShowLog("执行值修改事件 datas：{0}", Convert.ToString(_datas));
                        if (null != _player.useWeapon.modelData)
                        {
                            string sqlData = ArrayTransformString(_datas);
                            int i = sql.Execute("update '"+ _player.useWeapon.weapon + "' set datas = '" + sqlData + "' where ID = " + _player.useWeapon.modelData.ID);
                            _player.useWeapon.modelData.datas = sqlData;
                            LogHelper.ShowLog("保存数据库 {0}", i);
                        }
                    }));
                }
                return valueChange;
            }
        }

        // 刷新页面
        public void Refresh()
        {
            this.RaisePropertyChanged("WeaponSort");
            this.RaisePropertyChanged("Position");
            this.RaisePropertyChanged("WeaponName");
            this.RaisePropertyChanged("Scope");
            this.RaisePropertyChanged("Qiangkou");
            this.RaisePropertyChanged("WoBa");
            this.RaisePropertyChanged("QiangTuo");
            this.RaisePropertyChanged("Interval");
            this.RaisePropertyChanged("Datas");
        }

        public void RefreshIndex()
        {
            this.RaisePropertyChanged("Datas");
        }

        private String ArrayTransformString(List<WeaponDataModel> SafetyMeasure)
        {
            string sb = string.Empty;
            for (int i = 0; i < SafetyMeasure.Count; i++)
            {
                if (i < SafetyMeasure.Count - 1)
                {
                    sb += SafetyMeasure[i].Num + "|";
                }
                else
                {
                    sb += SafetyMeasure[i].Num;
                }
            }
            return sb;
        }
    }
}