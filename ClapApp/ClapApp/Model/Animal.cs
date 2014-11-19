using ClapApp.Control;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        private BitmapImage m_image;
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

        public List<Localizacao> Historico
        {
            get
            {
                return LocalizacoesControl.GetLocalizacoesByAnimal(this.Id);
            }
        }

        public string NomeEdit
        {
            get
            {
                return AnimaisControl.IsCreating() ? "Qual seu nome?" : this.Nome;
            }
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

        public void setImage(string path) {
            var img = new BitmapImage(new Uri(path, UriKind.Relative));
            m_image = img;
        }

        public string SetImageGambs
        {
            set
            {
                setImage(value);
            }
        }

        public BitmapImage Imagem
        {
            get
            {
                return m_image;
            }
            set
            {
                m_image = value;
            }
        }

        public BitmapImage ImagemDono
        {
            get
            {
                return this.Dono.Imagem;
            }
        }

        public Status Status
        {
            get;
            set;
        }

        public Perfil Dono
        {
            get
            {
                return PerfisControl.GetPerfilById(this.DonoId);
            }
        }

        public string DonoNomeSobrenome
        {
            get
            {
                return Dono.NomeSobrenome;
            }
        }

        public Visibility HasLocalizacaoVisibility
        {
            get
            {
                if (LocalizacoesControl.GetLocalizacoesByAnimal(this.Id).Count == 0)
                {
                    return Visibility.Visible;
                }

                return Visibility.Collapsed;
            }
        }

        public Visibility IsDonoCurrentUsuarioVisibility
        {
            get
            {
                return Dono.IsCurrentUsuarioVisiblity;
            }
        }

        public string EditPageTitle
        {
            get
            {
                return AnimaisControl.IsCreating() ? "NOVO ANIMAL" : "EDITANDO ANIMAL";
            }
        }

        public bool IsPerdido
        {
            get
            {
                return this.Status == Status.Perdido;
            }
        }

        public Visibility IsPerdidoVisibility
        {
            get
            {
                return IsPerdido ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility IsOkVisibility
        {
            get
            {
                return !IsPerdido ? Visibility.Visible : Visibility.Collapsed;
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

        public string Descricao
        {
            get;
            set;
        }

        public SolidColorBrush StatusColor
        {
            get
            {
                return IsPerdido ? new SolidColorBrush(new Color() { R = 255, G = 238, B = 0, A = 255 }) :
                    //App.Current.Resources["PerdidoColor"] as SolidColorBrush :
                    //new SolidColorBrush(new Color() { R = 255, G = 89, B = 60, A = 255 })
                    new SolidColorBrush(new Color() { R = 000, G = 255, B = 000, A = 255 });
            }
        }

        /*private static Random rand = new Random();

        private string m_gambs = rand.Next(2) == 0 ? "50°03'46.461\"S 125°48'26.533\"E 978.90m" : "Sem localização";*/

        public string LocalizacaoMaisRecente
        {
            get
            {
                var locals = this.Historico;

                if (locals.Count == 0) return "Sem localização";
                else
                {
                    var local = locals[0];
                    return "Rastreado em " + local.DataHora;
                }
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

        public Animal()
        {
            this.Descricao = "";
            this.Especie = "";
            this.Nome = "";
            this.Sexo = Sexo.Macho;
            this.Status = Status.OK;
        }

        public Animal Assimilate(Animal that)
        {
            this.Id = that.Id;
            this.DonoId = that.DonoId;
            this.Especie = that.Especie.Substring(0);
            //this.m_gambs = that.m_gambs.Substring(0);
            this.Nome = that.Nome.Substring(0);
            this.Sexo = that.Sexo;
            this.Status = that.Status;
            this.m_image = that.m_image;
            return this;
        }

        public Animal Copy()
        {
            return new Animal().Assimilate(this);
        }
    }
}
