using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClapApp.Models;
using System.Collections.ObjectModel;

namespace ClapApp.Pages
{
    public partial class MeuPerfilPage : PhoneApplicationPage
    {
        public MeuPerfilPage()
        {
            InitializeComponent();
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/MeuPerfilPage.xaml", UriKind.Relative);
        }

        Perfil teste = new Perfil();

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            teste.Numeros.AddRange(new Perfil.NumeroTelefonico[]{
                    new Perfil.NumeroTelefonico(){ DDD="92", Numero="8266-6492" },
                    new Perfil.NumeroTelefonico(){ DDD="92", Numero="9266-6492" }
                });

            lstNumeros.ItemsSource = teste.Numeros;
        }

        private void btnVerAnimais_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(AnimaisPage.GetUri());
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            teste.Numeros.Add(new Perfil.NumeroTelefonico() { DDD = "92", Numero = "8266-6492" });
            lstNumeros.ItemsSource = new List<Perfil.NumeroTelefonico>(teste.Numeros);

            teste.Emails.Add(new Perfil.Email() { Value = "mechamutoh@gmail.com" });
            lstEmails.ItemsSource = new List<Perfil.Email>(teste.Emails);
        }
    }
}