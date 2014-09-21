using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClapApp.Model
{
    public static class Editing
    {
        public static Perfil Usuario = Perfil.GetExemplo();
        public static Animal Animal = null;

        private static Perfil _tempUsuario = null;
        private static Animal _tempAnimal = null;

        public static void SetTempUsuario()
        {
            TempUsuario = Usuario;
        }

        public static void SaveUsuario()
        {
            if (TempUsuario != null)
            {
                Usuario = Perfil.Copy(TempUsuario);
                TempUsuario = Perfil.Copy(Usuario);
            }
        }

        public static void CancelTempUsuario()
        {
            TempUsuario = null;
        }

        public static Perfil TempUsuario
        {
            get { return _tempUsuario; }
            private set
            {
                _tempUsuario = Perfil.Copy(value);
            }
        }

        public static void SetTempAnimal()
        {
            TempAnimal = Animal;
        }

        public static void SaveAnimal()
        {
            Animal = Animal.Copy(TempAnimal);
            TempAnimal = Animal.Copy(Animal);
        }

        public static void CancelTempAnimal()
        {
            TempAnimal = null;
        }

        public static Animal TempAnimal
        {
            get { return _tempAnimal; }
            private set
            {
                _tempAnimal = Animal.Copy(value);
            }
        }
    }
}
