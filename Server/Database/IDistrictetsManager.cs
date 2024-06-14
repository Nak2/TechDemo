using Server.Model;
using Server.Models;

namespace Server.Database;

public interface IDistrictetsManager
{
    /// <inheritdoc cref="DistrictetsManager.AddDistrictet(SalesUser, string)"/>
    OperationResult<int> AddDistrictet(int primaryUserId, string name);

    /// <inheritdoc cref="DistrictetsManager.Delete(int)"/>
    OperationResult Delete(int id);

    /// <inheritdoc cref="DistrictetsManager.Get(int)"/>
    OperationResult<Districtet?> Get(int id);

    /// <inheritdoc cref="DistrictetsManager.GetAll"/>
    OperationResult<List<Districtet>> GetAll();

    /// <inheritdoc cref="DistrictetsManager.GetSalesUsers(int)"/>
    OperationResult<List<SalesUser>> GetSalesUsers(int districtId);

    /// <inheritdoc cref="DistrictetsManager.Update(Districtet)"/>
    OperationResult Update(Districtet districtet);
}