using CalculadoraDeJuros.Contratos.Dto;
using System.Threading.Tasks;

namespace Microservices.CalculadorasDeJuros.Services.Clients.v1
{
    public interface ITaxasDeJurosV1Client
    {
        Task<TaxaDeJurosDto> GetAsync();
    }
}