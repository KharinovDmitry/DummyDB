using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyDB.Core
{
    public static class FileReader
    {
        public static string GetPathFromDBFile(string path)
        {
            return File.ReadAllText(path);
        }

        public static List<Table> ParseTables(string path)
        {
            List<Table> tables = new List<Table>();
            foreach (var dirMain in Directory.GetDirectories(path))
            {
                TableScheme tableScheme = null;
                string csvData = null;

                foreach (var file in Directory.GetFiles(dirMain))
                {
                    if (file.Contains(".json"))
                    {
                        string jsonScheme = File.ReadAllText(file);
                        tableScheme = JsonConvert.DeserializeObject<TableScheme>(jsonScheme);
                    }
                    else if (file.Contains(".csv"))
                    {
                        csvData = File.ReadAllText(file).Replace("\r", "");
                    }
                }

                if (tableScheme != null && csvData != null)
                {
                    Table table = new Table(tableScheme);
                    table.ReadCsv(csvData);
                    tables.Add(table);
                }
            }
            return tables;
        }
    }
}
