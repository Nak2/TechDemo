using Server.Models;

namespace Server.Database;
public interface ISecondaryRoleManager
{
    /// <inheritdoc cref="SecondaryRoleManager.AddSecondaryRole(SalesUser, int)"/>/>
    OperationResult Add(int userId, int disctrictId);

    /// <inheritdoc cref="SecondaryRoleManager.RemoveSecondaryRole(SalesUser, int)"/>/>
    OperationResult Remove(int userId, int districtId);

    /// <inheritdoc cref="SecondaryRoleManager.GetAll"/>
    OperationResult<List<SecondaryRole>> GetAll();

    /// <inheritdoc cref="SecondaryRoleManager.GetUserRoles(int)"/>/>
    OperationResult<List<SecondaryRole>> GetUserRoles(int userId);
}