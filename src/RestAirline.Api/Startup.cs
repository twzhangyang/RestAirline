using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Autofac.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestAirline.CommandHandlers;
using RestAirline.Domain;
using RestAirline.QueryHandlers;
using RestAirline.ReadModel.EntityFramework;
using RestAirline.ReadModel.InMemory;

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

            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

//            services.AddDbContext<EventStoreContext>(options =>
//            {
//                options.UseSqlServer(Configuration["EventStoreConnectionString"]);
//            });

            var serviceProvider = EventFlowOptions.New
                .UseServiceCollection(services)
                .AddAspNetCore(options => { options.AddUserClaimsMetadata(); })
                .ConfigureMsSql(MsSqlConfiguration.New.SetConnectionString(Configuration["EventStoreConnectionString"]))
                .UseMssqlEventStore()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<InMemoryReadModelModule>()
                .RegisterModule<QueryHandlersModule>()
                .RegisterModule<BookingModule>()
                .CreateServiceProvider();

            return serviceProvider;

//            var msSqlDatabaseMigrator = container.Resolve<IMsSqlDatabaseMigrator>();
//            EventFlowEventStoresMsSql.MigrateDatabase(msSqlDatabaseMigrator);
//
//            return new AutofacServiceProvider(container);
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