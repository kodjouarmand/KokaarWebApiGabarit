using Microsoft.AspNetCore.Identity;

namespace Catsa.Domain.Entities
{
    public class UserAccount : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
