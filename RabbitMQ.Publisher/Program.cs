using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    Port = 5672,
    UserName = "guest",
    Password = "guest"
};
//Bağlantı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue oluşturma
channel.QueueDeclare(queue: "example", exclusive: false);

//Queueya mesaj gönderme işlemi
//RabbitMQ kuyruğa atacağı mesajları byte türünden kabul eder.Mesajları byte türünden göndermek lazım.

byte[] message = Encoding.UTF8.GetBytes("Merhaba");
channel.BasicPublish(exchange: "", routingKey: "example", body: message);

Console.ReadLine();
