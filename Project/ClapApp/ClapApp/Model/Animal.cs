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
        Fêmea,
        Indefinido
    }

    public class Animal
    {
        public string Nome { get; set; }
        public Status Status { get; set; }

        public string Especie { get; set; }
        public Sexo Sexo { get; set; }

        public string Informacoes { get; set; }

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
            get { return new SolidColorBrush(IsPerdido? Colors.Red : Colors.White); }
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
            get { return Especie + ", " + SexoString; }
        }
    }
}
