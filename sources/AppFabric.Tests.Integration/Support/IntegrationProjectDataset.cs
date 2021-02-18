using System;
using System.Collections.Generic;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Persistence;
using AppFabric.Persistence.Model;
using AppFabric.Persistence.ReadModel;
using AutoFixture;

namespace AppFabric.Tests.Integration.Support
{
    public class IntegrationProjectDataset
    {
        public Fixture DatabaseFixture { get; }
        public AppFabricDbContext DbContext { get; }

        public IntegrationProjectDataset(AppFabricDbContext db)
        {
            DbContext = db;

            DatabaseFixture = new Fixture();
            
            DatabaseFixture.Register<ProjectState>(() => 
                    new ProjectState(DatabaseFixture.Create<Guid>(),
                        DatabaseFixture.Create<string>(),
                        DatabaseFixture.Create<string>(),
                        DatabaseFixture.Create<decimal>(),
                    DatabaseFixture.Create<DateTime>(),
                        DatabaseFixture.Create<Guid>(),
                        string.Format($"{DatabaseFixture.Create<string>()}@teste.com"),
                        DatabaseFixture.Create<string>(),
                        ProjectStatus.Default().Value,
                BitConverter.GetBytes(DatabaseFixture.Create<int>())));
            
            DatabaseFixture.Register<ProjectProjection>(() => 
                new ProjectProjection(DatabaseFixture.Create<Guid>(), 
                    DatabaseFixture.Create<string>(),
                    DatabaseFixture.Create<string>(),
                    DatabaseFixture.Create<decimal>(),
                    DatabaseFixture.Create<DateTime>(),
                    DatabaseFixture.Create<Guid>(),
                    DatabaseFixture.Create<string>(),
                    string.Format($"{DatabaseFixture.Create<string>()}@teste.com"),
                    DatabaseFixture.Create<string>(),
                    DatabaseFixture.Create<int>(),
                    DatabaseFixture.Create<string>(),
                     DatabaseFixture.Create<int>()));
                    
            
        }
        public void InitializeDbForTests()
        {
            DbContext.Projects.AddRange(GetSeedingProjectForDeleting());
            DbContext.Projects.AddRange(GetSeedingProjectForEditing());
            DbContext.Users.AddRange(GetSeedingUsersForProjectIsertion());
            
            DbContext.UsersProjection.AddRange(GetSeedingUsersProjectionForProjectIsertion());
            DbContext.ProjectsProjection.AddRange(GetSeedingProjectProjectionForEditing());
            DbContext.ProjectsProjection.AddRange(GetSeedingProjectProjectionForListing());
            DbContext.ProjectsProjection.AddRange(GetSeedingProjectProjectionForFiltering());
            DbContext.ProjectsProjection.AddRange(GetSeedingProjectProjectionForDeleting());

            
            DbContext.SaveChanges();
        }

        public void ReinitializeDbForTests()
        {
            DbContext.Users.RemoveRange(DbContext.Users);
            DbContext.UsersProjection.RemoveRange(DbContext.UsersProjection);
            InitializeDbForTests();
        }

        public List<UserProjection> GetSeedingUsersProjectionForProjectIsertion()
        {
            return new List<UserProjection>()
            {
                DatabaseFixture.Build<UserProjection>()
                    .With(up=> up.Id, Guid.Parse("232C32F1-5A69-43FF-8FFB-360E1EF6A08E"))
                    .With(up=> up.RowVersion, 1)
                    .With(up=> up.CommercialEmail,string.Format($"{DatabaseFixture.Create<string>()}@teste.com"))
                    .Create()
            };
        }

