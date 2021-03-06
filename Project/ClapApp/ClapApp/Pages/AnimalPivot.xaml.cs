﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Maps.Toolkit;
using ClapApp.Model;

//Localização
using System.Device.Location; // Provides the GeoCoordinate class.
using Windows.Devices.Geolocation; //Provides the Geocoordinate class.
using System.Windows.Media;
using System.Windows.Shapes;
using ShowMyLocationOnMap;
using PhotoHubSample.ViewModels;

namespace ClapApp.Pages
{
    public partial class AnimalPivot : PhoneApplicationPage
    {
        ApplicationBarIconButton[] _perfilButtons, _localButtons, _fotosButtons, _rastrButtons;

        public AnimalPivot()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(AnimalGaleriaPage_Loaded);

            _perfilButtons = new ApplicationBarIconButton[] {
                PivotBar.MakeButton("status.png", "dono", (object sender, EventArgs e) =>
                {
                    NavigationService.Navigate(PerfilPivot.GetUri());
                }),
                PivotBar.MakeButton("edit.png", "editar", (object sender, EventArgs e) =>
                {
                    Current.BeginEditingAnimal();
                    NavigationService.Navigate(AnimalEditPage.GetUri());
                }),
                PivotBar.MakeButton("delete.png", "deletar")
            };

            // ---

            _localButtons = new ApplicationBarIconButton[] {
                PivotBar.MakeButton("cog.png", "configurar"),
                PivotBar.MakeButton("sync.png", "atualizar", ShowAnimalLocationOnTheMap)
            };

            // ---

            _fotosButtons = new ApplicationBarIconButton[] {
                PivotBar.MakeButton("add.png", "adicionar")
            };

            // ---

            _rastrButtons = new ApplicationBarIconButton[] {
                PivotBar.MakeButton("cog.png", "configurar")
            };

            // ---

            //_updateButtons();

            ShowAnimalLocationOnTheMap(null, null);
        }

        // ---

        private void AnimalGaleriaPage_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = new PhotosViewModel();
            DataContext = viewModel;
        }

        // ---

        private void updateDataContext()
        {
            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = Current.Animal;
        }

        // ---
        //Código para o dia 26/09/2014

        /*private async void LocateMe(object sender, EventArgs e)
        {

            Geolocator simulatedLocation = new Geolocator();
            Geoposition myGeoPosition = null;
            simulatedLocation.DesiredAccuracyInMeters = 5;

            try
            {
                myGeoPosition = await simulatedLocation.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Você não pode ocultar seus números e e-mails ao mesmo tempo.", "Bloqueado", MessageBoxButton.OK);
            }

            this.mapaLocalizacao.Center = new GeoCoordinate(myGeoPosition.Coordinate.Latitude, myGeoPosition.Coordinate.Longitude);
            this.mapaLocalizacao.ZoomLevel = 15;
        }*/

        private Ellipse createMarker()
        {
            return new Ellipse()
            {
                Fill = new SolidColorBrush(Colors.Blue),
                Height = 20,
                Width = 20,
                Opacity = 50
            };
        }

        // --- 
        //Código a ser usado futuramente...
        private async void ShowAnimalLocationOnTheMap(object sender, EventArgs e)
        {
            Geolocator myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            GeoCoordinate myGeoCoordinate =
            CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);

            this.mapaLocalizacao.Center = myGeoCoordinate;
            this.mapaLocalizacao.ZoomLevel = 13;

            //Círculo de marcação no mapa


            //Criando camada para conter a marcação
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = createMarker();
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = myGeoCoordinate;

            //Atribuição para localização
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);

            //Adição da camada no mapa
            this.mapaLocalizacao.Layers.Add(myLocationLayer);
        }

        // ---

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            updateDataContext();
        }

        // ---

        public static Uri GetUri()
        {
            return new Uri("/Pages/AnimalPivot.xaml", UriKind.Relative);
        }

        private void _addButons(ApplicationBarIconButton[] buttons, bool minimize = true)
        {
            this.ApplicationBar.AddButtons(buttons);
        }

        private void _updateButtons(int index)
        {
            switch (index)
            {
                case 0:
                    _addButons(_perfilButtons);
                    break;

                case 1:
                    _addButons(_fotosButtons);
                    break;

                case 2:
                    _addButons(_localButtons);
                    break;

                case 3:
                    _addButons(_rastrButtons);
                    break;

                default:
                    _addButons(null);
                    break;
            }
        }

        private void _updateButtons()
        {
            _updateButtons(this.Pivot.SelectedIndex);
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _updateButtons();
        }

        private void txtDescricao_GotFocus(object sender, RoutedEventArgs e)
        {
            ApplicationBar.IsVisible = false;
        }

        private void txtDescricao_LostFocus(object sender, RoutedEventArgs e)
        {
            ApplicationBar.IsVisible = true;
        }

        private void tglStatus_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        //private void 

        private void toggleGPS_Checked(object sender, RoutedEventArgs e)
        {
            toggleGPS.Content = "Ligado";
        }

        private void toggleGPS_Unchecked(object sender, RoutedEventArgs e)
        {
            toggleGPS.Content = "Desligado";
        }

        private void toggleLuz_Checked(object sender, RoutedEventArgs e)
        {
            toggleLuz.Content = "Ligada";
        }

        private void toggleLuz_Unchecked(object sender, RoutedEventArgs e)
        {
            toggleLuz.Content = "Desligada";
        }

        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {
            _updateButtons();
        }

        private void btnGaleria_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(AnimalGaleriaPage.GetUri());
        }

        private void stkStatus_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Current.Animal.ToggleStatus();
            updateDataContext();
        }
    }
}