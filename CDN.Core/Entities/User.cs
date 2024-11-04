namespace CDN.Core.Entities;
public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string MobileNo { get; set; } = string.Empty;

    public string Skills { get; set; } = string.Empty;

    public string Hobby { get; set; } = string.Empty;
}
