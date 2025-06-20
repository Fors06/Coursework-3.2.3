using Microsoft.EntityFrameworkCore;
using RequestDataAccess;
using RequestDataAccess.Entity;
using RequestDataAccess.User;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Курсовая_на_Майкрософте.Forms.Admin.The_common_window;
using Курсовая_на_Майкрософте.View.Client;

namespace Курсовая_на_Майкрософте.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public int userName;

        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand EnterKeyCommand { get; }
        public ICommand GoToEntranceCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginExecute);
            EnterKeyCommand = new RelayCommand(LoginExecute);
            GoToEntranceCommand = new RelayCommand(GoToEntrance);
        }

        public static int LoggedInUserId { get; set; } // Публичное свойство для сохранения идентификатора

        public async void LoginExecute(object obj)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль.", "Ошибка авторизации");
                return;
            }

            using (var dbContext = new ApplicationContext())
            {
                Users user = await dbContext.users
                    .Include(u => u.User_Typeid)
                    .FirstOrDefaultAsync(u => u.Email == Username && u.Пароль == Password);

                if (user != null)
                {
                    // Проводим проверку роли и уровня доступа
                    if (user.User_Typeid.Роль == "Администратор")
                    {
                        // Формируем профиль пользователя
                        CurrentUserInfo.LoggedInUser = new UserProfile
                        {
                            FirstName = user.Имя,
                            LastName = user.Фамилия,
                            MiddleName = user.Отчество,
                            Email = user.Email,
                            PhoneNumber = user.Телефон,
                            Role = user.User_Typeid.Роль
                        };



                        var currentWindow = App.Current.MainWindow as Window ?? Window.GetWindow(obj as DependencyObject);

                        // Создаем новое окно Employee
                        The_common_window employeeWindow = new The_common_window();

                        // Назначаем новое окно главным окном приложения
                        Application.Current.MainWindow = employeeWindow;

                        employeeWindow.Show();

                        currentWindow.Close();

                    }
                    else if (user.User_Typeid.Роль == "Сотрудник")
                    {

                        // Получить текущее окно 
                        var currentWindow = App.Current.MainWindow as Window ?? Window.GetWindow(obj as DependencyObject);

                        WindowEmployee employeeWindow = new WindowEmployee();

                        // Назначаем новое окно главным окном приложения
                        Application.Current.MainWindow = employeeWindow;

                        employeeWindow.Show();

                        currentWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не имеет прав доступа.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Неверные данные для входа.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void GoToEntrance(object obj)
        {
            var currentWindow = App.Current.MainWindow as Window ?? Window.GetWindow(obj as DependencyObject);

            ClientWindow clientWindow = new ClientWindow();

            Application.Current.MainWindow = clientWindow;

            clientWindow.Show();

            currentWindow.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}