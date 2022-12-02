namespace DummyDB.Core
{
    public class Table
    {
        private TableScheme Scheme { get; set; }
        public string Name { get; private set; }
        public List<Row> Rows { get; private set; }

        public Table(TableScheme scheme)
        {
            Name = scheme.TableName;
            Scheme = scheme;
            Rows = new List<Row>();
        }

        public void ReadCsv(string csv)
        {
            Rows = CsvParser.ParseCsv(csv, Scheme);
        }

        public static Table GetTableByName(string name, List<Table> tables)
        {
            foreach(var table in tables)
            {
                if(table.Name == name)
                {
                    return table;
                }
            }
            throw new ArgumentException($"Несуществующая таблица: {name}");
        }

        public Row GetElement(int key)
        {
            foreach (var row in Rows)
            {
                string keyRow = "";
                foreach (var column in row.Data)
                {
                    if(column.Key.isPrimary)
                    {
                        keyRow += ((int)column.Value).ToString();
                    }
                }
                if(int.Parse(keyRow) == key)
                {
                    return row;
                }
            }
            throw new ArgumentException($"Неизвестный элемент таблицы");
        }
    }

    public class Row
    {
        public Dictionary<Column, object> Data { get; }

        public Row()
        {
            Data = new Dictionary<Column, object>();
        }
    }
}
