using Microsoft.AspNetCore.Identity;

namespace MohDemo.DataAccess.Data.Repository.IRepository
{
	public interface IUserRoleRepository : IRepository<IdentityUserRole<string>>
	{
	}
}