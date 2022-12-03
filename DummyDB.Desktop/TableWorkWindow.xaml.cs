using DummyDB.Core;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DummyDB.Desktop
{
    public partial class TableWorkWindow : Window
    {
        private Table table;

        public TableWorkWindow(Table selectedTable)
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;

            table = selectedTable;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = table.Name;
            ShowTable();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void ShowTable()
        {
            DataGrid tableGrid = new DataGrid();
            List<DataExample> testList = new List<DataExample>()
            {
                new DataExample("one", 1),
                new DataExample("two", 2)
            };
            tableGrid.ItemsSource = testList;
            tableGrid.AutoGenerateColumns = false;
            tableGrid.Columns.Add(new DataGridTextColumn() { Header = "Test Text", Binding = new Binding("DataText") });
            tableGrid.Columns.Add(new DataGridTextColumn() { Header = "Test Int", Binding = new Binding("DataInt") });
            mainGrid.Children.Add(tableGrid);
        }

        private class DataExample
        {
            public string DataText { get; set; }
            public int DataInt { get; set; }

            public DataExample(string dataText, int dataInt)
            {
                DataText = dataText;
                DataInt = dataInt;
            }
        }
    }
}
