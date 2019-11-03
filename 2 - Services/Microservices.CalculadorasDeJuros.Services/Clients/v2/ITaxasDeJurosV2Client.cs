using CalculadoraDeJuros.Contratos.Dto;
using System.Threading.Tasks;

namespace Microservices.CalculadorasDeJuros.Services.Clients.v2
{
    public interface ITaxasDeJurosV2Client
    {
        Task<TaxaDeJurosDto> GetAsync();
    }
}