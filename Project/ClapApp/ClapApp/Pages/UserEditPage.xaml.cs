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
using Windows.Foundation;
using Windows.Storage;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

namespace ClapApp.Pages
{
    public partial class UserEditPage : PhoneApplicationPage
    {
        PhotoChooserTask photoPicker;
        bool canFinish = true;
        public UserEditPage()
        {
            InitializeComponent();
            photoPicker = new PhotoChooserTask();
            photoPicker.Completed += new EventHandler<PhotoResult>(PhotoChooserTask_Completed);
        }

        // ---

        private void PhotoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage img = new BitmapImage();
                img.SetSource(e.ChosenPhoto);
                this.imgPerfil.Source = img;
                canFinish = true;
            }
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
            LayoutRoot.DataContext = Current.Usuario;
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

            if (canFinish)
                Current.FinishEditingUsuario();
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
            this.Focus();
            Current.SaveEditingUsuario();
            NavigationService.GoBack();
        }

        private void tglSobrenome_Checked(object sender, RoutedEventArgs e)
        {
            Current.Usuario.SobrenomePublico = (sender as ToggleButton).IsChecked ?? false;
        }

        private void tglNome_Checked(object sender, RoutedEventArgs e)
        {
            Current.Usuario.NomePublico = (sender as ToggleButton).IsChecked ?? false;
        }

        private void tglCidadeEstado_Checked(object sender, RoutedEventArgs e)
        {
            Current.Usuario.CidadeEstadoPublico = (sender as ToggleButton).IsChecked ?? false;
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            canFinish = false;
            this.photoPicker.Show();
        }

    }
}