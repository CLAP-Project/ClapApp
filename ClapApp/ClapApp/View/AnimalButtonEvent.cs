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
        public static void OnClick(object sender, System.Windows.Input.GestureEventArgs e)
        {
            object test = (sender as StackPanel).DataContext;

            if (test != null)
                MessageBox.Show(test.ToString());
        }
    }
}
