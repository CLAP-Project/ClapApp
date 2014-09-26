using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Microsoft.Devices;
using System.IO;
using System.Windows.Media.Imaging;

namespace ClapApp.Pages
{
    public partial class FotoQRCode2 : PhoneApplicationPage
    {
        private PhotoCamera camera;
        private bool capturing = false;

        public FotoQRCode2()
        {
            InitializeComponent();
        }

        // ---

        public static Uri GetUri()
        {
            return new Uri("/Pages/FotoQRCodePage2.xaml", UriKind.Relative);
        }
        // --- Eventos de Câmera

        private void camera_Initialised(object sender, CameraOperationCompletedEventArgs e)
        {
            // set the camera resolution
            if (e.Succeeded)
            {
                var res = from resolution in camera.AvailableResolutions
                          where resolution.Width == 640
                          select resolution;

                camera.Resolution = res.First();
            }
        }

        // user has pressed the camera button
        private void camera_ButtonFullPress(object sender, EventArgs e)
        {
            if (capturing) return;

            capturing = true;

            camera.CaptureImage();
        }

        // ---

        protected override void OnNavigatedTo(
           System.Windows.Navigation.NavigationEventArgs e)
        {
            if (null == camera)
            {
                camera = new PhotoCamera();

                // filred when the camera is initialised
                camera.Initialized += camera_Initialised;

                // fired when the button is fully pressed
                CameraButtons.ShutterKeyPressed += camera_ButtonFullPress;

                // fired when an image is available.
                camera.CaptureImageAvailable += camera_CaptureImageAvailable;

                // set the VideoBrush source to the camera output
                videoRotateTransform.CenterX = videoRectangle.Width / 2;
                videoRotateTransform.CenterY = videoRectangle.Height / 2;
                videoRotateTransform.Angle = 90;

                viewfinderBrush.SetSource(camera);
            }

            base.OnNavigatedTo(e);
        }

        // user navigated away from page
        protected override void OnNavigatedFrom(
                   System.Windows.Navigation.NavigationEventArgs e)
        {
            if (camera != null)
            {
                // unhook the event handlers
                CameraButtons.ShutterKeyPressed -= camera_ButtonFullPress;
                camera.CaptureImageAvailable -= camera_CaptureImageAvailable;
                camera.Initialized -= camera_Initialised;

                // dispose of the camera object
                camera.Dispose();
            }

            base.OnNavigatedFrom(e);
        }
    }
}