using RequestDataAccess.Entity;
using RequestDataAccess.Repository;
using RequestDataAccess.Repository.Abstraction;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RequestDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using System.Windows;

namespace Курсовая_на_Майкрософте.ViewModels.Admin.PagesAdminViewModels
{
    namespace Курсовая_на_Майкрософте.ViewModels.Admin.PagesAdminViewModels
    {
        public class UsersPageAdminViewModel : INotifyPropertyChanged
        {
            // Сервис для работы с базой данных
            private readonly ApplicationContext _context;
            private readonly IRepository<Client> _clientRepository;
            private readonly IRepository<Employee> _employeeRepository;
            private readonly IRepository<Users> _userRepository;
            private readonly IRepository<Car_service_center> _carServiseRepository;

            // Коллекции
            public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();
            public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

            // Список возможных должностей
            public IEnumerable<string> Ranges { get; } = new[] { "Менеджер", "Техник" ,"Администратор" };

            // Список состояний сотрудников
            public IEnumerable<string> Condition { get; } = new[] { "Работает", "В отпуске", "На больничном" };

            #region Поля

            private object _activeTab;
            public object ActiveTab
            {
                get => _activeTab;
                set
                {
                    if (_activeTab != value)
                    {
                        _activeTab = value;
                        OnPropertyChanged(nameof(ActiveTab));

                        ClearFields(null);
                    }
                }
            }

            // Свойство выбранного положения
            private string _selectedPosition;
            public string SelectedPosition
            {
                get => _selectedPosition;
                set
                {
                    _selectedPosition = value;
                    OnPropertyChanged(nameof(SelectedPosition));
                }
            }

            // Поля формы ввода для клиентов
            private string _ClientLastName;
            public string ClientLastName
            {
                get => _ClientLastName;
                set
                {
                    _ClientLastName = value;
                    OnPropertyChanged(nameof(ClientLastName));
                }
            }

            private string _ClientFirstName;
            public string ClientFirstName
            {
                get => _ClientFirstName;
                set
                {
                    _ClientFirstName = value;
                    OnPropertyChanged(nameof(ClientFirstName));
                }
            }

            private string _ClientMiddleName;
            public string ClientMiddleName
            {
                get => _ClientMiddleName;
                set
                {
                    _ClientMiddleName = value;
                    OnPropertyChanged(nameof(ClientMiddleName));
                }
            }

            private string _ClientPhoneNumber;
            public string ClientPhoneNumber
            {
                get => _ClientPhoneNumber;
                set
                {
                    _ClientPhoneNumber = value;
                    OnPropertyChanged(nameof(ClientPhoneNumber));
                }
            }

            private string _ClientEmail;
            public string ClientEmail
            {
                get => _ClientEmail;
                set
                {
                    _ClientEmail = value;
                    OnPropertyChanged(nameof(ClientEmail));
                }
            }

            // Поля формы ввода для сотрудников
            private string _EmployeeLastName;
            public string EmployeeLastName
            {
                get => _EmployeeLastName;
                set
                {
                    _EmployeeLastName = value;
                    OnPropertyChanged(nameof(EmployeeLastName));
                }
            }

            private string _EmployeeFirstName;
            public string EmployeeFirstName
            {
                get => _EmployeeFirstName;
                set
                {
                    _EmployeeFirstName = value;
                    OnPropertyChanged(nameof(EmployeeFirstName));
                }
            }

            private string _EmployeeMiddleName;
            public string EmployeeMiddleName
            {
                get => _EmployeeMiddleName;
                set
                {
                    _EmployeeMiddleName = value;
                    OnPropertyChanged(nameof(EmployeeMiddleName));
                }
            }

            private string _EmployeePhoneNumber;
            public string EmployeePhoneNumber
            {
                get => _EmployeePhoneNumber;
                set
                {
                    _EmployeePhoneNumber = value;
                    OnPropertyChanged(nameof(EmployeePhoneNumber));
                }
            }

