using Microsoft.AspNetCore.Mvc;

namespace Server.Database;

public static class DatabaseExceptionHandler
{
    public static ActionResult HandleException(MySqlConnector.MySqlException ex)
    {
        if(ex.ErrorCode == MySqlConnector.MySqlErrorCode.DuplicateKeyEntry)
            return new ConflictObjectResult(ex.Message);

        return new BadRequestObjectResult(ex.Message);
        
    }
}
