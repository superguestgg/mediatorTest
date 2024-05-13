using mediator.People;
using mediator.SecretMessage;
using mediator.Weather;
using MediatR;

namespace mediator;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddMediatR(typeof(PeopleHandler));
        services.AddMediatR(typeof(WeatherHandler));
        services.AddHttpClient<WeatherService>();
        services.AddScoped<WeatherService>();
        services.AddHttpClient<SecretMessageService>();
        services.AddScoped<SecretMessageService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}