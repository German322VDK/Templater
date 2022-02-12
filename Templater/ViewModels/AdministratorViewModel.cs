using Microsoft.Win32;
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
using Templater.Infrastructure.Methods;
using Templater.ViewModels.Base;
using Templator.DTO.DTOModels;
using Templator.DTO.Models;

namespace Templater.ViewModels
{
    public class AdministratorViewModel : ViewModel
    {
        public string Title1 { get; } = "Выбор файлов";

        public string Title2 { get; } = "Шаблон";

        public string SelectedStatus { get; set; } 

        public ObservableCollection<string> Statuses { get; set; } = new ObservableCollection<string>() 
        {
            "Новые файлы",  "Все файлы", "Готовые к печати",  "Отложенные файлы", "Распечатанные файлы", "Очередь печати",
        };

        public Document SelectedDocument { get; set; }

        private ObservableCollection<Document> documents;
        public ObservableCollection<Document> Documents 
        {
            get
            {
                if (SelectedStatus == "Новые файлы")
                    return new ObservableCollection<Document>(documents.Where(el => el.Status == Status.Unchecked));

                if (SelectedStatus == "Все файлы")
                    return documents;

                if (SelectedStatus == "Готовые к печати")
                    return new ObservableCollection<Document>(documents.Where(el => el.Status == Status.ReadyToPrint));

                if (SelectedStatus == "Отложенные файлы")
                    return new ObservableCollection<Document>(documents.Where(el => el.Status == Status.Deferred));

                if (SelectedStatus == "Распечатанные файлы")
                    return new ObservableCollection<Document>(documents.Where(el => el.Status == Status.Printed));

                if (SelectedStatus == "Очередь печати")
                    return new ObservableCollection<Document>(documents.Where(el => el.Status == Status.InPrintedQueue));

                return documents;
            }  
        }

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

        private bool CanLoadWordCommandExecute(object p) => SelectedDocument is not null;

        private void OnLoadWordCommandExecuted(object p)
        {
            WordMethods.OpenWord($"Docs/{SelectedDocument.FileName}");
        }

        private Status GetStatus(string statuses)
        {
            Status status;

            switch (statuses)
            {
                case "Новые файлы":
                    status = Status.Unchecked;
                    break;
                case "Все файлы":
                    status = Status.Unchecked;
                    break;
                case "Готовые к печати":
                    status = Status.ReadyToPrint;
                    break;
                case "Отложенные файлы":
                    status = Status.Deferred;
                    break;
                case "Распечатанные файлы":
                    status = Status.Printed;
                    break;
                case "Очередь печати":
                    status = Status.InPrintedQueue;
                    break;
                default:
                    status = Status.Unchecked;
                    break;
            }

            return status;
        }

        private IStore<Document> _docs;

        public AdministratorViewModel(IStore<Document> docs)
        {
            _docs = docs;

            documents = PrintOperatorViewModel.Documents;

            SelectedStatus = Statuses[1];
        }
    }
}
