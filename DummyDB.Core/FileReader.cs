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

        public static List<Table> ReadTables(string path)
        {
            List<Table> tables = new List<Table>();
            foreach (var dirMain in Directory.GetDirectories(path))
            {
                TableScheme? tableScheme = null;
                string? csvData = null;

                string schemePath = "";
                string dataPath = "";

                foreach (var file in Directory.GetFiles(dirMain))
                {
                    if (file.Contains(".json"))
                    {
                        schemePath = file;
                        string jsonScheme = File.ReadAllText(file);
                        tableScheme = JsonConvert.DeserializeObject<TableScheme>(jsonScheme);
                    }
                    else if (file.Contains(".csv"))
                    {
                        dataPath = file;
                        csvData = File.ReadAllText(file).Replace("\r", "");
                    }
                }

                if (tableScheme != null && csvData != null)
                {
                    Table table = new Table(tableScheme, dataPath, schemePath);
                    tables.Add(table);
                }
            }
            return tables;
        }
    }
}
