using Microsoft.Extensions.DependencyInjection;

namespace Templater.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();

        public AdministratorViewModel AdministratorModel => App.Services.GetRequiredService<AdministratorViewModel>();

        public DataOperatorViewModel DataOperatorModel => App.Services.GetRequiredService<DataOperatorViewModel>();

        public PrintOperatorViewModel PrintOperatorModel => App.Services.GetRequiredService<PrintOperatorViewModel>();
    }
}
