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
