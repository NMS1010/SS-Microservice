using RabbitMQ.Client;

namespace SS_Microservice.Services.Order.Application.Common
{
    public static class UtilMethod
    {
        public static string GenerateCode()
        {
            var guid = Guid.NewGuid().ToString();

            return guid.Replace("-", string.Empty).ToUpper();
        }
    }
}