using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace Data.Query.Handlers
{
    public class CustomerQueryHandler: IQueryHandler<CustomerByIdQuery, Customer>
    {
        public Task<Customer> ExecuteAsync(ReadOnlyDataContext context, CustomerByIdQuery query)
        {
            var customer = context.Clients
                            .Include(x => x.TypesHousingToCustomers)
                            .Include(x => x.DistrictToClients)
                            .FirstOrDefaultAsync(x => x.Id == query.Id);
            return customer;
        }
    }
}
