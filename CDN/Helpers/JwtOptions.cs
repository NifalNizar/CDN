namespace CDN.Helpers;
public class JwtOptions
{
    public const string Key = "JWT";

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string Secret { get; set; } = string.Empty;

    public int Duration { get; set; }
}
