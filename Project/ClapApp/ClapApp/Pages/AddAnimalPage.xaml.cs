using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ClapApp.Pages
{
    public partial class AddAnimalPage : PhoneApplicationPage
    {
        public AddAnimalPage()
        {
            InitializeComponent();
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/AddAnimalPage.xaml", UriKind.Relative);
        }
    }
}