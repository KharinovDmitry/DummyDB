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
        private List<Column> columns;
        private List<Element> rows;

        public TableWorkWindow(Table selectedTable)
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;

            table = selectedTable;
            columns = selectedTable.GetColumns();
            rows = GetRows();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = table.Name;
            ShowTable();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private List<Element> GetRows()
        {
            List<Element> resList = new List<Element>();
            foreach (var item in table.Rows)
            {
                resList.Add(new Element());
                resList[resList.Count - 1].Data = new List<object>();
                foreach (var data in item.Data)
                {
                    resList[resList.Count - 1].Data.Add(data);
                }
            }
            return resList;
        }

        private void ShowTable()
        {
            DataGrid tableGrid = new DataGrid();
         
            tableGrid.ItemsSource = rows;
            tableGrid.AutoGenerateColumns = false;
            for(int i = 0; i < columns.Count; i++)
            {
                tableGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = columns[i].Name,
                    Binding = new Binding($"Data[{i}].Value")
                });
            }

            mainGrid.Children.Add(tableGrid);
        }
        
        private class Element
        {
            public List<object> Data { get; set; }
        }
    }
}
