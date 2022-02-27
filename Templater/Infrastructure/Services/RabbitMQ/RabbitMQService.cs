using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using RabbitMQ.Client;
using System.Text;
using Templater.Infrastructure.Interfaces;
using Templator.DTO.Models;
using RabbitMQ.Client.Events;
using Templater.ViewModels;

namespace Templater.Infrastructure.Services.RabbitMQ
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;

        public RabbitMQService(IConnectionFactory connectionFactory,  IConfiguration configuration)
        {
            _connectionFactory = connectionFactory;
            _configuration = configuration;
        }
        public void Publish(IntegrationEvent @event)
        {
            var queueName = _configuration["RabbitMQ:Queue"];

            using (var connection = _connectionFactory.CreateConnection()) 

            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclarePassive(queueName);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: queueName,
                    basicProperties: null,
                    body: body);
            }
        }

        public void Subscribe(/*EventHandler<BasicDeliverEventArgs> @event*/)
        {
            var queueName = _configuration["RabbitMQ:Queue"];

            using (var connection = _connectionFactory.CreateConnection())

            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclarePassive(queueName);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    DataOperatorViewModel.Subs.Add(message);
                };

                var res = channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }

    }
}
