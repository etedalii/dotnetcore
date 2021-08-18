using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MohDemo.Models
{
	public class ApplicationUser : IdentityUser
	{
		public ApplicationUser() : base()
		{

		}

		[Required(ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "RequierdError")]
		public string Name { get; set; }

		[Required(ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "RequierdError")]
		public string Family { get; set; }

		[Required(ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "RequierdError")]
		public bool IsAdmin { get; set; }

		[Required(ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "RequierdError")]
		[StringLength(15, ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "LengthError", MinimumLength = 3)]
		[Remote("CheckDuplicate", "SystemUser", AdditionalFields = "Id,Email2,PhoneNumber2", HttpMethod = "Post", ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "DuplicateError")]
		[NotMapped]
		public string UserName2 { get; set; }

		[Required(ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "RequierdError")]
		[StringLength(25, ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "LengthError", MinimumLength = 6)]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "PasswordError")]
		[DataType(DataType.Password)]
		[NotMapped]
		public string PasswordHash2 { get; set; }

		[DataType(DataType.Password)]
		[Compare("PasswordHash2", ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "PasswordAndConfirmAreUnmatched")]
		[NotMapped]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "RequierdError")]
		[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "EmailError")]
		[Remote("CheckDuplicate", "SystemUser", AdditionalFields = "Id,UserName2,PhoneNumber2", HttpMethod = "Post", ErrorMessageResourceType = typeof(Utility.Resources.Messages), ErrorMessageResourceName = "DuplicateError")]
		[NotMapped]
		public string Email2 { get; set; }

		[NotMapped]
		public string RoleId { get; set; }
	}
}