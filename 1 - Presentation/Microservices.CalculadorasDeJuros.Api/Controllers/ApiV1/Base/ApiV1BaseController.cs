using Microsoft.AspNetCore.Mvc;

namespace Microservices.CalculadorasDeJuros.Api.Controllers.ApiV1.Base
{
    [ApiController]
    [ApiVersion("1")]
    public abstract class ApiV1BaseController : ControllerBase
    {
    }
}