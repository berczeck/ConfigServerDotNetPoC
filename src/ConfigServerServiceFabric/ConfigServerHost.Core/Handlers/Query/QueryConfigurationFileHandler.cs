using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigServerHost.Core.Domain;
using ConfigServerHost.Core.Domain.Query;
using ConfigServerHost.Core.Interfaces;

namespace ConfigServerHost.Core.Handlers.Query
{
    public class QueryConfigurationFileHandler : IProcessHandlerAsync<QueryConfigurationFileRequest, IEnumerable<ConfigurationFileResponse>>
    {
        private readonly IConfigurationFileRepository configurationFileRepository;

        public QueryConfigurationFileHandler(IConfigurationFileRepository configurationFileRepository)
        {
            this.configurationFileRepository = configurationFileRepository;
        }
        public async Task<OperationResult<IEnumerable<ConfigurationFileResponse>>> HandleAsync(QueryConfigurationFileRequest request)
        {
            var result = await configurationFileRepository.GetLastVersionByApplicationEnvironment(request.ApplicationName, request.Environment);

            if (result.Failure)
            {
                //TODO: Devlver
            }

            return result;
        }
    }
}
