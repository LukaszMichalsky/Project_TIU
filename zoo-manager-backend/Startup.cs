using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using MongoDB.Driver;

using zoo_manager_backend.Services;
using zoo_manager_backend.Repositories;
using zoo_manager_backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using System;

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
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
                    Description = "JWT Authentication",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme() {
                            Reference = new OpenApiReference() {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, new List<string>()
                    }
                });
            });

            services.AddAuthentication(authServices => {
                authServices.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authServices.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters() {
                    ValidateActor = false,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateTokenReplay = false,

                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("zoo-manager-backend"))
                };
            });

            services.AddSingleton(new MongoClient(Config.DB_CONNECTION_STRING));

            services.AddSingleton<MongoRepository<AnimalSpecimen>>();       services.AddSingleton<IAnimalSpecimenService, AnimalSpecimenService>();
            services.AddSingleton<MongoRepository<AnimalType>>();           services.AddSingleton<IAnimalTypeService, AnimalTypeService>();
            services.AddSingleton<MongoRepository<Category>>();             services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<MongoRepository<Food>>();                 services.AddSingleton<IFoodService, FoodService>();
            services.AddSingleton<MongoRepository<FoodAssociation>>();      services.AddSingleton<IFoodAssociationService, FoodAssociationService>();
            services.AddSingleton<MongoRepository<Zookeeper>>();            services.AddSingleton<IZookeeperService, ZookeeperService>();
            services.AddSingleton<MongoRepository<ZookeeperAssociation>>(); services.AddSingleton<IZookeeperAssociationService, ZookeeperAssociationService>();
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
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
