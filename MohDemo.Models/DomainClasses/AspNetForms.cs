using System;
using System.Collections.Generic;

namespace MohDemo.Models
{
    public partial class AspNetForms
    {
        public AspNetForms()
        {
            AspNetFormRoles = new HashSet<AspNetFormRoles>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
        public int AspNetGroupId { get; set; }

        public virtual AspNetGroups AspNetGroup { get; set; }
        public virtual ICollection<AspNetFormRoles> AspNetFormRoles { get; set; }
    }
}
