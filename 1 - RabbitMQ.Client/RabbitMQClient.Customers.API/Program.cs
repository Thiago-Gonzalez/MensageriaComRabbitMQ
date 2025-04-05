using MessagingEvents.Shared;
using RabbitMQClient.Customers.API.Bus;
using RabbitMQClient.Customers.API.MessageBus.Configuration;
using RabbitMQClient.Customers.API.MessageBus.Publishers;
using RabbitMQClient.Customers.API.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RabbitMQSettings>(
    builder.Configuration.GetSection("RabbitMQ")
);
builder.Services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
builder.Services.AddTransient<IEventPublisher<CustomerCreated>, CustomerCreatedPublisher>();

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
