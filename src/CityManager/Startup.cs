using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using CityManager.Helper;
using CityManager.Model;
using CityManager.Repository;
using CityManager.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CityManager
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
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "City Manager",
                    Description = "Perform CRUD operations on a city"
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Add AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddExpressionMapping();

            }, typeof(MappingProfile));

            // Add DbContext
            services.AddDbContext<CityManagerDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<AppSettings>(Configuration)
                    .AddScoped<IRepository<City>, CityRepository>()
                    .AddScoped<ICityService, CityService>()
                    .AddScoped<ICountryService, CountryService>()
                    .AddScoped<IWeatherService, WeatherService>();
                    
            // Add HTTP Client - Countries API
            services.AddHttpClient(nameof(CountriesApi), c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>($"{nameof(CountriesApi)}:{nameof(CountriesApi.Url)}"));
            }).AddTypedClient(c => Refit.RestService.For<IRestApiClient<ICollection<CountryDetails>, CountryParams, string>>(c));

            // Add HTTP Client - Weather API
            services.AddHttpClient(nameof(WeatherApi), c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>($"{nameof(WeatherApi)}:{nameof(WeatherApi.Url)}"));
            }).AddTypedClient(c => Refit.RestService.For<IRestApiClient<WeatherDetails, WeatherParams, string>>(c));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Manager V1");
                c.RoutePrefix = string.Empty;
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
