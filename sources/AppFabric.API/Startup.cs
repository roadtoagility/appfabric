using FluentMediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using AppFabric.Business.CommandHandlers;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;
using AppFabric.Business.QueryHandlers;
using AppFabric.Business.QueryHandlers.Filters;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.AggregationUser.Events;
using AppFabric.Persistence;
using AppFabric.Persistence.Framework;
using AppFabric.Persistence.Model.Repositories;
using AppFabric.Persistence.ReadModel.Repositories;
using AppFabric.Persistence.SyncModels.DomainEventHandlers;
using AppFabric.Domain.AggregationActivity.Events;
using AppFabric.Domain.AggregationBilling.Events;
using AppFabric.Domain.AggregationRelease.Events;
using DFlow.Domain.Events;

namespace AppFabric.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
            
            services.AddSwaggerGen();
            
            services.AddDbContext<AppFabricDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("ModelConnection")));
            
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

            services.AddScoped<AddProjectCommandHandler>();
            services.AddScoped<UpdateProjectCommandHandler>();
            services.AddScoped<RemoveProjectCommandHandler>();
            
            services.AddScoped<GetProjectsByQueryHandler>();
            services.AddScoped<GetProjectByIdQueryHandler>();
            
            services.AddScoped<DomainEventHandler<ProjectAddedEvent>, AddedProjectProjectionHandler>();
            services.AddScoped<DomainEventHandler<ProjectDetailUpdatedEvent>, UpdateProjectDetailsProjectionHandler>();
            services.AddScoped<DomainEventHandler<ProjectRemovedEvent>,RemoveProjectProjectionHandler>();

            services.AddScoped<AddUserCommandHandler>();
            services.AddScoped<RemoveUserCommandHandler>();
            
            services.AddScoped<GetClientsByQueryHandler>();
            services.AddScoped<GetClientByIdQueryHandler>();
            services.AddScoped<GetProjectsByClientAndNameQueryHandler>();

            services.AddScoped<CreateActivityCommandHandler>();
            services.AddScoped<CloseActivityCommandHandler>();
            services.AddScoped<AsignResponsibleCommandHandler>();
            services.AddScoped<CreateBillingCommandHandler>();
            services.AddScoped<AddReleaseCommandHandler>();
            services.AddScoped<CreateProjectCommandHandler>();
            services.AddScoped<CreateReleaseCommandHandler>();
            services.AddScoped<AddActivityCommandHandler>();


            services.AddScoped<DomainEventHandler<ActivityCreatedEvent>, CreatedActivityProjectionHandler>();
            services.AddScoped<DomainEventHandler<MemberAssignedEvent>, AsignedMemberProjectionHandler>();
            services.AddScoped<DomainEventHandler<EffortDecreasedEvent>, DecreasedEffortProjectionHandler>();
            services.AddScoped<DomainEventHandler<EffortIncreasedEvent>, IncreasedEffortProjectionHandler>();
            services.AddScoped<DomainEventHandler<ActivityClosedEvent>, ClosedActivityProjectionHandler>();
            services.AddScoped<DomainEventHandler<ActivityRemovedEvent>, RemovedActivityProjectionHandler>();
            services.AddScoped<DomainEventHandler<BillingCreatedEvent>, CreatedBillingProjectionHandler>();
            services.AddScoped<DomainEventHandler<BillingRemovedEvent>, RemovedBillingProjectionHandler>();
            services.AddScoped<DomainEventHandler<ReleaseAddedEvent>, AddedReleaseProjectionHandler>();
            services.AddScoped<DomainEventHandler<ActivityAddedEvent>, AddedActivityProjectionHandler>();
            services.AddScoped<DomainEventHandler<ReleaseCreatedEvent>, CreatedReleaseProjectionHandler>();
            services.AddScoped<DomainEventHandler<ReleaseRemovedEvent>, RemovedReleaseProjectionHandler>();


            services.AddScoped<DomainEventHandler<UserAddedEvent>, AddedUserProjectionHandler>();
            services.AddScoped<DomainEventHandler<UserRemovedEvent>,RemoveUserProjectionHandler>();


            services.AddFluentMediator(builder =>
            {
               
                builder.On<GetClientByIdFilter>().Pipeline()
                    .Return<GetClientResponse, GetClientByIdQueryHandler>(
                        (handler, request) => handler.Execute(request));
                
                builder.On<GetClientsByFilter>().Pipeline()
                    .Return<GetClientsResponse, GetClientsByQueryHandler>(
                        (handler, request) => handler.Execute(request));
                
                //operations
                builder.On<AddUserCommand>().Pipeline()
                    .Return<CommandResult<Guid>, AddUserCommandHandler>(
                        (handler, request) => handler.Execute(request));
                
                builder.On<AddProjectCommand>().Pipeline()
                    .Return<ExecutionResult, AddProjectCommandHandler>(
                        (handler, request) => handler.Execute(request));

                builder.On<UpdateProjectCommand>().Pipeline()
                    .Return<ExecutionResult, UpdateProjectCommandHandler>(
                        (handler, request) => handler.Execute(request));
                
                builder.On<RemoveUserCommand>().Pipeline()
                    .Return<ExecutionResult, RemoveUserCommandHandler>(
                        (handler, request) => handler.Execute(request));
                
                builder.On<RemoveProjectCommand>().Pipeline()
                    .Return<ExecutionResult, RemoveProjectCommandHandler>(
                        (handler, request) => handler.Execute(request));



                builder.On<CreateActivityCommand>().Pipeline()
                    .Return<ExecutionResult, CreateActivityCommandHandler>(
                        (handler, request) => handler.Execute(request));

                builder.On<CloseActivityCommand>().Pipeline()
                    .Return<ExecutionResult, CloseActivityCommandHandler>(
                        (handler, request) => handler.Execute(request));

                builder.On<AsignResponsibleCommand>().Pipeline()
                    .Return<ExecutionResult, AsignResponsibleCommandHandler>(
                        (handler, request) => handler.Execute(request));

                builder.On<CreateBillingCommand>().Pipeline()
                    .Return<ExecutionResult, CreateBillingCommandHandler>(
                        (handler, request) => handler.Execute(request));

                builder.On<AddReleaseCommand>().Pipeline()
                    .Return<ExecutionResult, AddReleaseCommandHandler>(
                        (handler, request) => handler.Execute(request));

                builder.On<CreateProjectCommand>().Pipeline()
                    .Return<ExecutionResult, CreateProjectCommandHandler>(
                        (handler, request) => handler.Execute(request));

                builder.On<CreateReleaseCommand>().Pipeline()
                    .Return<ExecutionResult, CreateReleaseCommandHandler>(
                        (handler, request) => handler.Execute(request));

                builder.On< AddActivityCommand>().Pipeline()
                    .Return<ExecutionResult, AddActivityCommandHandler>(
                        (handler, request) => handler.Execute(request));

                //queries
                builder.On<GetProjectByIdFilter>().Pipeline()
                    .Return<GetProjectResponse, GetProjectByIdQueryHandler>(
                        (handler, request) => handler.Execute(request));
                
                builder.On<GetProjectsByFilter>().Pipeline()
                    .Return<GetProjectsResponse, GetProjectsByQueryHandler>(
                        (handler, request) => handler.Execute(request));

                builder.On<GetProjectsByClientAndNameFilter>().Pipeline()
                    .Return<GetProjectsResponse, GetProjectsByClientAndNameQueryHandler>(
                        (handler, request) => handler.Execute(request));
                
                //readmodel sync
                builder.On<ProjectAddedEvent>().Pipeline()
                    .Call<IDomainEventHandler<ProjectAddedEvent>>(
                        (handler, request) => handler.Handle(request));
                
                builder.On<ProjectDetailUpdatedEvent>().Pipeline()
                    .Call<IDomainEventHandler<ProjectDetailUpdatedEvent>>(
                        (handler, request) => handler.Handle(request));
                
                builder.On<ProjectRemovedEvent>().Pipeline()
                    .Call<IDomainEventHandler<ProjectRemovedEvent>>(
                        (handler, request) => handler.Handle(request));
                
                builder.On<UserAddedEvent>()
                .Pipeline()
                    .Call<IDomainEventHandler<UserAddedEvent>>(
                        (handler, request) => handler.Handle(request));
                
                builder.On<UserRemovedEvent>().Pipeline()
                    .Call<IDomainEventHandler<UserRemovedEvent>>(
                        (handler, request) => handler.Handle(request));
            });
            
           
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy
                          .AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseCors("default");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
