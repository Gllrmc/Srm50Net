using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class ArtistViewModel
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public int? mainroleid { get; set; }
        public string mainrole { get; set; }
        public string projectsworked { get; set; }
        public string cost { get; set; }
        [DataType(DataType.Date)]
        public DateTime? costingdate { get; set; }
        public int? costinguserid { get; set; }
        public string email { get; set; }
        public string alternativecontact { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string imgartist { get; set; }
        public int? proveedorid { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
