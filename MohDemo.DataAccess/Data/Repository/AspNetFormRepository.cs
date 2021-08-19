using MohDemo.DataAccess.Data.Repository.IRepository;
using MohDemo.Models;

namespace MohDemo.DataAccess.Data.Repository
{
	public class AspNetFormRepository: Repository<AspNetForms> , IAspNetFormRepository
	{
		private readonly ApplicationDbContext _db;

		public AspNetFormRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}