using EventBus.Base.Standard;
using System;
using System.Threading.Tasks;


namespace Templater.IntegrationEvents.Handlers
{
    public class GetDataIntegrationEventHandler : IIntegrationEventHandler<IntegrationEvent>
    {
        public Task Handle(IntegrationEvent @event)
        {
            throw new NotImplementedException();
        }

        public GetDataIntegrationEventHandler() { }
    }
}
