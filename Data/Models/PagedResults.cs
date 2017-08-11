using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Data
{
    public class PagedResults<T>
    {
        public PageInfo PageInfo { get; set; }
        public List<T> Items { get; set; }
    }
}
