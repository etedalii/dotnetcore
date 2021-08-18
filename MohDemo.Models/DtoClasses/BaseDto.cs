using System.Collections.Generic;

namespace MohDemo.Models.DtoClasses
{
	public abstract class BaseDto<T>
	{
		public IEnumerable<T> data { get; set; }
		public int draw { get; set; }
		public int recordsTotal { get; set; }
		public int recordsFiltered { get; set; }
		public int Id { get; set; }
		public int start { get; set; }
		public int length { get; set; }

		public Dictionary<string, string> Search { get; set; }
		public Dictionary<string, string> Order { get; set; }
	}
}