namespace ConfigServerHost.Core.Domain.Query
{
    public class ConfigurationFileResponse
    {
        public int Identifier { get; set; }
        public int ApplicationIdentifier { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Version { get; set; }
        public string Content { get; set; }
        public string Environment { get; set; }
        public string Extension { get; set; }
    }
}
