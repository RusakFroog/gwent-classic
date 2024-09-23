using DataAccess.Databases;
using Core.Interfaces;
using MySqlConnector;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using Core.Entities.Database;

namespace DataAccess.Repositories;
public abstract class Repository<T> : IRepository<T> where T : EntityBase
{
    protected abstract string _table { get; }

    protected readonly MySqlDbContext _database;

    protected readonly Dictionary<int, T> _cachedItems = [];

    public Repository(MySqlDbContext database)
    {
        _database = database;
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        try
        {
            if (entity.Id != -1 && (_cachedItems.ContainsKey(entity.Id) || (await GetByIdAsync(entity.Id)) != null))
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

            MySqlCommand cmd = new MySqlCommand($"INSERT INTO @table (@columns) VALUES (@values)");
            cmd.Parameters.AddWithValue("table", _table);
            cmd.Parameters.AddWithValue("columns", columns);
            cmd.Parameters.AddWithValue("values", values);

            int lastInsertedId = await _database.ExecuteAsync(cmd);

            entity.Id = lastInsertedId;

            _cachedItems.Add(lastInsertedId, entity);

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

        MySqlCommand cmd = new MySqlCommand($"UPDATE @table SET {updates} WHERE id = @id");
        cmd.Parameters.AddWithValue("table", _table);
        cmd.Parameters.AddWithValue("id", entity.Id);

        await _database.ExecuteAsync(cmd);

        if (_cachedItems.TryAdd(entity.Id, entity) == false)
            _cachedItems[entity.Id] = entity;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        if (!_cachedItems.ContainsKey(entity.Id))
        {
            Console.WriteLine("[DEBUG] Entity doesn't deleted because `_cachedItems` doesn't contain");
            return;
        }

        MySqlCommand cmd = new MySqlCommand("DELETE FROM @table WHERE id = @id");
        cmd.Parameters.AddWithValue("table", _table);
        cmd.Parameters.AddWithValue("id", entity.Id);

        await _database.ExecuteAsync(cmd);

        _cachedItems.Remove(entity.Id);
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        if (_cachedItems.TryGetValue(id, out T cachedItem))
            return cachedItem;

        MySqlCommand cmd = new MySqlCommand("SELECT * FROM @table WHERE id = @id");
        cmd.Parameters.AddWithValue("table", _table);
        cmd.Parameters.AddWithValue("id", id);

        var dataTable = await _database.QueryAsync(cmd);

        if (dataTable == null || dataTable.Rows.Count == 0)
            return null;

        var item = _getParsedItem(dataTable.Columns, dataTable.Rows[0]);

        if (item == null)
            return null;

        _cachedItems.Add(id, item);

        return item;
    }

    protected static T _getParsedItem(DataColumnCollection tableColumns, DataRow row)
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

                    if (value == DBNull.Value)
                        continue;

                    if (property.PropertyType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                    {
                        var dictionaryValue = JsonConvert.DeserializeObject(value.ToString(), property.PropertyType);
                        property.SetValue(item, dictionaryValue);
                    }
                    else if (property.PropertyType.IsEnum && typeof(Enum).IsAssignableFrom(property.PropertyType) && value is string)
                    {
                        var enumValue = Enum.Parse(property.PropertyType, value.ToString());
                        property.SetValue(item, enumValue);
                    }
                    else
                    {
                        property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
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