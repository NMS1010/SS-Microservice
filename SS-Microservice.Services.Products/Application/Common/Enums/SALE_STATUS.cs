namespace SS_Microservice.Services.Products.Application.Common.Enums
{
	public class SALE_STATUS
	{
		public const string ACTIVE = "ACTIVE";
		public const string INACTIVE = "INACTIVE";
		public const string EXPIRED = "EXPIRED";

		public static List<string> Status = new()
		{
			ACTIVE,
			INACTIVE,
			EXPIRED
		};
	}
}