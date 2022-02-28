using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Templater.Infrastructure.Commands;
using Templater.Infrastructure.Interfaces;
using Templater.Infrastructure.Mapping;
using Templater.Infrastructure.Methods;
using Templater.Infrastructure.Services.RabbitMQ;
using Templater.ViewModels.Base;
using Templator.DTO.DTOModels;
using Templator.DTO.Models;

namespace Templater.ViewModels
{
    public class DataOperatorViewModel : ViewModel
    {
        public string Title1 { get; } = "Проверка данных";

        public string Title2 { get; } = "Документ";

        public Template SelectedTemplate { get; set; }

        public static ObservableCollection<Template> Templates { get; set; }

        private DataIntegrationEvent _selectedSub;

        public DataIntegrationEvent SelectedSub
        {
            get => _selectedSub;

            set
            {
                
                Set(ref _selectedSub, value);

                //SubData = new ObservableCollection<StSt>(GetListData(_selectedSub.Data));
            }
        }

        public static ObservableCollection<DataIntegrationEvent> Subs { get; set; } = new();

        //public static ObservableCollection<StSt> SubData { get; set; } = new();

        private ICommand openFile;

        public ICommand OpenFile => openFile
            ??= new LambdaCommand(OnOpenFileExecute, CanOpenFileExecute);

        private bool CanOpenFileExecute(object p) => SelectedTemplate is not null;
        private void OnOpenFileExecute(object p)
        {
            WordMethods.OpenWord($"Templates/{SelectedTemplate.FileName}");
        }


        private IStore<Template> _templateDB;

        private IRabbitMQService _service;

        public DataOperatorViewModel(IStore<Template> templateDB, IRabbitMQService service)
        {
            _templateDB = templateDB;

            _service = service;

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
                        DataOperatorViewModel.Subs.Add(data);
                    });
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

            });
        }

        public DataOperatorViewModel()
        {
        }

        //private ICollection<StSt> GetListData(Dictionary<string, string> keyValues)
        //{
        //    var data = new List<StSt>();
        //    foreach (var item in keyValues)
        //    {
        //        data.Add(new StSt 
        //        { 
        //            Key = item.Key,
        //            Value = item.Value
        //        });
        //    }

        //    return data;
        //}
    }
}
