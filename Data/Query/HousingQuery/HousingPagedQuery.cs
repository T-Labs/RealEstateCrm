using Data.Query;
using WebApp.Data;
using WebApp.Entities;

namespace Data.Query
{
    public class HousingPagedQuery : IQuery<PagedResults<Housing>>
    {

        public int? Page { get; set; }
        public int[] HouseTypeId { get; set; } = new int[] { };
        public int? CityId { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public bool? IsArchived { get; set; }
        public int[] DistrictId { get; set; } = new int[] { };

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int? CustomerId { get; set; }
    }
}