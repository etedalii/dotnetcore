using Microsoft.EntityFrameworkCore;
using MohDemo.DataAccess.Data.Repository.IRepository;
using MohDemo.Models;
using MohDemo.Models.BaseFolder;
using MohDemo.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MohDemo.DataAccess.Data.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly DbContext context;
		internal DbSet<T> dbSet;

		public Repository(DbContext context)
		{
			this.context = context;
			this.dbSet = context.Set<T>();
		}

		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public int Count()
		{
			return dbSet.Count();
		}

		public int Count(Expression<Func<T, bool>> expression)
		{
			return dbSet.Where(expression).Count();
		}

		public T Find(int id)
		{
			return dbSet.Find(id);
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = null)
		{
			IQueryable<T> query = dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (includeProperties != null)
			{
				foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(item);
				}
			}

			if (orderby != null)
			{
				return orderby(query).ToList();
			}

			return query.ToList();
		}
		
		public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = null)
		{
			IQueryable<T> query = dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (includeProperties != null)
			{
				foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(item);
				}
			}

			if (orderby != null)
			{
				return orderby(query).ToList();
			}

			return await query.ToListAsync();
		}

		public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
		{
			IQueryable<T> query = dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (includeProperties != null)
			{
				foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(item);
				}
			}

			return query.FirstOrDefault();
		}

		public void Insert(T entity)
		{
			if (entity.GetType().GetProperty("CreatorId") != null)
			{

				BaseModel newT2 = (BaseModel)(object)entity;
				newT2.CreationDate = DateTime.Now;
				newT2.CreatorId = Global.UserId;
				newT2.LastModifierId = Global.UserId;
			}
			dbSet.Add(entity);
		}

		public void Update(T entity)
		{
			if (entity.GetType().GetProperty("LastModifierId") != null)
			{
				BaseModel newT2 = (BaseModel)(object)entity;
				newT2.LastModificationDate = DateTime.Now;
				newT2.LastModifierId = Global.UserId;
			}
			dbSet.Update(entity);
		}

		public void Remove(int id)
		{
			T entityToRemove = dbSet.Find(id);
			Remove(entityToRemove);
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> expression)
		{
			return dbSet.Where(expression);
		}

		public int TotalRows(WhereClause expression)
		{
			return dbSet.Where(expression.WhereStatement, expression.Parameters).Count();
		}

		public dynamic Select(string field)
		{
			IQueryable query = dbSet.Select("new(" + field + " as Name)");
			List<dynamic> results = new List<dynamic>();
			foreach (var item in query)
			{
				string str = item.ToString().Replace("{", "").Replace("}", "");
				str = str.Substring(str.LastIndexOf("=") + 1);
				results.Add(str);
			}
			return results;
		}

		public async Task<int> CountAsync()
		{
			return await dbSet.CountAsync();
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
		{
			return await dbSet.Where(expression).CountAsync();
		}
	}
}