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

namespace ClapApp.Pages
{
    public partial class AnimalEditPage : PhoneApplicationPage
    {
        public AnimalEditPage()
        {
            InitializeComponent();
        }

        private bool _canChangeSexo = false;

        public static Uri GetUri()
        {
            return new Uri("/Pages/AnimalEditPage.xaml", UriKind.Relative);
        }

        private void updateLayoutRoot()
        {
            var animal = AnimaisControl.GetEditingAnimal();

            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = animal;

            SexoPicker.SelectedIndex = (int)animal.Sexo;

            _canChangeSexo = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            updateLayoutRoot();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _canChangeSexo = false;
            base.OnNavigatedFrom(e);
            AnimaisControl.FinishEditing();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (AnimaisControl.IsCreating())
            {
                MessageBox.Show("Animal cadastrado.", "Sucesso", MessageBoxButton.OK);
            }

            AnimaisControl.SaveEditing();
            NavigationService.GoBack();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SexoPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_canChangeSexo) return;

            var animal = AnimaisControl.GetEditingAnimal();

            switch (((sender as ListPicker).SelectedItem as ListPickerItem).Content.ToString())
            {
                case "Macho":
                    animal.Sexo = Sexo.Macho;
                    break;

                case "Fêmea":
                    animal.Sexo = Sexo.Femea;
                    break;

                case "Indefinido":
                    animal.Sexo = Sexo.Indefinido;
                    break;

                default:
                    break;
            }
        }
    }
}