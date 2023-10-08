using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Jwt
{
    public class JwtConfig
    {
        public string Issuer { get; set; }
        public string Key { get; set; }
    }
}