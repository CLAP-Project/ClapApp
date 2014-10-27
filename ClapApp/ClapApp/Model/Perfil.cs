using ClapApp.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClapApp.Model
{
    public class NumeroTelefonico
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

        public string DDD
        {
            get;
            set;
        }

        public string Numero
        {
            get;
            set;
        }

        public string DDDNumero
        {
            get
            {
                return '(' + DDD + ") " + Numero;
            }
        }

        public string DonoNomeSobrenome
        {
            get
            {
                return PerfisControl.GetPerfilById(this.DonoId).NomeSobrenome;
            }
        }

        public string EditPageTitle
        {
            get
            {
                return NumerosControl.IsCreating() ?
                    "NOVO NÚMERO" :
                    "EDITANDO NÚMERO";
            }
        }

        // ---

        public NumeroTelefonico()
        {
            this.DDD = "";
            this.Numero = "";
        }

        // ---

        public NumeroTelefonico Assimilate(NumeroTelefonico that)
        {
            this.Id = that.Id;
            this.DonoId = that.DonoId;
            this.DDD = that.DDD.Substring(0);
            this.Numero = that.Numero.Substring(0);

            return this;
        }

        public NumeroTelefonico Copy()
        {
            return new NumeroTelefonico().Assimilate(this);
        }
    }

    public class Perfil
    {
        public int Id
        {
            get;
            set;
        }

        // ---

        public bool IsCurrentUsuario
        {
            get
            {
                return PerfisControl.IsLoggedInPerfil(this.Id);
            }
        }

        public Visibility IsCurrentUsuarioVisiblity
        {
            get
            {
                return IsCurrentUsuario ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public string Email
        {
            get;
            set;
        }

        public string Senha
        {
            get;
            set;
        }

        public List<Animal> Animais
        {
            get
            {
                return AnimaisControl.GetAnimaisByDono(this.Id);
            }
        }

        // ---

        public string Nome
        {
            get;
            set;
        }

        public string Sobrenome
        {
            get;
            set;
        }

        public string NomeSobrenome
        {
            get
            {
                if (Sobrenome.Trim().Equals(""))
                    return Nome;

                if (Nome.Trim().Equals(""))
                    return Sobrenome;

                return Nome + ' ' + Sobrenome;
            }
        }

        public string NomeSobrenomeTitle
        {
            get
            {
                var ns = NomeSobrenome;

                if (ns.Trim().Equals("")) return "Não logado";
                else return ns;
            }
        }

        public Visibility NomeSobrenomeVisibility
        {
            get
            {
                return NomeSobrenome.Trim().Equals("") ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public string Cidade
        {
            get;
            set;
        }

        public string Estado
        {
            get;
            set;
        }

        public string CidadeEstado
        {
            get
            {
                if (Cidade.Trim().Equals(""))
                    return Estado;

                if (Estado.Trim().Equals(""))
                    return Cidade;

                return Cidade + ", " + Estado;
            }
        }

        public Visibility CidadeEstadoVisiblity
        {
            get
            {
                return CidadeEstado.Trim().Equals("") ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        // ---

        public static string PerfilEditTitle
        {
            get
            {
                return PerfisControl.IsCreating() ? "CADASTRO DE USUÁRIO" : "EDITANDO USUÁRIO";
            }
        }

        public static Visibility EditingStuffVisibility
        {
            get
            {
                return PerfisControl.IsCreating() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public static Visibility CreatingStuffVisibility
        {
            get
            {
                return !PerfisControl.IsCreating() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        // ---

        public List<NumeroTelefonico> Numeros
        {
            get
            {
                return NumerosControl.GetNumerosByDono(this.Id);
            }
        }

        public bool AddNumero(NumeroTelefonico numero)
        {
            var found = NumerosControl.GetNumeroByValue(this.Id, numero.DDD, numero.Numero);

            if (found == null)
            {
                numero.DonoId = this.Id;
                NumerosControl.InsertNumero(numero);

                return true;
            }

            return false;
        }

        // ---

        public Perfil Assimilate(Perfil that)
        {
            this.Id = that.Id;
            this.Email = that.Email.Substring(0);
            this.Cidade = that.Cidade.Substring(0);
            this.Estado = that.Estado.Substring(0);
            this.Nome = that.Nome.Substring(0);
            this.Senha = that.Senha.Substring(0);
            this.Sobrenome = that.Sobrenome.Substring(0);

            this.Numeros.Clear();

            foreach (var numero in that.Numeros)
            {
                this.Numeros.Add(numero.Copy());
            }

            return this;
        }

        public Perfil Copy()
        {
            return new Perfil().Assimilate(this);
        }

        // ---

        public Perfil()
        {
            this.Cidade = "";
            this.Email = "";
            this.Estado = "";
            this.Nome = "";
            this.Senha = "";
            this.Sobrenome = "";
        }

        public Perfil(params NumeroTelefonico[] numeros)
        {
            foreach (var numero in numeros)
            {
                numero.DonoId = this.Id;
                NumerosControl.InsertNumero(numero);
            }    
        }
    }
}
