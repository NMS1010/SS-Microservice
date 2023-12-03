namespace SS_Microservice.Services.Basket.Application.Common.Enums
{
    public static class PRODUCT_STATUS
    {
        public const string ACTIVE = "ACTIVE";
        public const string INACTIVE = "INACTIVE";
        public const string SOLD_OUT = "SOLD_OUT";

        public static List<string> Status = new()
        {
            ACTIVE,
            INACTIVE,
            SOLD_OUT
        };
    }
}
