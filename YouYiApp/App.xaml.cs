using Panuon.UI.Silver;
using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using YouYiApp.common;
using YouYiApp.config;
using YouYiApp.keyboard;
using YouYiApp.model;
using YouYiApp.mouse;

namespace YouYiApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        Process process;

        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_Exit);
        }

        MouseHook mh;

        KeyboardHook k_hook;

        GlobalParam globalParam = GlobalParam.GetGlobalParam();

        void App_Startup(object sender, StartupEventArgs e)
        {
            //UI线程未捕获异常处理事件
            this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // 设置进程优先级
            this.process = Process.GetCurrentProcess();
            process.PriorityClass = ProcessPriorityClass.RealTime;

            RegisterHookEven();
            InitApp();
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true; //把 Handled 属性设为true，表示此异常已处理，程序可以继续运行，不会强制退出      
                LogHelper.ShowLog("捕获未处理异常:" + e.Exception.Message);
            }
            catch (Exception ex)
            {
                //此时程序出现严重异常，将强制结束退出
                LogHelper.ShowLog("程序发生致命错误，将终止，请联系客服处理！");
            }

        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StringBuilder sbEx = new StringBuilder();
            if (e.IsTerminating)
            {
                sbEx.Append("程序发生致命错误，将终止，请联系客服处理！\n");
            }
            sbEx.Append("捕获未处理异常：");
            if (e.ExceptionObject is Exception)
            {
                sbEx.Append(((Exception)e.ExceptionObject).Message);
            }
            else
            {
                sbEx.Append(e.ExceptionObject);
            }
            LogHelper.ShowLog(sbEx.ToString());
        }

        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //task线程内未处理捕获
            LogHelper.ShowLog("捕获线程内未处理异常：" + e.Exception.Message);
            e.SetObserved();//设置该异常已察觉（这样处理后就不会引起程序崩溃）
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/");
            //程序退出时需要处理的业务
            if (System.IO.Directory.Exists(path) == true)//如果不存在就创建file文件夹
            {
                System.IO.Directory.Delete(path, true);
            }
        }

        /**
         * 注册各类监听事件
         *
        **/
        private void RegisterHookEven()
        {
            LogHelper.ShowLog("开始注册事件");
            try
            {
                int mvid = Convert.ToInt32(BaseConfig.YOUXI_VID, 16);
                int mpid = Convert.ToInt32(BaseConfig.YOUXI_PID, 16);
                IntPtr m_Handle = YouyiSdk.M_Open_VidPid(mvid, mpid);
                //IntPtr m_Handle = YouyiSdk.aeBGGOyo4(3);
                int check = YouyiSdk.M_ResolutionUsed(m_Handle, (int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);
                if ((int)m_Handle == -1)
                {
                    LogHelper.ShowLog("打开设备失败，请检查USB设备是否已经插入");
                    var result = MessageBoxX.Show("请检查USB设备是否已经插入", "提示", Application.Current.MainWindow, MessageBoxButton.OK, new MessageBoxXConfigurations()
                    {
                        MessageBoxIcon = MessageBoxIcon.Error,
                        ButtonBrush = "#FF4C4C".ToColor().ToBrush(),
                    });
                    this.Shutdown();
                }
                else
                {
                    globalParam.m_Handle = m_Handle;
                    LogHelper.ShowLog("打开设备成功 m_Handle: {0} check: {1}", m_Handle, check);
                    
                }

                // 注册鼠标事件
                mh = new MouseHook();
                mh.SetHook();
                MouseEvens evens = new MouseEvens(m_Handle);
                mh.MouseDownEvent += evens.mh_MouseDownEvent;
                mh.MouseUpEvent += evens.mh_MouseUpEvent;

                //注册键盘事件
                k_hook = new KeyboardHook();
                KeyboardEvens keyEvens = new KeyboardEvens();
                k_hook.KeyDownEvent += new System.Windows.Forms.KeyEventHandler(keyEvens.Hook_KeyDown);//按键按下 
                k_hook.KeyUpEvent += new System.Windows.Forms.KeyEventHandler(keyEvens.Hook_KeyUp);//按键松开 
                k_hook.KeyPressEvent += keyEvens.K_hook_KeyPressEvent;
                k_hook.Start();
            }
            catch (DllNotFoundException e)
            {
                LogHelper.ShowLog("调用动态链接库异常 {0}", e.Message);
                MessageBox.Show("软件资源损坏，请重新下载安装 \n" + e.Message);
                this.Shutdown();
            }
        }

        /**
         * 读取配置
         *
        * */
        private void InitApp()
        {
            String infoPath = BaseConfig.CONFIG_PATH;
            INIHelper.CheckPath(infoPath);
            // 是否初始化
            /**
            String init = INIHelper.Read("Info", "Init", "false", infoPath);
            LogHelper.ShowLog("App 是否初始化：{0}", init);
            if (!Convert.ToBoolean(init))
            {
                // 初始化数据库
                Task.Run(() =>
                {
                    string[] guns = new string[] { "98K", "AKM", "AUG", "AWM", "DP28", "G36C", "GROZA", "M16A4", "M249", "M416", "M762",
                    "MINI14", "MK14", "MK47", "MP5K", "PP19", "QBU", "QBZ", "S12K", "S1897", "S686", "SCAR", "SKS", "UMP45", "UZI", "VSS", "WIN94"};
                    foreach (var a in guns)
                    {
                        InputData(a);
                    }
                });
                init = "true";
                INIHelper.Write("Info", "Init", init, infoPath);
            }
            **/
            //InputData("M0SI", "30.mdb");
            //InputCustom();
            AppInfo appInfo = AppInfo.GetAppInfo();
            appInfo.isInit = Convert.ToBoolean(true);
        }

        private void InputData(string gun, String db)
        {
            var msg = "";
            string mdbFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, db); 

            MDBHelp mdbHelp = new MDBHelp(mdbFile);
            SQLiteHelper sqlHelper = new SQLiteHelper();
            //sqlHelper.Execute("DROP TABLE 'SLR'");
            sqlHelper.Execute("CREATE TABLE \""+gun+"\" ( \"ID\" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, \"dataNo\" TEXT ( 255 ), \"datas\" TEXT ( 255 ), \"descs\" TEXT ( 255 ) );");
            try
            {
                mdbHelp.Open();
                var d = new Dictionary<string, string>();
                var ds1 = mdbHelp.GetDataSet("select *  from M24狙;");
                var ColumnsList = ds1.Tables[0].Columns;
                foreach (DataRow item in ds1.Tables[0].Rows)

                {

                    msg = "<ROW ";

                    d = new Dictionary<string, string>();

                    foreach (DataColumn item1 in ColumnsList)

                    {

                        var ColumnName = item1.ColumnName;

                        d.Add(ColumnName, item[ColumnName].ToString());

                    }

                    int _id = Convert.ToInt32(d["ID"]);
                    string dataNo = d["弹道编号"];
                    string datas = d["弹道数据"];
                    string descs = d["弹道备注"];
                    msg += string.Format("ID=\"{0}\" 弹道编号=\"{1}\" 弹道数据=\"{2}\"  弹道备注=\"{3}\" ", _id, dataNo, datas, descs);

                    msg += "></ROW>";

                    // 写入sqlite
                    
                    string sql = string.Format("INSERT INTO \"" + gun + "\" (\"ID\", \"dataNo\", \"datas\", \"descs\") VALUES ({0}, '{1}', '{2}', '{3}');", _id, dataNo, datas, descs);
                    int i = sqlHelper.Execute(sql);
                    //LogHelper.ShowLog("row info: {0}", msg);
                    //LogHelper.ShowLog("inset status: {0}", i);
                }
            } catch (Exception e)
            {
                LogHelper.ShowLog("open MDB {0}", e.Message);
            } finally
            {
                /**
                mdbHelp.Close();
                mdbHelp = null;
                sqlHelper.Close();
                sqlHelper = null;
                **/
            }
            LogHelper.ShowLog("{0} 数据导入完成", gun);
        }

        private void InputCustom()
        {
            SQLiteHelper sqlHelper = new SQLiteHelper();
            string gun = "M0SI";
            int _id = 841;
            string dataNo = gun + "-36126";
            string datas = "0|0|0|0|0|0|4|90";
            string descs = "枪口空--握把空--枪托空--站--8倍镜";;
            string sql = string.Format("INSERT INTO \"" + gun + "\" (\"ID\", \"dataNo\", \"datas\", \"descs\") VALUES ({0}, '{1}', '{2}', '{3}');", _id, dataNo, datas, descs);
            int i = sqlHelper.Execute(sql);

            _id = 842;
            dataNo = gun + "-36127";
            descs = "枪口空--握把空--枪托空--蹲--8倍镜"; ;
            sql = string.Format("INSERT INTO \"" + gun + "\" (\"ID\", \"dataNo\", \"datas\", \"descs\") VALUES ({0}, '{1}', '{2}', '{3}');", _id, dataNo, datas, descs);
            i = sqlHelper.Execute(sql);

            _id = 843;
            dataNo = gun + "-36128";
            descs = "枪口空--握把空--枪托空--趴--8倍镜"; ;
            sql = string.Format("INSERT INTO \"" + gun + "\" (\"ID\", \"dataNo\", \"datas\", \"descs\") VALUES ({0}, '{1}', '{2}', '{3}');", _id, dataNo, datas, descs);
            i = sqlHelper.Execute(sql);
        }
    }
}
