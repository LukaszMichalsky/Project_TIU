using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using MongoDB.Driver;

using zoo_manager_backend.Services;
using zoo_manager_backend.Models;

namespace zoo_manager_backend {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "zoo_manager_backend", Version = "v1" });
            });

            services.AddSingleton(new MongoClient(Config.DB_CONNECTION_STRING));

            services.AddSingleton<MongoService<AnimalSpecimen>>();
            services.AddSingleton<MongoService<AnimalType>>();
            services.AddSingleton<MongoService<Category>>();
            services.AddSingleton<MongoService<Food>>();
            services.AddSingleton<MongoService<FoodAssociation>>();
            services.AddSingleton<MongoService<Zookeeper>>();
            services.AddSingleton<MongoService<ZookeeperAssociation>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "zoo_manager_backend v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
