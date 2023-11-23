using System.ComponentModel;

namespace LR4.Models;

public class RegisterModel
{
    [DisplayName("Your first name")]
    public string FirstName { get; set; }

    [DisplayName("Your last name")]
    public string LastName { get; set; }

    [DisplayName("Your email")]
    public string Email { get; set; }

    [DisplayName("Your password")]
    public string Password { get; set; }
}
