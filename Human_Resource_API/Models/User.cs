using Microsoft.AspNetCore.Identity;

namespace Human_Resource_API.Models
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
    }
}
