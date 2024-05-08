namespace mediator;
using mediator;
using MediatR;

public class PeopleHandler : IRequestHandler<CreatePersonCommand, string>
{
    public async Task<string> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.FirstName + request.LastName);
        return "dick";
    }
}