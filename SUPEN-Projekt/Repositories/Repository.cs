

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;



namespace SUPEN_Projekt.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int? id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    
		public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions) {
			IDbSet<TEntity> dbSet = Context.Set<TEntity>();

			IQueryable<TEntity> query = null;
			foreach(var includeExpression in includeExpressions) {
				query = dbSet.Include(includeExpression);
			}

			return query ?? dbSet;
		}
    }
}