using Microservices.CalculadorasDeJuros.Api.Controllers.ApiV1.Base;
using Microservices.CalculadorasDeJuros.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Microservices.CalculadorasDeJuros.Api.Controllers.ApiV1
{
    [Route("v{version:apiVersion}/calculaJuros")]
    public class CalculadoraDeJurosController : ApiV1BaseController
    {
        private readonly ICalculadoraDeJurosServices _calculadoraDeJurosServices;

        public CalculadoraDeJurosController(ICalculadoraDeJurosServices calculadoraDeJurosServices)
        {
            _calculadoraDeJurosServices = calculadoraDeJurosServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] decimal valorInicial, int meses)
        {
            try
            {
                var result = await _calculadoraDeJurosServices.GetAsync(valorInicial, meses);

                if (result.IsValid())
                    return Ok(result);

                return BadRequest(result.GetErrors());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}