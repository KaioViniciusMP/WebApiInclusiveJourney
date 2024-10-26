using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInclusiveJourney.Application.DTO.Request
{
    public class ForgotPasswordRequest
    {
        public string email { get; set; }
        public string newPassword { get; set; }
    }
}
