using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClapApp.Model
{
    public class AnimalLocalizacoes
    {
        private Animal _animal;
        private Localizacao[] _localizacoes;

        public AnimalLocalizacoes(Animal animal, Localizacao[] localizacoes)
        {
            _animal = animal;
            _localizacoes = localizacoes;
        }

        public Animal Animal
        {
            get
            {
                return _animal;
            }
            set
            {
                _animal = value;
            }
        }

        public Localizacao[] Localizacoes
        {
            get
            {
                return _localizacoes;
            }
            set
            {
                _localizacoes = value;
            }
        }
    }
}
