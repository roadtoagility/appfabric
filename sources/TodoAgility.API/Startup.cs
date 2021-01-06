using FluentMediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoAgility.Business.CommandHandlers;
using TodoAgility.Business.CommandHandlers.Commands;
using TodoAgility.Business.Framework;
using TodoAgility.Domain.AggregationProject.Events;
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
                options.UseSqlite(
                    Configuration.GetConnectionString("ModelConnection")));

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectProjectionRepository, ProjectProjectionRepository>();
            services.AddScoped<IDbSession<IProjectRepository>, DbSession<IProjectRepository>>();
            services.AddScoped<IDbSession<IProjectProjectionRepository>, ProjectionDbSession<IProjectProjectionRepository>>();
            services.AddScoped<AddProjectCommandHandler>();
            services.AddScoped<UpdateProjectProjectionHandler>();
           
            services.AddFluentMediator(builder =>
            {
                builder.On<AddProjectCommand>().Pipeline()
                    .Return<ExecutionResult, AddProjectCommandHandler>(
                        (handler, request) => handler.Execute(request));

                builder.On<ProjectAddedEvent>().Pipeline()
                    .Call<UpdateProjectProjectionHandler>(
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
