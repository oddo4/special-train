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

namespace test_20180207
{
    /// <summary>
    /// Interakční logika pro StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {
        Student student;
        public StudentPage()
        {
            InitializeComponent();
            student = new Student();
        }
        public StudentPage(Student s)
        {
            InitializeComponent();
            student = s;
            txtName.Text = s.Jmeno;
            txtClass.Text = s.NazevTridy;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            student.Jmeno = txtName.Text;
            var tridy = App.Databaze.FindTridaAsync(txtClass.Text.ToUpper()).Result;
            if (tridy.Any())
            {
                student.IDTrida = tridy[0].ID;
            }
            else
            {
                Trida trida = new Trida();
                trida.Nazev = txtClass.Text.ToUpper();
                App.Databaze.SaveTridaAsync(trida);

                var result = App.Databaze.GetTridaId(trida.Nazev).Result;

                student.IDTrida = result.ID;
            }

            App.Databaze.SaveStudentAsync(student);

            NavigationService.GoBack();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
