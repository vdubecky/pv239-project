namespace ChatAppBackend.Settings;

public class AppSettings
{
    public required JwtSettings JwtSettings { get; set; }
}

public class JwtSettings
{
    public required string SigningKey { get; set; }
    public required int ExpiryInSeconds { get; set; }
}