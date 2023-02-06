namespace GameShop.Common.Settings
{
    public class SqlServerSettings
    {
        public string? Host { get; init; }
        public int Port { get; init; }
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}