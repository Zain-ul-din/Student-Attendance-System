using BLL.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Helpers
{
    public static class DBUpdateExceptionHandler
    {
        public static DBUpdateStatus GetExceptionStatus(this DbUpdateException ex)
        {
            //
            // reference:
            // https://stackoverflow.com/questions/3967140/duplicate-key-exception-from-entity-framework
            //
            SqlException? innerException = ex.InnerException as SqlException;
            if (innerException != null)
                return DBUpdateStatus.DuplicateEntry;
            
            return DBUpdateStatus.FailUnkown;
        }
    }
}
