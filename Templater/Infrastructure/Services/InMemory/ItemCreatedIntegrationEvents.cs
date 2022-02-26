using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templater.IntegrationEvents.Events;

namespace Templater.Infrastructure.Services.InMemory
{
    public class ItemCreatedIntegrationEvents
    {
        public static ObservableCollection<ItemCreatedIntegrationEvent> Events;
    }
}
