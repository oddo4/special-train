﻿using System;
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

namespace cviceni_20180220
{
    /// <summary>
    /// Interakční logika pro AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        public AddPage()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Item newItem = new Item();
            newItem.Name = txtName.Text;
            newItem.Cost = int.Parse(txtCost.Text);
            newItem.DateSpent = dpDate.SelectedDate.Value;

            App.Database.SaveItemAsync(newItem);
            NavigationService.GoBack();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
