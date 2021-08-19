using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MohDemo.Models;
using MohDemo.Models.DomainClasses;
using MohDemo.Utility;
using System;
using System.Linq;

namespace MohDemo.DataAccess.Data.Initializer
{
	public class DbInitializer : IDbInitializer
	{
		private readonly ApplicationDbContext context;
		private readonly UserManager<IdentityUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			context = db;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}
		public void Initialize()
		{
			try
			{
				if (context.Database.GetPendingMigrations().Count() > 0)
				{
					context.Database.Migrate();
				}
			}
			catch (Exception)
			{
				throw;
			}

			if (context.Roles.Any(_ => _.Name == "Administrator"))
				return;

			roleManager.CreateAsync(new IdentityRole("Administrator")).GetAwaiter().GetResult();

			userManager.CreateAsync(new ApplicationUser()
			{
				Name = "Mohammad",
				Family = "Etedali",
				UserName = "etedali",
				Email = "etedali@gmail.com",
				PhoneNumber = "4167221611",
				PhoneNumberConfirmed = true,
				EmailConfirmed = true,
				IsAdmin = true,
			}, "Admin123*").GetAwaiter().GetResult();


			IdentityUser user = context.Users.Where(_ => _.UserName == "etedali").FirstOrDefault();
			userManager.AddToRoleAsync(user, "Administrator").GetAwaiter().GetResult();
			context.People.Add(new Person() { UserId = user.Id });

			context.SaveChanges();

			#region InsertProvincesAndCities

			context.Database.ExecuteSqlRaw(@"DECLARE @tags NVARCHAR(500) = 'تهران,آذربایجان شرقی,آذربایجان غربی,اردبیل,اصفهان ,ایلام,بوشهر,چهارمحال و بختیاری,خوزستان,زنجان ,سمنان ,سیستان و بلوچستان ,فارس ,قزوین ,قم ,کردستان ,کرمان ,کرمانشاه ,کهکیلویه و بویراحمد ,گلستان,گیلان,لرستان,مازندران,مرکزی,هرمزگان,همدان,یزد ,خراسان جنوبی ,خراسان رضوی ,خراسان شمالی,البرز';  
                                            DECLARE @Id INT
                                            SET @Id = 1
                                            DECLARE dbcursor CURSOR
                                            FOR
                                                SELECT  value
                                                FROM    STRING_SPLIT(@tags, ',')
                                                WHERE   RTRIM(value) <> '';
                                                        DECLARE @item NVARCHAR(200);
                                                        OPEN dbcursor;
                                                        FETCH NEXT FROM dbcursor INTO @item;
                                                        WHILE @@FETCH_STATUS = 0
                                                BEGIN
                                            IF(@item <> ' ')
                                            BEGIN
                                            INSERT INTO dbo.Provinces
                                                    (Code, Name)
                                            VALUES(CONVERT(NVARCHAR, @Id),
                                                      RTRIM(LTRIM(@item)))
                                              SET @Id = SCOPE_IDENTITY() + 1;
                                                        END
                                                                FETCH NEXT FROM dbcursor INTO @item;
                                                        END;
                                                        CLOSE dbcursor;
                                                        DEALLOCATE dbcursor; ");

			context.Database.ExecuteSqlRaw(@"INSERT INTO dbo.Cities
        ( ProvinceId, Name )
VALUES 
( 1, N'آدران اسلامشهر 1' ), 
( 1, N'آدران اسلامشهر 2' ), 
( 1, N'آدران کرج' ), 
( 1, N'احمدآباد مستوفی اسلامشهر' ), 
( 1, N'اسلامشهر' ), 
( 1, N'اشتهارد' ), 
( 1, N'بومهن' ), 
( 1, N'پاکدشت' ), 
( 1, N'پیشوا' ), 
( 1, N'تهران' ), 
( 1, N'جاجرود' ), 
( 1, N'چهاردانگه' ), 
( 1, N'رودهن' ), 
( 1, N'رباط کریم' ), 
( 1, N'زرین دشت' ), 
( 1, N'سرخاب' ), 
( 1, N'سولوقان' ), 
( 1, N'شمس آباد' ), 
( 1, N'شهریار' ), 
( 1, N'طالقان' ), 
( 1, N'فردوس کرج' ), 
( 1, N'فشم' ), 
( 1, N'فیروزکوه' ), 
( 1, N'قلعه حسن خان' ), 
( 1, N'کرج' ), 
( 1, N'کردان' ), 
( 1, N'کهریزک' ), 
( 1, N'گچسر' ), 
( 1, N'لواسان' ), 
( 1, N'ملارد' ), 
( 1, N'میگون' ), 
( 1, N'نصیرآباد' ), 
( 1, N'ورامین' ), 
( 1, N'هشتگرد' ), 
( 1, N'دماوند' ), 
( 1, N'آلارد' ), 
( 2, N'ملکان' ), 
( 2, N'میانه' ), 
( 2, N'ممقان' ), 
( 2, N'مرند' ), 
( 2, N'مراغه' ), 
( 2, N'کلیبر' ), 
( 2, N'قره چمن' ), 
( 2, N'عجب شیر' ), 
( 2, N'شبستر' ), 
( 2, N'سراب' ), 
( 2, N'جلفا' ), 
( 2, N'تبریز' ), 
( 2, N'بستان آباد تبریز' ), 
( 2, N'بستان آباد سراب' ), 
( 2, N'بناب تبریز' ), 
( 2, N'اهر' ), 
( 2, N'آذر شهر' ), 
( 3, N'ارومیه' ), 
( 3, N'بوکان' ), 
( 3, N'پیرانشهر' ), 
( 3, N'سردشت' ), 
( 3, N'مهاباد' ), 
( 3, N'نقده' ), 
( 3, N'میاندو آب' ), 
( 3, N'سلماس' ), 
( 3, N'ماکو' ), 
( 3, N'خوی' ), 
( 3, N'تکاب' ), 
( 3, N'خوی' ), 
( 3, N'ماکو' ), 
( 4, N'مشکین شهر' ), 
( 4, N'گرمی' ), 
( 4, N'سرعین' ), 
( 4, N'خلخال' ), 
( 4, N'پارس آباد' ), 
( 4, N'اردبیل' ), 
( 5, N'اردستان' ), 
( 5, N'آران و بیدگل' ), 
( 5, N'اصفهان' ), 
( 5, N'خمینی شهر' ), 
( 5, N'خوانسار' ), 
( 5, N'زرین شهر' ), 
( 5, N'زواره' ), 
( 5, N'قمصر' ), 
( 5, N'کاشان' ), 
( 5, N'شاهین شهر' ), 
( 5, N'سمیرم' ), 
( 5, N'شهرضا' ), 
( 5, N'فلاورجان' ), 
( 5, N'نائین' ), 
( 5, N'نجف آباد' ), 
( 5, N'نطنز' ), 
( 5, N'مورچه خورت' ), 
( 5, N'مبارکه' ), 
( 5, N'گلپایگان' ), 
( 5, N'باغ بهادران' ), 
( 5, N'دهاقان' ), 
( 5, N'داران' ), 
( 5, N'فریدون شهر' ), 
( 5, N'فولادشهر' ), 
( 5, N'شهرضا' ), 
( 6, N'دره شهر' ), 
( 6, N'مهران' ), 
( 6, N'سرآبله' ), 
( 6, N'دهلران' ), 
( 6, N'ایلام' ), 
( 6, N'آبدانان' ), 
( 7, N'آبدان' ), 
( 7, N'اهرم' ), 
( 7, N'بوشهر' ), 
( 7, N'برازجان' ), 
( 7, N'بندردیر' ), 
( 7, N'بندردیلم' ), 
( 7, N'بندرریگ' ), 
( 7, N'خورموج' ), 
( 7, N'عسلویه' ), 
( 7, N'بندر گناوه' ), 
( 8, N'لردگان' ), 
( 8, N'فارسان' ), 
( 8, N'شهرکرد' ), 
( 8, N'کوهرنگ' ), 
( 8, N'دهنو' ), 
( 8, N'بروجن' ), 
( 8, N'اردل' ), 
( 9, N'اهواز' ), 
( 9, N'ایذه' ), 
( 9, N'اندیمشک' ), 
( 9, N'آبادان' ), 
( 9, N'آغاجاری' ), 
( 9, N'بهبهان' ), 
( 9, N'بندرماهشهر' ), 
( 9, N'دزفول' ), 
( 9, N'رامهرمز' ), 
( 9, N'خرمشهر' ), 
( 9, N'جزیره مینو' ), 
( 9, N'سربندر' ), 
( 9, N'گتوند' ), 
( 9, N'سوسنگرد' ), 
( 9, N'شوش' ), 
( 9, N'شوشتر' ), 
( 9, N'مسجدسلیمان' ), 
( 9, N'هویزه' ), 
( 10, N'خرمدره' ), 
( 10, N'زنجان' ), 
( 10, N'خدابنده' ), 
( 10, N'ابهر' ), 
( 11, N'دامغان' ), 
( 11, N'سمنان' ), 
( 11, N'مهدی شهر' ), 
( 11, N'شهمیرزاد' ), 
( 11, N'شاهرود' ), 
( 11, N'گرمسار' ), 
( 12, N'نیک شهر' ), 
( 12, N'سراوان' ), 
( 12, N'زابل' ), 
( 12, N'زابلی' ), 
( 12, N'زاهدان' ), 
( 12, N'خاش' ), 
( 12, N'چابهار' ), 
( 12, N'ایرانشهر' ), 
( 13, N'ارسنجان' ), 
( 13, N'آباده' ), 
( 13, N'جهرم' ), 
( 13, N'اقلید' ), 
( 13, N'داراب' ), 
( 13, N'مرودشت' ), 
( 13, N'فسا' ), 
( 13, N'لار' ), 
( 13, N'لامرد' ), 
( 13, N'کازرون' ), 
( 13, N'شیراز' ), 
( 13, N'گراش' ), 
( 14, N'قزوین' ), 
( 14, N'بوئین زهرا' ), 
( 14, N'تاکستان' ), 
( 15, N'راهجرد' ), 
( 15, N'سلفچگان' ), 
( 15, N'قم' ), 
( 15, N'قمرود' ), 
( 16, N'کامیاران' ), 
( 16, N'سنندج' ), 
( 16, N'سقز' ), 
( 16, N'بانه' ), 
( 16, N'قروه' ), 
( 16, N'مریوان' ), 
( 16, N'دیواندره' ), 
( 16, N'بیجار' ), 
( 17, N'بردسیر' ), 
( 17, N'بافت' ), 
( 17, N'بم' ), 
( 17, N'جیرفت' ), 
( 17, N'سرچشمه' ), 
( 17, N'زرند' ), 
( 17, N'رفسنجان' ), 
( 17, N'سیرجان' ), 
( 17, N'شهر بابک' ), 
( 17, N'کرمان' ), 
( 17, N'گلبافت' ), 
( 17, N'کهنوج' ), 
( 18, N'کنگاور' ), 
( 18, N'کرمانشاه' ), 
( 18, N'کرند غرب' ), 
( 18, N'گیلان غرب' ), 
( 18, N'سومار' ), 
( 18, N'سرپل ذهاب' ), 
( 18, N'پاوه' ), 
( 19, N'سی سخت' ), 
( 19, N'یاسوج' ), 
( 20, N'کردکوی' ), 
( 20, N'کلاله' ), 
( 20, N'گرگان' ), 
( 20, N'گنبدکاووس' ), 
( 20, N'بندرترکمن' ), 
( 21, N'بندرانزلی' ), 
( 21, N'آستارا' ), 
( 21, N'آستانه اشرفیه' ), 
( 21, N'سیاهکل' ), 
( 21, N'رودبار' ), 
( 21, N'رودپشت' ), 
( 21, N'رودسر' ), 
( 21, N'رشت' ), 
( 21, N'لاهیجان' ), 
( 21, N'لنگرود' ), 
( 21, N'کوچصفهان' ), 
( 21, N'کسما' ), 
( 21, N'صومعه سرا' ), 
( 21, N'شاندرمن' ), 
( 21, N'قاضیان' ), 
( 21, N'فومن' ), 
( 21, N'لوشان' ), 
( 21, N'ماسال' ), 
( 21, N'ماسوله' ), 
( 22, N'خرم اباد' ), 
( 22, N'ازنا' ), 
( 22, N'الیگودرز' ), 
( 22, N'بروجرد' ), 
( 22, N'پلدختر' ), 
( 22, N'درود' ), 
( 23, N'محمود آباد' ), 
( 23, N'پلور' ), 
( 23, N'بهشهر' ), 
( 23, N'بابل' ), 
( 23, N'بابلسر' ), 
( 23, N'آمل' ), 
( 23, N'چالوس' ), 
( 23, N'جویبار' ), 
( 23, N'تنکابن' ), 
( 23, N'رامسر' ), 
( 23, N'ساری' ), 
( 23, N'سلمانشهر' ), 
( 23, N'مرزن آباد' ), 
( 23, N'هشتپر' ), 
( 23, N'نکا' ), 
( 23, N'نور' ), 
( 23, N'نوشهر' ), 
( 23, N'عباس آباد' ), 
( 23, N'قائمشهر' ), 
( 23, N'فریدونکنار' ), 
( 23, N'کلار آباد' ), 
( 23, N'کلاردشت' ), 
( 24, N'کمیجان' ), 
( 24, N'فراهان' ), 
( 24, N'نراق' ), 
( 24, N'محلات' ), 
( 24, N'ساوه' ), 
( 24, N'دلیجان' ), 
( 24, N'تفرش' ), 
( 24, N'خمین' ), 
( 24, N'آشتیان' ), 
( 24, N'اراک' ), 
( 24, N'آستانه اراک' ), 
( 25, N'ابوموسی' ), 
( 25, N'بندرخمیر' ), 
( 25, N'بندرعباس' ), 
( 25, N'بندرلنگه' ), 
( 25, N'جزیره هرمز' ), 
( 25, N'سرخون' ), 
( 25, N'میناب' ), 
( 25, N'قشم' ), 
( 25, N'کیش' ), 
( 26, N'کبودرآهنگ' ), 
( 26, N'نهاوند' ), 
( 26, N'ملایر' ), 
( 26, N'همدان' ), 
( 26, N'رزن' ), 
( 26, N'تویسرکان' ), 
( 26, N'اسدآباد همدان' ), 
( 26, N'تویسرکان' ), 
( 27, N'اردکان یزد' ), 
( 27, N'بافق' ), 
( 27, N'تفت' ), 
( 27, N'یزد' ), 
( 27, N'مهریز' ), 
( 27, N'میبد' ), 
( 27, N'طبس' ), 
( 28, N'بیرجند' ), 
( 28, N'درمیان' ), 
( 28, N'سرایان' ), 
( 28, N'سربیشه' ), 
( 28, N'فردوس' ), 
( 28, N'قائنات' ), 
( 28, N'نهبندان' ), 
( 29, N'بردسکن' ), 
( 29, N'تایباد' ), 
( 29, N'تربت جام' ), 
( 29, N'تربت حیدریه' ), 
( 29, N'چناران' ), 
( 29, N'خلیل‌آباد' ), 
( 29, N'خواف' ), 
( 29, N'درگز' ), 
( 29, N'رشتخوار' ), 
( 29, N'سبزوار' ), 
( 29, N'سرخس' ), 
( 29, N'فریمان' ), 
( 29, N'قوچان' ), 
( 29, N'کاشمر' ), 
( 29, N'کلات' ), 
( 29, N'گناباد' ), 
( 29, N'مشهد' ), 
( 29, N'مه ولات' ), 
( 29, N'نیشابور' ), 
( 30, N'بجنورد' ), 
( 30, N'شیروان' ), 
( 30, N'اسفراین' ), 
( 30, N'مانه وسملقان' ), 
( 30, N'جاجرم' ), 
( 30, N'فاروج' ),
( 31, N'کرج' ),
( 31, N'فردیس' ),
( 31, N'کمال شهر' ),
( 31, N'نظرآباد' ),
( 31, N'محمدشهر' ),
( 31, N'ماهدشت' ),
( 31, N'مشکین دشت' ),
( 31, N'هشتگرد' ),
( 31, N'ساوجبلاغ' ),
( 31, N'اشتهارد' ),
( 31, N'گرمده' ),
( 31, N'تنکمان' ),
( 31, N'طالقان' ),
( 31, N'آسارا' )");

			#endregion
		}

		public void SeedData()
		{
			try
			{
				if (context.Database.GetPendingMigrations().Count() > 0)
				{
					context.Database.Migrate();
				}
			}
			catch (Exception)
			{
				throw;
			}

			
			if (context.AspNetGroups.Count(_ => _.Id == (int)eGroupId.BasicDefinitions) == 0)
				context.AspNetGroups.Add(new AspNetGroups { Name = Utility.Resources.Titles.BasicDefinitions });

			if (context.AspNetGroups.Count(_ => _.Id == (int)eGroupId.Setting) == 0)
				context.AspNetGroups.Add(new AspNetGroups { Name = Utility.Resources.Titles.Setting });

			if (context.AspNetGroups.Count(_ => _.Id == (int)eGroupId.GeneralDefinition) == 0)
				context.AspNetGroups.Add(new AspNetGroups { Name = "GeneralDefinition" });

			context.SaveChanges();

			if (context.AspNetForms.Count(_ => _.Id == (int)eFormId.Zone) == 0)
				context.AspNetForms.Add(new AspNetForms() { Name = "ZoneDefinition", AspNetGroupId = (int)eGroupId.BasicDefinitions, Visible = true });

			if (context.AspNetForms.Count(_ => _.Id == (int)eFormId.Role) == 0)
				context.AspNetForms.Add(new AspNetForms() { Name = Utility.Resources.Titles.RoleDefinition, AspNetGroupId = (int)eGroupId.Setting, Visible = true });

			if (context.AspNetForms.Count(_ => _.Id == (int)eFormId.RoleToForm) == 0)
				context.AspNetForms.Add(new AspNetForms() { Name = Utility.Resources.Titles.RoleToForm, AspNetGroupId = (int)eGroupId.Setting, Visible = true });

			if (context.AspNetForms.Count(_ => _.Id == (int)eFormId.SystemUser) == 0)
				context.AspNetForms.Add(new AspNetForms() { Name = "SystemUser", AspNetGroupId = (int)eGroupId.Setting, Visible = true });

			context.SaveChanges();
		}
	}
}