            private string _EmployeeEmail;
            public string EmployeeEmail
            {
                get => _EmployeeEmail;
                set
                {
                    _EmployeeEmail = value;
                    OnPropertyChanged(nameof(EmployeeEmail));
                }
            }

            private string _EmployeesPosition;
            public string EmployeeloyeesPosition
            {
                get => _EmployeesPosition;
                set
                {
                    _EmployeesPosition = value;
                    OnPropertyChanged(nameof(EmployeeloyeesPosition));
                }
            }

            private string _EmployeeCondition;
            public string EmployeesCondition
            {
                get => _EmployeeCondition;
                set
                {
                    _EmployeeCondition = value;
                    OnPropertyChanged(nameof(EmployeesCondition));
                }
            }

            private string _Passwod;
            public string Passwod
            {
                get => _Passwod;
                set
                {
                    _Passwod = value;
                    OnPropertyChanged(nameof(Passwod));
                }
            }



            private string _EmployeeAutoServise;
            public string EmployeeAutoServise
            {
                get => _EmployeeAutoServise;
                set
                {
                    _EmployeeAutoServise = value;
                    OnPropertyChanged(nameof(EmployeeAutoServise));

                    FilterAddresses();
                }
            }


            private Car_service_center selectedService;

            public Car_service_center SelectedService
            {
                get => selectedService;
                set
                {
                    selectedService = value;
                    OnPropertyChanged(nameof(SelectedService));
                   
                    if (value != null)
                    {
                        EmployeeAutoServise = value.Адрес; // Устанавливаем адрес в текстовое поле
                        ShowSuggestions = false; // Закрываем список
                    }
                }
            }

            // Текущие выбранные клиенты и сотрудники
            private Client _selectedClient;
            public Client SelectedClient
            {
                get => _selectedClient;
                set
                {
                    _selectedClient = value;
                    OnPropertyChanged(nameof(SelectedClient));

                    ClientLastName = _selectedClient?.Имя ?? String.Empty;
                    ClientFirstName = _selectedClient?.Фамилия ?? String.Empty;
                    ClientMiddleName = _selectedClient?.Отчество ?? String.Empty;
                    ClientPhoneNumber = _selectedClient?.Телефон ?? String.Empty;
                    Passwod = _selectedClient?.Users.Пароль ?? String.Empty;
                    ClientEmail = _selectedClient?.Users.Email ?? String.Empty;

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

                    EmployeeFirstName = _selectedEmployee?.Имя ?? String.Empty;
                    EmployeeLastName = _selectedEmployee?.Фамилия ?? String.Empty;
                    EmployeeMiddleName = _selectedEmployee?.Отчество ?? String.Empty;
                    EmployeePhoneNumber = _selectedEmployee?.Телефон ?? String.Empty;
                    EmployeeEmail = _selectedEmployee?.Users?.Email ?? String.Empty;
                    EmployeeloyeesPosition = _selectedEmployee?.Должность ?? String.Empty;
                    Passwod = _selectedEmployee?.Users?.Пароль ?? String.Empty;
                    EmployeeAutoServise = _selectedEmployee?.AutoServise?.Адрес ?? String.Empty;
                    EmployeesCondition = _selectedEmployee?.Состояние ?? String.Empty;
                }
            }

#endregion

            // Команды
            public ICommand AddCommand => new RelayCommand(CreateEmployeesAndClients);
            public ICommand EditCommand => new RelayCommand(Edit);
            public ICommand DeleteCommand => new RelayCommand(Delete);

            public ICommand ClearCommand => new RelayCommand(ClearFields);



            // Конструктор ViewModel
            public UsersPageAdminViewModel()
            {
                _context = new ApplicationContext();
                _clientRepository = new Clientepository(_context);
                _employeeRepository = new EmployeeRepository(_context);
                _userRepository = new UsersRepository(_context);
                _carServiseRepository = new CarServiseCenterRepository(_context);
                LoadCarServiceCenters();

                // Загрузим первоначальные данные
                LoadClients();
                LoadEmployees();
            }



