using DummyDB.Core;
using System.Collections.Generic;
using System.Windows;


namespace DummyDB.Desktop
{
    public partial class SelectTableWindow : Window
    {
        private List<Table> tables = new List<Table>();

        public SelectTableWindow(string path)
        {
            InitializeComponent();
            tables = FileReader.ParseTables(path);
        }
    }
}
