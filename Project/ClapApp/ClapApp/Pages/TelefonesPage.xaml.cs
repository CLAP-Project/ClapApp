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
using ClapApp.Model;

namespace ClapApp.Pages
{
    public partial class TelefonesPage : PhoneApplicationPage
    {
        ApplicationBarIconButton phoneButton, editButton;

        public TelefonesPage()
        {
            InitializeComponent();

            phoneButton = PanoramaBar.MakeButton("phone.png", "telefonar");
            phoneButton.IsEnabled = false;

            editButton = PanoramaBar.MakeButton("edit.png", "editar", (object sender, EventArgs e) =>
            {
                Editing.SetTempUsuario();
                NavigationService.Navigate(TelefonesEditPage.GetUri());
            });

            ApplicationBar.AddButtons(new ApplicationBarIconButton[] { phoneButton, editButton });
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/TelefonesPage.xaml", UriKind.Relative);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = Editing.Usuario;
        }

        TextBlock selected = null;
        Brush prevBrush;

        private void lstNumeros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lstNumeros_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (selected != null)
            {
                var temp = selected.Foreground;
                selected.Foreground = prevBrush;
                prevBrush = temp;
            }

            selected = sender as TextBlock;

            if (phoneButton.IsEnabled = selected != null)
            {
                prevBrush = selected.Foreground;
                selected.Foreground = new SolidColorBrush(Colors.Blue);
            }
        }
    }
}