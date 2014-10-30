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
            ViewAnimalProfile(page, ((sender as StackPanel).DataContext as Animal).Id);
        }

        public static void ViewAnimalProfile(Page page, int id)
        {
            AnimaisControl.SetCurrentAnimal(id);
            page.NavigationService.Navigate(AnimalPivot.GetUri());
        }
    }
}
