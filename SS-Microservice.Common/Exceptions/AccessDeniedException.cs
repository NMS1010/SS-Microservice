using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Services.Auth.Application.Common.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException() : base("Not permission to access this resource")
        { }

        public AccessDeniedException(string message) : base(message)
        {
        }
    }
}