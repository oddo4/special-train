using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace cviceni_20180220.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private string sumMonth;

        public string SumMonth
        {
            get
            {
                return sumMonth;
            }
            set
            {
                sumMonth = value;
                RaisePropertyChanged("SumMonth");
            }
        }
        private string sumYear;

        public string SumYear
        {
            get
            {
                return sumYear;
            }
            set
            {
                sumYear = value;
                RaisePropertyChanged("SumYear");
            }
        }
        private RelayCommand showAllDebts;

        public RelayCommand ShowAllDebts
        {
            get
            {
                return showAllDebts;
            }
            set
            {
                showAllDebts = value;
                RaisePropertyChanged("ShowAllDebts");
            }
        }
        private RelayCommand showAllSpends;

        public RelayCommand ShowAllSpends
        {
            get
            {
                return showAllSpends;
            }
            set
            {
                showAllSpends = value;
                RaisePropertyChanged("ShowAllSpends");
            }
        }

        public MainPageViewModel()
        {
            ShowAllDebts = new RelayCommand(ShowDebtsLists, true);
            ShowAllSpends = new RelayCommand(ShowSpendsLists, true);
        }

        public void ShowDebtsLists()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateToPage(new AllDebtsPage());
        }
        public void ShowSpendsLists()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateToPage(new AllSpendsPage());
        }
        public void SetTotalYear()
        {
            var result = App.Database.GetItemTotalYear();
            var totalYear = 0.0;
            if (result != null)
            {
                foreach (Item item in result)
                {
                    totalYear += item.Cost;
                }
            }
            SumYear = "Tento rok: " + totalYear.ToString() + " ,-";
        }
        public void SetTotalMonth()
        {
            var result = App.Database.GetItemTotalMonth();
            var totalMonth = 0.0;
            if (result != null)
            {
                foreach (Item item in result)
                {
                    totalMonth += item.Cost;
                }
            }

            SumMonth = "Tento měsíc: " + totalMonth.ToString() + " ,-";
        }
        public void CheckDebts()
        {
            var today = DateTime.Today;
            var result = App.Database.GetAllItemsSync();

            foreach (Item item in result)
            {
                var debt = App.Database.GetDebt(item.ID);
                if (debt != null)
                {
                    if (debt.NextDateToPay < today)
                    {
                        debt.RaiseCounter++;
                        debt.NextDateToPay = debt.NextDateToPay.AddMonths(1);

                        App.Database.SaveDebtSync(debt);
                    }
                }
            }
        }
    }
}
