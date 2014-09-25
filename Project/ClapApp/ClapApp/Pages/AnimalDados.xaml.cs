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
    public partial class AnimalDados : PhoneApplicationPage
    {
        public AnimalDados()
        {
            InitializeComponent();
        }

        // ---

        public static Uri GetUri()
        {
            return new Uri("/Pages/AnimalDados.xaml", UriKind.Relative);
        }

        private bool canChangeSexo = false;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = Editing.TempAnimal;

            // Previne que, ao resetar da página, o sexo do animal seja revertido para Macho (o valor padrão do ListPicker).
            canChangeSexo = true;
            pkrSexo.SelectedIndex = (int)Editing.TempAnimal.Sexo;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Editing.CancelTempAnimal();
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
            Editing.SaveAnimal();
            NavigationService.GoBack();
        }

        private void pkrSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (canChangeSexo)
                Editing.TempAnimal.Sexo = Animal.ParseSexo(sender as ListPicker);
        }
    }
}