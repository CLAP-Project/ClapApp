using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhotoHubSample.ViewModels;
using System.Windows.Media;

namespace ClapApp.Pages
{
    public partial class AnimalGaleriaPage : PhoneApplicationPage
    {
        public AnimalGaleriaPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(AnimalGaleriaPage_Loaded);

            this.LayoutRoot.Background = new SolidColorBrush(App.BackgroundColor);
        }

        private void AnimalGaleriaPage_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = new PhotosViewModel();
            DataContext = viewModel;

            PhotoHubLLS.ScrollTo(PhotoHubLLS.ItemsSource[1]);
        }

        internal static Uri GetUri()
        {
            return new Uri("/Pages/AnimalGaleriaPage.xaml", UriKind.Relative);
        }
    }
}