using CalculadoraDeJuros.Contratos.Dto;
using Microservices.CalculadorasDeJuros.Services.Clients.v1;
using System;
using System.Threading.Tasks;

namespace Microservices.CalculadorasDeJuros.Services
{
    public class CalculadoraDeJurosServices : ICalculadoraDeJurosServices
    {
        private readonly ITaxasDeJurosV1Client _taxasDeJurosV1Client;

        public CalculadoraDeJurosServices(ITaxasDeJurosV1Client taxasDeJurosV1Client)
        {
            _taxasDeJurosV1Client = taxasDeJurosV1Client;
        }

        public async Task<CalculoDeJurosDto> GetAsync(decimal valorInicial, int meses)
        {
            var taxaDeJuros = await _taxasDeJurosV1Client.GetAsync();
            return Calcular(valorInicial, meses, taxaDeJuros.Valor);
        }

        private CalculoDeJurosDto Calcular(decimal valorInicial, int meses, decimal taxaDeJuros)
        {
            var dto = new CalculoDeJurosDto();

            var pow = Math.Pow((double)(1 + taxaDeJuros), meses);
            var result = valorInicial * (decimal)pow;
            dto.Resultado = decimal.Parse(result.ToString("##.00"));

            return dto;
        }
    }
}