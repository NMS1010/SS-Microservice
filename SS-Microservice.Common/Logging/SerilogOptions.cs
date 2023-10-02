using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Logging
{
    public class SerilogOptions
    {
        public bool ConsoleEnabled { get; set; }
        public string Level { get; set; }
    }
}