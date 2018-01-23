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

namespace test_23012018
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MovieDatabase _database;
        public static MovieDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    var fileHelper = new FileHelper();
                    _database = new MovieDatabase(fileHelper.GetLocalFilePath("MovieDB.db3"));
                }
                return _database;
            }
        }

        ObservableCollection<Movie> listMovie = new ObservableCollection<Movie>();
        string sort = "DESC";
        Movie edtMovie = null;
        public MainWindow()
        {
            InitializeComponent();

            /*Movie movie = new Movie();
            movie.Name = "Film 2";
            movie.Length = 130;
            movie.Director = "Jan Novák";
            movie.Rating = 7;*/

            //Database.SaveItemAsync(movie);

            /*foreach(Movie item in Database.GetAllItemsAsync().Result)
            {
                listMovie.Add(item);
            }

            movieList.ItemsSource = listMovie;*/

            SetListMovie();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NewMovieItem();

            SetListMovie();
        }

        private void SetListMovie()
        {
            movieList.ItemsSource = null;
            listMovie = new ObservableCollection<Movie>();

            foreach (Movie item in Database.GetItemsSortAsync(sort).Result)
            {
                listMovie.Add(item);
            }

            movieList.ItemsSource = listMovie;
        }
        private void btnAsc_Click(object sender, RoutedEventArgs e)
        {
            sort = "ASC";
            SortButton(1);
            SetListMovie();
        }
        private void btnDsc_Click(object sender, RoutedEventArgs e)
        {
            sort = "DESC";
            SortButton(0);
            SetListMovie();
        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (movieList.SelectedIndex != -1)
            {
                Database.DeleteItemAsync(listMovie[movieList.SelectedIndex]);
                listMovie.RemoveAt(movieList.SelectedIndex);
                txtName.Text = "";
                txtLength.Text = "";
                txtDirector.Text = "";
                txtRating.Text = "";
                SwitchButton(1);
                btnEdt.IsEnabled = false;
            }
        }
        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            SwitchButton(1);
            btnEdt.IsEnabled = false;
            NewMovieItem();
            SetListMovie();
        }
        private void btnEdt_Click(object sender, RoutedEventArgs e)
        {
            SwitchButton(0);
            edtMovie = listMovie[movieList.SelectedIndex];
            txtName.Text = edtMovie.Name;
            txtLength.Text = edtMovie.Length.ToString();
            txtDirector.Text = edtMovie.Director;
            txtRating.Text = edtMovie.Rating.ToString();
        }

        private void MovieSelected(object sender, SelectionChangedEventArgs e)
        {
            if (movieList.SelectedIndex != -1)
            {
                btnEdt.IsEnabled = true;
            }
        }
        private void NewMovieItem()
        {
            Movie newMovie;
            if (int.TryParse(txtRating.Text, out int rating) && int.TryParse(txtLength.Text, out int length) && txtName.Text != "" && txtDirector.Text != "")
            {
                if (rating > 10)
                {
                    rating = 10;
                }
                else if (rating < 0)
                {
                    rating = 0;
                }
            }
            else
            {
                lblInfo.Content = "Špatně zadané hodnoty!";
                return;
            }

            if (edtMovie != null)
            {
                edtMovie.Update(length, rating, txtName.Text, txtDirector.Text);
                newMovie = edtMovie;
                edtMovie = null;
            }
            else
            {
                newMovie = new Movie(length, rating, txtName.Text, txtDirector.Text);
            }

            txtName.Text = "";
            txtLength.Text = "";
            txtDirector.Text = "";
            txtRating.Text = "";

            Database.SaveItemAsync(newMovie);
            lblInfo.Content = "...";
        }

        private void SwitchButton(int i)
        {
            switch(i)
            {
                case 0:
                    btnAdd.Visibility = Visibility.Hidden;
                    btnUpd.Visibility = Visibility.Visible;
                    break;
                case 1:
                    btnAdd.Visibility = Visibility.Visible;
                    btnUpd.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
        }

        private void SortButton(int i)
        {
            switch (i)
            {
                case 0:
                    btnAsc.IsEnabled = true;
                    btnDsc.IsEnabled = false;
                    break;
                case 1:
                    btnAsc.IsEnabled = false;
                    btnDsc.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
