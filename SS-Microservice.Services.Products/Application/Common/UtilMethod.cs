using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Entities.Intefaces;
using SS_Microservice.Common.Services.CurrentUser;

namespace SS_Microservice.Services.Products.Application.Common
{
    public class UtilMethod
    {
        private static ICurrentUserService _currentUserService;

        public UtilMethod(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public static void SetAuditable(DateTime now, IAuditableEntity<string> entity, bool isUpdate = false)
        {
            entity.UpdatedDate = now;
            entity.UpdatedBy = _currentUserService?.UserId ?? "system";
            if (!isUpdate)
            {
                entity.CreatedDate = now;
                entity.CreatedBy = _currentUserService?.UserId ?? "system";
            }
        }
    }
}