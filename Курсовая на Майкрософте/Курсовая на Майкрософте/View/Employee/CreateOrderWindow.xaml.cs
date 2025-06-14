using RequestDataAccess.Repository;
using RequestDataAccess;
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
using System.Windows.Shapes;
using RequestDataAccess.Entity;
using System.Collections.ObjectModel;

namespace Курсовая_на_Майкрософте.View.Employee
{
    /// <summary>
    /// Логика взаимодействия для CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        public CreateOrderWindow()
        {
            InitializeComponent();
            DataContext = new CreateOrderViewModel();
            //DataContext = new CreateOrderViewModel(new OrderRepository(new ApplicationContext()), new ApplicationContext());
        }

    }
}
