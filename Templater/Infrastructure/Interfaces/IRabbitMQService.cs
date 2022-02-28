using RabbitMQ.Client.Events;
using System;
using Templator.DTO.Models;

namespace Templater.Infrastructure.Interfaces
{
    public interface IRabbitMQService
    {
        //public void Subscribe<T, TH>()
        //    where T : IntegrationEvent
        //    where TH : IIntegrationEventHandler<T>;

        public void Publish(IntegrationEvent @event);

        public void Subscribe(EventHandler<BasicDeliverEventArgs> @event);

    }
}
