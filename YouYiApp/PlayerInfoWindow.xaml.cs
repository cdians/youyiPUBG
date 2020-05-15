using System;
using System.Windows;
using System.Windows.Interop;
using YouYiApp.common;
using YouYiApp.model.view;

namespace YouYiApp
{
    /// <summary>
    /// PlayerInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerInfoWindow : Window
    {

        PlayerViewModel _playerViewModel;

        public PlayerInfoWindow()
        {
            InitializeComponent();
            _playerViewModel = PlayerViewModel.GetPlayerViewModel();
            this.DataContext = _playerViewModel;
        }

        // 鼠标点击穿透
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }

    }
}
