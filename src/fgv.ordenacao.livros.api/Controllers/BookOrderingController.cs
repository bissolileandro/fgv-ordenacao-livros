using fgv.ordenacao.livros.application.Contracts.Requests;
using fgv.ordenacao.livros.application.Contracts.Responses;
using fgv.ordenacao.livros.application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fgv.ordenacao.livros.api.Controllers;

[ApiController]
[Route("api/book-ordering")]
public sealed class BookOrderingController : ControllerBase
{
    private readonly IBookOrderingApplicationService _bookOrderingApplicationService;

    public BookOrderingController(IBookOrderingApplicationService bookOrderingApplicationService)
    {
        _bookOrderingApplicationService = bookOrderingApplicationService;
    }

    [HttpPost("sort")]
    [ProducesResponseType(typeof(OrderBooksResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Sort([FromBody] OrderBooksRequest request)
    {
        var response = _bookOrderingApplicationService.Order(request);
        return Ok(response);
    }
}
