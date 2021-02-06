using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppFabric.API.Mock
{
    public static class DashboardMock
    {
        public static List<dynamic> GetProjectReleases()
        {
            var data = new List<dynamic>()
            {
                new
                {
                    name = "Projects",
                    data = new dynamic []
                    {
                        new { value= 6, name= "Portal do Cliente" },
                        new { value= 10, name="Sistema de controle de frota" },
                        new { value= 5, name= "Compliance" },
                        new { value= 4, name= "GED" },
                        new { value= 2, name= "SGRH" },
                    }
                }
            };

            return data;
        }

        public static List<dynamic> GetFinishedActivities()
        {
            var data = new List<dynamic>()
            {
                new
                {
                    labels = new List<string>(){ "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" },
                    data = new int [10, 52, 200, 334, 390, 330, 220],
                    name = "Score"
                }
            };
            return data;
        }

        public static List<dynamic> GetClientsRevenue()
        {
            var data = new List<dynamic>()
            {
                new {
                    name= "D&G Seguradora",
                    stack= "Total amount",
                    data=new int[120, 132, 101, 134, 90, 230, 210],
                },
                new {
                    name= "Locamais",
                    stack= "Total amount",
                    data= new int [220, 182, 191, 234, 290, 330, 310],
                },
                new {
                    name= "Fiat do Brasil LTDA.",
                    stack= "Total amount",
                    data= new int [150, 232, 201, 154, 190, 330, 410],
                },
                new {
                    name= "Tribunal Superior de Contas da União",
                    stack= "Total amount",
                    data=new int[320, 332, 301, 334, 390, 330, 320],
                },
                new {
                    name= "Granel",
                    stack= "Total amount",
                    data=new int[820, 932, 901, 934, 1290, 1330, 1320],
                }
            };

            return data;
        }

        public static List<dynamic> GetFavoritedProjects()
        {
            var data = new List<dynamic>()
            {
                new {
                  data = new { name= "Portal do Cliente" },
                  children = new dynamic[]
                  {
                    new { data= new { name= "project-1.doc" } },
                    new { data= new { name= "project-2.doc"} },
                    new { data= new { name= "project-3"} },
                    new { data= new { name= "project-4.docx" } },
                  }
                },
                new {
                  data= new { name= "Sistema de Controle de Qualidade", items= 2 },
                  children= new dynamic[]{
                    new { data= new { name= "Report 1" } },
                    new { data= new { name= "Report 2" } },
                  }
                },
                new {
                    data= new { name= "Compliance", items= 2 },
                    children= new dynamic[]
                    {
                        new { data = new { name = "backup.bkp" } },
                        new { data = new { name = "secret-note.txt" } },
                    }
                }
            };
            return data;
        }
    }
}
