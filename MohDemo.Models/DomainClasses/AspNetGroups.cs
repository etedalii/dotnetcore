using System;
using System.Collections.Generic;

namespace MohDemo.Models
{
    public partial class AspNetGroups
    {
        public AspNetGroups()
        {
            AspNetForms = new HashSet<AspNetForms>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspNetForms> AspNetForms { get; set; }
    }
}
