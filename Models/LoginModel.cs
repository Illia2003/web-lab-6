using System.ComponentModel;

namespace LR4.Models;

public class LoginModel
{
    [DisplayName("Your email")]
    public string Username { get; set; }

    [DisplayName("Your password")]
    public string Password { get; set; }

    public string? ReturnUrl { get; set; }
}
