using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Models;

namespace Data.Query
{
    public class UserByIdQuery: IQuery<ApplicationUser>
    {
        public string Id { get; }

        public UserByIdQuery(string id)
        {
            Id = id;
        }
    }
}
