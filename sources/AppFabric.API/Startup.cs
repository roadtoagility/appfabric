using System;
using System.Threading.Tasks;
using AppFabric.Business;
using AppFabric.Business.CommandHandlers;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.QueryHandlers;
using AppFabric.Business.QueryHandlers.Filters;
using AppFabric.Domain.AggregationActivity.Events;
using AppFabric.Domain.AggregationBilling.Events;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.AggregationRelease.Events;
using AppFabric.Domain.AggregationUser.Events;
using AppFabric.Persistence;
using AppFabric.Persistence.SyncModels.DomainEventHandlers;
using DFlow.Business.Cqrs;
using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Domain.Events;
using FluentMediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddRepositories();

            services.AddScoped<GetProjectsByQueryHandler>();
            services.AddScoped<GetProjectByIdQueryHandler>();

            services.AddScoped<DomainEventHandler<ProjectAddedEvent>, AddedProjectProjectionHandler>();
            services.AddScoped<DomainEventHandler<ProjectDetailUpdatedEvent>, UpdateProjectDetailsProjectionHandler>();
            services.AddScoped<DomainEventHandler<ProjectRemovedEvent>, RemoveProjectProjectionHandler>();

            services.AddAggregationFactories();

            services.AddScoped<GetClientsByQueryHandler>();
            services.AddScoped<GetClientByIdQueryHandler>();
            services.AddScoped<GetProjectsByClientAndNameQueryHandler>();

            services.AddCommandHandlers();

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
            services.AddScoped<DomainEventHandler<UserRemovedEvent>, RemoveUserProjectionHandler>();


            services.AddFluentMediator(builder =>
            {
                builder.On<GetClientByIdFilter>().Pipeline()
                    .Return<GetClientResponse, GetClientByIdQueryHandler>(
                        (handler, request) => handler.Execute(request));

                builder.On<GetClientsByFilter>().Pipeline()
                    .Return<GetClientsResponse, GetClientsByQueryHandler>(
                        (handler, request) => handler.Execute(request));

                //operations
                builder.On<AddUserCommand>().PipelineAsync()
                    .Return<CommandResult<Guid>, AddUserCommandHandler>(
                        async (handler, request) => await handler.Execute(request));

                builder.On<AddProjectCommand>().PipelineAsync()
                    .Return<CommandResult<Guid>, AddProjectCommandHandler>(
                        async (handler, request) => await handler.Execute(request));

                builder.On<UpdateProjectCommand>().PipelineAsync()
                    .Return<ExecutionResult, UpdateProjectCommandHandler>(
                        async (handler, request) => await handler.Execute(request));

                builder.On<RemoveUserCommand>().PipelineAsync()
                    .Return<ExecutionResult, RemoveUserCommandHandler>(
                        async (handler, request) => await handler.Execute(request));

                builder.On<RemoveProjectCommand>().Pipeline()
                    .Return<Task<ExecutionResult>, RemoveProjectCommandHandler>(
                        (handler, request) => handler.Execute(request));


                builder.On<CreateActivityCommand>().PipelineAsync()
                    .Return<ExecutionResult, CreateActivityCommandHandler>(
                        async (handler, request) => await handler.Execute(request));

                builder.On<CloseActivityCommand>().PipelineAsync()
                    .Return<ExecutionResult, CloseActivityCommandHandler>(
                        async (handler, request) => await handler.Execute(request));

                builder.On<AssignResponsibleCommand>().PipelineAsync()
                    .Return<ExecutionResult, AssignResponsibleCommandHandler>(
                        async (handler, request) => await handler.Execute(request));

                builder.On<CreateBillingCommand>().PipelineAsync()
                    .Return<ExecutionResult, CreateBillingCommandHandler>(
                        async (handler, request) => await handler.Execute(request));

                builder.On<AddReleaseCommand>().PipelineAsync()
                    .Return<ExecutionResult, AddReleaseCommandHandler>(
                        async (handler, request) => await handler.Execute(request));

                builder.On<CreateProjectCommand>().PipelineAsync()
                    .Return<ExecutionResult, CreateProjectCommandHandler>(
                        async (handler, request) => await handler.Execute(request));

                builder.On<CreateReleaseCommand>().PipelineAsync()
                    .Return<ExecutionResult, CreateReleaseCommandHandler>(
                        async (handler, request) => await handler.Execute(request));

                builder.On<AddActivityCommand>().PipelineAsync()
                    .Return<ExecutionResult, AddActivityCommandHandler>(
                        async (handler, request) => await handler.Execute(request));

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

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}