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
        public PerfilAnimaisPage()
        {
            InitializeComponent();
            updateComponents();
        }

        // ---

        public static Uri GetUri()
        {
            return new Uri("/Pages/PerfilAnimaisPage.xaml", UriKind.Relative);
        }

        // ---

        Perfil editingPerfil = Perfil.GetExemplo();

        void updateComponents()
        {
            stkPerfil.DataContext = null;
            stkPerfil.DataContext = editingPerfil;

            stkAnimais.DataContext = null;
            stkAnimais.DataContext = editingPerfil;
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
    }
}