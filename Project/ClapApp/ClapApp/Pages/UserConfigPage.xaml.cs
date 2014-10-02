using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace ClapApp.Pages
{
    public partial class UserConfigPage : PhoneApplicationPage
    {
        public UserConfigPage()
        {
            InitializeComponent();
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/UserConfigPage.xaml", UriKind.Relative);
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnRecuperarSenha_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sua senha será enviada para mechamutoh@gmail.com", "Confirmar", MessageBoxButton.OKCancel);
        }
    }
}