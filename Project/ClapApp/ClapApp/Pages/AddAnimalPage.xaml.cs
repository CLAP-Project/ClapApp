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
using System.Windows.Media;

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

        private static Sexo ParseSexo(ListPicker lst)
        {
            return Animal.ParseSexo(lst);
        }

        private static string ParseEspecie(string str)
        {
            return Animal.ParseEspecie(str);
        }

        private static string checkText(string str, string errorMessage)
        {
            str = str.Trim();

            if (str.Equals(""))
            {
                MessageBox.Show(errorMessage, "Dados incompletos", MessageBoxButton.OK);
                return null;
            }

            return str;
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            bool errors = false;

            var nome = checkText(txtNome.Text, "Seu animal precisa de um nome.");
            if (nome == null) errors = true;

            var especie = checkText(txtEspecie.Text, "Seu animal precisa de uma espécie.");
            if (especie == null) errors = true;

            var numeroColeira = checkText(txtNumeroColeira.Text, "Seu animal precisa de um número para sua coleira.");
            if (numeroColeira == null) errors = true;

            if (errors) return;

            var cargo = new Animal()
            {
                Nome = nome,
                Especie = ParseEspecie(especie),
                Sexo = ParseSexo(pkrSexo),
                Descricao = txtDescricao.Text.Trim(),
                NumeroColeira = numeroColeira,
                Status = Status.Ok
            };

            Editing.TempUsuario.AddAnimal(cargo);
            Editing.SaveUsuario();

            NavigationService.GoBack();
        }
    }
}