using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Metrics
{
    public class MetricsOptions
    {
        public bool Enabled { get; set; }
        public bool PrometheusEnabled { get; set; }
        public string PrometheusFormatter { get; set; }
        public int Interval { get; set; }
        public string Env { get; set; }
    }
}