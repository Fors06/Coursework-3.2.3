using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using RequestDataAccess;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository;
using RequestDataAccess.Repository.Abstraction;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Курсовая_на_Майкрософте.ViewModels.Admin.PagesAdminViewModels
{
    public class ServisePageAdminViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationContext _context;

        private IRepository<Car_service_center> _carServiseCentrRepository;
        private IRepository<Services> _serviseRepository;


        public ICommand EditCommand => new RelayCommand(Edit);
        public ICommand ClearFieldsCommand => new RelayCommand(ClearFields);
        public ICommand AddCommand => new RelayCommand(Create);
        public ICommand DeleteServiceCommand => new RelayCommand(DeleteService);

        public ICommand BrowsePhotoCommand => new RelayCommand(BrowsePhoto);

        public ObservableCollection<Services> Servises { get; set; } = new ObservableCollection<Services>();

        public ServisePageAdminViewModel()
        {
            _context = new ApplicationContext();

            _carServiseCentrRepository = new CarServiseCenterRepository(_context);
            _serviseRepository = new ServicesRepository(_context);
            LoadCarServiceCenters();

            LoadServise();
        }

        #region Поля 

        private string _название;
        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
            }
        }

        private string _описание;
        public string Описание
        {
            get => _описание;
            set
            {
                _описание = value;
                OnPropertyChanged(nameof(Описание));
            }
        }

        private decimal? _стоимость;
        public decimal? Стоимость
        {
            get => _стоимость;
            set
            {
                _стоимость = value;
                OnPropertyChanged(nameof(Стоимость));
            }
        }

        private string _adress;
        public string Adress
        {
            get => _adress;
            set
            {
                    _adress = value;
                    OnPropertyChanged(nameof(Adress));
                    FilterAddresses();
                
            }
        }

        private bool showSuggestions;
        public bool ShowSuggestions
        {
            get => showSuggestions;
            set
            {
                showSuggestions = value;
                OnPropertyChanged(nameof(ShowSuggestions));
            }
        }

        private string _photoPath;
        public string PhotoPath
        {
            get => _photoPath;
            set
            {
                _photoPath = value;
                OnPropertyChanged(nameof(PhotoPath));
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

        private Services _selectedServise;
        public Services SelectedServise
        {
            get => _selectedServise;
            set
            {
                _selectedServise = value;
                OnPropertyChanged(nameof(SelectedServise));

                Название = _selectedServise?.Наименование ?? String.Empty;
                Описание = _selectedServise?.Описание ?? String.Empty;
                Стоимость = _selectedServise?.Стоимость;
                Adress = _selectedServise?.ServiseCentr.Адрес ?? String.Empty;
            }
        }

        #endregion

        private void LoadServise()
        {
            var servises = _serviseRepository.GetStatusList()
                                               .Include(e => e.ServiseCentr)
                                               .ToList();
            Servises.Clear();
            foreach (var servise in servises)
            {
                Servises.Add(servise);
            }
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
                Servises.Clear();
                    foreach (var servise in _context.serviceses.Where(c => c.Наименование.Contains(_searchText) ||
                                                                       c.Описание.Contains(_searchText)))
                    {
                    Servises.Add(servise);
                    }
            }
            else
            {
                LoadServise();
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

        #region Редактирование

        public void Edit(object obj)
        {
            if (SelectedServise != null)
            {
                // Получаем значение пути к файлу изображения из привязанного свойства PhotoPath
                string newPhotoPath = PhotoPath;

                // Проверяем, есть ли указанный путь к новому изображению
                if (!string.IsNullOrEmpty(newPhotoPath))
                {
                    // Если да, то заменяем старое изображение новым
                    using (var fs = new FileStream(newPhotoPath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] photoData = new byte[fs.Length];
                        fs.Read(photoData, 0, photoData.Length);
                        SelectedServise.Фотография = photoData;
                    }
                }


                SelectedServise.Наименование = Название;
                SelectedServise.Описание = Описание;
                SelectedServise.Стоимость = Стоимость;

                _serviseRepository.Update(SelectedServise);
                _serviseRepository.Save();

                LoadServise();
                ClearFields(null);
            }
        }

        #endregion

        #region Очистка полей

        public void ClearFields(object obj)
        {
            Название = "";
            Описание = "";
            Стоимость = null;
            Adress = "";
            PhotoPath = "";
        }

        #endregion

        #region Добавление

        public void Create(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(Название) ||
               string.IsNullOrEmpty(Описание) ||
               Стоимость <= 0 ||
               string.IsNullOrEmpty(Adress) ||
               string.IsNullOrEmpty(PhotoPath))
                {
                    MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var normalizedAddress = NormalizeString(Adress);
                var serviceCenter = FindServiceCenterByAddress(_context, Adress);

                if (serviceCenter == null)
                {
                    MessageBox.Show("Нет автосервиса с таким адресом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка стоимости на наличие букв
                if (!Decimal.TryParse(Стоимость.ToString(), out _)) // Пробуем распарсить стоимость
                {
                    MessageBox.Show("Введена некорректная стоимость. Разрешены только цифры и знак разделения целой и дробной частей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var servise = new Services
                {
                    Наименование = Название,
                    Описание = Описание,
                    Стоимость = Стоимость,
                    Auto_Service_id = serviceCenter.Id
                };
                if (!string.IsNullOrEmpty(PhotoPath))
                {
                    servise.Фотография = File.ReadAllBytes(PhotoPath);
                }

                _serviseRepository.Create(servise);
                _serviseRepository.Save();

                LoadServise();
                ClearFields(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private Car_service_center FindServiceCenterByAddress(ApplicationContext context, string address)
        {
            var normalizedAddress = NormalizeString(address); // нормализация введенного адреса
            return context.car_Service_Centers
                .AsEnumerable()
                .FirstOrDefault(c => NormalizeString(c.Адрес) == normalizedAddress); // сравнение нормализованных версий
        }

        #endregion

        #region Удаление

        private void DeleteService(object obj)
        {
            if (SelectedServise != null)
            {
                try
                {
                    _serviseRepository.Delete(SelectedServise.Id);
                    _serviseRepository.Save();

                    // Обновляем список услуг
                    LoadServise();
                }
                catch (Exception ex)
                {
                    // Обрабатываем ошибку, если удаление прошло неудачно
                    MessageBox.Show($"Ошибка при удалении услуги: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion



        private void BrowsePhoto(object obj)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*",
                Multiselect = false
            };

            if (dialog.ShowDialog() == true)
            {
                PhotoPath = dialog.FileName;
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

