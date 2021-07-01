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

        public UserManagementPage()
        {
            InitializeComponent();
            this.DataContext = this.viewModel;

            Messenger.Default.Send(this.Resources["users"] as CollectionViewSource);
        }
    }
}
