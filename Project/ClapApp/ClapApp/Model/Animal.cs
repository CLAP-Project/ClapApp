using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ClapApp.Model
{
    public enum Status {
        Perdido,
        Ok
    }

    public enum Sexo
    {
        Macho,
        Femea,
        Indefinido
    }

    public class Animal
    {
        public string NumeroColeira { get; set; }

        public string Nome { get; set; }
        public Status Status { get; set; }

        public string Especie { get; set; }
        public Sexo Sexo { get; set; }

        public string Descricao { get; set; }

        public string EspecieFormatted
        {
            get { return Especie; }
            set { Especie = ParseEspecie(value); }
        }

        // ---

        public static string ParseEspecie(string str)
        {
            if (str.Length != 0)
            {
                var strArray = str.ToLowerInvariant().ToCharArray();
                strArray[0] = char.ToUpperInvariant(strArray[0]);
                str = new string(strArray);
            }

            return str;
        }

        public static Sexo ParseSexo(ListPicker lst)
        {
            var str = (lst.SelectedItem as ListPickerItem).Content.ToString();

            switch (str)
            {
                case "Macho":
                    return Sexo.Macho;

                case "Fêmea":
                    return Sexo.Femea;

                default:
                    return Sexo.Indefinido;
            }
        }

        public Animal Assimilate(Animal that)
        {
            Nome=that.Nome;
            Status=that.Status;
            Especie=that.Especie;
            Sexo=that.Sexo;
            Descricao = that.Descricao;

            return this;
        }

        public static Animal Copy(Animal that)
        {
            if (that == null)
                return null;

            return new Animal().Assimilate(that);
        }

        // ---

        public bool IsPerdido
        {
            get { return Status == Status.Perdido; }
        }

        public string StatusString
        {
            get { return "Status: " + StatusStringSimple; }
        }

        public string StatusStringSimple
        {
            get { return IsPerdido? "Perdido" : "Ok" ;}
        }

        public SolidColorBrush StatusBrush
        {
            get
            {
                return IsPerdido ? App.Current.Resources["PerdidoColor"] as SolidColorBrush :
                    new SolidColorBrush(new Color() { R = 0, G = 255, B = 0, A = 255 });
            }
        }

        public void ToggleStatus()
        {
            this.Status = IsPerdido ? Status.Ok : Status.Perdido;
        }

        // ---

        public string SexoString
        {
            get { return Sexo == Sexo.Macho ? "macho" : "fêmea"; }
        }

        public bool SexoVisivel
        {
            get { return Sexo != Sexo.Indefinido; }
        }

        public string EspecieSexo
        {
            get { return Especie + (SexoVisivel? ", " + SexoString : ""); }
        }
    }
}
