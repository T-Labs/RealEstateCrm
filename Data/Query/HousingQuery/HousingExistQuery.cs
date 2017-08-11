using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Query
{
    public class HousingExistQuery: IQuery<bool>
    {
        public int Id { get; }

        public HousingExistQuery(int id)
        {
            Id = id;
        }
    }
}
