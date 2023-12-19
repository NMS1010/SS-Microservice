namespace SS_Microservice.Common.Services.CurrentUser
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        public string Email { get; }
        public string UserName { get; }
        bool IsInRole(string role);
    }
}