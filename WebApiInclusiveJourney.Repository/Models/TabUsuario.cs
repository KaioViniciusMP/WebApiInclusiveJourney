using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInclusiveJourney.Repository.Models
{
    public class TabUsuario
    {
        [Key]
        public int codigo { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("email")]
        public string email { get; set; }

        [Column("password")]
        public string password { get; set; }
    }
}
