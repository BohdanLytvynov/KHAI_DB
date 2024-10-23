using Data.Interfaces;
using DB_Lab7.ViewModels.Pages;
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

namespace DB_Lab7.Views.Pages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        AdminPageViewModel m_viewModel;

        public AdminPage(IDatabase database)
        {
            InitializeComponent();

            m_viewModel = new AdminPageViewModel(database);

            this.DataContext = m_viewModel;  
        }
    }
}
