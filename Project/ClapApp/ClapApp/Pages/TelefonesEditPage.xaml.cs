using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ClapApp.Pages
{
    public partial class TelefonesEditPage : PhoneApplicationPage
    {
        public TelefonesEditPage()
        {
            InitializeComponent();
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/TelefonesEditPage.xaml", UriKind.Relative);
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnAddTelefone_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}