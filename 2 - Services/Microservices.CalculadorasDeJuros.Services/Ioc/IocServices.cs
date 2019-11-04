using Microservices.CalculadorasDeJuros.Services.Clients.v1;
using Microservices.CalculadorasDeJuros.Services.Clients.v2;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.CalculadorasDeJuros.Services.Ioc
{
    public static class IocServices
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ITaxasDeJurosV1Client, TaxasDeJurosV1Client>();
            services.AddScoped<ITaxasDeJurosV2Client, TaxasDeJurosV2Client>();
            services.AddScoped<ICalculadoraDeJurosServices, CalculadoraDeJurosServices>();
        }
    }
}