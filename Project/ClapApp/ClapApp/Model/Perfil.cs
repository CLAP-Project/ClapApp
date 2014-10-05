using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ClapApp.Model
{
    public class NumeroTelefonico
    {
        public string DDD { get; set; }
        public string Numero { get; set; }

        public string DDDNumero
        {
            get
            {
                return '(' + DDD + ") " + Numero;
            }
        }

        public static NumeroTelefonico GetExemplo()
        {
            return new NumeroTelefonico() { DDD = "92", Numero = "8266-6492" };
        }

        public SolidColorBrush TextBrush
        {
            get
            {
                return new SolidColorBrush(App.ThemeColor);
            }
        }
    }

    public class Email
    {
        public string Value { get; set; }

        public static Email GetExemplo()
        {
            return new Email() { Value = "mechamutoh@gmail.com" };
        }
    }

    public class OcultandoNumerosEEmailsException : Exception
    {

    }

    public class OcultandoEmailsSemNumerosException : Exception
    {

    }

    public class Perfil
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public string NomeSobrenome
        {
            get { return ((NomePublico? Nome + ' ' : "") + (SobrenomePublico? Sobrenome : "")).Trim(); }
        }

        public string NomeSobrenomeRaw
        {
            get { return Nome + ' ' + Sobrenome; }
        }

        public bool NomePublico { get; set; }
        public bool SobrenomePublico { get; set; }

        public Visibility NomeSobrenomeVisible
        {
            get { return (NomeSobrenome.Equals(""))? Visibility.Collapsed : Visibility.Visible; }
        }

        public string Cidade { get; set; }
        public string Estado { get; set; }

        public string CidadeEstado
        {
            get { return Cidade + ", " + Estado; }
        }

        public bool CidadeEstadoPublico { get; set; }
        public Visibility CidadeEstadoVisible { get { return CidadeEstadoPublico ? Visibility.Visible : Visibility.Collapsed; } }

        List<NumeroTelefonico> _numeros = new List<NumeroTelefonico>();
        List<Email> _emails = new List<Email>();
        List<Animal> _animais = new List<Animal>();

        public List<NumeroTelefonico> Numeros { get { return _numeros; } }
        public List<Email> Emails { get { return _emails; } }

        private bool _numerosPublico = true;
        public Visibility NumerosVisible { get { return NumerosPublico ? Visibility.Visible : Visibility.Collapsed; } }

        public bool NumerosPublico
        {
            set
            {
                if (value)
                {
                    _numerosPublico = true;
                }
                else
                {
                    if (!EmailsPublico)
                    {
                        throw new OcultandoNumerosEEmailsException();
                    }
                    else _numerosPublico = false;
                }
            }

            get
            {
                return _numerosPublico && Numeros.Count != 0;
            }
        }

        private bool _emailsPublico = true;
        public Visibility EmailsVisible { get { return EmailsPublico ? Visibility.Visible : Visibility.Collapsed; } }

        public bool EmailsPublico
        {
            set
            {
                if (value)
                {
                    _emailsPublico = true;
                }
                else
                {
                    if (!NumerosPublico)
                    {
                        throw new OcultandoEmailsSemNumerosException();
                    }
                    else _emailsPublico = false;
                }
            }

            get
            {
                return _emailsPublico;
            }
        }

        public List<Animal> Animais { get { return _animais; } }

        public Perfil()
        {
            NomePublico = true;
            SobrenomePublico = true;
            CidadeEstadoPublico = true;
            EmailsPublico = true;
            NumerosPublico = true;
        }

        public Animal AddAnimal(Animal animal)
        {
            Animais.Add(animal);
            return animal;
        }

        public static Perfil Copy(Perfil that)
        {
            if (that == null)
                return null;
            
            var copy = new Perfil()
            {
                Nome=that.Nome.Trim(),
                NomePublico = that.NomePublico,

                Sobrenome=that.Sobrenome.Trim(),
                SobrenomePublico=that.SobrenomePublico,

                Cidade=that.Cidade.Trim(),
                Estado=that.Estado.Trim(),
                CidadeEstadoPublico=that.CidadeEstadoPublico
            };

            copy.Numeros.AddRange(that.Numeros);
            copy.Emails.AddRange(that.Emails);
            copy.Animais.AddRange(that.Animais);

            copy.NumerosPublico = that.NumerosPublico;
            copy.EmailsPublico = that.EmailsPublico;

            return copy;
        }

        public static Perfil GetExemplo() {
            Perfil mox = new Perfil()
            {
                Nome = "Max",
                Sobrenome = "Willams",
                Cidade = "Manaus",
                Estado = "Amazonas"
            };

            //mox._emails.Add(Email.GetExemplo());
            mox._numeros.Add(NumeroTelefonico.GetExemplo());

            mox._animais.Add(new Animal()
            {
                Nome = "Jack Tartaruga",
                Sexo = Sexo.Macho,
                Especie = "Cágado",
                Status = Status.Perdido,
                Descricao = "A tartaruguinha mais sapeca do pedaço está pronta pra levar uma família à loucura! Ah, e ela come alface pra caramba..."
            });

            return mox;
        }
    }
}