            #region Поиск

            private IEnumerable<Car_service_center> carServiceCenters;
            private ObservableCollection<Car_service_center> filteredAddresses;
            private bool showSuggestions;

        

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

            public bool ShowSuggestions
            {
                get => showSuggestions;
                set
                {
                    showSuggestions = value;
                    OnPropertyChanged(nameof(ShowSuggestions));
                }
            }

            private void LoadCarServiceCenters()
            {
                CarServiceCenters = _carServiseRepository.GetStatusList();
            }

            private void FilterAddresses()
            {
                if (string.IsNullOrEmpty(EmployeeAutoServise))
                {
                    FilteredAddresses = new ObservableCollection<Car_service_center>();
                    ShowSuggestions = false;
                    return;
                }

                var cleanQuery = NormalizeString(EmployeeAutoServise);
                var filteredResult = CarServiceCenters.Where(c => NormalizeString(c.Адрес).Contains(cleanQuery));

                // Если новая коллекция отличается от старой, обновляем
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

            #region Поиск


            private string _searchText;
            public string SearchText
            {
                get => _searchText;
                set
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));

                    ApplyFilters();
                }
            }

            private void ApplyFilters()
            {

                if (!string.IsNullOrEmpty(_searchText))
                {
                    if (ActiveTab is TabItem clientTab && clientTab.Header.Equals("Клиенты"))
                    {
                        Clients.Clear();
                        foreach (var client in _context.clients.Where(c => c.Фамилия.Contains(_searchText)  || 
                                                                           c.Имя.Contains(_searchText)      ||
                                                                           c.Отчество.Contains(_searchText) ||
                                                                           c.Телефон.Contains(_searchText)))
                        {
                            Clients.Add(client);
                        }
                    }
                    
                    if(ActiveTab is TabItem employeeTab && employeeTab.Header.Equals("Сотрудники"))
                    {
                        Employees.Clear();
                        foreach (var employee in _context.employees.Where(e => e.Фамилия.Contains(_searchText) ||
                                                                               e.Имя.Contains(_searchText) ||
                                                                               e.Отчество.Contains(_searchText) ||
                                                                               e.Телефон.Contains(_searchText)))
                        {
                            Employees.Add(employee);
                        }
                    }
                    
                }
                else
                {
                    LoadClients();

                    LoadEmployees();
                }
            }

            #endregion

            #endregion

            #region Загрузка данных
            // Методы для загрузки данных
            private void LoadClients()
            {
                var clients = _clientRepository.GetStatusList().Include(e => e.Users).ToList();
                Clients.Clear();
                foreach (var client in clients)
                {
                    Clients.Add(client);
                }
            }

            private void LoadEmployees()
            {
                var employees = _employeeRepository.GetStatusList()
                                                   .Include(e => e.Users)
                                                   .Include(e => e.AutoServise)
                                                   .ToList();
                Employees.Clear();
                foreach (var employee in employees)
                {
                    Employees.Add(employee);
                }
            }

            #endregion


            #region Общие кнопки

            #region Очищение полей

            public void ClearFields(object obj)
            {
                if (ActiveTab is TabItem employeeTab && employeeTab.Header.Equals("Сотрудники") && SelectedEmployee != null)
                {
                    EmployeeLastName = String.Empty;
                    EmployeeFirstName = String.Empty;
                    EmployeeMiddleName = String.Empty;
                    EmployeeloyeesPosition = null;
                    EmployeePhoneNumber = String.Empty;
                    EmployeeEmail = String.Empty;
                    Passwod = String.Empty;
                    EmployeeAutoServise = String.Empty;
                    EmployeesCondition = null;
                }
                if (ActiveTab is TabItem clientTab && clientTab.Header.Equals("Клиенты") && SelectedClient != null)
                {
                    ClientFirstName = String.Empty;
                    ClientLastName = String.Empty;
                    ClientMiddleName = String.Empty;
                    ClientPhoneNumber = String.Empty;
                    Passwod = String.Empty;
                    ClientEmail = String.Empty;
                }
            }

