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
using Microsoft.Phone.Tasks;

namespace ClapApp.Pages
{
    public partial class PerfilPivot : PhoneApplicationPage
    {
        private ApplicationBarIconButton[][] _barButtons;
        private ApplicationBarIconButton[] _telefonesButtons, _animaisButtons, _perfilButtons;
        private ApplicationBarIconButton _editarTelefoneButton, _excluirTelefoneButton, _excluirAnimalButton;

        private int _selectedAnimalId = -1;

        private static bool _focusOnProfile = false;

        public static void FocusOnProfile()
        {
            _focusOnProfile = true;
        }

        public PerfilPivot()
        {
            InitializeComponent();
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/PerfilPivot.xaml", UriKind.Relative);
        }

        // ---

        private bool _loaded = false, _navigated = false, _indexSelected = false;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (_loaded)
            {
                updateLayoutRoot();
                updateAppBar();
            }

            _navigated = true;
        }

        private void updateAppBar()
        {
            ApplicationBar.IsVisible = PerfisControl.IsCurrentUsuarioLoggedIn();

            if (_focusOnProfile)
            {
                _focusOnProfile = false;
                Pivot.SelectedIndex = 1;
            }

            _excluirAnimalButton.IsEnabled = false;
            _selectedAnimalId = -1;
        }

        private void updateLayoutRoot()
        {
            _selectedTextBlock = null;

            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = PerfisControl.GetCurrentUsuarioPerfil();
        }

        private void updateButtons()
        {
            ApplicationBar.AddButtons(_barButtons[Pivot.SelectedIndex]);
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_loaded)
                updateButtons();

            _indexSelected = true;
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
            (new PhoneCallTask()
            {
                PhoneNumber = _selectedTextBlock.Text
            }).Show();
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

        private void AnimalButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var id = ((sender as StackPanel).DataContext as Animal).Id;

            if (id == _selectedAnimalId)
            {
                _excluirAnimalButton.IsEnabled = false;
                _selectedAnimalId = -1;
            }
            else
            {
                _excluirAnimalButton.IsEnabled = true;
                _selectedAnimalId = id;
            }

            StackPanel stackpanel = sender as StackPanel;
            ContextMenu contextMenu = ContextMenuService.GetContextMenu(stackpanel);
            if (contextMenu.Parent == null)
            {
                contextMenu.IsOpen = true;
            }
        }

        private void AnimalButton_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            AnimalButtonEvent.OnClick(this, sender, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            updateLayoutRoot();
            updateAppBar();
        }

        private void ExcluirAnimal_Action(object sender, EventArgs e)
        {
            AnimaisControl.EraseAnimalById(_selectedAnimalId);

            _excluirAnimalButton.IsEnabled = false;
            _selectedAnimalId = -1;

            updateLayoutRoot();
        }

        private void LayoutRoot_Loaded(object o, RoutedEventArgs _)
        {
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

            _excluirAnimalButton = BarButtons.MakeButton("delete.png", "excluir", ExcluirAnimal_Action);

            _excluirAnimalButton.IsEnabled = false;

            _animaisButtons = new ApplicationBarIconButton[]
            {
                BarButtons.MakeButton("add.png", "novo", (object sender, EventArgs e) =>
                {
                    AnimaisControl.BeginCreating(new Animal(), PerfisControl.GetLoggedUsuarioId());
                    NavigationService.Navigate(AnimalEditPage.GetUri());
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

            // ===

            _barButtons = new ApplicationBarIconButton[][]
            {
                _animaisButtons,
                _perfilButtons,
                _telefonesButtons
            };

            // ===

            _loaded = true;

            if (_navigated)
            {
                updateLayoutRoot();
                updateAppBar();
            }

            if (_indexSelected)
                updateButtons();
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void GestureListener_Tap(object sender, EventArgs e)
        {
           
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            StackPanel stackpanel = sender as StackPanel;
            ContextMenu contextMenu = ContextMenuService.GetContextMenu(stackpanel);

            (contextMenu.Items.ElementAt(0) as MenuItem).Tap += ExcluirAnimal_Action;
            (contextMenu.Items.ElementAt(1) as MenuItem).Tap += (object sender2, System.Windows.Input.GestureEventArgs ge) =>
            {
                AnimalButtonEvent.OnClick(this, sender, ge); //Aqui é utilizado o sender e não o sender2, pois o sender2 não virá como um Stackpanel.
            };
            
        }
    }
}