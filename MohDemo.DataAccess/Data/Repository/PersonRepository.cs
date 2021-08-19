using MohDemo.DataAccess.Data.Repository.IRepository;
using MohDemo.Models.DomainClasses;

namespace MohDemo.DataAccess.Data.Repository
{
	public class PersonRepository : Repository<Person> , IPersonRepository
	{
		private readonly ApplicationDbContext _db;

		public PersonRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}