using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories

{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int? id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
		//   Task<bool> APIContact(string inUrl, TEntity inObject);

		IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions);

	}
}
