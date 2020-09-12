using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RazorFromDatabase.Data;

namespace RazorFromDatabase.Models
{
    public class Repository<TModel, TId> : IRepository<TModel, TId> where TModel: class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TModel> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }

        /// <summary>
        /// Deletes all the elements in the tracking collection that matches the query <paramref name="expression" />.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public void Delete(Func<TModel, bool> expression)
        {
            IEnumerable<TModel> entities = _dbSet.Where(expression);
            if (entities != null)
            {
                _dbSet.RemoveRange(entities);
            }
        }

        /// <summary>
        /// Deletes an entity from the tracking collection with the requested <paramref name="Id" /> which is the primary key.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public void Delete(TId Id)
        {
            TModel entity = _dbSet.Find(Id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        /// <summary>
        /// Inserts a new <paramref name="Entity" /> into the database.
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns>TEntity Status</returns>
        public async Task Insert(TModel entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Inserts a list of entities presented to the tracking entity.
        /// </summary>
        /// <param name="Entities"></param>
        /// <returns>OperationResult</returns>
        public async Task Insert(IEnumerable<TModel> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public IQueryable<TModel> Return()
        {
            return _dbSet.AsQueryable();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {

            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(TModel entity)
        {
            _dbSet.Update(entity);
        }
    }

    public class LongRepository<TModel> : Repository<TModel, long>, ILongRepository<TModel> where TModel: class
    {
        public LongRepository(ApplicationDbContext context) : base(context)
        {

        }
    }

    public class StringRepository<TModel> : Repository<TModel, string> where TModel : class
    {
        public StringRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
