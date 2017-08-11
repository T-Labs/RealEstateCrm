using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;
using WebApp;

namespace Data.Query
{
    //https://www.future-processing.pl/blog/cqrs-simple-architecture/
    public interface IQuery<TResult>
    {
    }
}
