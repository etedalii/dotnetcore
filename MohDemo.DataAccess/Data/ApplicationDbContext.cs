using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MohDemo.Models;
using MohDemo.Models.DomainClasses;

namespace MohDemo.DataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public virtual DbSet<AspNetFormRoles> AspNetFormRoles { get; set; }
		public virtual DbSet<AspNetForms> AspNetForms { get; set; }
		public virtual DbSet<AspNetGroups> AspNetGroups { get; set; }
		public virtual DbSet<Person> People { get; set; }
		public object PatterntTypes { get; internal set; }
	}
}
