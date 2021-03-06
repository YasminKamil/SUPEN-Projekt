﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories {
	//En generell repository med dess metoder
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {

		protected readonly DbContext Context;

		public Repository(DbContext context) {
			Context = context;
		}

		public async Task<TEntity> Get(int? id) {
			return await Context.Set<TEntity>().FindAsync(id);
		}

		public async Task<IEnumerable<TEntity>> GetAll() {
			return await Context.Set<TEntity>().ToListAsync();
		}

		public void Add(TEntity entity) {
			Context.Set<TEntity>().Add(entity);
		}

		public void AddRange(IEnumerable<TEntity> entities) {
			Context.Set<TEntity>().AddRange(entities);
		}

		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) {
			return Context.Set<TEntity>().Where(predicate);
		}

		public void Remove(TEntity entity) {
			Context.Set<TEntity>().Remove(entity);
		}

		public void RemoveRange(IEnumerable<TEntity> entities) {
			Context.Set<TEntity>().RemoveRange(entities);
		}
	}
}