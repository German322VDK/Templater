using System;
using System.Threading.Tasks;
using Templater.Infrastructure.Interfaces;
using Templator.DTO.Models;

namespace Templater.IntegrationEvents.Handlers
{
    public class GetDataIntegrationEventHandler : IIntegrationEventHandler<IntegrationEvent>
    {
        public Task Handle(IntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
