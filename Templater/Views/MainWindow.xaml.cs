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

namespace Templater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Template(object sender, RoutedEventArgs e) => Close();

        private void CbDataOperator_Selected(object sender, RoutedEventArgs e) => TbControl.SelectedItem = DataOperator;

        private void CbPrintOperator_Selected(object sender, RoutedEventArgs e) => TbControl.SelectedItem = PrintOperator;

        private void CbAdministrator_Selected(object sender, RoutedEventArgs e) => TbControl.SelectedItem = Administrator;
    }
}
