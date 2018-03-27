using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace formular.ViewModels
{
    class ViewModelFormPage : ViewModelBase
    {
        private Visibility sendNotice = Visibility.Hidden;

        public Visibility SendNotice
        {
            get
            {
                return sendNotice;
            }
            set
            {
                sendNotice = value;
                RaisePropertyChanged("SendNotice");
            }
        }
        private string firstName;

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                BoolFirstName = CheckText(FirstName);
                RaisePropertyChanged("FirstName");
            }
        }
        private bool boolFirstName = false;

        public bool BoolFirstName
        {
            get
            {
                return boolFirstName;
            }
            set
            {
                boolFirstName = value;
                RaisePropertyChanged("BoolFirstName");
            }
        }
        private string surName;

        public string SurName
        {
            get
            {
                return surName;
            }
            set
            {
                surName = value;
                BoolSurName = CheckText(SurName);
                RaisePropertyChanged("SurName");
            }
        }
        private bool boolSurName = false;

        public bool BoolSurName
        {
            get
            {
                return boolSurName;
            }
            set
            {
                boolSurName = value;
                RaisePropertyChanged("BoolSurName");
            }
        }

        private DateTime? date;

        public DateTime? Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                BoolDate = CheckDate(Date);
                RaisePropertyChanged("Date");
            }
        }
        private bool boolDate = false;

        public bool BoolDate
        {
            get
            {
                return boolDate;
            }
            set
            {
                boolDate = value;
                RaisePropertyChanged("BoolDate");
            }
        }
        private string email;

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                BoolEmail = CheckMail(Email);
                RaisePropertyChanged("Email");
            }
        }
        private bool boolEmail = false;

        public bool BoolEmail
        {
            get
            {
                return boolEmail;
            }
            set
            {
                boolEmail = value;
                RaisePropertyChanged("BoolEmail");
            }
        }

        private RelayCommand sendCommand;

        public RelayCommand SendCommand
        {
            get
            {
                return sendCommand;
            }
            set
            {
                sendCommand = value;
                RaisePropertyChanged("SendCommand");
            }
        }

        public ViewModelFormPage()
        {
            SendCommand = new RelayCommand(ValidateForm, true);
        }

        private void ValidateForm()
        {
            if (!BoolFirstName && !BoolSurName && !BoolDate && !BoolEmail)
            {
                SendNotice = Visibility.Visible;
            }
            else
            {
                BoolFirstName = CheckText(FirstName);
                BoolSurName = CheckText(SurName);
                BoolDate = CheckDate(Date);
                BoolEmail = CheckMail(Email);
            }
        }

        private bool CheckText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return true;
            }

            return false;
        }
        private bool CheckMail(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
            {
                return true;
            }
            else
            {
                try
                {
                    return !Regex.IsMatch(mail,
                          @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                          RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException)
                {
                    return true;
                }
            }
        }
        private bool CheckDate(DateTime? date)
        {
            if (date == null)
            {
                return true;
            }

            return false;
        }
    }
}
