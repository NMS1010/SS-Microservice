namespace SS_Microservice.Common.Services.CurrentUser
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        bool IsInRole(string role);
    }
}