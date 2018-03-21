using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvvm_cvik
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private int clickCount;

        public int ClickCount
        {
            get
            {
                return clickCount;
            }
            set
            {
                clickCount = value;
                notifyPropertyChanged("ClickCount");
            }
        }

        private ClickCommand clickCommand;

        public ClickCommand ClickCommand
        {
            get
            {
                return clickCommand;
            }
            set
            {
                clickCommand = value;
                notifyPropertyChanged("ClickCommand");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void notifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public MainWindowViewModel()
        {
            ClickCommand = new ClickCommand(this);
        }
    }
}
