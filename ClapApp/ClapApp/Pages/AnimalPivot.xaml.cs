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
using ClapApp.View;
using Windows.Devices.Geolocation;
using System.Device.Location;
using ShowMyLocationOnMap;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Maps.Services;

namespace ClapApp.Pages
{
    public partial class AnimalPivot : PhoneApplicationPage
    {
        private ApplicationBarIconButton[] _animalButtons, _localizacaoButtons, _historicoButtons, _coleiraButtons;
        private ApplicationBarIconButton[][] _buttons;

        private RouteQuery RotaQuery = null;
        private TravelMode _travelMode = TravelMode.Walking;

        public AnimalPivot()
        {
            InitializeComponent();

            _animalButtons = new ApplicationBarIconButton[]
            {
                BarButtons.MakeButton("edit.png", "editar", (object e, EventArgs args) =>
                {
                    AnimaisControl.BeginEditing(LayoutRoot.DataContext as Animal);
                    NavigationService.Navigate(AnimalEditPage.GetUri());
                })
            };

            _localizacaoButtons = new ApplicationBarIconButton[]
            {

            };

            _historicoButtons = new ApplicationBarIconButton[]
            {

            };

            _coleiraButtons = new ApplicationBarIconButton[]
            {

            };

            _buttons = new ApplicationBarIconButton[][]
            {
                _animalButtons,
                _localizacaoButtons,
                _historicoButtons,
                _coleiraButtons
            };

            ShowAnimalLocationOnTheMap(null, null);
            CalcularRota(LocalizacoesControl.GetLocalizacoesByAnimal(AnimaisControl.GetCurrentAnimal().Id));
        }

        private void ShowAnimalLocationOnTheMap(object sender, EventArgs e)
        {
            //Geolocator myGeolocator = new Geolocator();
            //Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            //Geocoordinate myGeocoordinate = myGeoposition.Coordinate;

            int animalId = AnimaisControl.GetCurrentAnimal().Id;
            List<Localizacao> localizacoes = LocalizacoesControl.GetLocalizacoesByAnimal(animalId);

            MapLayer myLocationLayer = new MapLayer();

            if (localizacoes.Count != 0) {
                GeoCoordinate geo = localizacoes.ElementAt(0).Coordenada;
                this.mapaLocalizacao.Center = geo;
                this.mapaLocalizacao.ZoomLevel = 13;

                MapOverlay myLocationOverlay = new MapOverlay();
                myLocationOverlay.Content = createMarker(true);
                myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
                myLocationOverlay.GeoCoordinate = geo;

                myLocationLayer.Add(myLocationOverlay);
            }
            //Círculo de marcação no mapa

            

            //Criando camada para conter a marcação
            for (int i = 1; i < localizacoes.Count; i++)
            {
                MapOverlay myLocationOverlay = new MapOverlay();
                myLocationOverlay.Content = createMarker(false);
                myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
                myLocationOverlay.GeoCoordinate = localizacoes.ElementAt(i).Coordenada;

                myLocationLayer.Add(myLocationOverlay);
            }

            this.mapaLocalizacao.Layers.Add(myLocationLayer);
        }

        private void SetMapCenterCoordenate(GeoCoordinate g) {
            this.mapaLocalizacao.Center = g;
        }

        private Route RotaAnimal = null;
        private MapRoute RotaMapaAnimal = null;

        private void RotaQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            if (e.Error == null)
            {
                RotaAnimal = e.Result;
                RotaMapaAnimal = new MapRoute(RotaAnimal);
                mapaLocalizacao.AddRoute(RotaMapaAnimal);
            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao traçar a rota do animal!");
            }
        }

        private void CalcularRota(List<Localizacao> rota)
        {
            List<GeoCoordinate> lst = new List<GeoCoordinate>();

            if (rota != null)
            {
                if (rota.Count != 0)
                {
                    foreach (Localizacao loc in rota)
                    {
                        lst.Add(loc.Coordenada);
                    }
                    RotaQuery = new RouteQuery();
                    RotaQuery.TravelMode = _travelMode;
                    RotaQuery.Waypoints = lst;
                    RotaQuery.QueryCompleted += RotaQuery_QueryCompleted;
                    RotaQuery.QueryAsync();
                }
            }
        }

        private Image createMarker(bool isAtual)
        {
            Image im = new Image();
            if (!isAtual)
            {
                im.Source = new BitmapImage(new Uri("../Images/pinoTrajeto.png", UriKind.Relative));
            }
            else
            {
                im.Source = new BitmapImage(new Uri("../Images/pinoAtual.png", UriKind.Relative));
            }
            return im;
            /*return new Ellipse()
            {
                Fill = new SolidColorBrush(Colors.Blue),
                Height = 20,
                Width = 20,
                Opacity = 50    
            };*/
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

            ApplicationBar.IsVisible = (LayoutRoot.DataContext as Animal).Dono.IsCurrentUsuario;
        }

        private void DonoButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PerfisControl.SetCurrentUsuario((LayoutRoot.DataContext as Animal).DonoId);
            PerfilPivot.FocusOnProfile();
            NavigationService.Navigate(PerfilPivot.GetUri());
        }

        private void StatusButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var animal = LayoutRoot.DataContext as Animal;

            if (!PerfisControl.IsLoggedInPerfil(animal.DonoId))
                return;

            string message = (animal.Status == Status.OK) ?
                "Seu animal será considerado perdido e ficará visível para outros usuários em sua proximidade." :
                "Seu animal será considerado encontrado e não estará mais visível para outros usuários em sua proximidade.";

            var result = MessageBox.Show(message, "Mudança de status", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                animal.Status = (animal.Status == Status.OK) ? Status.Perdido : Status.OK;
                updateLayoutRoot();
            }
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationBar.AddButtons(_buttons[(sender as Pivot).SelectedIndex]);
        }

        private void GaleriaButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void StackPanelHistorico_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.Pivot.SelectedIndex = 1;
            GeoCoordinate gc = ((sender as StackPanel).DataContext as Localizacao).Coordenada;
            SetMapCenterCoordenate(gc);
        }

    }
}