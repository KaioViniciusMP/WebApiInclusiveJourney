using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInclusiveJourney.Application.DTO.Response
{
    public class RegistrarImagePlaceResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public PlacesResponse? preview { get; set; }
    }
}
