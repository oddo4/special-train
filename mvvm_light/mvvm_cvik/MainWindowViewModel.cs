using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvvm_cvik
{
    class MainWindowViewModel : ViewModelBase
    {
        public CooldownTimer timer;
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
                CheckLimit();
                RaisePropertyChanged("ClickCount");
            }
        }
        private string buttonContent = "Click!";

        public string ButtonContent
        {
            get
            {
                return buttonContent;
            }
            set
            {
                buttonContent = value;
                RaisePropertyChanged("ButtonContent");
            }
        }
        private string info = "Game starts at 10!";

        public string Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                RaisePropertyChanged("Info");
            }
        }
        private bool enabled = true;

        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
                RaisePropertyChanged("Enabled");
            }
        }


        private RelayCommand clickCommand;

        public RelayCommand ClickCommand
        {
            get
            {
                return clickCommand;
            }
            set
            {
                clickCommand = value;
                RaisePropertyChanged("ClickCommand");
            }
        }

        public MainWindowViewModel()
        {
            ClickCommand = new RelayCommand(AddCount, true);
            timer = new CooldownTimer(this);
        }

        public void AddCount()
        {
            ClickCount++;
        }
        private void CheckLimit()
        {
            if (ClickCount >= 10)
            {
                Enabled = false;
                Info = "Game started!";
                ButtonContent = "Stop!";
                timer.TimerStart();
            }
            else
            {
                Enabled = true;
                ButtonContent = "Click!";
            }
        }
    }
}
