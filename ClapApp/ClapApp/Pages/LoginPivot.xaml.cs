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
using ClapApp.Control;
using ClapApp.View;

namespace ClapApp.Pages
{
    public partial class LoginPivot : PhoneApplicationPage
    {
        public LoginPivot()
        {
            InitializeComponent();
            PerfisControl.PopulateExamples();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            updateDataContext();
        }

        private void updateDataContext()
        {
            AnimaisListBox.ItemsSource = null;
            AnimaisListBox.ItemsSource = AnimaisControl.GetAllAnimaisPerdidos();
        }

        private void AnimalButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            AnimalButtonEvent.OnClick(this, sender, e);
        }

        private void login(string email, string senha)
        {
            if (PerfisControl.Login(email, senha))
            {
                PerfisControl.SetCurrentUsuario(PerfisControl.GetLoggedUsuarioId());
                NavigationService.Navigate(PerfilPivot.GetUri());
            }
            else MessageBox.Show("E-mail ou senha incorretos.", "Falha de login", MessageBoxButton.OK);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = TxtEmail.Text;

            if (email.Equals(""))
            {
                login("mox@a.com", "mox");
            }
            else login(email, TxtSenha.Password);
        }

        private void BtnCriarPerfil_Click(object sender, RoutedEventArgs e)
        {
            PerfisControl.BeginCreating(new Perfil());
            NavigationService.Navigate(PerfilEditPage.GetUri());
        }
    }
}