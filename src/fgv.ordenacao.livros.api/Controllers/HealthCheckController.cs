using Microsoft.AspNetCore.Mvc;

namespace fgv.ordenacao.livros.api.Controllers;

[ApiController]
[Route("api/healthcheck")]
public sealed class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "ok",
            service = "fgv-ordenacao-livros"
        });
    }
}
