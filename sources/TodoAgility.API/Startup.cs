using FluentMediator;
using MediatR;
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
using TodoAgility.Domain.Framework.DomainEvents;
using TodoAgility.Persistence;
using TodoAgility.Persistence.Framework;
using TodoAgility.Persistence.Framework.Repositories;
using TodoAgility.Persistence.Model;
using TodoAgility.Persistence.Repositories;


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
            services.AddControllers();
            services.AddSwaggerGen();
            
            services.AddDbContext<TodoAgilityDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddScoped<IDbSession<IProjectRepository>, ProjectDbSession>();
            services.AddScoped<AddProjectCommandHandler>();

            services.AddFluentMediator(builder =>
            {
                builder.On<AddProjectCommand>().Pipeline()
                    .Return<ExecutionResult, ICommandHandler<AddProjectCommand,ExecutionResult>>(
                        (handler, request) => handler.Execute(request));

                builder.On<ProjectAddedEvent>().Pipeline()
                    .Call<IDomainEventHandler<ProjectAddedEvent>>(
                        (handler, request) => handler.Handle(request));
            });

            services.AddScoped(typeof(IDbSession<>), typeof(DbSession<>));

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
