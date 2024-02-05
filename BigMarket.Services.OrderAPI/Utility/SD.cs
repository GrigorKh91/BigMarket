namespace BigMarket.Services.OrderAPI.Utility
{
    public sealed class SD
    {
        public const string Status_Pending = "Pending";
        public const string Status_Approved = "Approved";
        public const string Status_ReadyForPickup = "ReadyForPickup";
        public const string Status_Completed = "Completed";
        public const string Status_Refunded = "Refunded";
        public const string Status_Canceled = "Canceled";

        public const string Role_Admin = "ADMIN";
        public const string Role_Customer = "CUSTOMER";
    }
}
