using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templator.DTO.DTOModels;

namespace Templater.Infrastructure.Services.InMemory
{
    public class RegistryElement : ObservableCollection<Document>
    {
        public string Name { get; set; } = "Реестр ";
    }
}
