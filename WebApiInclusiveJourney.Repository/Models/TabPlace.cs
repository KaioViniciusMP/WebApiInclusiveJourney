using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInclusiveJourney.Repository.Models
{
    public class TabPlaces
    {
        [Key]
        public int Codigo { get; set; }
        public string NameLocal { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string NumberHome { get; set; }
        public string State { get; set; }
        public string OpeningHours { get; set; }
        public string LocalAssessment { get; set; }
        public string Description { get; set; }
        public string TypeAcessibility { get; set; }
        public int ZoneCode { get; set; }
        public int ZoneCategorie { get; set; }
        public bool IsFavorite { get; set; }
    }
}
