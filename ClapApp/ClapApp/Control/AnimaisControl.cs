using ClapApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClapApp.Control
{
    static class AnimaisControl
    {
        private static int _inserted = 0;
        private static Dictionary<int, Animal> _animais = new Dictionary<int,Animal>();

        public static int InsertAnimal(Animal animal)
        {
            animal.Id = _inserted++;
            _animais.Add(animal.Id, animal);

            return animal.Id;
        }

        public static void EraseAnimalById(int id)
        {
            _animais.Remove(id);
        }

        public static List<Animal> GetAllAnimaisPerdidos()
        {
            List<Animal> result = new List<Animal>();

            foreach (var animal in _animais.Values)
            {
                if (animal.IsPerdido)
                    result.Add(animal.Copy());
            }

            return result;
        }

        public static List<Animal> GetAllAnimais()
        {
            List<Animal> result = new List<Animal>();

            foreach (var animal in _animais.Values)
                result.Add(animal.Copy());

            return result;
        }

        public static Animal GetAnimalById(int id)
        {
            return _animais[id];
        }

        public static List<Animal> GetAnimaisByDono(int donoId)
        {
            List<Animal> result = new List<Animal>();

            foreach (var animal in _animais.Values)
            {
                if (animal.DonoId.Equals(donoId))
                    result.Add(animal.Copy());
            }

            return result;
        }

        private static int _current = -1;

        public static void SetCurrentAnimal(int id)
        {
            _current = id;
        }

        public static Animal GetCurrentAnimal()
        {
            return _animais[_current];
        }
    }
}
