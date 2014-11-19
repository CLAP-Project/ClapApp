using ClapApp.Model;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClapApp.Control
{
    static class PerfisControl
    {
        private static int _inserted = 0;
        private static Dictionary<int, Perfil> _perfis = new Dictionary<int, Perfil>();

        private static void make(Perfil perfil, NumeroTelefonico[] numeros, params AnimalLocalizacoes[] animaislocs)
        {
            var id = InsertPerfil(perfil);
            int animalId = 0;

            foreach (var numero in numeros)
            {
                numero.DonoId = id;
                NumerosControl.InsertNumero(numero);
            }

            if (animaislocs != null)
            {
                foreach (AnimalLocalizacoes animalloc in animaislocs)
                {
                    if (animalloc.Animal != null) //Têm coisas a melhorar nessa parte do código, essas validações de null e tal, mas não vou mexer com isso não. O foco hoje, 12/11/2014 é nas localizações no mapa.
                    {
                        animalloc.Animal.DonoId = id;
                        animalId = AnimaisControl.InsertAnimal(animalloc.Animal);
                    }

                    if (animalloc.Localizacoes != null)
                    {
                        foreach (Localizacao localizacao in animalloc.Localizacoes)
                        {
                            localizacao.AnimalId = animalId;
                            LocalizacoesControl.InsertLocalizacao(localizacao);
                        }
                    }
                }
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
                Senha = "mox",
                SetImageByPath = "../Images/max.jpg"
            },
            new NumeroTelefonico[] {
                new NumeroTelefonico() { DDD = "92", Numero = "81009100" },
                new NumeroTelefonico() { DDD = "92", Numero = "82209100" },
                new NumeroTelefonico() { DDD = "92", Numero = "83009110" }
            },

            new AnimalLocalizacoes(new Animal()
            {
                Nome = "Ligeirinho",
                Especie = "Caramujo",
                SetImageGambs = "../Images/caramujo.jpg",
                Sexo = Sexo.Indefinido,
                Status = Status.Perdido,
                Descricao = "O caramujo mais ligeiro na face da Terra. Ele consegue te passar esquistossomose sem que você veja quem foi!"
            }, new Localizacao[] {
                new Localizacao() { Coordenada = new GeoCoordinate(-3.1356065, -59.9909563), DataHora = new DateTime(2014, 10, 9, 12, 30, 00) },
                new Localizacao() { Coordenada = new GeoCoordinate(-3.1350387, -59.9908061), DataHora = new DateTime(2014, 10, 9, 12, 50, 05) },
                new Localizacao() { Coordenada = new GeoCoordinate(-3.1352315, -59.9897976), DataHora = new DateTime(2014, 10, 9, 13, 10, 20) },
                new Localizacao() { Coordenada = new GeoCoordinate(-3.135585, -59.9886711), DataHora = new DateTime(2014, 10, 9, 13, 30, 10) },
                new Localizacao() { Coordenada = new GeoCoordinate(-3.1348137, -59.9882205), DataHora = new DateTime(2014, 10, 9, 13, 50, 10) }
            }),
            new AnimalLocalizacoes(new Animal()
            {
                Nome = "Felpuda",
                Especie = "Gato",
                Sexo = Sexo.Femea,
                Status = Status.OK,
                Descricao = "Gata velha e careca, tem só alguns fiapinhos de pelo. Vai morrer já já."
            }, null),
            new AnimalLocalizacoes(new Animal()
            {
                Nome = "Magalhães",
                Especie = "Barata",
                Sexo = Sexo.Indefinido,
                Status = Status.OK,
                Descricao = "A barata mais preta que você já viu na sua vida. Ela tem só cinco pernas e meia antena."
            }, null));

            make(new Perfil()
            {
                Nome = "Timóteo",
                Sobrenome = "Santos",
                Cidade = "Manaus",
                Estado = "Amazonas",
                Email = "tim@a.com",
                Senha = "tim",
                SetImageByPath = "../Images/timoteo2.jpg"
            },
            new NumeroTelefonico[] {
                new NumeroTelefonico() { DDD = "92", Numero = "83009300" }
            },
            new AnimalLocalizacoes(new Animal()
            {
                Nome = "Clarice",
                Especie = "Ovelha",
                Sexo = Sexo.Femea,
                Status = Status.OK,
                Descricao = "Uma ovelha branquinha e felpuda. Responde pelo nome e é muito dócil, embora tímida diante de estranhos.",
                SetImageGambs = "../Images/ovelha.jpg"
            }, new Localizacao[] {
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0895888, -60.0344724), DataHora = DateTime.Today},
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0895352, -60.0336892), DataHora = DateTime.Today},
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0888174, -60.0333888), DataHora = DateTime.Today},
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0879068, -60.0335926), DataHora = DateTime.Today},
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0872533, -60.0337321), DataHora = DateTime.Today}
            }),
            new AnimalLocalizacoes(new Animal()
            {
                Nome = "Guido",
                Especie = "Gato",
                Sexo = Sexo.Macho,
                Status = Status.Perdido,
                Descricao = "Gato com pelagem de \"tuxedo\", costas pretas, \"máscara\" preta no rosto, mancha preta no queixo que lembra uma barbicha. Olhos amarelos. Muito gordo. Sua cauda tem a ponta torta.",
                SetImageGambs = "../Images/guido.jpg"
            }, new Localizacao[] {
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0895888, -60.0344724), DataHora = DateTime.Today},
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0895352, -60.0336892), DataHora = DateTime.Today},
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0888174, -60.0333888), DataHora = DateTime.Today},
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0879068, -60.0335926), DataHora = DateTime.Today},
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0872533, -60.0337321), DataHora = DateTime.Today}
            }),
            new AnimalLocalizacoes(new Animal()
            {
                Nome = "Dory",
                Especie = "Cachorro",
                SetImageGambs = "../Images/dory.jpg",
                Sexo = Sexo.Femea,
                Status = Status.OK,
                Descricao = "Cadela schnauzer de pelo preto e patas brancas. Tem a cauda cortada. Responde pelo nome."
            }, null),
            new AnimalLocalizacoes(new Animal()
            {
                Nome = "Cosmo",
                Especie = "Peixe beta",
                SetImageGambs = "../Images/peixebeta.jpg",
                Sexo = Sexo.Macho,
                Status = Status.Perdido,
                Descricao = "É um peixe multicolorido que sabe fazer malabarismo e responde pelo nome."
            }, null),
            new AnimalLocalizacoes(new Animal()
            {
                Nome = "Jack Tartaruga",
                Especie = "Cágado",
                Sexo = Sexo.Macho,
                SetImageGambs = "../Images/cagado.jpg",
                Status = Status.OK,
                Descricao = "Essa é tartaruguinha mais louca do pedaço. Ela vai aprontar altas confusões que deixarão de pernas para o ar uma turma muito doida!"
            }, null));

            make(new Perfil()
            {
                Nome = "Rúben",
                Sobrenome = "Belém",
                Cidade = "Manaus",
                Estado = "Amazonas",
                Email = "rub@a.com",
                Senha = "rub",
                SetImageByPath = "../Images/ruben.jpg"
            },
            new NumeroTelefonico[] {
                new NumeroTelefonico() { DDD = "92", Numero = "84009400" }
            },

            new AnimalLocalizacoes(new Animal()
            {
                Nome = "Didu",
                Especie = "Cachorro",
                Sexo = Sexo.Macho,
                Status = Status.Perdido,
                SetImageGambs = "../Images/didu.jpg",
                Descricao = "Vira-lata que sabe voar com as orelhas. Ele traz o jornal sempre que mandado e adora Doritos, assim como o dono."
            }, new Localizacao[] {
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0774366, -60.0402115), DataHora = new DateTime(2014, 9, 3, 20, 7, 0)},
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0778973, -60.0406407), DataHora = new DateTime(2014, 9, 3, 20, 2, 10)},
                new Localizacao() { Coordenada = new GeoCoordinate(-3.0786258, -60.0405656), DataHora = new DateTime(2014, 9, 3, 20, 4, 20)}
            }),
            new AnimalLocalizacoes(new Animal()
            {
                Nome = "Amanito",
                Especie = "Cogumelo",
                Sexo = Sexo.Macho,
                Status = Status.Perdido,
                SetImageGambs = "../Images/amanita.jpg",
                Descricao = "Cultivado desde que era um esporinho no meu pescoço, não é agressivo com estranhos, mas pode liberar esporos venenosos se provocado. Odeia luz."
            }, null));

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
            new AnimalLocalizacoes(new Animal()
            {
                Nome = "Pulguinha 1",
                Especie = "Pulga",
                Sexo = Sexo.Femea,
                SetImageGambs = "../Images/pulgafemea.jpg",
                Status = Status.Perdido,
                Descricao = "Ela gosta muito de cabelo, estou começando a achar que talvez ela seja um piolho!"
            }, null),
            new AnimalLocalizacoes(new Animal()
            {
                Nome = "Pulguinha 2",
                Especie = "Pulga",
                Sexo = Sexo.Femea,
                SetImageGambs = "../Images/pulgamacho.jpg",
                Status = Status.Perdido,
                Descricao = "Sabe muitos truques, como pular por dentro de aros e assobiar enquanto chupa cana. Seu tipo de sangue favorito é O+."
            }, null));
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

        public static void Logout()
        {
            _loggedUsuarioId = _currentUsuarioId = -1;
        }

        public static bool IsLoggedIn()
        {
            return _loggedUsuarioId != -1;
        }
    }
}