            #endregion 

            #region Удаление

            public void Delete(object obj)
            {



                if (SelectedEmployee != null || SelectedClient != null)
                {
                    if (ActiveTab is TabItem clientTab && clientTab.Header.Equals("Клиенты") && SelectedClient != null)
                    {
                        _context.Database.ExecuteSqlRaw("EXEC DeleteClient @p0", new object[] { SelectedClient.Id });

                        // Перезагрузка данных после удаления
                        LoadClients();
                    }

                    if (ActiveTab is TabItem employeeTab && employeeTab.Header.Equals("Сотрудники") && SelectedEmployee != null)
                    {
                        _context.Database.ExecuteSqlRaw(" EXEC DeleteEmployee @p0", new object[] { SelectedEmployee.Id });

                        LoadEmployees();
                    }
                    ClearFields(null);
                }
            }

            #endregion

            #region Редактирование

            public void Edit(object obj)
            {
                if (SelectedEmployee != null)
                {
                    // Работаем с имеющимся сотрудником
                    var existingEmployee = SelectedEmployee;

                    // Обновляем профиль пользователя
                    var user = _userRepository.GetStatus(existingEmployee.Users_id);
                    if (user != null)
                    {
                        user.Имя = EmployeeFirstName;
                        user.Фамилия = EmployeeLastName;
                        user.Отчество = EmployeeMiddleName;
                        user.Email = EmployeeEmail;
                        user.Телефон = EmployeePhoneNumber;
                        user.Пароль = Passwod;
                        _userRepository.Update(user);
                        _userRepository.Save();
                    }

                    // Обновляем профиль сотрудника
                    existingEmployee.Имя = EmployeeFirstName;
                    existingEmployee.Фамилия = EmployeeLastName;
                    existingEmployee.Отчество = EmployeeMiddleName;
                    existingEmployee.Телефон = EmployeePhoneNumber;
                    existingEmployee.Должность = EmployeeloyeesPosition;
                    existingEmployee.Состояние = EmployeesCondition;

                    // Авто-сервис не трогаем, если не надо менять адрес
                    _employeeRepository.Update(existingEmployee);
                    _employeeRepository.Save();

                    LoadEmployees(); // Обновляем список сотрудников
                }

                if (SelectedClient != null)
                {
                    // Работаем с клиентом
                    var existingClient = SelectedClient;

                    // Обновляем профиль пользователя
                    var user = _userRepository.GetStatus((int)existingClient.Users_id);
                    if (user != null)
                    {
                        user.Имя = ClientFirstName;
                        user.Фамилия = ClientLastName;
                        user.Отчество = ClientMiddleName;
                        user.Email = ClientEmail;
                        user.Телефон = ClientPhoneNumber;
                        user.Пароль = Passwod;
                        _userRepository.Update(user);
                        _userRepository.Save();
                    }

                    // Обновляем профиль клиента
                    existingClient.Имя = ClientFirstName;
                    existingClient.Фамилия = ClientLastName;
                    existingClient.Отчество = ClientMiddleName;
                    existingClient.Телефон = ClientPhoneNumber;

                    _clientRepository.Update(existingClient);
                    _clientRepository.Save();

                    LoadClients(); // Обновляем список клиентов
                }

                ClearFields(null);
               
            }

            #endregion

