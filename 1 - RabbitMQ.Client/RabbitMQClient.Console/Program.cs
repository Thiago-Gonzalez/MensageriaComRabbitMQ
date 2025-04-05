using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

const string EXCHANGE = "curso-rabbitmq";

var person = new Person("Thiago", "123.456.789-10", new DateTime(2000, 02, 03));

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};

var connection = await connectionFactory.CreateConnectionAsync("curso-rabbitmq");

var channel = await connection.CreateChannelAsync();

var json = JsonSerializer.Serialize(person);
var byteArray = Encoding.UTF8.GetBytes(json);

var basicProperties = new BasicProperties();

await channel.BasicPublishAsync(EXCHANGE, "hr.person-created", true, basicProperties, byteArray);

Console.WriteLine($"Mensagem publicada: {json}");

var consumerChanel = await connection.CreateChannelAsync();

var consumer = new AsyncEventingBasicConsumer(consumerChanel);

consumer.ReceivedAsync += async (sender, eventArgs) => 
{
    var contentArray = eventArgs.Body.ToArray();
    var contentString = Encoding.UTF8.GetString(contentArray);

    var message = JsonSerializer.Deserialize<Person>(contentString);

    Console.WriteLine($"Message received: {contentString}");

    await consumerChanel.BasicAckAsync(eventArgs.DeliveryTag, false);
};

await consumerChanel.BasicConsumeAsync("person-created", false, consumer);

Console.ReadLine();

public class Person
{
    public Person(string fullName, string document, DateTime birthDate)
    {
        FullName = fullName;
        Document = document;
        BirthDate = birthDate;
    }

    public string FullName { get; private set; }
    public string Document { get; private set; }
    public DateTime BirthDate { get; private set; }
}