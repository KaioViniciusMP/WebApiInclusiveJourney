using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInclusiveJourney.Application.DTO.Request
{
    public class AuthRequest
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
