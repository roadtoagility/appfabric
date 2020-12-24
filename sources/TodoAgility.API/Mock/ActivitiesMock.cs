using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAgility.API.Mock
{
    public class ActivityDto
    {
        public string Titulo { get; set; }
        public string Responsavel { get; set; }
        public int EsforcoHoras { get; set; }
        public string Projeto { get; set; }
        public int Status { get; set; }
    }

    public static class ActivitiesMock
    {
        public static List<dynamic> GetActivities()
        {
            var activities = new List<dynamic>()
            {
                new {
                    Id= 1,
                    Titulo= "Criar API Questão",
                    Projeto= "SISRH",
                    Responsavel= "Douglas Ramalho",
                    Esforco= 4,
                    Status= "Iniciado",
                  },
                new {
                    Id= 2,
                    Titulo= "Atualizar repositório",
                    Projeto= "SISRH",
                    Responsavel= "Adriano Ribeiro",
                    Esforco= 2,
                    Status= "Não Iniciado",
                  },
                new {
                    Id= 3,
                    Titulo= "Criar estrutura do banco",
                    Projeto= "SISRH",
                    Responsavel= "Flávio Henrique",
                    Esforco= 8,
                    Status= "Iniciado",
                  },
                new {
                Id= 4,
                    Titulo= "Mapear entIdade",
                    Projeto= "SISRH",
                    Responsavel= "Fernando Ribeiro",
                    Esforco= 3,
                    Status= "Concluído",
                  },
                new {
                Id= 5,
                    Titulo= "Criar DML e DDL para integração",
                    Projeto= "SISRH",
                    Responsavel= "Douglas Ramalho",
                    Esforco= 3,
                    Status= "Concluído",
                  },
                new {
                Id= 6,
                    Titulo= "Criar ambiente de HML",
                    Projeto= "SISRH",
                    Responsavel= "Douglas Ramalho",
                    Esforco= 2,
                    Status= "Concluído",
                  },
                new {
                Id= 7,
                    Titulo= "Atualizar plano de implantação",
                    Projeto= "SISRH",
                    Responsavel= "Adriano Ribeiro",
                    Esforco= 2,
                    Status= "Iniciado",
                  },
                new {
                Id= 8,
                    Titulo= "Elaborar plano de testes",
                    Projeto= "SISRH",
                    Responsavel= "Flávio Henrique",
                    Esforco= 8,
                    Status= "Não Iniciado",
                  },
                new {
                Id= 9,
                    Titulo= "Adicionar interface de criação questão",
                    Projeto= "SISRH",
                    Responsavel= "Fernando Ribeiro",
                    Esforco= 8,
                    Status= "Não Iniciado",
                  },
                new
                {
                    Id= 10,
                    Titulo= "Remover pacotes desnecessários",
                    Projeto= "SISRH",
                    Responsavel= "Flávio Henrique",
                    Esforco= 2,
                    Status= "Não Iniciado",
                  },
                  new
                  {
                      Id= 11,
                    Titulo= "Criar PR para master",
                    Projeto= "SISRH",
                    Responsavel= "Douglas Ramalho",
                    Esforco= 1,
                    Status= "Não Iniciado",
                  }
            };
            return activities;
        }
    }
}
