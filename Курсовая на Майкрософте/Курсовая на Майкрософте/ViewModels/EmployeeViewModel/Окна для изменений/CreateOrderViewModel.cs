using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using RequestDataAccess.Entity;
using System;
using Курсовая_на_Майкрософте;
using RequestDataAccess;
using System.Windows;
using System.Collections.ObjectModel;
using RequestDataAccess.Repository;
using RequestDataAccess.Repository.Abstraction;
using Курсовая_на_Майкрософте.View.Employee;

namespace Курсовая_на_Майкрософте.ViewModels.EmployeeViewModel.Окна_для_изменений
{
    public class CreateOrderViewModel : INotifyPropertyChanged
    {
        private readonly Window _window; 
        private readonly ApplicationContext _context;

        private IRepository<Car_service_center> _carServiseCentrRepository;
        private IRepository<Users> _usersRepository;
        private IRepository<Order> _orderRepository;

        // Список мастеров, машин, клиентов и сервисов
        private List<Employee> _masters;
        private List<Cars> _cars;
        private List<Client> _clients;
        private List<Car_service_center> _carCenters;
        private List<Services> _services;

        public ObservableCollection<string> AvailableBrands { get; private set; }
        public ObservableCollection<string> AvailableModels { get; private set; }

        // Выбранные элементы
        private Employee _selectedMaster;
        private Cars _selectedCar;
        private Client _selectedClient;
        private Car_service_center _selectedCarCenter;
        private Services _selectedService;

    

        // Поля формы
        private string _фио;
        private string _clientFirstName;
        private string _clientLastName;
        private string _clientMiddleName;
        private string _телефон;
        private string _carBrand;
        private string _carModel;
        private string _carYear;
        private string _serviceCost;
        private string _адрес;
        private DateTime _startDate;
        private bool showSuggestions;

        // Команда для сохранения заказа
        public ICommand SaveOrderCommand { get; }

        public CreateOrderViewModel(Window window)
        {
            _window = window;

            _context = new ApplicationContext();
            _carServiseCentrRepository = new CarServiseCenterRepository(_context);
            _usersRepository = new UsersRepository(_context);
            _orderRepository = new OrderRepository(_context);
            SaveOrderCommand = new RelayCommand(SaveOrder, CanExecuteSaveOrder);
            LoadDataFromDatabase();
            LoadCarServiceCenters();
        }

        // Синхронизация данных с БД
        private void LoadDataFromDatabase()
        {
            using var context = new ApplicationContext();

            _masters = context.employees.ToList();
            _cars = context.cars.ToList();
            _clients = context.clients.ToList();
            _carCenters = context.car_Service_Centers.ToList();
            _services = context.serviceses.ToList();

            AvailableBrands = new ObservableCollection<string>(_cars.Select(car => car.Марка).Distinct().ToList());
            AvailableModels = new ObservableCollection<string>(_cars.Select(car => car.Модель).Distinct().ToList());
        }

        // Методы getter/setter для данных и событий изменения
        public List<Employee> Masters => _masters;
        public List<Cars> Cars => _cars;
        public List<Client> Clients => _clients;
        public List<Car_service_center> CarCenters => _carCenters;
        public List<Services> Services => _services;

        public string ФИО
        {
            get => _фио;
            set
            {
                _фио = value;
                OnPropertyChanged(nameof(ФИО));
            }
        }


        public Employee SelectedMaster
        {
            get => _selectedMaster;
            set
            {
                if (_selectedMaster != value)
                {
                    _selectedMaster = value;
                    OnPropertyChanged();
                }
            }
        }

