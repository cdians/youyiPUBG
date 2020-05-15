using System.Windows.Controls;
using YouYiApp.model.view;

namespace YouYiApp.control
{
    /// <summary>
    /// JichuUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class JichuUserControl : UserControl
    {
        JichuViewModel _jichuViewModel;

        public JichuUserControl()
        {
            InitializeComponent();
            _jichuViewModel = JichuViewModel.GetJichuViewModel();
            this.DataContext = _jichuViewModel;
        }
    }
}
