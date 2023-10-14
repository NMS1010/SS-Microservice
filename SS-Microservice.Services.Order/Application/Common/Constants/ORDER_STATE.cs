using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Constants
{
    public static class ORDER_STATE
    {
        public static string ORDER_DRAFT = "ORDER_DRAFT";
        public static string ORDER_PENDING = "ORDER_PENDING";
        public static string ORDER_HANDLING = "ORDER_HANDLING";
        public static string ORDER_ON_THE_WAY = "ORDER_ON_THE_WAY";
        public static string ORDER_COMPLETED = "ORDER_COMPLETED";
        public static string ORDER_CANCELED = "ORDER_CANCELED";
        public static string ORDER_FAILED = "ORDER_FAILED";
    }
}