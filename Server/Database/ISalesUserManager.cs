using Server.Models;

namespace Server.Database;
public interface ISalesUserManager
{
    OperationResult<int> AddUser(SalesUser user);
    OperationResult<bool> DeleteUser(int id);
    OperationResult<SalesUser?> GetUser(int id);
    OperationResult<List<SalesUser>> GetUsers();
    OperationResult UpdateUser(SalesUser user);
}