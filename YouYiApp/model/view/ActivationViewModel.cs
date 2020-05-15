using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Text;
using System.Timers;
using System.Windows;
using YouYiApp.common;
using YouYiApp.config;

namespace YouYiApp.model.view
{
    public class ActivationViewModel : NotificationObject
    {

        GlobalParam _globalParam;

        ActivationWindow _window;

        public static ActivationViewModel _activationViewModel;

        private static object singleton_Lock = new object(); //锁同步

        public static ActivationViewModel GetActivationViewModel(ActivationWindow window)
        {
            lock (singleton_Lock)
            {
                if (null == _activationViewModel)
                {
                    _activationViewModel = new ActivationViewModel(window);
                }
            }
            return _activationViewModel;
        }

        public ActivationViewModel(ActivationWindow window)
        {
            this.Loading = false;
            _globalParam = GlobalParam.GetGlobalParam();
            _window = window;
        }

        private string _ActivationCode;

        private bool _Loading;

        private BaseCommand button_Click;

        private Timer aTimer = null;

        public string ActivationCode
        {
            get { return _ActivationCode; }
            set { _ActivationCode = value; this.RaisePropertyChanged("ActivationCode"); }
        }

        public bool Loading
        {
            get { return _Loading; }
            set { _Loading = value; this.RaisePropertyChanged("Loading"); }
        }

        public BaseCommand Button_Click
        {
            get
            {
                if (button_Click == null)
                {
                    button_Click = new BaseCommand(new System.Action<object>(o => 
                    {
                        string activationCode = _ActivationCode;
                        if (!string.IsNullOrEmpty(activationCode) && !string.IsNullOrWhiteSpace(activationCode))
                        {
                            Loading = true;
                            aTimer = new Timer();
                            aTimer.Elapsed += new ElapsedEventHandler(ValiCode);
                            aTimer.Interval = 1000 * 5;
                            aTimer.Enabled = true;
                            aTimer.AutoReset = false;
                            
                            //SetSn(_globalParam.m_Handle, activationCode);
                        }
                    }));
                }
                return button_Click;
            }
        }

        // D5puPvS6GzfOsdaW6Kjwle63AUeLFVVc
        private void SetSn(IntPtr m_Handle, string codeStr)
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
                string s = new UTF8Encoding().GetString(Hex.Encode(md));
                LogHelper.ShowLog("摘要加密：{0} 长度：{1}", s.ToUpper(), s.Length);

                YouyiSdk.M_SetUserData(m_Handle, md.Length, ref md[0]);
            }
        }

        private void ValiCode(object sender, EventArgs e)
        {
            string codeStr = _ActivationCode;
            int resLen = 256;
            byte[] res = new byte[resLen];
            int v = YouyiSdk.M_GetDevSn(_globalParam.m_Handle, ref resLen, ref res[0]);
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
                //string s = new UTF8Encoding().GetString(Hex.Encode(md));
                //LogHelper.ShowLog("摘要加密：{0} 长度：{1}", s.ToUpper(), s.Length);

                // 验证自定义数据
                int vali = YouyiSdk.M_VerifyUserData(_globalParam.m_Handle, md.Length, ref md[0]);
                LogHelper.ShowLog("验证结果：{0}", vali);
                Loading = false;
                if (vali != 0)
                {
                    // 失败信息
                    MessageBox.Show("卡密不正确，请联系客服处理!");
                    _window.DelegeteShutDown();
                }
                else
                {
                    // 写入激活码
                    INIHelper.Write("Info", "ActivationCode", codeStr, BaseConfig.CONFIG_PATH);
                    _window.DelegeteClose();
                }
            }
        }

    }
}
