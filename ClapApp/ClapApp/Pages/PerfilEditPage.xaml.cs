using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClapApp.Control;

namespace ClapApp.Pages
{
    public partial class PerfilEditPage : PhoneApplicationPage
    {
        public PerfilEditPage()
        {
            InitializeComponent();
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/PerfilEditPage.xaml", UriKind.Relative);
        }

        private void updateLayoutRoot()
        {
            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = PerfisControl.GetEditingPerfil();
        }

        private bool _loaded = false, _navigated = false;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (_loaded)
                updateLayoutRoot();

            _navigated = true;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            PerfisControl.FinishEditing();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!PerfisControl.IsCreating())
            {
                var senha = TxtNovaSenha.Password;

                if (!senha.Equals(""))
                {
                    var result = MessageBox.Show("Sua senha será alterada. Confere?", "Confirmação", MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        PerfisControl.GetEditingPerfil().Senha = senha;
                    }
                    else return;
                }
            }

            PerfisControl.SaveEditing();

            if (PerfisControl.IsCreating())
                MessageBox.Show("Conta cadastrada.", "Sucesso", MessageBoxButton.OK);

            NavigationService.GoBack();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnEsqueci_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Sua senha será enviada para " + PerfisControl.GetLoggedUsuarioPerfil().Email, "Confirmação", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
                MessageBox.Show("Senha enviada.");
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            _loaded = true;

            if (_navigated)
                updateLayoutRoot();
        }
    }
}