using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mvvm_cvik
{
    class ClickCommand : ICommand
    {
        public MainWindowViewModel ViewModel;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.ClickCount++;
        }
        public ClickCommand(MainWindowViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
