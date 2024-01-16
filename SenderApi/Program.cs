using MassTransit;
using SenderApi.Middleware;
using SenderApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMessageBusinessLogic, MessageBusinessLogic>();
builder.Services.AddMassTransit(_ =>
{
    _.SetKebabCaseEndpointNameFormatter();

    var settings = builder.Configuration.GetSection("RabbitMq");
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandler>();

app.MapControllers();

app.Run();
