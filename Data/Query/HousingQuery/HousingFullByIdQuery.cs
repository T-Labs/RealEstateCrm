using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Entities;

namespace Data.Query
{
    public class HousingFullByIdQuery : IQuery<Housing>
    {
            public int Id { get; }

            public HousingFullByIdQuery(int id)
            {
                Id = id;
            }
    }
}
