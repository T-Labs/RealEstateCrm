using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Query.CitiesQuery;
using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace Data.Query.Handlers
{
    public class CitiesQueryHandler: IQueryHandler<CitiesAllQuery, List<City>>
    {
        public Task<List<City>> ExecuteAsync(ReadOnlyDataContext context, CitiesAllQuery query)
        {
            return context.Cities.ToListAsync();
        }
    }
}
