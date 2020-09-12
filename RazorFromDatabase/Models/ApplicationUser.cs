
using Microsoft.AspNetCore.Identity;

namespace RazorFromDatabase.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUserStatus Status { get; set; }
    }
}
