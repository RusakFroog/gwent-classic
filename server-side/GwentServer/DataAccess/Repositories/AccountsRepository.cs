using Core.Entities.Database;
using DataAccess.Databases;

namespace DataAccess.Repositories;

public class AccountsRepository : Repository<AccountEntity>
{
    protected override string _table => "accounts";

    public AccountsRepository(MySqlDbContext database) : base(database)
    {

    }
    
    public async Task<AccountEntity> GetByLoginAsync(string login)
    {
        var findAccountEntity = _cachedItems.Values.FirstOrDefault(a => a.Login == login);

        if (findAccountEntity != null)
            return findAccountEntity;

        MySqlConnector.MySqlCommand cmd = new MySqlConnector.MySqlCommand("SELECT id, login, name, email, hashed_password, decks FROM @table WHERE login = @login");
        cmd.Parameters.AddWithValue("table", _table);
        cmd.Parameters.AddWithValue("login", login);

        var dataTable = await _database.QueryAsync(cmd);

        if (dataTable == null || dataTable.Rows.Count == 0)
            return null;

        var item = _getParsedItem(dataTable.Columns, dataTable.Rows[0]);

        if (item == null)
            return null;

        _cachedItems.Add(item.Id, item);

        return item;
    }

    public async Task<AccountEntity> GetByEmailAsync(string email)
    {
        var findAccountEntity = _cachedItems.Values.FirstOrDefault(a => a.Email == email);

        if (findAccountEntity != null)
            return findAccountEntity;
        
        MySqlConnector.MySqlCommand cmd = new MySqlConnector.MySqlCommand("SELECT id, login, name, email, hashed_password, decks FROM @table WHERE email = @email");
        cmd.Parameters.AddWithValue("table", _table);
        cmd.Parameters.AddWithValue("email", email);

        var dataTable = await _database.QueryAsync(cmd);

        if (dataTable == null || dataTable.Rows.Count == 0)
            return null;

        var item = _getParsedItem(dataTable.Columns, dataTable.Rows[0]);

        if (item == null)
            return null;

        _cachedItems.Add(item.Id, item);

        return item;
    }
}
