using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace mediator;


[ApiController]
[Route("[controller]")]
public partial class PeopleController : ControllerBase
{
    private IMediator _mediator;
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name = "mediator">Mediator.</param>
    public PeopleController(IMediator mediator) => _mediator = mediator;
    
    [HttpGet("ggg")]
    public async Task<ActionResult<string>> CreatePersonCommand(CancellationToken cancellationToken)
    {
        var query = new CreatePersonCommand("ff", "gg"); // Создаем запрос

        return await _mediator.Send(query, cancellationToken);
    }

    private async Task<CreatedResult> SendCreateCommand<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken, string actionName = null)
    {
        var ret = new
        {
            id = await _mediator.Send(command)
        };
        string url = actionName != null ? Url.Link(actionName, ret) ?? string.Empty : string.Empty;
        return Created(url, ret);
    }
}
