using FluentMediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TodoAgility.Business.CommandHandlers;
using TodoAgility.Business.CommandHandlers.Commands;
using TodoAgility.Business.Framework;
using TodoAgility.Business.QueryHandlers;
using TodoAgility.Business.QueryHandlers.Filters;
using TodoAgility.Domain.AggregationProject.Events;
using TodoAgility.Domain.AggregationUser.Events;
using TodoAgility.Domain.Framework.DomainEvents;
using TodoAgility.Persistence.Framework;
using TodoAgility.Persistence.Framework.ReadModel.Projections;
using TodoAgility.Persistence.Model;
using TodoAgility.Persistence.Model.Repositories;
using TodoAgility.Persistence.ReadModel;
using TodoAgility.Persistence.ReadModel.Projections;
using TodoAgility.Persistence.ReadModel.Repositories;
using TodoAgility.Persistence.SyncModels.DomainEventHandlers;


namespace TodoAgility.API
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
            
            services.Configure<ProjectionDbOptions>(Configuration.GetSection(
                ProjectionDbOptions.ProjectionConnectionStrings));
            services.AddSingleton<TodoAgilityProjectionsDbContext>();
            
            services.AddDbContext<TodoAgilityDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("ModelConnection")));

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectProjectionRepository, ProjectProjectionRepository>();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserProjectionRepository, UserProjectionRepository>();
            
            services.AddScoped<IDbSession<IProjectRepository>, DbSession<IProjectRepository>>();
            services.AddScoped<IDbSession<IProjectProjectionRepository>, ProjectionDbSession<IProjectProjectionRepository>>();
            
            services.AddScoped<IDbSession<IUserRepository>, DbSession<IUserRepository>>();
            services.AddScoped<IDbSession<IUserProjectionRepository>, ProjectionDbSession<IUserProjectionRepository>>();
            
            services.AddScoped<AddProjectCommandHandler>();
            services.AddScoped<UpdateProjectCommandHandler>();
            services.AddScoped<RemoveProjectCommandHandler>();
            services.AddScoped<GetProjectsByQueryHandler>();
            services.AddScoped<IDomainEventHandler<ProjectAddedEvent>, UpdateProjectProjectionHandler>();
            services.AddScoped<IDomainEventHandler<ProjectRemovedEvent>,RemoveProjectProjectionHandler>();

            services.AddScoped<AddUserCommandHandler>();
            services.AddScoped<RemoveUserCommandHandler>();
            services.AddScoped<IDomainEventHandler<UserAddedEvent>, UpdateUserProjectionHandler>();
            services.AddScoped<IDomainEventHandler<UserRemovedEvent>,RemoveUserProjectionHandler>();
            services.AddScoped<GetClientsByQueryHandler>();
            services.AddScoped<GetClientByIdQueryHandler>();
            services.AddScoped<GetProjectByIdQueryHandler>();
            
            

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
                
                //queries
                builder.On<GetProjectByIdFilter>().Pipeline()
                    .Return<GetProjectResponse, GetProjectByIdQueryHandler>(
                        (handler, request) => handler.Execute(request));
                
                builder.On<GetProjectsByFilter>().Pipeline()
                    .Return<GetProjectsResponse, GetProjectsByQueryHandler>(
                        (handler, request) => handler.Execute(request));

                //readmodel sync
                builder.On<ProjectAddedEvent>().Pipeline()
                    .Call<UpdateProjectProjectionHandler>(
                        (handler, request) => handler.Handle(request));
                
                builder.On<ProjectDetailUpdatedEvent>().Pipeline()
                    .Call<UpdateDetailsProjectProjectionHandler>(
                        (handler, request) => handler.Handle(request));
                
                builder.On<UserAddedEvent>()
                .Pipeline()
                    .Call<IDomainEventHandler<UserAddedEvent>>(
                        (handler, request) => handler.Handle(request));
                
                builder.On<UserRemovedEvent>().Pipeline()
                    .Call<IDomainEventHandler<UserRemovedEvent>>(
                        (handler, request) => handler.Handle(request));
                
                builder.On<ProjectRemovedEvent>().Pipeline()
                    .Call<RemoveProjectProjectionHandler>(
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
