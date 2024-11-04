using System.ComponentModel.DataAnnotations;

namespace CDN.Requests;
public class UserSaveRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "EmailAddress is required")]
    public string EmailAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "MobileNo is required")]
    public string MobileNo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Skills is required")]
    public string Skills { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hobby is required")]
    public string Hobby { get; set; } = string.Empty;
}
