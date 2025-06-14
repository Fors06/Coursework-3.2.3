using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Xceed.Wpf.Toolkit;
using Курсовая_на_Майкрософте.ViewModels.Admin.PagesAdminViewModels;
using Курсовая_на_Майкрософте.ViewModels.Admin.PagesAdminViewModels.Курсовая_на_Майкрософте.ViewModels.Admin.PagesAdminViewModels;

namespace Курсовая_на_Майкрософте.View.Admin.The_common_window.Pages
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class UsersPageAdmin : Page
    {
        public UsersPageAdmin()
        {
            InitializeComponent();

            DataContext = new UsersPageAdminViewModel();
        }
    } 
}