        public Cars SelectedCar
        {
            get => _selectedCar;
            set
            {
                if (_selectedCar != value)
                {
                    _selectedCar = value;
                    OnPropertyChanged();
                }
            }
        }

        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                if (_selectedClient != value)
                {
                    _selectedClient = value;
                    OnPropertyChanged();
                }
            }
        }


        public Car_service_center SelectedCarCenter
        {
            get => _selectedCarCenter;
            set
            {
                _selectedCarCenter = value;
                OnPropertyChanged(nameof(SelectedCarCenter));

                if (value != null)
                {
                    Adress = value.Адрес;
                    ShowSuggestions = false; // Закрываем список
                    OnPropertyChanged(nameof(Adress)); // Убедитесь, что событие вызывается
                }
            }
        }

        public Services SelectedService
        {
            get => _selectedService;
            set
            {
                if (_selectedService != value)
                {
                    _selectedService = value;
                    OnPropertyChanged();
                }
            }
        }

        #region Поля
        public string ClientFirstName
        {
            get => _clientFirstName;
            set
            {
                if (_clientFirstName != value)
                {
                    _clientFirstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ClientLastName
        {
            get => _clientLastName;
            set
            {
                if (_clientLastName != value)
                {
                    _clientLastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ClientMiddleName
        {
            get => _clientMiddleName;
            set
            {
                if (_clientMiddleName != value)
                {
                    _clientMiddleName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Телефон
        {
            get => _телефон;
            set
            {
                _телефон = value;
                OnPropertyChanged(nameof(Телефон));
            }
        }

        public string CarBrand
        {
            get => _carBrand;
            set
            {
                _carBrand = value;
                OnPropertyChanged();
            }
        }

        public string CarModel
        {
            get => _carModel;
            set
            {
                if (_carModel != value)
                {
                    _carModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CarYear
        {
            get => _carYear;
            set
            {
                if (_carYear != value)
                {
                    _carYear = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ServiceCost
        {
            get => _serviceCost;
            set
            {
                if (_serviceCost != value)
                {
                    _serviceCost = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Adress
        {
            get => _адрес;
            set
            {
                _адрес = value;
                FilterAddresses();
                OnPropertyChanged(nameof(Adress));
                
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ShowSuggestions
        {
            get => showSuggestions;
            set
            {
                showSuggestions = value;
                OnPropertyChanged(nameof(ShowSuggestions));
            }
        }

        #endregion

        #region список в Адресе

        private IEnumerable<Car_service_center> carServiceCenters;
        private ObservableCollection<Car_service_center> filteredAddresses;



        public IEnumerable<Car_service_center> CarServiceCenters
        {
            get => carServiceCenters;
            set
            {
                carServiceCenters = value;
                OnPropertyChanged(nameof(CarServiceCenters));
            }
        }

        public ObservableCollection<Car_service_center> FilteredAddresses
        {
            get => filteredAddresses;
            set
            {
                filteredAddresses = value;
                OnPropertyChanged(nameof(FilteredAddresses));
            }
        }



        private void LoadCarServiceCenters()
        {
            CarServiceCenters = _carServiseCentrRepository.GetStatusList();
        }

        private void FilterAddresses()
        {
            if (string.IsNullOrEmpty(Adress))
            {
                FilteredAddresses = new ObservableCollection<Car_service_center>();
                ShowSuggestions = false;
                return;
            }

            var cleanQuery = NormalizeString(Adress);
            var filteredResult = CarServiceCenters.Where(c => NormalizeString(c.Адрес).Contains(cleanQuery));

            if (filteredResult != null && FilteredAddresses != null && !Enumerable.SequenceEqual(filteredResult, FilteredAddresses))
            {
                FilteredAddresses = new ObservableCollection<Car_service_center>(filteredResult);
                ShowSuggestions = FilteredAddresses.Any();
            }
        }


        private static string NormalizeString(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            // Удаляем все знаки пунктуации и приводим строку к нижнему регистру
            var cleanedInput = new string(input.Where(ch => !char.IsPunctuation(ch)).ToArray());
            return cleanedInput.ToLowerInvariant();
        }

        #endregion

        // Сохранение заказа
        private void SaveOrder(object obj)
        {

            if (!ValidateForm()) return;

            if (_orderRepository == null)
            {
                MessageBox.Show("Репозиторий заказов не инициализирован!");
                return;
            }

            var order = new Order
            {
                Дата_Начала = StartDate,
                Дата_Окончания = StartDate,
                Стоимость = (decimal)SelectedService.Стоимость,
                Cars_id = GetOrAddCar(),
                Clients_id = GetOrAddClient(),
                Master_id = SelectedMaster.Id,
                Auto_Service_id = SelectedCarCenter.Id,
                Statuses_id = (int)OrderStatus.Принят,
                Services_id = SelectedService.Id,
            };
            try
            {
                _orderRepository.Create(order);
                _orderRepository.Save();

                _window.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения записи: {ex}");
            }
        }

        #region Валидация
        // Валидация формы
        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(ClientFirstName))
            {
                MessageBox.Show("Укажите имя клиента.");
                return false;
            }
            if (string.IsNullOrEmpty(ClientLastName))
            {
                MessageBox.Show("Укажите фамилию клиента.");
                return false;
            }
            if (SelectedMaster == null || SelectedCarCenter == null || SelectedService == null)
            {
                MessageBox.Show("Выберите мастера, сервис-центр и услугу.");
                return false;
            }
            if (string.IsNullOrEmpty(CarBrand) || string.IsNullOrEmpty(CarModel) || string.IsNullOrEmpty(CarYear))
            {
                MessageBox.Show("Заполните данные автомобиля.");
                return false;
            }
            return true;
        }

        #endregion

        #region Получение клиента и машины
        // Получение клиента или добавление нового
        private int GetOrAddClient()
        {
            using var context = new ApplicationContext();
            var existingClient = context.clients.FirstOrDefault(c => c.Фамилия == ClientLastName && c.Имя == ClientFirstName && c.Отчество == ClientMiddleName);

            if (existingClient != null)
            {
                return existingClient.Id;
            }
            else
            {
                var user = new Users
                {
                    Имя = ClientFirstName,
                    Фамилия = ClientLastName,
                    Отчество = ClientMiddleName,
                    Телефон = Телефон,
                    Пароль = "Passwod",
                    User_Type_id = (int)UserTypes.Клиент // Клиент
                };

                _usersRepository.Create(user);
                _usersRepository.Save();

                var newClient = new Client
                {
                    Имя = ClientFirstName,
                    Фамилия = ClientLastName,
                    Отчество = ClientMiddleName,
                    Телефон = Телефон,
                    Users_id = user.Id
                };
                context.Add(newClient);
                context.SaveChanges();
                return newClient.Id;
            }
        }

        // Получение машины или добавление новой
        private int GetOrAddCar()
        {
            using var context = new ApplicationContext();
            var existingCar = context.cars.FirstOrDefault(c => c.Марка == CarBrand && c.Модель == CarModel && c.Год_Выпуска == int.Parse(CarYear));

            return existingCar.Id;
        }
        #endregion

        // Уведомление об изменении свойств
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Возможность сохранить заказ
        private bool CanExecuteSaveOrder(object obj)
        {
            return !string.IsNullOrEmpty(ClientFirstName) &&
                   !string.IsNullOrEmpty(ClientLastName) &&
                   SelectedMaster != null &&
                   SelectedCarCenter != null &&
                   SelectedService != null &&
                   !string.IsNullOrEmpty(CarBrand) &&
                   !string.IsNullOrEmpty(CarModel) &&
                   !string.IsNullOrEmpty(CarYear);
        }
    
    }

}