using CeseatUserManagement.UserManagementSpace;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;

namespace CeseatUserManagement
{
    /// <summary>
    /// Logique d'interaction pour UserManagementPage.xaml
    /// </summary>
    public partial class UserManagementPage : Page
    {
        private UserManagementViewModel viewModel = new UserManagementViewModel();

        private string searchPlaceHolder = "Recherche...";

        public UserManagementPage()
        {
            InitializeComponent();
            this.DataContext = this.viewModel;

            Messenger.Default.Send(this.Resources["users"] as CollectionViewSource);
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox seachBox = sender as TextBox;
            if (seachBox.Text != this.searchPlaceHolder)
            {
                this.UsersTable.Items.Filter = new Predicate<object>(user => (user as IUser).LastName.ToUpper().Contains((sender as TextBox).Text.ToUpper()));
            }
        }

        private void Search_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox.Text == this.searchPlaceHolder)
            {
                searchBox.Clear();
            }
        }

        private void Search_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(searchBox.Text))
            {
                searchBox.Text = this.searchPlaceHolder;
            }
        }

        private void typeFilter(object sender, FilterEventArgs e)
        {

        }
    }
}
