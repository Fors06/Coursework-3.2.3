using Microsoft.Extensions.DependencyInjection;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository.Abstraction;
using RequestDataAccess.Repository;
using RequestDataAccess;
using System.Configuration;
using System.Data;
using System.Windows;
using Курсовая_на_Майкрософте.Forms.Admin.The_common_window;
using Курсовая_на_Майкрософте.View;
using Курсовая_на_Майкрософте.ViewModels.Admin.PagesAdminViewModels;
using Курсовая_на_Майкрософте.Data.SwitchTheme;

namespace Курсовая_на_Майкрософте
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Загружаем текущие настройки и устанавливаем тему
            var settings = SettingsManager.LoadAsync();
            SetCurrentTheme(settings.IsDarkThemeSelected);

            ConfigureServices();
        }

        private void SetCurrentTheme(bool isDarkThemeSelected)
        {
            var themeSource = isDarkThemeSelected
                                ? "Data/SwitchTheme/DarkTheme.xaml"
                                : "Data/SwitchTheme/LightTheme.xaml";

            var uri = new Uri(themeSource, UriKind.Relative);
            var dictionary = new ResourceDictionary { Source = uri };
            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add(dictionary);
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();

            // Регистрация репозиториев
            services.AddScoped<IRepository<Client>, Clientepository>();
            services.AddScoped<IRepository<Employee>, EmployeeRepository>();
            services.AddScoped<UsersRepository>();

            // Регистрация ApplicationContext
            services.AddScoped<ApplicationContext>();

            // Регистрация ViewModel
            //services.AddScoped<UsersPageAdminViewModel>();

            // Создаем провайдер услуг
            _serviceProvider = services.BuildServiceProvider();
        }

        // Глобальный доступ к провайдеру
        public IServiceProvider Services => _serviceProvider;
    }
}