using Microsoft.EntityFrameworkCore;
using RequestDataAccess;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository;
using RequestDataAccess.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Курсовая_на_Майкрософте.Data;
using Курсовая_на_Майкрософте.View;
using Курсовая_на_Майкрософте.View.Client;
using Курсовая_на_Майкрософте.View.Employee;
using Курсовая_на_Майкрософте.ViewModels.EmployeeViewModel.Окна_для_изменений;

namespace Курсовая_на_Майкрософте.ViewModels.EmployeeViewModel
{
    public class WindowEmployeeViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationContext _context;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Cars> _carsRepository;
        private readonly IRepository<Client> _clientsRepository;
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IRepository<Car_service_center> _serviceCentersRepository;
        private readonly IRepository<Services> _servicesRepository;
        private readonly IRepository<Malfunction> _malfunctionRepository;

        public ICommand OpenCreateOrderFormCommand => new RelayCommand(OpenCreateOrderForm);
        public ICommand OpenEditOrderFormCommand => new RelayCommand(OpenEditOrderForm);
        public ICommand GoToEntranceCommand => new RelayCommand(GoToEntrance);
        public ICommand UpdateCommand => new RelayCommand(LoadInitialData);
    

        // Коллекция заказов
        public ObservableCollection<Order> CompletedAndCanceledOrders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<Order> ActiveAndAcceptedOrders { get; set; } = new ObservableCollection<Order>();


        // Коллекция услуг
        public ObservableCollection<Services> Services { get; set; } = new ObservableCollection<Services>();

        // Коллекция автомобилей
        public ObservableCollection<Cars> Cars { get; set; } = new ObservableCollection<Cars>();

        // Коллекция клиентов
        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        // Коллекция сотрудников
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        // Коллекция сервисов
        public ObservableCollection<Car_service_center> ServiceCenters { get; set; } = new ObservableCollection<Car_service_center>();
        public ObservableCollection<Malfunction> Malfunctions { get; set; } = new ObservableCollection<Malfunction>();


        #region SelectedItem

