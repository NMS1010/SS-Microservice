namespace SS_Microservice.Common.RabbitMQ
{
    public static class EventBusConstant
    {
        // command
        public const string CreateAddress = "create-address-command";
        public const string UpdateAddress = "update-address-command";
        public const string ClearBasket = "clear-basket-command";
        public const string ExportInventory = "export-inventory-command";
        public const string RollBackInventory = "rollback-inventory-command";
        public const string ReserveStock = "reserve-stock-command";
        public const string RollBackStock = "rollback-stock-command";
        public const string UpdateProductQuantity = "update-product-quantity-command";

        public const string SendMail = "send-mail-command";

        public const string UpdateProductRating = "update-product-rating-command";

        // events
        public const string BasketCleared = "basket-cleared-event";
        public const string BasketClearedRejected = "basket-cleared-rejected-event";

        public const string InventoryExported = "inventory-exported-event";
        public const string InventoryExportationRejected = "inventory-exportation-rejected-event";

        public const string OrderCancelled = "order-cancelled-event";
        public const string OrderCreated = "order-created-event";
        public const string OrderCreationCompleted = "order-creation-completed-event";
        public const string OrderCreationRejected = "order-creation-rejected-event";
        public const string OrderPaypalCompleted = "order-paypal-completed-event";
        public const string OrderStatusUpdated = "order-status-updated-event";

        public const string StockReserved = "stock-reserved-event";
        public const string StockReservationRejected = "stock-reservation-rejected-event";
        public const string UserRegisted = "user-registed-event";

    }
}