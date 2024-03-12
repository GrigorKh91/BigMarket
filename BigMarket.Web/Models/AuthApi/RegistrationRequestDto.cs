using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BigMarket.Web.Models.AuthAPI
{
    public sealed class RegistrationRequestDto
    {
        [Required]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Auth", ErrorMessage = "Email is already is use")]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
