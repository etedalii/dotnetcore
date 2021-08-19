using Microsoft.AspNetCore.Identity;
using MohDemo.DataAccess.Data.Repository.IRepository;

namespace MohDemo.DataAccess.Data.Repository
{
	public class UserRoleRepository : Repository<IdentityUserRole<string>>, IUserRoleRepository
	{
		private readonly ApplicationDbContext _db;

		public UserRoleRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}