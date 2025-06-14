using Microsoft.EntityFrameworkCore;
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
using Курсовая_на_Майкрософте.ViewModels.Admin.PagesAdminViewModels;

namespace Курсовая_на_Майкрософте.View.Admin.The_common_window.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServisePageAdmin.xaml
    /// </summary>
    public partial class ServisePageAdmin : Page
    {
        public ServisePageAdmin()
        {
            InitializeComponent();

            DataContext = new ServisePageAdminViewModel();
        }
    }
}
