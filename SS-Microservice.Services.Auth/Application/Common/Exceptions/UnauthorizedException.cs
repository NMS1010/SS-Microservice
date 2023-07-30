using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Services.Auth.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("Token has been expired")
        {
        }

        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}