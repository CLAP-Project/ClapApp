using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClapApp.Model;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ClapApp.Pages
{
    public partial class UserEditPage : PhoneApplicationPage
    {
        public UserEditPage()
        {
            InitializeComponent();
        }

        // ---

        public static Uri GetUri()
        {
            return new Uri("/Pages/UserEditPage.xaml", UriKind.Relative);
        }

        // ---

        private void updateDataContext()
        {
            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = Editing.TempUsuario;
        }

        // ---

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            updateDataContext();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Editing.CancelTempUsuario();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnEditarEmails_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(EmailsEditPage.GetUri());
        }

        private void btnEditarTelefones_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(TelefonesEditPage.GetUri());
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            updateDataContext();
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            Editing.SaveUsuario();
            NavigationService.GoBack();
        }

        private void tglSobrenome_Checked(object sender, RoutedEventArgs e)
        {
            Editing.TempUsuario.SobrenomePublico = (sender as ToggleButton).IsChecked ?? false;
        }

        private void tglNome_Checked(object sender, RoutedEventArgs e)
        {
            Editing.TempUsuario.NomePublico = (sender as ToggleButton).IsChecked ?? false;
        }

        private void tglCidadeEstado_Checked(object sender, RoutedEventArgs e)
        {
            Editing.TempUsuario.CidadeEstadoPublico = (sender as ToggleButton).IsChecked ?? false;
        }
    }
}