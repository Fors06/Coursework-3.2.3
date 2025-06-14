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


    //public class CreateOrderViewModel : INotifyPropertyChanged
    //{
    //    private readonly ApplicationContext _context;

    //    // Список мастеров, машин, клиентов и сервисов
    //    private List<Employee> _masters;
    //    private List<Cars> _cars;
    //    private List<Client> _clients;
    //    private List<Car_service_center> _carCenters;
    //    private List<Services> _services;

    //    // Выбранные элементы
    //    private Employee _selectedMaster;
    //    private Cars _selectedCar;
    //    private Client _selectedClient;
    //    private Car_service_center _selectedCarCenter;
    //    private Services _selectedService;

    //    // Поля формы
    //    private string _clientFirstName;
    //    private string _clientLastName;
    //    private string _clientMiddleName;
    //    private string _carBrand;
    //    private string _carModel;
    //    private string _carYear;
    //    private string _serviceCost;
    //    private DateTime _startDate;

    //    // Команда для сохранения заказа
    //    public ICommand SaveOrderCommand { get; }

    //    public CreateOrderViewModel()
    //    {
    //        _context = new ApplicationContext();
    //        SaveOrderCommand = new RelayCommand(async _ => await SaveOrderAsync(), CanExecuteSaveOrder);
    //        LoadDataFromDatabase();
    //    }

    //    // Синхронизация данных с БД
    //    private async Task LoadDataFromDatabase()
    //    {
    //        await Task.Run(() =>
    //        {
    //            using var context = new ApplicationContext();
    //            _masters = context.employees.ToList();
    //            _cars = context.cars.ToList();
    //            _clients = context.clients.ToList();
    //            _carCenters = context.car_Service_Centers.ToList();
    //            _services = context.serviceses.ToList();

    //            // Загрузка марок и моделей автомобилей
    //            CarBrand = _cars.Select(car => car.Марка).Distinct().ToList();
    //            CarModel = _cars.Select(car => car.Модель).Distinct().ToList();
    //        });
    //    }

    //    // Методы getter/setter для данных и событий изменения
    //    public List<Employee> Masters => _masters;
    //    public List<Cars> Cars => _cars;
    //    public List<Client> Clients => _clients;
    //    public List<Car_service_center> CarCenters => _carCenters;
    //    public List<Services> Services => _services;


    //    public Employee SelectedMaster
    //    {
    //        get => _selectedMaster;
    //        set
    //        {
    //            if (_selectedMaster != value)
    //            {
    //                _selectedMaster = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    public Cars SelectedCar
    //    {
    //        get => _selectedCar;
    //        set
    //        {
    //            if (_selectedCar != value)
    //            {
    //                _selectedCar = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    public Client SelectedClient
    //    {
    //        get => _selectedClient;
    //        set
    //        {
    //            if (_selectedClient != value)
    //            {
    //                _selectedClient = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    public Car_service_center SelectedCarCenter
    //    {
    //        get => _selectedCarCenter;
    //        set
    //        {
    //            if (_selectedCarCenter != value)
    //            {
    //                _selectedCarCenter = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    public Services SelectedService
    //    {
    //        get => _selectedService;
    //        set
    //        {
    //            if (_selectedService != value)
    //            {
    //                _selectedService = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    public string ClientFirstName
    //    {
    //        get => _clientFirstName;
    //        set
    //        {
    //            if (_clientFirstName != value)
    //            {
    //                _clientFirstName = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    public string ClientLastName
    //    {
    //        get => _clientLastName;
    //        set
    //        {
    //            if (_clientLastName != value)
    //            {
    //                _clientLastName = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    public string ClientMiddleName
    //    {
    //        get => _clientMiddleName;
    //        set
    //        {
    //            if (_clientMiddleName != value)
    //            {
    //                _clientMiddleName = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }



    //    public string CarBrand
    //    {
    //        get => _carBrand;
    //        set
    //        {
    //            _carBrand = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public List<string> CarModels { get; private set; }

    //    public string CarModel
    //    {
    //        get => _carModel;
    //        set
    //        {
    //            if (_carModel != value)
    //            {
    //                _carModel = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    public string CarYear
    //    {
    //        get => _carYear;
    //        set
    //        {
    //            if (_carYear != value)
    //            {
    //                _carYear = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    public string ServiceCost
    //    {
    //        get => _serviceCost;
    //        set
    //        {
    //            if (_serviceCost != value)
    //            {
    //                _serviceCost = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    public DateTime StartDate
    //    {
    //        get => _startDate;
    //        set
    //        {
    //            if (_startDate != value)
    //            {
    //                _startDate = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    // Асинхронное сохранение заказа
    //    private async Task SaveOrderAsync()
    //    {
    //        try
    //        {
    //            if (!await ValidateFormAsync()) return;

    //            var order = new Order
    //            {
    //                Дата_Начала = StartDate,
    //                Стоимость = decimal.Parse(ServiceCost),
    //                Cars_id = GetOrAddCar(),
    //                Clients_id = GetOrAddClient(),
    //                Master_id = SelectedMaster.Id,
    //                Auto_Service_id = SelectedCarCenter.Id,
    //                Services_id = SelectedService.Id
    //            };

    //            using var context = new ApplicationContext();
    //            context.Add(order);
    //            await context.SaveChangesAsync();

    //            await Dispatcher.CurrentDispatcher.InvokeAsync(() => MessageBox.Show("Запись успешно сохранена"));
    //        }
    //        catch (Exception ex)
    //        {
    //            await Dispatcher.CurrentDispatcher.InvokeAsync(() => MessageBox.Show($"Ошибка сохранения записи: {ex.Message}"));
    //        }
    //    }

    //    // Валидатор формы
    //    private async Task<bool> ValidateFormAsync()
    //    {
    //        if (string.IsNullOrEmpty(ClientFirstName))
    //        {
    //            await Dispatcher.CurrentDispatcher.InvokeAsync(() => MessageBox.Show("Укажите имя клиента."));
    //            return false;
    //        }
    //        if (string.IsNullOrEmpty(ClientLastName))
    //        {
    //            await Dispatcher.CurrentDispatcher.InvokeAsync(() => MessageBox.Show("Укажите фамилию клиента."));
    //            return false;
    //        }
    //        if (SelectedMaster == null || SelectedCarCenter == null || SelectedService == null)
    //        {
    //            await Dispatcher.CurrentDispatcher.InvokeAsync(() => MessageBox.Show("Выберите мастера, сервис-центр и услугу."));
    //            return false;
    //        }
    //        if (string.IsNullOrEmpty(CarBrand) || string.IsNullOrEmpty(CarModel) || string.IsNullOrEmpty(CarYear))
    //        {
    //            await Dispatcher.CurrentDispatcher.InvokeAsync(() => MessageBox.Show("Заполните данные автомобиля."));
    //            return false;
    //        }
    //        if (string.IsNullOrEmpty(ServiceCost))
    //        {
    //            await Dispatcher.CurrentDispatcher.InvokeAsync(() => MessageBox.Show("Укажите стоимость услуги."));
    //            return false;
    //        }
    //        return true;
    //    }

    //    // Получение клиента
    //    private int GetOrAddClient()
    //    {
    //        using var context = new ApplicationContext();
    //        var existingClient = context.clients.FirstOrDefault(c => c.Фамилия == ClientLastName && c.Имя == ClientFirstName && c.Отчество == ClientMiddleName);
    //        if (existingClient != null)
    //        {
    //            return existingClient.Id;
    //        }
    //        else
    //        {
    //            var newClient = new Client
    //            {
    //                Фамилия = ClientLastName,
    //                Имя = ClientFirstName,
    //                Отчество = ClientMiddleName
    //            };
    //            context.Add(newClient);
    //            context.SaveChanges();
    //            return newClient.Id;
    //        }
    //    }

    //    // Получение машины
    //    private int GetOrAddCar()
    //    {
    //        using var context = new ApplicationContext();
    //        var existingCar = context.cars.FirstOrDefault(c => c.Марка == CarBrand && c.Модель == CarModel && c.Год_Выпуска == int.Parse(CarYear));
    //        if (existingCar != null)
    //        {
    //            return existingCar.Id;
    //        }
    //        else
    //        {
    //            var newCar = new Cars
    //            {
    //                Марка = CarBrand,
    //                Модель = CarModel,
    //                Год_Выпуска = int.Parse(CarYear)
    //            };
    //            context.Add(newCar);
    //            context.SaveChanges();
    //            return newCar.Id;
    //        }
    //    }

    //    // Уведомление об изменении свойств
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }

    //    // Возможность сохранить заказ
    //    private bool CanExecuteSaveOrder(object obj)
    //    {
    //        return !string.IsNullOrEmpty(ClientFirstName) &&
    //               !string.IsNullOrEmpty(ClientLastName) &&
    //               SelectedMaster != null &&
    //               SelectedCarCenter != null &&
    //               SelectedService != null &&
    //               !string.IsNullOrEmpty(CarBrand) &&
    //               !string.IsNullOrEmpty(CarModel) &&
    //               !string.IsNullOrEmpty(CarYear) &&
    //               !string.IsNullOrEmpty(ServiceCost);
    //    }
    //}
}






