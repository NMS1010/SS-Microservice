using SS_Microservice.Common.Types.Enums;

namespace SS_Microservice.Common.Logging.Messaging
{
    public static class LoggerMessaging
    {
        public static string StartPublishing(APPLICATION_SERVICE service, string name, string handlerName)
        {
            return $"[{service}] Start publishing {name} from {handlerName}";
        }

        public static string CompletePublishing(APPLICATION_SERVICE service, string name, string handlerName)
        {
            return $"[{service}] {name} from {handlerName} is published";
        }
    }
}
