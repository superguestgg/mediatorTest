using mediator.Encryption;
using mediator.Hare;
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
        services.AddScoped<IWeatherService, WeatherService>();
        services.AddHttpClient<SecretMessageService>();
        services.AddScoped<ISecretMessageService, SecretMessageService>();
        services.AddScoped<IEncrypter, MyEncrypter>();
        services.AddScoped<IRabbitClient, MyRabbitClient>();
        
        
        services.AddMvc(options => options.EnableEndpointRouting = false);

        services.AddSwaggerGen();
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
        app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

        app.UseMvc(
            routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "/{controller=Home}/{action=Index}/{id?}");
            }
        );
    }
}