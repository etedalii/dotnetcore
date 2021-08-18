using Microsoft.AspNetCore.Identity;

namespace MohDemo.Models
{
	public class ApplicationRole : IdentityRole
	{
		public ApplicationRole() : base() { }
		public ApplicationRole(string name) : base(name)
		{
		}
	}
}