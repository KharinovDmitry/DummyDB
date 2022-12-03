using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DummyDB.Core;

namespace DummyDB.Desktop
{
    public partial class SelectTableWindow : Window
    {
        private string path;

        public SelectTableWindow(string selectedPath)
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;

            path = selectedPath;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Table> tables = InitDateBase(path);
            ShowTables(tables);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void tableBtn_Click(object sender, RoutedEventArgs e)
        {
            Table selectedTable = (Table)((Button)sender).Tag;
            TableWorkWindow tableWorkWindow = new TableWorkWindow(selectedTable);
            tableWorkWindow.Owner = this;
            tableWorkWindow.Show();
        }

        private List<Table> InitDateBase(string path)
        {
            List<Table> resTables;
            try
            {
                return FileReader.ReadTables(path);
            }
            catch(ArgumentException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Newtonsoft.Json.JsonReaderException e)
            {
                MessageBox.Show($"Неккоректная json схема\n{e.Message}");
            }
            catch
            {
                MessageBox.Show("Неккоректный файл базы данных");
            }
            this.Close();
            return new List<Table>();
        }

        private void ShowTables(List<Table> tables)
        {
            ListBox listTables = new ListBox();
            mainGrid.Children.Add(listTables);

            foreach (var table in tables)
            {
                Button tableBtn = new Button();
                tableBtn.Content = table.Name;
                tableBtn.Tag = table;
                tableBtn.Click += new RoutedEventHandler(tableBtn_Click);
                listTables.Items.Add(tableBtn);
            }
        }
    }
}
