namespace BigMarket.Services.CouponAPI.CustomExceptions
{
    public class InvalidCouponIDException : ArgumentException
    {
        public InvalidCouponIDException() : base() { }
        public InvalidCouponIDException(string message) : base(message) { }
        public InvalidCouponIDException(string message, Exception innerException) : base(message, innerException) { }
    }
}
