using Microsoft.AspNetCore.Identity;
using MohDemo.DataAccess.Data.Repository.IRepository;

namespace MohDemo.DataAccess.Data.Repository
{
	public class RoleRepository : Repository<IdentityRole>, IRoleRepository
	{
		private readonly ApplicationDbContext _db;

		public RoleRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}