namespace SS_Microservice.Services.Order.Application.Common.Constants
{
    public static class ORDER_STATUS
    {
        public const string NOT_PROCESSED = "NOT_PROCESSED";
        public const string PROCESSING = "PROCESSING";
        public const string SHIPPED = "SHIPPED";
        public const string DELIVERED = "DELIVERED";
        public const string CANCELLED = "CANCELLED";

        public static List<string> Status = new()
        {
            NOT_PROCESSED,
            PROCESSING,
            SHIPPED,
            DELIVERED,
            CANCELLED
        };

        public static Dictionary<string, string> OrderStatusSubTitle = new()
        {
            {NOT_PROCESSED, "Chưa xử lý"},
            {PROCESSING, "Đang được xử lý"},
            {SHIPPED, "Đang được vận chuyển"},
            {DELIVERED, "Đã giao hàng"},
            {CANCELLED, "Bị huỷ"},
        };
    }
}