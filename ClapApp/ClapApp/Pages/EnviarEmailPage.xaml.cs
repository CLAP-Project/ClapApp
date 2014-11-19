using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClapApp.Control;
using ClapApp.Model;

namespace ClapApp.Pages
{
    public partial class EnviarEmailPage : PhoneApplicationPage
    {
        public EnviarEmailPage()
        {
            InitializeComponent();
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/EnviarEmailPage.xaml", UriKind.Relative);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = PerfisControl.GetCurrentUsuarioPerfil();
        }

        private void EnviarButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MessageBox.Show("E-mail enviado para " + (LayoutRoot.DataContext as Perfil).Email, "Sucesso", MessageBoxButton.OK);
            NavigationService.GoBack();
        }

        private void CancelarButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}