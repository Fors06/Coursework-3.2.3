using RequestDataAccess;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository;
using RequestDataAccess.Repository.Abstraction;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Курсовая_на_Майкрософте;

public class HomePageClientViewModel : INotifyPropertyChanged
{
    ApplicationContext _context;

    private IRepository<Services> _servicesRepository;

    private string _имя;
    private string _телефон;
    private string _кооментарий;

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

    public string Комментарий
    {
        get => _кооментарий;
        set
        {
            _кооментарий = value;
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


    private RelayCommand _sendCommand;
    public ICommand SendCommand => _sendCommand ??= new RelayCommand(SendEmail);

    private void SendEmail(object obj)
    {
        try
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
            smtpClient.Send(mailMessage);

            MessageBox.Show("Заявка успешно отправлена!");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при отправке заявки: {ex}");
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

   
}