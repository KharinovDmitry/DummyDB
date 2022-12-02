namespace DummyDB.Core
{
    public static class CsvParser
    {
        public static List<Row> ParseCsv(string input, TableScheme scheme)
        {
            if (!isValid(input, scheme))
            {
                return new List<Row>();
            }

            List<Row> rowsResult = new List<Row>();

            string[] lines = input.Split("\n");
            for (int i = 1; i < lines.Length; i++)
            {
                Row newRow = new Row();
                string[] data = lines[i].Split(";");
                for (int j = 0; j < data.Length; j++)
                {
                    object value;
                    switch (scheme.Columns[j].Type)
                    {
                        case DataType.String:
                            value = data[j];
                            break;
                        case DataType.Int:
                            value = int.Parse(data[j]);
                            break;
                        case DataType.Float:
                            value = float.Parse(data[j]);
                            break;
                        case DataType.DateTime:
                            value = DateTime.Parse(data[j]);
                            break;
                        default:
                            throw new ArgumentException($"Ошибка в {scheme.TableName}\n" +
                                $"Неизвестный тип данных {scheme.Columns[i].Type}");
                    }
                    newRow.Data.Add(scheme.Columns[j], value);
                }
                rowsResult.Add(newRow);
            }

            return rowsResult;
        }

        private static bool isValid(string input, TableScheme scheme)
        {
            string[] inputLines = input.Split("\n");
            string[] titles = inputLines[0].Split(";");
            return isValidTitles(scheme, titles) && isValidData(scheme, inputLines);
        }

        private static bool isValidData(TableScheme scheme, string[] inputLines)
        {
            for(int s = 1; s < inputLines.Length; s++)
            {
                string[] fields = inputLines[s].Split(";");
                for (int i = 0; i < fields.Length; i++)
                {
                    switch (scheme.Columns[i].Type)
                    {
                        case DataType.Int:
                            if(!int.TryParse(fields[i], out int _))
                            {
                                throw new ArgumentException($"Ошибка в {scheme.TableName}\n" +
                                    $"Неверный тип данных\n" +
                                    $"столбец: {s + 1} строка: {i + 1} - Ожидалось int, а встречено \"{fields[i]}\"");
                            }
                            break;
                        case DataType.Float:
                            if(!float.TryParse(fields[i], out float _))
                            {
                                throw new ArgumentException($"Ошибка в {scheme.TableName}\n" +
                                    $"Неверный тип данных\n" +
                                    $"столбец: {s + 1} строка: {i + 1} - Ожидалось int, а встречено \"{fields[i]}\"");
                            }
                            break;
                        case DataType.DateTime:
                            if (!DateTime.TryParse(fields[i], out DateTime _))
                            {
                                throw new ArgumentException($"Ошибка в {scheme.TableName}\n" +
                                    $"Неверный тип данных\n" +
                                    $"столбец: {s + 1} строка: {i + 1} - Ожидалось int, а встречено \"{fields[i]}\"");
                            }
                            break;
                        case DataType.String:
                            break;
                        default:
                            throw new ArgumentException($"Ошибка в {scheme.TableName}\n" +
                                $"Неизвестный тип данных {scheme.Columns[i].Type}");
                    }
                }
            }
            return true;
        }
        
        private static bool isValidTitles(TableScheme scheme, string[] titles)
        {
            if (titles.Length != scheme.Columns.Count)
            {
                throw new ArgumentException($"Ошибка в {scheme.TableName}!\n" +
                    $"Ожидалось {scheme.Columns.Count} столбцов, а получено {titles.Length}");
            }
            for (int i = 0; i < titles.Length; i++)
            {
                if (!titles[i].ToLower().Contains(scheme.Columns[i].Name.ToLower()))
                {
                    throw new ArgumentException($"Ошибка в ${scheme.TableName}!\n" +
                        $"Несоответсвие заголовков\n" +
                        $"Cтолбец: {i + 1} - Ожидалось {scheme.Columns[i].Name}, а встречено \"{titles[i]}\"");
                }
            }
            return true;
        }
    }
}
