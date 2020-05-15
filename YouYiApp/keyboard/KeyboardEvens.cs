using System;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouYiApp.common;
using YouYiApp.logic;
using YouYiApp.model;
using YouYiApp.model.view;
using YouYiApp.mouse;

namespace YouYiApp.keyboard
{
    /**
     * 键盘监控事件
     * 
     * */
    public class KeyboardEvens
    {

        // 鼠标状态
        MouseStatus mouseStatus = MouseStatus.GetMouseStatus();

        GlobalParam globalParam = GlobalParam.GetGlobalParam();

        Player player = Player.GetPlay();

        PlayerViewModel playerViewModel = PlayerViewModel.GetPlayerViewModel();

        GunsViewModel gunsViewModel = GunsViewModel.GetGunsViewModel();

        AnjianViewModel anjianViewModel = AnjianViewModel.GetAnjianViewModel();

        JichuViewModel jichuViewModel = JichuViewModel.GetJichuViewModel();

        private HideTaskmgrList _List = new HideTaskmgrList();

        private static object singleton_Lock = new object(); //锁同步

        ImageSelectWeapon imageSelectWeapon = ImageSelectWeapon.GetImageSelectWeapon();

        private bool _speakStatus = false;

        public void K_hook_KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            //tb1.Text += e.KeyChar;
            int i = (int)e.KeyChar;
            LogHelper.ShowLog("KeyPress: {0}", e.KeyChar);
        }

        public void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            LogHelper.ShowLog("keyDown: {0}", e.KeyCode);

            // 游戏运行执行逻辑 globalParam.runGame
            if (true)
            {

                if (e.KeyCode.ToString().Equals(anjianViewModel.KeyDiscern))
                {
                    // ~ 按键
                    // 执行识别枪械
                }
                else if (e.KeyValue == 9)
                {
                    // ValiOpenBag();
                    imageSelectWeapon.ValiLogic();
                }
                else if (e.KeyCode.ToString().Equals(anjianViewModel.KeyLogic))
                {
                    player.runLogic = !player.runLogic;
                    string runLogicSpeak = "开启";
                    if (!player.runLogic)
                    {
                        runLogicSpeak = "关闭";
                    }
                    SpeechSynthesizer synthes = new SpeechSynthesizer();
                    synthes.Speak(runLogicSpeak);//异步
                    LogHelper.ShowLog("开启/关闭压枪");
                }
                else if (e.KeyCode.ToString().Equals(anjianViewModel.KeySpeaker))
                {
                    if (!_speakStatus)
                    {
                        _speakStatus = true;
                        // 读取装备信息 TTS
                        Task.Run(() =>
                        {
                            string textToSpeak = "武器一：" + playerViewModel.weapon0 + "," + playerViewModel.scope0 + "," + playerViewModel.qiangkou0 + "," + playerViewModel.woBa0 + "," + playerViewModel.magazine0 + "," + playerViewModel.qiangTuo0;
                            SpeechSynthesizer synthes = new SpeechSynthesizer();
                            synthes.Speak(textToSpeak);//异步
                            _speakStatus = false;
                        });
                    }
                }
                else if (e.KeyCode.ToString().Equals(anjianViewModel.KeyShouQiang))
                {
                    // X 按键
                    // 查询是否需要关闭压枪
                    player.XDown = !player.XDown;
                }
                else if (e.KeyValue == 49)
                {
                    // 1 按键
                    if (player.useMissiles)
                    {
                        player.useMissiles = false;
                    }
                    if (!player.weapon0.weapon.Equals("空"))
                    {
                        player.useWeapon = player.weapon0;
                        
                    }
                    gunsViewModel.Refresh();
                    playerViewModel.Refresh();
                }
                else if (e.KeyValue == 50)
                {
                    // 2 按键
                    if (player.useMissiles)
                    {
                        player.useMissiles = false;
                    }
                    if (!player.weapon1.weapon.Equals("空"))
                    {
                        player.useWeapon = player.weapon1;
                    }
                    gunsViewModel.Refresh();
                    playerViewModel.Refresh();
                }
                else if (e.KeyValue == 30)
                {

                }
                else if (e.KeyCode.ToString().Equals(anjianViewModel.KeyDun))
                {
                    // C 按键
                    if (player.position.Equals("站") || player.position.Equals("趴"))
                    {
                        gunsViewModel.Position = "蹲";
                    }
                    else
                    {
                        gunsViewModel.Position = "站";

                    }
                    playerViewModel.RaisePropertyChanged("position");
                    // 刷新武器
                    imageSelectWeapon.GetWeaponData(player.useWeapon);
                    gunsViewModel.RaisePropertyChanged("Datas");
                    LogHelper.ShowLog("姿势：{0}", player.position);
                }
                else if (e.KeyCode.ToString().Equals(anjianViewModel.KeyPa))
                {
                    if (player.position.Equals("站") || player.position.Equals("蹲"))
                    {
                        gunsViewModel.Position = "趴";
                    }
                    else
                    {
                        gunsViewModel.Position = "站";
                    }
                    playerViewModel.RaisePropertyChanged("position");
                    // 刷新武器
                    imageSelectWeapon.GetWeaponData(player.useWeapon);
                    gunsViewModel.RaisePropertyChanged("Datas");
                    LogHelper.ShowLog("姿势：{0}", player.position);
                } else if (e.KeyCode.ToString().Equals(anjianViewModel.KeyTiao))
                {
                    if (!player.position.Equals("站"))
                    {
                        gunsViewModel.Position = "站";
                        playerViewModel.RaisePropertyChanged("position");
                        // 刷新武器
                        imageSelectWeapon.GetWeaponData(player.useWeapon);
                        gunsViewModel.RaisePropertyChanged("Datas");
                        LogHelper.ShowLog("姿势：{0}", player.position);
                    }
                } else if (e.KeyValue == 27)
                {
                    // ESC 按键
                    player.ESCDown = !player.ESCDown;
                } else if (e.KeyCode.ToString().Equals(anjianViewModel.KeyDiuLei1) || e.KeyCode.ToString().Equals(anjianViewModel.KeyDiuLei2) 
                    || e.KeyCode.ToString().Equals(anjianViewModel.KeyDiuLei3))
                {
                    player.useMissiles = true;
                } else if (e.KeyValue == 82)
                {
                    // R 按键
                    if (player.useMissiles && mouseStatus.leftDown && jichuViewModel.NieLei)
                    {
                        LogHelper.ShowLog("丢雷倒计时");
                        SoundPlayer player = new SoundPlayer(Properties.Resources.NieLei);
                        player.Play();
                    }
                } else if (e.KeyValue == 116)
                {
                }
            }

