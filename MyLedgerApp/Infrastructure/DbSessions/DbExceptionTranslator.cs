using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MyLedgerApp.Infrastructure.DbConfig;
using static MyLedgerApp.Utils.Exceptions;

namespace MyLedgerApp.Infrastructure.DbSessions
{
    public class DbExceptionTranslator
    {
        internal static Exception Translate(Exception exception, DBExceptionContext? dbExContext)
        {
            if (exception is not DbUpdateException dbEx)
                return exception;

            if (!IsUniqueConstraintViolation(dbEx, out string message))
                return exception;

            var constraint = ExtractFromExMsg(message);

            return constraint switch
            {
                UniqueConstraints.Username => new UsernameTakenException(dbExContext?.Username ?? string.Empty),
                UniqueConstraints.Email => new EmailTakenException(dbExContext?.Email ?? string.Empty),
                _ => exception
            };
        }

        private static bool IsUniqueConstraintViolation(DbUpdateException ex, out string message)
        {
            if (ex.InnerException is SqlException sqlEx &&
               sqlEx.Number is 2601 or 2627)
            {
                message = sqlEx.Message;
                return true;
            }
            if (ex.InnerException is SqliteException sqliteEx &&
                sqliteEx.SqliteErrorCode == 19 &&
                sqliteEx.SqliteExtendedErrorCode == 2067)
            {
                message = sqliteEx.Message;
                return true;
            }

            message = ex.Message;
            return false;
        }

        private static string? ExtractFromExMsg(string exMsg)
        {
            // SQL Server
            var uxIndex = exMsg.IndexOf("UX_");
            if (uxIndex >= 0)
            {
                var end = exMsg.IndexOf('\'', uxIndex);
                if (end < 0) end = exMsg.Length;
                return exMsg[uxIndex..end];
            }

            // SQLite
            if (exMsg.Contains("Users.Credential_Username"))
                return UniqueConstraints.Username;

            if (exMsg.Contains("Users.Email"))
                return UniqueConstraints.Email;

            return null;
        }

    }
}
