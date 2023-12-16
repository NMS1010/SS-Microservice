namespace SS_Microservice.Common.RabbitMQ
{
    public static class EventBusConstant
    {
        public const string UserRegisted = "user-registed-queue";
        public const string ExportInventory = "export-inventory-queue";
        public const string ReserveStock = "reserve-stock-queue";
        public const string ClearBasket = "clear-basket-queue";
        public const string RollBackInventory = "rollback-inventory-queue";
        public const string RollBackStock = "rollback-stock-queue";
    }
}