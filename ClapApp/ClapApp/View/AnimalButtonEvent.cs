using ClapApp.Control;
using ClapApp.Model;
using ClapApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClapApp.View
{
    public static class AnimalButtonEvent
    {
        public static void OnClick(Page page, object sender, System.Windows.Input.GestureEventArgs e)
        {
            var animal = (sender as StackPanel).DataContext as Animal;

            AnimaisControl.SetCurrentAnimal(animal.Id);
            page.NavigationService.Navigate(AnimalPivot.GetUri());
        }
    }
}
