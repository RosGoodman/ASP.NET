
namespace MetricsAgent.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
    }
}
