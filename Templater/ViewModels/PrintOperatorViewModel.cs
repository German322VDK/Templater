using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Templater.Infrastructure.Commands;
using Templater.Infrastructure.Interfaces;
using Templater.Infrastructure.Mapping;
using Templater.Infrastructure.Methods;
using Templater.Infrastructure.Services.InMemory;
using Templater.ViewModels.Base;
using Templator.DTO.DTOModels;
using Templator.DTO.Models;

namespace Templater.ViewModels
{
    public class PrintOperatorViewModel : ViewModel
    {
        private IStore<Document> _documents;
        private IStore<Template> _templates;

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

        public PrintOperatorViewModel(IStore<Document> documents, IStore<Template> templates)
        {
            _documents = documents;
            _templates = templates;

            Documents = new ObservableCollection<Document>(documents.GetAll());
        }

        private void LoadDocuments()
        {
            var item1 = _templates.GetById(4);

            var keys1 = item1.JSONKeys.FromJSONKeys().ToList();

            var keyVal1 = new Dictionary<string, string>();

            keyVal1.Add(keys1[0], "Петрова Ирина Васильевна");
            keyVal1.Add(keys1[1], "Иванова Инна Артёмовна");
            keyVal1.Add(keys1[2], "Взыскание долга");
            keyVal1.Add(keys1[3], DateTime.Now.ToString("g"));

            var result1 = CreateDoc(item1, keyVal1);

            var item2 = _templates.GetById(6);

            var keys2 = item2.JSONKeys.FromJSONKeys().ToList();

            var keyVal2 = new Dictionary<string, string>();

            keyVal2.Add(keys2[0], "Петрова Ирина Васильевна");
            keyVal2.Add(keys2[1], "0514");
            keyVal2.Add(keys2[2], "756432");

            var result2 = CreateDoc(item2, keyVal2);
        }

        private bool CreateDoc(Template item, Dictionary<string, string> keyVal)
        {
            var tempFN = item.FileName.Split(".")[0];

            var dt = DateTime.Now;

            var doc = new Document
            {
                FileName = $"{tempFN}---{dt.Year}-{dt.Month}-{dt.Day}--{dt.Hour}-{dt.Minute}-{dt.Second}.docx",
                Template = item,
                Status = Status.Unchecked,
                JSONValues = keyVal.ToJSONKeyValue()
            };

            _documents.Add(doc);
            Documents.Add(doc);

            return WordMethods.CreateDoc($"Templates/{item.FileName}", $"Docs/{doc.FileName}", keyVal);
        }
    }
}