public class CreateOrderViewModel : INotifyPropertyChanged
{
    private readonly ApplicationContext _context;

    private IRepository<Car_service_center> _carServiseCentrRepository;
    private IRepository<Services> _serviseRepository;
    private IRepository<Users> _usersRepository;
    private IRepository<Client> _clientRepository;
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

    public CreateOrderViewModel()
    {
        _context = new ApplicationContext();
        _carServiseCentrRepository = new CarServiseCenterRepository(_context);
        _usersRepository = new UsersRepository(_context);
        _clientRepository = new Clientepository(_context);
        SaveOrderCommand = new RelayCommand(SaveOrder, CanExecuteSaveOrder);
        LoadDataFromDatabase();
        LoadCarServiceCenters();
    }

    // Синхронизация данных с БД
    private void LoadDataFromDatabase()
    {
        using var context = new ApplicationContext();
        Employee master = new Employee();
        
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

    private Car_service_center selectedCarCenter;

    public Car_service_center SelectedCarCenter
    {
        get => selectedCarCenter;
        set
        {
            selectedCarCenter = value;
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
            OnPropertyChanged(nameof(Adress));
            FilterAddresses();
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

            if (SelectedService == null || SelectedMaster == null || SelectedCarCenter == null)
            {
                MessageBox.Show("Выберите услугу, мастера и сервис-центр.");
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
                Services_id = SelectedService.Id
            };
        try
        {
            _orderRepository.Create(order);
            _orderRepository.Save();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения записи: {ex}");
        }
    }

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
                Email = null,
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


    //private List<Employee> masters = new List<Employee>();
    //private List<Cars> cars = new List<Cars>();
    //private List<Client> clients = new List<Client>();
    //private List<Car_service_center> carCenters = new List<Car_service_center>();
    //private List<Services> services = new List<Services>();

    //// Выбранные элементы
    //private Employee selectedMaster;
    //private Cars selectedCar;
    //private Client selectedClient;
    //private Car_service_center selectedCarCenter;
    //private Services selectedService;

    //// Поля формы
    //private string clientFirstName;
    //private string clientLastName;
    //private string clientMiddleName;
    //private string carBrand;
    //private string carModel;
    //private string carYear;
    //private string serviceCost;
    //private DateTime startDate;

    //// Команды
    //public ICommand SaveOrderCommand { get; set; }

    //public event PropertyChangedEventHandler PropertyChanged;

    //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //}

    //// Свойства
    //public List<Employee> Masters => masters;
    //public List<Cars> Cars => cars;
    //public List<Client> Clients => clients;
    //public List<Car_service_center> CarCenters => carCenters;
    //public List<Services> Services => services;

    //public Employee SelectedMaster
    //{
    //    get => selectedMaster;
    //    set
    //    {
    //        if (selectedMaster != value)
    //        {
    //            selectedMaster = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public Cars SelectedCar
    //{
    //    get => selectedCar;
    //    set
    //    {
    //        if (selectedCar != value)
    //        {
    //            selectedCar = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public Client SelectedClient
    //{
    //    get => selectedClient;
    //    set
    //    {
    //        if (selectedClient != value)
    //        {
    //            selectedClient = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public Car_service_center SelectedCarCenter
    //{
    //    get => selectedCarCenter;
    //    set
    //    {
    //        if (selectedCarCenter != value)
    //        {
    //            selectedCarCenter = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public Services SelectedService
    //{
    //    get => selectedService;
    //    set
    //    {
    //        if (selectedService != value)
    //        {
    //            selectedService = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public string ClientFirstName
    //{
    //    get => clientFirstName;
    //    set
    //    {
    //        if (clientFirstName != value)
    //        {
    //            clientFirstName = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public string ClientLastName
    //{
    //    get => clientLastName;
    //    set
    //    {
    //        if (clientLastName != value)
    //        {
    //            clientLastName = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public string ClientMiddleName
    //{
    //    get => clientMiddleName;
    //    set
    //    {
    //        if (clientMiddleName != value)
    //        {
    //            clientMiddleName = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public string CarBrand
    //{
    //    get => carBrand;
    //    set
    //    {
    //        if (carBrand != value)
    //        {
    //            carBrand = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public string CarModel
    //{
    //    get => carModel;
    //    set
    //    {
    //        if (carModel != value)
    //        {
    //            carModel = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public string CarYear
    //{
    //    get => carYear;
    //    set
    //    {
    //        if (carYear != value)
    //        {
    //            carYear = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public string ServiceCost
    //{
    //    get => serviceCost;
    //    set
    //    {
    //        if (serviceCost != value)
    //        {
    //            serviceCost = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public DateTime StartDate
    //{
    //    get => startDate;
    //    set
    //    {
    //        if (startDate != value)
    //        {
    //            startDate = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    //public CreateOrderViewModel()
    //{
    //    // Инициализация команд
    //    SaveOrderCommand = new RelayCommand(SaveOrder);

    //    // Получение данных из базы данных или хранилища
    //    LoadDataFromDatabase();
    //}

    //private async Task LoadDataFromDatabase()
    //{
    //    await Task.Run(() =>
    //    {
    //        using var context = new ApplicationContext(); // Предположим, что контекст доступен

    //        masters = context.employees.ToList();
    //        cars = context.cars.ToList();
    //        clients = context.clients.ToList();
    //        carCenters = context.car_Service_Centers.ToList();
    //        services = context.serviceses.ToList();
    //    });
    //}

    //private void SaveOrder(object parameter)
    //{
    //    try
    //    {
    //        // Проверяем введённые данные
    //        if (!ValidateForm())
    //        {
    //            return;
    //        }

    //        // Создаем новый заказ
    //        var order = new Order
    //        {
    //            Дата_Начала = this.StartDate,
    //            Стоимость = decimal.Parse(ServiceCost),
    //            Cars_id = GetOrAddCar(),
    //            Clients_id = GetOrAddClient(),
    //            Master_id = selectedMaster.Id,
    //            Auto_Service_id = selectedCarCenter.Id,
    //            Services_id = selectedService.Id
    //        };

    //        // Сохраняем изменения в базу данных
    //        using var context = new ApplicationContext();
    //        context.Add(order);
    //        context.SaveChanges();

    //        MessageBox.Show("Запись успешно сохранена");
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show($"Ошибка сохранения записи: {ex.Message}");
    //    }
    //}

    //private bool ValidateForm()
    //{
    //    if (string.IsNullOrEmpty(ClientFirstName))
    //    {
    //        MessageBox.Show("Укажите имя клиента.");
    //        return false;
    //    }
    //    if (string.IsNullOrEmpty(ClientLastName))
    //    {
    //        MessageBox.Show("Укажите фамилию клиента.");
    //        return false;
    //    }
    //    if (SelectedMaster == null || SelectedCarCenter == null || SelectedService == null)
    //    {
    //        MessageBox.Show("Выберите мастера, сервис-центр и услугу.");
    //        return false;
    //    }
    //    if (string.IsNullOrEmpty(CarBrand) || string.IsNullOrEmpty(CarModel) || string.IsNullOrEmpty(CarYear))
    //    {
    //        MessageBox.Show("Заполните данные автомобиля.");
    //        return false;
    //    }
    //    if (string.IsNullOrEmpty(ServiceCost))
    //    {
    //        MessageBox.Show("Укажите стоимость услуги.");
    //        return false;
    //    }
    //    return true;
    //}

    //private int GetOrAddClient()
    //{
    //    using var context = new ApplicationContext();
    //    var existingClient = context.clients.FirstOrDefault(c => c.Фамилия == ClientLastName && c.Имя == ClientFirstName && c.Отчество == ClientMiddleName);
    //    if (existingClient != null)
    //    {
    //        return existingClient.Id;
    //    }
    //    throw new Exception("Клиент не найден в базе данных.");
    //    //else
    //    //{
    //    //    var newClient = new Client
    //    //    {
    //    //        Фамилия = ClientLastName,
    //    //        Имя = ClientFirstName,
    //    //        Отчество = ClientMiddleName
    //    //    };
    //    //    context.Add(newClient);
    //    //    context.SaveChanges();
    //    //    return newClient.Id;
    //    //}
    //}

    //private int GetOrAddCar()
    //{
    //    using var context = new ApplicationContext();
    //    var existingCar = context.cars.FirstOrDefault(c => c.Марка == CarBrand && c.Модель == CarModel && c.Год_Выпуска == int.Parse(CarYear));
    //    if (existingCar != null)
    //    {
    //        return existingCar.Id;
    //    }
    //    else
    //    {
    //        var newCar = new Cars
    //        {
    //            Марка = CarBrand,
    //            Модель = CarModel,
    //            Год_Выпуска = int.Parse(CarYear)
    //        };
    //        context.Add(newCar);
    //        context.SaveChanges();
    //        return newCar.Id;
    //    }
    //}
}








//public class CreateOrderViewModel : INotifyPropertyChanged
//{
//    private readonly ApplicationContext _context;



//    // Список мастеров, машин, клиентов и сервисов
//    private List<Employee> _masters;
//    private List<Cars> _cars;
//    private List<Client> _clients;
//    private List<Car_service_center> _carCenters;
//    private List<Services> _services;

//    // Выбранные элементы
//    private Employee _selectedMaster;
//    private Cars _selectedCar;
//    private Client _selectedClient;
//    private Car_service_center _selectedCarCenter;
//    private Services _selectedService;

//    // Поля формы
//    private string _clientFirstName;
//    private string _clientLastName;
//    private string _clientMiddleName;
//    private string _clientEmail;
//    private string _carBrand;
//    private string _carModel;
//    private string _carYear;
//    private string _serviceCost;
//    private DateTime _startDate;

//    // Команда для сохранения заказа
//    public ICommand SaveOrderCommand { get; }

//    public CreateOrderViewModel()
//    {
//        _context = new ApplicationContext();
//        SaveOrderCommand = new RelayCommand(SaveOrder, CanExecuteSaveOrder);
//        LoadDataFromDatabase();
//    }

//    // Синхронизация данных с БД
//    private void LoadDataFromDatabase()
//    {
//        using var context = new ApplicationContext();
//        _masters = context.employees.ToList();
//        _cars = context.cars.ToList();
//        _clients = context.clients.ToList();
//        _carCenters = context.car_Service_Centers.ToList();
//        _services = context.serviceses.ToList();
//    }

//    // Методы getter/setter для данных и событий изменения
//    public List<Employee> Masters => _masters;
//    public List<Cars> Cars => _cars;
//    public List<Client> Clients => _clients;
//    public List<Car_service_center> CarCenters => _carCenters;
//    public List<Services> Services => _services;

//    public Employee SelectedMaster
//    {
//        get => _selectedMaster;
//        set
//        {
//            if (_selectedMaster != value)
//            {
//                _selectedMaster = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public Cars SelectedCar
//    {
//        get => _selectedCar;
//        set
//        {
//            if (_selectedCar != value)
//            {
//                _selectedCar = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public Client SelectedClient
//    {
//        get => _selectedClient;
//        set
//        {
//            if (_selectedClient != value)
//            {
//                _selectedClient = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public Car_service_center SelectedCarCenter
//    {
//        get => _selectedCarCenter;
//        set
//        {
//            if (_selectedCarCenter != value)
//            {
//                _selectedCarCenter = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public Services SelectedService
//    {
//        get => _selectedService;
//        set
//        {
//            if (_selectedService != value)
//            {
//                _selectedService = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public string ClientFirstName
//    {
//        get => _clientFirstName;
//        set
//        {
//            if (_clientFirstName != value)
//            {
//                _clientFirstName = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public string ClientLastName
//    {
//        get => _clientLastName;
//        set
//        {
//            if (_clientLastName != value)
//            {
//                _clientLastName = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public string ClientMiddleName
//    {
//        get => _clientMiddleName;
//        set
//        {
//            if (_clientMiddleName != value)
//            {
//                _clientMiddleName = value;
//                OnPropertyChanged();
//            }
//        }
//    }
//    public string ClientEmail
//    {
//        get => _clientEmail;
//        set
//        {
//            if(_clientEmail != value)
//            {
//                _clientEmail = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public string CarBrand
//    {
//        get => _carBrand;
//        set
//        {
//            if (_carBrand != value)
//            {
//                _carBrand = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public string CarModel
//    {
//        get => _carModel;
//        set
//        {
//            if (_carModel != value)
//            {
//                _carModel = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public string CarYear
//    {
//        get => _carYear;
//        set
//        {
//            if (_carYear != value)
//            {
//                _carYear = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public string ServiceCost
//    {
//        get => _serviceCost;
//        set
//        {
//            if (_serviceCost != value)
//            {
//                _serviceCost = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public DateTime StartDate
//    {
//        get => _startDate;
//        set
//        {
//            if (_startDate != value)
//            {
//                _startDate = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    // Сохранение заказа
//    private void SaveOrder(object obj)
//    {
//        try
//        {
//            if (!ValidateForm()) return;

//            var order = new Order
//            {
//                Дата_Начала = StartDate,
//                Стоимость = decimal.Parse(ServiceCost),
//                Cars_id = SelectedCar.Id,
//                Clients_id = GetOrAddClient(),
//                Master_id = SelectedMaster.Id,
//                Auto_Service_id = SelectedCarCenter.Id,
//                Services_id = SelectedService.Id
//            };

//            using var context = new ApplicationContext();
//            context.Add(order);
//            context.SaveChanges();

//            MessageBox.Show("Запись успешно сохранена");
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show($"Ошибка сохранения записи: {ex.Message}");
//        }
//    }

//    // Валидация формы
//    private bool ValidateForm()
//    {
//        if (string.IsNullOrEmpty(ClientFirstName))
//        {
//            MessageBox.Show("Укажите имя клиента.");
//            return false;
//        }
//        if (string.IsNullOrEmpty(ClientLastName))
//        {
//            MessageBox.Show("Укажите фамилию клиента.");
//            return false;
//        }
//        if (SelectedMaster == null || SelectedCarCenter == null || SelectedService == null)
//        {
//            MessageBox.Show("Выберите мастера, сервис-центр и услугу.");
//            return false;
//        }
//        if (string.IsNullOrEmpty(CarBrand) || string.IsNullOrEmpty(CarModel) || string.IsNullOrEmpty(CarYear))
//        {
//            MessageBox.Show("Заполните данные автомобиля.");
//            return false;
//        }
//        if (string.IsNullOrEmpty(ServiceCost))
//        {
//            MessageBox.Show("Укажите стоимость услуги.");
//            return false;
//        }
//        return true;
//    }

//    // Получение клиента или добавление нового
//    private int GetOrAddClient()
//    {
//        using var context = new ApplicationContext();
//        var existingClient = context.clients.FirstOrDefault(c => c.Фамилия == ClientLastName && c.Имя == ClientFirstName && c.Отчество == ClientMiddleName);

//        if (existingClient != null)
//        {
//            return existingClient.Id;
//        }
//        else
//        {
//            // Добавляем пользователя
//            var user = new Users
//            {
//                Имя = ClientFirstName,
//                Фамилия = ClientLastName,
//                Отчество = ClientMiddleName,
//                Email = ClientEmail,
//                Телефон = ClientPhoneNumber,
//                Пароль = Passwod,
//                User_Type_id = (int)UserTypes.Клиент // Клиент
//            };

//            _userRepository.Create(user);
//            _userRepository.Save();

//            // Добавляем клиента
//            var client = new Client
//            {
//                Имя = ClientFirstName,
//                Фамилия = ClientLastName,
//                Отчество = ClientMiddleName,
//                Телефон = ClientPhoneNumber,
//                Users_id = user.Id
//            };

//            _clientRepository.Create(client);
//            _clientRepository.Save();

//            return client.Id;
//        }
//    }

//    // Уведомление об изменении свойств
//    public event PropertyChangedEventHandler PropertyChanged;

//    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//    {
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//    }

//    // Возможность сохранить заказ
//    private bool CanExecuteSaveOrder(object obj)
//    {
//        return !string.IsNullOrEmpty(ClientFirstName) &&
//               !string.IsNullOrEmpty(ClientLastName) &&
//               SelectedMaster != null &&
//               SelectedCarCenter != null &&
//               SelectedService != null &&
//               !string.IsNullOrEmpty(CarBrand) &&
//               !string.IsNullOrEmpty(CarModel) &&
//               !string.IsNullOrEmpty(CarYear) &&
//               !string.IsNullOrEmpty(ServiceCost);
//    }
//}