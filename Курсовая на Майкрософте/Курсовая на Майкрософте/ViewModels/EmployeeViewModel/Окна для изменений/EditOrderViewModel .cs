using RequestDataAccess;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository;
using RequestDataAccess.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Курсовая_на_Майкрософте.ViewModels.EmployeeViewModel.Окна_для_изменений
{
    public class EditOrderViewModel : INotifyPropertyChanged
    {
        private readonly Window _window;

        ApplicationContext _context;

        private readonly Order _originalOrder;
        private Order _editedOrder;

        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Services> _servicesRepository;
        private readonly IRepository<Order_status> _statusRepository;

        private Order_status selectedStatus;
        private Services selectedService;

        public List<Order_status> AvailableStatuses { get; private set; }
        public List<Services> Services { get; private set; }

        public ICommand SaveOrderCommand => new RelayCommand(SaveOrder);
        public ICommand CancelCommand => new RelayCommand(Cancel);


        public EditOrderViewModel(Order originalOrder, Window window)
        {
            _window = window;

            _context = new ApplicationContext();
            _orderRepository = new OrderRepository(_context);
            _servicesRepository = new ServicesRepository(_context);
            _statusRepository = new OrderStatusRepository(_context);

            _originalOrder = originalOrder ?? throw new ArgumentNullException(nameof(originalOrder));

            _editedOrder = new Order
            {
                Id = originalOrder.Id,
                Дата_Начала = originalOrder.Дата_Начала,
                Дата_Окончания = originalOrder.Дата_Окончания,
                Стоимость = originalOrder.Стоимость,
                Cars_id = originalOrder.Cars_id,
                Clients_id = originalOrder.Clients_id,
                Master_id = originalOrder.Master_id,
                Auto_Service_id = originalOrder.Auto_Service_id,
                Statuses_id = originalOrder.Statuses_id,
                Services_id = originalOrder.Services_id
            };

            AvailableStatuses = _statusRepository.GetStatusList().ToList();
            Services = _servicesRepository.GetStatusList().ToList();

            SelectedStatus = AvailableStatuses.FirstOrDefault(s => s.Id == _editedOrder.Statuses_id);
            SelectedService = Services.FirstOrDefault(s => s.Id == _editedOrder.Services_id);
        }
       
        public Order_status SelectedStatus
        {
            get => selectedStatus;
            set
            {
                if (selectedStatus != value)
                {
                    selectedStatus = value;
                    _editedOrder.Statuses_id = value.Id; // Обновляем ID статуса
                    OnPropertyChanged(nameof(SelectedStatus));
                }
            }
        }
     
        public Services SelectedService
        {
            get => selectedService;
            set
            {
                if (selectedService != value)
                {
                    selectedService = value;
                    _editedOrder.Services_id = value.Id; // Обновляем ID услуги
                    Стоимость = value.Стоимость ?? 0; // Обновление стоимости заказа автоматически
                    OnPropertyChanged(nameof(SelectedService));
                    OnPropertyChanged(nameof(Стоимость)); // Сообщаем об изменении стоимости
                }
            }
        }

        #region Поля
        public string НазваниеРаботы
        {
            get => _editedOrder.Servicesid?.Наименование;
            set
            {
                if (_editedOrder.Servicesid?.Наименование != value)
                {
                    _editedOrder.Servicesid.Наименование = value;
                    OnPropertyChanged(nameof(НазваниеРаботы));
                }
            }
        }

        public DateTime Дата_Начала
        {
            get => _editedOrder.Дата_Начала;
            set
            {
                if (_editedOrder.Дата_Начала != value)
                {
                    _editedOrder.Дата_Начала = value;
                    OnPropertyChanged(nameof(Дата_Начала));
                }
            }
        }

        public DateTime Дата_Окончания
        {
            get => _editedOrder.Дата_Окончания;
            set
            {
                if (_editedOrder.Дата_Окончания != value)
                {
                    _editedOrder.Дата_Окончания = value;
                    OnPropertyChanged(nameof(Дата_Окончания));
                }
            }
        }

        public decimal Стоимость
        {
            get => _editedOrder.Стоимость;
            set
            {
                if (_editedOrder.Стоимость != value)
                {
                    _editedOrder.Стоимость = value;
                    OnPropertyChanged(nameof(Стоимость));
                }
            }
        }

        #endregion

        #region Команды
        // Команда для сохранения изменений
        private void SaveOrder(object obj)
        {
            try
            {
                _orderRepository.Update(_editedOrder);
                _orderRepository.Save();

                // Получаем ссылку на главный ViewModel и принудительно загружаем новые данные
                var mainVm = App.Current.MainWindow.DataContext as WindowEmployeeViewModel;
                mainVm.LoadInitialData(null); // Перезагрузить начальные данные

                _window.Close();
            }
            catch (Exception ex)
            {
                _window.Close();
                Console.WriteLine($"Ошибка обновления записи: {ex.Message}");
            }
        }

        // Команда отмены изменений
        private void Cancel(object obj)
        {
            _editedOrder = new Order
            {
                Id = _originalOrder.Id,
                Дата_Начала = _originalOrder.Дата_Начала,
                Дата_Окончания = _originalOrder.Дата_Окончания,
                Стоимость = _originalOrder.Стоимость,
                Cars_id = _originalOrder.Cars_id,
                Clients_id = _originalOrder.Clients_id,
                Master_id = _originalOrder.Master_id,
                Auto_Service_id = _originalOrder.Auto_Service_id,
                Statuses_id = _originalOrder.Statuses_id,
                Services_id = _originalOrder.Services_id
            };
            _window.Close();
        }

        #endregion



        // Обработчик изменения свойств
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}