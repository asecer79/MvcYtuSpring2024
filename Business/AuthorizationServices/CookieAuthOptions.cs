using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AuthorizationServices
{
    public class CookieAuthOptions
    {
        public string? Name { get; set; }
        public string? LoginPath { get; set; }
        public string? LogoutPath { get; set; }
        public string? AccessDeniedPath { get; set; }
        public bool SlidingExpiration { get; set; }
        public int TimeOut { get; set; }
    }
}
