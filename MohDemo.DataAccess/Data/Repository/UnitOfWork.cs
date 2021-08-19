
using MohDemo.DataAccess.Data.Repository.IRepository;
using MohDemo.Utility;

namespace MohDemo.DataAccess.Data.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;

		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;

			AspNetForm = new AspNetFormRepository(_db);
			AspNetFormRole = new AspNetFormRoleRepository(_db);
			AspNetGroup = new AspNetGroupRepository(_db);
			Person = new PersonRepository(_db);
			Sp_Call = new Sp_Call(_db);
			Users = new UserRepository(_db);
			UserRole = new UserRoleRepository(_db);
			Role = new RoleRepository(_db);
		}

		public IAspNetFormRepository AspNetForm { get; private set; }
		public IAspNetFormRoleRepository AspNetFormRole { get; private set; }
		public IAspNetGroupRepository AspNetGroup { get; private set; }
		public IPersonRepository Person { get; private set; }
		public ISp_Call Sp_Call { get; private set; }
		public IRoleRepository Role { get; private set; }
		public IUserRoleRepository UserRole { get; private set; }
		public IUserRepository Users { get; private set; }

		public void BeginTransaction()
		{
			_db.Database.BeginTransaction();
		}

		public void Commit()
		{
			_db.Database.CommitTransaction();
		}

		public void Dispose()
		{
			_db.Dispose();
		}

		public void Rollback()
		{
			_db.Database.RollbackTransaction();
		}

		public int Save(bool isDelete = false)
		{
			try
			{
				_db.SaveChanges();
				
			}
			catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
			{
				if (isDelete)
					return (int)eDeleteResult.UseAnotherPlace;
				else
					throw;
					
			}
			catch
			{
				if (isDelete)
					return (int)eDeleteResult.Failed;
				else
					throw;
			}
			
			return (int)eDeleteResult.Success;
		}
	}
}