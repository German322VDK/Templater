using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;

namespace Templater.Views
{
    /// <summary>
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : UserControl
    {
        public Administrator()
        {
            InitializeComponent();
        }

        private void BtLoadTemplate_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();

            //openFileDialog.Filter = "txt files (.docx)|.docx|All files (.)|.";

            //bool result = openFileDialog.ShowDialog(this) ?? false;

            //if (!result)
            //{
            //    MessageBox.Show("Проблема с выбранным файлом", "Ошибка");

            //    return;
            //}
        }

        private void BtWord_Click(object sender, RoutedEventArgs e)
        {
            var application = new Word.Application();
            var document = new Word.Document();
            document = application.Documents.Add();
            application.Visible = true;            
        }
    }
}
