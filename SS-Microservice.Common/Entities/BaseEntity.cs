using SS_Microservice.Common.Entities.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Entities
{
    public class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}