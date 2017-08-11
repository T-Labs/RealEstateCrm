using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace Data.Query.Handlers
{
    public class TypesHousingHandler: IQueryHandler<AllTypesHousingQuery, List<TypesHousing>>
    {
        public Task<List<TypesHousing>> ExecuteAsync(ReadOnlyDataContext context, AllTypesHousingQuery query)
        {
            return context.TypesHousing.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
