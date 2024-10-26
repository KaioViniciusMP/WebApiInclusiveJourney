using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInclusiveJourney.Application.DTO.Response
{
    public class PersonResponse
    {
        public int Codigo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string DisabilityType { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string AdditionalInfo { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string Number { get; set; }
        public string State { get; set; }
        public string Username { get; set; }
        public string UserDescription { get; set; }
        public string Avatar { get; set; }
    }
}
