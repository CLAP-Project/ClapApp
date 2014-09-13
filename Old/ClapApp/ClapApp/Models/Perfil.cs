using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClapApp.Models
{
    public class Perfil
    {

        public class NumeroTelefonico
        {
            public string DDD { get; set; }
            public string Numero { get; set; }

            public string DDDNumero
            {
                get { return "(" + DDD + ") " + Numero; }
            }
        }

        public class Email
        {
            public string Value { get; set; }
        }

        public List<NumeroTelefonico> Numeros = new List<NumeroTelefonico>();
        public List<Email> Emails = new List<Email>();

        // imagem
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Endereco { get; set; }




    }
}
