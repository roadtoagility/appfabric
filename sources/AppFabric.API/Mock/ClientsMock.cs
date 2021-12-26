using System.Collections.Generic;

namespace AppFabric.API.Mock
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string EmailComercial { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Area { get; set; }
        public string ResponsavelComercial { get; set; }
        public string TelefoneComercial { get; set; }
    }

    public static class ClientsMock
    {
        public static List<dynamic> GetClients()
        {
            var clients = new List<dynamic>
            {
                new
                {
                    Id = 1,
                    RazaoSocial = "D&G Seguradora",
                    Cnpj = "95.900.099/0001-94",
                    EmailComercial = "comercial@deg.com.br"
                },
                new
                {
                    Id = 2,
                    RazaoSocial = "Locamais",
                    Cnpj = "35.458.325/0001-05",
                    EmailComercial = "comercial@locamais.com.br"
                },
                new
                {
                    Id = 3,
                    RazaoSocial = "Fiat do Brasil LTDA.",
                    Cnpj = "43.296.919/0001-87",
                    EmailComercial = "comercialfiat@fiatdobrasil.com.br"
                },
                new
                {
                    Id = 4,
                    RazaoSocial = "Tribunal Superior de Contas da União",
                    Cnpj = "43.296.919/0001-87",
                    EmailComercial = "sutec@tcu.com.br"
                },
                new
                {
                    Id = 5,
                    RazaoSocial = "Granel",
                    Cnpj = "43.296.919/0001-87",
                    EmailComercial = "contato@granel.com.br"
                }
            };

            return clients;
        }
    }
}