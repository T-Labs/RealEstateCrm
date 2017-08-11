using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp;
using WebApp.Entities;

namespace Data.Query.Handlers
{
    public class HousiongQueryHandler: 
        IQueryHandler<HousingFullByIdQuery, Housing>,
        IQueryHandler<HousingExistQuery, bool>,
        IQueryHandler<HousingCountQuery, int>
    {
        public Task<Housing> ExecuteAsync(ReadOnlyDataContext context, HousingFullByIdQuery query)
        {
            return context.Housing.IncludeAll().SingleOrDefaultAsync(m => m.Id == query.Id);
        }

        public async Task<bool> ExecuteAsync(ReadOnlyDataContext context, HousingExistQuery query)
        {
            var item = await context.Housing.FirstOrDefaultAsync(x => x.Id == query.Id);
            return item != null;
        }

        public Task<int> ExecuteAsync(ReadOnlyDataContext context, HousingCountQuery query)
        {
            return context.Housing.CountAsync();
        }
    }
}