            #region Добавление клиента и сотрудника
            public void CreateEmployeesAndClients(object obj)
            {
                // Добавление клиента
                if (!string.IsNullOrEmpty(ClientFirstName) &&
                    !string.IsNullOrEmpty(ClientLastName) &&
                    !string.IsNullOrEmpty(ClientMiddleName) &&
                    !string.IsNullOrEmpty(ClientPhoneNumber) &&
                    !string.IsNullOrEmpty(ClientEmail) &&
                    ActiveTab is TabItem clientTab && clientTab.Header.Equals("Клиенты"))
                {
                    // Добавляем пользователя
                    var user = new Users
                    {
                        Имя = ClientFirstName,
                        Фамилия = ClientLastName,
                        Отчество = ClientMiddleName,
                        Email = ClientEmail,
                        Телефон = ClientPhoneNumber,
                        Пароль = Passwod,
                        User_Type_id = (int)UserTypes.Клиент // Клиент
                    };

                    _userRepository.Create(user);
                    _userRepository.Save();

                    // Добавляем клиента
                    var client = new Client
                    {
                        Имя = ClientFirstName,
                        Фамилия = ClientLastName,
                        Отчество = ClientMiddleName,
                        Телефон = ClientPhoneNumber,
                        Users_id = user.Id
                    };

                    _clientRepository.Create(client);
                    _clientRepository.Save();

                    LoadClients();
                } 

                // Добавление сотрудника
                else if (!string.IsNullOrEmpty(EmployeeFirstName) &&
                         !string.IsNullOrEmpty(EmployeeLastName) &&
                         !string.IsNullOrEmpty(EmployeeMiddleName) &&
                         !string.IsNullOrEmpty(EmployeeEmail) &&
                         !string.IsNullOrEmpty(EmployeePhoneNumber) &&
                         !string.IsNullOrEmpty(EmployeeAutoServise) &&
                         ActiveTab is TabItem employeeTab && employeeTab.Header.Equals("Сотрудники"))
                {
                    // Ищем автосервис по введённому адресу
                    var normalizedAddress = NormalizeString(EmployeeAutoServise);
                    var serviceCenter = FindServiceCenterByAddress(_context, EmployeeAutoServise);

                    if (serviceCenter == null)
                    {
                        MessageBox.Show("Нет автосервиса с таким адресом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Добавляем пользователя
                    var user = new Users
                    {
                        Имя = EmployeeFirstName,
                        Фамилия = EmployeeLastName,
                        Отчество = EmployeeMiddleName,
                        Email = EmployeeEmail,
                        Телефон = EmployeePhoneNumber,
                        Пароль = Passwod,
                        User_Type_id = (int)UserTypes.Сотрудник // Сотрудник
                    };

                    _userRepository.Create(user);
                    _userRepository.Save();

                    // Добавляем сотрудника
                    var employee = new Employee
                    {
                        Имя = EmployeeFirstName,
                        Фамилия = EmployeeLastName,
                        Отчество = EmployeeMiddleName,
                        Телефон = EmployeePhoneNumber,
                        Должность = EmployeeloyeesPosition,
                        Состояние = EmployeesCondition,
                        Auto_Service_id = serviceCenter.Id,
                        Users_id = user.Id
                    };

                    _employeeRepository.Create(employee);
                    _employeeRepository.Save();

                    LoadEmployees();
                    ClearFields(null);
                }
                else
                {
                    MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
            }

            #endregion

            private Car_service_center FindServiceCenterByAddress(ApplicationContext context, string address)
            {
                var normalizedAddress = NormalizeString(address);
                return context.car_Service_Centers
                    .AsEnumerable() 
                    .FirstOrDefault(c => NormalizeString(c.Адрес) == normalizedAddress);
            }


            #endregion


            public event PropertyChangedEventHandler? PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }



            //// Новое свойство для блокировки уведомлений
            //private bool raisePropertyChangedEvents = true;
            //public bool RaisePropertyChangedEvents
            //{
            //    get => raisePropertyChangedEvents;
            //    set => raisePropertyChangedEvents = value;
            //}
        }
    }
}
