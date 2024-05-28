#pragma warning disable S3903 // Types should be defined in named namespaces
internal class DatabaseConfig
#pragma warning restore S3903 // Types should be defined in named namespaces
{
    public string Datasource { get; set; }
    public string Database { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }
    public string Provider { get; set; }
    public byte ConnectTimeout { get; set; }
    public byte ConnectRetryCount { get; set; }
    public byte ConnectRetryInterval { get; set; }
}