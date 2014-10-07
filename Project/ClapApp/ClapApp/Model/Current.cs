using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClapApp.Model
{
    public static class Current
    {
        private static List<Perfil> _usuarioStack = new List<Perfil>();
        private static List<Animal> _animalStack = new List<Animal>();

        static Current()
        {
            SetUsuario(Perfil.GetExemplo());
        }

        // ---

        public static Perfil Usuario
        {
            get
            {
                return _usuarioStack[_usuarioStack.Count - 1];
            }
        }

        public static void SetUsuario(Perfil usuario)
        {
            _usuarioStack.Clear();
            _usuarioStack.Add(usuario);
        }

        public static void PushUsuarioForEdit()
        {
            _usuarioStack.Add(Perfil.Copy(Current.Usuario));
        }

        public static void SaveEditingUsuario()
        {
            var usuarioSize = _usuarioStack.Count;
            _usuarioStack[usuarioSize - 2] = Perfil.Copy(_usuarioStack[usuarioSize - 1]);
        }

        public static void FinishEditingUsuario()
        {
            _usuarioStack.RemoveAt(_usuarioStack.Count - 1);
        }

        // ---

        public static Animal Animal
        {
            get
            {
                return _animalStack[_animalStack.Count - 1];
            }
        }

        public static void SetAnimal(Animal animal)
        {
            _animalStack.Clear();
            _animalStack.Add(animal);
        }

        public static void PushAnimalForEdit()
        {
            _animalStack.Add(Animal.Copy(Current.Animal));
        }

        public static void SaveEditingAnimal()
        {
            var animalSize = _animalStack.Count;
            _animalStack[animalSize - 2] = Animal.Copy(_animalStack[animalSize - 1]);
        }

        public static void FinishEditingAnimal()
        {
            _animalStack.RemoveAt(_animalStack.Count - 1);
        }
    }
}