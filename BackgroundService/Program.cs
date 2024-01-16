using BackgroundService.Middleware;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(_ =>
{
    _.SetKebabCaseEndpointNameFormatter();

    var settings = builder.Configuration.GetSection("RabbitMq");
    var assembly = typeof(Program).Assembly;
    _.AddConsumers(assembly);
    _.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(settings["Hostname"], h =>
        {
            h.Username(settings["Username"]);
            h.Password(settings["Password"]);
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();

app.Run();
