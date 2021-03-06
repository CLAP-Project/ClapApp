﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClapApp.Resources;
using ClapApp.Pages;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClapApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PerfilPivot.GetUri());
        }

        private void btnVisitante_Click(object sender, RoutedEventArgs e)
        {
            double f = 5.43456;
            MessageBox.Show(f.ToString("0.00"));
            //NavigationService.Navigate(FotoQRCodePage2.GetUri());
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}