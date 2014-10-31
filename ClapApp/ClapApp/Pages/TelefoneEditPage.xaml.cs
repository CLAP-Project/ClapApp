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

namespace ClapApp.Pages
{
    public partial class TelefoneEditPage : PhoneApplicationPage
    {
        public TelefoneEditPage()
        {
            InitializeComponent();
        }

        public static Uri GetUri()
        {
            return new Uri("/Pages/TelefoneEditPage.xaml", UriKind.Relative);
        }

        private void updateLayoutRoot()
        {
            LayoutRoot.DataContext = null;
            LayoutRoot.DataContext = NumerosControl.GetEditingNumero();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            updateLayoutRoot();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            NumerosControl.FinishEditing();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool failed = false;

            if (TxtDDD.Text.Trim().Equals(""))
            {
                MessageBox.Show("DDD", "Campo não preenchido", MessageBoxButton.OK);
                failed = true;
            }

            if (TxtNumero.Text.Trim().Equals(""))
            {
                MessageBox.Show("Número", "Campo não preenchido", MessageBoxButton.OK);
                failed = true;
            }

            if (failed)
                return;

            try
            {
                NumerosControl.SaveEditing();
                NavigationService.GoBack();
            }
            catch (NumeroJaCadastradoException)
            {
                MessageBox.Show("Este número já está cadastrado.", NumerosControl.GetEditingNumero().DDDNumero, MessageBoxButton.OK);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}