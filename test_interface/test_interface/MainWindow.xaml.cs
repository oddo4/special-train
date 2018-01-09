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

namespace test_interface
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Computer com = new Computer("Notebook 1", 100);
            lblBalanced.Content = com.GetEstimatedBatteryTime();
            com.BatteryConsumptionStrategy = new MaxPerformancePowerConsumptionStrategy();
            lblMax.Content = com.GetEstimatedBatteryTime();
            com.BatteryConsumptionStrategy = new SavingPowerConsumptionStrategy();
            lblSave.Content = com.GetEstimatedBatteryTime();
        }
    }
}
