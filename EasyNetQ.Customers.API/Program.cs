using EasyNetQ;
using EasyNetQ.Customers.API.MessageBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var bus = RabbitHutch.CreateBus("host=localhost");

builder.Services.AddSingleton<IMessageBusService, EasyNetQService>(
    o => new EasyNetQService(bus)
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
