using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAgility.API.Mock
{
    public static class ReleaseMock
    {
        public static List<dynamic> GetReleases()
        {
            var releases = new List<dynamic>()
            {
                new {
                    Id = 1,
                    ClientId = 1,
                    Client = "Locamais",
                    Status = "Aprovada",
                    TotalEffort = 24,
                    Resume = "Lorem Ipsum Dolor siamet"
                },
                new {
                    Id = 2,
                    ClientId = 2,
                    Client = "D&G Seguradora",
                    Status = "Rejeitada",
                    TotalEffort = 80,
                    Resume = "Lorem Ipsum Dolor siamet"
                },
                new {
                    Id = 3,
                    ClientId = 3,
                    Client = "Fiat do Brasil LTDA.",
                    Status = "Executada",
                    TotalEffort = 44,
                    Resume = "Lorem Ipsum Dolor siamet"
                },
                new {
                    Id = 4,
                    ClientId = 4,
                    Client = "D&G Seguradora",
                    Status = "Executada",
                    TotalEffort = 120,
                    Resume = "Lorem Ipsum Dolor siamet"
                },
                new {
                    Id = 5,
                    ClientId = 1,
                    Client = "Tribunal Superior de Contas da União",
                    Status = "Executada",
                    TotalEffort = 36,
                    Resume = "Lorem Ipsum Dolor siamet"
                },
                new {
                    Id = 6,
                    ClientId = 1,
                    Client = "Locamais",
                    Status = "Aprovada",
                    TotalEffort = 210,
                    Resume = "Lorem Ipsum Dolor siamet"
                },
                new {
                    Id = 7,
                    ClientId = 3,
                    Client = "Tribunal Superior de Contas da União",
                    Status = "Aguardando Aprovação",
                    TotalEffort = 300,
                    Resume = "Lorem Ipsum Dolor siamet"
                },
                new {
                    Id = 8,
                    ClientId = 3,
                    Client = "Granel",
                    Status = "Executada",
                    TotalEffort = 108,
                    Resume = "Lorem Ipsum Dolor siamet"
                }
            };

            return releases;
        }
    }

    public class ReleaseDto
    {

    }
}