        // Текущий заказ (для редактирования)
        private Order _selectedOrder;


        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        private Services _selectedServices;
        public Services SelectedServices
        {
            get => _selectedServices;
            set
            {
                _selectedServices = value;
                OnPropertyChanged(nameof(SelectedServices));
            }
        }
        private Cars _selectedCars;
        public Cars SelectedCars
        {
            get => _selectedCars;
            set
            {
                _selectedCars = value;
                OnPropertyChanged(nameof(SelectedCars));
            }
        }
        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }
        private Car_service_center _selectedCar;
        public Car_service_center SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        #endregion

        // Конструктор
        public WindowEmployeeViewModel()
        {
            _context = new ApplicationContext();
            _orderRepository = new OrderRepository(_context);
            _carsRepository = new CarsRepository(_context);
            _clientsRepository = new Clientepository(_context);
            _employeesRepository = new EmployeeRepository(_context);
            _serviceCentersRepository = new CarServiseCenterRepository(_context);
            _servicesRepository = new ServicesRepository(_context);
            _malfunctionRepository = new MalfunctionRepository(_context);

            // Загрузка начальных данных
            LoadInitialData(null);
        }

        // Загрузка начальных данных
        private void LoadInitialData(object obj)
        {
            // Очистка существующих данных
            ActiveAndAcceptedOrders.Clear();
            CompletedAndCanceledOrders.Clear();
            Services.Clear();
            Cars.Clear();
            Clients.Clear();
            Employees.Clear();
            ServiceCenters.Clear();

            // Загрузка свежих данных
            LoadOrders();
            LoadServices();
            LoadCars();
            LoadClients();
            LoadEmployees();
            LoadServiceCenters();
        }

        public void LoadMalfunction()
        {
            var malfunctions = _malfunctionRepository.GetStatusList().ToList();

            Malfunctions.Clear();
            foreach(var malfunction in malfunctions)
            {
                Malfunctions.Add(malfunction);
            }
        }

        // Загрузка заказов
        private void LoadOrders()
        {
            var orders = _orderRepository.GetStatusList()
                                            .Include(e => e.Masterid)
                                            .Include(e => e.Carsid)
                                            .Include(e => e.Clientsid)
                                            .Include(e => e.Auto_Serviceid)
                                            .Include(e => e.Servicesid)
                                            .Include(e => e.Orderstatusid)
                                            .ToList();

            // Готовые и отменённые заказы
            CompletedAndCanceledOrders.Clear();
            foreach (var order in orders.Where(o => o.Orderstatusid.Статус_Заказа == "Готов" || o.Orderstatusid.Статус_Заказа == "Отменён"))
            {
                CompletedAndCanceledOrders.Add(order);
            }

            // Выполняемые и принятые заказы
            ActiveAndAcceptedOrders.Clear();
            foreach (var order in orders.Where(o => o.Orderstatusid.Статус_Заказа == "Выполняется" || o.Orderstatusid.Статус_Заказа == "Принят"))
            {
                ActiveAndAcceptedOrders.Add(order);
            }
        }

        // Загрузка услуг
        private void LoadServices()
        {
            var services = _servicesRepository.GetStatusList()
                                                  .Include(e => e.ServiseCentr)
                                                  .ToList();
            Services.Clear();
            foreach (var service in services)
            {
                Services.Add(service);
            }
        }

        // Загрузка автомобилей
        private void LoadCars()
        {
            var cars = _carsRepository.GetStatusList()
                                        .Include(e => e.Clientid)
                                        .Include(e => e.Malfunctionid)
                                        .ToList();
            Cars.Clear();
            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }

        // Загрузка клиентов
        private void LoadClients()
        {
            var clients = _clientsRepository.GetStatusList().Include(e => e.Users).ToList();
            Clients.Clear();
            foreach (var client in clients)
            {
                Clients.Add(client);
            }
        }

        // Загрузка сотрудников
        private void LoadEmployees()
        {
            var employees = _employeesRepository.GetStatusList()
                                                  .Include(e => e.Users)
                                                  .Include(e => e.AutoServise)
                                                  .ToList();
            Employees.Clear();
            foreach (var employee in employees)
            {
                Employees.Add(employee);
            }
        }

        // Загрузка автосервисов
        private void LoadServiceCenters()
        {
            var centers =  _serviceCentersRepository.GetStatusList().ToList();
            ServiceCenters.Clear();
            foreach (var center in centers)
            {
                ServiceCenters.Add(center);
            }
        }

        public void OpenCreateOrderForm(object obj)
        {
            // Создаём экземпляр формы создания заказа
            var createOrderWindow = new CreateOrderWindow();

            // Передаем окно в конструктор ViewModel
            createOrderWindow.DataContext = new CreateOrderViewModel(createOrderWindow);

            // Показываем окно как модальное
            createOrderWindow.ShowDialog();
        }

        public void OpenEditOrderForm(object obj)
        {
            if (SelectedOrder == null)
            {
                // Если заказ не выбран, выводим предупреждение
                MessageBox.Show("Необходимо выбрать заказ.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editWindow = new EditOrderWindow(SelectedOrder);
            editWindow.DataContext = new EditOrderViewModel(SelectedOrder, editWindow); // Передаем только заказ
            bool? result = editWindow.ShowDialog();

            if (result == true)
            {
                LoadInitialData(null);
                MessageBox.Show("Данные успешно сохранены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
               
            }
        }


        public void GoToEntrance(object obj)
        {
            var currentWindow = App.Current.MainWindow as Window ?? Window.GetWindow(obj as DependencyObject);

            // Создаем новое окно Employee
            ClientWindow employeeWindow = new ClientWindow();

            // Назначаем новое окно главным окном приложения
            Application.Current.MainWindow = employeeWindow;

            // Показываем новое окно
            employeeWindow.Show();

            // Закрываем текущее окно (LoginWindow)
            currentWindow.Close();
           
        }

      

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
