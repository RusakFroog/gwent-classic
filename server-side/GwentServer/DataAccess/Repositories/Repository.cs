using DataAccess.Entities;
using DataAccess.Intrefaces;
using MySqlConnector;
using Newtonsoft.Json;
using System.Collections;
using System.Data;

namespace DataAccess.Repositories;
public abstract class Repository<T> : IRepository<T> where T : EntityBase
{
    protected abstract string _table { get; }

    protected readonly IDatabase _database;

    protected readonly Dictionary<int, T> _items = [];

    public Repository(IDatabase database)
    {
        _database = database;
    }

    public virtual async Task<T?> AddAsync(T entity)
    {
        try
        {
            if (entity.Id != -1 && (_items.ContainsKey(entity.Id) || (await GetByIdAsync(entity.Id)) != null))
                return null;

            string columns = string.Join(", ", typeof(T).GetProperties()
                .Where(p => p.Name != "Id")
                .Select(p => $"`{_getFormatedField(p.Name)}`"));

            string values = string.Join(", ", typeof(T).GetProperties()
                .Where(p => p.Name != "Id")
                .Select(p =>
                {
                    var value = p.GetValue(entity);

                    if (value is IEnumerable && value is not string)
                        return $"'{JsonConvert.SerializeObject(value)}'";
                    else
                        return $"'{value}'";
                }));

            MySqlCommand cmd = new MySqlCommand($"INSERT INTO `{_table}` ({columns}) VALUES ({values})");

            int lastInsertedId = await _database.ExecuteAsync(cmd);

            entity.Id = lastInsertedId;

            _items.Add(lastInsertedId, entity);

            return entity;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync("AddAsync: " + ex.ToString() + ex.Message);
            return null;
        }
    }

    public virtual async Task UpdateAsync(T entity)
    {
        string updates = string.Join(", ", typeof(T).GetProperties()
            .Where(p => p.Name != "Id")
            .Select(p =>
            {
                var value = p.GetValue(entity);

                if (value is IEnumerable && value is not string)
                    return $"`{_getFormatedField(p.Name)}` = '{JsonConvert.SerializeObject(value)}'";
                else 
                    return $"`{_getFormatedField(p.Name)}` = '{value}'";
            }));

        MySqlCommand cmd = new MySqlCommand($"UPDATE `{_table}` SET {updates} WHERE `id` = {entity.Id}");

        await _database.ExecuteAsync(cmd);

        if (_items.TryAdd(entity.Id, entity) == false)
            _items[entity.Id] = entity;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        if (!_items.ContainsKey(entity.Id))
        {
            Console.WriteLine("Entity doesn't deleted because _items don't contain");
            return;
        }

        await _database.ExecuteAsync($"DELETE FROM `{_table}` WHERE `id` = {entity.Id}");

        _items.Remove(entity.Id);
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        if (_items.TryGetValue(id, out T? cachedItem))
            return cachedItem;

        var dataTable = await _database.QueryAsync($"SELECT * FROM `{_table}` WHERE `id` = {id}");

        if (dataTable == null || dataTable.Rows.Count == 0)
            return null;

        var item = _getParsedItem(dataTable.Columns, dataTable.Rows[0]);

        if (item == null)
            return null;

        _items.Add(id, item);

        return item;
    }

    protected static T? _getParsedItem(DataColumnCollection tableColumns, DataRow row)
    {
        try
        {
            var item = Activator.CreateInstance<T>();

            foreach (var property in typeof(T).GetProperties())
            {
                string propertyName = _getFormatedField(property.Name);

                if (tableColumns.Contains(propertyName))
                {
                    var value = row[propertyName];

                    if (value != DBNull.Value)
                    {
                        if (property.PropertyType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                        {
                            var dictionaryValue = JsonConvert.DeserializeObject(value.ToString(), property.PropertyType);
                            property.SetValue(item, dictionaryValue);
                        }
                        else
                        {
                            property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                        }
                    }
                }
            }

            foreach (var field in typeof(T).GetFields())
            {
                string fieldName = _getFormatedField(field.Name);

                if (tableColumns.Contains(fieldName))
                {
                    var value = row[fieldName];

                    if (value != DBNull.Value)
                    {
                        if (field.FieldType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                        {
                            var dictionaryValue = JsonConvert.DeserializeObject(value.ToString(), field.FieldType);
                            field.SetValue(item, dictionaryValue);
                        }
                        else
                        {
                            field.SetValue(item, Convert.ChangeType(value, field.FieldType));
                        }
                    }
                }
            }

            return item;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception in parse:\n" + ex);
            return null;
        }
    }

    private static string _getFormatedField(string field)
    {
        string result = "";

        for (int i = 0; i < field.Length; i++)
        {
            char character = field[i];

            if (char.IsUpper(character) && i != 0)
                result += "_";

            result += character;
        }

        return result.ToLower();
    }
}