using EasyNetQ;
using EasyNetQ.Topology;
using Newtonsoft.Json;

const string EXCHANGE = "curso-rabbitmq";
const string QUEUE = "person-created";
const string ROUTING_KEY = "hr.person-created";

var person = new Person("Thiago", "123.456.789-10", new DateTime(2000, 02, 03));

var bus = RabbitHutch.CreateBus("host=localhost");

var advancedBus = bus.Advanced;

var exchange = advancedBus.ExchangeDeclare(EXCHANGE, ExchangeType.Topic);
var queue = advancedBus.QueueDeclare(QUEUE);
advancedBus.Bind(exchange, queue, ROUTING_KEY);

advancedBus.Publish(exchange, ROUTING_KEY, true, new Message<Person>(person));

advancedBus.Consume<Person>(queue, (msg, info) =>
{
    var json = JsonConvert.SerializeObject(msg.Body);

    Console.WriteLine(json);
});

//await bus.PubSub.PublishAsync(person);

//await bus.PubSub.SubscribeAsync<Person>("marketing", msg =>
//{
//    var json = JsonConvert.SerializeObject(msg);

//    Console.WriteLine(json);
//});

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