namespace ConfigServerHost.Core.Domain.Query
{
    public class QueryConfigurationFileRequest
    {
        public string ApplicationName { get; set; }
        public string Environment { get; set; }
    }
}
