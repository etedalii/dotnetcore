using MohDemo.Models.DomainClasses;
using System;

namespace MohDemo.Models.BaseFolder
{
	public partial class BaseModel
	{
		public int CreatorId { get; set; }
		public DateTime CreationDate { get; set; }
		public int? LastModifierId { get; set; }
		public DateTime? LastModificationDate { get; set; }

		public virtual Person Creator { get; set; }
		public virtual Person LastModifier { get; set; }
	}
}