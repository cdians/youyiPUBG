using System.Windows;
using YouYiApp.model.view;

namespace YouYiApp
{
    /// <summary>
    /// ActivationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ActivationWindow : Window
    {
        private delegate void UpdateUIDelegate();

        ActivationViewModel activationViewModel;

        public ActivationWindow()
        {
            InitializeComponent();
            activationViewModel = ActivationViewModel.GetActivationViewModel(this);
            this.DataContext = activationViewModel;
        }

        public void DelegeteShutDown()
        {
            UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(ShutDownAPP);
            this.Dispatcher.Invoke(updateUIDelegate);
        }

        public void DelegeteClose()
        {
            UpdateUIDelegate updateUIDelegate = new UpdateUIDelegate(CloseWindow);
            this.Dispatcher.Invoke(updateUIDelegate);
        }

        private void ShutDownAPP()
        {
            Application.Current.Shutdown();
        }

        private void CloseWindow()
        {
            this.Close();
        }
    }
}
