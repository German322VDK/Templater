using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Templater.Infrastructure.Commands;
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

        private ICommand _LoadDocumentsСommand;

        public ICommand LoadDocumentsСommand => _LoadDocumentsСommand
            ??= new LambdaCommand(OnLoadServersCommandExecuted, CanLoadServersCommandExecute);

        private bool CanLoadServersCommandExecute(object p) => true;

        private void OnLoadServersCommandExecuted(object p)
        {
            LoadDocuments();
        }

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
