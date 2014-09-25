using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // ---

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

        public static Animal Copy(Animal that)
        {
            if (that == null)
                return null;

            return new Animal()
            {
                Nome=that.Nome,
                Status=that.Status,
                Especie=that.Especie,
                Sexo=that.Sexo,
                Descricao = that.Descricao
            };
        }

        // ---

        public bool IsPerdido
        {
            get { return Status == Status.Perdido; }
        }

        public string StatusString
        {
            get { return "Status: " + (IsPerdido ? "Perdido!" : "Ok"); }
        }

        public SolidColorBrush StatusBrush
        {
            get { return new SolidColorBrush(IsPerdido ? Colors.Red : new Color() { R = 0, G = 255, B = 0, A = 255 }); }
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
