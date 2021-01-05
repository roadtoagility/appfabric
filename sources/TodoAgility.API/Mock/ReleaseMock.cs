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
                    Client = "Locamais",
                    Status = "Aprovada",
                    TotalEffort = 24
                },
                new {
                    Id = 2,
                    Client = "D&G Seguradora",
                    Status = "Rejeitada",
                    TotalEffort = 80
                },
                new {
                    Id = 3,
                    Client = "Fiat do Brasil LTDA.",
                    Status = "Executada",
                    TotalEffort = 44
                },
                new {
                    Id = 4,
                    Client = "D&G Seguradora",
                    Status = "Executada",
                    TotalEffort = 120
                },
                new {
                    Id = 5,
                    Client = "Tribunal Superior de Contas da União",
                    Status = "Executada",
                    TotalEffort = 36
                },
                new {
                    Id = 6,
                    Client = "Locamais",
                    Status = "Aprovada",
                    TotalEffort = 210
                },
                new {
                    Id = 7,
                    Client = "Tribunal Superior de Contas da União",
                    Status = "Aguardando Aprovação",
                    TotalEffort = 300
                },
                new {
                    Id = 8,
                    Client = "Granel",
                    Status = "Executada",
                    TotalEffort = 108
                }
            };

            return releases;
        }
    }

    public class ReleaseDto
    {

    }
}
