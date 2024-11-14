using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInclusiveJourney.Repository.Models
{
    public class TabPlaceFavorite_PersonCode
    {
        [Key]
        public long codigo { get; set; }
        public int PlacesCode { get; set; }
        public int PersonCode { get; set; }
    }
}
