using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBV_SB_Shared.Messages
{
    public class RabbitMQSender : IRabbitMQSender
    {
        public class QueueConfiguration
        {
            public string QueueName { get; set; }
            public bool Durable { get; set; }
            public bool Exclusive { get; set; }
            public bool AutoDelete { get; set; }
            public IDictionary<string, object> Arguments { get; set; }
        }

        private readonly IConfigurationRoot _configuration;
        private readonly ILogger<RabbitMQSender> _logger;
        public RabbitMQSender(IConfigurationRoot configuration,
            ILogger<RabbitMQSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void Send(string queue, string data)
        {
            var factory = GetConnectionFactory();
            var queueConfig = GetQueueConfiguration(queue);

            _logger.LogInformation("Enviando mensaje RabbitMQ {0}", data);


            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: queueConfig.QueueName,
                durable: queueConfig.Durable, //Esto es para hacer persistente la queue. Si se reinicia Rabbit la queue sigue estando
                exclusive: queueConfig.Exclusive,
                autoDelete: queueConfig.AutoDelete,
                arguments: queueConfig.Arguments
            );

            var body = Encoding.UTF8.GetBytes(data);

            channel.BasicPublish(exchange: "",
                                 routingKey: queueConfig.QueueName,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine("Mensaje RabbitMQ enviado: {0}", data);
            _logger.LogInformation("Mensaje RabbitMQ enviado: {0}", data);
        }

        private ConnectionFactory GetConnectionFactory()
            => new ConnectionFactory()
            {
                UserName = _configuration["ConfiguracionInicial:RabbitMqConsumer:UserName"],
                Password = _configuration["ConfiguracionInicial:RabbitMqConsumer:Password"],
                HostName = _configuration["ConfiguracionInicial:RabbitMqConsumer:Host"],
                Port = Convert.ToInt32(_configuration["ConfiguracionInicial:RabbitMqConsumer:Port"])
            };

        private QueueConfiguration GetQueueConfiguration(string queue)
            => new QueueConfiguration()
            {
                QueueName = queue,
                Durable = Convert.ToBoolean(_configuration["ConfiguracionInicial:RabbitMqConsumer:QueueConfig:Durable"]),
                Exclusive = Convert.ToBoolean(_configuration["ConfiguracionInicial:RabbitMqConsumer:QueueConfig:Exclusive"]),
                AutoDelete = Convert.ToBoolean(_configuration["ConfiguracionInicial:RabbitMqConsumer:QueueConfig:AutoDelete"]),
                Arguments = null
            };
    }
}
