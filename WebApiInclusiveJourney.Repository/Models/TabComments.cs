using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInclusiveJourney.Repository.Models
{
    public class TabComments
    {
        [Key]
        public int codigo { get; set; }
        public string namePerson { get; set; }
        public string description { get; set; }
    }
}
