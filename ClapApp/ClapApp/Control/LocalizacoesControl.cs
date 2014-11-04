using ClapApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClapApp.Control
{
    public static class LocalizacoesControl
    {
        private static Dictionary<int, Localizacao> _localizacoes = new Dictionary<int, Localizacao>();

        public static List<Localizacao> GetLocalizacoesByAnimal(int animalId)
        {
            List<Localizacao> result = new List<Localizacao>();

            foreach (var localizacao in _localizacoes.Values)
            {
                if (localizacao.AnimalId.Equals(animalId))
                    result.Add(localizacao.Copy());
            }

            result.Sort((Localizacao left, Localizacao right) =>
            {
                return left.DataHora.CompareTo(right.DataHora);
            });

            return result;
        }
    }
}
