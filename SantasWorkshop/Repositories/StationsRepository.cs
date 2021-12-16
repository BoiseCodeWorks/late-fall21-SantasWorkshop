using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using SantasWorkshop.Models;

namespace SantasWorkshop.Repositories
{
  public class StationsRepository
  {
    private readonly IDbConnection _db;

    public StationsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Station> Get()
    {
      string sql = "SELECT * FROM stations;";
      return _db.Query<Station>(sql).ToList();
    }

    internal Station Get(int id)
    {
      string sql = "SELECT * FROM stations WHERE id = @id;";
      return _db.QueryFirstOrDefault<Station>(sql, new { id });
    }

    internal Station Create(Station newStation)
    {
      // this runs in two parts, execute scalar says grab the result of the second
      // part one is the create which would return `1` as its the number of lines changed
      // part two gets the id (primary key) of the last created row
      string sql = @"
      INSERT INTO stations
      (name, creatorId)
      VALUES
      (@Name, @CreatorId);
      SELECT LAST_INSERT_ID()
      ;";
      // NOTE newStation is already an object so no need to wrap it in one
      int id = _db.ExecuteScalar<int>(sql, newStation);
      newStation.Id = id;
      return newStation;
    }

    internal void Remove(int id)
    {
      string sql = "DELETE FROM stations WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}