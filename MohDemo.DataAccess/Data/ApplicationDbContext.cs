using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MohDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
