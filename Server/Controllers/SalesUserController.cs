using Microsoft.AspNetCore.Mvc;
using Server.Authentication;
using Server.Database;
using Server.Models;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesUserController(ISalesUserManager manager) : Controller
{
    private readonly ISalesUserManager _manager = manager;

    /// <summary>
    /// Get all users.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<IEnumerable<SalesUser>> GetAll()
    {
        var users = _manager.GetUsers();
        if (!users.Success)
            return Problem(users.Error);

        return users.Result;
    }

    /// <summary>
    /// Get a user by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<SalesUser> GetById(int id)
    {
        var user = _manager.GetUser(id);
        if (user.Success)
        {
            if (user.Result == null)
                return NotFound();

            return user.Result;
        }
        else
        {
            return Problem(user.Error);
        }
    }

    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>The id of the new user. Otherwise, returns a bad request.</returns>
    [HttpPost]
    public ActionResult<int> Create([FromBody] NewUser user)
    {
        var newId = _manager.AddUser(new SalesUser
        {
            Name = user.Name,
            DistrictetId = user.DistrictId
        });
        if (newId.Success)
            return newId.Result;

        return BadRequest(newId.Error);
    }

    /// <summary>
    /// Delete a user by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var result = _manager.DeleteUser(id);
        if (result.Success)
        {
            return result.Result ? Ok() : NotFound();
        }
        else
        {
            return Problem(result.Error);
        }
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] NewUser user)
    {
        var result = _manager.UpdateUser(new SalesUser
        {
            Id = id,
            Name = user.Name,
            DistrictetId = user.DistrictId
        });

        return result.Success ? Ok() : Problem(result.Error);
    }

    public class NewUser
    {
        public required string Name { get; set; }
        public int? DistrictId { get; set; }
    }
}