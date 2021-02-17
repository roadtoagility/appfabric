using System;
using System.Collections.Generic;
using AppFabric.Persistence;
using AppFabric.Persistence.Model;
using AppFabric.Persistence.ReadModel;
using AutoFixture;

namespace AppFabric.Tests.Integration.Support
{
    public class IntegrationDataset
    {
        public Fixture DatabaseFixture { get; }
        public AppFabricDbContext DbContext { get; }

        public IntegrationDataset(AppFabricDbContext db)
        {
            DbContext = db;

            DatabaseFixture = new Fixture();
            
            DatabaseFixture.Register<UserState>(() => 
                    new UserState(DatabaseFixture.Create<Guid>(), 
                        DatabaseFixture.Create<string>(),
                        DatabaseFixture.Create<string>(),
                        string.Format($"{DatabaseFixture.Create<string>()}@teste.com"),
                        BitConverter.GetBytes(DatabaseFixture.Create<int>())));
            
            DatabaseFixture.Register<UserProjection>(() => 
                new UserProjection(DatabaseFixture.Create<Guid>(), 
                    DatabaseFixture.Create<string>(),
                    DatabaseFixture.Create<string>(),
                    string.Format($"{DatabaseFixture.Create<string>()}@teste.com"),
                    DatabaseFixture.Create<int>()));
            
        }
        public void InitializeDbForTests()
        {
            DbContext.Users.AddRange(GetSeedingUsersForDeletion());
            DbContext.UsersProjection.AddRange(GetSeedingUsersProjectionForDeleting());
            DbContext.UsersProjection.AddRange(GetSeedingUsersProjectionForListing());
            DbContext.UsersProjection.AddRange(GetSeedingUsersProjectionForFiltering());
            DbContext.SaveChanges();
        }

        public void ReinitializeDbForTests()
        {
            DbContext.Users.RemoveRange(DbContext.Users);
            DbContext.UsersProjection.RemoveRange(DbContext.UsersProjection);
            InitializeDbForTests();
        }

        public List<UserProjection> GetSeedingUsersProjectionForListing()
        {
            return new List<UserProjection>()
            {
                DatabaseFixture.Create<UserProjection>(),
                DatabaseFixture.Create<UserProjection>(),
                DatabaseFixture.Create<UserProjection>(),
                DatabaseFixture.Create<UserProjection>(),
                DatabaseFixture.Create<UserProjection>()
            };
        }
        
        public List<UserProjection> GetSeedingUsersProjectionForFiltering()
        {
            return new List<UserProjection>()
            {
                //para filtragem por id
                DatabaseFixture.Build<UserProjection>()
                    .With(up=> up.Id, Guid.Parse("81DC52BA-5D45-4E17-97EC-BEE71E459232")).Create(),
                DatabaseFixture.Build<UserProjection>()
                    .With(up=> up.Id, Guid.Parse("E2528E3F-601F-4B67-92BA-D9E27462006F")).Create()
            };
        }

        public List<UserState> GetSeedingUsersForDeletion()
        {
            return new List<UserState>()
            {
                DatabaseFixture.Build<UserState>()
                    .With(up=> up.Id, Guid.Parse("65CC91A2-267F-4FFE-8CE0-796AECD6AB4D"))
                    .With(up=> up.RowVersion, BitConverter.GetBytes(1))
                    .With(up=> up.CommercialEmail,string.Format($"{DatabaseFixture.Create<string>()}@teste.com"))
                    .Create()
            };
        }
        
        public List<UserProjection> GetSeedingUsersProjectionForDeleting()
        {
            return new List<UserProjection>()
            {
                //para filtragem por id
                DatabaseFixture.Build<UserProjection>()
                    .With(up=> up.Id, Guid.Parse("65CC91A2-267F-4FFE-8CE0-796AECD6AB4D"))
                    .With(up=> up.RowVersion, 1)
                    .With(up=> up.CommercialEmail,string.Format($"{DatabaseFixture.Create<string>()}@teste.com"))
                    .Create()
            };
        }
    }
}