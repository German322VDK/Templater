using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Templater.Infrastructure.Commands;
using Templater.ViewModels.Base;

namespace Templater.ViewModels
{
    public class AdministratorViewModel : ViewModel
    {
        public string Title1 { get; } = "Выбор файлов";

        public string Title2 { get; } = "Шаблон";

        private ICommand _LoadTemplaterWordCommand;

        public ICommand LoadTemplaterWordCommand => _LoadTemplaterWordCommand
            ??= new LambdaCommand(OnLoadTemplaterWordCommandExecuted, CanLoadTemplaterWordCommandExecute);

        private bool CanLoadTemplaterWordCommandExecute(object p) => true;

        private void OnLoadTemplaterWordCommandExecuted(object p)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();

            //openFileDialog.Filter = "txt files (.docx)|.docx|All files (.)|.";

            //bool result = openFileDialog.ShowDialog() ?? false;

            //if (!result)
            //{
            //    MessageBox.Show("Проблема с выбранным файлом", "Ошибка");

            //    return;
            //}
        }

        private ICommand _LoadWordCommand;

        public ICommand LoadWordCommand => _LoadWordCommand
            ??= new LambdaCommand(OnLoadWordCommandExecuted, CanLoadWordCommandExecute);

        private bool CanLoadWordCommandExecute(object p) => true;

        private void OnLoadWordCommandExecuted(object p)
        {
            var application = new Microsoft.Office.Interop.Word.Application();
            var document = new Microsoft.Office.Interop.Word.Document();
            document = application.Documents.Add();
            application.Visible = true;

            ////более универсальный способ открытия любого файла дефолтным приложением 
            //string file = @"...data.docx";
            //var proc = new Process();
            //proc.StartInfo.FileName = file;
            //proc.StartInfo.UseShellExecute = true;
            //proc.Start();
        }


    }
}
