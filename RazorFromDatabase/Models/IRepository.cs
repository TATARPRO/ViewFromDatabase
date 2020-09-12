using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorFromDatabase.Models
{
    public interface IRepository<TModel, TId> where TModel : class
    {
        void Delete(Func<TModel, bool> expression);

        void Delete(TId Id);

        Task Insert(TModel Entity);

        Task Insert(IEnumerable<TModel> Entities);

        IQueryable<TModel> Return();

        Task SaveChangesAsync();

        void SaveChanges();

        void Update(TModel entity);
    }
    public interface ILongRepository<TModel> : IRepository<TModel, long> where TModel : class
    {

    }

}
