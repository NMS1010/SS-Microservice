﻿using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Products.Application.Dto
{
    public class UnitDto : BaseAuditableEntity<long>
	{
		public string Name { get; set; }
		public bool Status { get; set; }
	}
}