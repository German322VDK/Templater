using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
