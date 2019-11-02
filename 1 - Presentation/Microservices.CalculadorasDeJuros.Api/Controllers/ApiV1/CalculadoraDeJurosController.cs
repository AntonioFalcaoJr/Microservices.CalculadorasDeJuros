using Microservices.CalculadorasDeJuros.Api.Controllers.ApiV1.Base;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.CalculadorasDeJuros.Api.Controllers.ApiV1
{
    [Route("v{version:apiVersion}/calculaJuros")]
    public class CalculadoraDeJurosController : ApiV1BaseController
    {
        [HttpGet]
        public IActionResult Get() => Ok();
    }
}