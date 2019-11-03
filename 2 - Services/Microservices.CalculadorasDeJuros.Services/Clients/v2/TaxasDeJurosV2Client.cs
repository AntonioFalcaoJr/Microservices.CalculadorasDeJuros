using CalculadoraDeJuros.Contratos.Dto;
using Microservices.CalculadorasDeJuros.Domain;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.CalculadorasDeJuros.Services.Clients.v2
{
    public class TaxasDeJurosV2Client : ITaxasDeJurosV2Client
    {
        private const string Endpoint = "taxaJuros";
        private readonly IHttpClientFactory _httpClientFactory;

        public TaxasDeJurosV2Client(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TaxaDeJurosDto> GetAsync()
        {
            var dto = new TaxaDeJurosDto
            {
                Valor = 10,
                TaxaDeJuros = new TaxaDeJurosPadrao()
            };

            var client = GetClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var jsonInString = JsonConvert.SerializeObject(dto);

            var postAsync = await client.PostAsync(Endpoint,
                new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            return postAsync.IsSuccessStatusCode
                ? await OnSuccess(postAsync)
                : OnError(postAsync);
        }

        private HttpClient GetClient() => _httpClientFactory.CreateClient("taxaDeJurosV2");

        private TaxaDeJurosDto OnError(HttpResponseMessage responseMessage)
        {
            var dto = new TaxaDeJurosDto();
            dto.AddError(responseMessage.ToString());
            return dto;
        }

        private async Task<TaxaDeJurosDto> OnSuccess(HttpResponseMessage responseMessage)
        {
            var resultAsString = await responseMessage.Content.ReadAsStringAsync();
            return new TaxaDeJurosDto { Valor = Convert.ToDecimal(resultAsString) };
        }
    }
}