using System;
using System.Collections.Generic;

namespace AppFabric.API.Mock
{
    public class ProjectDto
    {
        public string NomeProjeto { get; set; }
        public string Sigla { get; set; }
        public DateTime DataInicio { get; set; }
        public bool Favorito { get; set; }
        public decimal Budget { get; set; }
        public decimal CustoHora { get; set; }
        public decimal MargemLucro { get; set; }
        public int StatusOs { get; set; }
        public int IdClient { get; set; }
        public string EmailResponsavel { get; set; }
        public string NumeroOrdemServico { get; set; }
    }

    public static class ProjectsMock
    {
        public static List<dynamic> GetProjects()
        {
            var projects = new List<dynamic>
            {
                new
                {
                    Id = 1,
                    Cliente = "Locamais",
                    ClientId = 2,
                    Nome = "Portal do Cliente",
                    Sigla = "PTC",
                    Gerente = "Marcio Alberto",
                    StatusOrdemServico = "Aprovado"
                },
                new
                {
                    Id = 2,
                    Cliente = "Locamais",
                    ClientId = 2,
                    Nome = "Sistema de controle de frota",
                    Sigla = "SCF",
                    Gerente = "Marcio Alberto",
                    StatusOrdemServico = "Aprovado"
                },
                new
                {
                    Id = 3,
                    Cliente = "D&G Seguradora",
                    ClientId = 1,
                    Nome = "Portal do Segurado",
                    Sigla = "PTS",
                    Gerente = "Ana Catarina",
                    StatusOrdemServico = "Pendente de aprovação"
                },
                new
                {
                    Id = 4,
                    Cliente = "Fiat do Brasil LTDA.",
                    ClientId = 3,
                    Nome = "Sistema de Controle de Qualidade",
                    Sigla = "SCQ",
                    Gerente = "Renata Soares",
                    StatusOrdemServico = "Encerrado"
                },
                new
                {
                    Id = 5,
                    Cliente = "Fiat do Brasil LTDA.",
                    ClientId = 3,
                    Nome = "Compliance",
                    Sigla = "Compliance",
                    Gerente = "Renata Soares",
                    StatusOrdemServico = "Pendente de OS"
                },
                new
                {
                    Id = 6,
                    Cliente = "Tribunal Superior de Contas da União",
                    ClientId = 4,
                    Nome = "Gerenciamento Eletrônico de documentos",
                    Sigla = "GED",
                    Gerente = "Felipe Dias",
                    StatusOrdemServico = "Aprovado"
                },
                new
                {
                    Id = 7,
                    Cliente = "Tribunal Superior de Contas da União",
                    ClientId = 4,
                    Nome = "Sistema de Gerenciamento de Recursos Humanos",
                    Sigla = "SGRH",
                    Gerente = "Felipe Dias",
                    StatusOrdemServico = "Aprovado"
                }
            };

            return projects;
        }
    }
}