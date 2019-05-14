﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories {
	//Ett generellt repository interface av typen IEntity
	public interface IRepository<TEntity> where TEntity : class {
		Task<TEntity> Get(int? id);
		Task<IEnumerable<TEntity>> GetAll();
		IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
		void Add(TEntity entity);
		void AddRange(IEnumerable<TEntity> entities);
		void Remove(TEntity entity);
		void RemoveRange(IEnumerable<TEntity> entities);

	}
}
