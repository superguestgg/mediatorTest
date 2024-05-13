using mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MMLib.MediatR.Generators.Controllers;

//using MediatR.Extensions.Microsoft.DependencyInjection

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();
//Console.WriteLine("ejfggsekuy");

//var services = new ServiceCollection();
//services.AddMediatR(typeof(PeopleHandler)); // Замените Startup на ваш класс, где определены обработчики запросов

//services.AddTransient<IRequestHandler<CreatePersonCommand, string>, PeopleHandler>();

//var serviceProvider = services.BuildServiceProvider();
//var mediator = serviceProvider.GetRequiredService<IMediator>();
//var mediator = new Mediator(new ServiceFactory(() => container.GetInstance));

//var query = new CreatePersonCommand("erfewf", "weewwe");
//var userInfo = await mediator.Send(query);
Program.CreateHostBuilder(args).Build().Run();

public partial class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}

[MMLib.MediatR.Generators.Controllers.HttpGet("gg", Controller = "People")]
public record CreatePersonCommand(string FirstName, string LastName) : IRequest<string>
{
}


[MMLib.MediatR.Generators.Controllers.HttpGet("{cityName:string}/now", Controller = "Weather", From = From.Route)]
public record WeatherNowCommand(string cityName) : IRequest<string>
{
}


[MMLib.MediatR.Generators.Controllers.HttpGet("{cityName:string}/week", Controller = "Weather", From = From.Route)]
public record WeatherWeekCommand(string cityName) : IRequest<string>
{
}


[MMLib.MediatR.Generators.Controllers.HttpGet("getForm", Controller = "SecretMessage")]
public record GetSecretMessageForm() : IRequest<string>
{
}

[MMLib.MediatR.Generators.Controllers.HttpGet("createByUrl/{message:string}", Controller = "SecretMessage", From=From.Route)]
public record CreateSecretMessageByUrlCommand(string message) : IRequest<string>
{
}
[MMLib.MediatR.Generators.Controllers.HttpPost("create", Controller = "SecretMessage", From=From.Form)]
public record CreateSecretMessageCommand(string message) : IRequest<string>
{
}

[MMLib.MediatR.Generators.Controllers.HttpGet("getAddress/{address:string}", Controller = "SecretMessage", From=From.Route)]
public record GetSecretMessageAddressCommand(string messageForwardingAddress) : IRequest<string>
{
}

[MMLib.MediatR.Generators.Controllers.HttpGet("getMessage/{address:string}", Controller = "SecretMessage", From=From.Route)]
public record GetSecretMessageCommand(string secretMessageAddress) : IRequest<string>
{
}
