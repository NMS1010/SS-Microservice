using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Model.CustomResponse
{
    public class Violations
    {
        public string Field { get; set; }
        public List<string> Messages { get; set; }
    }
}