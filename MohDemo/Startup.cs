using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MohDemo.DataAccess.Data;
using MohDemo.DataAccess.Data.Initializer;
using MohDemo.DataAccess.Data.Repository;
using MohDemo.DataAccess.Data.Repository.IRepository;
using MohDemo.Utility;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MohDemo
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<IdentityOptions>(options =>
			{
				// Password settings.
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 8;
				options.Password.RequiredUniqueChars = 1;
				// Lockout settings.
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
				options.Lockout.MaxFailedAccessAttempts = 30;

				// User settings.
				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+=";
				options.User.RequireUniqueEmail = false;
			});

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));
			services.AddDatabaseDeveloperPageExceptionFilter();

			services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			//Inittializer section
			services.AddScoped<IDbInitializer, DbInitializer>();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(15);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			services.ConfigureApplicationCookie(options =>
			{
				options.AccessDeniedPath = new PathString("/Identity/Account/Login");
				options.Cookie.Name = "Cookie";
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
				options.LoginPath = new PathString("/Identity/Account/Login");
				options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
				options.SlidingExpiration = true;
			});

			services.AddSingleton<IEmailSender, EmailSender>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddControllersWithViews().AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.ContractResolver = new DefaultContractResolver();
			}).AddRazorRuntimeCompilation();

			services.AddScoped<IAuthorizationHandler, PermissionHandler>();
			services.AddAuthorization(options =>
			{
				options.AddPolicy("Authorization", policyCorrectUser =>
				{
					policyCorrectUser.Requirements.Add(new AuthorizationRequirement());
				});
			});

			services.AddRazorPages();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				//app.UseExceptionHandler("/Admin/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}
