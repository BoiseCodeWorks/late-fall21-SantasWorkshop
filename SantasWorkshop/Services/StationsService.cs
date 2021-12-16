using System;
using System.Collections.Generic;
using SantasWorkshop.Models;
using SantasWorkshop.Repositories;

namespace SantasWorkshop.Services
{
  public class StationsService
  {
    private readonly StationsRepository _repo;

    public StationsService(StationsRepository repo)
    {
      _repo = repo;
    }

    internal List<Station> Get()
    {
      return _repo.Get();
    }

    internal Station Get(int id)
    {
      Station found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Station Create(Station newStation)
    {
      return _repo.Create(newStation);
    }

    internal void Remove(int id, string userId)
    {
      Station station = Get(id);
      if (station.CreatorId != userId)
      {
        throw new Exception("You are not allowed to remove this station");
      }
      _repo.Remove(id);
    }
  }
}