using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInclusiveJourney.Application.DTO.Request
{
    public class RegistrarImagePlaceRequest
    {
        public int placeCode { get; set; }
        public string ImageName { get; set; }
        public string ImageStream { get; set; }
    }
}
