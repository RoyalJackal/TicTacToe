using Microsoft.AspNetCore.Identity;

namespace Data.Models.Users;

public class User : IdentityUser
{
    public int Games { get; set; }
    
    public int Wins { get; set; }
}