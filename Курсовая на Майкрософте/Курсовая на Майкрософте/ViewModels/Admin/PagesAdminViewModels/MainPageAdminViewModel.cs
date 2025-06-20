using System.Collections.ObjectModel;
using System.ComponentModel;
using RequestDataAccess;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using RequestDataAccess.User;
using OxyPlot;
using OxyPlot.Series;
using System.Windows.Input;
using OxyPlot.Axes;
using System.Diagnostics;
using RequestDataAccess.Repository.Abstraction;

namespace Курсовая_на_Майкрософте.ViewModels.Admin.PagesAdminViewModels
{
    public class MainPageAdminViewModel
    {
        private readonly UsersRepository _usersRepository;


        private readonly ApplicationContext _context;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Employee> _employeeRepository;

        // Коллекции
        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        private UserProfile _profile;

        public UserProfile Profile
        {
            get => _profile;
            set
            {
                if (_profile != value)
                {
                    _profile = value;
                    OnPropertyChanged(nameof(Profile));
                }
            }
        }

        public MainPageAdminViewModel(UserProfile profile)
        {
            _context = new ApplicationContext();

            _clientRepository = new Clientepository(_context);
            _employeeRepository = new EmployeeRepository(_context);
            _usersRepository = new UsersRepository(_context);

            //LoadCurrentUser(userId); // Загрузка данных пользователя

            Profile = profile;

            // Настройка фильтров для разных диапазонов времени
            _rangeFilters = new Dictionary<string, Func<IEnumerable<Order>>>()
            {
                ["За день"] = () => FilterByDate(DateTime.Now.Date),
                ["За неделю"] = () => FilterByWeek(),
                ["За месяц"] = () => FilterByMonth(),
                ["За полгода"] = () => FilterByHalfYear(),
                ["За год"] = () => FilterByYear(),
                ["За всё время"] = () => AllOrders()
            };

           
            RefreshGraphCommand = new RelayCommand(async (_) => await RefreshGraph());
            InitializeGraph();

            LoadClients();
            LoadEmployee();
            LoadUsers();

            // Автозапуск графика
            Task.Run(async () => await RefreshGraph()); // Асинхронная подгрузка графика


        }
        #region Вывод и подсчет клиентов

        public int TotalClientsCount { get; private set; }
        private void LoadClients()
        {
            var clients = _clientRepository.GetStatusList();
            Clients.Clear();
            foreach (var client in clients)
            {
                Clients.Add(client);
            }

            TotalClientsCount = Clients.Count();

            OnPropertyChanged(nameof(TotalClientsCount));
        }
        #endregion

        #region Вывод и подсчет Сотрудников

        public int TotalEmloyeeCount {  get; private set; } 

        private void LoadEmployee()
        {
            var employees = _employeeRepository.GetStatusList();
            Employees.Clear();
            foreach (var employee in employees)
            {
                Employees.Add(employee);
            }
            TotalEmloyeeCount = Employees.Count();
            OnPropertyChanged(nameof(TotalEmloyeeCount));
        }

        #endregion

        #region Подсчет сотрудников
        public int TotalUsersCount { get; private set; }
        

        private void LoadUsers()
        {
            var users = _usersRepository.GetStatusList();
            TotalUsersCount = users.Count();
            OnPropertyChanged(nameof(TotalUsersCount));
        }

        #endregion

        #region Построение графика

        public IEnumerable<string> Ranges { get; } = new[] { "За день", "За неделю", "За месяц", "За полгода", "За год", "За всё время" };

        public string SelectedRange { get; set; } = "За всё время";

        public ICommand RefreshGraphCommand { get; }


        private PlotModel _plotModel;
        private List<Order> _cachedOrders;
        private Dictionary<string, Func<IEnumerable<Order>>> _rangeFilters;

        public PlotModel PlotModel
        {
            get => _plotModel;
            set
            {
                if (_plotModel != value)
                {
                    _plotModel = value;
                    OnPropertyChanged(nameof(PlotModel));
                }
            }
        }

        #region Обновление и загрузка графика

        private async Task RefreshGraph()
        {

            try
            {
                await LoadCachedOrders();
                var filteredOrders = _rangeFilters[SelectedRange].Invoke().ToArray();
                PlotModel = CreatePlotModel(filteredOrders);
                PlotModel.InvalidatePlot(true); // Принуждаем к перерисовке
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error refreshing graph: " + ex.Message);
            }
        }

        private async Task LoadCachedOrders()
        {

            using (var db = new ApplicationContext())
            {
                _cachedOrders = await db.orders.OrderBy(o => o.Дата_Начала).ToListAsync(); // Сортируем по дате
                //Debug.WriteLine($"Loaded {_cachedOrders.Count()} orders.");
            }
        }

        #endregion

