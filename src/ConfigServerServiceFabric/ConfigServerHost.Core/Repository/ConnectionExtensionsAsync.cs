using ConfigServerHost.Core.Domain;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ConfigServerHost.Core.Repository
{
    public static class ConnectionExtensionsAsync
    {
        public static async Task<OperationResult<IEnumerable<T>>> CustomQueryAsync<T>(this IDbConnection connection, string procedure, object parameter)
        {
            try
            {
                var result = await connection.QueryAsync<T>(procedure, parameter, commandType: CommandType.StoredProcedure);
                return OperationResult.Create(result);
            }
            catch (System.Exception exception)
            {
                return
                    OperationResult.CreateError<IEnumerable<T>>(exception, "DAT0001", GetErrorMessage(procedure, parameter));
            }
            finally
            {
                connection.CloseIfOpen();
            }
        }

        private static string GetErrorMessage(string procedure, object parameter)
            => $"{procedure}. Params:{parameter.ConvertToJson()}";

        internal static void CloseIfOpen(this IDbConnection connection)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
