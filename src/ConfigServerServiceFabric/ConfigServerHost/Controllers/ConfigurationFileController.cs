using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigServerHost.Core.Domain;
using ConfigServerHost.Core.Domain.Query;
using ConfigServerHost.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConfigServerHost.Controllers
{
    [Produces("application/json")]
    [Route("v1")]
    public class ConfigurationFileController : Controller
    {
        private readonly IProcessHandlerAsync<QueryConfigurationFileRequest, IEnumerable<ConfigurationFileResponse>> handler;
        public ConfigurationFileController(IProcessHandlerAsync<QueryConfigurationFileRequest, IEnumerable<ConfigurationFileResponse>> handler)
        {
            this.handler = handler;
        }

        [HttpGet("{application}/{environment}")]
        public async Task<ServiceOperationResult> Get(string application, string environment)
        {
            //TODO: Validar la version del archivo para no descargar a cada momento
            var result = await handler.HandleAsync(new QueryConfigurationFileRequest { ApplicationName = application, Environment = environment });

            return ServiceOperationResult.Create(result);
        }
    }
}

