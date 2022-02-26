using EventBus.Base.Standard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Templater.Infrastructure.Services.InMemory;
using Templater.IntegrationEvents.Events;

namespace Templater.IntegrationEvents.Handlers
{
    public  class GetDataIntegrationEventHandler : IIntegrationEventHandler<ItemCreatedIntegrationEvent>
    {      
        public Task Handle(ItemCreatedIntegrationEvent @event)
        {
            ItemCreatedIntegrationEvents.Events.Add(@event);
            throw new NotImplementedException();
        }

        public GetDataIntegrationEventHandler() { }
    }
}
