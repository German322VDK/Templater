using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templater.ViewModels.Base;

namespace Templater.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private string _title = "Шаблонизатор";

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }



        public MainWindowViewModel()
        {

        }
    }
}
