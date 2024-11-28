using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInclusiveJourney.Application.DTO.Request
{
    public class RequestPlace
    {
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
        public string relacaoTutelado { get; set; }
        public int ZoneCode { get; set; }
        public bool IsFavorite { get; set; } = false;
        public int ZoneCategorie { get; set; }
        public string ImageStream { get; set; } // Novo campo para o stream de imagem
        public string ImageName { get; set; } // Novo campo para o nome da imagem
    }
}
