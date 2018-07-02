namespace ConfigServerHost.Core.Domain.Command
{
    public class System
    {
        public int Identifier { get; set; }
        public string Name { get; }
        public bool Enabled { get; }

        private System(string name, bool enabled)
        {
            Name = name;
            Enabled = enabled;
        }

        public static System CreateNewSystem(string name)
        {
            return new System(name, true);
        }
    }
}
