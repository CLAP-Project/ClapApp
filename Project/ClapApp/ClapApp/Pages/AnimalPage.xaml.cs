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
    public partial class AnimalPage : PhoneApplicationPage
    {
        ApplicationBarIconButton[] _perfilButtons, _localButtons, _fotosButtons, _rastrButtons;

        public AnimalPage()
        {
            InitializeComponent();

            // ---
            
            _perfilButtons = new ApplicationBarIconButton[] {
                PanoramaBar.MakeButton("status.png", "dono", (object sender, EventArgs e) =>
                {
                    NavigationService.Navigate(PerfilAnimaisPage.GetUri());
                }),
                PanoramaBar.MakeButton("edit.png", "editar", (object sender, EventArgs e) =>
                {
                    NavigationService.Navigate(AnimalDados.GetUri());
                }),
                PanoramaBar.MakeButton("delete.png", "deletar")
            };

            // ---

            _localButtons = new ApplicationBarIconButton[] {
                PanoramaBar.MakeButton("cog.png", "configurar")
            };

            // ---

            _fotosButtons = new ApplicationBarIconButton[] {
                PanoramaBar.MakeButton("add.png", "adicionar")
            };

            // ---

            _rastrButtons = new ApplicationBarIconButton[] {
                PanoramaBar.MakeButton("cog.png", "configurar")
            };

            // ---

            _updateButtons();
        }

        // ---

        public static Uri GetUri()
        {
            return new Uri("/Pages/AnimalPage.xaml", UriKind.Relative);
        }

        private void _addButons(ApplicationBarIconButton[] buttons, bool minimize = true)
        {
            this.ApplicationBar.AddButtons(buttons);
        }

        private void _updateButtons(int index)
        {
            switch (index)
            {
                case 0:
                    _addButons(_perfilButtons);
                    break;

                case 1:
                    _addButons(_fotosButtons);
                    break;

                case 2:
                    _addButons(_localButtons);
                    break;

                case 3:
                    _addButons(_rastrButtons);
                    break;

                default:
                    _addButons(null);
                    break;
            }
        }

        private void _updateButtons()
        {
            _updateButtons(Panorama.SelectedIndex);
        }

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _updateButtons();   
        }

        private void txtDescricao_GotFocus(object sender, RoutedEventArgs e)
        {
            ApplicationBar.IsVisible = false;
        }

        private void txtDescricao_LostFocus(object sender, RoutedEventArgs e)
        {
            ApplicationBar.IsVisible = true;
        }

        private void toggleGPS_Checked(object sender, RoutedEventArgs e)
        {
            toggleGPS.Content = "Ligado";
        }

        private void toggleGPS_Unchecked(object sender, RoutedEventArgs e)
        {
            toggleGPS.Content = "Desligado";
        }

        private void toggleLuz_Checked(object sender, RoutedEventArgs e)
        {
            toggleLuz.Content = "Ligada";
        }

        private void toggleLuz_Unchecked(object sender, RoutedEventArgs e)
        {
            toggleLuz.Content = "Desligada";
        }

        private void Panorama_Loaded(object sender, RoutedEventArgs e)
        {
            _updateButtons();
        }
    }
}