        public List<UserState> GetSeedingUsersForProjectIsertion()
        {
            return new List<UserState>()
            {
                DatabaseFixture.Build<UserState>()
                    .With(up=> up.Id, Guid.Parse("232C32F1-5A69-43FF-8FFB-360E1EF6A08E"))
                    .With(up=> up.RowVersion, BitConverter.GetBytes(1))
                    .With(up=> up.CommercialEmail,string.Format($"{DatabaseFixture.Create<string>()}@teste.com"))
                    .Create()
            };
        }
        
        
        public List<ProjectProjection> GetSeedingProjectProjectionForListing()
        {
            return new List<ProjectProjection>()
            {
                DatabaseFixture.Create<ProjectProjection>(),
                DatabaseFixture.Create<ProjectProjection>(),
                DatabaseFixture.Create<ProjectProjection>(),
                DatabaseFixture.Create<ProjectProjection>(),
                DatabaseFixture.Create<ProjectProjection>()
            };
        }

        public List<ProjectProjection> GetSeedingProjectProjectionForFiltering()
        {
            return new List<ProjectProjection>()
            {
                //para filtragem por id
                DatabaseFixture.Build<ProjectProjection>()
                    .With(up=> up.Id, Guid.Parse("DEF2A92E-A53E-4754-B811-2C0C7C7858FA"))
                    .With(up=> up.Status, 1)
                    .With(up=> up.Owner,string.Format($"{DatabaseFixture.Create<string>()}@teste.com"))

                    .Create(),
                DatabaseFixture.Build<ProjectProjection>()
                    .With(up=> up.Id, Guid.Parse("11263366-D54A-4E7A-B820-D8894B6C5362"))
                    .With(up=> up.Status, 1)
                    .With(up=> up.Owner,string.Format($"{DatabaseFixture.Create<string>()}@teste.com"))

                    .Create()
            };
        }
        
        public List<ProjectState> GetSeedingProjectForEditing()
        {
            return new List<ProjectState>()
            {
                //para filtragem por id
                DatabaseFixture.Build<ProjectState>()
                    .With(up=> up.Id, Guid.Parse("7D74E1C4-3C35-47B9-B17B-7D5F9D9DFCF6"))
                    .With(up=> up.RowVersion, BitConverter.GetBytes(1))
                    .With(up=> up.Status, 1)
                    .With(up=> up.Owner,string.Format($"{DatabaseFixture.Create<string>()}@teste.com"))

                    .Create()
            };
        }

        public List<ProjectProjection> GetSeedingProjectProjectionForEditing()
        {
            return new List<ProjectProjection>()
            {
                //para filtragem por id
                DatabaseFixture.Build<ProjectProjection>()
              
                    .With(up => up.Id, Guid.Parse("7D74E1C4-3C35-47B9-B17B-7D5F9D9DFCF6"))
                    .With(up => up.Status, 1)
                    .With(up => up.Owner, string.Format($"{DatabaseFixture.Create<string>()}@teste.com"))

                    .Create()
            };
        }
        
        public List<ProjectState> GetSeedingProjectForDeleting()
        {
            return new List<ProjectState>()
            {
                //para filtragem por id
                DatabaseFixture.Build<ProjectState>()
                    .With(up=> up.Id, Guid.Parse("41557BC9-1809-4B0F-B8E7-4F61CC06D2C9"))
                    .With(up=> up.RowVersion, BitConverter.GetBytes(1))
                    .With(up=> up.Status, 1)
                    .With(up=> up.Owner,string.Format($"{DatabaseFixture.Create<string>()}@teste.com"))

                    .Create()
            };
        }
        
        public List<ProjectProjection> GetSeedingProjectProjectionForDeleting()
        {
            return new List<ProjectProjection>()
            {
                //para filtragem por id
                DatabaseFixture.Build<ProjectProjection>()
              
                    .With(up => up.Id, Guid.Parse("41557BC9-1809-4B0F-B8E7-4F61CC06D2C9"))
                    .With(up => up.Status, 1)
                    .With(up => up.Owner, string.Format($"{DatabaseFixture.Create<string>()}@teste.com"))

                    .Create()
            };
        }
    }
}