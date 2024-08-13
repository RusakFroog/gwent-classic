using MySqlConnector;
using System.Data;

namespace DataAccess.Intrefaces;

public interface IDatabase
{
    public Task<int> ExecuteAsync(MySqlCommand command);
    public Task<DataTable?> QueryAsync(MySqlCommand command);
    public Task<int> ExecuteAsync(string command);
    public Task<DataTable?> QueryAsync(string command);
}
