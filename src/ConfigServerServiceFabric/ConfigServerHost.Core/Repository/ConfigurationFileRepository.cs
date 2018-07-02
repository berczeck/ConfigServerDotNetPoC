using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ConfigServerHost.Core.Domain;
using ConfigServerHost.Core.Domain.Query;
using ConfigServerHost.Core.Interfaces;

namespace ConfigServerHost.Core.Repository
{
    public class ConfigurationFileRepository : IConfigurationFileRepository
    {
        private readonly string connectionString;

        public ConfigurationFileRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<OperationResult<IEnumerable<ConfigurationFileResponse>>> GetLastVersionByApplicationEnvironment(string applicationName, string environment)
        {
            var connection = new SqlConnection(connectionString);
            var parameters = new
            {
                @applicationName=applicationName,
                @environment = environment
            };

            return await connection.CustomQueryAsync<ConfigurationFileResponse>("[dbo].[USPS_GETConfigurationFileByApplicationEnvironment]", parameters);
        }
    }
}
