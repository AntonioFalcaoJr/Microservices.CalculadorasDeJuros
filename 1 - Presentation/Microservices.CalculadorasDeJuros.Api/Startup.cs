using Microservices.CalculadorasDeJuros.Domain.Ioc;
using Microservices.CalculadorasDeJuros.Services.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;

namespace Microservices.CalculadorasDeJuros.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calculadora de Juros - Web API - v1");
            });

            app.UseMvc()
                .UseApiVersioning();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(m => { m.EnableEndpointRouting = false; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddApiVersioning(s =>
            {
                s.DefaultApiVersion = new ApiVersion(1, 0);
                s.ReportApiVersions = true;
                s.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Calculadora de Juros - Web API",
                    Version = "v1"
                });
            });

            services.AddHttpClient("taxaDeJurosV1", c => { c.BaseAddress = new Uri(_config["UrlDaApiDeTaxasDeJurosV1"]); })
                .AddPolicyHandler(GetRetryPolicy());

            services.AddHttpClient("taxaDeJurosV2", c => { c.BaseAddress = new Uri(_config["UrlDaApiDeTaxasDeJurosV2"]); })
                .AddPolicyHandler(GetRetryPolicy());

            IocServices.Register(services);
            IocDomain.Register(services);
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
            HttpPolicyExtensions.HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}