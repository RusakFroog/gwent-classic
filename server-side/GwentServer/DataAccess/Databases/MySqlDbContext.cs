using MySqlConnector;
using System.Data;

namespace DataAccess.Databases;

public sealed class MySqlDbContext
{
    private readonly MySqlConnection _dbConnection;

    // MySqlConnection connection getting from DI
    public MySqlDbContext(MySqlConnection connection)
    {
        _dbConnection = connection;

        if (_dbConnection.State != ConnectionState.Open)
        {
            _dbConnection.Open();

            Console.WriteLine("Database connected");
        }
    }

    /// <summary>
    /// Execute command to database
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Last inserted ID</returns>
    public async Task<int> ExecuteAsync(MySqlCommand command)
    {
        try
        {
            if (_dbConnection.State != ConnectionState.Open)
                await _dbConnection.OpenAsync();

            command.Connection = _dbConnection;
            await command.ExecuteNonQueryAsync();

            return (int)command.LastInsertedId;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
            return -1;
        }
    }

    /// <summary>
    /// Execute command to database and return data
    /// </summary>
    /// <param name="command"></param>
    /// <returns>DataTable - data from command</returns>
    public async Task<DataTable?> QueryAsync(MySqlCommand command)
    {
        try
        {
            if (_dbConnection.State != ConnectionState.Open)
                await _dbConnection.OpenAsync();

            command.Connection = _dbConnection;

            DataTable result = new DataTable();
            result.Load(await command.ExecuteReaderAsync());

            return result;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());

            return null;
        }
    }

    /// <summary>
    /// Execute command to database
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Last inserted ID</returns>
    public async Task<int> ExecuteAsync(string command) => await ExecuteAsync(new MySqlCommand(command));
 
    /// <summary>
    /// Execute command to database and return data
    /// </summary>
    /// <param name="command"></param>
    /// <returns>DataTable - data from command</returns>
    public async Task<DataTable?> QueryAsync(string command) => await QueryAsync(new MySqlCommand(command));
}