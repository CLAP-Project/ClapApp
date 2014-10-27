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

        static AnimaisControl()
        {
            return;

            for (int i = 0; i < 20; ++i)
            {
                Status status = (i % 2 == 0)? Status.Perdido : Status.OK;

                switch (i % 3) {
                    case 0:
                        _animais.Add(i, new Animal()
                        {
                            Id = i,
                            Nome = "Guido",
                            Especie = "Gato",
                            Sexo = Sexo.Macho,
                            Status = status
                        });
                        break;

                    case 1:
                        _animais.Add(i, new Animal()
                        {
                            Id = i,
                            Nome = "Jack Tartaruga",
                            Especie = "Tartaruga",
                            Sexo = Sexo.Femea,
                            Status = status
                        });
                        break;

                    case 2:
                        _animais.Add(i, new Animal()
                        {
                            Id = i,
                            Nome = "Ligeirinho",
                            Especie = "Caracol",
                            Sexo = Sexo.Indefinido,
                            Status = status
                        });
                        break;

                    default:
                        break;
                }
            }
        }

        public static int InsertAnimal(Animal animal)
        {
            animal.Id = _inserted++;
            _animais.Add(animal.Id, animal);

            return animal.Id;
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
    }
}
