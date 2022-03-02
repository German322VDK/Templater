using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Templater.Infrastructure.Commands;
using Templater.Infrastructure.Interfaces;
using Templater.Infrastructure.Mapping;
using Templater.Infrastructure.Methods;
using Templater.ViewModels.Base;
using Templator.DTO.DTOModels;
using Templator.DTO.Models;

namespace Templater.ViewModels
{
    public class DataOperatorViewModel : ViewModel
    {
        public string Title1 { get; } = "Проверка данных";

        public string Title2 { get; } = "Шаблоны";

        public string _titleCheck = "Не выбран";

        public string TitleCheck 
        {
            get => _titleCheck;
            set => Set(ref _titleCheck, value);
        }

        private Visibility _isGood = Visibility.Hidden;

        public Visibility isGood
        {
            get => _isGood;
            set => Set(ref _isGood, value);
        }

        public ObservableCollection<string> TemplateKeys { get; set; } = new();

        private Template _selectedTemplate;

        public Template SelectedTemplate 
        { 
            get => _selectedTemplate;
            set 
            {
                Set(ref _selectedTemplate, value);

                var data = _selectedTemplate.JSONKeys.FromJSONKeys();

                TemplateKeys.Clear();

                foreach (var item in data)
                {
                    TemplateKeys.Add(item);
                }
            }
        }

        public static ObservableCollection<Template> Templates { get; set; }

        private DataIntegrationEvent _selectedSub;

        public DataIntegrationEvent SelectedSub
        {
            get => _selectedSub;

            set => Set(ref _selectedSub, value);
        }

        public static ObservableCollection<DataIntegrationEvent> Subs { get; set; } = new();

        private ICommand openFile;

        public ICommand OpenFile => openFile
            ??= new LambdaCommand(OnOpenFileExecute, CanOpenFileExecute);

        private bool CanOpenFileExecute(object p) => SelectedTemplate is not null;
        private void OnOpenFileExecute(object p)
        {
            WordMethods.OpenWord($"Templates/{SelectedTemplate.FileName}");
        }


        private ICommand _LoadDocumentsСommand;

        public ICommand CreateDocumentСommand => _LoadDocumentsСommand
            ??= new LambdaCommand(OnLoadServersCommandExecuted, CanLoadServersCommandExecute);

        private bool CanLoadServersCommandExecute(object p) => true;

        private async void OnLoadServersCommandExecuted(object p)
        {
            await DecisionToDoc();
            OnPropertyChanged("Docs");
        }

        private ICommand _checkСommand;

        public ICommand CheckСommand => _checkСommand
            ??= new LambdaCommand(OnCheckСommandExecuted, CanCheckСommandExecute);

        private bool CanCheckСommandExecute(object p) => SelectedSub is not null;

        private void OnCheckСommandExecuted(object p)
        {
            var result = CheckMarks();

            if (result)
            {
                TitleCheck = "Метки совпали";
                isGood = Visibility.Visible;
            }

            else
            {
                TitleCheck = "Метки не совпали";
                isGood = Visibility.Hidden;
            }
        }

        private bool CheckMarks()
        {
            var template = _templateDB.GetById(int.Parse(SelectedSub.TemplateId));

            var templateKeys = template.JSONKeys.FromJSONKeys().ToList();
            templateKeys.Sort();

            var subKeys = SelectedSub.Data.Select(el => el.Key).ToList();
            subKeys.Sort();

            if (templateKeys.Count != subKeys.Count)
                return false;

            for (int i = 0; i < templateKeys.Count; i++)
            {
                if (templateKeys[i] != subKeys[i])
                    return false;
            }

            return true;

        }


        private async Task DecisionToDoc()
        {
            var item1 = _templateDB.GetById(int.Parse(SelectedSub.TemplateId));

            var keys1 = item1.JSONKeys.FromJSONKeys().ToList();          

            var result1 = await CreateDoc(item1, SelectedSub.Data);

            Subs.Remove(SelectedSub);

            
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

            _documnetDB.Add(doc);

            _administratorViewModel.Documents.Add(doc);

            var result = await Task.Run(() =>
                    WordMethods.CreateDoc($"Templates/{item.FileName}", $"Docs/{doc.FileName}", keyVal))
                .ConfigureAwait(false);

            return result;
        }

        private IStore<Document> _documnetDB;

        private IStore<Template> _templateDB;

        private IRabbitMQService _service;

        private AdministratorViewModel _administratorViewModel;

        public DataOperatorViewModel(IStore<Template> templateDB, IStore<Document> documnetDB, IRabbitMQService service,
            AdministratorViewModel administratorViewModel)
        {
            _templateDB = templateDB;

            _service = service;

            _documnetDB = documnetDB;

            _administratorViewModel = administratorViewModel;

            Templates = new ObservableCollection<Template>(_templateDB.GetAll());

            _service.Subscribe((model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var data = message.FromData();

                    App.Current.Dispatcher.Invoke(delegate // <--- HERE
                    {
                        Subs.Add(data);
                    });
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

            });
        }


    }
}
