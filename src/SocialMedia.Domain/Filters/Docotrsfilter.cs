using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Filters
{
    public class Docotrsfilter:MasterFilter
    {
       
        public string? Name { get; set; }
        public string? Title { get; set; }
    }
}
