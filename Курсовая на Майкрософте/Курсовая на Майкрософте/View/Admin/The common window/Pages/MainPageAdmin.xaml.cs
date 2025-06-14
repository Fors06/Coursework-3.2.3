using Microsoft.EntityFrameworkCore;
using RequestDataAccess;
using RequestDataAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Курсовая_на_Майкрософте.ViewModels;
using Курсовая_на_Майкрософте.ViewModels.Admin.PagesAdminViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Курсовая_на_Майкрософте.Forms.Admin.The_common_window.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPageAdmi.xaml
    /// </summary>
    public partial class MainPageAdmin : Page
    {
        private UserProfile _profile;

        public MainPageAdmin(UserProfile profile)
        {
            InitializeComponent();
            _profile = profile;
            DataContext = new MainPageAdminViewModel(profile);
        }

    }
}
