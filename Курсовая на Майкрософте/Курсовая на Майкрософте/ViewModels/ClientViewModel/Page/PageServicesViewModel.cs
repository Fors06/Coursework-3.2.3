using OxyPlot.Series;
using RequestDataAccess;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository;
using RequestDataAccess.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Курсовая_на_Майкрософте.ViewModels.ClientViewModel.Page
{
    public class PageServicesViewModel : INotifyPropertyChanged
    {
        ApplicationContext _context;

        private IRepository<Services> _serviseRepository;


        private ICommand _requestTireServiceCommand;

        public ObservableCollection<Services> Servises { get; set; } = new ObservableCollection<Services>();

        public PageServicesViewModel()
        {
            _context = new ApplicationContext();
            _serviseRepository = new ServicesRepository(_context);
            LoadServices();
        }

        private void LoadServices()
        {
            var services = _serviseRepository.GetStatusList(); 
            foreach (var s in services)
            {
                Servises.Add(s);
            }
        }

        #region Переход к главной с прокруткой

        public ICommand RequestTireServiceCommand
        {
            get
            {
                return _requestTireServiceCommand ??= new RelayCommand(RequestTireService);
            }
        }

        private void RequestTireService(object obj)
        {
            // Получаем NavigationService текущего окна
            //var navigationService = Application.Current.MainWindow.NavigationService;

            //// Переход на главную страницу
            //navigationService.Navigate(new Uri("/View/Client/MainPage.xaml", UriKind.Relative));

            //// Прокрутка ScrollViewer в самый низ
            //if (ScrollViewer != null)
            //{
            //    ScrollViewer.ScrollToBottom();
            //}
        }

        public ScrollViewer ScrollViewer { get; set; }

        #endregion

        #region OnPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
