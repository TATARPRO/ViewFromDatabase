
using Microsoft.AspNetCore.Identity;

namespace ViewsFromDatabase.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUserStatus Status { get; set; }
    }
}
