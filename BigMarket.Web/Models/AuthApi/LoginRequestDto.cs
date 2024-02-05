using System.ComponentModel.DataAnnotations;

namespace BigMarket.Web.Models.AuthAPI
{
    public sealed class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
