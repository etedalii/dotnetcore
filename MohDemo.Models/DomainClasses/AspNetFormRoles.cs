using System;
using System.Collections.Generic;

namespace MohDemo.Models
{
    public partial class AspNetFormRoles
    {
        public string RoleId { get; set; }
        public int AspNetFormId { get; set; }

        public virtual AspNetForms AspNetForm { get; set; }
    }
}
