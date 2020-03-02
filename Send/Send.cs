using System;
using System.Collections.Generic;
using System.Text;
using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
	class Send
	{
		public static void Main()
		{
			//var factory = new ConnectionFactory() { HostName = "http://192.168.99.100:5672" };
			var factory = new ConnectionFactory()
			{
				HostName = "192.168.99.100",
				Port = 5672,
				UserName = "guest",
				Password = "guest"
			};
			using (var connection = factory.CreateConnection())
			{
				using (var channel = connection.CreateModel())
				{
					channel.QueueDeclare(queue: "hello",
										 durable: false,
										 exclusive: false,
										 autoDelete: false,
										 arguments: null);

					string message = "Hello World!";
					var body = Encoding.UTF8.GetBytes(message);

					channel.BasicPublish(exchange: "",
										 routingKey: "hello",
										 basicProperties: null,
										 body: body);
					Console.WriteLine(" [x] Sent {0}", message);
				}
				Console.WriteLine(" Press [enter] to exit.");
				Console.ReadLine();
			}
		}
	}
}
