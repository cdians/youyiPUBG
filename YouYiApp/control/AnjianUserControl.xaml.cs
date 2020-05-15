using System.Windows.Controls;
using YouYiApp.model.view;

namespace YouYiApp.control
{
    /// <summary>
    /// AnjianUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class AnjianUserControl : UserControl
    {
        AnjianViewModel _anjianViewModel;

        public AnjianUserControl()
        {
            InitializeComponent();
            _anjianViewModel = AnjianViewModel.GetAnjianViewModel();
            this.DataContext = _anjianViewModel;
        }
    }
}
