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

namespace test_20180207
{
    /// <summary>
    /// Interakční logika pro MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        ObservableCollection<Student> list = new ObservableCollection<Student>();
        public MainPage()
        {
            InitializeComponent();

            //App.Databaze.DeleteTables();

            CreateList();
            CreateSubjects();
            lViewStudents.ItemsSource = list;
        }

        private void CreateList()
        {
            var result = App.Databaze.GetStudentAsync().Result;

            foreach (Student s in result)
            {
                list.Add(s);
            }
        }
        private void CreateSubjects()
        {
            var result = App.Databaze.GetPredmetAsync().Result;
            if (!result.Any())
            {
                List<string> l = new List<string>() { "Matematika", "Český jazyk", "Anglický jazyk", "Německý jazyk", "Zeměpis", "Přírodopis", "Chemie" };

                for (int i = 0; i < l.Count; i++)
                {
                    Predmet p = new Predmet();
                    p.Nazev = l[i];

                    App.Databaze.SavePredmetAsync(p);
                }
            }
        }
        private void btnAddGrades_Click(object sender, RoutedEventArgs e)
        {
            if (lViewStudents.SelectedIndex != -1)
            {
                NavigationService.Navigate(new GradePage(list[lViewStudents.SelectedIndex]));
            }
        }

        private void btnEditStudent_Click(object sender, RoutedEventArgs e)
        {
            if (lViewStudents.SelectedIndex != -1)
            {
                NavigationService.Navigate(new StudentPage(list[lViewStudents.SelectedIndex]));
            }
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudentPage());
        }

        private void btnDelStudent_Click(object sender, RoutedEventArgs e)
        {
            if (lViewStudents.SelectedIndex != -1)
            {
                var student = list[lViewStudents.SelectedIndex];
                list.RemoveAt(lViewStudents.SelectedIndex);
                App.Databaze.DeleteStudent(student.ID);
            }
        }
    }
}
