using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Курсовая_на_Майкрософте.View;
using Курсовая_на_Майкрософте.View.Client.Pages;

namespace Курсовая_на_Майкрософте.ViewModels.ClientViewModel
{
    public class ClientFormViewModels : INotifyPropertyChanged
    {
        private Frame mainFrame;
        public ICommand GoToHomeCommand => new RelayCommand(OnGoToHome);
        public ICommand GoToServicesCommand => new RelayCommand(OnGoToServices);
        public ICommand GoToEntranceCommand => new RelayCommand(GoToEntrance);
        public ClientFormViewModels(Frame frame)
        {
            mainFrame = frame;
            OnGoToHome(null);
        }

        private void OnGoToHome(object obj)
        {
            mainFrame.Navigate(new HomePageClient());
        }

        private void OnGoToServices(object obj)
        {
            mainFrame.Navigate(new PageServices());
        }

        private void GoToEntrance(object obj)
        {


            var currentWindow = App.Current.MainWindow as Window ?? Window.GetWindow(obj as DependencyObject);

            // Создаем новое окно Employee
            MainWindow employeeWindow = new MainWindow();

            // Назначаем новое окно главным окном приложения
            Application.Current.MainWindow = employeeWindow;

            employeeWindow.Show();

            currentWindow.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
