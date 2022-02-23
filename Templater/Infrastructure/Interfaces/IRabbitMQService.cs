using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templator.DTO.Models;

namespace Templater.Infrastructure.Interfaces
{
    public interface IRabbitMQService
    {
        //public void Subscribe<T, TH>()
        //    where T : IntegrationEvent
        //    where TH : IIntegrationEventHandler<T>;

        public void Publish(IntegrationEvent @event);

    }
}
