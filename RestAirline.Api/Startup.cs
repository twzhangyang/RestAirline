using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Autofac.Extensions;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestAirline.CommandHandlers;
using RestAirline.Domain;
using RestAirline.QueryHandlers;
using RestAirline.ReadModel;

namespace RestAirline.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            var container = EventFlowOptions.New
                .UseAutofacContainerBuilder(containerBuilder)
                .AddAspNetCoreMetadataProviders()
                .ConfigureMsSql(MsSqlConfiguration.New.SetConnectionString("Server=localhost;Database=RestAirline;User Id=sa;Password=RestAirline123"))
                .UseMssqlEventStore()
                .ConfigureBookingCommands()
                .ConfigureBookingCommandHandlers()
                .ConfigureReadModel()
                .ConfigureBookingQueryHandlers()
                .ConfigureBookingDomain()
                .CreateContainer();

            var msSqlDatabaseMigrator = container.Resolve<IMsSqlDatabaseMigrator>();
            EventFlowEventStoresMsSql.MigrateDatabase(msSqlDatabaseMigrator);
            
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
