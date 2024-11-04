namespace CDN.Dtos;
public class UserAuditDto
{
    public DateTime Date { get; set; }

    public int Id { get; set; }

    public int Status { get; set; }

    public string Username { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string MobileNo { get; set; } = string.Empty;

    public string Skills { get; set; } = string.Empty;

    public string Hobby { get; set; } = string.Empty;
}
