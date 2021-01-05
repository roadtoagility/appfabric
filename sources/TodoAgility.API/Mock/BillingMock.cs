using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAgility.API.Mock
{
    public static class BillingMock
    {
        public static List<dynamic> GetBillings()
        {
            var billings = new List<dynamic>()
            {
                new {
                    Id = 1,
                    Client= "Locamais",
                    Amout= 150000m,
                    Status= "Billed"
                },
                new {
                    Id = 2,
                    Client= "D&G Seguradora",
                    Amout= 400000m,
                    Status= "Not Billed"
                },
                new {
                    Id = 3,
                    Client= "Tribunal Superior de Contas da União",
                    Amout= 1000000m,
                    Status= "Billed"
                },
                new {
                    Id = 4,
                    Client= "Fiat do Brasil LTDA.",
                    Amout= 575000m,
                    Status= "Billed"
                },
            };

            return billings;
        }
    }
}
