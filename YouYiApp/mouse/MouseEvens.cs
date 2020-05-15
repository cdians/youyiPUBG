using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouYiApp.common;
using YouYiApp.logic;
using YouYiApp.model;
using YouYiApp.model.view;

namespace YouYiApp.mouse
{
    /**
     * 鼠标监听事件
     * 
    * */
    public class MouseEvens
    {

        IntPtr m_Handle;

        // 鼠标状态
        MouseStatus mouseStatus;

        //全局变量
        GlobalParam globalParam;

        JichuViewModel jichuViewModel;

        Player player;

        // 鼠标开始坐标
        Point startPoint = new Point(0,0);

        // 鼠标结束坐标
        Point endPoint = new Point(0,0);

        public MouseEvens(IntPtr m_Handle)
        {
            this.m_Handle = m_Handle;
            this.mouseStatus = MouseStatus.GetMouseStatus();
            this.globalParam = GlobalParam.GetGlobalParam();
            this.jichuViewModel = JichuViewModel.GetJichuViewModel();
            this.player = Player.GetPlay();
        }

        public void mh_MouseDownEvent(object sender, MouseEventArgs e)
        {
            LogHelper.ShowLog("鼠标按键 {0}", e.Button.ToString());
            if (e.Button == MouseButtons.Left)
            {
                LogHelper.ShowLog("按下了左键");
                mouseStatus.leftDown = true;

                // 判断是否需要压枪
                if (player.runLogic)
                {
                    // 判断是否开镜
                    // 常规开镜
                    if (jichuViewModel.KaiJingMeth)
                    {
                        // 判断射击模式
                        // TODO 同步验证是否需要延迟验证
                        Task.Run(() =>
                        {
                            Thread.Sleep(10);
                            FireLogic.ValiShoot(globalParam, player);
                            FireLogic.OpenFire();
                        });
                    }
                    else
                    {
                        // 快速开镜 异步延迟检查是否为开镜射击射击
                        player.isShoot = true;
                        Task.Run(() =>
                        {
                            Thread.Sleep(100);
                            FireLogic.ValiShoot(globalParam, player);
                        });
                        Task.Run(() =>
                        {
                            FireLogic.OpenFire();
                        });
                    }
                }

                // TODO 执行相关逻辑
            }
            else if (e.Button == MouseButtons.Right)
            {
                LogHelper.ShowLog("按下了右键");
                mouseStatus.rightDown = true;
            } else if (e.Button == MouseButtons.XButton1)
            {
                player.autoShoot = true;
                LogHelper.ShowLog("自动射击");
                Task.Run(() =>
                {
                    FireLogic.AutoShoot();
                });
                
            }
            startPoint = e.Location;
        }

        public void mh_MouseUpEvent(object sender, MouseEventArgs e)
        {
            endPoint = e.Location;
            // 鼠标移动距离
            double value = Math.Sqrt(Math.Abs(startPoint.X - endPoint.X) * Math.Abs(startPoint.X - endPoint.X) + 
                Math.Abs(startPoint.Y - endPoint.Y) * Math.Abs(startPoint.Y - endPoint.Y));

            if (e.Button == MouseButtons.Left)
            {
                LogHelper.ShowLog("松开了左键");
                mouseStatus.leftDown = false;

                if (player.isShoot)
                {
                    player.isShoot = false;
                }

                // TODO 执行相关逻辑
            } else if (e.Button == MouseButtons.Right)
            {
                LogHelper.ShowLog("松开了右键");
                mouseStatus.rightDown = false;

                // 判断是否运行丢雷提示
                if (player.useMissiles)
                {
                    player.useMissiles = false;
                }

                // TODO 执行相关逻辑

                // 执行血雾逻辑
                if (player.runXueWuLogic)
                {
                    player.runXueWuLogic = false;
                }
            } else if (e.Button == MouseButtons.XButton1)
            {
                if (player.autoShoot)
                {
                    player.autoShoot = false;
                    LogHelper.ShowLog("自动射击 取消");
                }
            }
            LogHelper.ShowLog("移动了 {0} 距离", value);
            startPoint = new Point(0, 0);
            endPoint = new Point(0, 0);
        }

    }
}
