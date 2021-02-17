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
                    .With(up=> up.Id, Guid.Parse("117EB8AE-21EF-4049-B11C-36DB81D182E9")).Create(),
                DatabaseFixture.Build<UserProjection>()
                    .With(up=> up.Id, Guid.Parse("77E69C95-E50C-410A-AC07-14D1F0D5CCA0")).Create(),
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
                    .Create(),
                DatabaseFixture.Build<UserState>()
                    .With(up=> up.Id, Guid.Parse("1107FA5B-6DFC-45BB-9F58-5834F1F1A38C"))
                    .With(up=> up.RowVersion, BitConverter.GetBytes(1))
                    .Create(),
                DatabaseFixture.Build<UserState>()
                    .With(up=> up.Id, Guid.Parse("2E28CC8F-2127-4313-BA61-6063F8983585"))
                    .With(up=> up.RowVersion, BitConverter.GetBytes(1))
                    .Create(),
            };
        }
    }
}