using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClapApp.Model;
using ClapApp.Control;
using ClapApp.View;
using Microsoft.Devices;
using System.IO;
using MessagingToolkit.Barcode;
using System.Windows.Media.Imaging;

namespace ClapApp.Pages
{
    class QRInfo
    {
        private Animal _animal;

        public QRInfo(int animalId)
        {
            _animal = AnimaisControl.GetAnimalById(animalId);
        }

        public string Nome
        {
            get
            {
                return _animal.Nome;
            }
        }

        public string NumeroDoDono
        {
            get
            {
                return _animal.Dono.Numeros[0].DDDNumero;
            }
        }

        public int Id
        {
            get
            {
                return _animal.Id;
            }
        }
    }

    public partial class LoginPivot : PhoneApplicationPage
    {
        private PhotoCamera camera;
        private bool capturing = false;

        public LoginPivot()
        {
            InitializeComponent();
            PerfisControl.PopulateExamples();
        }

        //------------QR CODE -----------------

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

        private void camera_CaptureImageAvailable(object sender, ContentReadyEventArgs e)
        {
            capturing = false;

            Stream imageStream = (Stream)e.ImageStream;
            BarcodeDecoder barcodeDecoder = new BarcodeDecoder();

            Dictionary<DecodeOptions, object> decodingOptions =
                                new Dictionary<DecodeOptions, object>();
            List<BarcodeFormat> possibleFormats = new List<BarcodeFormat>(1);
            Result result;

            Dispatcher.BeginInvoke(
                () =>
                {

                    WriteableBitmap qrImage = new WriteableBitmap((int)camera.Resolution.Width, (int)camera.Resolution.Height);
                    imageStream.Position = 0;
                    qrImage.LoadJpeg(imageStream);

                    possibleFormats.Add(BarcodeFormat.QRCode);

                    decodingOptions.Add(
                        DecodeOptions.PossibleFormats,
                        possibleFormats);

                    try
                    {
                        result = barcodeDecoder.Decode(qrImage, decodingOptions);

                        //resultText.Text = result.Text;

                        resultText.Text = "Animal identificado.";

                        StkQRInfo.Visibility = Visibility.Visible;
                        StkQRInfo.DataContext = new QRInfo(0);
                    }
                    catch (NotFoundException)
                    {
                        // this is expected if the image does not contain a valid
                        // code, Or is too distorted to read
                        resultText.Text = "Imagem não identificada. Aponte a câmera para o código QR na coleira do animal.";
                    }
                    catch (Exception ex)
                    {
                        // something else went wrong, so alert the user
                        MessageBox.Show(
                            ex.Message,
                            "Erro ao decodificar a imagem",
                            MessageBoxButton.OK);
                    }
                }
            );
        }

        // user navigated away from page
        protected override void OnNavigatedFrom(
                   System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (camera != null)
            {
                // unhook the event handlers
                CameraButtons.ShutterKeyPressed -= camera_ButtonFullPress;
                camera.CaptureImageAvailable -= camera_CaptureImageAvailable;
                camera.Initialized -= camera_Initialised;

                // dispose of the camera object
                camera.Dispose();
                camera = null;
            }
        }

        //-----------------------------

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            updateDataContext();

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
        }

        private void updateDataContext()
        {
            AnimaisListBox.ItemsSource = null;
            AnimaisListBox.ItemsSource = AnimaisControl.GetAllAnimaisPerdidos();
        }

        private void AnimalButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            AnimalButtonEvent.OnClick(this, sender, e);
        }

        private void login(string email, string senha)
        {
            if (PerfisControl.Login(email, senha))
            {
                PerfisControl.SetCurrentUsuario(PerfisControl.GetLoggedUsuarioId());
                NavigationService.Navigate(PerfilPivot.GetUri());
            }
            else MessageBox.Show("E-mail ou senha incorretos.", "Falha de login", MessageBoxButton.OK);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = TxtEmail.Text;

            if (email.Equals(""))
            {
                login("mox@a.com", "mox");
            }
            else login(email, TxtSenha.Password);
        }

        private void BtnCriarPerfil_Click(object sender, RoutedEventArgs e)
        {
            PerfisControl.BeginCreating(new Perfil());
            NavigationService.Navigate(PerfilEditPage.GetUri());
        }

        private void BtnAnimal_Click(object sender, RoutedEventArgs e)
        {
            var qrInfo = StkQRInfo.DataContext as QRInfo;
            AnimalButtonEvent.ViewAnimalProfile(this, qrInfo.Id);
        }
    }
}