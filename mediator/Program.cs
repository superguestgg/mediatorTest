using mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;
//using MediatR.Extensions.Microsoft.DependencyInjection

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();
Console.WriteLine("ejfggsekuy");

var services = new ServiceCollection();
services.AddMediatR(typeof(PeopleHandler)); // Замените Startup на ваш класс, где определены обработчики запросов

services.AddTransient<IRequestHandler<CreatePersonCommand, string>, PeopleHandler>();

var serviceProvider = services.BuildServiceProvider();
var mediator = serviceProvider.GetRequiredService<IMediator>();
//var mediator = new Mediator(new ServiceFactory(() => container.GetInstance));

var query = new CreatePersonCommand("erfewf", "weewwe");
var userInfo = await mediator.Send(query);
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
