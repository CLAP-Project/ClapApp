﻿using ClapApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClapApp.Control
{
    static class PerfisControl
    {
        private static int _inserted = 0;
        private static Dictionary<int, Perfil> _perfis = new Dictionary<int,Perfil>();

        private static void make(Perfil perfil, NumeroTelefonico[] numeros, params Animal[] animais)
        {
            var id = InsertPerfil(perfil);

            foreach (var numero in numeros)
            {
                numero.DonoId = id;
                NumerosControl.InsertNumero(numero);
            }

            foreach (var animal in animais)
            {
                animal.DonoId = id;
                AnimaisControl.InsertAnimal(animal);
            }
        }

        public static void PopulateExamples()
        {
            make(new Perfil()
            {
                Nome = "Mox",
                Sobrenome = "Wolliamsa",
                Cidade = "Manaus",
                Estado = "Amazonas",
                Email = "mox@a.com",
                Senha = "mox"
            },
            new NumeroTelefonico[] {
                new NumeroTelefonico() { DDD = "92", Numero = "81009100" },
                new NumeroTelefonico() { DDD = "92", Numero = "82209100" },
                new NumeroTelefonico() { DDD = "92", Numero = "83009110" },
                new NumeroTelefonico() { DDD = "92", Numero = "84039100" },
                new NumeroTelefonico() { DDD = "92", Numero = "85009100" },
                new NumeroTelefonico() { DDD = "92", Numero = "86009100" },
                new NumeroTelefonico() { DDD = "92", Numero = "87009100" },
                new NumeroTelefonico() { DDD = "92", Numero = "88009100" },
                new NumeroTelefonico() { DDD = "92", Numero = "89009100" },
                new NumeroTelefonico() { DDD = "92", Numero = "89909100" },
                new NumeroTelefonico() { DDD = "92", Numero = "81009200" }
            },
            new Animal()
            {
                Nome = "Ligeirinho",
                Especie = "Caramujo",
                Sexo = Sexo.Indefinido,
                Status = Status.Perdido
            },
            new Animal()
            {
                Nome = "Felpuda",
                Especie = "Gato",
                Sexo = Sexo.Femea,
                Status = Status.OK
            },
            new Animal()
            {
                Nome = "Magalhães",
                Especie = "Barata",
                Sexo = Sexo.Indefinido,
                Status = Status.OK
            });

            make(new Perfil()
            {
                Nome = "Timóteo",
                Sobrenome = "Santos",
                Cidade = "Manaus",
                Estado = "Amazonas",
                Email = "tim@a.com",
                Senha = "tim"
            },
            new NumeroTelefonico[] {
                new NumeroTelefonico() { DDD = "92", Numero = "83009300" }
            },
            new Animal()
            {
                Nome = "Guido",
                Especie = "Gato",
                Sexo = Sexo.Macho,
                Status = Status.OK
            },
            new Animal()
            {
                Nome = "Dory",
                Especie = "Cachorro",
                Sexo = Sexo.Femea,
                Status = Status.Perdido
            },
            new Animal()
            {
                Nome = "Cosmo",
                Especie = "Peixe beta",
                Sexo = Sexo.Macho,
                Status = Status.Perdido
            },
            new Animal()
            {
                Nome = "Jack Tartaruga",
                Especie = "Cágado",
                Sexo = Sexo.Macho,
                Status = Status.Perdido
            });

            make(new Perfil()
            {
                Nome = "Rubêm",
                Sobrenome = "Belêm",
                Cidade = "Manaus",
                Estado = "Amazonas",
                Email = "rub@a.com",
                Senha = "rub"
            },
            new NumeroTelefonico[] {
                new NumeroTelefonico() { DDD = "92", Numero = "84009400" }
            },
            new Animal()
            {
                Nome = "Didu",
                Especie = "Cachorro",
                Sexo = Sexo.Macho,
                Status = Status.Perdido
            },
            new Animal()
            {
                Nome = "Amanito",
                Especie = "Cogumelo",
                Sexo = Sexo.Macho,
                Status = Status.Perdido
            });

            make(new Perfil()
            {
                Nome = "Haydê",
                Sobrenome = "Cristhine",
                Cidade = "Manaus",
                Estado = "Amazonas",
                Email = "hay@a.com",
                Senha = "hay"
            },
            new NumeroTelefonico[] {
                new NumeroTelefonico() { DDD = "92", Numero = "85009500" }
            },
            new Animal()
            {
                Nome = "Pulguinha 1",
                Especie = "Pulga",
                Sexo = Sexo.Femea,
                Status = Status.Perdido
            },
            new Animal()
            {
                Nome = "Pulguinha 2",
                Especie = "Pulga",
                Sexo = Sexo.Femea,
                Status = Status.Perdido
            });
        }

        // ---

        public static int InsertPerfil(Perfil perfil)
        {
            perfil = perfil.Copy();
            perfil.Id = _inserted++;

            _perfis.Add(perfil.Id, perfil);

            return perfil.Id;
        }

        public static void UpdatePerfil(Perfil perfil)
        {
            _perfis[perfil.Id].Assimilate(perfil);
        }

        public static Perfil GetPerfilById(int id)
        {
            return _perfis[id].Copy();
        }

        // ---

        private static Perfil _editing = null;
        private static bool _creating = false;

        public static void BeginEditing()
        {
            _editing = GetCurrentUsuarioPerfil();
            _creating = false;
        }

        public static void BeginCreating(Perfil perfil)
        {
            _editing = perfil.Copy();
            _creating = true;
        }

        public static void SaveEditing()
        {
            if (_creating)
            {
                InsertPerfil(_editing);
            }
            else UpdatePerfil(_editing);
        }

        public static void FinishEditing()
        {
            _editing = null;
            _creating = false;
        }

        public static Perfil GetEditingPerfil()
        {
            return _editing;
        }

        public static bool IsCreating()
        {
            return _creating;
        }

        // ---

        private static int _currentUsuarioId = -1;

        public static Perfil GetCurrentUsuarioPerfil()
        {
            return _editing ?? GetPerfilById(_currentUsuarioId);
        }

        public static void SetCurrentUsuario(int perfilId)
        {
            _currentUsuarioId = perfilId;
        }

        // ---

        private static int _loggedUsuarioId = -1;

        public static bool IsCurrentUsuarioLoggedIn()
        {
            return _currentUsuarioId.Equals(_loggedUsuarioId);
        }

        public static int GetLoggedUsuarioId()
        {
            return _loggedUsuarioId;
        }

        public static Perfil GetLoggedUsuarioPerfil()
        {
            return GetPerfilById(_loggedUsuarioId);
        }

        public static bool IsLoggedInPerfil(int perfilId)
        {
            return _loggedUsuarioId.Equals(perfilId);
        }

        public static bool Login(string email, string senha)
        {
            foreach (var perfil in _perfis.Values)
            {
                if (perfil.Email.Equals(email) && perfil.Senha.Equals(senha))
                {
                    _loggedUsuarioId = perfil.Id;
                    return true;
                }
            }

            return false;
        }
    }
}