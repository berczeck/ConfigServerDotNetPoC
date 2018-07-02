using System.Threading.Tasks;
using ConfigServerHost.Core.Domain;
using ConfigServerHost.Core.Domain.Query;
using ConfigServerHost.Core.Handlers.Query;
using ConfigServerHost.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ConfigServerHost.Controllers
{
    [Produces("application/json")]
    [Route("v1")]
    public class ConfigurationFileController : Controller
    {
        [HttpGet("{application}/{environment}/{version?}")]
        public async Task<ServiceOperationResult> Get(string application, string environment, string version = "")
        {
            var repository = new ConfigurationFileRepository("Data Source=WAEDRMEX01-2861\\SQLEXPRESS;Initial Catalog=ConfigServer;User ID=configserver;Password=configserver");
            var handler = new QueryConfigurationFileHandler(repository);
            var result = await handler.HandleAsync(new QueryConfigurationFileRequest { ApplicationName = application, Environment = environment });

            return ServiceOperationResult.Create(result);
        }
    }
}

