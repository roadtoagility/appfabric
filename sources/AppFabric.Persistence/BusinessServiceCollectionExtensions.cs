// Copyright (C) 2021  Road to Agility
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Library General Public
// License as published by the Free Software Foundation; either
// version 2 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Library General Public License for more details.
//
// You should have received a copy of the GNU Library General Public
// License along with this library; if not, write to the
// Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
// Boston, MA  02110-1301, USA.
//

using AppFabric.Persistence.Model.Repositories;
using AppFabric.Persistence.ReadModel.Repositories;
using DFlow.Persistence;
using DFlow.Persistence.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace AppFabric.Persistence
{
    public static class BusinessServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectProjectionRepository, ProjectProjectionRepository>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserProjectionRepository, UserProjectionRepository>();

            services.AddScoped<IDbSession<IProjectRepository>, DbSession<IProjectRepository>>();
            services.AddScoped<IDbSession<IProjectProjectionRepository>, DbSession<IProjectProjectionRepository>>();

            services.AddScoped<IDbSession<IUserRepository>, DbSession<IUserRepository>>();
            services.AddScoped<IDbSession<IUserProjectionRepository>, DbSession<IUserProjectionRepository>>();

            services.AddScoped<IDbSession<IActivityRepository>, DbSession<IActivityRepository>>();
            services.AddScoped<IDbSession<IActivityProjectionRepository>, DbSession<IActivityProjectionRepository>>();

            services.AddScoped<IDbSession<IBillingRepository>, DbSession<IBillingRepository>>();
            services.AddScoped<IDbSession<IBillingProjectionRepository>, DbSession<IBillingProjectionRepository>>();

            services.AddScoped<IDbSession<IReleaseRepository>, DbSession<IReleaseRepository>>();
            services.AddScoped<IDbSession<IReleaseProjectionRepository>, DbSession<IReleaseProjectionRepository>>();

            services.AddScoped<IDbSession<IMemberRepository>, DbSession<IMemberRepository>>();
            services.AddScoped<IDbSession<IMemberProjectionRepository>, DbSession<IMemberProjectionRepository>>();
        }
    }
}