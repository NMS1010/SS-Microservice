using RabbitMQ.Client;

namespace SS_Microservice.Services.Order.Application.Common
{
    public static class UtilMethod
    {
        public static string GenerateCode()
        {
            return Guid.NewGuid().ToString();
        }
    }
}