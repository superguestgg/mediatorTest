using MediatR;

namespace mediator.SecretMessage;

public class SecretMessageHandler:
    IRequestHandler<GetSecretMessageForm, string>,
    IRequestHandler<CreateSecretMessageByUrlCommand, string>,
    IRequestHandler<CreateSecretMessageCommand, string>,
    IRequestHandler<GetSecretMessageAddressCommand, string>,
    IRequestHandler<GetSecretMessageCommand, string>
{
    private readonly SecretMessageService _messagesService;
    public SecretMessageHandler(SecretMessageService messagesService)
    {
        _messagesService = messagesService;
    }
    
    public async Task<string> Handle(GetSecretMessageForm request, CancellationToken cancellationToken)
    {
        return "<form action='create' method='Post'><input name='message'><input type='submit'></form>";
    }

    public async Task<string> Handle(CreateSecretMessageByUrlCommand request, CancellationToken cancellationToken)
    {
        return await _messagesService.SaveMessage(request.message);
    }

    public async Task<string> Handle(CreateSecretMessageCommand request, CancellationToken cancellationToken)
    {
        return await _messagesService.SaveMessage(request.message);
    }

    public Task<string> Handle(GetSecretMessageAddressCommand request, CancellationToken cancellationToken)
    {
        return _messagesService.GetPrivateKey(request.messageForwardingAddress);
    }

    public Task<string> Handle(GetSecretMessageCommand request, CancellationToken cancellationToken)
    {
        return _messagesService.GetMessage(request.secretMessageAddress);
    }
}