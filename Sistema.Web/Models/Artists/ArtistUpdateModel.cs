using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class ArtistUpdateModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string fullname { get; set; }
        public int? mainroleid { get; set; }
        public string projectsworked { get; set; }
        public string cost { get; set; }
        public DateTime? costingdate { get; set; }
        public int? costinguserid { get; set; }
        public string email { get; set; }
        public string alternativecontact { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string imgartist { get; set; }
        public int? proveedorid { get; set; }
        [Required]
        public int iduserumod { get; set; }
    }
}
