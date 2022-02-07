using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Templater.Infrastructure.Commands;
using Templater.Infrastructure.Interfaces;
using Templater.Infrastructure.Methods;
using Templater.Infrastructure.Services.InDB;
using Templater.ViewModels.Base;
using Templator.DTO.DTOModels;

namespace Templater.ViewModels
{
    public class DataOperatorViewModel : ViewModel
    {
        public string Title1 { get; } = "Проверка данных";

        public string Title2 { get; } = "Документ";

        public Template SelectedTemplate { get; set; }

        public ObservableCollection<Template> Templates { get; set; }

        private ICommand openFile;

        public ICommand OpenFile => openFile
            ??= new LambdaCommand(OnOpenFileExecute, CanOpenFileExecute);

        private bool CanOpenFileExecute(object p) => SelectedTemplate is not null;
        private void OnOpenFileExecute(object p)
        {
            WordMethods.OpenWord($"Templates/{SelectedTemplate.FileName}");
        }

        private IStore<Template> _templateDB;
        public DataOperatorViewModel(IStore<Template> templateDB)
        {
            _templateDB = templateDB;

            Templates = new ObservableCollection<Template>(_templateDB.GetAll());


        }

        public DataOperatorViewModel()
        {
        }
    }
}
