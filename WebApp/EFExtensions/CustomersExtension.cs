using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp
{
    public class CustomersExtension
    {
        public class FilterData
        {
            public int? Page { get; set; }
            public int[] HouseTypeIds { get; set; } = new int[] { };
            public int? CityId { get; set; }
            public int? PriceFrom { get; set; }
            public int? PriceTo { get; set; }
            public bool? IsArchived { get; set; }
            public bool? IsSiteAccessOnly { get; set; }
            public int[] DistrictIds { get; set; } = new int[] { };
        }


        public static Func<Customer, bool> Filter(FilterData filter)
        {
            Func<Customer, bool> func = (x) =>
            {
                bool result = true;
                if (filter.CityId.HasValue)
                {
                    result &= x.CityId == filter.CityId.Value;
                }

                if (filter.DistrictIds.Length > 0)
                {
                    result &= filter.DistrictIds.All(districtId => x.DistrictToClients.Any(d => d.DistrictId == districtId));//x.DistrictToClients == filter.DistrictId.Value;
                }

                if (filter.HouseTypeIds.Length > 0)
                {
                    result &= filter.HouseTypeIds.All(f => x.TypesHousingToCustomers.Any(type => type.TypesHousingId == f));
                }

                if (filter.PriceFrom.HasValue && filter.PriceTo.HasValue)
                {
                    result &= x.MinSum <= filter.PriceFrom.Value && x.MaxSum >= filter.PriceTo.Value;
                }
                else if (filter.PriceFrom.HasValue)
                {
                    result &= x.MinSum <= filter.PriceFrom.Value;
                }
                else if (filter.PriceTo.HasValue)
                {
                    result &= x.MaxSum >= filter.PriceTo.Value;
                }

                if (filter.IsArchived.HasValue)
                {
                    result &= x.IsArchive == filter.IsArchived.Value;
                }
                else
                {
                    result &= x.IsArchive == false;
                }

                if (filter.IsSiteAccessOnly.HasValue && filter.IsSiteAccessOnly.Value)
                {
                    result &= x.IsSiteAccess == true;
                }
                return result;
            };

            return func;
        }
    }
}
