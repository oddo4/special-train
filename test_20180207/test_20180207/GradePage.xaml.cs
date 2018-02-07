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
    /// Interakční logika pro GradePage.xaml
    /// </summary>
    public partial class GradePage : Page
    {
        List<Predmet> listPredmet = new List<Predmet>();
        ObservableCollection<Znamka> listZnamka = new ObservableCollection<Znamka>();
        List<string> listPredmetNames = new List<string>();
        Student student;
        public GradePage()
        {
            InitializeComponent();

            CreateListPredmet();
            cBoxSubject.ItemsSource = listPredmetNames;
        }
        public GradePage(Student s)
        {
            InitializeComponent();
            student = s;
            CreateListPredmet();
            CreateListZnamka();
            lViewGrades.ItemsSource = listZnamka;
            cBoxSubject.ItemsSource = listPredmetNames;
        }
        private void CreateListPredmet()
        {
            var result = App.Databaze.GetPredmetAsync().Result;
            listPredmet = result;

            foreach (Predmet p in result)
            {
                listPredmetNames.Add(p.Nazev);
            }
        }
        private void CreateListZnamka()
        {
            var result = App.Databaze.GetZnamkyAsync(student.ID).Result;

            foreach (Znamka z in result)
            {
                listZnamka.Add(z);
            }
        }
        private void btnAddGrade_Click(object sender, RoutedEventArgs e)
        {
            Znamka z = new Znamka();
            z.IDStudent = student.ID;
            z.IDPredmet = listPredmet[cBoxSubject.SelectedIndex].ID;
            z.Hodnota = int.Parse(cBoxGrade.SelectionBoxItem.ToString());

            App.Databaze.SaveZnamkaAsync(z);
            listZnamka.Add(z);
            cBoxGrade.SelectedItem = null;
            cBoxSubject.SelectedItem = null;
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
