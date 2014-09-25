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
using System.Windows.Media;

namespace ClapApp.Pages
{
    public partial class TelefonesEditPage : PhoneApplicationPage
    {
        ApplicationBarIconButton deleteButton;

        public TelefonesEditPage()
        {
            InitializeComponent();

            deleteButton = PanoramaBar.MakeButton("delete.png", "remover");
            deleteButton.IsEnabled = false;

            ApplicationBar.Buttons.Add(deleteButton);
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/TelefonesEditPage.xaml", UriKind.Relative);
        }

        private void updateDataContext()
        {
            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = Editing.TempUsuario;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            updateDataContext();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            Editing.CancelTempUsuario();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnAddTelefone_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void tglTelefones_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Editing.TempUsuario.NumerosPublico = !Editing.TempUsuario.NumerosPublico;
            }
            catch (OcultandoNumerosEEmailsException ex)
            {
                MessageBox.Show("Você não pode ocultar seus números e e-mails ao mesmo tempo.", "Bloqueado", MessageBoxButton.OK);
            }
        }

        TextBlock selected = null;
        Brush prevBrush;

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (selected != null)
            {
                var temp = selected.Foreground;
                selected.Foreground = prevBrush;
                prevBrush = temp;
            }

            selected = sender as TextBlock;

            if (deleteButton.IsEnabled = selected != null)
            {
                prevBrush = selected.Foreground;
                selected.Foreground = new SolidColorBrush(Colors.Blue);
            }
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            Editing.SaveUsuario();
            NavigationService.Navigate(PerfilAnimaisPage.GetUri());
        }
    }
}