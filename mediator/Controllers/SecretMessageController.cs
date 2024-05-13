using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace mediator;

    [ApiController]
    [Route("[controller]")]
    public partial class SecretMessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name = "mediator">Mediator.</param>
        public SecretMessageController(IMediator mediator) => _mediator = mediator;
        
        [HttpGet("getForm")]
        public async Task<string> GetSecretMessageForm([FromRoute] GetSecretMessageForm query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet("createByUrl/{message}")]
        public async Task<string> CreateSecretMessageByUrlCommand([FromRoute] CreateSecretMessageByUrlCommand query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateSecretMessageCommand([FromForm] CreateSecretMessageCommand command, CancellationToken cancellationToken)
        {
            return await SendCreateCommand(command, cancellationToken, nameof(CreateSecretMessageCommand));
        }

        [HttpGet("getAddress/{messageForwardingAddress}")]
        public async Task<string> GetSecretMessageAddressCommand([FromRoute] GetSecretMessageAddressCommand query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet("getMessage/{secretMessageAddress}")]
        public async Task<string> GetSecretMessageCommand([FromRoute] GetSecretMessageCommand query, CancellationToken cancellationToken)
        {
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