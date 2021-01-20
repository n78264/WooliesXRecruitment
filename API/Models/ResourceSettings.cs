using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ResourceSettings
    {
        public string UserName { get; set; }
        public string BaseUrl { get; set; }
        public string RelativePath { get; set; }
        public string Token { get; set; }
    }
}
