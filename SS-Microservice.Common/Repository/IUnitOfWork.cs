namespace SS_Microservice.Common.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;

        Task CreateTransaction();

        Task Commit();

        Task Rollback();

        Task<int> Save();
    }
}