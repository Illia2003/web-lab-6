using Microsoft.AspNetCore.Identity;

namespace LR4.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole(string name) : base(name) { }
}

