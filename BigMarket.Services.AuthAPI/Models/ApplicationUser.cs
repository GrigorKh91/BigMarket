using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BigMarket.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(200)]
        public string  Name { get; set; }
    }
}
