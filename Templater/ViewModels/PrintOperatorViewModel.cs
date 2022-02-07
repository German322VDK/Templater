using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templater.Infrastructure.Services.InMemory;
using Templater.ViewModels.Base;
using Templator.DTO.DTOModels;

namespace Templater.ViewModels
{
    public class PrintOperatorViewModel : ViewModel
    {
        private readonly DocumentsRepository _Documents;

        public string Title { get; } = "Документы на печать";        

        public ObservableCollection<Document> Documents { get; } = new ();

        public PrintOperatorViewModel(DocumentsRepository Documents)
        {
            _Documents = Documents;
        }

        private void LoadDocuments()
        {
            foreach (var document in _Documents.GetAll())
                Documents.Add(document);
        }
    }
}
