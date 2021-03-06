using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model;
using Model.InMemoryDataAccess;

namespace Api
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
            AddInMemoryDataStore(services);
            AddCommandsAndQueries(services);
            AddInMemoryDataAccess(services);
        }

        protected void AddInMemoryDataStore(IServiceCollection services)
        {
            services.AddSingleton<LocationStore>();
        }

        protected void AddCommandsAndQueries(IServiceCollection services)
        {
            services.AddScoped<CreateLocationCommand>();
            services.AddScoped<LocationsQuery>();
            services.AddScoped<UserLocationQuery>();
        }

        protected void AddInMemoryDataAccess(IServiceCollection services)
        {
            services.AddScoped<LocationsReader>();
            services.AddScoped<UserLocationReader>();
            services.AddScoped<AreaLocationsReader>();
            services.AddScoped<UserHistoryReader>();
            services.AddScoped<LocationWriter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // No HttpsRedirection - use Nginx to proxy to dotnet in docker;

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
