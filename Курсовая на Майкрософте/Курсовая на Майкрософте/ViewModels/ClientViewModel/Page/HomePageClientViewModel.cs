using RequestDataAccess;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository;
using RequestDataAccess.Repository.Abstraction;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Курсовая_на_Майкрософте;

public class HomePageClientViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
{
    ApplicationContext _context;

    private IRepository<Services> _servicesRepository;

    private RelayCommand _sendCommand;

    public ICommand SendCommand => _sendCommand ??= new RelayCommand(SendEmail);

    private string _имя;
    private string _телефон;
    private string _комментарий;

    private Services _selectedService;
    private BitmapImage _image;

    #region Поля
    public string Имя
    {
        get => _имя;
        set
        {
            _имя = value;
            OnPropertyChanged(nameof(Имя));
            ValidateProperty(nameof(Имя), value);
        }
    }

    public string Телефон
    {
        get => _телефон;
        set
        {
            _телефон = value;
            OnPropertyChanged(nameof(Телефон));
            ValidateProperty(nameof(Телефон), value);
        }
    }

    public string Комментарий
    {
        get => _комментарий;
        set
        {
            _комментарий = value;
            OnPropertyChanged(nameof(Комментарий));
        }
    }
    #endregion


    public Services SelectedService
    {
        get => _selectedService;
        set
        {
            _selectedService = value;
            OnPropertyChanged();
            LoadImage();
        }
    }

    public BitmapImage Image
    {
        get => _image;
        set
        {
            _image = value;
            OnPropertyChanged();
        }
    }

    public HomePageClientViewModel()
    {
        _context = new ApplicationContext();
        _servicesRepository = new ServicesRepository(_context);
        // Инициализация данных
        LoadData();
    }

    private void LoadData()
    {
        var services = _servicesRepository.GetStatus(1);

        SelectedService = services;
    }

    private void LoadImage()
    {
        if (SelectedService != null && SelectedService.Фотография != null)
        {
            Image = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(SelectedService.Фотография))
            {
                Image.BeginInit();
                Image.CacheOption = BitmapCacheOption.OnLoad;
                Image.StreamSource = stream;
                Image.EndInit();
            }
        }
    }


    #region Отправка сообщения

    private void SendEmail(object obj)
    {
        if (!HasErrors)
        {
            // Данные для отправки
            string fromEmail = "iv_artem06@mail.ru";
            string toEmail = "my_sql_06@mail.ru";
            string subject = "Новая заявка с сайта";
            string body = $"Имя: {Имя}\nТелефон: {Телефон}\nКомментарий: {Комментарий}";

            // SMTP-сервер mail.ru
            SmtpClient smtpClient = new SmtpClient("smtp.mail.ru", 587)
            {
                Credentials = new NetworkCredential(fromEmail, "mfwYEsjQtm2pgNM4FVjn"),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage(fromEmail, toEmail, subject, body);

            try
            {
                smtpClient.Send(mailMessage); // Отправляем письмо

                MessageBox.Show("Заявка успешно отправлена!");
            }
            catch (SmtpException ex)
            {
                // Обрабатываем ошибку отправки, возможно отсутствие интернета
                MessageBox.Show($"Ошибка отправки электронной почты: {ex.Message}");
            }
        }
        else
        {
            MessageBox.Show("Исправьте ошибки в данных перед отправкой.");
        }
    }

    #endregion

    #region Валидация и сообщения об изменении
    public bool HasErrors => _errors.Count > 0;

    private void ValidateProperty(string propertyName, object value)
    {
        _errors.Remove(propertyName);

        switch (propertyName)
        {
            case nameof(Имя):
                if (string.IsNullOrWhiteSpace((string)value))
                    AddError(nameof(Имя), "Имя не может быть пустым");
                break;
            case nameof(Телефон):
                if (string.IsNullOrWhiteSpace((string)value))
                    AddError(nameof(Телефон), "Телефон не может быть пустым");
                if (!Regex.IsMatch((string)value, @"^\+?\d{1,3}?[-.\s]?$?(?:\d{2,3})$?[-.\s]?\d{3}[-.\s]?\d{4,7}$"))
                    AddError(nameof(Телефон), "Некорректный формат телефона");
                break;
        }

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    private void AddError(string propertyName, string errorMessage)
    {
        if (!_errors.ContainsKey(propertyName))
            _errors[propertyName] = new List<string>();
        _errors[propertyName].Add(errorMessage);
    }


    public event PropertyChangedEventHandler PropertyChanged;
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }



    public IEnumerable GetErrors(string? propertyName)
    {
        if (_errors.ContainsKey(propertyName))
            return _errors[propertyName];
        return null;
    }
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    #endregion
}