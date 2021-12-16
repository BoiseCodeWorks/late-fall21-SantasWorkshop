using System.Data;
using SantasWorkshop.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace SantasWorkshop.Repositories
{
  public class AccountsRepository
  {
    private readonly IDbConnection _db;

    public AccountsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<AccountStationViewModel> GetByAccountStation(int stationId)
    {
      string sql = @"
        SELECT
            a.*,
            acctStations.id AS accountStationId
        FROM accountstations acctStations
        JOIN accounts a ON acctStations.accountId = a.id
        WHERE acctStations.stationId = @stationId;";
      return _db.Query<AccountStationViewModel>(sql, new { stationId }).ToList();
    }


    internal Account GetByEmail(string userEmail)
    {
      string sql = "SELECT * FROM accounts WHERE email = @userEmail";
      return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
    }

    internal Account GetById(string id)
    {
      string sql = "SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Account>(sql, new { id });
    }

    internal Account Create(Account newAccount)
    {
      string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
      _db.Execute(sql, newAccount);
      return newAccount;
    }

    internal Account Edit(Account update)
    {
      string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
      _db.Execute(sql, update);
      return update;
    }
  }
}
