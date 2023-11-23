using Microsoft.AspNetCore.Identity;

namespace LR4.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
}
