using Microsoft.AspNetCore.Mvc;
using Server.Authentication;
using Server.Database;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class SecondaryRole(ISecondaryRoleManager roleManager) : Controller
{
    private readonly ISecondaryRoleManager _roleManager = roleManager;

    /// <summary>
    /// Returns all districts that a user has a secondary role in.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("GetDistricts")]
    public ActionResult<List<int>> GetAll(int userId)
    {
        var roles = _roleManager.GetUserRoles(userId);
        if (!roles.Success)
            return Problem(roles.Error);

        return roles.Result.Select(d => d.DistrictId).ToList();
    }

    /// <summary>
    /// Returns all users that have a secondary role in the given district.
    /// </summary>
    /// <param name="districtId"></param>
    /// <returns></returns>
    [HttpGet("GetUsers")]
    public ActionResult<List<int>> GetUsers(int districtId)
    {
        var roles = _roleManager.GetAll();
        if (!roles.Success)
            return Problem(roles.Error);

        return roles.Result.Where(r => r.DistrictId == districtId).Select(r => r.SalesUserId).ToList();
    }

    /// <summary>
    /// Returns whether a user has a secondary role in a district.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="discrictId"></param>
    /// <returns></returns>
    [HttpGet("HasUser")]
    public ActionResult<bool> HasUser(int userId, int discrictId)
    {
        var roles = _roleManager.GetAll();
        if (!roles.Success)
            return Problem(roles.Error);

        return roles.Result.Any(r => r.DistrictId == discrictId && r.SalesUserId == userId);
    }

    /// <summary>
    /// Adds a secondary role to a user.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="districtId"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Add(int userId, int districtId)
    {
        var result = _roleManager.Add(userId, districtId);
        if (!result.Success)
            return Problem(result.Error);

        return Ok();
    }

    /// <summary>
    /// Removes a secondary role from a user.
    /// </summary>
    /// <param name="districtId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpDelete]
    public ActionResult Remove(int userId, int districtId)
    {
        var result = _roleManager.Remove(userId, districtId);
        if (!result.Success)
            return Problem(result.Error);

        return Ok();
    }
}
