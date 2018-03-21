using cviceni_20180220.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace cviceni_20180220
{
    /// <summary>
    /// Interakční logika pro MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        MainPageViewModel viewModel = new MainPageViewModel();
        public MainPage()
        {
            InitializeComponent();
            //App.Database.DeleteTables();
            this.DataContext = viewModel;
            NavigationServiceSingleton.GetNavigationService().SetCurrentPage(this);
            viewModel.CheckDebts();
        }
        private void UpdatePage(object sender, EventArgs e)
        {
            viewModel.SetTotalYear();
            viewModel.SetTotalMonth();
        }
    }
}
