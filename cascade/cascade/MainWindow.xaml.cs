using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace cascade
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /*MailManager mailManager = new MailManager()
                .From("from@mail.com")
                .To("to@mail.com")
                .Subject("Subject")
                .Body("Sample body of the email.");

            mailManager.Send(mailManager);*/

            MailManager.Send(
                (mail) => mail
                .From("from@mail.com")
                .To("to@mail.com")
                .Subject("Subject")
                .Body("Sample body of the email.")
                );
        }
    }
}
