using Microsoft.AspNetCore.Identity;
using System;

namespace Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public DateTime? BirthDate { get; set; }
    }
}
