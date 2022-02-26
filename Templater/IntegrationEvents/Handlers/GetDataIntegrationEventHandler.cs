using EventBus.Base.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Templater.IntegrationEvents.Events;

namespace Templater.IntegrationEvents.Handlers
{
    public class GetDataIntegrationEventHandler : IIntegrationEventHandler<ItemCreatedIntegrationEvent>
    {
        public ICollection<ItemCreatedIntegrationEvent> Events;
        public Task Handle(ItemCreatedIntegrationEvent @event)
        {
            Events.Add(@event);
            throw new NotImplementedException();
        }

        public GetDataIntegrationEventHandler() { }
    }
}
