namespace ConfigServerHost.Core.Domain.Command
{
    public class ConfigurationFile
    {
        public int Identifier { get; }
        public int ApplicationIdentifier { get; }
        public string Name { get; }
        public bool Enabled { get; }
        public string Version { get; }
        public string Content { get; }
        public string Environment { get; }
        public string Extension { get; }

        private ConfigurationFile(string name, int applicationIdentifier, string version, string content, string environment, string extension, bool enabled)
        {
            Name = name;
            Enabled = enabled;
            ApplicationIdentifier = applicationIdentifier;
            Version = version;
            Content = content;
            Environment = environment;
            Extension = extension;
        }
    }
}
