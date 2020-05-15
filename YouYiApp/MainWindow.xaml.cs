using IWshRuntimeLibrary;
using Org.BouncyCastle.Utilities.Encoders;
using Panuon.UI.Silver;
using System;
using System.Reflection;
using System.Text;
using System.Timers;
using System.Windows;
using YouYiApp.common;
using YouYiApp.config;
using YouYiApp.model;
using YouYiApp.model.view;

namespace YouYiApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowX
    {
        private delegate void UpdateUIDelegate();

        MainWindowViewModel _mainWindowViewModel;

        GlobalParam _globalParam;

        private Timer timer;

        private bool showMessageBox = false;

        private int _valiCode = 0;

        public MainWindow()
        {
            InitializeComponent();
            this._mainWindowViewModel = MainWindowViewModel.GetMainWindowViewModel();
            base.DataContext = _mainWindowViewModel;
            _globalParam = GlobalParam.GetGlobalParam();
            _globalParam.MainWindow = this;
            this.Closing += MainWindow_Closing;
            //QueryProcess();
             

            // 查询是否激活
            String activationCode = INIHelper.Read("Info", "ActivationCode", "", BaseConfig.CONFIG_PATH);
            if (string.IsNullOrEmpty(activationCode) || string.IsNullOrWhiteSpace(activationCode))
            {
                // 填写激活码
                ActivationWindow activation = new ActivationWindow();
                activation.ShowDialog();
            }
            // 进入首次验证
            TimerQueryActivation(null, null);
            if (_valiCode == 1)
            {
                YouyiSdk.M_Close(_globalParam.m_Handle);
                if (!showMessageBox)
                {
                    showMessageBox = true;
                    MessageBox.Show("卡密不正确，请重新激活!");
                }
                INIHelper.DeleteKey("Info", "ActivationCode", BaseConfig.CONFIG_PATH);
                DelegeteShutDownApp();
            }
            // 定时查询激活
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(TimerQueryActivation);
            timer.Interval = 1000 * 60 * 3;
            timer.Enabled = true;
            timer.AutoReset = true;
        }

        // codeStr:D5puPvS6GzfOsdaW6Kjwle63AUeLFVVc
        private void ValiCode(IntPtr m_Handle, string codeStr)
        {
            int resLen = 256;
            byte[] res = new byte[resLen];
            int v = YouyiSdk.M_GetDevSn(m_Handle, ref resLen, ref res[0]);
            if (v == 0)
            {
                byte[] sec = new UTF8Encoding().GetBytes(BaseConfig.AC_SECRET);
                byte[] newRes = new byte[resLen];
                Array.Copy(res, newRes, resLen);
                LogHelper.ShowLog("设备SN：{0}", new UTF8Encoding().GetString(Hex.Encode(newRes)));
                // 验证激活码与SN
                byte[] md = new byte[32];
                byte[] code = Encoding.Default.GetBytes(codeStr);
                byte[] bt = new byte[resLen + sec.Length + code.Length];
                newRes.CopyTo(bt, 0);
                sec.CopyTo(bt, newRes.Length);
                code.CopyTo(bt, bt.Length - code.Length);

                SM3Digest sm3 = new SM3Digest();
                sm3.BlockUpdate(bt, 0, bt.Length);
                sm3.DoFinal(md, 0);

                // 验证自定义数据
                int vali = YouyiSdk.M_VerifyUserData(m_Handle, md.Length, ref md[0]);
                LogHelper.ShowLog("验证结果：{0}", vali);
                if (vali != 0)
                {
                    _valiCode += 1;
                } else
                {
                    if (_valiCode != 0)
                    {
                        _valiCode = 0;
                    }
                }
            } else
            {
                _valiCode += 1;
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 关闭芯片
            YouyiSdk.M_Close(_globalParam.m_Handle);
            // 删除武器识别图片
            if (!BaseConfig.DEBUG)
            {
                if (System.IO.File.Exists(_globalParam.IdenWeaponImgName))
                {
                    System.IO.File.Delete(_globalParam.IdenWeaponImgName);
                }
            }

            CreateDeskTopLnk();

            Application.Current.Shutdown();
        }

        // 查询游戏进程
        private void QueryProcess()
        {
            // TslGame.exe
            if (System.Diagnostics.Process.GetProcessesByName("TslGame").Length > 1)
            {
                _globalParam.runGame = true;
                LogHelper.ShowLog("吃鸡已运行");
            } else
            {
                _globalParam.runGame = false;
                LogHelper.ShowLog("程序未运行");
            }

        }

        private void DelegeteShutDownApp()
        {
            UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(ShutDownApp);
            this.Dispatcher.Invoke(updateUIDelegate);
        }

        private void ShutDownApp()
        {
            Application.Current.Shutdown();
        }

        // 定时查询是否激活 防止破解
        private void TimerQueryActivation(object sender, EventArgs e)
        {
            String activationCode = INIHelper.Read("Info", "ActivationCode", "", BaseConfig.CONFIG_PATH);
            if (string.IsNullOrEmpty(activationCode) || string.IsNullOrWhiteSpace(activationCode))
            {
                Application.Current.Shutdown();
            }
            else
            {
                ValiCode(_globalParam.m_Handle, activationCode);
                // 验证失败直接关闭应用
                if (_valiCode >= 3)
                {
                    YouyiSdk.M_Close(_globalParam.m_Handle);
                    if (!showMessageBox)
                    {
                        showMessageBox = true;
                        MessageBox.Show("卡密不正确，请重新激活!");
                    }
                    INIHelper.DeleteKey("Info", "ActivationCode", BaseConfig.CONFIG_PATH);
                    DelegeteShutDownApp();
                }
            }
        }

        private void CreateDeskTopLnk()
        {

            string deskTop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string dirPath = Environment.CurrentDirectory;
            string exePath = Assembly.GetExecutingAssembly().Location;
            System.Diagnostics.FileVersionInfo exeInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(exePath);
            if (System.IO.File.Exists(string.Format(@"{0}\you壹平台.lnk", deskTop)))
            {
                return;
            }
            String create = INIHelper.Read("Info", "CreateLnk", "true", BaseConfig.CONFIG_PATH);
            if (Convert.ToBoolean(create))
            {
                var result = MessageBoxX.Show("是否创建桌面快捷方式？", "提示", Application.Current.MainWindow, MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    WshShell shell = new WshShell();
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + "you壹平台.lnk");
                    shortcut.TargetPath = @exePath;         //目标文件
                    shortcut.WorkingDirectory = dirPath;    //目标文件夹
                    shortcut.WindowStyle = 1;               //目标应用程序的窗口状态分为普通、最大化、最小化【1,3,7】
                    shortcut.Description = "you壹游戏平台";   //描述
                    shortcut.IconLocation = string.Format(@"{0}\YY.ico", dirPath);  //快捷方式图标
                    shortcut.Arguments = "";
                    shortcut.Hotkey = "SHIFT+Y";              // 快捷键
                    shortcut.Save();
                    INIHelper.Write("Info", "CreateLnk", "true", BaseConfig.CONFIG_PATH);
                }
                else
                {
                    INIHelper.Write("Info", "CreateLnk", "false", BaseConfig.CONFIG_PATH);
                }
            }
        }
    }
}
