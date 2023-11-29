using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Products.Application.Model.Unit
{
    public class UpdateUnitRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; }

        public bool Status { get; set; }
    }
}