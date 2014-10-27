using ClapApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClapApp.Control
{
    public class NumeroJaCadastradoException : Exception
    {

    }

    public static class NumerosControl
    {
        private static int _inserted = 0;
        private static Dictionary<int, NumeroTelefonico> _numeros = new Dictionary<int, NumeroTelefonico>();

        // ---

        public static int InsertNumero(NumeroTelefonico numero)
        {
            numero = numero.Copy();
            numero.Id = _inserted++;

            _numeros.Add(numero.Id, numero);

            return numero.Id;
        }

        public static void UpdateNumero(NumeroTelefonico numero)
        {
            _numeros[numero.Id].Assimilate(numero);
        }

        public static void EraseNumeroById(int numeroId)
        {
            _numeros.Remove(numeroId);
        }

        // ---

        private static NumeroTelefonico _editing = null;
        private static int _creatingDono = -1;

        public static void BeginEditing(NumeroTelefonico numero)
        {
            _editing = numero.Copy();
            _creatingDono = -1;
        }

        public static void BeginCreating(NumeroTelefonico numero, int donoId)
        {
            _editing = numero.Copy();
            _creatingDono = donoId;
        }

        public static bool IsCreating()
        {
            return _creatingDono != -1;
        }

        public static void SaveEditing()
        {
            if (IsCreating())
            {
                if (!PerfisControl.GetPerfilById(_creatingDono).AddNumero(_editing))
                    throw new NumeroJaCadastradoException();
            }
            else if (GetNumeroByValue(_editing.DonoId, _editing.DDD, _editing.Numero) == null)
            {
                UpdateNumero(_editing);
            }
            else throw new NumeroJaCadastradoException();
        }

        public static void FinishEditing()
        {
            _editing = null;
            _creatingDono = -1;
        }

        public static NumeroTelefonico GetEditingNumero()
        {
            return _editing;
        }

        // ---

        public static List<NumeroTelefonico> GetAllNumeros()
        {
            List<NumeroTelefonico> result = new List<NumeroTelefonico>();

            foreach (var numero in _numeros.Values)
                result.Add(numero.Copy());

            return result;
        }

        public static List<NumeroTelefonico> GetNumerosByDono(int donoId)
        {
            List<NumeroTelefonico> result = new List<NumeroTelefonico>();

            foreach (var numero in _numeros.Values)
            {
                if (numero.DonoId.Equals(donoId))
                    result.Add(numero.Copy());
            }

            return result;
        }

        public static NumeroTelefonico GetNumeroById(int id)
        {
            return _numeros[id];
        }

        public static NumeroTelefonico GetNumeroByValue(int donoId, string DDD, string numeroStr)
        {
            foreach (var numero in _numeros.Values)
            {
                if (numero.DonoId.Equals(donoId) &&
                    numero.DDD.Equals(DDD) &&
                    numero.Numero.Equals(numeroStr))
                {
                    return numero;
                }
            }

            return null;
        }
    }
}
