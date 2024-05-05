using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Models
{
    public class Password
    {
        public int RequiredLength { get; set; } = 10;
        public int RequiredUniqueChars { get; set; } = 1;
        public bool RequireNonAlphanumeric { get; set; } = true;
        public bool RequireLowercase { get; set; } = true;
        public bool RequireUppercase { get; set; } = true;
        public bool RequireDigit { get; set; } = true;



    }
}
