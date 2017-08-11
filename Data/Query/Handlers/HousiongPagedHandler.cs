using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp;
using WebApp.Data;
using WebApp.Entities;

namespace Data.Query.Handlers
{
    public class HousiongPagedHandler: IQueryHandler<HousingPagedQuery, PagedResults<Housing>>
    {
        public async Task<PagedResults<Housing>> ExecuteAsync(ReadOnlyDataContext context, HousingPagedQuery queryParams)
        {
            var query = context.Housing.IncludeAll().OrderByDescending(x => x.CreatedAt).AsQueryable();
            
            if (queryParams.CustomerId.HasValue)
            {
                var customerHandler = new CustomerQueryHandler();
                var customer =
                    await customerHandler.ExecuteAsync(context, new CustomerByIdQuery(queryParams.CustomerId.Value));

                if (customer == null)
                {
                    throw new Exception("Customer not found");
                }
                
                var houseTypeId = customer.TypesHousingToCustomers.Select(x => x.TypesHousingId).ToArray();
                var districtIds = customer.DistrictToClients.Select(x => x.DistrictId).ToArray();
                
                query = query.Where(x => houseTypeId.Contains(x.TypesHousingId))
                             .Where(x => districtIds.Contains(x.DistrictId))
                             .Where(x => x.CityId == customer.CityId);
            }
            else
            {
                if (queryParams.CityId.HasValue)
                {
                    query = query.Where(x => x.CityId == queryParams.CityId.Value);
                }

                if (queryParams.DistrictId.Length > 0)
                {
                    query = query.Where(x => queryParams.DistrictId.Contains(x.DistrictId));
                }

                if (queryParams.HouseTypeId.Length > 0)
                {
                    query = query.Where(x => queryParams.HouseTypeId.Contains(x.TypesHousingId));
                }
            }

            if (queryParams.PriceFrom.HasValue && queryParams.PriceTo.HasValue)
            {
                query = query.Where(x => x.Sum >= queryParams.PriceFrom.Value && x.Sum <= queryParams.PriceTo.Value);
            }
            else if (queryParams.PriceFrom.HasValue)
            {
                query = query.Where(x => x.Sum >= queryParams.PriceFrom.Value);
            }
            else if (queryParams.PriceTo.HasValue)
            {
                query = query.Where(x => x.Sum <= queryParams.PriceTo.Value);
            }

            if (queryParams.IsArchived.HasValue)
            {
                query = query.Where(x => x.IsArchive == queryParams.IsArchived.Value);
            }
            else
            {
                query = query.Where(x => x.IsArchive == false);
            }
            
            var items = await query.PagedResultAsync(queryParams.PageNumber, queryParams.PageSize);
            return items;
        }
    }
}
