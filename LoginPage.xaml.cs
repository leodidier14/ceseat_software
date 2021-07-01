﻿using CeseatUserManagement.LoginSpace;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CeseatUserManagement
{
    /// <summary>
    /// Logique d'interaction pour LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private LoginViewModel viewModel;
        public LoginPage()
        {
            InitializeComponent();
            this.viewModel = new LoginViewModel();
            this.DataContext = this.viewModel;
        }

        private void login(object sender, RoutedEventArgs e)
        {
            Boolean isConnected = this.viewModel.Login(this.email.Text, this.password.Password).Result;
            if (isConnected)
            {
                this.NavigationService.Navigate(new Uri("UserManagementPage.xaml", UriKind.Relative));
            }
        }
    }
}
