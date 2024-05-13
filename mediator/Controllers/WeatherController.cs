using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace mediator.Controllers;

[ApiController]
[Route("[controller]")]
public partial class WeatherController : ControllerBase
{
    private readonly IMediator _mediator;
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name = "mediator">Mediator.</param>
    public WeatherController(IMediator mediator) => _mediator = mediator;
    
    [HttpGet("{cityName}/now")]
    public async Task<string> WeatherNowCommand([FromRoute] string cityName, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new WeatherNowCommand(cityName), cancellationToken);
    }

    [HttpGet("{cityName}/week")]
    public async Task<string> WeatherWeekCommand([FromRoute] string cityName, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new WeatherWeekCommand(cityName), cancellationToken);
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