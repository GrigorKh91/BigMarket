namespace BigMarket.Web.Utility
{
    public class SD // static details
    {
        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }

        public const string RolaAdmin = "ADMIN";
        public const string RolaCastomer = "CASTOMER";
        public const string TokenCookie = "JWTToken";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
