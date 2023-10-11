using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Auth.Application.Features.User.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SS_Microservice.Services.Auth.Application.Specifications.User
{
    public class StaffSpecification : BaseSpecification<Domain.Entities.Staff>
    {
        public StaffSpecification(GetListStaffQuery request, bool isPaging = false)
        {
            AddInclude(x => x.User);
            var key = request.Search;
            if (!string.IsNullOrEmpty(key))
            {
                Criteria = x => x.User.Email.ToLower().Contains(key)
                || x.User.PhoneNumber.ToLower().Contains(key)
                || x.User.Gender.ToLower().Contains(key)
                || x.User.Dob.Value.ToString().Contains(key)
                || x.User.FirstName.ToLower().Contains(key)
                || x.User.LastName.ToLower().Contains(key)
                || x.Type.ToLower().Contains(key);
            }
            if (!isPaging) return;
            int skip = (request.PageIndex - 1) * request.PageSize;
            int take = request.PageSize;
            ApplyPaging(take, skip);
        }

        public StaffSpecification(long staffId) : base(x => x.Id == staffId)
        {
            AddInclude(x => x.User);
        }
    }
}