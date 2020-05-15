using System.Windows.Controls;
using YouYiApp.model.view;

namespace YouYiApp.control
{
    /// <summary>
    /// DandaoUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class DandaoUserControl : UserControl
    {
        GunsViewModel _gunsViewModel;

        public DandaoUserControl()
        {
            InitializeComponent();
            _gunsViewModel = GunsViewModel.GetGunsViewModel();
            base.DataContext = _gunsViewModel;
        }
    }
}
