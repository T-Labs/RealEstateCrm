using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Helpers
{
    public interface IModelBuilder<TModel>
    {
        TModel Model { get; }
    }
    
    public interface IModelBuilder<TEntity, TModel>: IModelBuilder<TModel>
    {
        Task<TModel> BuildAsync(int id);
        TModel Build(TEntity id);
    }
}
