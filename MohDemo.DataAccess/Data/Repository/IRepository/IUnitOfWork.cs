using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohDemo.DataAccess.Data.Repository.IRepository
{
	public interface IUnitOfWork : IDisposable
	{
		ISp_Call Sp_Call { get; }
		IAspNetFormRepository AspNetForm { get; }
		IAspNetFormRoleRepository AspNetFormRole { get; }
		IAspNetGroupRepository AspNetGroup { get; }
		IPersonRepository Person { get; }
		IRoleRepository Role { get; }
		IUserRoleRepository UserRole { get; }
		IUserRepository Users { get; }

		int Save(bool isDelete = false);
		void BeginTransaction();
		void Commit();
		void Rollback();
	}
}
