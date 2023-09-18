using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Entities.Intefaces
{
    public interface IAuditableEntity<T> : IEntity<T>
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
    }
}