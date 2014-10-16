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
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Input;

namespace ClapApp.Pages
{
    public partial class AnimalEditPage : PhoneApplicationPage
    {
        public AnimalEditPage()
        {
            InitializeComponent();
        }

        // ---

        public static Uri GetUri()
        {
            return new Uri("/Pages/AnimalEditPage.xaml", UriKind.Relative);
        }

        private bool canChangeSexo = false;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = Current.Animal;

            // Previne que, ao resetar da página, o sexo do animal seja revertido para Macho (o valor padrão do ListPicker).
            canChangeSexo = true;
            pkrSexo.SelectedIndex = (int)Current.Animal.Sexo;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            StaticMethods.TextBox_TextChanged(sender, e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Current.FinishEditingAnimal();
            canChangeSexo = false;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        /*private void pkrSexo_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Editing.TempAnimal.Sexo = Animal.ParseSexo(sender as ListPicker);
        }*/
        
        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            Current.SaveEditingAnimal();
            NavigationService.GoBack();
        }

        private void pkrSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (canChangeSexo)
                Current.Animal.Sexo = Animal.ParseSexo(sender as ListPicker);
        }
    }
}