namespace SS_Microservice.Services.Order.Application.Common.Constants
{
    public static class ORDER_STATUS
    {
        public const string DRAFT = "DRAFT";
        public const string NOT_PROCESSED = "NOT_PROCESSED";
        public const string PROCESSING = "PROCESSING";
        public const string SHIPPED = "SHIPPED";
        public const string DELIVERED = "DELIVERED";
        public const string CANCELLED = "CANCELLED";

        public static List<string> Status = new()
        {
            DRAFT,
            NOT_PROCESSED,
            PROCESSING,
            SHIPPED,
            DELIVERED,
            CANCELLED
        };

        public static Dictionary<string, string> OrderStatusSubTitle = new()
        {
            {DRAFT, "Tạm thời"},
            {NOT_PROCESSED, "Chưa xử lý"},
            {PROCESSING, "Đang được xử lý"},
            {SHIPPED, "Đang được vận chuyển"},
            {DELIVERED, "Đã giao hàng"},
            {CANCELLED, "Bị huỷ"},
        };
    }
}