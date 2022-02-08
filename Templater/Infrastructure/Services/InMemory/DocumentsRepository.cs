using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templator.DTO.DTOModels;

namespace Templater.Infrastructure.Services.InMemory
{
    public class DocumentsRepository
    {
        private List<Document> _Documents;

        public DocumentsRepository()
        {
            _Documents = Enumerable.Range(0, 100)
            .Select(i => new Document
            {
                
            })
            .ToList();
        }

        public IEnumerable<Document> GetAll() => _Documents;
    }
}
