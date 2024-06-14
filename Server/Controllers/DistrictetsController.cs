using Microsoft.AspNetCore.Mvc;
using Server.Authentication;
using Server.Database;
using Server.Model;
using Server.Models;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class DistrictetsController(IDistrictetsManager districtetsManager) : Controller
{
    private readonly IDistrictetsManager _districtetsManager = districtetsManager;

    /// <summary>
    /// Get all districtets.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<IEnumerable<Districtet>> GetAll()
    {
        var districtets = _districtetsManager.GetAll();
        if (!districtets.Success)
            return Problem(districtets.Error);

        return districtets.Result!;
    }

    /// <summary>
    /// Get a districtet by id.
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<Districtet?> GetById(int id)
    {
        var districtet = _districtetsManager.Get(id);
        if (districtet.Success)
        {
            if (districtet.Result == null)
                return NotFound();

            return districtet.Result;
        }
        else
        {
            return Problem(districtet.Error);
        }
    }

    /// <summary>
    /// Create a new districtet.
    /// </summary>
    [HttpPost]
    public ActionResult<int> Create([FromBody] NewDistrictet districtet)
    {
        var newId = _districtetsManager.AddDistrictet(districtet.PrimaryUserId, districtet.Name);

        if (!newId.Success)
            return Problem(newId.Error);

        return newId.Result;
    }

    /// <summary>
    /// Update a districtet.
    /// </summary>
    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] NewDistrictet districtet)
    {
        var result = _districtetsManager.Update(new Districtet
        {
            Id = id,
            Name = districtet.Name,
            PrimaryUserId = districtet.PrimaryUserId
        });
        return result.Success ? Ok() : Problem(result.Error);
    }

    /// <summary>
    /// Delete a districtet by id.
    /// </summary>
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var result = _districtetsManager.Delete(id);
        return result.Success ? Ok() : Problem(result.Error);
    }

    /// <summary>
    /// Get all sales users in a districtet.
    /// </summary>
    [HttpGet("{id}/sales-users")]
    public ActionResult<IEnumerable<SalesUser>> GetSalesUsers(int id)
    {
        var salesUsers = _districtetsManager.GetSalesUsers(id);
        if (!salesUsers.Success)
            return Problem(salesUsers.Error);

        return salesUsers.Result!;
    }

    public class NewDistrictet
    {
        public required string Name { get; set; }
        public required int PrimaryUserId { get; set; }
    }
}
