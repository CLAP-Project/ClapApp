﻿using System;
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

        private void navigateTelefones()
        {
            NavigationService.Navigate(TelefonesPage.GetUri());
        }

        public PerfilAnimaisPage()
        {
            InitializeComponent();

            // ---

            var phoneButton = PivotBar.MakeButton("phone.png", "telefones", (object sender, EventArgs e) =>
            {
                navigateTelefones();
            });

            var emailButton = PivotBar.MakeButton("email.png", "e-mails");

            // ---

            _perfilButtons = new ApplicationBarIconButton[] {
                emailButton, phoneButton,
                PivotBar.MakeButton("edit.png", "editar", (object sender, EventArgs e) =>
                {
                    Editing.SetTempUsuario();
                    NavigationService.Navigate(UserEditPage.GetUri());
                }),
                PivotBar.MakeButton("cog.png", "configurar", (object sender, EventArgs e) =>
                {
                    NavigationService.Navigate(UserConfigPage.GetUri());
                })
            };

            _animaisButtons = new ApplicationBarIconButton[] {
                PivotBar.MakeButton("add.png", "adicionar", (object sender, EventArgs e) =>
                {
                    Editing.SetTempUsuario();
                    NavigationService.Navigate(AddAnimalPage.GetUri());
                })
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

        void updateComponents()
        {
            Panorama.DataContext = null;
            Panorama.DataContext = Editing.Usuario;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            updateComponents();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Editing.Usuario.Emails.Add(Email.GetExemplo());
            Editing.Usuario.Numeros.Add(NumeroTelefonico.GetExemplo());

            Editing.Usuario.Animais.Add(new Animal()
            {
                Nome="Guido",
                Status=Status.Ok
            });

            updateComponents();
        }

        private void lstAnimais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lstAnimais_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateButtons();
        }

        private void Panorama_Loaded(object sender, RoutedEventArgs e)
        {
            updateButtons();
            updateComponents();
        }

        private void stkEmails_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void stkTelefones_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            navigateTelefones();
        }

        private void AnimalStackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var selected = lstAnimais.SelectedItem;
            var animalCast = selected as Animal;

            if (animalCast != null)
            {
                Editing.Animal = animalCast;
                NavigationService.Navigate(AnimalPivot.GetUri());
            }
            else MessageBox.Show("Isso não deveria aparecer... [animalCast == null; " + selected + ']', "Erro", MessageBoxButton.OK);
        }
    }
}