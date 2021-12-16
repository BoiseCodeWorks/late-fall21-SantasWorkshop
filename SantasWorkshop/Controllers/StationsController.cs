using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantasWorkshop.Models;
using SantasWorkshop.Services;

namespace SantasWorkshop.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StationsController : ControllerBase
  {
    private readonly StationsService _ss;
    private readonly AccountService _acctService;

    public StationsController(StationsService ss, AccountService acctService)
    {
      _ss = ss;
      _acctService = acctService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Station>> Get()
    {
      try
      {
        var stations = _ss.Get();
        return Ok(stations);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Station> Get(int id)
    {
      try
      {
        var station = _ss.Get(id);
        return Ok(station);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // stations/id/accounts
    [HttpGet("{id}/accounts")]
    public ActionResult<IEnumerable<AccountStationViewModel>> GetAccounts(int id)
    {
      try
      {
        List<AccountStationViewModel> accounts = _acctService.GetByStationId(id);
        return Ok(accounts);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Station>> Create([FromBody] Station newStation)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // NOTE if you are not using [Authorize] use the '?' to prevent userInfo being null and trying to access the id of null
        newStation.CreatorId = userInfo?.Id;
        Station station = _ss.Create(newStation);
        return Ok(station);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Station>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _ss.Remove(id, userInfo.Id);
        return Ok("Delorted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }
}