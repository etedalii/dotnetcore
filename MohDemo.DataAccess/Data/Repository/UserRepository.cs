using MohDemo.DataAccess.Data.Repository.IRepository;
using MohDemo.Models;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace MohDemo.DataAccess.Data.Repository
{
	public class UserRepository : Repository<ApplicationUser>, IUserRepository
	{
		private readonly ApplicationDbContext _db;

		public UserRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public string GetFullName(string userName)
		{
			var user = _db.ApplicationUsers.FirstOrDefault(_ => _.UserName == userName);
			return string.Format("{0} {1}", user.Name, user.Family);
		}

		public string GetUserId(string userName)
		{
			var user = _db.ApplicationUsers.FirstOrDefault(_ => _.UserName == userName);
			return user.Id;
		}

		public void LockUser(string userId)
		{
			var user = _db.ApplicationUsers.FirstOrDefault(_ => _.Id == userId);
			user.LockoutEnd = DateTime.Now.AddHours(12);
			_db.SaveChanges();
		}

		public void UnLockUser(string userId)
		{
			var user = _db.ApplicationUsers.FirstOrDefault(_ => _.Id == userId);
			user.LockoutEnd = DateTime.Now;
			_db.SaveChanges();
		}
	}
}