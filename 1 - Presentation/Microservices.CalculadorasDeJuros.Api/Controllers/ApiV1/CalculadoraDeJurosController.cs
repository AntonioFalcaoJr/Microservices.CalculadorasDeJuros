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
        public async Task<IActionResult> GetAsync([FromQuery] decimal valorInicial, int meses)
        {
            try
            {
                var calculoDeJurosDto = await _calculadoraDeJurosServices.GetAsync(valorInicial, meses);

                if (calculoDeJurosDto.IsValid())
                    return Ok(calculoDeJurosDto.Resultado);

                return BadRequest(calculoDeJurosDto.GetErrors());
            }
            catch (Exception e)
            {
                return BadRequest("Não foi possível realizar o calculo solicitado, Messagem original do erro:" + e.Message);
            }
        }
    }
}