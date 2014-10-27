using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClapApp.Model
{
    public enum Sexo
    {
        Macho,
        Femea,
        Indefinido
    }

    public enum Status
    {
        Perdido,
        OK
    }

    public class Animal
    {
        public int Id
        {
            get;
            set;
        }

        public int DonoId
        {
            get;
            set;
        }

        public string Nome
        {
            get;
            set;
        }

        public string Especie
        {
            get;
            set;
        }

        public Sexo Sexo
        {
            get;
            set;
        }

        public Status Status
        {
            get;
            set;
        }

        public bool IsPerdido
        {
            get
            {
                return this.Status == Status.Perdido;
            }
        }

        public string StatusToString
        {
            get
            {
                return IsPerdido ? "Perdido!" : "OK";
            }
        }

        public string StatusToStringFull
        {
            get
            {
                return "Status: " + StatusToString ;
            }
        }

        public string SexoToString
        {
            get
            {
                switch (this.Sexo)
                {
                    case Sexo.Macho:
                        return "macho";

                    case Sexo.Femea:
                        return "fêmea";

                    default:
                        return "";
                }
            }
        }

        private static Random rand = new Random();

        private string m_gambs = rand.Next(2) == 0 ? "50°03'46.461\"S 125°48'26.533\"E 978.90m" : "Sem localização";

        public string LocalizacaoMaisRecente
        {
            get
            {
                return m_gambs;
            }
        }

        public string EspecieSexo
        {
            get
            {
                if (this.Sexo != Sexo.Indefinido)
                    return Especie + ", " + SexoToString;

                return Especie;
            }
        }

        // ---

        public Animal Assimilate(Animal that)
        {
            this.Id = that.Id;
            this.DonoId = that.DonoId;
            this.Especie = that.Especie.Substring(0);
            this.m_gambs = that.m_gambs.Substring(0);
            this.Nome = that.Nome.Substring(0);
            this.Sexo = that.Sexo;
            this.Status = that.Status;

            return this;
        }

        public Animal Copy()
        {
            return new Animal().Assimilate(this);
        }
    }
}
