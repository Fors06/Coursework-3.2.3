using RequestDataAccess;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository;
using RequestDataAccess.Repository.Abstraction;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace Курсовая_на_Майкрософте.ViewModels.ClientViewModel.Page
{
    public class PageServicesViewModel : INotifyPropertyChanged
    {
        ApplicationContext _context;

        private IRepository<Services> _serviseRepository;



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


        #region OnPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
