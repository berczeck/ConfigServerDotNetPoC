using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigServerHost.Core.Domain;
using ConfigServerHost.Core.Domain.Query;

namespace ConfigServerHost.Core.Interfaces
{
    public interface IConfigurationFileRepository
    {
        Task<OperationResult<IEnumerable<ConfigurationFileResponse>>> GetLastVersionByApplicationEnvironment(string applicationName, string environment);
    }
}
