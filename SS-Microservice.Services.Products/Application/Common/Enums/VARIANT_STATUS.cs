namespace SS_Microservice.Services.Products.Application.Common.Enums
{
	public class VARIANT_STATUS
	{
		public const string ACTIVE = "ACTIVE";
		public const string INACTIVE = "INACTIVE";

		public static List<string> Status = new()
		{
			ACTIVE,
			INACTIVE
		};
	}
}