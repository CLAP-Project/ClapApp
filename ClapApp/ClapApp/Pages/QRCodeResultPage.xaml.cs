using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClapApp.View;

namespace ClapApp.Pages
{
    public partial class QRCodeResultPage : PhoneApplicationPage
    {
        public QRCodeResultPage()
        {
            InitializeComponent();
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/QRCodeResultPage.xaml", UriKind.Relative);
        }

        public static QRInfo QRInfo
        {
            get;
            set;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            StkQRInfo.DataContext = null;
            StkQRInfo.DataContext = QRInfo;
        }

        private void BtnAnimal_Click(object sender, RoutedEventArgs e)
        {
            var qrInfo = StkQRInfo.DataContext as QRInfo;
            AnimalButtonEvent.ViewAnimalProfile(this, qrInfo.Id);
        }
    }
}