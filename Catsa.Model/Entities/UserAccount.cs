using Microsoft.AspNetCore.Identity;

namespace Catsa.Model.Entities
{
    public class UserAccount : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
