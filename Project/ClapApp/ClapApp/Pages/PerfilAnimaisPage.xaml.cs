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

namespace ClapApp.Pages
{
    public partial class PerfilAnimaisPage : PhoneApplicationPage
    {
        ApplicationBarIconButton[] _perfilButtons, _animaisButtons;

        public PerfilAnimaisPage()
        {
            InitializeComponent();

            // ---

            _perfilButtons = new ApplicationBarIconButton[] {
                PanoramaBar.MakeButton("edit.png", "editar"),
                PanoramaBar.MakeButton("cog.png", "configurar")
            };

            _animaisButtons = new ApplicationBarIconButton[] {
                PanoramaBar.MakeButton("add.png", "adicionar"),
                PanoramaBar.MakeButton("delete.png", "deletar")
            };

            // ---

            updateComponents();
        }

        // ---

        public static Uri GetUri()
        {
            return new Uri("/Pages/PerfilAnimaisPage.xaml", UriKind.Relative);
        }

        // ---

        private void addButtons(ApplicationBarIconButton[] buttons)
        {
            ApplicationBar.AddButtons(buttons);
        }

        private void updateButtons(int index)
        {
            switch (index)
            {
                case 0:
                    addButtons(_perfilButtons);
                    break;

                case 1:
                    addButtons(_animaisButtons);
                    break;

                default:
                    addButtons(null);
                    break;
            }
        }

        private void updateButtons()
        {
            updateButtons(Panorama.SelectedIndex);
        }

        // ---

        Perfil editingPerfil = Perfil.GetExemplo();

        void updateComponents()
        {
            Panorama.DataContext = null;
            Panorama.DataContext = editingPerfil;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            editingPerfil.Emails.Add(Email.GetExemplo());
            editingPerfil.Numeros.Add(NumeroTelefonico.GetExemplo());

            editingPerfil.Animais.Add(new Animal()
            {
                Nome="Guido",
                Status=Status.Ok
            });

            updateComponents();
        }

        private void lstAnimais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(AnimalPage.GetUri());
        }

        private void lstAnimais_GotFocus(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(AnimalPage.GetUri());
        }

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateButtons();
        }

        private void Panorama_Loaded(object sender, RoutedEventArgs e)
        {
            updateButtons();
        }
    }
}