namespace SS_Microservice.Services.Products.Core.Interfaces
{
    public interface IMongoDBSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}