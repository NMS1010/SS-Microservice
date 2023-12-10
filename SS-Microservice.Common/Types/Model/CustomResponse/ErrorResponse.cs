using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Types.Model.CustomResponse
{
    public class ErrorResponse
    {
        [JsonPropertyName("type")]
        public Uri Type { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonPropertyName("instance")]
        public string Instance { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("trace_id")]
        public string TraceId { get; set; }

        [JsonPropertyName("violations")]
        public List<Violations> Violations { get; set; } = new List<Violations>();
    }
}