        #region Инициализация графика
        private void InitializeGraph()
        {
            PlotModel = new PlotModel { Title = "Изменение стоимости заказов" };

            // Настраиваем ось времени (X):
            var dateTimeAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Дата",
                StringFormat = "yyyy.MM.dd",              // Формат отображения даты
                MajorGridlineStyle = LineStyle.Solid,      // Включаем сетку
                MinorGridlineStyle = LineStyle.Dot,       // Мелкая сетка
                AbsoluteMinimum = double.NaN,              // Автоопределение минимума
                AbsoluteMaximum = double.NaN,              // Автоопределение максимума
                IsZoomEnabled = true,                     // Включаем масштабирование
                IsPanEnabled = true                       // Включаем перемещение графика
            };
            PlotModel.Axes.Add(dateTimeAxis);

            // Настраиваем ось цен (Y):
            var linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Цена",
                MajorGridlineStyle = LineStyle.Solid,      // Включаем основную сетку
                MinorGridlineStyle = LineStyle.Dot,       // Включаем мелкую сетку
                MinimumPadding = 0.1,                      // Расстояние снизу до минимального значения
                MaximumPadding = 0.1,                      // Расстояние сверху до максимального значения
                AbsoluteMinimum = double.NaN,              // Автоопределение минимума
                AbsoluteMaximum = double.NaN,              // Автоопределение максимума
                IsZoomEnabled = true,                     // Масштабируемость по оси Y
                IsPanEnabled = true,                        // Перемещаемость по оси Y
                
            };
            PlotModel.Axes.Add(linearAxis);

            // Добавляем линию данных
            PlotModel.Series.Add(new LineSeries { Title = "Стоимость заказов" });

            // Первая загрузка графика
            PlotModel.InvalidatePlot(true);
        }

        #endregion

        #region Рисование графика
        private PlotModel CreatePlotModel(IEnumerable<Order> filteredOrders)
        {
            // Очищаем старую серию
            if (PlotModel.Series.Any())
            {
                PlotModel.Series.Clear();
            }

            // Добавляем серию
            var lineSeries = new LineSeries { Title = "Стоимость заказов" };
            foreach (var order in filteredOrders.OrderBy(o => o.Дата_Начала))
            {
                lineSeries.Points.Add(new DataPoint(
                    DateTimeAxis.ToDouble(order.Дата_Начала), // Преобразование даты в удобное для графика число
                    (double)order.Стоимость
                ));
            }

            PlotModel.Series.Add(lineSeries);
            return PlotModel;

            // PlotModel = new PlotModel { Title = "Изменение стоимости заказов" };

            //// Формируем новый график
            //if (PlotModel.Series.Any()) PlotModel.Series.Clear();

            //// Создаем серию для отображения данных
            //var lineSeries = new LineSeries { Title = "Стоимость заказов" };
            //foreach (var order in filteredOrders.OrderBy(o => o.Дата))
            //{
            //    lineSeries.Points.Add(new DataPoint(
            //        DateTimeAxis.ToDouble(order.Дата),   // Время
            //        (double)order.Стоимость));           // Цена
            //}
            //PlotModel.Series.Add(lineSeries);

            //// Найдем минимальное и максимальное значение дат среди полученных заказов
            //var minDate = filteredOrders.Min(o => o.Дата);
            //var maxDate = filteredOrders.Max(o => o.Дата);

            //// Установим пределы оси времени (X)
            //var xAxis = (DateTimeAxis)PlotModel.Axes.FirstOrDefault(a => a.Position == AxisPosition.Bottom);
            //if (xAxis != null)
            //{
            //    xAxis.AbsoluteMinimum = DateTimeAxis.ToDouble(minDate);
            //    xAxis.AbsoluteMaximum = DateTimeAxis.ToDouble(maxDate);
            //}

            //return PlotModel;
        }

        #endregion

        #region Фильтрация (За день, за неделю, за месяц, за полгода, за год, за все время)

        private IEnumerable<Order> FilterByDate(DateTime date)
        {
            return _cachedOrders.Where(o => o.Дата_Начала.Date == date).ToList();
        }

        private IEnumerable<Order> FilterByWeek()
        {
            var startOfWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            return _cachedOrders.Where(o => o.Дата_Начала >= startOfWeek && o.Дата_Начала <= startOfWeek.AddDays(7)).ToList();
        }

        private IEnumerable<Order> FilterByMonth()
        {
            //var monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //var nextMonthStart = monthStart.AddMonths(1);
            //return _cachedOrders.Where(o => o.Дата >= monthStart && o.Дата < nextMonthStart).ToList();

            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1); // Последний день месяца

            return _cachedOrders.Where(o => o.Дата_Начала >= firstDayOfMonth && o.Дата_Начала <= lastDayOfMonth).ToList();
        }

        private IEnumerable<Order> FilterByHalfYear()
        {
            var sixMonthsAgo = DateTime.Now.AddMonths(-6);
            return _cachedOrders.Where(o => o.Дата_Начала >= sixMonthsAgo).ToList();
        }

        private IEnumerable<Order> FilterByYear()
        {
            var oneYearAgo = DateTime.Now.AddYears(-1);
            return _cachedOrders.Where(o => o.Дата_Начала >= oneYearAgo).ToList();
        }

        private IEnumerable<Order> AllOrders()
        {
            return _cachedOrders;
        }

        #endregion

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
