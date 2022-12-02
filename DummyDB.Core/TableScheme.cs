using Newtonsoft.Json;

namespace DummyDB.Core
{
    public enum DataType
    {
        Int, Float, String, DateTime
    }

    public class Column
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public DataType Type { get; set; }
        [JsonProperty(PropertyName = "referencedTable")]
        public string? ReferencedTable { get; set; }
        [JsonProperty(PropertyName = "isPrimary")]
        public bool isPrimary { get; set; }
    }
    public class TableScheme
    {

        [JsonProperty(PropertyName = "name")]
        public string TableName { get; set; }
        [JsonProperty(PropertyName = "columns")]
        public List<Column> Columns { get; set; }
    }

}
