using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQReceiver.DAL;

namespace RabbitMQReceiver
{
    class Program
    {
	    private static IDatabaseContext _databaseContext;
	    private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

		static void Main(string[] args)
	    {
		    var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
		    optionsBuilder.UseSqlServer("Data Source=DECAIREPC;Initial Catalog=DemoData;Integrated Security=True");
			_databaseContext = new DatabaseContext(optionsBuilder.Options);

			var stopwatch = new Stopwatch();
			stopwatch.Start();

			var factory = new ConnectionFactory() {HostName = "localhost"};
		    using (var connection = factory.CreateConnection())
		    using (var channel = connection.CreateModel())
		    {
			    channel.QueueDeclare(queue: "SoldierQueue",
				    durable: true,
				    exclusive: false,
				    autoDelete: false,
				    arguments: null);

			    var consumer = new EventingBasicConsumer(channel);
			    consumer.Received += (model, ea) =>
			    {
				    var body = ea.Body;
				    var soldier = JsonConvert.DeserializeObject<Soldier>(Encoding.UTF8.GetString(body));

				    var data = FindOneSoldier(soldier.Id);

				    if (data != null)
				    {
					    data.X = soldier.X;
					    data.Y = soldier.Y;
					    _databaseContext.SaveChanges();
				    }
				    //Console.WriteLine(" [x] Received {0}", soldier.X + "," + soldier.Y);
					//_logger.Debug($"{soldier.Id}");
				};
			    channel.BasicConsume(queue: "SoldierQueue",
				    autoAck: true,
				    consumer: consumer);

				Console.WriteLine(" Press [enter] to exit.");
			    Console.ReadLine();
			}
	    }

	    public static SoldierRecord FindOneSoldier(Guid id)
	    {
		    return (from s in _databaseContext.SoldierRecords
				    where s.Id == id
				    select s)
			    .FirstOrDefault();
	    }
	}
}
