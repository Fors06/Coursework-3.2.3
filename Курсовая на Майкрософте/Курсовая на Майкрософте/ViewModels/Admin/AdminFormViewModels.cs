using RequestDataAccess.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Курсовая_на_Майкрософте.Data;
using Курсовая_на_Майкрософте.Forms.Admin.The_common_window.Pages;
using Курсовая_на_Майкрософте.View;
using Курсовая_на_Майкрософте.View.Admin.The_common_window.Pages;
using Курсовая_на_Майкрософте.View.Client;
using Курсовая_на_Майкрософте.ViewModels.Admin.PagesAdminViewModels;

namespace Курсовая_на_Майкрософте.ViewModels.Admin
{
    public class AdminFormViewModels : INotifyPropertyChanged
    {
        private Frame mainFrame; // Основной фрейм для навигации

        // Команда перехода на главную страницу
        public ICommand GoToHomeCommand { get; }

        // Команда перехода на страницу пользователей
        public ICommand GoToUsersCommand { get; }

        // Команда перехода на страницу настроек
        public ICommand GoToSettingsCommand { get; }

        // Команда перехода на страницу заказов
        public ICommand GoToSrviseCommand { get; }

        // Команда выхода из программы
        public ICommand ExitApplicationCommand { get; }

        // Конструктор
        public AdminFormViewModels(Frame frame)
        {
            mainFrame = frame;

            GoToHomeCommand = new RelayCommand(OnGoToHome);
            GoToUsersCommand = new RelayCommand(OnGoToUsers);
            GoToSettingsCommand = new RelayCommand(OnGoToSettings);
            ExitApplicationCommand = new RelayCommand(OnExitApplication);
            GoToSrviseCommand = new RelayCommand(GoToSrvise);
        }

        // Переход на главную страницу
        private void OnGoToHome(object obj)
        {
            mainFrame.Navigate(new MainPageAdmin(CurrentUserInfo.LoggedInUser));
        }

        // Переход на страницу пользователей
        private void OnGoToUsers(object obj)
        {
            mainFrame.Navigate(new UsersPageAdmin());
        }

        private void GoToSrvise(object obj)
        {
            mainFrame.Navigate(new ServisePageAdmin());
        }

        // Переход на страницу настроек
        private void OnGoToSettings(object obj)
        {
            mainFrame.Navigate(new SettingsPageAdmin());
        }

        // Выход из приложения
        private void OnExitApplication(object obj)
        {
            //WindowManager.SwitchWindow(new ClientWindow()); // Перейти к окну админа


            var currentWindow = App.Current.MainWindow as Window ?? Window.GetWindow(obj as DependencyObject);

            // Создаем новое окно Employee
            ClientWindow employeeWindow = new ClientWindow();

            // Назначаем новое окно главным окном приложения
            Application.Current.MainWindow = employeeWindow;

            // Показываем новое окно
            employeeWindow.Show();

            // Закрываем текущее окно (LoginWindow)
            currentWindow.Close();


            //var currentWindow = App.Current.MainWindow as Window ?? Window.GetWindow(obj as DependencyObject);

            //if (currentWindow != null)
            //{
            //    var clientWindow = new ClientWindow(); // Создаем новое окно клиента
            //    clientWindow.Show();                   // Показываем новое окно

            //    currentWindow.Close();                 // Закрываем текущее окно администратора
            //}


            //Application.Current.Shutdown();
        }

        // Реализация уведомлений об изменениях свойств
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

