using MohDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MohDemo.DataAccess.Data.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		T Find(int id);

		Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
			string includeProperties = null);

		IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
			string includeProperties = null);

		T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null);

		void Insert(T entity);

		void Update(T entity);

		void Remove(int id);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);

		int Count();
		int Count(Expression<Func<T, bool>> expression);
		Task<int> CountAsync();
		Task<int> CountAsync(Expression<Func<T, bool>> expression);

		int TotalRows(WhereClause expression);
		IQueryable<T> Where(Expression<Func<T, bool>> expression);

		/// <summary>
		/// This method use for auto complete fields
		/// </summary>
		/// <param name="field">you must pass one parameter or more with comma seprated type</param>
		/// <returns></returns>
		dynamic Select(string field);
	}
}