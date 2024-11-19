using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInclusiveJourney.Application.DTO.Request
{
    public class FavoriteAndRemovedPlaceFavoritedRequest
    {
        public int PlacesCode { get; set; }
        public int PersonCode { get; set; }
    }
}
