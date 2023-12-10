using SS_Microservice.Common.Types.Enums;

namespace SS_Microservice.Common.Attributes
{
    public class InternalCommunicationAPIAttribute : Attribute
    {
        public IEnumerable<APPLICATION_SERVICE> Services { get; set; }
        public InternalCommunicationAPIAttribute(params APPLICATION_SERVICE[] values)
        {
            Services = values;
        }
    }
}
