using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Templater.Infrastructure.Commands;
using Templater.Infrastructure.Interfaces;
using Templater.Infrastructure.Methods;
using Templater.ViewModels.Base;
using Templator.DTO.DTOModels;

namespace Templater.ViewModels
{
    public class DataOperatorViewModel : ViewModel
    {
        public string Title1 { get; } = "Проверка данных";

        public string Title2 { get; } = "Документ";

        public Template SelectedTemplate { get; set; }

        public static ObservableCollection<Template> Templates { get; set; }

        public string Sub { get; set; }

        public static ObservableCollection<string> Subs { get; set; } = new ObservableCollection<string>() { "1", "2"};

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

            //_service.Subscribe();
        }

        public DataOperatorViewModel()
        {
        }
    }
}
