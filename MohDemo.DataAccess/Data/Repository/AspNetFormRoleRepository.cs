using System;
using System.Collections.Generic;
using System.Text;
using MohDemo.DataAccess.Data.Repository.IRepository;
using MohDemo.Models;

namespace MohDemo.DataAccess.Data.Repository
{
	public class AspNetFormRoleRepository : Repository<AspNetFormRoles>, IAspNetFormRoleRepository
	{
		private readonly ApplicationDbContext _db;

		public AspNetFormRoleRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}