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
using ClapApp.Model;
using ClapApp.View;

namespace ClapApp.Pages
{
    public partial class AnimalPivot : PhoneApplicationPage
    {
        private ApplicationBarIconButton[] _animalButtons, _localizacaoButtons, _historicoButtons, _coleiraButtons;
        private ApplicationBarIconButton[][] _buttons;

        public AnimalPivot()
        {
            InitializeComponent();

            _animalButtons = new ApplicationBarIconButton[]
            {
                BarButtons.MakeButton("edit.png", "editar", (object e, EventArgs args) =>
                {
                    AnimaisControl.BeginEditing(LayoutRoot.DataContext as Animal);
                    NavigationService.Navigate(AnimalEditPage.GetUri());
                })
            };

            _localizacaoButtons = new ApplicationBarIconButton[]
            {

            };

            _historicoButtons = new ApplicationBarIconButton[]
            {

            };

            _coleiraButtons = new ApplicationBarIconButton[]
            {

            };

            _buttons = new ApplicationBarIconButton[][]
            {
                _animalButtons,
                _localizacaoButtons,
                _historicoButtons,
                _coleiraButtons
            };
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/AnimalPivot.xaml", UriKind.Relative);
        }

        private void updateLayoutRoot()
        {
            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = AnimaisControl.GetCurrentAnimal();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            updateLayoutRoot();

            ApplicationBar.IsVisible = PerfisControl.IsCurrentUsuarioLoggedIn();
        }

        private void DonoButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PerfisControl.SetCurrentUsuario((LayoutRoot.DataContext as Animal).DonoId);
            NavigationService.Navigate(PerfilPivot.GetUri());
        }

        private void StatusButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var animal = LayoutRoot.DataContext as Animal;

            string message = (animal.Status == Status.OK) ?
                "Seu animal será considerado perdido e ficará visível para outros usuários em sua proximidade." :
                "Seu animal será considerado encontrado e não estará mais visível para outros usuários em sua proximidade.";

            var result = MessageBox.Show(message, "Mudança de status", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                animal.Status = (animal.Status == Status.OK) ? Status.Perdido : Status.OK;
                updateLayoutRoot();
            }
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationBar.AddButtons(_buttons[(sender as Pivot).SelectedIndex]);
        }

        private void GaleriaButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

    }
}