using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public ObservableCollection<ObservableCollection<Document>> Registrys { get; set; } = new();


        private RegistryElement _SelectedRegistry;
        public RegistryElement SelectedRegistry {
            get { return _SelectedRegistry; }
            set {
                if (_SelectedRegistry == value) return;
                _SelectedRegistry = value;
                OnPropertyChanged("SelectedRegistry");
            } }


        public ObservableCollection<Document> SelectedDocuments { get; set; } = new();


        public IList SelectedItems
        {
            get => SelectedDocuments;
            set
            {
                SelectedDocuments.Clear();
                foreach (var doc in value)
                {
                    SelectedDocuments.Add((Document)doc);
                }
            }
        }

        private ICommand _getReadyToPrint;

        public ICommand GetReadyToPrint => _getReadyToPrint
            ??= new LambdaCommand(OnGetReadyToPrintCommandExecuted, CanGetReadyToPrintCommandExecute);

        private bool CanGetReadyToPrintCommandExecute(object p) => SelectedDocuments.Count>0;

        private void OnGetReadyToPrintCommandExecuted(object p)
        {
            foreach (var document in SelectedDocuments)
            {
                _documents.Update(document.Id, Status.ReadyToPrint);

                Documents.SingleOrDefault(el => el.Id == document.Id).Status = Status.ReadyToPrint;
            }

            OnPropertyChanged("Docs");
        }

        private ICommand _getRegistryToPrint;

        public ICommand GetRegistryToPrint => _getRegistryToPrint
            ??= new LambdaCommand(OnGetRegistryToPrintCommandExecuted, CanGetRegistryToPrintCommandExecute);

        private bool CanGetRegistryToPrintCommandExecute(object p) => SelectedRegistry is not null;

        private void OnGetRegistryToPrintCommandExecuted(object p)
        {           
            foreach (var document in SelectedRegistry)
            {
                _documents.Update(document.Id, Status.ReadyToPrint);

                Documents.SingleOrDefault(el => el.Id == document.Id).Status = Status.ReadyToPrint;
                Registrys.Remove(SelectedRegistry);
            }

            OnPropertyChanged("Registrys");
        }

        private ICommand _getDeferred;

        public ICommand GetDeferred => _getDeferred
            ??= new LambdaCommand(OnGetDeferredCommandExecuted, CanGetDeferredCommandExecute);

        private bool CanGetDeferredCommandExecute(object p) => SelectedDocuments.Count>0;

        private void OnGetDeferredCommandExecuted(object p)
        {
            foreach (var document in SelectedDocuments)
            {

                Documents.SingleOrDefault(el => el.Id == document.Id).Status = Status.Deferred;
                _documents.Update(document.Id, Status.Deferred);
            }
        }

        private ICommand _registryAdd;

        public ICommand RegistryAdd => _registryAdd
            ??= new LambdaCommand(OnRegistryAddCommandExecuted, CanRegistryAddCommandExecute);

        private bool CanRegistryAddCommandExecute(object p) => SelectedDocuments.Count > 0;

        private void OnRegistryAddCommandExecuted(object p)
        {
            RegistryElement Registry = new ();
            Registry.Name += Registrys.Count + 1;
            foreach (var document in SelectedDocuments)
            {
                document.InRegistry = true;
                Registry.Add(document);                
            }
            SelectedDocuments.Clear();               
            Registrys.Add(Registry);
            OnPropertyChanged("Registrys");
            OnPropertyChanged("Docs");
        }
        public string Title { get; } = "Документы на печать";        

        public static ObservableCollection<Document> Documents { get; set; } = new ();

        public ObservableCollection<Document> Docs
        {
            get => new ObservableCollection<Document>(Documents.Where(el => el.Status == Status.Unchecked && el.InRegistry == false));

            //set => Documents.Add(value);
        }

        private ICommand _LoadDocumentsСommand;

        public ICommand LoadDocumentsСommand => _LoadDocumentsСommand
            ??= new LambdaCommand(OnLoadServersCommandExecuted, CanLoadServersCommandExecute);

        private bool CanLoadServersCommandExecute(object p) => true;

        private async void OnLoadServersCommandExecuted(object p)
        {
            await LoadDocuments();
            OnPropertyChanged("Docs");
        }

        public PrintOperatorViewModel(IStore<Document> documents, IStore<Template> templates)
        {
            _documents = documents;
            _templates = templates;

            Documents = new ObservableCollection<Document>(documents.GetAll());          
        }

        private async Task LoadDocuments()
        {
            var item1 = _templates.GetById(4);

            var keys1 = item1.JSONKeys.FromJSONKeys().ToList();

            var keyVal1 = new Dictionary<string, string>();

            keyVal1.Add(keys1[0], "Петрова Ирина Васильевна");
            keyVal1.Add(keys1[1], "Иванова Инна Артёмовна");
            keyVal1.Add(keys1[2], "Взыскание долга");
            keyVal1.Add(keys1[3], DateTime.Now.ToString("g"));

            var result1 = await CreateDoc(item1, keyVal1);

            var item2 = _templates.GetById(6);

            var keys2 = item2.JSONKeys.FromJSONKeys().ToList();

            var keyVal2 = new Dictionary<string, string>();

            keyVal2.Add(keys2[0], "Петрова Ирина Васильевна");
            keyVal2.Add(keys2[1], "0514");
            keyVal2.Add(keys2[2], "756432");

            var result2 = await CreateDoc(item2, keyVal2);

            var item3 = _templates.GetById(5);

            var keys3 = item3.JSONKeys.FromJSONKeys().ToList();

            var keyVal3 = new Dictionary<string, string>();

            keyVal3.Add(keys3[0], "Петрова Ирина Васильевна");
            keyVal3.Add(keys3[1], "Иванова Инна Артёмовна");          
            keyVal3.Add(keys3[2], DateTime.Now.ToString("g"));

            var result3 = await CreateDoc(item3, keyVal3);
        }

        private async Task<bool> CreateDoc(Template item, Dictionary<string, string> keyVal)
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

            var result = await Task.Run(() => 
                    WordMethods.CreateDoc($"Templates/{item.FileName}", $"Docs/{doc.FileName}", keyVal))
                .ConfigureAwait(false);

            return result;
        }
    }
}
