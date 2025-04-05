using EasyNetQ;
using EasyNetQ.Marketing.API.Subscribers;
using MessagingEvents.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var bus = RabbitHutch.CreateBus("host=localhost");
builder.Services.AddSingleton(bus);

builder.Services.AddHostedService<CustomerCreatedSubscriber>();

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
