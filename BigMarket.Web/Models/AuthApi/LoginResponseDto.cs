namespace BigMarket.Web.Models.AuthAPI
{
    public sealed class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
