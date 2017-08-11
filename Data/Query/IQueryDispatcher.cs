using System.Threading.Tasks;

namespace Data.Query
{
    public interface IQueryDispatcher
    {
        Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;
    }
}