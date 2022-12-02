using DummyDB.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DummyDB.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectDBBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Title = "Выберите БД";
            dialog.Filter = "Файл базы данных(*.db)|*.db";
            dialog.ShowDialog();        
            string path = FileReader.GetPathFromDBFile(dialog.FileName);
            
            SelectTableWindow selectTableWindow = new SelectTableWindow(path);
            selectTableWindow.Show();
            this.Close();
        }

        private void CreateDBBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
