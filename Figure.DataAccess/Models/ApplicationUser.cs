using Microsoft.AspNetCore.Identity;

namespace Figure.DataAccess.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
}

