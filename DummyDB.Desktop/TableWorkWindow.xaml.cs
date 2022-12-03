using DummyDB.Core;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DummyDB.Desktop
{
    public partial class TableWorkWindow : Window
    {
        private Table table;
        private List<Column> columns;
        private List<RowAdapter> elements;

        public TableWorkWindow(Table selectedTable)
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;

            table = selectedTable;
            columns = selectedTable.GetColumns();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = table.Name;
            try
            {
                table.ReadCsv();
                elements = GetElements();
            }
            catch(ArgumentException exep)
            {
                MessageBox.Show(exep.Message);
                this.Close();
            }
            ShowTable();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
            table.SaveCsv();
            MessageBox.Show("Данные сохраненны!");
        }

        private List<RowAdapter> GetElements()
        {
            List<RowAdapter> resList = new List<RowAdapter>();
            foreach (var item in table.Rows)
            {
                resList.Add(new RowAdapter());
                resList[resList.Count - 1].Data = new List<object>();
                foreach (var data in item.Data)
                {
                    resList[resList.Count - 1].Data.Add(data.Value);
                }
            }
            return resList;
        }

        private void ShowTable()
        {
            DataGrid tableGrid = new DataGrid();
            tableGrid.Margin = new Thickness(0, 50, 0, 0);
            
            tableGrid.ItemsSource = elements;
            tableGrid.AutoGenerateColumns = false;
            for(int i = 0; i < columns.Count; i++)
            {
                tableGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = columns[i].Name,
                    Binding = new Binding($"Data[{i}]")
                });
            }

            mainGrid.Children.Add(tableGrid);
        }

        private void UpdateTable()
        {
            table.Rows.Clear();
            for (int i = 0; i < elements.Count; i++)
            {  
                table.Rows.Add(AdapterToRow(elements[i]));
            }
        }
        
        private Row AdapterToRow(RowAdapter rowAdapter)
        {
            Row res = new Row();
            for (int i = 0; i < columns.Count; i++)
            {
                res.Data.Add(columns[i], rowAdapter.Data[i]);
            }
            return res;
        }

        private class RowAdapter
        {
            public List<object> Data { get; set; }
        }
    }
}
