using DummyDB.Core;
using System.Collections.Generic;
using System.Windows;

namespace DummyDB.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Table> tables = FileReader.ParseTables("./data");
        }
    }
}
