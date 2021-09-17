using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CBV_SB_Shared.Messages
{
    public class RabbitMQReciber : IRabbitMQReciber
    {
        #region Class QueueConfiguration
        private class QueueConfiguration
        {
            public string QueueName { get; set; }
            public bool Durable { get; set; }
            public bool Exclusive { get; set; }
            public bool AutoDelete { get; set; }
            public IDictionary<string, object> Arguments { get; set; }
        }
        #endregion

        private readonly IConfigurationRoot _configuration;
        private readonly ILogger<RabbitMQReciber> _logger;
        public event IRabbitMQReciber.NewMessageHandler OnNewMessage;

        public RabbitMQReciber(IConfigurationRoot configuration, ILogger<RabbitMQReciber> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task StartListening(string queueName, CancellationToken stoppingToken)
        {
            var factory = GetConnectionFactory();
            var queueConfig = GetQueueConfiguration(queueName);

            try
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    //don't dispatch a new message to a worker until it has processed and acknowledged the previous one
                    channel.BasicQos(0, 1, false);

                    channel.QueueDeclare(queue: queueConfig.QueueName,
                                         durable: queueConfig.Durable, //Esto es para hacer persistente la queue. Si se reinicia Rabbit la queue sigue estando
                                         exclusive: queueConfig.Exclusive,
                                         autoDelete: queueConfig.AutoDelete,
                                         arguments: queueConfig.Arguments);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) => EmitValue(model, ea, channel);

                    channel.BasicConsume(queue: queueConfig.QueueName,
                                         autoAck: false,
                                         consumer: consumer);

                    //para frenar el proceso y que no se detenga en la primera ejecucion
                    await StopProcess(stoppingToken);
                }
            }
            catch (Exception e1)
            {
                Console.WriteLine($"Se genero la siguiente excepcion durante la creacion del cliente de RabbitMQ: {e1.Message} { e1.InnerException.Message }");
                _logger.LogError($"Se genero la siguiente excepcion durante la creacion del cliente de RabbitMQ: {e1.Message} { e1.InnerException.Message }");
            }
        }

        private ConnectionFactory GetConnectionFactory()
            => new ConnectionFactory()
            {
                UserName = _configuration["ConfiguracionInicial:RabbitMqConsumer:UserName"],
                Password = _configuration["ConfiguracionInicial:RabbitMqConsumer:Password"],
                HostName = _configuration["ConfiguracionInicial:RabbitMqConsumer:Host"],
                Port = Convert.ToInt32(_configuration["ConfiguracionInicial:RabbitMqConsumer:Port"])
            };

        private QueueConfiguration GetQueueConfiguration(string queueName)
            => new QueueConfiguration()
            {
                QueueName = queueName,
                Durable = Convert.ToBoolean(_configuration["ConfiguracionInicial:RabbitMqConsumer:QueueConfig:Durable"]),
                Exclusive = Convert.ToBoolean(_configuration["ConfiguracionInicial:RabbitMqConsumer:QueueConfig:Exclusive"]),
                AutoDelete = Convert.ToBoolean(_configuration["ConfiguracionInicial:RabbitMqConsumer:QueueConfig:AutoDelete"]),
                Arguments = null
            };

        private void EmitValue(object? model, BasicDeliverEventArgs ea, IModel channel)
        {
            try
            {
                var body = ea.Body.ToArray();
                var data = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Datos recibidos: { Path.GetFileName(data) }");
                _logger.LogInformation($"Datos recibidos: { Path.GetFileName(data) }");

                OnNewMessage?.Invoke(data);

                //Se envia el ACK a la queye indicando que el mensaje se proceso exitosamente y que se elimine de la cola
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
            catch (Exception e1)
            {
                Console.WriteLine($"Se genero la siguiente excepcion durante el precesamiento del mensaje: {e1.Message} { e1.InnerException.Message }");
                _logger.LogError($"Se genero la siguiente excepcion durante el precesamiento del mensaje: {e1.Message} { e1.InnerException.Message }");
            }
        }

        /// <summary>
        /// Frena el proceso para que no se detenga en la primer ejecucion
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected async Task StopProcess(CancellationToken stoppingToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            stoppingToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
            await tcs.Task;
        }
    }
}
