using EventBus.Base.Standard;
using System;
using System.Threading.Tasks;
using Templater.IntegrationEvents.Events;

namespace Templater.IntegrationEvents.Handlers
{
    public class GetDataIntegrationEventHandler : IIntegrationEventHandler<ItemCreatedIntegrationEvent>
    {
        public Task Handle(ItemCreatedIntegrationEvent @event)
        {

            throw new NotImplementedException();
        }

        public GetDataIntegrationEventHandler() { }
    }
}
