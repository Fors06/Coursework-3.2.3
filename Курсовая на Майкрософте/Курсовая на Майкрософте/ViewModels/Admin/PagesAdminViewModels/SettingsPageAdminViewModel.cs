using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Курсовая_на_Майкрософте;
using Курсовая_на_Майкрософте.Data;
using Курсовая_на_Майкрософте.Data.SwitchTheme;
using Курсовая_на_Майкрософте.Forms.Admin.The_common_window;
using Курсовая_на_Майкрософте.Properties;

namespace Курсовая_на_Майкрософте.ViewModels.Admin.PagesAdminViewModels
{
    public class SettingsPageAdminViewModel : INotifyPropertyChanged
    {
        private bool _isDarkThemeSelected;


        public bool IsDarkThemeSelected
        {
            get => _isDarkThemeSelected;
            set
            {
                if (_isDarkThemeSelected != value)
                {
                    _isDarkThemeSelected = value;
                    OnPropertyChanged(nameof(IsDarkThemeSelected));
                }
            }
        }
        private void SaveSettings()
        {
            var appSettings = new AppSettings
            {
                IsDarkThemeSelected = IsDarkThemeSelected
            };
            SettingsManager.SaveAsync(appSettings);
        }

        public SettingsPageAdminViewModel()
        {
            LoadSettings(); // Гарантированно грузим настройки при старте
        }

        private void LoadSettings()
        {
            var settings = SettingsManager.LoadAsync(); // Загружаем настройки 
            IsDarkThemeSelected = settings.IsDarkThemeSelected; // Устанавливаем выбранную тему
        }

        public ICommand SaveSettingsCommand => new RelayCommand(р =>
        {
            SaveSettings();
            SwitchTheme(null);
        });

        private void SwitchTheme(object parameter)
        {
            var themeSource = IsDarkThemeSelected
                ? "Data/SwitchTheme/DarkTheme.xaml"
                : "Data/SwitchTheme/LightTheme.xaml";

            var uri = new Uri(themeSource, UriKind.Relative);
            var dictionary = new ResourceDictionary { Source = uri };
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}