using MohDemo.Models;
using System.Collections.Generic;

namespace MohDemo.DataAccess.Data.Repository.IRepository
{
    public interface IUserRepository : IRepository<ApplicationUser>
	{
		void LockUser(string userId);
		void UnLockUser(string userId);
		string GetFullName(string userName);
		string GetUserId(string userName);
	}
}