            if (e.KeyCode.ToString().Equals(anjianViewModel.KeyHSMainWindow))
            {
                // 隐藏/显示主窗口
                if (globalParam.MainWindow.IsActive)
                {
                    globalParam.MainWindow.Hide();
                    _List.ProcessName = Process.GetCurrentProcess().ProcessName + ".exe";
                    _List.Star();
                }
                else
                {
                    globalParam.MainWindow.Show();
                    _List.Stop();
                }
            } else if (e.KeyCode.ToString().Equals(anjianViewModel.KeyHSPlayerInfo))
            {
                lock (singleton_Lock)
                {
                    if (null == globalParam.PlayerWin)
                    {
                        globalParam.PlayerWin = new PlayerInfoWindow();
                    }
                    if (!globalParam.topWindow)
                    {
                        globalParam.PlayerWin.Left = 0;
                        globalParam.PlayerWin.Top = globalParam.ScreenHeight / 2;
                        globalParam.PlayerWin.Show();
                        TopMostTool.setTopCustomBar("PlayerInfoWindow");
                        globalParam.PlayerWin.Topmost = true;
                    }
                    else
                    {
                        globalParam.PlayerWin.Topmost = false;
                        TopMostTool.setTopCustomBar("PlayerInfoWindow");
                        globalParam.PlayerWin.Hide();
                    }
                    globalParam.topWindow = !globalParam.topWindow;
                }
            }
        }

        public void Hook_KeyUp(object sender, KeyEventArgs e)
        {

            LogHelper.ShowLog("KeyUp: {0}", e.KeyValue);
            // Tab 按键
            if (e.KeyValue == 9)
            {
                ValiOpenBag();
            }
        }

        int interval = 80;

        private void ValiOpenBag()
        {
            Thread.Sleep(50);
            IntPtr hdc = SelectColor.GetHDC();
            string colorHex = "";
            string colorGRB = "#A4A3A3";
            colorHex = SelectColor.GetColorHexByDC(hdc, globalParam.ValiOpenBagPoint);

            Color colorToSearch = ColorTranslator.FromHtml(colorHex);
            Color color = ColorTranslator.FromHtml(colorGRB);
            if (Math.Abs(colorToSearch.R - color.R) < interval &&
                    Math.Abs(colorToSearch.G - color.G) < interval &&
                    Math.Abs(colorToSearch.B - color.B) < interval)
            {
                player.openBag = true;
                LogHelper.ShowLog("openBag");
            }
            else
            {
                player.openBag = false;
            }
            LogHelper.ShowLog("ValiOpenBag color: {0}", colorHex);
        }
    }
}