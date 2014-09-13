using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public class Email
    {
        public string Value { get; set; }

        public static Email GetExemplo()
        {
            return new Email() { Value = "mechamutoh@gmail.com" };
        }
    }

    public class Perfil
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public string NomeSobrenome
        {
            get { return Nome + ' ' + Sobrenome; }
        }

        public string Cidade { get; set; }
        public string Estado { get; set; }

        public string CidadeEstado
        {
            get { return Cidade + ", " + Estado; }
        }

        List<NumeroTelefonico> _numeros = new List<NumeroTelefonico>();
        List<Email> _emails = new List<Email>();
        List<Animal> _animais = new List<Animal>();

        public List<NumeroTelefonico> Numeros { get { return _numeros; } }
        public List<Email> Emails { get { return _emails; } }

        public List<Animal> Animais { get { return _animais; } }

        public static Perfil GetExemplo() {
            Perfil mox = new Perfil()
            {
                Nome = "Max",
                Sobrenome = "Wolliamsa",
                Cidade = "Manaus",
                Estado = "Amazonas"
            };

            mox._emails.Add(Email.GetExemplo());
            mox._numeros.Add(NumeroTelefonico.GetExemplo());

            mox._animais.Add(new Animal()
            {
                Nome="Jack Tartaruga",
                Status=Status.Perdido
            });

            return mox;
        }
    }
}
