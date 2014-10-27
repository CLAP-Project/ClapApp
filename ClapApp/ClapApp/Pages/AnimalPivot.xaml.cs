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
    public partial class AnimalPivot : PhoneApplicationPage
    {
        public AnimalPivot()
        {
            InitializeComponent();
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
        }

        private void DonoButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PerfisControl.SetCurrentUsuario((LayoutRoot.DataContext as Animal).DonoId);
            NavigationService.Navigate(PerfilPivot.GetUri());
        }

        private void StatusButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var animal = LayoutRoot.DataContext as Animal;
            animal.Status = (animal.Status == Status.OK) ? Status.Perdido : Status.OK;
            updateLayoutRoot();
        }

    }
}