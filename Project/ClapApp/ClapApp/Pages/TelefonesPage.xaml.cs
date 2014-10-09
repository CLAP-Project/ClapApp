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
using Microsoft.Phone.Tasks;

namespace ClapApp.Pages
{
    public partial class TelefonesPage : PhoneApplicationPage
    {
        ApplicationBarIconButton phoneButton, editButton;

        private void callTelephone(object sender, EventArgs e)
        {
            (new PhoneCallTask()
            {
                PhoneNumber = selected.Text
            }).Show();

            selected = null;
            phoneButton.IsEnabled = false;
        }

        public TelefonesPage()
        {
            InitializeComponent();
			
            phoneButton = PivotBar.MakeButton("phone.png", "telefonar", callTelephone);
			
            phoneButton.IsEnabled = false;

            editButton = PivotBar.MakeButton("edit.png", "editar", (object sender, EventArgs e) =>
            {
                Current.BeginEditingUsuario();
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
            LayoutRoot.DataContext = Current.Usuario;
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

            var tempSelected = sender as TextBlock;

            if (phoneButton.IsEnabled = (tempSelected != null && tempSelected != selected))
            {
                selected = tempSelected;

                prevBrush = selected.Foreground;
                selected.Foreground = new SolidColorBrush(App.ThemeColor);
            }
            else selected = null;
        }

        private void TextBlock_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            callTelephone(sender, e);
        }
    }
}