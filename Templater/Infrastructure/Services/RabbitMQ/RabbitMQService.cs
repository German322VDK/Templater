using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;
using Templater.Infrastructure.Interfaces;
using Templator.DTO.Models;
using RabbitMQ.Client.Events;

namespace Templater.Infrastructure.Services.RabbitMQ
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IConfiguration _configuration;

        public RabbitMQService(IRabbitMQConnection connection,  IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }
        public void Publish(IntegrationEvent @event)
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            var queueName = _configuration["RabbitMQ:Queue"];

            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                arguments: null,
                durable: true,
                exclusive: false,
                autoDelete: false);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: "",
                    basicProperties: null,
                    body: body);
            }
        }

        /*
        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);

            _subsManager.AddSubscription<T, TH>();
            StartBasicConsume();
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);

            var queueName = _configuration["RabbitMQ:Queue"];

            if (!containsKey)
            {
                if (!_connection.IsConnected)
                {
                    _connection.TryConnect();
                }

                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                        arguments: null,
                        durable: true,
                        exclusive: false,
                        autoDelete: false);
                }
            }
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }

            _subsManager.Clear();
        }

        private void StartBasicConsume()
        {
            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer);
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body);

            try
            {
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
                }

                await ProcessEvent(eventName, message);
                _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
            }
            //if not implemented handler method
            catch (NotSupportedException nex)
            {
                _consumerChannel.BasicReject(eventArgs.DeliveryTag, true);
            }
            //other exceptions
            catch (Exception ex)
            {
                _consumerChannel.BasicNack(eventArgs.DeliveryTag, false, false);
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        if (subscription.IsDynamic)
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType) as IDynamicIntegrationEventHandler;
                            if (handler == null) continue;
                            dynamic eventData = JObject.Parse(message);

                            await Task.Yield();
                            await handler.Handle(eventData);
                        }
                        else
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType);
                            if (handler == null) continue;
                            var eventType = _subsManager.GetEventTypeByName(eventName);
                            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                            await Task.Yield();
                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                        }
                    }
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        */
    }
}
