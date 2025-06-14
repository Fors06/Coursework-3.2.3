using System.Windows;
using System.Windows.Input;
using Курсовая_на_Майкрософте;
using Курсовая_на_Майкрософте.ViewModels;



namespace Курсовая_на_Майкрософте.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      

        public MainWindow()
        {
            InitializeComponent();

           
            DataContext = new LoginViewModel();
           
        }



        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
           
                var viewModel = DataContext as LoginViewModel;
                if (viewModel != null)
                {
                    viewModel.Password = passwordBox.Password;
                }
            
        }
    }
}