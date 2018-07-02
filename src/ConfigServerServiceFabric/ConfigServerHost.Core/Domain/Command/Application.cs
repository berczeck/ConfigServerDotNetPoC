namespace ConfigServerHost.Core.Domain.Command
{
    public class Application
    {
        public int Identifier { get; }
        public int SystemIdentifier { get; }
        public string Name { get; }
        public bool Enabled { get; }

        private Application(string name, int systemIdentifier, bool enabled)
        {
            Name = name;
            Enabled = enabled;
            SystemIdentifier = systemIdentifier;
        }

        public static Application CreateNewSystem(string name, int systemIdentifier)
        {
            return new Application(name, systemIdentifier, true);
        }
    }
}
