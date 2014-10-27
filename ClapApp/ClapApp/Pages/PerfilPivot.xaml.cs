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
using ClapApp.View;
using ClapApp.Model;

namespace ClapApp.Pages
{
    public partial class PerfilPivot : PhoneApplicationPage
    {
        private ApplicationBarIconButton[] _telefonesButtons, _animaisButtons, _perfilButtons;
        private ApplicationBarIconButton _editarTelefoneButton, _excluirTelefoneButton, _excluirAnimalButton;

        public PerfilPivot()
        {
            InitializeComponent();

            // ---

            _editarTelefoneButton = BarButtons.MakeButton("edit.png", "editar", (object sender, EventArgs e) =>
            {
                NumerosControl.BeginEditing(NumerosControl.GetNumeroById(_selectedTelefone));
                NavigationService.Navigate(TelefoneEditPage.GetUri());
            });

            _editarTelefoneButton.IsEnabled = false;

            _excluirTelefoneButton = BarButtons.MakeButton("delete.png", "excluir", (object sender, EventArgs e) =>
            {
                NumerosControl.EraseNumeroById(_selectedTelefone);
                _excluirTelefoneButton.IsEnabled = _editarTelefoneButton.IsEnabled = false;
                updateLayoutRoot();
            });

            _excluirTelefoneButton.IsEnabled = false;

            _telefonesButtons = new ApplicationBarIconButton[]
            {
                BarButtons.MakeButton("add.png", "novo", (object sender, EventArgs e) =>
                {
                    NumerosControl.BeginCreating(new NumeroTelefonico(), (LayoutRoot.DataContext as Perfil).Id);
                    NavigationService.Navigate(TelefoneEditPage.GetUri());
                }),
                _editarTelefoneButton,
                _excluirTelefoneButton
            };

            // ---

            _excluirAnimalButton = BarButtons.MakeButton("delete.png", "excluir", (object sender, EventArgs e) =>
            {
            });

            _excluirAnimalButton.IsEnabled = false;

            _animaisButtons = new ApplicationBarIconButton[]
            {
                BarButtons.MakeButton("add.png", "novo", (object sender, EventArgs e) =>
                {
                    // ...
                }),
                _excluirAnimalButton
            };

            // ---

            _perfilButtons = new ApplicationBarIconButton[]
            {
                BarButtons.MakeButton("edit.png", "editar", (object sender, EventArgs e) =>
                {
                    PerfisControl.BeginEditing();
                    NavigationService.Navigate(PerfilEditPage.GetUri());
                })
            };
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/PerfilPivot.xaml", UriKind.Relative);
        }

        // ---

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            updateLayoutRoot();

            ApplicationBar.IsVisible = PerfisControl.IsCurrentUsuarioLoggedIn();
        }

        private void updateLayoutRoot()
        {
            _selectedTextBlock = null;

            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = PerfisControl.GetCurrentUsuarioPerfil();
        }

        private void AnimalButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            AnimalButtonEvent.OnClick(sender, e);
        }

        private void updateButtons()
        {
            switch (Pivot.SelectedIndex)
            {
                case 0:
                    ApplicationBar.AddButtons(_animaisButtons);
                    break;

                case 1:
                    ApplicationBar.AddButtons(_perfilButtons);
                    break;

                case 2:
                    ApplicationBar.AddButtons(_telefonesButtons);
                    break;

                default:
                    break;
            }
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateButtons();
        }

        private int _selectedTelefone = -1;
        private TextBlock _selectedTextBlock = null;

        private void TelefoneText_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var telefone = textBlock.DataContext as NumeroTelefonico;

            if (_editarTelefoneButton.IsEnabled = _excluirTelefoneButton.IsEnabled = (telefone.Id != _selectedTelefone))
            {
                if (_selectedTextBlock != null)
                    _selectedTextBlock.Foreground = NormalBlock.Foreground;

                textBlock.Foreground = SelectedBlock.Foreground;

                _selectedTelefone = telefone.Id;
                _selectedTextBlock = textBlock;
            }
            else
            {
                textBlock.Foreground = NormalBlock.Foreground;

                _selectedTelefone = -1;
                _selectedTextBlock = null;
            }
        }

        private void TelefoneText_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void TelefoneText_Loaded(object sender, RoutedEventArgs e)
        {
            var textBlock = sender as TextBlock;

            if ((textBlock.DataContext as NumeroTelefonico).Id == _selectedTelefone)
            {
                textBlock.Foreground = SelectedBlock.Foreground;
                _selectedTextBlock = textBlock;
            }
            else textBlock.Foreground = NormalBlock.Foreground;
        }
    }
}