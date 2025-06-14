using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Xceed.Wpf.Toolkit;
namespace ViewModels1
{
    public class LoginViewModel : INotifyPropertyChanged
    {
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

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginExecute);
        }

        private async void LoginExecute(object obj)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль.");
                return;
            }

            var user = await AuthenticateUser(Username, Password);

            if (user != null)
            {
                MessageBox.Show($"Успешная авторизация! Привет, {user.FirstName} {user.LastName}");
            }
            else
            {
                MessageBox.Show("Неверные имя пользователя или пароль.");
            }
        }

        private async Task<User> AuthenticateUser(string username, string password)
        {
            using (var connection = new SqlConnection("YourConnectionString"))
            {
                await connection.OpenAsync();

                var query = "SELECT * FROM [dbo].[Пользователь] WHERE Email = @Email AND Пароль = @Password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", username);
                    command.Parameters.AddWithValue("@Password", password);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                Password = reader.GetString(4),
                                Phone = reader.GetString(5),
                                UserTypeId = reader.GetInt32(6)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
