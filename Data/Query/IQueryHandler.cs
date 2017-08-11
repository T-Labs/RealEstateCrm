using System.Threading.Tasks;

namespace Data.Query
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
        
    {
        Task<TResult> ExecuteAsync(ReadOnlyDataContext context, TQuery query);
    }
}