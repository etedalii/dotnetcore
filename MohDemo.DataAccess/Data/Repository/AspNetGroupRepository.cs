using MohDemo.DataAccess.Data.Repository.IRepository;
using MohDemo.Models;

namespace MohDemo.DataAccess.Data.Repository
{
	public class AspNetGroupRepository : Repository<AspNetGroups> , IAspNetGroupRepository
	{
		private readonly ApplicationDbContext _db;

		public AspNetGroupRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

	}
}