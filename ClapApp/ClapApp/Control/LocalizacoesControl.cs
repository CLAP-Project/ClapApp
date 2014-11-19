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
        private static int _inserted = 0;
        private static Dictionary<int, Localizacao> _localizacoes = new Dictionary<int, Localizacao>();

        public static int InsertLocalizacao(Localizacao localizacao)
        {
            localizacao = localizacao.Copy();
            localizacao.Id = _inserted++;

            _localizacoes.Add(localizacao.Id, localizacao);

            return localizacao.Id;
        }

        public static void UpdateLocalizacao(Localizacao localizacao) {
            _localizacoes[localizacao.Id].Assimilate(localizacao);
        }

        public static void EraseLocalizacaoById(int localizacaoId)
        {
            _localizacoes.Remove(localizacaoId);
        }

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

        public static List<Localizacao> GetAllLocalizacoesFromAnimaisPerdido()
        {
            List<Localizacao> result = new List<Localizacao>();
            
            foreach (var localizacao in _localizacoes.Values)
            {
                
                if (AnimaisControl.GetAnimalStatusById(localizacao.AnimalId) == Status.Perdido)
                    result.Add(localizacao.Copy());
            }

            result.Sort((Localizacao left, Localizacao right) =>
            {
                return right.DataHora.CompareTo(left.DataHora);
            });

            return result;
        }
    }
}
