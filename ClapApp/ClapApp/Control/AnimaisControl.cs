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

        public static void UpdateAnimal(Animal animal)
        {
            _animais[animal.Id].Assimilate(animal);
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

        // ---

        private static Animal _editing = null;
        private static int _creatingDono = -1;

        public static void BeginEditing(Animal animal)
        {
            _editing = animal;
            _creatingDono = -1;
        }

        public static void BeginCreating(Animal animal, int donoId)
        {
            _editing = animal;
            _creatingDono = donoId;
        }

        public static bool IsCreating()
        {
            return _creatingDono != -1;
        }

        public static Animal GetEditingAnimal()
        {
            return _editing;
        }

        public static void SaveEditing()
        {
            if (IsCreating())
            {
                _editing.Id = _creatingDono;
                InsertAnimal(_editing);
            }
            else UpdateAnimal(_editing);
        }

        public static void FinishEditing()
        {
            _editing = null;
            _creatingDono = -1;
        }
    }
}
