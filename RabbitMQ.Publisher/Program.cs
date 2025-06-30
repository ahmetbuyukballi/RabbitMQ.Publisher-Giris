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
channel.QueueDeclare(queue: "example", exclusive: false,durable:true);

//Queueya mesaj gönderme işlemi
//RabbitMQ kuyruğa atacağı mesajları byte türünden kabul eder.Mesajları byte türünden göndermek lazım.


//Mesajların kalıcılığı için aşağıdaki yöntemi kullanacağız

IBasicProperties basicProperties=channel.CreateBasicProperties();
basicProperties.Persistent = true;
for(int i=0;i<100;i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba"+ i);
    channel.BasicPublish(exchange: "", routingKey: "example", body: message,basicProperties:basicProperties);
}


Console.ReadLine();
