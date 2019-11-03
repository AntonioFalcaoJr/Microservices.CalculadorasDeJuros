using CalculadoraDeJuros.Contratos.Dto;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microservices.CalculadorasDeJuros.Services.Clients.v1
{
    public class TaxasDeJurosV1Client : ITaxasDeJurosV1Client
    {
        private const string Endpoint = "taxaJuros";
        private readonly IHttpClientFactory _httpClientFactory;

        public TaxasDeJurosV1Client(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TaxaDeJurosDto> GetAsync()
        {
            var responseMessage = await GetClient().GetAsync(Endpoint);

            return responseMessage.IsSuccessStatusCode
                ? await OnSuccess(responseMessage)
                : OnError(responseMessage);
        }

        private HttpClient GetClient() => _httpClientFactory.CreateClient("taxaDeJurosV1");

        private TaxaDeJurosDto OnError(HttpResponseMessage responseMessage)
        {
            var dto = new TaxaDeJurosDto();
            dto.AddError(responseMessage.ToString());
            return dto;
        }

        private async Task<TaxaDeJurosDto> OnSuccess(HttpResponseMessage responseMessage)
        {
            var resultAsString = await responseMessage.Content.ReadAsStringAsync();
            return new TaxaDeJurosDto { Valor = decimal.Parse(resultAsString, CultureInfo.InvariantCulture) };
        }
    }
}