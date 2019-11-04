using Microservices.CalculadorasDeJuros.Api.Controllers.ApiV1.Base;
using Microservices.CalculadorasDeJuros.CrossCutting;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.CalculadorasDeJuros.Api.Controllers.ApiV1
{
    [Route("v{version:apiVersion}/showMeTheCode")]
    public class ShowMeTheCodeController : ApiV1BaseController
    {
        [HttpGet]
        public IActionResult Get() =>
           Ok(CalculadoraDeJurosContants.MsCalculadoraDeJurosGitHubRepository + "\n" +
              CalculadoraDeJurosContants.MsDeTaxasDeJurosGitHubRepository + "\n" +
              CalculadoraDeJurosContants.CalculadoraDeJurosContratosGitHubRepository);
    }
}